#Region "Imports"
#End Region

Public Class IOPresenterDevice
    Implements IIOPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pDeviceToIOBridge As DeviceToIOBridge
    Private pProcessMsgObserver As IObserver
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IOPresenterDevice
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IOPresenterDevice = New IOPresenterDevice
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As IOPresenterDevice
        Return New IOPresenterDevice
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property DeviceToIOBridge() As DeviceToIOBridge
        Get
            Return pDeviceToIOBridge
        End Get
        Set(ByVal value As DeviceToIOBridge)
            pDeviceToIOBridge = value
        End Set
    End Property

    Public Property IIO() As IIO Implements IIOPresenter.IIO
        Get
            Return DeviceToIOBridge.IIO
        End Get
        Set(ByVal value As IIO)
            DeviceToIOBridge.IIO = value
        End Set
    End Property

    Public Function BuildIO(ByRef ioType As ISFT) As Boolean Implements IIOPresenter.BuildIO
        DeviceToIOBridge.IOType = ioType
        Return DeviceToIOBridge.BuildIIO()
    End Function

    Public Sub ShutdownIO() Implements IO.IIOPresenter.ShutdownIO
        DeviceToIOBridge.Shutdown()
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
