Imports NUnit.Framework

<TestFixture()> Public Class TestSiTechUtil

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestGetDevice()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsInstanceOfType(GetType(ServoMotor), SiTechUtil.GetDevice(IDevice, SiTechUtil.DeviceOrder.PrimaryServoMotor))
        Assert.IsInstanceOfType(GetType(Encoder), SiTechUtil.GetDevice(IDevice, SiTechUtil.DeviceOrder.PrimaryEncoder))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetDevPropName()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsInstanceOfType(GetType(DevPropName), SiTechUtil.GetDevPropName(IDevice))
        Assert.IsInstanceOfType(GetType(DevPropName), SiTechUtil.GetDevPropName(SiTechUtil.GetDevice(IDevice, SiTechUtil.DeviceOrder.PrimaryServoMotor)))
        Assert.IsInstanceOfType(GetType(DevPropName), SiTechUtil.GetDevPropName(SiTechUtil.GetDevice(IDevice, SiTechUtil.DeviceOrder.PrimaryEncoder)))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestIsPrimaryController()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsTrue(SiTechUtil.IsPrimaryController(IDevice))
        IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name).Value = CStr(3)
        Assert.IsFalse(SiTechUtil.IsPrimaryController(IDevice))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetPrimaryServoMotor()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsInstanceOfType(GetType(ServoMotor), SiTechUtil.GetPrimaryServoMotor(IDevice))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetSecondaryServoMotor()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsInstanceOfType(GetType(ServoMotor), SiTechUtil.GetSecondaryServoMotor(IDevice))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetPrimaryEncoder()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsInstanceOfType(GetType(Encoder), SiTechUtil.GetPrimaryEncoder(IDevice))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetSecondaryEncoder()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsInstanceOfType(GetType(Encoder), SiTechUtil.GetSecondaryEncoder(IDevice))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetPrimaryServoMotorEncoder()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsInstanceOfType(GetType(Encoder), SiTechUtil.GetPrimaryServoMotorEncoder(IDevice))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetSecondaryServoMotorEncoder()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        Assert.IsInstanceOfType(GetType(Encoder), SiTechUtil.GetSecondaryServoMotorEncoder(IDevice))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestBuildNames()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))

        Assert.AreEqual(SiTechAxes.PriAxis.Name & DeviceName.ServoMotor.Name, SiTechUtil.GetDevPropName(SiTechUtil.GetPrimaryServoMotor(IDevice)).Name)
        Assert.AreEqual(SiTechAxes.SecAxis.Name & DeviceName.ServoMotor.Name, SiTechUtil.GetDevPropName(SiTechUtil.GetSecondaryServoMotor(IDevice)).Name)
        Assert.AreEqual(SiTechAxes.PriAxis.Name & DeviceName.Encoder.Name, SiTechUtil.GetDevPropName(SiTechUtil.GetPrimaryEncoder(IDevice)).Name)
        Assert.AreEqual(SiTechAxes.SecAxis.Name & DeviceName.Encoder.Name, SiTechUtil.GetDevPropName(SiTechUtil.GetSecondaryEncoder(IDevice)).Name)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

