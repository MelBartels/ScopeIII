Imports NUnit.Framework

<TestFixture()> Public Class TestDeviceReplyCmd

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestClone()
        Dim testReplyCmd As String = "testReplyCmd"
        Dim testExpectedByteCount As Int32 = 10
        Dim IDeviceReplyCmd As IDeviceReplyCmd = Test1ReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = CmdReplyParms.GetInstance(testReplyCmd, ReceiveInspectParms.GetInstance(testExpectedByteCount, False, 0, 0))

        Dim IDeviceReplyCmd2 As IDeviceReplyCmd = CType(IDeviceReplyCmd.Clone, Devices.IDeviceReplyCmd)

        Assert.AreNotSame(IDeviceReplyCmd, IDeviceReplyCmd2)
        Assert.AreNotSame(IDeviceReplyCmd.CmdReplyParms, IDeviceReplyCmd2.CmdReplyParms)

        Assert.AreEqual(testReplyCmd, IDeviceReplyCmd2.CmdReplyParms.Reply)
        Assert.AreEqual(testExpectedByteCount, IDeviceReplyCmd2.CmdReplyParms.ReceiveInspectParms.ExpectedByteCount)

        Dim testReplyCmd2 As String = "testReplyCmd2"
        Dim testBytes2() As Byte = {4, 5, 6}
        IDeviceReplyCmd2.CmdReplyParms.Reply = testReplyCmd2
        Assert.AreNotEqual(IDeviceReplyCmd.CmdReplyParms.Reply, IDeviceReplyCmd2.CmdReplyParms.Reply)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

