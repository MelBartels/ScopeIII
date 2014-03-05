#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D

#End Region

Public MustInherit Class LinearArcGaugeRendererBase
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

    'Public Shared Function GetInstance() As LinearArcGaugeRendererBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As LinearArcGaugeRendererBase = New LinearArcGaugeRendererBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIGaugeValue = GaugeLinearValue.GetInstance
    End Sub

    'Public Shared Function GetInstance() As LinearArcGaugeRendererBase
    '    Return New LinearArcGaugeRendererBase
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
        Dim angleIncr As Double = pArcGaugeAngleFromVert * 2 / pNumMarks
        For ix As Int32 = 1 To pNumMarks - 1
            Dim markAngle As Double = -pArcGaugeAngleFromVert + ix * angleIncr
            Dim incrLengthRatio As Double = minorIncrLengthRatio
            Dim pen As Pen = scalePen
            If (ix Mod eMath.RInt(pNumMarks / pMajorNumMarks)).Equals(0) Then
                incrLengthRatio = majorIncrLengthRatio
                pen = majorScalePen
            End If
            drawLineInGauge(pen, incrLengthRatio, markAngle * Units.DegToRad)
        Next
    End Sub

    Protected Overrides Sub renderTickLabels()
        setTickLabelsProps()

        Dim markIncr As Double = pIGaugeValue.ValueIncrement(pNumMarks)
        Dim markLabelPoint As New PointF(0, 0)
        Dim mark As String

        Dim angleIncr As Double = pArcGaugeAngleFromVert * 2 / pNumMarks
        For ix As Int32 = 0 To pNumMarks
            If (ix Mod eMath.RInt(pNumMarks / pMajorNumMarks)).Equals(0) Then
                Dim markAngle As Double = -pArcGaugeAngleFromVert + ix * angleIncr
                Dim markAngleRad As Double = markAngle * Units.DegToRad
                mark = CStr(ix * markIncr)
                Dim radius As Double = pOuterArcRect.Height / 2 - pMarkFontSize / 4
                Dim innerOffsetX As Int32 = eMath.RInt(radius * Math.Sin(markAngleRad))
                Dim innerOffsetY As Int32 = eMath.RInt(radius * Math.Cos(markAngleRad))
                Dim fontWidth As Int32 = CInt(CType(pG.MeasureString(mark, pMarkFont), SizeF).Width)
                Dim fontHeight As Int32 = CInt(CType(pG.MeasureString(mark, pMarkFont), SizeF).Height)
                Dim fontOffsetX As Int32
                If ix = 0 Then
                    fontOffsetX = -fontWidth
                ElseIf ix = pNumMarks Then
                    fontOffsetX = +fontWidth
                Else
                    fontOffsetX = eMath.RInt(fontWidth / 2)
                End If
                Dim fontOffsetY As Int32 = eMath.RInt(fontHeight * Math.Sin(Math.Abs(markAngleRad)) / 2)
                markLabelPoint.X = pPlotCenterPoint.X + innerOffsetX - fontOffsetX
                markLabelPoint.Y = pPlotCenterPoint.Y - innerOffsetY - fontOffsetY
                pG.DrawString(mark, pMarkFont, pMarkBrush, markLabelPoint)
            End If
        Next
    End Sub
#End Region

End Class
