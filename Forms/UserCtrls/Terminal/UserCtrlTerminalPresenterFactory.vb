#Region "Imports"
#End Region

Public Class UserCtrlTerminalPresenterFactory

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "protected and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'protected Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlTerminalPresenterFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'protected Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlTerminalPresenterFactory = New UserCtrlTerminalPresenterFactory
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlTerminalPresenterFactory
        Return New UserCtrlTerminalPresenterFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Build(ByRef IIOPresenter As IIOPresenter, ByRef userCtrlTerminal As UserCtrlTerminal) As IUserCtrlTerminalPresenter
        Dim presenter As IUserCtrlTerminalPresenter = CType(FormsDependencyInjector.GetInstance.IUserCtrlTerminalPresenterFactory, UserCtrlTerminalPresenter)
        ' build IIOPresenter before user control is added becauser setting .IMVPUserCtrl results in IIOPresenter.IIO being used
        presenter.IIOPresenter = IIOPresenter
        presenter.IMVPUserCtrl = CType(FormsDependencyInjector.GetInstance.IUserCtrlTerminalFactory(userCtrlTerminal), IMVPUserCtrl)
        Return presenter
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
