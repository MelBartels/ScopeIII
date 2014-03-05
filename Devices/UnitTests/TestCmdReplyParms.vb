Imports NUnit.Framework

<TestFixture()> Public Class TestCmdReplyParms

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestClone()
        Dim testReply As String = "testReply"
        Dim testExpectedByteCount As Int32 = 10
        Dim cmdReplyParms As CmdReplyParms = Devices.CmdReplyParms.GetInstance
        cmdReplyParms.Build(testReply, ReceiveInspectParms.GetInstance(testExpectedByteCount, True, 0, 0))

        Dim cmdReplyParms2 As CmdReplyParms = CType(cmdReplyParms.Clone, Devices.CmdReplyParms)
        Assert.AreEqual(testReply, cmdReplyParms2.Reply)
        Assert.AreEqual(testExpectedByteCount, cmdReplyParms2.ReceiveInspectParms.ExpectedByteCount)

        Dim testReply2 As String = "testReply2"
        cmdReplyParms2.Reply = testReply2
        Assert.AreEqual(testReply2, cmdReplyParms2.Reply)
        Assert.AreNotEqual(cmdReplyParms.Reply, cmdReplyParms2.Reply)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

