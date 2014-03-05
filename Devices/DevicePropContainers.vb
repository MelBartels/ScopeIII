#Region "Imports"
#End Region

Public Class DevicePropContainers

#Region "Inner Classes"
    Private Class TestIOStatusObserver : Implements IObserver
        Public Success As Boolean
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Dim msg As String = CStr([object])
            If msg.Equals(IOStatus.ValidResponse.Name) Then
                Success = True
            Else
                Success = False
            End If
        End Function
    End Class

    Private Class TestSensorStatusObserver : Implements IObserver
        Public Success As Boolean
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Dim msg As String = CStr([object])
            If msg.Equals(SensorStatus.ValidRead.Name) Then
                Success = True
            Else
                Success = False
            End If
        End Function
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pDeviceFactory As DeviceFactory
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevicePropContainers
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevicePropContainers = New DevicePropContainers
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pDeviceFactory = DeviceFactory.GetInstance
    End Sub

    Public Shared Function GetInstance() As DevicePropContainers
        Return New DevicePropContainers
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function PropContainerFactory(ByVal namedDevice As ISFT) As DevicesPropContainer
        If namedDevice Is DeviceName.EncodersBox Then
            Return BuildEncodersBoxPropContainer()
        End If

        ExceptionService.Notify("Unable to build property container for device " & namedDevice.Name)
        Return Nothing
    End Function

    Public Function BuildEncodersBoxPropContainer() As DevicesPropContainer
        Dim devicesPropContainer As DevicesPropContainer = ScopeIII.Devices.DevicesPropContainer.GetInstance

        Dim IDevice As IDevice = pDeviceFactory.Build(CType(DeviceName.EncodersBox, ISFT))
        devicesPropContainer.Add(IDevice)

        Return devicesPropContainer
    End Function

    Public Function BuildDevicesSettingsPropContainer() As DevicesPropContainer
        Dim devicesPropContainer As DevicesPropContainer = ScopeIII.Devices.DevicesPropContainer.GetInstance

        ' build all known devices
        Dim eDeviceName As IEnumerator = DeviceName.ISFT.Enumerator
        While eDeviceName.MoveNext
            Dim IDevice As IDevice = pDeviceFactory.Build(CType(eDeviceName.Current, ISFT))
            devicesPropContainer.Add(IDevice)
        End While

        Return devicesPropContainer
    End Function

    Public Function BuildTestEncoderPropContainer() As DevicesPropContainer
        Dim devicesPropContainer As DevicesPropContainer = ScopeIII.Devices.DevicesPropContainer.GetInstance

        Dim IDevice As IDevice = pDeviceFactory.Build(CType(DeviceName.Encoder, ISFT), ScopeLibrary.Constants.TestEncoder)
        devicesPropContainer.Add(IDevice)

        Return devicesPropContainer
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
