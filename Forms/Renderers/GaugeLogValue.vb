#Region "Imports"
#End Region

Public Class GaugeLogValue
    Inherits GaugeValueBase

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

    'Public Shared Function GetInstance() As GaugeLogValue
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As GaugeLogValue = New GaugeLogValue
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As GaugeLogValue
        Return New GaugeLogValue
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function ScaleValue(ByVal percentOfScale As Double) As Double
        Return eMath.GetValueLogScaling(percentOfScale, MinValue, MaxValue)
    End Function

    Public Overrides Function ScalePercent(ByVal value As Double) As Double
        Return eMath.GetScalePercentLogScaling(value, MinValue, MaxValue)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class

