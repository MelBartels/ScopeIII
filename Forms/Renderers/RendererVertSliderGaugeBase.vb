#Region "Imports"
Imports System.Drawing
#End Region

Public MustInherit Class RendererVertSliderGaugeBase
    Inherits RendererSliderGaugePrototype

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pfontFormat As StringFormat
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RendererVertSliderGaugeBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RendererVertSliderGaugeBase = New RendererVertSliderGaugeBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        SliderSlotWidthRatio = 0.3
        SliderSlotHeightRatio = 0.9
        SliderSlotHeight = 5
        SliderSlotWidth = SliderSlotHeight
        SliderKnobWidth = 20
        SliderKnobHeight = 8

        pfontFormat = New StringFormat
        pfontFormat.FormatFlags = StringFormatFlags.DirectionVertical
    End Sub

    'Public Shared Function GetInstance() As RendererVertSliderGaugeBase
    '    Return New RendererVertSliderGaugeBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        MyBase.Render(g, width, height)
        Return g
    End Function

    Public Overrides Function InsideGauge(ByVal point As Point) As Boolean
        Return point.Y >= pSliderSlotRect.Y AndAlso point.Y <= pSliderSlotRect.Y + pSliderSlotRect.Height _
        AndAlso point.X >= pSliderKnobRect.X AndAlso point.X <= pSliderKnobRect.X + pSliderKnobRect.Width
    End Function

    Public Overrides Function MeasurementToPoint(ByVal point As Point) As Double
        Dim value As Double = pIGaugeValue.MaxValue - pIGaugeValue.ValueRange * (point.Y - pSliderSlotRect.Y) / pSliderSlotRect.Height
        value = pIGaugeValue.Validate(value)
        Return value
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub setDimensions(ByVal width As Int32, ByVal height As Int32)
        pScaleFactor = height / BaseScale
        pSliderSlotHeight = eMath.RInt(height * SliderSlotHeightRatio)
        pQuietZoneX = eMath.RInt((height - pSliderSlotHeight) / 2)
        pSliderSlotWidth = eMath.RInt(SliderSlotWidth * pScaleFactor)
        If pSliderSlotWidth > width * pScaleFactor Then
            pSliderSlotWidth = eMath.RInt(width * pScaleFactor)
        End If
        pSliderKnobHeight = eMath.RInt(SliderKnobHeight * pScaleFactor)
        pSliderKnobWidth = eMath.RInt(SliderKnobWidth * pScaleFactor)

        pSliderSlotRect.Height = pSliderSlotHeight
        If pSliderSlotRect.Height < 1 Then
            pSliderSlotRect.Height = 1
        End If
        pSliderSlotRect.Width = pSliderSlotWidth
        If pSliderSlotRect.Width < 1 Then
            pSliderSlotRect.Width = 1
        End If
        pSliderSlotRect.X = eMath.RInt((height - pSliderSlotHeight) / 2)
        pSliderSlotMidY = eMath.RInt(width * SliderSlotWidthRatio)
        pSliderSlotRect.Y = eMath.RInt(pSliderSlotMidY - pSliderSlotWidth / 2)

        pSliderKnobRect.Height = pSliderKnobHeight
        If pSliderKnobRect.Height < 1 Then
            pSliderKnobRect.Height = 1
        End If
        pSliderKnobRect.Width = pSliderKnobWidth
        If pSliderKnobRect.Width < 1 Then
            pSliderKnobRect.Width = 1
        End If

        pMarkLen = eMath.RInt(MarkLenRatio * SliderKnobWidth * pScaleFactor)
    End Sub

    Protected Overrides Sub renderUOM()
        Dim uomFontSize As Int32 = eMath.RInt(pSliderSlotHeight / FontScale)
        If uomFontSize < 1 Then
            uomFontSize = 1
        End If
        Dim uomFont As Font = New Font(pFontFamilyName, uomFontSize, FontStyle.Regular)
        Dim uomBrush As SolidBrush = New SolidBrush(pUomColor)

        pG.DrawString(pIGaugeValue.UOM.Description, uomFont, uomBrush, eMath.RInt(pGMidX + uomFontSize), eMath.RInt(pGMidY - uomFontSize * pIGaugeValue.UOM.Description.Length / 3), pfontFormat)
    End Sub

    Protected Overrides Sub renderKnob()
        Dim pointerScalarValue As Double = MeasurementFromObjectToRender()
        Dim knobCenterY As Int32 = pQuietZoneX + pSliderSlotHeight - eMath.RInt(pSliderSlotHeight * pIGaugeValue.ScalePercent(pointerScalarValue))
        pSliderKnobRect.Y = eMath.RInt(knobCenterY - pSliderKnobHeight / 2)
        pSliderKnobRect.X = eMath.RInt(pSliderSlotMidY - pSliderKnobWidth / 2)
        pG.FillRectangle(pSliderKnobBrush, pSliderKnobRect)

        pSliderKnobHorizShadowRect.Height = pSliderKnobRect.Height
        pSliderKnobHorizShadowRect.Width = SliderKnobShadowThick
        pSliderKnobHorizShadowRect.Y = pSliderKnobRect.Y
        pSliderKnobHorizShadowRect.X = pSliderKnobRect.X + pSliderKnobRect.Width - SliderKnobShadowThick
        pG.FillRectangle(pSliderKnobShadowBrush, pSliderKnobHorizShadowRect)

        pSliderKnobVertShadowRect.Height = SliderKnobShadowThick
        pSliderKnobVertShadowRect.Width = pSliderKnobRect.Width
        pSliderKnobVertShadowRect.Y = pSliderKnobRect.Y + pSliderKnobRect.Height - SliderKnobShadowThick
        pSliderKnobVertShadowRect.X = pSliderKnobRect.X
        pG.FillRectangle(pSliderKnobShadowBrush, pSliderKnobVertShadowRect)

        pSliderKnobHorizHighlightRect.Height = pSliderKnobRect.Height
        pSliderKnobHorizHighlightRect.Width = SliderKnobShadowThick
        pSliderKnobHorizHighlightRect.Y = pSliderKnobRect.Y
        pSliderKnobHorizHighlightRect.X = pSliderKnobRect.X
        pG.FillRectangle(pSliderKnobHighlightBrush, pSliderKnobHorizHighlightRect)

        pSliderKnobVertHighlightRect.Height = SliderKnobShadowThick
        pSliderKnobVertHighlightRect.Width = pSliderKnobRect.Width
        pSliderKnobVertHighlightRect.Y = pSliderKnobRect.Y
        pSliderKnobVertHighlightRect.X = pSliderKnobRect.X
        pG.FillRectangle(pSliderKnobHighlightBrush, pSliderKnobVertHighlightRect)
    End Sub

    Protected Overrides Sub renderTicksAndLabels()
        pMarkFontSize = eMath.RInt(pSliderSlotHeight / FontScale)
        If pMarkFontSize < 1 Then
            pMarkFontSize = 1
        End If
        pMarkFont = New Font(pFontFamilyName, pMarkFontSize, FontStyle.Regular)
        pMarkBrush = New SolidBrush(pMarkColor)

        Dim leftPoint As New Point(0, 0)
        Dim rightPoint As New Point(0, 0)
        Dim fontPoint As New PointF(0, 0)

        For ix As Int32 = 0 To pNumMarks
            Dim measurement As Double = ix * pStepMeasurement
            leftPoint.Y = eMath.RInt(pSliderSlotRect.Y + pSliderSlotRect.Height - pSliderSlotRect.Height * measurement / pIGaugeValue.ValueRange)
            leftPoint.X = eMath.RInt(pSliderSlotMidY - pSliderSlotRect.Width / 2 - pMarkLen)
            rightPoint.Y = leftPoint.Y
            rightPoint.X = leftPoint.X + pMarkLen
            pG.DrawLine(pMarkPen, leftPoint, rightPoint)

            Dim measurementString As String = formatMeasurement(measurement + pIGaugeValue.MinValue)
            fontPoint.Y = eMath.RInt(rightPoint.Y - pMarkFontSize * 2 / 3)
            fontPoint.X = rightPoint.X + 15 - 5 * measurementString.Length
            pG.DrawString(measurementString, pMarkFont, pMarkBrush, fontPoint)
        Next
    End Sub
#End Region

End Class
