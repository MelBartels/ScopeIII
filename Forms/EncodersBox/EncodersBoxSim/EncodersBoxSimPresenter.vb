#Region "imports"
#End Region

Public Class EncodersBoxSimPresenter
    Inherits EncodersBoxPresenterBase
    Implements IEncodersBoxSimPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "protected and Protected Members"
    Protected pDeviceReceiveCmdFacade As DeviceReceiveCmdFacade
#End Region

#Region "Constructors (Singleton Pattern)"
    'protected Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EncodersBoxSimPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'protected Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EncodersBoxSimPresenter = New EncodersBoxSimPresenter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As EncodersBoxSimPresenter
        Return New EncodersBoxSimPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        buildDeviceReceiveCmdFacade()
        setTermPortTypeToDeviceType()
    End Sub

#Region "CmdFacade"
    ' share DeviceToIOBridge instance between pDeviceReceiveCmdFacade and pUserCtrlTerminalPresenter
    Protected Sub buildDeviceReceiveCmdFacade()
        pDeviceReceiveCmdFacade = ScopeIII.Devices.DeviceReceiveCmdFacade.GetInstance
        pDeviceReceiveCmdFacade.IDevice = getEncodersBox()
        CType(pUserCtrlTerminalPresenter.IIOPresenter, IOPresenterDevice).DeviceToIOBridge = pDeviceReceiveCmdFacade.DeviceToIOBridge
    End Sub

    Protected Overrides Sub newPortBuilt()
        pDeviceReceiveCmdFacade.IIO.StatusObservers.Attach(CType(ObserverWithID.GetInstance.Build(Me.GetType.Name, "IO", Me), IObserver))
        pDeviceReceiveCmdFacade.StartIOListening()
        updateTerminalIOFromDevice()
    End Sub
#End Region

#End Region

End Class
