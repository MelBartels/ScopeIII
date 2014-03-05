#Region "Imports"
Imports System.Drawing
#End Region

Public Class ScalarArcGaugeRenderer
    Inherits LinearArcGaugeRendererBase

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

    'Public Shared Function GetInstance() As ScalarArcGaugeRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ScalarArcGaugeRenderer = New ScalarArcGaugeRenderer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIGaugeValue.MaxValue = 100
        pIGaugeValue.MinValue = 0
        pIGaugeValue.UOM = UOM.Scalar
    End Sub

    Public Shared Function GetInstance() As ScalarArcGaugeRenderer
        Return New ScalarArcGaugeRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function MeasurementFromObjectToRender() As Double
        Return CDbl(pObjectToRender)
    End Function

    Public Overrides Function MeasurementToPoint(ByVal point As Point) As Double
        Return measurementToPointSubr(point)
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub renderPointer()
        renderPointerSubr(MeasurementFromObjectToRender())
    End Sub
#End Region

End Class
