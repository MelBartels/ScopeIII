#Region "Imports"
#End Region

Public Class DevPropType
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
    Private pType As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevPropType
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevPropType = New DevPropType
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DevPropType
        Return New DevPropType
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Type() As String
        Get
            Return pType
        End Get
        Set(ByVal Value As String)
            pType = Value
        End Set
    End Property

    Public Overrides Property Value() As String
        Get
            Return Type()
        End Get
        Set(ByVal value As String)
            Type = value
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Dim DevPropType As DevPropType = ScopeIII.Devices.DevPropType.GetInstance
        DevPropType.Type = Type
        Return DevPropType
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
