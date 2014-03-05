#Region "Imports"
Imports System.Drawing
#End Region

Public MustInherit Class LogArcGaugeRendererBase
    Inherits ArcGaugeRendererBase

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

    'Public Shared Function GetInstance() As LogArcGaugeRendererBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As LogArcGaugeRendererBase = New LogArcGaugeRendererBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIGaugeValue = GaugeLogValue.GetInstance
    End Sub

    'Public Shared Function GetInstance() As LogArcGaugeRendererBase
    '    Return New LogArcGaugeRendererBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub renderTicks()
        ' draw scale markings: major and minor
        Dim scaleThick As Int32 = 1
        Dim majorScaleThick As Int32 = 2 * scaleThick
        Dim scalePen As New Pen(pScaleColor, scaleThick)
        Dim majorScalePen As New Pen(pScaleColor, majorScaleThick)

        Dim majorIncrLengthRatio As Double = 0.5
        Dim minorIncrLengthRatio As Double = majorIncrLengthRatio / 2

        ' determine # of ticks
        Dim maxScaleLog10 As Double = Math.Log10(pIGaugeValue.MaxValue)
        ' # of marks per order of magnitude increase, eg, 0 to 10, 10 to 100
        Dim div As Int32 = 3
        pNumMarks = eMath.RInt(maxScaleLog10 * div)

        ' draw scale=1, log=0 (not part of sequence below)
        renderTick(scalePen, minorIncrLengthRatio, 1, maxScaleLog10)
        ' continue with rest of ticks, ie, 2,5,10,20,50,100,200,500
        For ix As Int32 = 1 To pNumMarks - 1
            Dim incrLengthRatio As Double = minorIncrLengthRatio
            Dim pen As Pen = scalePen
            If ix Mod div = 0 Then
                incrLengthRatio = majorIncrLengthRatio
                pen = majorScalePen
            End If
            Dim roundUp As Int32 = div - ix Mod div
            If roundUp = div Then
                roundUp = 0
            End If
            Dim logIncr As Double = Math.Pow(10, (ix + roundUp) / div)
            Dim logFactor As Double = 1
            If ix Mod div = 1 Then
                logFactor = 0.2
            ElseIf ix Mod div = 2 Then
                logFactor = 0.5
            End If
            Dim scaleValue As Double = logFactor * logIncr
            renderTick(pen, incrLengthRatio, scaleValue, maxScaleLog10)
        Next
    End Sub

    Protected Sub renderTick(ByVal pen As Pen, ByVal incrLengthRatio As Double, ByVal scaleValue As Double, ByVal maxScaleLog10 As Double)
        Dim scalePercent As Double = scalePercentUsingLog(scaleValue, pIGaugeValue.MaxValue, maxScaleLog10)
        Dim angleIncr As Double = scalePercent * pArcSweepAngle
        Dim tickAngle As Double = -pArcGaugeAngleFromVert + angleIncr
        Dim tickAngleRad As Double = tickAngle * Units.DegToRad
        drawLineInGauge(Pen, incrLengthRatio, tickAngleRad)
    End Sub

    Protected Function scalePercentUsingLog(ByVal scaleValue As Double, ByVal maxValue As Double, ByVal maxScaleLog10 As Double) As Double
        ' log(1)=0, so expand scale beginning part of scale so that scale% of 0 means scale value of 1
        Dim expandedScaleValue As Double = 2 * scaleValue - eMath.ScaleValue(scaleValue, -1, 1, maxValue)
        Return Math.Log10(expandedScaleValue) / maxScaleLog10
    End Function

    Protected Overrides Sub renderTickLabels()
        setTickLabelsProps()

        Dim markLabelPoint As New PointF(0, 0)
        Dim mark As String
        Dim scalePercent As Double

        ' determine # of ticks
        Dim maxScaleLog10 As Double = Math.Log10(pIGaugeValue.MaxValue)
        ' # of marks per order of magnitude increase, eg, 0 to 10, 10 to 100
        Dim div As Int32 = 3
        pNumMarks = eMath.RInt(maxScaleLog10 * div)

        ' label 0
        renderTickLabel(0, 0, CStr(0), markLabelPoint)
        ' label 1
        renderTickLabel(1, scalePercentUsingLog(1, pIGaugeValue.MaxValue, maxScaleLog10), CStr(1), markLabelPoint)
        ' continue with rest of tick labels, ie, 2,5,10,20,50,100,200,500
        For ix As Int32 = 1 To pNumMarks
            Dim roundUp As Int32 = div - ix Mod div
            If roundUp = div Then
                roundUp = 0
            End If
            Dim logIncr As Double = Math.Pow(10, (ix + roundUp) / div)
            Dim logFactor As Double = 1
            If ix Mod div = 1 Then
                logFactor = 0.2
            ElseIf ix Mod div = 2 Then
                logFactor = 0.5
            End If
            Dim scaleValue As Double = logFactor * logIncr
            mark = CStr(scaleValue)
            scalePercent = scalePercentUsingLog(scaleValue, pIGaugeValue.MaxValue, maxScaleLog10)
            renderTickLabel(ix, scalePercent, mark, markLabelPoint)
        Next
    End Sub

    Protected Sub renderTickLabel(ByVal ix As Integer, ByVal scalePercent As Double, ByVal mark As String, ByVal markLabelPoint As PointF)
        Dim angleIncr As Double = scalePercent * pArcSweepAngle
        Dim tickAngle As Double = -pArcGaugeAngleFromVert + angleIncr
        Dim tickAngleRad As Double = tickAngle * Units.DegToRad
        Dim radius As Double = pOuterArcRect.Height / 2 - pMarkFontSize / 2
        Dim innerOffsetX As Int32 = eMath.RInt(radius * Math.Sin(tickAngleRad))
        Dim innerOffsetY As Int32 = eMath.RInt(radius * Math.Cos(tickAngleRad))
        Dim fontWidth As Int32 = eMath.RInt(CType(pG.MeasureString(mark, pMarkFont), SizeF).Width)
        Dim fontHeight As Int32 = eMath.RInt(CType(pG.MeasureString(mark, pMarkFont), SizeF).Height)
        Dim fontOffsetX As Int32
        If ix = 0 Then
            fontOffsetX = eMath.RInt(-fontWidth / 2)
        ElseIf ix = pNumMarks Then
            fontOffsetX = eMath.RInt(fontWidth)
        Else
            fontOffsetX = eMath.RInt(fontWidth / 2)
        End If
        Dim fontOffsetY As Int32 = eMath.RInt(fontHeight * Math.Sin(Math.Abs(tickAngleRad)) / 2)
        markLabelPoint.X = pPlotCenterPoint.X + innerOffsetX - fontOffsetX
        markLabelPoint.Y = pPlotCenterPoint.Y - innerOffsetY - fontOffsetY
        pG.DrawString(mark, pMarkFont, pMarkBrush, markLabelPoint)
    End Sub

#End Region

End Class
