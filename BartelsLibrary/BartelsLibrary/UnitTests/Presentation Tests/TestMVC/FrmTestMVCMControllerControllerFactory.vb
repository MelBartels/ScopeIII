#Region "imports"
#End Region

Public Class frmTestMVCMControllerFactory
    Inherits ControllerFactory

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

    'Public Shared Function GetInstance() As frmTestMVCMControllerFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As frmTestMVCMControllerFactory = New frmTestMVCMControllerFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As frmTestMVCMControllerFactory
        Return New frmTestMVCMControllerFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Function GetController() As Controller
        Return frmTestMVCMController.GetInstance
    End Function

    Protected Overrides Function GetModels() As Object()
        Return Nothing
    End Function
#End Region

End Class
