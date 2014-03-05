#Region "Imports"
#End Region

Public Class DevPropIOType
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
    Private pIOType As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevPropIOType
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevPropIOType = New DevPropIOType
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DevPropIOType
        Return New DevPropIOType
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IOType() As String
        Get
            Return pIOType
        End Get
        Set(ByVal Value As String)
            pIOType = Value
        End Set
    End Property

    Public Overrides Property Value() As String
        Get
            Return IOType
        End Get
        Set(ByVal value As String)
            IOType = value
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Dim DevPropIOType As DevPropIOType = ScopeIII.Devices.DevPropIOType.GetInstance
        DevPropIOType.IOType = IOType
        Return DevPropIOType
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
