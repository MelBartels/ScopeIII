Imports NUnit.Framework

<TestFixture()> Public Class TestDeviceReceiveCmd

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    ' queue (push onto the stack) a couple of commands, then dequeue (pop the stack) the CmdQueue, 
    ' verifying that the commands were placed in the CmdQueue
    <Test()> Public Sub TestCmdQueue()
        Dim drcf As New DeviceReceiveCmdFacadeFake
        drcf.IDevice = ScopeIII.Devices.DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        Dim testCmd As String = "Q"
        Dim testCmd2 As String = "R"
        drcf.ProcessMsg(CObj(testCmd))
        drcf.ProcessMsg(CObj(testCmd2))
        Assert.IsTrue(testCmd.Equals(drcf.CmdQueue.Dequeue))
        Assert.IsTrue(testCmd2.Equals(drcf.CmdQueue.Dequeue))
    End Sub

    ' note: vbCr is 1 char, vbCrLf is 2 chars
    <Test()> Public Sub TestInspectCmdQueue()
        Dim drcf As New DeviceReceiveCmdFacadeFake
        drcf.DeviceCmdsFacade = DeviceCmdsFacade.GetInstance
        drcf.DeviceCmdsFacade.CmdSet = TangentEncodersWithResetRCmds.GetInstance
        Dim cq As New Queue

        ' test for nonsense chars
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("One", cq)
        Dim cmds As Generic.List(Of ReceivedCmd) = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(0, cmds.Count)

        ' test Tangent query command using command built in code as the received chars
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        Dim cmdMsgParms As CmdMsgParms = drcf.DeviceCmdsFacade.GetIDeviceCmd(CType(TangentEncodersWithResetRCmds.Query, ISFT)).CmdMsgParms
        enQueue(cmdMsgParms.Cmd, cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        Dim rCmd As ReceivedCmd = cmds(0)
        Assert.AreEqual(0, rCmd.CmdBeginQueueArrayIx)
        Assert.AreEqual(0, rCmd.CmdEndQueueArrayIx)
        Assert.AreEqual(1, rCmd.CmdMsgBeginQueueArrayIx)
        Assert.AreEqual(0, rCmd.CmdMsgEndQueueArrayIx)
        Assert.IsFalse(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)

        ' test Tangent query command using hardcoded received chars
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("Q", cq)
        cmds = drcf.InspectCmdQueue(cq)
        rCmd = cmds(0)
        Assert.AreEqual(1, cmds.Count)
        Assert.IsFalse(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)
        Assert.AreEqual(0, rCmd.CmdByteCountCompare)

        ' test two Tangent query commands in a row
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("QQ", cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(2, cmds.Count)
        rCmd = cmds(0)
        Assert.AreEqual(0, rCmd.CmdBeginQueueArrayIx)
        Assert.AreEqual(0, rCmd.CmdEndQueueArrayIx)
        Assert.AreEqual(1, rCmd.CmdMsgBeginQueueArrayIx)
        Assert.AreEqual(0, rCmd.CmdMsgEndQueueArrayIx)
        Assert.IsFalse(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)
        rCmd = cmds(1)
        Assert.AreEqual(1, rCmd.CmdBeginQueueArrayIx)
        Assert.AreEqual(1, rCmd.CmdEndQueueArrayIx)
        Assert.AreEqual(2, rCmd.CmdMsgBeginQueueArrayIx)
        Assert.AreEqual(1, rCmd.CmdMsgEndQueueArrayIx)
        Assert.IsFalse(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)

        ' test two valid Tangent commands interspersed amongst nonsense chars
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("xQyRz1", cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(2, cmds.Count)
        rCmd = cmds(0)
        Assert.AreEqual(1, rCmd.CmdBeginQueueArrayIx)
        Assert.AreEqual(1, rCmd.CmdEndQueueArrayIx)
        Assert.AreEqual(2, rCmd.CmdMsgBeginQueueArrayIx)
        Assert.AreEqual(2, rCmd.CmdMsgEndQueueArrayIx)
        Assert.IsTrue(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)
        Dim rCmd2 As ReceivedCmd = cmds(1)
        Assert.AreEqual(3, rCmd2.CmdBeginQueueArrayIx)
        Assert.AreEqual(3, rCmd2.CmdEndQueueArrayIx)
        Assert.AreEqual(4, rCmd2.CmdMsgBeginQueueArrayIx)
        Assert.AreEqual(5, rCmd2.CmdMsgEndQueueArrayIx)
        Assert.IsTrue(rCmd2.CmdMsgExists)
        Assert.IsFalse(rCmd2.ValidCmdMsg)

        ' test Tangent reset command
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("R12345" & vbTab & "12345" & vbCr, cq)
        cmds = drcf.InspectCmdQueue(cq)
        rCmd = cmds(0)
        Assert.AreEqual(1, cmds.Count)
        Assert.IsTrue(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)
        Assert.AreEqual(0, rCmd.CmdByteCountCompare)

        ' test SiTech get status command
        drcf.DeviceCmdsFacade.CmdSet = SiTechCmds.GetInstance
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("XXS" & vbCr, cq)
        cmds = drcf.InspectCmdQueue(cq)
        rCmd = cmds(0)
        Assert.AreEqual(1, cmds.Count)
        Assert.IsFalse(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)
        Assert.AreEqual(0, rCmd.CmdByteCountCompare)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestIncompleteCmdViaTangentCmds()
        Dim drcf As New DeviceReceiveCmdFacadeFake
        drcf.DeviceCmdsFacade = DeviceCmdsFacade.GetInstance
        drcf.DeviceCmdsFacade.CmdSet = TangentEncodersWithResetRCmds.GetInstance
        Dim cq As New Queue

        ' use incomplete Tangent reset command
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("R123", cq)
        Dim cmds As Generic.List(Of ReceivedCmd) = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        Dim rCmd As ReceivedCmd = cmds(0)
        Assert.AreEqual(0, rCmd.CmdBeginQueueArrayIx)
        Assert.AreEqual(0, rCmd.CmdEndQueueArrayIx)
        Assert.AreEqual(1, rCmd.CmdMsgBeginQueueArrayIx)
        Assert.AreEqual(3, rCmd.CmdMsgEndQueueArrayIx)
        Assert.IsTrue(rCmd.CmdMsgExists)
        Assert.IsFalse(rCmd.ValidCmdMsg)

        ' finish command started above
        ' processing received cmds will otherwise clear the array of received commands
        drcf.ProcessReceivedCmds.Clear()
        enQueue("45" & vbTab & "12345" & vbCr, cq)
        cmds = drcf.InspectCmdQueue(cq)
        rCmd = cmds(0)
        Assert.AreEqual(1, cmds.Count)
        Assert.IsTrue(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)
        Assert.AreEqual(0, rCmd.CmdByteCountCompare)

        ' try incomplete command where command (ignoring cmd msg) > 1 char
        drcf.DeviceCmdsFacade.CmdSet = TestCmds.GetInstance
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("XY", cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(0, cmds.Count)

        ' finish the command started above
        enQueue("Z" & vbCr, cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        Assert.IsTrue(rCmd.CmdMsgExists)
        Assert.IsTrue(rCmd.ValidCmdMsg)
        Assert.AreEqual(0, rCmd.CmdByteCountCompare)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestSortCmds()
        Dim drcf As New DeviceReceiveCmdFacadeFake
        drcf.DeviceCmdsFacade = DeviceCmdsFacade.GetInstance
        drcf.DeviceCmdsFacade.CmdSet = TestCmds.GetInstance
        Dim cq As New Queue
        Dim cmds As Generic.List(Of ReceivedCmd)

        ' try cmd 'AA' where there's also a cmd 'A': should return cmd 'AA' and not return two 'A's
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("AA", cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        Dim defaultCmd As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmdMsgParms(CType(TestCmds.Three, ISFT)).Cmd
        Dim testCmd As String = cmds(0).DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(defaultCmd, testCmd)

        ' now try cmd 'A'
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        enQueue("A", cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        defaultCmd = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmdMsgParms(CType(TestCmds.Two, ISFT)).Cmd
        testCmd = cmds(0).DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(defaultCmd, testCmd)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestIdenticalCmdsThatVaryByParm()
        Dim drcf As New DeviceReceiveCmdFacadeFake
        drcf.DeviceCmdsFacade = DeviceCmdsFacade.GetInstance
        drcf.DeviceCmdsFacade.CmdSet = SiTechCmds.GetInstance
        Dim cq As New Queue
        Dim cmds As Generic.List(Of ReceivedCmd)

        ' try distinguishing between SiTech get/set commands that vary only that the set sends a parm and the get returns a parm
        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        Dim cmdMsgParms As CmdMsgParms = drcf.DeviceCmdsFacade.GetIDeviceCmd(CType(SiTechCmds.GetPriAccel, ISFT)).CmdMsgParms
        enQueue(cmdMsgParms.Cmd & vbCr, cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        Dim defaultCmd As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmdMsgParms(CType(SiTechCmds.GetPriAccel, ISFT)).Cmd
        Dim testCmd As String = cmds(0).DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(defaultCmd, testCmd)

        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        Dim testAccel As Int32 = 1234
        cmdMsgParms = drcf.DeviceCmdsFacade.GetIDeviceCmd(CType(SiTechCmds.SetPriAccel, ISFT)).CmdMsgParms
        enQueue(cmdMsgParms.Cmd & CStr(testAccel) & vbCr, cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        defaultCmd = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmdMsgParms(CType(SiTechCmds.SetPriAccel, ISFT)).Cmd
        testCmd = cmds(0).DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(defaultCmd, testCmd)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestCmdOfBytes()
        Dim drcf As New DeviceReceiveCmdFacadeFake
        drcf.DeviceCmdsFacade = DeviceCmdsFacade.GetInstance
        drcf.DeviceCmdsFacade.CmdSet = TestCmds.GetInstance
        Dim cq As New Queue
        Dim cmds As Generic.List(Of ReceivedCmd)

        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        Dim cmdMsgParms As CmdMsgParms = drcf.DeviceCmdsFacade.GetIDeviceCmd(CType(TestCmds.Four, ISFT)).CmdMsgParms
        enQueue(cmdMsgParms.Cmd & vbCr, cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        Dim defaultCmd As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmdMsgParms(CType(TestCmds.Four, ISFT)).Cmd
        Dim testCmd As String = cmds(0).DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(defaultCmd, testCmd)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestJRKerrCmds()
        Dim drcf As New DeviceReceiveCmdFacadeFake
        drcf.DeviceCmdsFacade = DeviceCmdsFacade.GetInstance
        drcf.DeviceCmdsFacade.CmdSet = JRKerrCmds.GetInstance
        Dim cq As New Queue
        Dim cmds As Generic.List(Of ReceivedCmd)

        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        Dim cmdMsgParms As CmdMsgParms = drcf.DeviceCmdsFacade.GetIDeviceCmd(CType(JRKerrCmds.NmcHardReset, ISFT)).CmdMsgParms
        ' get cmd bytes to alter
        Dim cmdBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd)
        ' in this test, don't alter any of the cmd bytes
        enQueue(BartelsLibrary.Encoder.BytesToString(cmdBytes), cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        Dim defaultCmd As String = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmdMsgParms(CType(JRKerrCmds.NmcHardReset, ISFT)).Cmd
        Dim testCmd As String = cmds(0).DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(defaultCmd, testCmd)

        drcf.ProcessReceivedCmds.Clear()
        cq.Clear()
        cmdMsgParms = drcf.DeviceCmdsFacade.GetIDeviceCmd(CType(JRKerrCmds.SetAddress, ISFT)).CmdMsgParms
        ' get cmd bytes to alter
        cmdBytes = BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd)
        ' alter the # of data bits from 2 to 4
        cmdBytes(JRKerrUtil.Indeces.Cmd) = CByte((cmdBytes(JRKerrUtil.Indeces.Cmd) And &HF) Or &H40)
        ' alter the 'set address to' value
        cmdBytes(3) = 1
        ' set checksum
        JRKerrUtil.SetCmdChecksum(cmdBytes)
        ' queue the altered cmd bytes
        enQueue(BartelsLibrary.Encoder.BytesToString(cmdBytes), cq)
        cmds = drcf.InspectCmdQueue(cq)
        Assert.AreEqual(1, cmds.Count)
        defaultCmd = DeviceCmdAndReplyTemplateDefaults.GetDefaultCmdMsgParms(CType(JRKerrCmds.SetAddress, ISFT)).Cmd
        testCmd = cmds(0).DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd
        Assert.AreEqual(defaultCmd, testCmd)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestBuildEncoderValueString()
        Dim tqrc As New TangentQueryReceiveCmdFake
        Assert.AreEqual("+00020", tqrc.BuildEncoderValueString("20"))
        Assert.AreEqual("+20000", tqrc.BuildEncoderValueString("20000"))
        Assert.AreEqual("-00020", tqrc.BuildEncoderValueString("-20"))
        Assert.AreEqual("-20000", tqrc.BuildEncoderValueString("-20000"))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeviceFactoryBuildsDeviceCmdsFacade()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        Assert.GreaterOrEqual(IDevice.DeviceCmdsFacade.CmdList.Count, 1)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestViaTangentReceiveCmds()
        ' build the device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        ' add IO status observer
        Dim IOStatusObserver As IObserver = TestIObserver.GetInstance
        IDevice.GetDeviceTemplate.StatusObserver.Attach(IOStatusObserver)

        ' create the facade, which will receive commands (here, a single command for testing purposes)
        Dim DeviceReceiveCmdFacade As DeviceReceiveCmdFacade = ScopeIII.Devices.DeviceReceiveCmdFacade.GetInstance
        If DeviceReceiveCmdFacade.BuildIIO(IDevice) Then
            ' test that IOBuild succeeded
            Assert.IsTrue(CType(IOStatusObserver, TestIObserver).Msg.StartsWith(IOStatus.Build.Description))
            CType(IOStatusObserver, TestIObserver).Msg = Nothing

            ' start listening (make this call after BuildIIO)
            DeviceReceiveCmdFacade.StartIOListening()

            ' add our IOXmtObserver to test transmitted returned data from the device
            Dim IOXmtObserver As TestIObserver = TestIObserver.GetInstance
            DeviceReceiveCmdFacade.IIO.TransmitObservers.Attach(CType(IOXmtObserver, IObserver))

            If DeviceReceiveCmdFacade.IIOOpen Then
                ' test that IOOpen worked
                Assert.IsTrue(IOStatus.Open.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))

                ' valid command
                Dim cmdMsgParms As CmdMsgParms = IDevice.DeviceCmdsFacade.GetIDeviceCmd(CType(TangentEncodersWithResetRCmds.Query, ISFT)).CmdMsgParms
                DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd))
                Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
                Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))
                ' and test for transmitted results
                Assert.IsTrue(IOXmtObserver.Msg.Length > 0)
                Assert.IsTrue(IOXmtObserver.Msg.Equals("+00000" & vbTab & "+00000" & vbCr))

                ' non-zero encoder values test
                Dim testValuePri As String = "+01000"
                Dim testValueSec As String = "+02000"
                CType(IDevice, EncodersBox).SetValues(testValuePri, testValueSec)
                DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd))
                Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
                Assert.IsTrue(IOXmtObserver.Msg.Equals(testValuePri & vbTab & testValueSec & vbCr))

                ' all bad commands
                DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes("melbiewon"))
                Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
                Assert.IsTrue(IOStatus.CmdFailed.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))

                ' mixed valid and bad commands, ending with valid command
                DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes("xxQyQ"))
                Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
                Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))

                ' mixed valid and bad commands, ending with valid command with extraneous chars
                DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes("xxQyQzz"))
                Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
                Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))

                ' incomplete command
                DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes("R+20000"))
                Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
                Assert.IsTrue(IOStatus.CmdIncomplete.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))

                ' finish the incomplete command
                DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(vbTab & "+20000" & vbCr))
                Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
                Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))

                Assert.IsTrue(True)
            Else
                Assert.Fail(IOStatus.OpenFailed.Description)
            End If

            DeviceReceiveCmdFacade.ShutdownIO()
            ' test that IO was closed
            Assert.IsTrue(IOStatus.Closed.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))
            DeviceReceiveCmdFacade.IIO.TransmitObservers.Detach(CType(IOXmtObserver, IObserver))
        Else
            Assert.Fail(IOStatus.BuildFailed.Description)
        End If

        IDevice.GetDeviceTemplate.StatusObserver.Detach(IOStatusObserver)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Sub enQueue(ByVal [string] As String, ByRef queue As Queue)
        For ix As Int32 = 0 To [string].Length - 1
            queue.Enqueue([string].Substring(ix, 1))
        Next
    End Sub

    Private Class DeviceReceiveCmdFacadeFake : Inherits DeviceReceiveCmdFacade
        Public DeviceCmdsFacade As DeviceCmdsFacade
        Public Overrides Function ProcessMsg(ByRef [object] As Object) As Boolean
            addReceivedObjectToCmdQueue([object], CmdQueue)
        End Function
        Protected Overrides Function getDeviceCmdsFacade() As DeviceCmdsFacade
            Return DeviceCmdsFacade
        End Function
        Public Overloads Function InspectCmdQueue(ByRef cmdQueue As Queue) As Generic.List(Of ReceivedCmd)
            Me.CmdQueue = cmdQueue
            InspectCmdQueue(getDeviceCmdsFacade.DeviceCmdAndReplyTemplateList, getDeviceCmdsFacade.IDeviceCmdMsgDiscriminator, Me.CmdQueue)
            Return Me.ProcessReceivedCmds
        End Function
    End Class

    Private Class TangentQueryReceiveCmdFake : Inherits TangentEncodersQueryReplyCmd
        Public Overloads Function BuildEncoderValueString(ByVal testValue As String) As String
            Return MyBase.buildEncoderValueString(testValue)
        End Function
    End Class
End Class

