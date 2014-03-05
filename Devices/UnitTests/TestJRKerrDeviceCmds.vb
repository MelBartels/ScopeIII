Imports NUnit.Framework

<TestFixture()> Public Class TestJRKerrDeviceCmds

    Delegate Sub cmdDelegate(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)

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
        cmdShell(New cmdDelegate(AddressOf readStatus))
        Assert.IsTrue(True)
    End Sub

    Private Sub cmdShell(ByVal cmdDelegate As cmdDelegate)
        ' build the device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))
        ' add IO status observer
        Dim IOStatusObserver As IObserver = New TestIOStatusObserver
        IDevice.GetDeviceTemplate.StatusObserver.Attach(IOStatusObserver)
        ' create the facade, which will fire off various commands (here, a single command for testing purposes)
        Dim DeviceCmdFacade As DeviceCmdFacade = ScopeIII.Devices.DeviceCmdFacade.GetInstance
        DeviceCmdFacade.IDevice = IDevice
        If DeviceCmdFacade.BuildIIO(IDevice) Then
            Try
                DeviceCmdFacade.StartIOListening()
                ' test that IOBuild succeeded
                Assert.IsTrue(CType(IOStatusObserver, TestIOStatusObserver).msg.StartsWith(IOStatus.Build.Description))
                CType(IOStatusObserver, TestIOStatusObserver).msg = Nothing
                ' open IIO
                If DeviceCmdFacade.IIOOpen Then
                    ' test that IOOpen worked
                    Assert.IsTrue(IOStatus.Open.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
                    ' add the xmt observer
                    Dim IOXmtObserver As IObserver = New IOXmtObserver
                    DeviceCmdFacade.IIO.TransmitObservers.Attach(IOXmtObserver)
                    ' invoke the particular command
                    cmdDelegate.Invoke(IDevice, IOStatusObserver, IOXmtObserver, DeviceCmdFacade)
                Else
                    Assert.Fail(IOStatus.OpenFailed.Description)
                End If
                ' close IIO
                DeviceCmdFacade.ShutdownIO()
                ' test that IO was closed
                Assert.IsTrue(IOStatus.Closed.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Catch ex As Exception
                Debug.WriteLine(ExceptionService.BuildExceptionMsg(ex, Nothing))
                Throw ex
            Finally
                DeviceCmdFacade.ShutdownIO()
            End Try
        Else
            Assert.Fail(IOStatus.BuildFailed.Description)
        End If
    End Sub

    Private Sub nmcHardReset(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim IDeviceCmd As IDeviceCmd = IDevice.DeviceCmdsFacade.GetIDeviceCmd(CType(JRKerrCmds.NmcHardReset, ISFT))
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' check xmt'd msg
            Dim xmtBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(CType(IOXmtObserver, IOXmtObserver).Msg)
            Assert.AreEqual(&HAA, xmtBytes(20))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub setAddress(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name).Value = CStr(1)
        Dim IDeviceCmd As IDeviceCmd = IDevice.DeviceCmdsFacade.GetIDeviceCmd(CType(JRKerrCmds.SetAddress, ISFT))
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(IDeviceCmd.CmdReplyParms.Reply))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            ' response means that two bytes were returned, status and checksum, and that the checksum was valid
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub readStatus(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name).Value = CStr(1)
        JRKerrUtil.GetJRKerrServoStatus(IDevice).StatusItems = SEND_POS
        Dim jrk As JRKerrServoController = CType(IDevice, JRKerrServoController)
        Dim IDeviceCmd As IDeviceCmd = IDevice.DeviceCmdsFacade.GetIDeviceCmd(CType(JRKerrCmds.ReadStatus, ISFT))
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            Dim testPosition As Int32 = 1000
            ' 232,0,0,0 is 1000 decoded into bytes, lowest to highest significant 
            Dim statusBytes() As Byte = {MOVE_DONE, 232, 3, 0, 0, 236}
            DeviceCmdFacade.IIO.QueueReceiveBytes(statusBytes)
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' test that position and encoder were updated
            Dim encoder As Encoder = CType(jrk.GetDevicesByName(ServoMotor.MotorEncoderName).Item(0), Encoder)
            Assert.AreEqual(CStr(testPosition), encoder.Value)
            Assert.AreEqual(testPosition, JRKerrUtil.GetJRKerrServoStatus(IDevice).Position)
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    <Test()> Public Sub TestInstantiations()
        Dim JRKerrCmds As JRKerrCmds = JRKerrCmds.GetInstance
        Assert.IsNotNull(JRKerrCmds)
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

