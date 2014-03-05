#Region "Imports"
#End Region

Public Class DegreeNegSliderRenderer
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

    'Public Shared Function GetInstance() As DegreeNegSliderRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DegreeNegSliderRenderer = New DegreeNegSliderRenderer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIGaugeValue.MinValue = -Units.HalfRev
        pIGaugeValue.MaxValue = Units.HalfRev
        pNumMarks = 8
        pStepMeasurement = pIGaugeValue.ValueIncrement(pNumMarks)
        pIGaugeValue.UOM = UOM.Degree
    End Sub

    Public Shared Function GetInstance() As DegreeNegSliderRenderer
        Return New DegreeNegSliderRenderer
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
        Return CStr(emath.rint(measurement * Units.RadToDeg))
    End Function
#End Region

End Class
