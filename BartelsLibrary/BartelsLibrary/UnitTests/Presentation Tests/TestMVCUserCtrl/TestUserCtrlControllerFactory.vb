#Region "imports"
#End Region

Public Class TestUserCtrlControllerFactory
    Inherits UserCtrlControllerFactory

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

    'Public Shared Function GetInstance() As TestUserCtrlControllerFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestUserCtrlControllerFactory = New TestUserCtrlControllerFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As TestUserCtrlControllerFactory
        Return New TestUserCtrlControllerFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Function GetUserCtrlController() As UserCtrlController
        Return TestUserCtrlController.GetInstance
    End Function

    Protected Overrides Function GetModels() As Object()
        Return Nothing
    End Function
#End Region

End Class
