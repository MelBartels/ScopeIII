#Region "Imports"
#End Region

Public Class UserCtrlTerminalPresenterTestBuilder

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
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlTerminalPresenterTestBuilder
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As UserCtrlTerminalPresenterTestBuilder = New UserCtrlTerminalPresenterTestBuilder
    End Class
#End Region

#Region "Constructors"
    'Protected Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlTerminalPresenterTestBuilder
    '    Return New UserCtrlTerminalPresenterTestBuilder
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function BuildTestPresenter() As EndoTestUserCtrlTerminalPresenter
        Return CType(UserCtrlTerminalPresenterFactory.GetInstance.Build _
            (IOPresenterIO.GetInstance, New UserCtrlTerminal), EndoTestUserCtrlTerminalPresenter)
    End Function

    Public Function BuildTestPresenter(ByVal portDesc As String, ByVal port As Int32) As EndoTestUserCtrlTerminalPresenter
        Dim endoTestUserCtrlTerminalPresenter As EndoTestUserCtrlTerminalPresenter = BuildTestPresenter()
        endoTestUserCtrlTerminalPresenter.EndoTestUserCtrlTerminal.FireEvents = False
        ' eventually calls UserCtrlTerminalPresenter.buildIO (UserCtrlterminal must already be built because it is assigned as an IOobserver)
        endoTestUserCtrlTerminalPresenter.EndoTestPortType(portDesc)
        endoTestUserCtrlTerminalPresenter.UpdatePortViaSettingsPresenter(port)
        Return endoTestUserCtrlTerminalPresenter
    End Function

    Public Function BuildTestPresenterWithIOPresenterDevice(ByVal portDesc As String, ByVal port As Int32) As EndoTestUserCtrlTerminalPresenter
        Dim endoTestUserCtrlTerminalPresenter As EndoTestUserCtrlTerminalPresenter = CType(UserCtrlTerminalPresenterFactory.GetInstance.Build _
            (IOPresenterDevice.GetInstance, New UserCtrlTerminal), EndoTestUserCtrlTerminalPresenter)
        Dim deviceToIOBridge As DeviceToIOBridge = deviceToIOBridge.GetInstance
        deviceToIOBridge.IDevice = CType(ScopeIII.Devices.DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT)), EncodersBox)
        CType(endoTestUserCtrlTerminalPresenter.IIOPresenter, IOPresenterDevice).DeviceToIOBridge = deviceToIOBridge
        endoTestUserCtrlTerminalPresenter.EndoTestUserCtrlTerminal.FireEvents = False
        endoTestUserCtrlTerminalPresenter.EndoTestPortType(portDesc)
        endoTestUserCtrlTerminalPresenter.UpdatePortViaSettingsPresenter(port)
        Return endoTestUserCtrlTerminalPresenter
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
