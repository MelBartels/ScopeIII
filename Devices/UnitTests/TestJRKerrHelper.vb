Imports NUnit.Framework

<TestFixture()> Public Class TestJRKerrHelper

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
        jrk.JRKerrModule.StatusItems = SEND_POS
        Dim statusBytes() As Byte = JRKerrHelper.EncodeStatusBytes(IDevice)
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
        JRKerrHelper.EncodeStatusValues(IDevice)
        Assert.AreEqual(0, jrk.JRKerrModule.Position)

        ' build status return manually
        jrk.JRKerrModule.StatusItems = SEND_POS
        Dim statusBytes() As Byte = JRKerrHelper.EncodeStatusBytes(IDevice)
        Assert.AreEqual(6, statusBytes.Length)
        ' least significant byte 1st
        statusBytes(1) = 232
        statusBytes(2) = 3
        JRKerrHelper.SetReplyChecksum(statusBytes)
        'checksum is 1+232+3=236
        Assert.AreEqual(236, statusBytes(5))
        ' see if status return was processed correctly
        Dim testPosition As Int32 = 1000
        JRKerrHelper.DecodeStatusBytes(IDevice, statusBytes)
        Assert.AreEqual(testPosition, jrk.JRKerrModule.Position)
        Assert.AreEqual(CStr(testPosition), encoder.Value)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestUpdateDeviceCmdAndReplyTemplateList()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))
        Dim valueObject As ValueObject = IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name)
        ' verify that module address starts at 0
        Assert.AreEqual(CStr(0), valueObject.Value)
        ' verify that starting set address cmd's address is 0
        Dim deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate = IDevice.DeviceCmdsFacade.GetDeviceCmdAndReplyTemplate(CType(JRKerrCmds.ReadStatus, ISFT))
        Dim cmdMsgParms As CmdMsgParms = deviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms
        Dim cmdBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd)
        Assert.AreEqual(0, cmdBytes(JRKerrHelper.Indeces.Address))

        ' update module address to 1
        valueObject.Value = CStr(1)
        JRKerrHelper.UpdateDeviceCmdAndReplyTemplateList(IDevice)
        cmdBytes = BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd)
        Assert.AreEqual(1, cmdBytes(JRKerrHelper.Indeces.Address))



        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

