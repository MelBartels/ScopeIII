#Region "Imports"
#End Region

Public MustInherit Class DeviceCmdFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pDeviceToIOBridge As DeviceToIOBridge
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceCmdFacadeBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceCmdFacadeBase = New DeviceCmdFacadeBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As DeviceCmdFacadeBase
    '    Return New DeviceCmdFacadeBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' DeviceToIOBridge contains both IIO and IDevice
    Public Property DeviceToIOBridge() As DeviceToIOBridge
        Get
            If pDeviceToIOBridge Is Nothing Then
                pDeviceToIOBridge = DeviceToIOBridge.GetInstance
            End If
            Return pDeviceToIOBridge
        End Get
        Set(ByVal value As DeviceToIOBridge)
            pDeviceToIOBridge = value
        End Set
    End Property

    Public Property IIO() As IO.IIO
        Get
            Return DeviceToIOBridge.IIO
        End Get
        Set(ByVal Value As IO.IIO)
            DeviceToIOBridge.IIO = Value
        End Set
    End Property

    Public Property IDevice() As IDevice
        Get
            Return DeviceToIOBridge.IDevice
        End Get
        Set(ByVal Value As IDevice)
            DeviceToIOBridge.IDevice = Value
        End Set
    End Property

    Public Function BuildIIO(ByRef IDevice As IDevice) As Boolean
        Me.IDevice = IDevice
        Return DeviceToIOBridge.BuildIIO()
    End Function

    Public Function IIOOpen() As Boolean
        Return DeviceToIOBridge.IIOOpen
    End Function

    Public Sub ShutdownIO()
        DeviceToIOBridge.Shutdown()
    End Sub

    Public MustOverride Function ProcessMsg(ByRef [object] As Object) As Boolean
#End Region

#Region "Private and Protected Methods"
    ' begin to listen for command results, handled by ProcessMsg()
    Protected Sub startIOListening(ByRef IObserver As IObserver)
        IIO.ReceiveObservers.Attach(IObserver)
    End Sub
#End Region

End Class
