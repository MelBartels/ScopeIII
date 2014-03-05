Imports NUnit.Framework

<TestFixture()> Public Class TestDevicesProperties

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestGetDevicesByName()
        Dim eb As EncodersBox = CType(ScopeIII.Devices.DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT)), EncodersBox)
        Assert.AreEqual(1, (eb.GetDevicesByName(eb.PriAxisName).Count))
    End Sub

    <Test()> Public Sub TestGetDevicesByType()
        Dim eb As EncodersBox = CType(ScopeIII.Devices.DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT)), EncodersBox)
        Assert.AreEqual(2, (eb.GetDevicesByType(GetType(Encoder).Name).Count))
    End Sub

    <Test()> Public Sub TestGetProperty()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT))
        Dim prop As IDevProp = IDevice.GetProperty(GetType(DevPropStatus).Name)
        Assert.IsTrue(CObj(prop).GetType.Name.Equals(GetType(DevPropStatus).Name))
    End Sub

    <Test()> Public Sub TestGetProperties()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.ServoMotor, ISFT))
        Dim props As System.Collections.Generic.List(Of IDevProp) = IDevice.GetProperties(GetType(ValueObject).Name)
        Assert.IsTrue(props.Count > 1)
    End Sub

    <Test()> Public Sub TestGetValueObject()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.ServoMotor, ISFT))
        Dim valueObject As ValueObject = IDevice.GetValueObject(ValueObjectNames.Velocity.Name)
        Assert.IsNotNull(valueObject)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

