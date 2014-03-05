Imports NUnit.Framework

<TestFixture()> Public Class TestDevPropEncoderValue

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestClone()
        Dim encoderValue As EncoderValue = ScopeIII.Devices.EncoderValue.GetInstance
        encoderValue.Build(0, 1000, Rotation.CW.Name)
        Dim testValue As String = CStr(500)
        Dim testMinValue As String = CStr(-100)
        Dim testCarries As Int32 = 3
        encoderValue.Value = testValue
        encoderValue.MinValue = testMinValue
        encoderValue.Carries = testCarries
        ' verify that properties are set
        Assert.AreEqual(testValue, encoderValue.Value)
        Assert.AreEqual(testMinValue, encoderValue.MinValue)
        Assert.AreEqual(testCarries, encoderValue.Carries)
        ' clone object, including properties
        Dim clonedEncoderValue As EncoderValue = CType(encoderValue.Clone, ScopeIII.Devices.EncoderValue)
        ' verify that cloned properties came across
        Assert.AreEqual(testValue, clonedEncoderValue.Value)
        Assert.AreEqual(testMinValue, clonedEncoderValue.MinValue)
        Assert.AreEqual(testCarries, clonedEncoderValue.Carries)
        ' reset original object's values
        encoderValue.Value = CStr(0)
        encoderValue.MinValue = CStr(0)
        encoderValue.Carries = 0
        ' verify that cloned values didn't change
        Assert.AreEqual(testValue, clonedEncoderValue.Value)
        Assert.AreEqual(testMinValue, clonedEncoderValue.MinValue)
        Assert.AreEqual(testCarries, clonedEncoderValue.Carries)
    End Sub

    <Test()> Public Sub TestEncoderValueCarries()
        Dim encoderValue As EncoderValue = ScopeIII.Devices.EncoderValue.GetInstance
        encoderValue.Build(0, 9999, Rotation.CW.Name)
        Assert.AreEqual((9999).ToString, encoderValue.MaxValue)
        Assert.IsTrue(encoderValue.UOM.Equals(UOM.Counts.Name))
        encoderValue.Value = CStr(0)
        Assert.AreEqual(0, encoderValue.Carries)
        Assert.IsTrue(SensorStatus.ValidRead.Name.Equals(encoderValue.ValueStatus))
        encoderValue.Value = CStr(10)
        Assert.AreEqual(0, encoderValue.Carries)
        encoderValue.Value = CStr(20)
        Assert.AreEqual(0, encoderValue.Carries)
        encoderValue.Value = CStr(10)
        Assert.AreEqual(0, encoderValue.Carries)
        encoderValue.Value = CStr(0)
        Assert.AreEqual(0, encoderValue.Carries)
        encoderValue.Value = CStr(9990)
        Assert.AreEqual(-1, encoderValue.Carries)
        encoderValue.Value = CStr(9980)
        Assert.AreEqual(-1, encoderValue.Carries)
        encoderValue.Value = CStr(9990)
        Assert.AreEqual(-1, encoderValue.Carries)
        encoderValue.Value = CStr(0)
        Assert.AreEqual(0, encoderValue.Carries)
        encoderValue.Value = CStr(4000)
        Assert.AreEqual(0, encoderValue.Carries)
        encoderValue.Value = CStr(8000)
        Assert.AreEqual(0, encoderValue.Carries)
        encoderValue.Value = CStr(2000)
        Assert.AreEqual(1, encoderValue.Carries)
    End Sub

    <Test()> Public Sub TestEncoderValueSensorStatus()
        Dim encoderValue As EncoderValue = ScopeIII.Devices.EncoderValue.GetInstance
        encoderValue.Build(0, 9999, Rotation.CW.Name)
        Assert.AreEqual((9999).ToString, encoderValue.MaxValue)
        Assert.IsTrue(encoderValue.UOM.Equals(UOM.Counts.Name))
        encoderValue.Value = CStr(0)
        Assert.IsTrue(SensorStatus.ValidRead.Name.Equals(encoderValue.ValueStatus))
        encoderValue.Value = CStr(20000)
        Assert.IsTrue(SensorStatus.ExceedsMaximum.Name.Equals(encoderValue.ValueStatus))
        encoderValue.Value = CStr(-1000)
        Assert.IsTrue(SensorStatus.BelowMinimum.Name.Equals(encoderValue.ValueStatus))
    End Sub

    <Test()> Public Sub TestEncoderValueConvertRadToTicks()
        Dim encoderValue As EncoderValue = ScopeIII.Devices.EncoderValue.GetInstance

        encoderValue.Build(0, 3, Rotation.CW.Name)
        encoderValue.Value = encoderValue.ConvertRadToTicks(Units.HalfRev)
        Assert.AreEqual((2).ToString, encoderValue.Value)

        encoderValue.Build(0, 9999, Rotation.CW.Name)
        encoderValue.Value = encoderValue.ConvertRadToTicks(Units.HalfRev)
        Assert.AreEqual((5000).ToString, encoderValue.Value)
    End Sub

    <Test()> Public Sub TestTotalTicks()
        Dim encoderValue As EncoderValue = ScopeIII.Devices.EncoderValue.GetInstance

        encoderValue.Build(0, 3, Rotation.CW.Name)
        encoderValue.Value = CStr(2)
        encoderValue.Carries = 0
        Assert.AreEqual(2, encoderValue.TotalTicks)

        encoderValue.Build(-1000, 1999, Rotation.CW.Name)
        encoderValue.Value = CStr(50)
        encoderValue.Carries = 3
        Assert.AreEqual(10050, encoderValue.TotalTicks)
    End Sub

    <Test()> Public Sub TestEncoderValueGetEncoderCount()
        Dim testValue As String = (1000).ToString

        ' necessary for DeviceFactory.Build's DevicePropContainers.GetInstance.Build(IDevice)
        Dim eb As EncodersBox = CType(DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, SFTPrototype)), ScopeIII.Devices.EncodersBox)
        eb.SetValues(CStr(testValue), CStr(testValue))
        Dim encoder As Encoder = CType(eb.GetDevicesByName(eb.PriAxisName).Item(0), Encoder)
        Assert.AreEqual(testValue, encoder.Value)
        Assert.IsTrue(True)

        ' via encoder as IDevice and IEncoder
        Dim encoder2 As IDevice = CType(encoder.GetInstance, IDevice)
        encoder2.Build()
        ' must build the DeviceTemplate
        DeviceTemplateBuilder.GetInstance.Build(encoder2)
        CType(encoder2, Encoder).Value = testValue.ToString
        Assert.AreEqual(testValue, CType(encoder2, Encoder).Value)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestEncoderValueFromCfg()
        ' save a non-standard EncoderValue...

        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT))
        Dim encoderValue As EncoderValue = Nothing
        For Each prop As Object In IDevice.Properties
            If prop.GetType.Name.Equals(GetType(EncoderValue).Name) Then
                encoderValue = CType(prop, EncoderValue)
                Exit For
            End If
        Next
        ' encoder range is from 0 to 9, covering 10 ticks; default is range (covered ticks) - 1
        Dim testValue As String = ((Encoder.DefaultMaxEncoderValue + 1) / 2).ToString
        encoderValue.Value = testValue

        ' via SettingsFacade, saving the encoder as type 'Encoder'...

        Dim SF As SettingsFacade = SettingsFacade.GetInstance
        Dim al As New ArrayList
        al.Add(IDevice)
        SF.Settings = al
        SF.SaveConfig()
        ' reload config and see if non-standard EncoderValue is there
        SF.ClearSettings()
        SF.LoadConfig()
        Dim testReached As Boolean = False
        Dim encoderCfg As Encoder
        For Each device As Object In SF.Settings
            If device.GetType.Name.Equals(GetType(Encoder).Name) Then
                encoderCfg = CType(device, Encoder)
                For Each prop As Object In IDevice.Properties
                    If prop.GetType.Name.Equals(GetType(EncoderValue).Name) Then
                        encoderValue = CType(prop, EncoderValue)
                        Assert.IsTrue(testValue.Equals(encoderValue.Value))
                        testReached = True
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        ' via SettingsFacadeTemplate, saving as type 'Setting'...

        Dim settingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.BuildDeviceSettingsFacadeTemplate(IDevice)
        settingsFacadeTemplate.SaveSettingsToConfig()
        ' load from scratch
        Dim loadedSettingsFacadeTemplate As SettingsFacadeTemplate = DeviceSettingsFacadeTemplate.GetInstance.LoadDeviceSettingsFacadeTemplate(DeviceName.Encoder.Name)
        Dim loadedIDevice As IDevice = CType(CType(loadedSettingsFacadeTemplate.ISettings, DevicesSettings).DevicesPropContainer.IDevices(0), ScopeIII.Devices.IDevice)
        ' verify non-standard value
        Assert.AreEqual(testValue, CType(loadedIDevice, Encoder).Value)

        Assert.IsTrue(testReached)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

