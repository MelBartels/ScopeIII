#Region "Imports"
#End Region

Public Class DevPropName
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
    Private pName As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevPropName
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevPropName = New DevPropName
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DevPropName
        Return New DevPropName
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Name() As String
        Get
            Return pName
        End Get
        Set(ByVal Value As String)
            pName = Value
        End Set
    End Property

    Public Overrides Property Value() As String
        Get
            Return Name
        End Get
        Set(ByVal value As String)
            Name = value
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Dim DevPropName As DevPropName = ScopeIII.Devices.DevPropName.GetInstance
        DevPropName.Name = Name
        Return DevPropName
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
