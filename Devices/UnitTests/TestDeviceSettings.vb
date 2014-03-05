Imports NUnit.Framework

<TestFixture()> Public Class TestDevicesSettings

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    ' object relationship:
    ' SettingsFacadeTemplate (type=SettingsFacadeTemplate)
    ' SettingsFacade (type=SettingsFacade)
    ' SettingsPackage (type=SettingsPackage), inner class in SettingsFacade
    ' Settings (type=ArrayList), contained w/in SettingsPackage
    ' Setting (type=Setting), public properties Name, Tag, and Type
    ' Tag (type=Object), is an arraylist of IDevice's
    ' IDevice (type=IDevice), a member of the above array
    '     (DeviceTemplateArrayIx and GetDeviceTemplate() point to device's DeviceTemplate (type=DeviceTemplate) contained w/in the DeviceTemplateArray)
    ' Properties (type=ArrayList)
    ' EncoderValue (type=EncoderValue)
    ' MaxValue (type=string)

    <Test()> Public Sub TestBuildSettingsFacadeTemplate()
        ' create device, SettingsFacadeTemplate 
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT))
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.BuildDeviceSettingsFacadeTemplate(IDevice)
        Dim deviceSettingsName As String = settingsFacadeTemplate.ISettings.Name
        ' set test value
        Dim testMaxValue As String = CStr(5)
        CType(IDevice.GetProperty(GetType(EncoderValue).Name), EncoderValue).MaxValue = testMaxValue
        ' save device through settings facade
        settingsFacadeTemplate.SaveSettingsToConfig()
        ' retrieve settings
        Dim loadedSettingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.LoadDeviceSettingsFacadeTemplate(deviceSettingsName)
        Dim loadedIDevice As IDevice = CType(CType(loadedSettingsFacadeTemplate.ISettings, DevicesSettings).DevicesPropContainer.IDevices(0), IDevice)
        ' retrieve value
        Dim loadedMaxValue As String = CType(loadedIDevice.GetProperty(GetType(EncoderValue).Name), EncoderValue).MaxValue
        ' see if it matches value before saving the config
        Assert.AreEqual(eMath.RInt(testMaxValue), eMath.RInt(loadedMaxValue))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestLoadDeviceSettingsFacadeTemplate()
        Dim templateCount1 As Int32 = DeviceTemplateArray.GetInstance.DeviceTemplates.Count
        DeviceSettingsFacadeTemplate.GetInstance.LoadDeviceSettingsFacadeTemplate()
        Dim templateCount2 As Int32 = DeviceTemplateArray.GetInstance.DeviceTemplates.Count
        Assert.Greater(templateCount2, templateCount1)
        DeviceSettingsFacadeTemplate.GetInstance.LoadDeviceSettingsFacadeTemplate(DeviceName.Encoder.Name)
        Dim templateCount3 As Int32 = DeviceTemplateArray.GetInstance.DeviceTemplates.Count
        Assert.Greater(templateCount3, templateCount2)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestStatuses()
        ' create encoder device and set status to inactive; default is active...

        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT))
        Dim statusType As String = GetType(DeviceStatus).Name
        Dim statusValue As String = DeviceStatus.Inactive.Name
        Dim testReached As Boolean = False
        For Each prop As Object In IDevice.Properties
            If prop.GetType.Name.Equals(GetType(DevPropStatus).Name) Then
                CType(prop, DevPropStatus).SetStatus(statusType, statusValue)
                testReached = True
                Exit For
            End If
        Next
        Assert.IsTrue(testReached)

        ' via SettingsFacade, saving the encoder as type 'Encoder'...

        Dim SF As SettingsFacade = SettingsFacade.GetInstance
        Dim al As New ArrayList
        al.Add(IDevice)
        SF.Settings = al
        SF.SaveConfig()
        ' now reload from cfg and test if changed status value was saved/loaded
        SF.ClearSettings()
        SF.LoadConfig()
        IDevice = CType(SF.Settings(0), ScopeIII.Devices.IDevice)
        testReached = False
        For Each prop As Object In IDevice.Properties
            If prop.GetType.Name.Equals(GetType(DevPropStatus).Name) Then
                Assert.IsTrue(statusValue.Equals(CType(prop, DevPropStatus).GetStatus(statusType)))
                testReached = True
            End If
        Next
        Assert.IsTrue(testReached)
        ' get status without using the for each loop
        Assert.IsTrue(statusValue.Equals(CType(IDevice.GetProperty(GetType(DevPropStatus).Name), DevPropStatus).GetStatus(statusType)))

        ' via SettingsFacadeTemplate, saving as type 'Setting'...

        Dim settingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.BuildDeviceSettingsFacadeTemplate(IDevice)
        settingsFacadeTemplate.SaveSettingsToConfig()
        ' load from scratch
        Dim loadedSettingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.LoadDeviceSettingsFacadeTemplate(DeviceName.Encoder.Name)
        Dim loadedIDevice As IDevice = CType(CType(loadedSettingsFacadeTemplate.ISettings, DevicesSettings).DevicesPropContainer.IDevices(0), ScopeIII.Devices.IDevice)
        ' verify status 
        Assert.IsTrue(statusValue.Equals(CType(loadedIDevice.GetProperty(GetType(DevPropStatus).Name), DevPropStatus).GetStatus(statusType)))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeviceSaveLoad()
        ' add a single Device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT))

        ' via SettingsFacade, saving the encoder as type 'Encoder'...

        Dim SF As SettingsFacade = SettingsFacade.GetInstance
        Dim al As New ArrayList
        al.Add(IDevice)
        SF.Settings = al
        SF.SaveConfig()
        ' now reload settings and see if a single Device is there
        SF.ClearSettings()
        SF.LoadConfig()
        Assert.AreEqual(1, SF.Settings.Count)

        ' via SettingsFacadeTemplate, saving as type 'Setting'...

        Dim settingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.BuildDeviceSettingsFacadeTemplate(IDevice)
        settingsFacadeTemplate.SaveSettingsToConfig()
        ' load from scratch
        Dim loadedSettingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.LoadDeviceSettingsFacadeTemplate(DeviceName.Encoder.Name)
        ' verify single Device is there
        Assert.AreEqual(1, CType(loadedSettingsFacadeTemplate.ISettings, DevicesSettings).DevicesPropContainer.IDevices.Count)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestEncoderSettingsSingleton()
        ' create device and save
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT))
        Dim encoderDeviceName As String = "TestEncoderSettingsSingleton"
        CType(IDevice.GetProperty(GetType(DevPropName).Name), DevPropName).Name = encoderDeviceName
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.BuildDeviceSettingsFacadeTemplate(IDevice)
        settingsFacadeTemplate.SaveSettingsToConfig()
        ' load device, twice
        Dim loadedSettingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.LoadDeviceSettingsFacadeTemplate
        Dim setting As Setting = settingsFacadeTemplate.SettingsFacade.GetSetting(encoderDeviceName)
        Dim loadedSetting As Setting = loadedSettingsFacadeTemplate.SettingsFacade.GetSetting(encoderDeviceName)
        ' see if each loaded device refers to same IDevice object
        Dim origIDevice As IDevice = CType(CType(setting.Tag, DevicesSettings).DevicesPropContainer.IDevices(0), ScopeIII.Devices.IDevice)
        Dim loadedIDevice As IDevice = CType(CType(loadedSetting.Tag, DevicesSettings).DevicesPropContainer.IDevices(0), ScopeIII.Devices.IDevice)
        Assert.AreSame(origIDevice, loadedIDevice)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

