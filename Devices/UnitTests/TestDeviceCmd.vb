Imports NUnit.Framework

<TestFixture()> Public Class TestDeviceCmd

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestClone()
        Dim testCmd As String = "testCmd"
        Dim testExpectedByteCount As Int32 = 10
        Dim IDeviceCmd As IDeviceCmd = Test1Cmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance(testCmd, testExpectedByteCount, False, 0)

        Dim IDeviceCmd2 As IDeviceCmd = CType(IDeviceCmd.Clone, Devices.IDeviceCmd)

        Assert.AreNotSame(IDeviceCmd, IDeviceCmd2)
        Assert.AreNotSame(IDeviceCmd.CmdMsgParms, IDeviceCmd2.CmdMsgParms)

        Assert.AreEqual(testCmd, IDeviceCmd2.CmdMsgParms.Cmd)
        Assert.AreEqual(testExpectedByteCount, IDeviceCmd2.CmdMsgParms.ExpectedByteCount)

        Dim testCmd2 As String = "testCmd2"
        IDeviceCmd2.CmdMsgParms.Cmd = testCmd2
        Assert.AreNotEqual(IDeviceCmd.CmdMsgParms.Cmd, IDeviceCmd2.CmdMsgParms.Cmd)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

