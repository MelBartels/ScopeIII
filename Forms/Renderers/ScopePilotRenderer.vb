#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class ScopePilotRenderer
    Inherits RendererBase
    Implements IRenderer2MeasurementsGauge

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const RimRatio As Double = 0.95
    Public Const PointRadius As Int32 = 3
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public PriAxisDirection As ISFT
#End Region

#Region "Private and Protected Members"
    Private pPd As PointData
    Private pPointCoord As PointData
    Private pInitPointDatas(ConvertMatrix.NumberOfInits - 1) As PointData
    Private pHemisphere As Hemisphere
    Private pDisplayInits As Boolean
    Private pRimDimen As Int32
    Private pGlobeRadius As Double
    Private pRimUlPoint As Point
    Private pRimRect As Rectangle

    Private pRimColor As Color
    Private pRimThick As Int32
    Private pRimPen As Pen
    Private pGroundPlaneBrush As SolidBrush
    Private pBackPlotPen As Drawing.Pen
    Private pForePlotPen As Drawing.Pen
    Private pRulerText As String
    Private pRulerFont As Font
    Private pRulerBrush As SolidBrush
    Private pLineToPointPen As Pen
    Private pLineFromCenterColor As Drawing.Color
    Private pPointBrush As SolidBrush
    Private pPriPolyBrush As SolidBrush
    Private pSecPolyBrush As SolidBrush
    Private pSecPolyForeBrush As SolidBrush
    Private pSecPolyBackBrush As SolidBrush
    Private pPointForeColor As Drawing.Color
    Private pPointBackColor As Drawing.Color

    Private pSettings As ScopeLibrary.Settings
    Private CoordFrameworkRenderers As ArrayList
    Private pIScopePilotAnalyzers As ArrayList
    Private pOrthographicProjectionCalculator As OrthographicProjectionCalculator
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ScopePilotRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ScopePilotRenderer = New ScopePilotRenderer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        ToolTip = BartelsLibrary.Constants.GaugeToolTip
        WidthToHeightRatio = 1
        PriAxisDirection = Rotation.CW
        CoordFrameworkRenderers = New ArrayList
        pIScopePilotAnalyzers = New ArrayList
        pPd = PointData.GetInstance
        pPointCoord = PointData.GetInstance
        For init As Int32 = 0 To pInitPointDatas.Length - 1
            pInitPointDatas(init) = PointData.GetInstance
        Next
        pOrthographicProjectionCalculator = OrthographicProjectionCalculator.GetInstance

        With BartelsLibrary.Settings.GetInstance
            pRimColor = .GaugeRimColor
        End With
        pSettings = ScopeLibrary.Settings.GetInstance
        With pSettings
            pGroundPlaneBrush = New SolidBrush(.ScopePilotGroundPlane)
            pBackPlotPen = New Drawing.Pen(.ScopePilotBackgroundPlotPen)
            pForePlotPen = New Drawing.Pen(.ScopePilotForegroundPlotPen)
            pLineFromCenterColor = .ScopePilotClickToColor
            pPointForeColor = .ScopePilotClickToColor
            pRulerBrush = New SolidBrush(.ScopePilotRulerColor)
        End With

        pRulerFont = New Font(pFontFamilyName, 8, FontStyle.Regular)
        pRimThick = 1
        pRimPen = New Pen(pRimColor, pRimThick)
        pPriPolyBrush = New SolidBrush(CType(AxisColors.Primary.Tag, Drawing.Color))
        pPointBackColor = getLightColor(pPointForeColor, 64)
        pSecPolyForeBrush = New SolidBrush(CType(AxisColors.Secondary.Tag, Drawing.Color))
        pSecPolyBackBrush = New SolidBrush(getLightColor(pSecPolyForeBrush.Color, 128))
    End Sub

    Public Shared Function GetInstance() As ScopePilotRenderer
        Return New ScopePilotRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        Try
            Dim size As Int32 = getSize(width, height)

            MyBase.Render(g, size, size)

            decodeMeasurementsFromObject(MeasurementsFromObjectToRender)

            setBasicParms(size)
            setBasicPointDataParms()

            ' paint Background
            pG.Clear(pSettings.ScopePilotBackgroundColor)
            pG.FillEllipse(pGLinGradBrush, pRimRect)
            pG.DrawEllipse(pRimPen, pRimRect)

            ' set Z or tiertiary axis transform
            Dim myMatrix As New Matrix
            myMatrix.RotateAt(CType(pPd.TiltTierRad * Units.RadToDeg, Single), New PointF(pGMidX, pGMidY))
            pG.Transform = myMatrix

            For Each coordFrameworkRenderer As CoordFrameworkRenderer In CoordFrameworkRenderers
                coordFrameworkRenderer.Init(pG, _
                                            CType(pPd.Clone, PointData), _
                                            pGSize, _
                                            pGMidX, _
                                            pGMidY, _
                                            CType(pBackPlotPen.Clone, Pen), _
                                            CType(pForePlotPen.Clone, Pen), _
                                            pRulerText, _
                                            pRulerFont, _
                                            CType(pRulerBrush.Clone, SolidBrush), _
                                            pRimDimen, _
                                            pGlobeRadius, _
                                            pRimUlPoint)
            Next

            For Each analyzer As IScopePilotAnalyzer In pIScopePilotAnalyzers
                analyzer.Init(pG, _
                              CType(pPd.Clone, PointData), _
                              pGSize, _
                              pGMidX, _
                              pGMidY, _
                              CType(pBackPlotPen.Clone, Pen), _
                              CType(pForePlotPen.Clone, Pen), _
                              pRulerText, _
                              pRulerFont, _
                              CType(pRulerBrush.Clone, SolidBrush), _
                              pRimDimen, _
                              pGlobeRadius, _
                              pRimUlPoint)
            Next

            ' calc if point is beyond map range (projected in the background)
            calcPoint(pPd)
            If pPd.BeyondMapRange Then
                pSecPolyBrush = pSecPolyBackBrush
            Else
                pSecPolyBrush = pSecPolyForeBrush
            End If

            For Each coordFrameworkRenderer As CoordFrameworkRenderer In CoordFrameworkRenderers
                coordFrameworkRenderer.CalcGreatCircles()
                coordFrameworkRenderer.DrawBackgroundGreatCircles()
            Next

            If eMath.ValidRadPi(pPd.TiltSecRad) > 0 AndAlso pPointCoord.SecRad < 0 _
            OrElse eMath.ValidRadPi(pPd.TiltSecRad) < 0 AndAlso pPointCoord.SecRad > 0 Then
                ' draw secondary then ground then primary so that primary is on top of ground which is on top of secondary
                drawSecondaryArc()
                drawLineFromCenterToPoint()
            End If

            DrawGroundPlane()

            ' draw center point
            pG.FillEllipse(pRulerBrush, pGMidX - PointRadius, pGMidY - PointRadius, 2 * PointRadius, 2 * PointRadius)

            ' fill arc along primary axis from primary=0 to primary=pPointCoord.PriRad
            ' fill arc along secondary axis from secondary=0 to secondary=pPointCoord.SecRad
            If eMath.ValidRadPi(pPd.TiltSecRad) > 0 AndAlso pPointCoord.SecRad < 0 _
            OrElse eMath.ValidRadPi(pPd.TiltSecRad) < 0 AndAlso pPointCoord.SecRad > 0 Then
                ' draw primary on top of already drawn ground which is on top of already drawn secondary
                drawPrimaryArc()
            Else
                ' draw primary then secondary so that secondary is on top of primary which is on top of already drawn ground
                drawPrimaryArc()
                drawSecondaryArc()
                drawLineFromCenterToPoint()
            End If

            For Each coordFrameworkRenderer As CoordFrameworkRenderer In CoordFrameworkRenderers
                coordFrameworkRenderer.DrawForegroundGreatCircles()
            Next

            For Each analyzer As IScopePilotAnalyzer In pIScopePilotAnalyzers
                analyzer.DrawAnalysis()
            Next

            For Each coordFrameworkRenderer As CoordFrameworkRenderer In CoordFrameworkRenderers
                coordFrameworkRenderer.DrawRulers()
            Next

            drawCardinalDirections()

            ' draw outer rim to clean up presentation
            drawProjectionOuterEllipse(pRimRect)

            drawPointedToPoint(pPointCoord, pPointForeColor, pPointBackColor)

            displayInitPoints()

        Catch oex As OverflowException
            DebugTrace.WriteLine(oex.Message & oex.StackTrace)
        End Try

        Return g
    End Function

    Public Function MeasurementsToPoint(ByVal point As Point) As Object Implements IRenderer2MeasurementsGauge.MeasurementsToPoint
        Return pOrthographicProjectionCalculator.MeasurementsToPoint(point, CType(pPd.Clone, PointData), pGMidX, pGMidY, pRimDimen, PriAxisDirection)
    End Function

    Public Function InsideGauge(ByVal point As Point) As Boolean Implements IRenderer2MeasurementsGauge.InsideGauge
        Return Math.Sqrt(Math.Pow(Math.Abs(point.X - pGMidX), 2) + Math.Pow(Math.Abs(point.Y - pGMidY), 2)) <= pRimDimen / 2
    End Function

    Public Function MeasurementsFromObjectToRender() As Object Implements IRenderer2MeasurementsGauge.MeasurementsFromObjectToRender
        If ObjectToRender IsNot Nothing AndAlso ObjectToRender.GetType.Equals(GetType(ScopePilotObjectToRender)) Then
            Return ObjectToRender
        End If
        Return Nothing
    End Function

    Public Sub AddCoordFrameworkRenderer(ByRef CoordFrameworkRenderer As CoordFrameworkRenderer)
        CoordFrameworkRenderers.Add(CoordFrameworkRenderer)
    End Sub

    Public Sub RemoveCoordFrameworkRenderer(ByRef CoordFrameworkRenderer As CoordFrameworkRenderer)
        CoordFrameworkRenderers.Remove(CoordFrameworkRenderer)
    End Sub

    Public Sub ClearCoordFrameworkRenderers()
        CoordFrameworkRenderers.Clear()
    End Sub

    Public Sub AddIScopePilotAnalyzerRenderer(ByRef analyzer As IScopePilotAnalyzer)
        pIScopePilotAnalyzers.Add(analyzer)
    End Sub

    Public Sub RemoveIScopePilotAnalyzerRenderer(ByRef analyzer As IScopePilotAnalyzer)
        pIScopePilotAnalyzers.Remove(analyzer)
    End Sub

    Public Sub ClearIScopePilotAnalyzerRenderers()
        pIScopePilotAnalyzers.Clear()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Function getSize(ByVal width As Int32, ByVal height As Int32) As Int32
        If width > height Then
            Return height
        Else
            Return width
        End If
    End Function

    Private Sub decodeMeasurementsFromObject(ByRef [object] As Object)
        If [object] IsNot Nothing Then
            With CType([object], ScopePilotObjectToRender)
                pPointCoord.PriRad = .PriRad
                pPointCoord.SecRad = .SecRad
                pPd.TiltPriRad = .TiltPriRad
                pPd.TiltSecRad = .TiltSecRad
                pPd.TiltTierRad = .TiltTierRad
                pPriPolyBrush.Color = .PrimaryColor
                pSecPolyForeBrush.Color = .SecondaryColor
                pSecPolyBackBrush.Color = getLightColor(.SecondaryColor, 128)
                pDisplayInits = .DisplayInits
                For init As Int32 = 0 To pInitPointDatas.Length - 1
                    If pInitPointDatas(init) IsNot Nothing AndAlso .Inits(init) IsNot Nothing Then
                        pInitPointDatas(init).PriRad = .Inits(init).Z
                        pInitPointDatas(init).SecRad = .Inits(init).A
                    End If
                Next
                pHemisphere = .Hemisphere
            End With
        End If
    End Sub

    Private Sub calcPoint(ByRef pd As PointData)
        pOrthographicProjectionCalculator.CalcPoint(pd, PriAxisDirection)
    End Sub

    Private Sub setBasicParms(ByVal size As Int32)
        ' gauge circle
        pRimDimen = eMath.RInt(size * RimRatio)
        ' eg, if rimRatio=.5, then ul=.25 and lr=.75, centering rimRatio between 0 and 1
        Dim rimUlDimen As Int32 = eMath.RInt(size * (1 - RimRatio) / 2) + eMath.RInt(pRimThick / 2)
        Dim rimLrDimen As Int32 = eMath.RInt(size * (1 + RimRatio) / 2) - eMath.RInt(pRimThick / 2)
        pRimUlPoint = New Point(rimUlDimen, rimUlDimen)
        Dim rimLrPoint As New Point(rimLrDimen, rimLrDimen)
        Dim rimRectDimen As Int32 = eMath.RInt(pRimDimen - pRimThick)
        Dim rimSize As New Size(rimRectDimen, rimRectDimen)
        pRimRect = New Rectangle(pRimUlPoint, rimSize)
        setLinGradBrush(pSettings.ScopePilotGlobeBackgroundColor)
    End Sub

    Private Sub setBasicPointDataParms()
        pGlobeRadius = RimRatio * pGSize.Width / 2 - 1
        pPd.GlobeRadius = pGlobeRadius
        pPd.Xcenter = pGMidX
        pPd.Ycenter = pGMidY
    End Sub

    Private Sub drawPointedToPoint(ByRef pointData As PointData, ByRef foreColor As Color, ByRef backColor As Color)
        pPd.PriRad = pointData.PriRad
        pPd.SecRad = pointData.SecRad
        calcPoint(pPd)
        If pPd.BeyondMapRange Then
            pPointBrush = New SolidBrush(backColor)
        Else
            pPointBrush = New SolidBrush(foreColor)
        End If
        pG.FillEllipse(pPointBrush, pPd.Point.X - PointRadius, pPd.Point.Y - PointRadius, 2 * PointRadius, 2 * PointRadius)
    End Sub

    Private Sub drawPrimaryArc()
        pPd.SecRad = 0
        fillArc(pPd.PriRad, pPointCoord.PriRad, pPriPolyBrush)
    End Sub

    Private Sub drawSecondaryArc()
        pPd.PriRad = pPointCoord.PriRad
        fillArc(pPd.SecRad, pPointCoord.SecRad, pSecPolyBrush)
    End Sub

    Private Sub pPdDrawString(ByVal [string] As String)
        If pPd.BeyondMapRange Then
        Else
            pG.DrawString([string], pRulerFont, pRulerBrush, pPd.Point.X, pPd.Point.Y)
        End If
    End Sub

    Private Sub drawLineFromCenterToPoint()
        pPd.PriRad = pPointCoord.PriRad
        pPd.SecRad = pPointCoord.SecRad
        drawLineFromCenter(pPointForeColor, pLineFromCenterColor)
    End Sub

    Private Sub drawLineFromCenterTo_0_0()
        pPd.PriRad = 0
        pPd.SecRad = 0
        drawLineFromCenter(pPointForeColor, pLineFromCenterColor)
    End Sub

    Private Sub drawLineFromCenterToPriAxisPoint()
        pPd.PriRad = pPointCoord.PriRad
        pPd.SecRad = 0
        drawLineFromCenter(pPointForeColor, pLineFromCenterColor)
    End Sub

    Private Sub drawLineFromCenter(ByRef foreColor As Color, ByRef backColor As Color)
        calcPoint(pPd)
        If pPd.BeyondMapRange Then
            pLineToPointPen = New Pen(backColor)
        Else
            pLineToPointPen = New Pen(foreColor)
        End If
        pG.DrawLine(pLineToPointPen, pGMidPoint, pPd.Point)
    End Sub

    Private Sub drawLines(ByRef pen As Pen, ByRef lines As ArrayList)
        For Each points As Object() In lines
            pG.DrawLine(pen, CType(points(0), Point), CType(points(1), Point))
        Next
    End Sub

    Private Sub drawProjectionOuterEllipse(ByRef pRimRect As Rectangle)
        pG.DrawEllipse(pRimPen, pRimRect)
    End Sub

    Private Sub fillArc(ByRef rad As Double, ByVal value As Double, ByRef polyBrush As SolidBrush)
        Dim [object] As Object() = setPolyPointSizeAndReverseDir(eMath.RInt(value * Units.RadToDeg))
        Dim polyPointSize As Int32 = CType(CType([object], Object())(0), Int32)
        Dim polyReverseDir As Boolean = CBool(CType([object], Object())(1))

        Dim polyPoints() As Point = addArcPoints(rad, polyPointSize, polyReverseDir)

        ' add final point
        calcArcFinalPoint(rad, value)
        calcPtAndAddToArray(polyPoints, polyPointSize + 1)

        pG.FillPolygon(polyBrush, polyPoints)
    End Sub

    Private Sub calcArcFinalPoint(ByRef rad As Double, ByVal value As Double)
        If value > 180 Then
            rad = eMath.ReverseRad(rad)
        Else
            rad = value
        End If
    End Sub

    Private Function setPolyPointSizeAndReverseDir(ByVal deg As Double) As Object()
        Dim polyReverseDir As Boolean = False
        Dim polyPointSize As Int32 = eMath.RInt(deg)
        If polyPointSize < 0 Then
            polyPointSize = -polyPointSize
            polyReverseDir = True
        End If
        If polyPointSize > 180 Then
            polyPointSize = 360 - polyPointSize
            polyReverseDir = True
        End If
        Return New Object() {polyPointSize, polyReverseDir}
    End Function

    Private Function createPolyArrayAndAddCenterPoint(ByVal polyPointSize As Int32) As Point()
        Dim polyPoints(polyPointSize + 1) As Point
        ' add center point
        polyPoints(0) = pGMidPoint
        Return polyPoints
    End Function

    Private Sub calcPtAndAddToArray(ByRef polyPoints As Point(), ByVal polyPointIx As Int32)
        calcPoint(pPd)
        polyPoints(polyPointIx) = pPd.Point
    End Sub

    Private Function addArcPoints(ByRef rad As Double, ByVal polyPointSize As Int32, ByVal reverseDir As Boolean) As Point()
        Dim polyPoints() As Point = createPolyArrayAndAddCenterPoint(polyPointSize)
        For secPolyPointIx As Int32 = 0 To polyPointSize - 1
            rad = secPolyPointIx * Units.DegToRad
            If reverseDir Then
                rad = -rad
            End If
            calcPtAndAddToArray(polyPoints, secPolyPointIx + 1)
        Next
        Return polyPoints
    End Function

    Private Sub DrawGroundPlane()
        Dim groundPlaneHeight As Int32 = eMath.RInt(pRimDimen * Math.Sin(pPd.TiltSecRad))
        pG.FillEllipse(pGroundPlaneBrush, pRimUlPoint.X, eMath.RInt(pGMidY - groundPlaneHeight / 2), pRimDimen, groundPlaneHeight)
    End Sub

    Private Sub displayInitPoints()
        If pDisplayInits Then
            For init As Int32 = 0 To pInitPointDatas.Length - 1
                drawPointedToPoint(pInitPointDatas(init), pPointForeColor, pPointBackColor)
                pPdDrawString("Init" & init + 1)
            Next
        End If
    End Sub

    Private Sub drawCardinalDirections()
        Dim cardinalDirectionStepRad As Double = Units.QtrRev
        Dim hemispherePriRadAddition As Double = 0
        If pHemisphere.SelectedItem Is Hemisphere.Southern Then
            hemispherePriRadAddition = Units.HalfRev
        End If
        For PriRad As Double = 0 To Units.OneRev - cardinalDirectionStepRad Step cardinalDirectionStepRad
            pPd.PriRad = eMath.ValidRad(PriRad + hemispherePriRadAddition)
            pPd.SecRad = 0
            calcPoint(pPd)
            If Not pPd.BeyondMapRange Then
                pRulerText = CardinalDirection.ISFT.MatchKey(eMath.RInt(PriRad / cardinalDirectionStepRad)).Description
                Dim halfTextWidth As Int32 = eMath.RInt(CType(pG.MeasureString(pRulerText, pRulerFont), SizeF).Width / 2)
                Dim textHeight As Int32 = eMath.RInt(CType(pG.MeasureString(pRulerText, pRulerFont), SizeF).Height)
                pG.DrawString(pRulerText, pRulerFont, pRulerBrush, pPd.Point.X - halfTextWidth, pPd.Point.Y - textHeight)
            End If
        Next
    End Sub
#End Region

End Class

