Public MustInherit Class UserCtrlControllerFactory

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pUserControl As Windows.forms.UserControl
    Protected pViewArgs() As Object
    Protected pModels() As Object
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlControllerFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlControllerFactory = New UserCtrlControllerFactory
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As UserCtrlControllerFactory
    '    Return New UserCtrlControllerFactory
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function BuildAndInit(ByRef userControl As Windows.Forms.UserControl, ByRef ViewArgs() As Object) As UserCtrlController
        Dim UserCtrlController As UserCtrlController

        pUserControl = userControl
        pViewArgs = ViewArgs
        pModels = GetModels()
        ' get UserCtrlController from subclass
        UserCtrlController = GetUserCtrlController()
        ' run subclass UserCtrlController's Init()
        UserCtrlController.Init(userControl, ViewArgs, pModels)

        Return UserCtrlController
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected MustOverride Function GetModels() As Object()
    Protected MustOverride Function GetUserCtrlController() As UserCtrlController
#End Region

End Class
