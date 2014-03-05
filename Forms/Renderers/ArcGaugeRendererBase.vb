#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D

#End Region

Public MustInherit Class ArcGaugeRendererBase
    Inherits RendererBase
    Implements IRendererGauge

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pArcGaugeBrightColor As Color
    Protected pArcGaugeDarkColor As Color

    ' bottom arc is proportional to top arc; values closer to 1 mean narrower gauge
    Protected pArcGaugeInnerArcRatio As Double
    ' arc extends in each direction from vertical by this angle; 
    ' arc gauge designed for angles between 0 and 90;
    ' smaller angles result in fatter gauge with respect to its height
    Protected pArcGaugeAngleFromVert As Int32

    ' arc starting angle where vertical=270 (increases CW)
    Protected pArcStartAngle As Int32
    Protected pArcSweepAngle As Int32

    ' get gauge's width/height ratio
    ' greatest width is outer arc corner, greatest depth or height is inner arc corner
    Protected pGaugeWidthFactor As Double
    ' height upward from center to arc corner is pArcGaugeInnerArcRatio*cos(angle), so height downward from top is 1 minus this amount
    Protected pGaugeHeightFactor As Double
    Protected pGaugeWidthToHeightRatio As Double
    Protected pGaugeAngleFromVertRad As Double

    Protected pPlotW As Int32
    Protected pPlotH As Int32
    Protected pWidthOffset As Int32
    Protected pPlotCenterPoint As Point

    Protected pOuterArcRect As Rectangle
    Protected pInnerArcRect As Rectangle

    Protected pBackgroundColor As Color
    Protected pRimColor As Color
    Protected pScaleColor As Color
    Protected pPointerColor As Color
    Protected pUomColor As Color
    Protected pMarkColor As Color

    Protected pNumMarks As Int32
    Protected pMajorNumMarks As Int32
    Protected pMarkFontSize As Int32
    Protected pMarkFont As Font
    Protected pMarkBrush As SolidBrush
    Protected pMarkRadius As Double

    Protected pIGaugeValue As IGaugeValue
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ArcGaugeRendererBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ArcGaugeRendererBase = New ArcGaugeRendererBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        ToolTip = BartelsLibrary.Constants.GaugeToolTip
        getSettings()
        calcArcGaugeParms()
    End Sub

    'Public Shared Function GetInstance() As ArcGaugeRendererBase
    '    Return New ArcGaugeRendererBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property PointerColor() As Color
        Get
            Return pPointerColor
        End Get
        Set(ByVal Value As Color)
            pPointerColor = Value
        End Set
    End Property

    ' arc gauge is a gauge shaped like an arc;
    ' graphically it is two arcs, the outer arc that touches the top of the graphics area
    ' and a bottom arc that touches the bottom of the graphics area;
    ' the arcs' corners are joined by lines to outline the gauge;

    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        MyBase.Render(g, width, height)

        pG.Clear(pBackgroundColor)

        pNumMarks = 8
        pMajorNumMarks = 4

        calcWidthHeightDependentParms()
        renderGauge()
        outlineGauge()
        renderTicks()
        renderTickLabels()
        renderUOM()
        renderPointer()

        Return g
    End Function

    Public Overridable Function InsideGauge(ByVal point As Point) As Boolean Implements IRendererGauge.InsideGauge
        Dim pointedToAngleRad As Double = eMath.ValidRadPi(eMath.AngleRadFromPoints(pPlotCenterPoint, point))
        If pointedToAngleRad >= -pGaugeAngleFromVertRad AndAlso pointedToAngleRad <= pGaugeAngleFromVertRad Then
            Dim distanceFromCenter As Double = eMath.DistanceBetweenPoints(pPlotCenterPoint, point)
            Return distanceFromCenter <= pOuterArcRect.Height / 2 AndAlso distanceFromCenter >= pInnerArcRect.Height / 2
        End If
        Return False
    End Function

    Public MustOverride Function MeasurementFromObjectToRender() As Double Implements IRendererGauge.MeasurementFromObjectToRender

    Public MustOverride Function MeasurementToPoint(ByVal point As Point) As Double Implements IRendererGauge.MeasurementToPoint

#End Region

#Region "Private and Protected Methods"
    Protected Function measurementToPointSubr(ByVal point As Point) As Double
        Dim measuredAngleRad As Double = eMath.ValidRadPi(eMath.AngleRadFromPoints(pPlotCenterPoint, point))
        Dim measuredPercentOfScale As Double = (measuredAngleRad + pGaugeAngleFromVertRad) / (pArcSweepAngle * Units.DegToRad)
        Dim value As Double = pIGaugeValue.ScaleValue(measuredPercentOfScale)
        value = pIGaugeValue.Validate(value)
        Return value
    End Function

    Protected Sub getSettings()
        With BartelsLibrary.Settings.GetInstance
            pArcGaugeBrightColor = .ArcGaugeBrightColor
            pArcGaugeDarkColor = .ArcGaugeDarkColor
            pArcGaugeInnerArcRatio = .ArcGaugeInnerArcRatio
            pArcGaugeAngleFromVert = .ArcGaugeAngleFromVert

            pBackgroundColor = .ArcGaugeBackgroundColor
            pRimColor = .GaugeRimColor
            pScaleColor = .GaugeScaleColor
            pPointerColor = .GaugePointerColor
            pUomColor = .GaugeUomColor
            pMarkColor = .GaugeMarkColor
        End With
    End Sub

    Protected Sub calcArcGaugeParms()
        pGaugeAngleFromVertRad = pArcGaugeAngleFromVert * Units.DegToRad
        pArcStartAngle = 270 - pArcGaugeAngleFromVert
        pArcSweepAngle = pArcGaugeAngleFromVert * 2
        pGaugeWidthFactor = 2 * Math.Sin(pGaugeAngleFromVertRad)
        pGaugeHeightFactor = 1 - pArcGaugeInnerArcRatio * Math.Cos(pGaugeAngleFromVertRad)
        pGaugeWidthToHeightRatio = pGaugeWidthFactor / pGaugeHeightFactor
    End Sub

    Protected Sub calcWidthHeightDependentParms()
        Dim graphicsWidthToHeightRatio As Double = pGSize.Width / pGSize.Height
        ' drawn width is limited to sin(pArcGaugeAngleFromVert), so increase height so that width is maximized
        If graphicsWidthToHeightRatio > pGaugeWidthToHeightRatio Then
            pPlotW = 2 * eMath.RInt(pGSize.Height / pGaugeHeightFactor)
        Else
            ' graphics area not wide enough, so reduce plotted width/height proportionally
            pPlotW = 2 * eMath.RInt(pGSize.Height / pGaugeHeightFactor * graphicsWidthToHeightRatio / pGaugeWidthToHeightRatio)
        End If
        pPlotH = pPlotW
        pWidthOffset = eMath.RInt((pPlotW - pGSize.Width) / 2)
        pPlotCenterPoint = New Point(eMath.RInt(pPlotW / 2 - pWidthOffset), eMath.RInt(pPlotH / 2))
    End Sub

    Protected Sub outlineGauge()
        ' outer arc
        pG.DrawArc(New Pen(pRimColor), pOuterArcRect, pArcStartAngle, pArcSweepAngle)
        ' inner arc
        pG.DrawArc(New Pen(pRimColor), pInnerArcRect, pArcStartAngle, pArcSweepAngle)
        ' draw the two edge lines
        drawLineInGauge(New Pen(pRimColor), 1, -pGaugeAngleFromVertRad)
        drawLineInGauge(New Pen(pRimColor), 1, pGaugeAngleFromVertRad)
    End Sub

    Protected Sub renderGauge()
        pOuterArcRect = New Rectangle(-pWidthOffset, 0, pPlotW, pPlotH)
        Dim linGradBrush As LinearGradientBrush = New Drawing2D.LinearGradientBrush(pOuterArcRect, pArcGaugeBrightColor, pArcGaugeDarkColor, LinearGradientMode.ForwardDiagonal)
        ' upper pie (fills from center to outer arc)
        pG.FillPie(linGradBrush, pOuterArcRect, pArcStartAngle, pArcSweepAngle)
        ' repair the portion from the center to the inner arc
        Dim innerArcX As Int32 = eMath.RInt(pPlotW * (1 - pArcGaugeInnerArcRatio) / 2) - pWidthOffset
        Dim innerArcY As Int32 = eMath.RInt(pPlotH * (1 - pArcGaugeInnerArcRatio) / 2)
        pInnerArcRect = New Rectangle(innerArcX, innerArcY, (pPlotCenterPoint.X - innerArcX) * 2, (pPlotCenterPoint.Y - innerArcY) * 2)
        Dim overlapAngle As Double = 1
        pG.FillPie(New SolidBrush(pBackgroundColor), pInnerArcRect, CSng(pArcStartAngle - overlapAngle), CSng(pArcSweepAngle + overlapAngle * 2))
    End Sub

    Protected Sub drawLineInGauge(ByVal pen As Pen, ByVal lineIncrLengthRatio As Double, ByVal lineAngleRad As Double)
        ' get ratio from plot's center
        Dim lineLengthRatio As Double = (pArcGaugeInnerArcRatio + (1 - pArcGaugeInnerArcRatio) * lineIncrLengthRatio)

        Dim radius As Double = lineLengthRatio * pOuterArcRect.Height / 2
        Dim outerOffsetX As Int32 = eMath.RInt(radius * Math.Sin(lineAngleRad))
        Dim outerOffsetY As Int32 = eMath.RInt(radius * Math.Cos(lineAngleRad))

        radius = pInnerArcRect.Height / 2
        Dim innerOffsetX As Int32 = eMath.RInt(radius * Math.Sin(lineAngleRad))
        Dim innerOffsetY As Int32 = eMath.RInt(radius * Math.Cos(lineAngleRad))

        pG.DrawLine(pen, pPlotCenterPoint.X + innerOffsetX, pPlotCenterPoint.Y - innerOffsetY, pPlotCenterPoint.X + outerOffsetX, pPlotCenterPoint.Y - outerOffsetY)
    End Sub

    Protected Overridable Sub renderUOM()
        Dim uomFontSize As Int32 = eMath.RInt(pPlotH / 50)
        If uomFontSize < 1 Then
            uomFontSize = 1
        End If
        Dim uomFont As Font = New Font(pFontFamilyName, uomFontSize, FontStyle.Regular)
        Dim uomBrush As SolidBrush = New SolidBrush(pUomColor)

        Dim x As Int32 = eMath.RInt(pPlotCenterPoint.X - uomFontSize * pIGaugeValue.UOM.Description.Length / 3)
        Dim radius As Double = pArcGaugeInnerArcRatio * pOuterArcRect.Height / 2
        Dim outerOffsetY As Int32 = eMath.RInt(radius * Math.Cos(-pGaugeAngleFromVertRad))
        Dim y As Int32 = eMath.RInt(pPlotCenterPoint.Y - outerOffsetY - uomFontSize * 2)

        pG.DrawString(pIGaugeValue.UOM.Description, uomFont, uomBrush, x, y)
    End Sub

    Protected Sub setTickLabelsProps()
        pMarkFontSize = eMath.RInt(pPlotH / 50)
        If pMarkFontSize < 1 Then
            pMarkFontSize = 1
        End If
        pMarkFont = New Font(pFontFamilyName, pMarkFontSize, FontStyle.Regular)
        pMarkBrush = New SolidBrush(pMarkColor)
    End Sub

    Protected Sub renderPointerSubr(ByVal pointerScalarValue As Double)
        Dim measuredPercentOfScale As Double = pIGaugeValue.ScalePercent(pointerScalarValue)
        Dim pointerAngleRad As Double = (measuredPercentOfScale * pArcSweepAngle * Units.DegToRad - pGaugeAngleFromVertRad)

        Dim pointerThick As Int32 = eMath.RInt(pPlotH / 100)
        If pointerThick < 1 Then
            pointerThick = 1
        End If
        Dim pointerPen As New Pen(pPointerColor, pointerThick)
        Dim pointerLengthRatio As Double = 0.8

        drawLineInGauge(pointerPen, pointerLengthRatio, pointerAngleRad)
    End Sub

    Protected MustOverride Sub renderTicks()

    Protected MustOverride Sub renderTickLabels()

    Protected MustOverride Sub renderPointer()

#End Region

End Class
