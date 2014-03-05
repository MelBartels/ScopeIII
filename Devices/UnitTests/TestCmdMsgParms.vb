Imports NUnit.Framework

<TestFixture()> Public Class TestCmdMsgParms

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestClone()
        Dim testCmd As String = "testCmd"
        Dim testExpectedByteCount As Int32 = 10
        Dim cmdMsgParms As CmdMsgParms = Devices.CmdMsgParms.GetInstance
        cmdMsgParms.Build(testCmd, testExpectedByteCount, True, 0)

        Dim cmdMsgParms2 As CmdMsgParms = CType(cmdMsgParms.Clone, Devices.CmdMsgParms)
        Assert.AreEqual(testCmd, cmdMsgParms2.Cmd)
        Assert.AreEqual(testExpectedByteCount, cmdMsgParms2.ExpectedByteCount)

        Dim testCmd2 As String = "testCmd2"
        cmdMsgParms2.Cmd = testCmd2
        Assert.AreEqual(testCmd2, cmdMsgParms2.Cmd)
        Assert.AreNotEqual(cmdMsgParms.Cmd, cmdMsgParms2.Cmd)

        ' empty string
        Dim testCmd3 As String = Nothing
        cmdMsgParms = Devices.CmdMsgParms.GetInstance
        cmdMsgParms.Build(testCmd3, testExpectedByteCount, True, 0)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

