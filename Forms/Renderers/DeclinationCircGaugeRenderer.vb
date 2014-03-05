#Region "Imports"
Imports System.Drawing

#End Region

Public Class DeclinationCircGaugeRenderer
    Inherits RendererCoordGaugeBase

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

    'Public Shared Function GetInstance() As DeclinationCircGaugeRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeclinationCircGaugeRenderer = New DeclinationCircGaugeRenderer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pMaxGaugeReading = 360
    End Sub

    Public Shared Function GetInstance() As DeclinationCircGaugeRenderer
        Return New DeclinationCircGaugeRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        MyBase.Render(g, width, height)

        pNumMarks = 24
        pMajorNumMarks = 8
        pStepAngleRad = Units.OneRev / pNumMarks

        renderTicks()
        renderTickLabels()

        Return g
    End Function

    Public Overrides Function MeasurementToPoint(ByVal point As Point) As Double
        Return eMath.ValidRadPi(MyBase.MeasurementToPoint(point))
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Sub renderTickLabels()
        setTickLabelsProps()

        Dim markIncr As Int32 = emath.rint(pMaxGaugeReading / pMajorNumMarks)
        Dim markLabelPoint As New PointF(0, 0)

        For ix As Int32 = 0 To pMajorNumMarks - 1
            Dim angleRad As Double = ix * pStepAngleRad * pNumMarks / pMajorNumMarks
            setMarkLabelPoint(markLabelPoint, angleRad)

            Dim mark As String
            If ix * markIncr <= 90 Then
                mark = CStr(ix * markIncr)
            ElseIf ix * markIncr <= 180 Then
                mark = CStr(180 - ix * markIncr)
            ElseIf ix * markIncr <= 270 Then
                mark = CStr(180 - ix * markIncr)
            Else
                mark = CStr(ix * markIncr - 360)
            End If

            pG.DrawString(mark, pMarkFont, pMarkBrush, markLabelPoint)
        Next
    End Sub
#End Region

End Class
