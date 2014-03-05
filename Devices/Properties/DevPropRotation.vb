#Region "Imports"
#End Region

Public Class DevPropRotation
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
    Private pRotation As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevPropRotation
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevPropRotation = New DevPropRotation
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DevPropRotation
        Return New DevPropRotation
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Rotation() As String
        Get
            Return pRotation
        End Get
        Set(ByVal Value As String)
            pRotation = Value
        End Set
    End Property

    Public Overrides Property Value() As String
        Get
            Return Rotation
        End Get
        Set(ByVal value As String)
            Rotation = value
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Dim DevPropRotation As DevPropRotation = ScopeIII.Devices.DevPropRotation.GetInstance
        DevPropRotation.Rotation = Rotation
        Return DevPropRotation
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
