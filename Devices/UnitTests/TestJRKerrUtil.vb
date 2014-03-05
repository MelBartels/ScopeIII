Imports NUnit.Framework

<TestFixture()> Public Class TestJRKerrUtil

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestReturnStatusEncode()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))
        Dim jrk As JRKerrServoController = CType(IDevice, JRKerrServoController)
        Dim encoder As Encoder = CType(jrk.GetDevicesByName(ServoMotor.MotorEncoderName).Item(0), Encoder)
        ' add position only
        ' in hex: 0x3E8 or 0,0,3,232 in bytes
        Dim testPosition As Int32 = 1000
        encoder.Value = CStr(testPosition)
        JRKerrUtil.GetJRKerrServoStatus(IDevice).StatusItems = SEND_POS
        Dim statusBytes() As Byte = JRKerrUtil.EncodeStatusBytes(IDevice)
        Assert.AreEqual(6, statusBytes.Length)
        ' status byte is MOVE_DONE set in DeviceCmdBuilder
        Assert.AreEqual(MOVE_DONE, statusBytes(0))
        ' least significant byte 1st
        Assert.AreEqual(232, statusBytes(1))
        Assert.AreEqual(3, statusBytes(2))
        Assert.AreEqual(0, statusBytes(3))
        Assert.AreEqual(0, statusBytes(4))
        'checksum is 1+232+3=236
        Assert.AreEqual(236, statusBytes(5))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestReturnStatusDecode()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))
        Dim jrk As JRKerrServoController = CType(IDevice, JRKerrServoController)
        Dim encoder As Encoder = CType(jrk.GetDevicesByName(ServoMotor.MotorEncoderName).Item(0), Encoder)
        Assert.AreEqual(CStr(0), encoder.Value)
        JRKerrUtil.EncodeStatusValues(IDevice)
        Assert.AreEqual(0, JRKerrUtil.GetJRKerrServoStatus(IDevice).Position)

        ' build status return manually
        JRKerrUtil.GetJRKerrServoStatus(IDevice).StatusItems = SEND_POS
        Dim statusBytes() As Byte = JRKerrUtil.EncodeStatusBytes(IDevice)
        Assert.AreEqual(6, statusBytes.Length)
        ' least significant byte 1st
        statusBytes(1) = 232
        statusBytes(2) = 3
        JRKerrUtil.SetChecksum(statusBytes)
        'checksum is 1+232+3=236
        Assert.AreEqual(236, statusBytes(5))
        ' see if status return was processed correctly
        Dim testPosition As Int32 = 1000
        JRKerrUtil.DecodeStatusBytes(IDevice, statusBytes)
        Assert.AreEqual(testPosition, JRKerrUtil.GetJRKerrServoStatus(IDevice).Position)
        Assert.AreEqual(CStr(testPosition), encoder.Value)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestVerifyChecksum()
        Dim bytes() As Byte = {1, 2, 3, 6}
        Assert.IsTrue(JRKerrUtil.VerifyChecksum(bytes))
        bytes(3) = 0
        Assert.IsFalse(JRKerrUtil.VerifyChecksum(bytes))

        Assert.IsTrue(True)
    End Sub

    ' 1st byte (the cmd byte) is ignored
    <Test()> Public Sub TestVerifyCmdChecksum()
        Dim bytes() As Byte = {1, 2, 3, 5}
        Assert.IsTrue(JRKerrUtil.VerifyCmdChecksum(bytes))
        bytes(3) = 0
        Assert.IsFalse(JRKerrUtil.VerifyCmdChecksum(bytes))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

