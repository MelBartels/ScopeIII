#Region "Imports"
#End Region

Public Class EndoTestUserCtrlTerminalPresenter
    Inherits UserCtrlTerminalPresenter
    Implements IUserCtrlTerminalPresenter

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
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As EndoTestUserCtrlTerminalPresenter
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As EndoTestUserCtrlTerminalPresenter = New EndoTestUserCtrlTerminalPresenter
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Shadows Function GetInstance() As EndoTestUserCtrlTerminalPresenter
        Return New EndoTestUserCtrlTerminalPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function EndoTestUserCtrlTerminal() As EndoTestUserCtrlTerminal
        Return CType(IMVPUserCtrl, EndoTestUserCtrlTerminal)
    End Function

    Public Sub EndoTestToggleOpenClose()
        toggleOpenClose()
    End Sub

    Public Sub EndoTestPortType(ByVal portDesc As String)
        portType(portDesc)
    End Sub

    Public Sub EndoTestLogging(ByVal switch As Boolean)
        logging(switch)
    End Sub

    Public Sub EndoTestSendText(ByVal text As String)
        sendText(text)
    End Sub

    Public Sub UpdatePortViaSettingsPresenter(ByVal port As Int32)
        pSettingsPresenter.SettingsFacadeTemplate.ISettings = IIO.IOSettingsFacadeTemplate.ISettings
        CType(pSettingsPresenter.SettingsFacadeTemplate.ISettings, IPsettings).Port = port
        settingsUpdated()
    End Sub

    Public Sub UpdatePortViaPropGridPresenter(ByVal port As Int32)
        Dim endoTestPropGridPresenter As EndoTestPropGridPresenter = CType(pSettingsPresenter.IPropGridPresenter, EndoTestPropGridPresenter)
        Dim IPSettings As IPsettings = CType(endoTestPropGridPresenter.SettingsFacadeTemplate.ISettings, IPsettings)
        Dim clonedIPSettings As IPsettings = CType(endoTestPropGridPresenter.ISettingsFacadeClone.ISettings, IPsettings)
        clonedIPSettings.Port = port
        endoTestPropGridPresenter.FireOKButton()
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
