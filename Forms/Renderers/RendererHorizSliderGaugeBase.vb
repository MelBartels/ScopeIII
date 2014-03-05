#Region "Imports"
Imports System.Drawing
#End Region

Public MustInherit Class RendererHorizSliderGaugeBase
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
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RendererHorizSliderGaugeBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RendererHorizSliderGaugeBase = New RendererHorizSliderGaugeBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As RendererHorizSliderGaugeBase
    '    Return New RendererHorizSliderGaugeBase
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
        Return point.X >= pSliderSlotRect.X AndAlso point.X <= pSliderSlotRect.X + pSliderSlotRect.Width _
        AndAlso point.Y >= pSliderKnobRect.Y AndAlso point.Y <= pSliderKnobRect.Y + pSliderKnobRect.Height
    End Function

    Public Overrides Function MeasurementToPoint(ByVal point As Point) As Double
        Dim measuredPercentOfScale As Double = (point.X - pSliderSlotRect.X) / pSliderSlotRect.Width
        Dim value As Double = pIGaugeValue.ScaleValue(measuredPercentOfScale)
        value = pIGaugeValue.Validate(value)
        Return value
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub setDimensions(ByVal width As Int32, ByVal height As Int32)
        pScaleFactor = width / BaseScale
        pSliderSlotWidth = eMath.RInt(width * SliderSlotWidthRatio)
        pQuietZoneX = eMath.RInt((width - pSliderSlotWidth) / 2)
        pSliderSlotHeight = eMath.RInt(SliderSlotHeight * pScaleFactor)
        If pSliderSlotHeight > height * pScaleFactor Then
        End If
        pSliderKnobWidth = eMath.RInt(SliderKnobWidth * pScaleFactor)
        pSliderKnobHeight = eMath.RInt(SliderKnobHeight * pScaleFactor)

        pSliderSlotRect.Width = pSliderSlotWidth
        If pSliderSlotRect.Width < 1 Then
            pSliderSlotRect.Width = 1
        End If
        pSliderSlotRect.Height = pSliderSlotHeight
        If pSliderSlotRect.Height < 1 Then
            pSliderSlotRect.Height = 1
        End If
        pSliderSlotRect.X = eMath.RInt((width - pSliderSlotWidth) / 2)
        pSliderSlotMidY = eMath.RInt(height * SliderSlotHeightRatio)
        pSliderSlotRect.Y = eMath.RInt(pSliderSlotMidY - pSliderSlotHeight / 2)

        pSliderKnobRect.Width = pSliderKnobWidth
        If pSliderKnobRect.Width < 1 Then
            pSliderKnobRect.Width = 1
        End If
        pSliderKnobRect.Height = pSliderKnobHeight
        If pSliderKnobRect.Height < 1 Then
            pSliderKnobRect.Height = 1
        End If

        pMarkLen = eMath.RInt(MarkLenRatio * SliderKnobHeight * pScaleFactor)
    End Sub

    Protected Overrides Sub renderUOM()
        Dim uomFontSize As Int32 = eMath.RInt(pSliderSlotWidth / FontScale)
        If uomFontSize < 1 Then
            uomFontSize = 1
        End If
        Dim uomFont As Font = New Font(pFontFamilyName, uomFontSize, FontStyle.Regular)
        Dim uomBrush As SolidBrush = New SolidBrush(pUomColor)

        pG.DrawString(pIGaugeValue.UOM.Description, uomFont, uomBrush, eMath.RInt(pGMidX - uomFontSize * pIGaugeValue.UOM.Description.Length / 3), eMath.RInt(pGMidY + uomFontSize))
    End Sub

    Protected Overrides Sub renderKnob()
        Dim pointerScalarValue As Double = MeasurementFromObjectToRender()
        Dim knobCenterX As Int32 = pQuietZoneX + eMath.RInt(pSliderSlotWidth * pIGaugeValue.ScalePercent(pointerScalarValue))
        pSliderKnobRect.X = eMath.RInt(knobCenterX - pSliderKnobWidth / 2)
        pSliderKnobRect.Y = eMath.RInt(pSliderSlotMidY - pSliderKnobHeight / 2)
        pG.FillRectangle(pSliderKnobBrush, pSliderKnobRect)

        pSliderKnobHorizShadowRect.Width = pSliderKnobRect.Width
        pSliderKnobHorizShadowRect.Height = SliderKnobShadowThick
        pSliderKnobHorizShadowRect.X = pSliderKnobRect.X
        pSliderKnobHorizShadowRect.Y = pSliderKnobRect.Y + pSliderKnobRect.Height - SliderKnobShadowThick
        pG.FillRectangle(pSliderKnobShadowBrush, pSliderKnobHorizShadowRect)

        pSliderKnobVertShadowRect.Width = SliderKnobShadowThick
        pSliderKnobVertShadowRect.Height = pSliderKnobRect.Height
        pSliderKnobVertShadowRect.X = pSliderKnobRect.X + pSliderKnobRect.Width - SliderKnobShadowThick
        pSliderKnobVertShadowRect.Y = pSliderKnobRect.Y
        pG.FillRectangle(pSliderKnobShadowBrush, pSliderKnobVertShadowRect)

        pSliderKnobHorizHighlightRect.Width = pSliderKnobRect.Width
        pSliderKnobHorizHighlightRect.Height = SliderKnobShadowThick
        pSliderKnobHorizHighlightRect.X = pSliderKnobRect.X
        pSliderKnobHorizHighlightRect.Y = pSliderKnobRect.Y
        pG.FillRectangle(pSliderKnobHighlightBrush, pSliderKnobHorizHighlightRect)

        pSliderKnobVertHighlightRect.Width = SliderKnobShadowThick
        pSliderKnobVertHighlightRect.Height = pSliderKnobRect.Height
        pSliderKnobVertHighlightRect.X = pSliderKnobRect.X
        pSliderKnobVertHighlightRect.Y = pSliderKnobRect.Y
        pG.FillRectangle(pSliderKnobHighlightBrush, pSliderKnobVertHighlightRect)
    End Sub

    Protected Overrides Sub renderTicksAndLabels()
        pMarkFontSize = eMath.RInt(pSliderSlotWidth / FontScale)
        If pMarkFontSize < 1 Then
            pMarkFontSize = 1
        End If
        pMarkFont = New Font(pFontFamilyName, pMarkFontSize, FontStyle.Regular)
        pMarkBrush = New SolidBrush(pMarkColor)

        Dim topPoint As New Point(0, 0)
        Dim bottomPoint As New Point(0, 0)
        Dim fontPoint As New PointF(0, 0)

        For ix As Int32 = 0 To pNumMarks
            Dim measurement As Double = ix * pStepMeasurement
            topPoint.X = eMath.RInt(pSliderSlotRect.X + pSliderSlotRect.Width * measurement / pIGaugeValue.ValueRange)
            topPoint.Y = eMath.RInt(pSliderSlotMidY - pSliderSlotRect.Height / 2 - pMarkLen)
            bottomPoint.X = topPoint.X
            bottomPoint.Y = topPoint.Y + pMarkLen
            pG.DrawLine(pMarkPen, topPoint, bottomPoint)

            Dim measurementString As String = formatMeasurement(measurement + pIGaugeValue.MinValue)
            fontPoint.X = topPoint.X - 5 * measurementString.Length
            fontPoint.Y = bottomPoint.Y + pSliderSlotRect.Height + 3
            pG.DrawString(measurementString, pMarkFont, pMarkBrush, fontPoint)
        Next
    End Sub
#End Region

End Class
