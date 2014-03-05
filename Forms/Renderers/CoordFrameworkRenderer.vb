#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class CoordFrameworkRenderer

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public EquatorPenWidth As Int32 = 2
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ConvertCoords As [Delegate]
    Public GreatCircleResolutionDeg As Double
    Public GridLinesPerQuadrant As Double
    Public PriAxisDirection As ISFT
    Public ICoordExpPri As ICoordExp
    Public ICoordExpSec As ICoordExp
    Public ForeColor As Color
    Public BackColor As Color
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

    Private pPriBackgroundLines As ArrayList
    Private pPriForegroundLines As ArrayList
    Private pSecBackgroundLines As ArrayList
    Private pSecForegroundLines As ArrayList
    Private pEquatorBackgroundLines As ArrayList
    Private pEquatorForegroundLines As ArrayList

    Private pBackEquatorPlotPen As Drawing.Pen
    Private pForeEquatorPlotPen As Drawing.Pen

    Private pOrthographicProjectionCalculator As OrthographicProjectionCalculator
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As CoordFrameworkRenderer
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As CoordFrameworkRenderer = New CoordFrameworkRenderer
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pPriBackgroundLines = New ArrayList
        pPriForegroundLines = New ArrayList
        pSecBackgroundLines = New ArrayList
        pSecForegroundLines = New ArrayList
        pEquatorBackgroundLines = New ArrayList
        pEquatorForegroundLines = New ArrayList

        pOrthographicProjectionCalculator = OrthographicProjectionCalculator.GetInstance

        ' set (starting) values

        With ScopeLibrary.Settings.GetInstance
            ForeColor = .ScopePilotForegroundPlotPen
            BackColor = .ScopePilotBackgroundPlotPen
            GreatCircleResolutionDeg = .ScopePilotGreatCircleResolutionDeg
            GridLinesPerQuadrant = .ScopePilotGridLinesPerQuadrant
        End With

        PriAxisDirection = Rotation.CW
        ICoordExpPri = CoordExpFactory.GetInstance.Build(CoordExpType.WholeNumDegree)
        ICoordExpSec = CoordExpFactory.GetInstance.Build(CoordExpType.WholeNumNegDegree)
    End Sub

    Public Shared Function GetInstance() As CoordFrameworkRenderer
        Return New CoordFrameworkRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
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
                    ByVal RimUlPoint As Point)

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

        setColors()
        setEquatorialPens()
        setBasicPointDataParms()
    End Sub

    Public Sub CalcGreatCircles()
        pPriBackgroundLines.Clear()
        pPriForegroundLines.Clear()
        pSecBackgroundLines.Clear()
        pSecForegroundLines.Clear()
        pEquatorBackgroundLines.Clear()
        pEquatorForegroundLines.Clear()

        Dim stepRad As Double = Units.OneRev / pGlobeRadius
        If stepRad < GreatCircleResolutionDeg * Units.DegToRad Then
            stepRad = GreatCircleResolutionDeg * Units.DegToRad
        End If
        Dim gridResolutionRad As Double = Units.QtrRev / GridLinesPerQuadrant

        ' parallel to primary axis
        For pPd.SecRad = -Units.QtrRev To Units.QtrRev Step gridResolutionRad
            For currentStep As Double = 0 To Units.OneRev - stepRad Step stepRad
                Dim beginPointData As PointData = CType(pPd.Clone, PointData)
                beginPointData.PriRad = currentStep
                calcPoint(beginPointData)

                Dim endPointData As PointData = CType(pPd.Clone, PointData)
                endPointData.PriRad = currentStep + stepRad
                calcPoint(endPointData)

                If beginPointData.BeyondMapRange AndAlso endPointData.BeyondMapRange Then
                    pSecBackgroundLines.Add(New Object() {beginPointData.Point, endPointData.Point})
                Else
                    pSecForegroundLines.Add(New Object() {beginPointData.Point, endPointData.Point})
                End If
            Next
        Next

        ' emphasized equatorial great circle
        For currentStep As Double = 0 To Units.OneRev - stepRad Step stepRad
            Dim beginPointData As PointData = CType(pPd.Clone, PointData)
            beginPointData.PriRad = currentStep
            beginPointData.SecRad = 0
            calcPoint(beginPointData)

            Dim endPointData As PointData = CType(pPd.Clone, PointData)
            endPointData.PriRad = currentStep + stepRad
            endPointData.SecRad = 0
            calcPoint(endPointData)

            If beginPointData.BeyondMapRange AndAlso endPointData.BeyondMapRange Then
                pEquatorBackgroundLines.Add(New Object() {beginPointData.Point, endPointData.Point})
            Else
                pEquatorForegroundLines.Add(New Object() {beginPointData.Point, endPointData.Point})
            End If
        Next

        ' parallel to secondary axis
        For pPd.PriRad = 0 To Units.OneRev - stepRad Step gridResolutionRad
            For currentStep As Double = -Units.QtrRev To Units.QtrRev Step stepRad
                Dim beginPointData As PointData = CType(pPd.Clone, PointData)
                beginPointData.SecRad = currentStep
                calcPoint(beginPointData)

                Dim endPointData As PointData = CType(pPd.Clone, PointData)
                endPointData.SecRad = currentStep + stepRad
                calcPoint(endPointData)

                If beginPointData.BeyondMapRange AndAlso endPointData.BeyondMapRange Then
                    pPriBackgroundLines.Add(New Object() {beginPointData.Point, endPointData.Point})
                Else
                    pPriForegroundLines.Add(New Object() {beginPointData.Point, endPointData.Point})
                End If
            Next
        Next
    End Sub

    Public Sub DrawBackgroundGreatCircles()
        drawLines(pBackPlotPen, pPriBackgroundLines)
        drawLines(pBackPlotPen, pSecBackgroundLines)
        drawLines(pBackEquatorPlotPen, pEquatorBackgroundLines)
    End Sub

    Public Sub DrawForegroundGreatCircles()
        drawLines(pForePlotPen, pPriForegroundLines)
        drawLines(pForePlotPen, pSecForegroundLines)
        drawLines(pForeEquatorPlotPen, pEquatorForegroundLines)
    End Sub

    Public Sub DrawRulers()
        Dim stepRad As Double = Units.QtrRev / GridLinesPerQuadrant

        ' primary axis ruler
        For PriRad As Double = 0 To Units.OneRev Step stepRad
            pPd.PriRad = PriRad
            pPd.SecRad = 0
            calcPoint(pPd)
            If Not pPd.BeyondMapRange Then
                pRulerText = ICoordExpPri.ToString(PriRad)
                pG.DrawString(pRulerText, pRulerFont, pRulerBrush, pPd.Point.X, pPd.Point.Y)
            End If
        Next

        ' secondary axis ruler: front side
        ' don't display 0, 180 deg equivalent ruler markings as they conflict with primary ruler display
        For SecRad As Double = -Units.QtrRev To Units.QtrRev Step stepRad
            pPd.SecRad = SecRad
            pPd.PriRad = 0
            If Not (SecRad > -stepRad / 2 AndAlso SecRad < +stepRad / 2) Then
                calcPoint(pPd)
                If Not pPd.BeyondMapRange Then
                    pRulerText = ICoordExpSec.ToString(SecRad)
                    pG.DrawString(pRulerText, pRulerFont, pRulerBrush, pPd.Point.X, pPd.Point.Y)
                End If
            End If
        Next

        ' secondary axis ruler: back side
        ' don't display 0, 180 deg equivalent ruler markings as they conflict with primary ruler display
        For SecRad As Double = -Units.QtrRev + stepRad To Units.QtrRev - stepRad Step stepRad
            pPd.SecRad = SecRad
            pPd.PriRad = Units.HalfRev
            If Not (SecRad > -stepRad / 2 AndAlso SecRad < +stepRad / 2) Then
                calcPoint(pPd)
                If Not pPd.BeyondMapRange Then
                    pRulerText = ICoordExpSec.ToString(SecRad)
                    pG.DrawString(pRulerText, pRulerFont, pRulerBrush, pPd.Point.X, pPd.Point.Y)
                End If
            End If
        Next
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Sub setEquatorialPens()
        pBackEquatorPlotPen = CType(pBackPlotPen.Clone, Pen)
        pBackEquatorPlotPen.Width = EquatorPenWidth
        pForeEquatorPlotPen = CType(pForePlotPen.Clone, Pen)
        pForeEquatorPlotPen.Width = EquatorPenWidth
    End Sub

    Private Sub setColors()
        pBackPlotPen.Color = BackColor
        pForePlotPen.Color = ForeColor
        pRulerBrush.Color = ForeColor
    End Sub

    Private Sub setBasicPointDataParms()
        pPd.GlobeRadius = pGlobeRadius
        pPd.Xcenter = pGMidX
        pPd.Ycenter = pGMidY
    End Sub

    Private Sub calcPoint(ByRef pd As PointData)
        If ConvertCoords Is Nothing Then
            pOrthographicProjectionCalculator.CalcPoint(pd, PriAxisDirection)
        Else
            pOrthographicProjectionCalculator.CalcPoint(pd, PriAxisDirection, ConvertCoords)
        End If
    End Sub

    Private Sub drawLines(ByRef pen As Pen, ByRef lines As ArrayList)
        For Each points As Object() In lines
            pG.DrawLine(pen, CType(points(0), Point), CType(points(1), Point))
        Next
    End Sub

#End Region

End Class
