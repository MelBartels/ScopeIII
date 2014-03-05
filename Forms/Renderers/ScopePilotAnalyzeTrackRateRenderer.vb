#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class ScopePilotAnalyzeTrackRateRenderer
    Implements IScopePilotAnalyzer

#Region "Inner Classes"
    Private Class dataModel
        Public RaRad As Double
        Public DecRad As Double
        Public PointData As PointData
        Public TrackingRate As Double

        Public Sub New(ByVal RaRad As Double, ByVal decRad As Double, ByRef pointData As PointData, ByVal trackingRate As Double)
            Me.RaRad = RaRad
            Me.DecRad = decRad
            Me.PointData = pointData
            Me.TrackingRate = trackingRate
        End Sub
        Public Overrides Function ToString() As String
            Return RaRad * Units.RadToDeg & " " & DecRad * Units.RadToDeg & " " & TrackingRate * Units.RadToArcsec
        End Function
    End Class
#End Region

#Region "Constant Members"
    Public EquatorPenWidth As Int32 = 2
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public PriAxisDirection As ISFT
#End Region

#Region "Private and Protected Members"
    Private pG As Drawing.Graphics
    Private pPd As PointData
    Private pGSize As Size
    Private pGMidX As Int32
    Private pGMidY As Int32
    Private pBackPlotPen As Drawing.Pen
    Private pForePlotPen As Drawing.Pen
    Private pRulerText As String
    Private pRulerFont As Font
    Private pRulerBrush As SolidBrush
    Private pRimDimen As Int32
    Private pGlobeRadius As Double
    Private pRimUlPoint As Point

    Private pSpotRadius As Int32
    Private pConvertCoords As [Delegate]
    Private pCalculator As [Delegate]
    Private pAxis As Axis
    Private pStepResolutionDeg As Double
    Private pMinValue As Double
    Private pMaxValue As Double
    Private pDataModels As ArrayList
    Private pOrthographicProjectionCalculator As OrthographicProjectionCalculator
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As ScopePilotAnalyzeTrackRateRenderer
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As ScopePilotAnalyzeTrackRateRenderer = New ScopePilotAnalyzeTrackRateRenderer
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pAxis = Axis.GetInstance
        pOrthographicProjectionCalculator = OrthographicProjectionCalculator.GetInstance
        pDataModels = New ArrayList
        With ScopeLibrary.Settings.GetInstance
            StepResolutionDeg = .ScopePilotAnalysisStepResolutionDeg
            pSpotRadius = .ScopePilotAnalysisSpotRadius
        End With
        PriAxisDirection = Rotation.CW
    End Sub

    Public Shared Function GetInstance() As ScopePilotAnalyzeTrackRateRenderer
        Return New ScopePilotAnalyzeTrackRateRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ConvertCoords() As [Delegate]
        Get
            Return pConvertCoords
        End Get
        Set(ByVal value As [Delegate])
            pConvertCoords = value
        End Set
    End Property

    Public Property Calculator() As [Delegate]
        Get
            Return pCalculator
        End Get
        Set(ByVal value As [Delegate])
            pCalculator = value
        End Set
    End Property

    Public Property Axis() As Axis
        Get
            Return pAxis
        End Get
        Set(ByVal value As Axis)
            pAxis = value
        End Set
    End Property

    Public Property StepResolutionDeg() As Double
        Get
            Return pStepResolutionDeg
        End Get
        Set(ByVal value As Double)
            pStepResolutionDeg = value
        End Set
    End Property

    Public Property MaxValue() As Double
        Get
            Return pMaxValue
        End Get
        Set(ByVal value As Double)
            pMaxValue = value
        End Set
    End Property

    Public Property MinValue() As Double
        Get
            Return pMinValue
        End Get
        Set(ByVal value As Double)
            pMinValue = value
        End Set
    End Property

    Public Sub Init(ByRef G As Graphics, _
                    ByRef Pd As PointData, _
                    ByVal GSize As Size, _
                    ByVal GMidX As Int32, _
                    ByVal GMidY As Int32, _
                    ByVal BackPlotPen As Drawing.Pen, _
                    ByVal ForePlotPen As Drawing.Pen, _
                    ByVal RulerText As String, _
                    ByVal RulerFont As Font, _
                    ByVal RulerBrush As SolidBrush, _
                    ByVal RimDimen As Int32, _
                    ByVal GlobeRadius As Double, _
                    ByVal RimUlPoint As Point) Implements IScopePilotAnalyzer.Init

        Me.pG = G
        Me.pPd = Pd
        Me.pGSize = GSize
        Me.pGMidX = GMidX
        Me.pGMidY = GMidY
        Me.pBackPlotPen = BackPlotPen
        Me.pForePlotPen = ForePlotPen
        Me.pRulerText = RulerText
        Me.pRulerFont = RulerFont
        Me.pRulerBrush = RulerBrush
        Me.pRimDimen = RimDimen
        Me.pGlobeRadius = GlobeRadius
        Me.pRimUlPoint = RimUlPoint

        setBasicPointDataParms()
    End Sub

    Public Sub DrawAnalysis() Implements IScopePilotAnalyzer.DrawAnalysis
        buildDataModels()
        drawDataModels()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Sub buildDataModels()
        pDataModels.Clear()

        Dim stepRad As Double = Units.OneRev / pGlobeRadius
        If stepRad < StepResolutionDeg * Units.DegToRad Then
            stepRad = StepResolutionDeg * Units.DegToRad
        End If

        ' parallel to primary axis
        For pPd.SecRad = -Units.QtrRev To Units.QtrRev Step stepRad
            For currentStep As Double = 0 To Units.OneRev - stepRad Step stepRad
                Dim pointData As PointData = CType(pPd.Clone, PointData)
                pointData.PriRad = currentStep
                processPoint(pointData)
            Next
        Next

        ' parallel to secondary axis
        For pPd.PriRad = 0 To Units.OneRev - stepRad Step stepRad
            For currentStep As Double = -Units.QtrRev To Units.QtrRev Step stepRad
                Dim pointData As PointData = CType(pPd.Clone, PointData)
                pointData.SecRad = currentStep
                processPoint(pointData)
            Next
        Next
    End Sub

    Private Sub processPoint(ByRef pointData As PointData)
        Dim RaRad As Double = pointData.PriRad
        Dim DecRad As Double = pointData.SecRad
        If Math.Abs(DecRad).Equals(Units.QtrRev) Then
            Exit Sub
        End If

        Dim trackingRate As Double = getTrackRate(RaRad, DecRad)
        calcPoint(pointData)
        If pointData.BeyondMapRange Then
            Exit Sub
        End If
        ' only display above horizon (alt>=0)
        If pointData.SecRad < 0 Then
            Exit Sub
        End If

        pDataModels.Add(New dataModel(RaRad, DecRad, pointData, trackingRate))
    End Sub

    Private Function getTrackRate(ByVal RaRad As Double, ByVal DecRad As Double) As Double
        Return CDbl(pCalculator.DynamicInvoke(New Object() {pAxis, RaRad, DecRad}))
    End Function

    Private Sub calcPoint(ByRef pd As PointData)
        If ConvertCoords Is Nothing Then
            pOrthographicProjectionCalculator.CalcPoint(pd, PriAxisDirection)
        Else
            pOrthographicProjectionCalculator.CalcPoint(pd, PriAxisDirection, ConvertCoords)
        End If
    End Sub

    Private Sub drawDataModels()
        For Each dataModel As dataModel In pDataModels
            Dim red As Int32 = 0
            Dim green As Int32 = 0
            Dim blue As Int32 = 0

            If pMaxValue - pMinValue > 0 Then
                ' RGB ranges from:
                ' Red/Green 255:0 to 0:255, total of 256 colors
                ' Green/Blue 255:0 to 0:255, total of 256 colors
                ' total of 512 color values
                Dim colorPoint As Int32 = eMath.RInt((dataModel.TrackingRate * Units.RadToArcsec - pMinValue) / (pMaxValue - pMinValue) * 511)
                If colorPoint < 256 Then
                    red = 255 - colorPoint
                    green = colorPoint
                Else ' < 512
                    green = 255 - (colorPoint - 256)
                    blue = colorPoint - 256
                End If
                If red < 0 Then
                    red = 0
                End If
                If red > 255 Then
                    red = 255
                End If
                If green < 0 Then
                    green = 0
                End If
                If green > 255 Then
                    green = 255
                End If
                If blue < 0 Then
                    blue = 0
                End If
                If blue > 255 Then
                    blue = 255
                End If
            Else
                green = 255
            End If
            pRulerBrush.Color = Color.FromArgb(red, green, blue)
            'Debug.WriteLine(red & " " & green & " " & blue)
            pG.FillEllipse(pRulerBrush, dataModel.PointData.Point.X - pSpotRadius, dataModel.PointData.Point.Y - pSpotRadius, pSpotRadius * 2, pSpotRadius * 2)
        Next
    End Sub

    Private Sub setBasicPointDataParms()
        pPd.GlobeRadius = pGlobeRadius
        pPd.Xcenter = pGMidX
        pPd.Ycenter = pGMidY
    End Sub
#End Region
End Class
