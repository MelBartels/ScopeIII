#Region "imports"
#End Region

Public Class IOTerminalPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pUserCtrlTerminalPresenter As UserCtrlTerminalPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IOTerminalPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IOTerminalPresenter = New IOTerminalPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As IOTerminalPresenter
        Return New IOTerminalPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IIO() As IIO
        Get
            Return pUserCtrlTerminalPresenter.IIO
        End Get
        Set(ByVal Value As IIO)
            pUserCtrlTerminalPresenter.IIO = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        AddHandler frmIOTerminal.CloseForm, AddressOf closeForm

        pUserCtrlTerminalPresenter = CType(UserCtrlTerminalPresenterFactory.GetInstance.Build _
            (IOPresenterIO.GetInstance, frmIOTerminal.UserCtrlTerminal), UserCtrlTerminalPresenter)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function frmIOTerminal() As FrmIOTerminal
        Return CType(IMVPView, FrmIOTerminal)
    End Function

    Private Sub closeForm()
        pUserCtrlTerminalPresenter.CloseForm()
    End Sub
#End Region

End Class
