Imports NUnit.Framework

<TestFixture()> Public Class TestDeviceCmdsFacade

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub DeviceCmdAndReplyTemplateBuild()
        Dim fake As New DeviceCmdsFacadeFake

        ' verify that build not used during initial instantiation
        Assert.AreEqual(0, fake.BuildCalls)
        ' build called for 1st time
        fake.CmdSet = TestCmds.GetInstance
        Assert.AreEqual(1, fake.BuildCalls)
        ' call again, verify that build not used a 2nd time
        fake.CmdSet = TestCmds.GetInstance
        Assert.AreEqual(1, fake.BuildCalls)
        ' switch cmd set, resulting in 2nd build call 
        fake.CmdSet = TangentEncodersQueryCmds.GetInstance
        Assert.AreEqual(2, fake.BuildCalls)
        ' call again, verify that build not subsequently used
        fake.CmdSet = TangentEncodersQueryCmds.GetInstance

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeviceCmdDiscriminator()
        Dim fake As New DeviceCmdsFacadeFake

        fake.CmdSet = TestCmds.GetInstance
        Assert.IsInstanceOfType(GetType(DeviceCmdMsgDiscriminator), fake.IDeviceCmdMsgDiscriminator)

        fake.CmdSet = TangentEncodersQueryCmds.GetInstance
        Assert.IsInstanceOfType(GetType(DeviceCmdMsgDiscriminator), fake.IDeviceCmdMsgDiscriminator)

        fake.CmdSet = TangentEncodersWithResetRCmds.GetInstance
        Assert.IsInstanceOfType(GetType(DeviceCmdMsgDiscriminator), fake.IDeviceCmdMsgDiscriminator)

        fake.CmdSet = TangentEncodersWithResetZCmds.GetInstance
        Assert.IsInstanceOfType(GetType(DeviceCmdMsgDiscriminator), fake.IDeviceCmdMsgDiscriminator)

        fake.CmdSet = SiTechCmds.GetInstance
        Assert.IsInstanceOfType(GetType(DeviceCmdMsgDiscriminator), fake.IDeviceCmdMsgDiscriminator)

        fake.CmdSet = JRKerrCmds.GetInstance
        Assert.IsInstanceOfType(GetType(JRKerrDeviceCmdMsgDiscriminator), fake.IDeviceCmdMsgDiscriminator)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestCmdList()
        Dim fake As New DeviceCmdsFacadeFake
        fake.CmdSet = TestCmds.GetInstance

        Dim cmdList As Generic.List(Of ISFT) = TestCmds.ISFT.GetList
        Dim sortedCmdList As Generic.List(Of ISFT) = fake.CmdList

        ' unsorted command two is 2nd in sequence; sorted command two is last in sequence
        Dim cmdISFT As ISFT = TestCmds.Two
        Dim testCmd As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmd(cmdISFT)
        Assert.AreEqual(testCmd, DeviceCmdAndReplyTemplateDefaults.GetDefaultCmd(cmdList.Item(1)))
        Assert.AreEqual(testCmd, DeviceCmdAndReplyTemplateDefaults.GetDefaultCmd(sortedCmdList.Item(3)))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetDeviceCmdAndReplyTemplate()
        Dim fake As New DeviceCmdsFacadeFake
        fake.CmdSet = TestCmds.GetInstance

        Assert.AreEqual(TestCmds.ISFT.Size, fake.DeviceCmdAndReplyTemplateDictionary.Count)

        Dim cmdISFT As ISFT = TestCmds.Two
        Dim testCmd As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmd(cmdISFT)
        Dim cmd As String = fake.DeviceCmdAndReplyTemplateDictionary.Item(cmdISFT).IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(testCmd, cmd)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeviceCmdAndReplyTemplateList()
        Dim fake As New DeviceCmdsFacadeFake
        fake.CmdSet = TestCmds.GetInstance

        Assert.AreEqual(TestCmds.ISFT.Size, fake.DeviceCmdAndReplyTemplateList.Count)

        Dim cmdISFT As ISFT = TestCmds.Two
        Dim testCmd As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmd(cmdISFT)
        ' unsorted command two is 2nd in sequence; sorted command two is last in sequence
        Dim cmd As String = fake.DeviceCmdAndReplyTemplateList.Item(3).IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(testCmd, cmd)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetIDeviceCmd()
        Dim fake As New DeviceCmdsFacadeFake
        fake.CmdSet = TestCmds.GetInstance

        Dim cmdISFT As ISFT = TestCmds.Two
        Dim testCmd As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmd(cmdISFT)
        Dim cmd As String = fake.GetIDeviceCmd(cmdISFT).CmdMsgParms.Cmd
        Assert.AreEqual(testCmd, cmd)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetIDeviceReplyCmd()
        Dim fake As New DeviceCmdsFacadeFake
        fake.CmdSet = TestCmds.GetInstance

        Dim cmdISFT As ISFT = TestCmds.Two
        Dim testReply As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultReply(cmdISFT)
        Dim reply As String = fake.GetIDeviceReplyCmd(cmdISFT).CmdReplyParms.Reply
        Assert.AreEqual(testReply, reply)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetCmd()
        Dim fake As New DeviceCmdsFacadeFake
        fake.CmdSet = TestCmds.GetInstance

        Dim cmdISFT As ISFT = TestCmds.Two
        Dim deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate = fake.DeviceCmdAndReplyTemplateDictionary(cmdISFT)
        Dim resultCmdISFT As ISFT = fake.GetCmd(deviceCmdAndReplyTemplate)
        Assert.AreEqual(cmdISFT.Name, resultCmdISFT.Name)

        Assert.IsTrue(True)
    End Sub

    ' deviceCmdAndReplyTemplate.IDeviceCmd.CmdReplyParms and deviceCmdAndReplyTemplate.IDeviceReplyCmd.CmdReplyParms should be the same
    <Test()> Public Sub TestCmdReplyParmsIsUnique()
        Dim fake As New DeviceCmdsFacadeFake
        fake.CmdSet = TestCmds.GetInstance

        Dim cmdISFT As ISFT = TestCmds.Two
        Assert.AreSame(fake.GetIDeviceCmd(cmdISFT).CmdReplyParms, fake.GetIDeviceReplyCmd(cmdISFT).CmdReplyParms)
        Dim cmdReplyParms1 As CmdReplyParms = fake.GetIDeviceCmd(cmdISFT).CmdReplyParms
        Dim cmdReplyParms2 As CmdReplyParms = fake.GetIDeviceReplyCmd(cmdISFT).CmdReplyParms
        Dim testReply As String = "testReply"
        cmdReplyParms1.Reply = testReply
        Assert.AreEqual(testReply, cmdReplyParms2.Reply)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestUpdateDeviceCmdAndReplyTemplateListJRKerr()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))

        Dim cmdMsgParms As CmdMsgParms = IDevice.DeviceCmdsFacade.GetIDeviceCmd(CType(JRKerrCmds.ReadStatus, ISFT)).CmdMsgParms
        Dim cmdBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd)
        Assert.AreEqual(0, cmdBytes(JRKerrUtil.Indeces.Address))
        Dim modAddr As ValueObject = IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name)
        modAddr.Value = CStr(1)
        cmdBytes = BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd)
        Assert.AreEqual(1, cmdBytes(JRKerrUtil.Indeces.Address))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestUpdateDeviceCmdAndReplyTemplateListSiTech()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))

        Dim cmdMsgParms As CmdMsgParms = IDevice.DeviceCmdsFacade.GetIDeviceCmd(CType(SiTechCmds.SetSecAccel, ISFT)).CmdMsgParms
        Assert.AreEqual("X", cmdMsgParms.Cmd.Substring(0, 1))
        Dim modAddr As ValueObject = IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name)
        modAddr.Value = CStr(3)
        Assert.AreEqual("T", cmdMsgParms.Cmd.Substring(0, 1))

        cmdMsgParms = IDevice.DeviceCmdsFacade.GetIDeviceCmd(CType(SiTechCmds.SetPriAccel, ISFT)).CmdMsgParms
        modAddr.Value = CStr(1)
        Assert.AreEqual("Y", cmdMsgParms.Cmd.Substring(0, 1))
        modAddr.Value = CStr(3)
        Assert.AreEqual("U", cmdMsgParms.Cmd.Substring(0, 1))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Class DeviceCmdsFacadeFake : Inherits DeviceCmdsFacade
        Public BuildCalls As Int32 = 0
        Protected Overrides Sub Build()
            BuildCalls += 1
            MyBase.build()
        End Sub
    End Class

End Class

