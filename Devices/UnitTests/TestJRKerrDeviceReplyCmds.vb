Imports NUnit.Framework

<TestFixture()> Public Class TestJRKerrDeviceReplyCmds

    Delegate Sub cmdDelegate(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub NmcHardReset()
        cmdShell(New cmdDelegate(AddressOf NmcHardReset))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub SetAddress()
        cmdShell(New cmdDelegate(AddressOf SetAddress))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub ReadStatus()
        cmdShell(New cmdDelegate(AddressOf ReadStatus))
        Assert.IsTrue(True)
    End Sub

    Private Sub cmdShell(ByVal cmdDelegate As cmdDelegate)
        ' build the device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))
        ' add IO status observer
        Dim IOStatusObserver As IObserver = New TestIOStatusObserver
        IDevice.GetDeviceTemplate.StatusObserver.Attach(IOStatusObserver)

        ' create the facade, which will fire off various commands (here, a single command for testing purposes)
        Dim DeviceReceiveCmdFacade As DeviceReceiveCmdFacade = ScopeIII.Devices.DeviceReceiveCmdFacade.GetInstance
        DeviceReceiveCmdFacade.IDevice = IDevice
        If DeviceReceiveCmdFacade.BuildIIO(IDevice) Then
            Try
                DeviceReceiveCmdFacade.StartIOListening()
                ' test that IOBuild succeeded
                Assert.IsTrue(CType(IOStatusObserver, TestIOStatusObserver).msg.StartsWith(IOStatus.Build.Description))
                CType(IOStatusObserver, TestIOStatusObserver).msg = Nothing

                If DeviceReceiveCmdFacade.IIOOpen Then
                    ' test that IOOpen worked
                    Assert.IsTrue(IOStatus.Open.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
                    ' add the xmt observer
                    Dim IOXmtObserver As IObserver = New IOXmtObserver
                    DeviceReceiveCmdFacade.IIO.TransmitObservers.Attach(IOXmtObserver)
                    ' call the command
                    cmdDelegate.Invoke(IDevice, IOStatusObserver, IOXmtObserver, DeviceReceiveCmdFacade)
                Else
                    Assert.Fail(IOStatus.OpenFailed.Description)
                End If

                DeviceReceiveCmdFacade.ShutdownIO()
                ' test that IO was closed
                Assert.IsTrue(IOStatus.Closed.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Catch ex As Exception
                Debug.WriteLine(ExceptionService.BuildExceptionMsg(ex, Nothing))
                Throw ex
            Finally
                DeviceReceiveCmdFacade.ShutdownIO()
            End Try
        Else
            Assert.Fail(IOStatus.BuildFailed.Description)
        End If

        Assert.IsTrue(True)
    End Sub

    Private Sub nmcHardReset(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.JRKerrNmcHardReset.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
    End Sub

    Private Sub setAddress(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim cmd As String = IDevice.DeviceCmdsFacade.GetIDeviceCmd(CType(JRKerrCmds.SetAddress, ISFT)).CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Dim expectedMsg As String = DeviceCmdAndReplyTemplateDefaults.JRKerrSetAddress.IDeviceReplyCmd.CmdReplyParms.Reply
        Assert.AreEqual(expectedMsg, xmtMsg)
    End Sub

    Private Sub readStatus(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        ' set controller state
        Dim testAddress As Int32 = 1
        Dim testPosition As Int32 = 1000
        IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name).Value = CStr(testAddress)
        Dim jrk As JRKerrServoController = CType(IDevice, JRKerrServoController)
        With JRKerrUtil.GetJRKerrServoStatus(IDevice)
            .StatusItems = SEND_POS
            .Position = testPosition
            Dim encoder As Encoder = CType(jrk.GetDevicesByName(ServoMotor.MotorEncoderName).Item(0), Encoder)
            encoder.Value = CStr(testPosition)
            ' create cmdBytes
            Dim cmd As String = jrk.DeviceCmdsFacade.GetIDeviceCmd(CType(JRKerrCmds.ReadStatus, ISFT)).CmdMsgParms.Cmd
            Dim cmdBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(cmd)
            ' or 170,1,19,1,21
            Dim expectedBytes() As Byte = {JRKerrUtil.HEADER_BYTE, CByte(testAddress), &H10 Or READ_STAT, SEND_POS, 21}
            For ix As Int32 = 0 To expectedBytes.Length - 1
                Assert.AreEqual(expectedBytes(ix), cmdBytes(ix))
            Next
            ' queue the cmdBytes
            DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(cmdBytes)
            ' test results
            Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
            Dim xmtBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(xmtMsg)
            ' test xmtBytes
            Dim expectedXmtBytes() As Byte = {1, 232, 3, 0, 0, 236}
            For ix As Int32 = 0 To expectedXmtBytes.Length - 1
                Assert.AreEqual(expectedXmtBytes(ix), xmtBytes(ix))
            Next
            ' finally test DecodeStatus... methods
            ' first, reset
            .Position = 0
            encoder = CType(jrk.GetDevicesByName(ServoMotor.MotorEncoderName).Item(0), Encoder)
            encoder.Value = CStr(0)
            ' decode
            JRKerrUtil.DecodeStatusBytes(IDevice, xmtBytes)
            ' test decoding
            Assert.AreEqual(testPosition, .Position)
            encoder = CType(jrk.GetDevicesByName(ServoMotor.MotorEncoderName).Item(0), Encoder)
            Assert.AreEqual(CStr(testPosition), encoder.Value)
        End With
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class TestIOStatusObserver : Implements IObserver
        Public msg As String
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Me.msg = CStr([object])
        End Function
    End Class

    Private Class IOXmtObserver : Implements IObserver
        Public Msg As String
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements BartelsLibrary.IObserver.ProcessMsg
            Msg = CStr([object])
        End Function
    End Class
End Class

