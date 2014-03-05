#Region "Imports"
#End Region

Public MustInherit Class GaugeValueBase
    Implements IGaugeValue

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pUOM As ISFT
    Protected pMaxValue As Double
    Protected pMinValue As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As GaugeValueBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As GaugeValueBase = New GaugeValueBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As GaugeValueBase
    '    Return New GaugeValueBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property UOM() As ISFT Implements IGaugeValue.UOM
        Get
            Return pUOM
        End Get
        Set(ByVal value As ISFT)
            pUOM = value
        End Set
    End Property

    Public Property MaxValue() As Double Implements IGaugeValue.MaxValue
        Get
            Return pMaxValue
        End Get
        Set(ByVal value As Double)
            pMaxValue = value
        End Set
    End Property

    Public Property MinValue() As Double Implements IGaugeValue.MinValue
        Get
            Return pMinValue
        End Get
        Set(ByVal value As Double)
            pMinValue = value
        End Set
    End Property

    Public Function Validate(ByVal value As Double) As Double Implements IGaugeValue.Validate
        If value > MaxValue Then
            value = MaxValue
        End If
        If value < MinValue Then
            value = MinValue
        End If
        Return value
    End Function

    Public Function ValueRange() As Double Implements IGaugeValue.ValueRange
        Return MaxValue - MinValue
    End Function

    Public Function ValueIncrement(ByVal incr As Double) As Double Implements IGaugeValue.ValueIncrement
        Return (MaxValue - MinValue) / incr
    End Function

    Public MustOverride Function ScaleValue(ByVal percentOfScale As Double) As Double Implements IGaugeValue.ScaleValue

    Public MustOverride Function ScalePercent(ByVal value As Double) As Double Implements IGaugeValue.ScalePercent

#End Region

#Region "Private and Protected Methods"
#End Region

End Class

