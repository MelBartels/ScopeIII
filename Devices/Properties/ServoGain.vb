#Region "Imports"
#End Region

Public Class ServoGain
    Inherits DevPropBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pPositionGainKp As Int16
    Private pDerivativeGainKd As Int16
    Private pIntegralGainKi As Int16
    Private pIntegrationLimitIL As Int16
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ServoGain
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ServoGain = New ServoGain
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As ServoGain
        Return New ServoGain
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property PositionGainKp() As Short
        Get
            Return pPositionGainKp
        End Get
        Set(ByVal value As Short)
            pPositionGainKp = value
        End Set
    End Property

    Public Property DerivativeGainKd() As Short
        Get
            Return pDerivativeGainKd
        End Get
        Set(ByVal value As Short)
            pDerivativeGainKd = value
        End Set
    End Property

    Public Property IntegralGainKi() As Short
        Get
            Return pIntegralGainKi
        End Get
        Set(ByVal value As Short)
            pIntegralGainKi = value
        End Set
    End Property

    Public Property IntegrationLimitIL() As Short
        Get
            Return pIntegrationLimitIL
        End Get
        Set(ByVal value As Short)
            pIntegrationLimitIL = value
        End Set
    End Property

    Public Overrides Property Value() As String
        Get
            Return ToString()
        End Get
        Set(ByVal value As String)
            ' no setter
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Return Me.MemberwiseClone
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.Append(PositionGainKp)
        sb.Append(", ")
        sb.Append(DerivativeGainKd)
        sb.Append(", ")
        sb.Append(IntegralGainKi)
        sb.Append(", ")
        sb.Append(IntegrationLimitIL)
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
