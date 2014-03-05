Imports NUnit.Framework

<TestFixture()> Public Class TestSiTechController

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub FromSiTechReadControllerData()
        Dim SiTechController As SiTechController = CType(DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT)), Devices.SiTechController)
        Assert.IsNotNull(SiTechController)

        With SiTechController.SiTechReadControllerData
            .SecMotorPosition = 1
            .PriMotorPosition = 2
            .SecEncoderPosition = 3
            .PriEncoderPosition = 4
            .EncodeByteReturn()
        End With
        SiTechController.UpdateFromSiTechReadControllerData()

        ' primary axis is built 1st
        Dim encoders As ArrayList = SiTechController.GetDevicesByName(SiTechController.MotorEncoderName)
        Assert.AreEqual(1, eMath.RInt(CType(encoders(1), Encoder).Value))
        Assert.AreEqual(2, eMath.RInt(CType(encoders(0), Encoder).Value))

        Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryEncoder(SiTechController), Devices.Encoder)
        Assert.AreEqual(3, eMath.RInt(encoder.Value))

        encoder = CType(SiTechUtil.GetPrimaryEncoder(SiTechController), Devices.Encoder)
        Assert.AreEqual(4, eMath.RInt(encoder.Value))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub ToSiTechReadControllerData()
        Dim SiTechController As SiTechController = CType(DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT)), Devices.SiTechController)
        Assert.IsNotNull(SiTechController)

        ' primary axis is built 1st
        Dim encoders As ArrayList = SiTechController.GetDevicesByName(SiTechController.MotorEncoderName)
        CType(encoders(1), Encoder).Value = CStr(1)
        CType(encoders(0), Encoder).Value = CStr(2)

        Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryEncoder(SiTechController), Devices.Encoder)
        encoder.Value = CStr(3)

        encoder = CType(SiTechUtil.GetPrimaryEncoder(SiTechController), Devices.Encoder)
        encoder.Value = CStr(4)

        SiTechController.UpdateSiTechReadControllerData()

        With SiTechController.SiTechReadControllerData
            Assert.AreEqual(1, .SecMotorPosition)
            Assert.AreEqual(2, .PriMotorPosition)
            Assert.AreEqual(3, .SecEncoderPosition)
            Assert.AreEqual(4, .PriEncoderPosition)
        End With

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

