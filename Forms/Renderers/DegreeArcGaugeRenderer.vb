#Region "Imports"
Imports System.Drawing
#End Region

Public Class DegreeArcGaugeRenderer
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

    'Public Shared Function GetInstance() As DegreeArcGaugeRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DegreeArcGaugeRenderer = New DegreeArcGaugeRenderer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIGaugeValue.MaxValue = 360
        pIGaugeValue.MinValue = 0
        pIGaugeValue.UOM = UOM.Degree
    End Sub

    Public Shared Shadows Function GetInstance() As DegreeArcGaugeRenderer
        Return New DegreeArcGaugeRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function MeasurementFromObjectToRender() As Double
        Return CType(pObjectToRender, Coordinate).Rad
    End Function

    Public Overrides Function MeasurementToPoint(ByVal point As Point) As Double
        Return measurementToPointSubr(point) * Units.DegToRad
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub renderPointer()
        renderPointerSubr(MeasurementFromObjectToRender() * Units.RadToDeg)
    End Sub
#End Region

End Class
