#Region "Imports"
Imports System.Drawing
#End Region

Public Class ArcminCircGaugeRenderer
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

    'Public Shared Function GetInstance() As ArcminCircGaugeRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ArcminCircGaugeRenderer = New ArcminCircGaugeRenderer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pUOM = UOM.Arcmin
        pMaxGaugeReading = 120
    End Sub

    Public Shared Function GetInstance() As ArcMinCircGaugeRenderer
        Return New ArcMinCircGaugeRenderer
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

    Public Overrides Function MeasurementFromObjectToRender() As Double
        Return CType(pObjectToRender, Coordinate).Rad * 180
    End Function

    ' 0 deg is directly upward;
    ' returns within -60-60arcmin range
    Public Overrides Function MeasurementToPoint(ByVal point As Point) As Double
        Dim rawAngleRad As Double = eMath.AngleRadFromPoints(pGMidPoint, point)
        If rawAngleRad > Units.HalfRev Then
            rawAngleRad = rawAngleRad - Units.OneRev
        End If
        ' eg, gauge pointed to bottom returns rawAngleRad of 180deg, 
        ' which should result in 60arcminutes (1deg)
        Return rawAngleRad / 180
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
            If (ix * markIncr).Equals(emath.rint(pMaxGaugeReading / 2)) Then
                mark = "±60"
            ElseIf ix * markIncr > pMaxGaugeReading / 2 Then
                mark = CStr(ix * markIncr - pMaxGaugeReading)
            Else
                mark = CStr(ix * markIncr)
            End If

            pG.DrawString(mark, pMarkFont, pMarkBrush, markLabelPoint)
        Next
    End Sub
#End Region

End Class
