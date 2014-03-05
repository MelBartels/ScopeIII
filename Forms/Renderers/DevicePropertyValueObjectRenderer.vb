#Region "Imports"
Imports System.Drawing
#End Region

Public Class ValueObjectRenderer
    Inherits RendererGaugeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Property ValueObject() As ValueObject
        Get
            Return pValueObject
        End Get
        Set(ByVal value As ValueObject)
            pValueObject = value
            pUOM = UOM.ISFT.MatchString(ValueObject.UOM)
        End Set
    End Property
#End Region

#Region "Private and Protected Members"
    Private pValueObject As ValueObject
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ValueObjectRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ValueObjectRenderer = New ValueObjectRenderer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pUOM = UOM.Hour
        pMaxGaugeReading = 24
    End Sub

    Public Shared Function GetInstance() As ValueObjectRenderer
        Return New ValueObjectRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        If ValueObject IsNot Nothing Then
            MyBase.Render(g, width, height)

            pNumMarks = 8
            pMajorNumMarks = 4
            pStepAngleRad = Units.OneRev / pNumMarks

            renderTicks()
            renderTickLabels()
        End If

        Return g
    End Function

    Public Overrides Function MeasurementFromObjectToRender() As Double
        Return ValueObject.ConvertValueToRad(CDbl(pObjectToRender))
    End Function

    ' 0 deg is directly upward;
    ' returns within 0-360deg range 
    Public Overrides Function MeasurementToPoint(ByVal point As Point) As Double
        Return eMath.AngleRadFromPoints(pGMidPoint, point)
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Sub renderTickLabels()
        setTickLabelsProps()

        Dim markLabelPoint As New PointF(0, 0)
        For ix As Int32 = 0 To pMajorNumMarks - 1
            Dim angleRad As Double = ix * pStepAngleRad * pNumMarks / pMajorNumMarks
            setMarkLabelPoint(markLabelPoint, angleRad)

            ' labeling is offset by MinValue
            Dim label As String = CStr(ValueObject.Range * ix / pMajorNumMarks + CDbl(ValueObject.MinValue))
            pG.DrawString(label, pMarkFont, pMarkBrush, markLabelPoint)
        Next
    End Sub
#End Region

End Class
