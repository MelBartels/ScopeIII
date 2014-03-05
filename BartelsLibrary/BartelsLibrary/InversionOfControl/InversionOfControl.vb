#Region "Imports"
#End Region

Public Class InversionOfControl

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private Shared pResolver As IDependencyResolver
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As InversionOfControl
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As InversionOfControl = New InversionOfControl
    '    End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As InversionOfControl
        Return New InversionOfControl
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Shared Sub Initialize(ByVal resolver As IDependencyResolver)
        pResolver = resolver
    End Sub

    Public Shared Function Resolve(Of T)() As T
        Return pResolver.Resolve(Of T)()
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
