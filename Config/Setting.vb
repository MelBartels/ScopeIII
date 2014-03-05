#Region "Imports"
#End Region

Public Class Setting

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
    Private pTag As Object
    Private pType As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Setting
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Setting = New Setting
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Setting
        Return New Setting
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

    Public Property Tag() As Object
        Get
            Return pTag
        End Get
        Set(ByVal Value As Object)
            pTag = Value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return pType
        End Get
        Set(ByVal Value As String)
            pType = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region
End Class
