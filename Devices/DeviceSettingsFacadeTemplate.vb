#Region "Imports"
#End Region

Public Class DeviceSettingsFacadeTemplate

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
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceSettingsFacadeTemplate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceSettingsFacadeTemplate = New DeviceSettingsFacadeTemplate
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceSettingsFacadeTemplate
        Return New DeviceSettingsFacadeTemplate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function LoadDeviceSettingsFacadeTemplate() As SettingsFacadeTemplate
        Return LoadDeviceSettingsFacadeTemplate(ScopeLibrary.Constants.DevicesSettings)
    End Function

    Public Function LoadDeviceSettingsFacadeTemplate(ByVal deviceName As String) As SettingsFacadeTemplate
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance.Build(DevicesSettings.GetInstance, deviceName)
        settingsFacadeTemplate.LoadSettingsFromConfig()

        Dim deviceTemplateBuilder As DeviceTemplateBuilder = deviceTemplateBuilder.GetInstance
        For Each IDevice As IDevice In CType(settingsFacadeTemplate.ISettings, ScopeIII.Devices.DevicesSettings).DevicesPropContainer.IDevices
            deviceTemplateBuilder.Build(IDevice)
        Next

        Return settingsFacadeTemplate
    End Function

    Public Function BuildDeviceSettingsFacadeTemplate(ByRef IDevice As IDevice) As SettingsFacadeTemplate
        Dim devicesSettings As DevicesSettings = devicesSettings.GetInstance
        devicesSettings.DevicesPropContainer = DevicesPropContainer.GetInstance
        devicesSettings.DevicesPropContainer.Add(IDevice)
        Return SettingsFacadeTemplate.GetInstance.Build(CType(devicesSettings, ISettings), IDevice.GetProperty(GetType(DevPropName).Name).Value)
    End Function

#End Region

#Region "Private and Protected Methods"
#End Region

End Class
