#Region "Imports"
#End Region

Public Class ArcminSliderRenderer
    Inherits RendererHorizSliderGaugeBase

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

    'Public Shared Function GetInstance() As ArcminSliderRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ArcminSliderRenderer = New ArcminSliderRenderer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIGaugeValue.MinValue = -2 * Units.DegToRad
        pIGaugeValue.MaxValue = 2 * Units.DegToRad
        pNumMarks = 8
        pStepMeasurement = pIGaugeValue.ValueIncrement(pNumMarks)
        pIGaugeValue.UOM = UOM.Arcmin
    End Sub

    Public Shared Function GetInstance() As ArcminSliderRenderer
        Return New ArcminSliderRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        MyBase.Render(g, width, height)
        Return g
    End Function

    Public Overrides Function MeasurementFromObjectToRender() As Double
        Return CType(pObjectToRender, Coordinate).Rad
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Function formatMeasurement(ByVal measurement As Double) As String
        Return CStr(emath.rint(measurement * Units.RadToArcmin))
    End Function
#End Region

End Class
