#Region "Imports"
Imports System.Drawing
#End Region

Public MustInherit Class RendererGaugeBase
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
    Protected pRimColor As Color
    Protected pCenterColor As Color
    Protected pScaleColor As Color
    Protected pCenterFillColor As Color
    Protected pPointerColor As Color
    Protected pUomColor As Color
    Protected pMarkColor As Color

    Protected pUOM As ISFT

    Protected pCgDimen As Int32
    Protected pRimDimen As Int32
    Protected pCenterRadius As Int32
    Protected pMajorMarkLen As Int32

    Protected pNumMarks As Int32
    Protected pMajorNumMarks As Int32
    Protected pStepAngleRad As Double
    Protected pMarkFontSize As Int32
    Protected pMarkFont As Font
    Protected pMarkBrush As SolidBrush
    Protected pMarkRadius As Double

    Protected pMaxGaugeReading As Int32
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RendererGaugeBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RendererGaugeBase = New RendererGaugeBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        ToolTip = BartelsLibrary.Constants.GaugeToolTip
        WidthToHeightRatio = 1

        With BartelsLibrary.Settings.GetInstance
            pRimColor = .GaugeRimColor
            pCenterColor = .GaugeCenterColor
            pScaleColor = .GaugeScaleColor
            pCenterFillColor = .GaugeCenterFillColor
            pPointerColor = .GaugePointerColor
            pUomColor = .GaugeUomColor
            pMarkColor = .GaugeMarkColor
        End With

        pUOM = UOM.Degree
    End Sub

    'Public Shared Function GetInstance() As RendererGaugeBase
    '    Return New RendererGaugeBase
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

    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        MyBase.Render(g, width, height)

        setSquareDimension(width, height)
        renderGaugeCircle()
        renderCenterCircle()
        renderUOM()
        renderPointer()

        Return g
    End Function

    Public Overridable Function InsideGauge(ByVal point As Point) As Boolean Implements IRendererGauge.InsideGauge
        Return Math.Sqrt(Math.Pow(Math.Abs(point.X - pGMidX), 2) + Math.Pow(Math.Abs(point.Y - pGMidY), 2)) <= pRimDimen / 2
    End Function

    Public MustOverride Function MeasurementFromObjectToRender() As Double Implements IRendererGauge.MeasurementFromObjectToRender

    Public MustOverride Function MeasurementToPoint(ByVal point As Point) As Double Implements IRendererGauge.MeasurementToPoint

#End Region

#Region "Private and Protected Methods"
    Protected Overridable Sub setSquareDimension(ByVal width As Int32, ByVal height As Int32)
        pCgDimen = width
        If height < width Then
            pCgDimen = height
        End If
    End Sub

    Protected Overridable Sub renderGaugeCircle()
        Dim rimThick As Int32 = 1
        Dim rimPen As New Pen(pRimColor, rimThick)
        Dim rimRatio As Double = 0.9
        pRimDimen = eMath.RInt(pCgDimen * rimRatio)
        ' eg, if rimRatio=.5, then ul=.25 and lr=.75, centering rimRatio between 0 and 1
        Dim rimUlDimen As Int32 = eMath.RInt(pCgDimen * (1 - rimRatio) / 2) + eMath.RInt(rimThick / 2)
        Dim rimLrDimen As Int32 = eMath.RInt(pCgDimen * (1 + rimRatio) / 2) - eMath.RInt(rimThick / 2)
        Dim rimUlPoint As New Point(rimUlDimen, rimUlDimen)
        Dim rimLrPoint As New Point(rimLrDimen, rimLrDimen)
        Dim rimRectDimen As Int32 = eMath.RInt(pRimDimen - rimThick)
        Dim rimSize As New Size(rimRectDimen, rimRectDimen)
        Dim rimRect As New Rectangle(rimUlPoint, rimSize)
        pG.FillEllipse(pGLinGradBrush, rimRect)
        pG.DrawEllipse(rimPen, rimRect)
    End Sub

    Protected Overridable Sub renderCenterCircle()
        Dim centerThick As Int32 = 1
        Dim centerPen As New Pen(pCenterColor, centerThick)
        pCenterRadius = eMath.RInt(pCgDimen / 50)
        Dim centerUlDimen As Int32 = eMath.RInt(pCgDimen / 2 - pCenterRadius)
        Dim centerLrDimen As Int32 = eMath.RInt(pCgDimen / 2 + pCenterRadius)
        Dim centerUlPoint As New Point(centerUlDimen, centerUlDimen)
        Dim centerLrPoint As New Point(centerLrDimen, centerLrDimen)
        Dim centerSize As New Size(pCenterRadius * 2, pCenterRadius * 2)
        Dim centerRect As New Rectangle(centerUlPoint, centerSize)
        Dim centerFillBrush As New SolidBrush(pCenterFillColor)
        pG.FillEllipse(centerFillBrush, centerRect)
        pG.DrawEllipse(centerPen, centerRect)
    End Sub

    Protected Overridable Sub renderUOM()
        Dim uomFontSize As Int32 = eMath.RInt(pCgDimen / 20)
        If uomFontSize < 1 Then
            uomFontSize = 1
        End If
        Dim uomFont As Font = New Font(pFontFamilyName, uomFontSize, FontStyle.Regular)
        Dim uomBrush As SolidBrush = New SolidBrush(pUomColor)

        pG.DrawString(pUOM.Description, uomFont, uomBrush, eMath.RInt(pGMidX - uomFontSize * pUOM.Description.Length / 3), eMath.RInt(pGMidY + uomFontSize / 2))
    End Sub

    Protected Overridable Sub renderPointer()
        Dim pointerAngleRad As Double = MeasurementFromObjectToRender()

        Dim pointerPen As New Pen(pPointerColor, 1)
        Dim pointerBrush As New SolidBrush(pPointerColor)

        Dim maxPointerLen As Int32 = eMath.RInt(pRimDimen / 2 - pMajorMarkLen - pCenterRadius)
        Dim pointerLen As Int32 = eMath.RInt(0.8 * maxPointerLen)
        Dim pointerGap As Int32 = eMath.RInt((maxPointerLen - pointerLen) / 2)
        Dim pointerBaseWidth As Int32 = eMath.RInt(pointerLen / 25)

        ' array of points to define the polygon; points added in traversal order
        Dim polyPoints(3) As Point
        ' inside gap point
        Dim point As New Point(0, 0)
        Dim gapRadius As Int32 = pCenterRadius + 2 * pointerGap
        point.X = eMath.RInt(pGMidX + Math.Sin(pointerAngleRad) * gapRadius)
        point.Y = eMath.RInt(pGMidY - Math.Cos(pointerAngleRad) * gapRadius)
        polyPoints(0) = point
        ' outside tip point
        point = New Point(0, 0)
        Dim tipRadius As Int32 = (pCenterRadius + pointerGap + pointerLen)
        point.X = eMath.RInt(pGMidX + Math.Sin(pointerAngleRad) * tipRadius)
        point.Y = eMath.RInt(pGMidY - Math.Cos(pointerAngleRad) * tipRadius)
        polyPoints(2) = point
        ' base left point
        Dim diffAngleRad As Double = Math.Atan(0.5)
        point = New Point(0, 0)
        Dim baseRadius As Int32 = pCenterRadius + pointerGap
        point.X = eMath.RInt(pGMidX + Math.Sin(pointerAngleRad - diffAngleRad) * baseRadius)
        point.Y = eMath.RInt(pGMidY - Math.Cos(pointerAngleRad - diffAngleRad) * baseRadius)
        polyPoints(1) = point
        ' base right point
        point = New Point(0, 0)
        point.X = eMath.RInt(pGMidX + Math.Sin(pointerAngleRad + diffAngleRad) * baseRadius)
        point.Y = eMath.RInt(pGMidY - Math.Cos(pointerAngleRad + diffAngleRad) * baseRadius)
        polyPoints(3) = point

        pG.FillPolygon(pointerBrush, polyPoints)
    End Sub

    Protected Overridable Sub renderTicks()
        ' draw scale markings: major and minor
        Dim scaleThick As Int32 = 1
        Dim majorScaleThick As Int32 = 2 * scaleThick
        Dim scalePen As New Pen(pScaleColor, scaleThick)
        Dim majorScalePen As New Pen(pScaleColor, majorScaleThick)
        Dim markLen As Int32 = eMath.RInt(pCgDimen / 30)
        pMajorMarkLen = 2 * markLen
        Dim insidePoint As New Point(0, 0)
        Dim outsidePoint As New Point(0, 0)
        For ix As Int32 = 0 To pNumMarks - 1
            Dim angleRad As Double = ix * pStepAngleRad
            ' shorten a bit to prevent bleeding into outside rim
            insidePoint.X = eMath.RInt((pCgDimen + Math.Sin(angleRad) * (pRimDimen - 2)) / 2)
            insidePoint.Y = eMath.RInt((pCgDimen - Math.Cos(angleRad) * (pRimDimen - 2)) / 2)
            Dim len As Int32 = markLen
            Dim pen As Pen = scalePen
            If (ix Mod eMath.RInt(pNumMarks / pMajorNumMarks)).Equals(0) Then
                len = pMajorMarkLen
                pen = majorScalePen
            End If
            outsidePoint.X = eMath.RInt(insidePoint.X - Math.Sin(angleRad) * len)
            outsidePoint.Y = eMath.RInt(insidePoint.Y + Math.Cos(angleRad) * len)
            pG.DrawLine(pen, insidePoint, outsidePoint)
        Next
    End Sub

    Protected Sub setTickLabelsProps()
        pMarkFontSize = eMath.RInt(pCgDimen / 20)
        If pMarkFontSize < 1 Then
            pMarkFontSize = 1
        End If
        pMarkFont = New Font(pFontFamilyName, pMarkFontSize, FontStyle.Regular)
        pMarkBrush = New SolidBrush(pMarkColor)
        pMarkRadius = pRimDimen / 2 - 1.75 * pMajorMarkLen
    End Sub

    Protected Sub setMarkLabelPoint(ByRef markLabelPoint As PointF, ByVal angleRad As Double)
        ' offset because DrawString draws at upper left of string, and further offset for string containing 3 chars
        markLabelPoint.X = eMath.RInt(pGMidX - pMarkFontSize + Math.Sin(angleRad) * pMarkRadius)
        markLabelPoint.Y = eMath.RInt(pGMidY - pMarkFontSize / 1.5 - Math.Cos(angleRad) * pMarkRadius)
    End Sub
#End Region

End Class
