Imports NUnit.Framework

<TestFixture()> Public Class TestAddDevice

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestAddDeviceObject()
        ' add device class
        Dim newDeviceName As String = "JRKerrServoController"
        Dim newDeviceISFT As ISFT = DeviceName.ISFT.MatchString(newDeviceName)
        Assert.IsNotNull(DeviceFactory.GetInstance.Build(newDeviceISFT))
    End Sub

    <Test()> Public Sub TestAddDeviceName()
        ' add to DeviceName
        Dim newDeviceName As String = "JRKerrServoController"
        Assert.IsNotNull(DeviceName.ISFT.MatchString(newDeviceName))
    End Sub

    <Test()> Public Sub TestAddDeviceCmdSet()
        ' add to DeviceCmdSet
        Dim newDeviceCmdSet As String = "JRKerrServoController"
        Assert.IsNotNull(DeviceCmdSet.ISFT.MatchString(newDeviceCmdSet))
    End Sub

    <Test()> Public Sub TestAddCmds()
        ' add cmd class
        Assert.IsNotNull(JRKerrCmds.GetInstance)
    End Sub

    <Test()> Public Sub TestAddDeviceCmdBuilderMethod()
        ' add method for each command to DeviceCmdAndReplyTemplateDefaults
        Assert.IsNotNull(DeviceCmdAndReplyTemplateDefaults.TestOne)
    End Sub

    <Test()> Public Sub TestPreLoadConfig()
        ' add device class to PreLoadConfig()
        Dim JRKerrServoController As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.BuildDeviceSettingsFacadeTemplate(JRKerrServoController)
        settingsFacadeTemplate.SaveSettingsToConfig()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

