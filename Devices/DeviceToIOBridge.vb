#Region "Imports"
#End Region

Public Class DeviceToIOBridge

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIIO As IIO
    Private pIDevice As IDevice
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceToIOBridge
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceToIOBridge = New DeviceToIOBridge
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceToIOBridge
        Return New DeviceToIOBridge
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IIO() As IO.IIO
        Get
            Return pIIO
        End Get
        Set(ByVal Value As IO.IIO)
            pIIO = Value
        End Set
    End Property

    Public Property IDevice() As IDevice
        Get
            Return pIDevice
        End Get
        Set(ByVal Value As IDevice)
            pIDevice = Value
        End Set
    End Property

    Public Property IOType() As ISFT
        Get
            Return BartelsLibrary.IOType.ISFT.MatchString(getDevPropIOType.IOType)
        End Get
        Set(ByVal value As ISFT)
            getDevPropIOType.IOType = value.Name
        End Set
    End Property

    Public Property InspectorObservers() As ObservableImp
        Get
            Return IIO.ReceiveInspector.InspectorObservers
        End Get
        Set(ByVal value As ObservableImp)
            IIO.ReceiveInspector.InspectorObservers = value
        End Set
    End Property

    Public Function BuildIIO() As Boolean
        IIO = IOBuilder.GetInstance.Build(IOType)
        If IIO IsNot Nothing Then
            IDevice.SetIOStatus(IOStatus.Build.Description & " " & IIO.IOType.Description)
            Return True
        End If

        IDevice.SetIOStatus(IOStatus.BuildFailed.Description)
        Return False
    End Function

    Public Function IIOOpen() As Boolean
        If IIO.Open Then
            IDevice.SetIOStatus(IOStatus.Open.Description)
            Return True
        End If
        IDevice.SetIOStatus(IOStatus.OpenFailed.Description)
        Return False
    End Function

    Public Sub Shutdown()
        IIO.Shutdown()
        IDevice.SetIOStatus(IOStatus.Closed.Description)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Function getDevPropIOType() As DevPropIOType
        Return CType(IDevice.GetProperty(GetType(DevPropIOType).Name), DevPropIOType)
    End Function
#End Region
End Class
