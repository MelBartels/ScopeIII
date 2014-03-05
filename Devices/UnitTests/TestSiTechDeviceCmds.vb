Imports NUnit.Framework

<TestFixture()> Public Class TestSiTechDeviceCmds

    Delegate Sub cmdDelegate(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub ReadController()
        cmdShell(New cmdDelegate(AddressOf readControllerCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub SetPriAccel()
        cmdShell(New cmdDelegate(AddressOf setPriAccelCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub SetSecAccel()
        cmdShell(New cmdDelegate(AddressOf setSecAccelCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub SetPriVel()
        cmdShell(New cmdDelegate(AddressOf setPriVelCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub SetSecVel()
        cmdShell(New cmdDelegate(AddressOf setSecVelCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub SetPriPos()
        cmdShell(New cmdDelegate(AddressOf setPriPosCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub SetSecPos()
        cmdShell(New cmdDelegate(AddressOf setSecPosCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub GetPriAccel()
        cmdShell(New cmdDelegate(AddressOf getPriAccelCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub GetSecAccel()
        cmdShell(New cmdDelegate(AddressOf getSecAccelCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub GetPriVel()
        cmdShell(New cmdDelegate(AddressOf getPriVelCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub GetSecVel()
        cmdShell(New cmdDelegate(AddressOf getSecVelCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub GetPriPos()
        cmdShell(New cmdDelegate(AddressOf getPriPosCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub GetSecPos()
        cmdShell(New cmdDelegate(AddressOf getSecPosCmd))
        Assert.IsTrue(True)
    End Sub

    Private Sub cmdShell(ByVal cmdDelegate As cmdDelegate)
        ' build the device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
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

    Private Sub readControllerCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechReadController.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            ' least to most significant bytes
            Dim xmtBytes() As Byte = {1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 10}
            DeviceCmdFacade.IIO.QueueReceiveBytes(xmtBytes)
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' controller values (see TestSiTechController)
            ' primary axis is built 1st
            Dim encoders As ArrayList = IDevice.GetDevicesByName(SiTechController.MotorEncoderName)
            Assert.AreEqual(1, eMath.RInt(CType(encoders(1), Encoder).Value))
            Assert.AreEqual(2, eMath.RInt(CType(encoders(0), Encoder).Value))
            Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryEncoder(IDevice), Devices.Encoder)
            Assert.AreEqual(3, eMath.RInt(encoder.Value))
            encoder = CType(SiTechUtil.GetPrimaryEncoder(IDevice), Devices.Encoder)
            Assert.AreEqual(4, eMath.RInt(encoder.Value))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub setPriAccelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim valueObject As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
        valueObject.Value = CStr(testAccel)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechSetPriAccel.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' check xmt'd msg
            Dim expectedXmtMsg As String = "YR" & CStr(testAccel) & vbCr
            Assert.AreEqual(expectedXmtMsg, CType(IOXmtObserver, IOXmtObserver).Msg)
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub setSecAccelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
        valueObject.Value = CStr(testAccel)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechSetSecAccel.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' check xmt'd msg
            Dim expectedXmtMsg As String = "XR" & CStr(testAccel) & vbCr
            Assert.AreEqual(expectedXmtMsg, CType(IOXmtObserver, IOXmtObserver).Msg)
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub setPriVelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim testVel As Int32 = 1234
        Dim valueObject As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Velocity.Name)
        valueObject.Value = CStr(testVel)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechSetPriVel.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' check xmt'd msg
            Dim expectedXmtMsg As String = "YS" & CStr(testVel) & vbCr
            Assert.AreEqual(expectedXmtMsg, CType(IOXmtObserver, IOXmtObserver).Msg)
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub setSecVelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim testVel As Int32 = 1234
        Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Velocity.Name)
        valueObject.Value = CStr(testVel)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechSetSecVel.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' check xmt'd msg
            Dim expectedXmtMsg As String = "XS" & CStr(testVel) & vbCr
            Assert.AreEqual(expectedXmtMsg, CType(IOXmtObserver, IOXmtObserver).Msg)
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub setPriPosCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim testPos As Int32 = 1234
        Dim encoder As Encoder = CType(SiTechUtil.GetPrimaryServoMotorEncoder(IDevice), Devices.Encoder)
        encoder.Value = CStr(testPos)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechSetPriPos.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' check xmt'd msg
            Dim expectedXmtMsg As String = "Y" & CStr(testPos) & vbCr
            Assert.AreEqual(expectedXmtMsg, CType(IOXmtObserver, IOXmtObserver).Msg)
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub setSecPosCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim testPos As Int32 = 1234
        Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryServoMotorEncoder(IDevice), Devices.Encoder)
        encoder.Value = CStr(testPos)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechSetSecPos.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' check xmt'd msg
            Dim expectedXmtMsg As String = "X" & CStr(testPos) & vbCr
            Assert.AreEqual(expectedXmtMsg, CType(IOXmtObserver, IOXmtObserver).Msg)
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub getPriAccelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechGetPriAccel.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(CStr(testAccel) & vbCr))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Dim valueObject As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
            Assert.AreEqual(testAccel, eMath.RInt(valueObject.Value))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub getSecAccelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechGetSecAccel.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(CStr(testAccel) & vbCr))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
            Assert.AreEqual(testAccel, eMath.RInt(valueObject.Value))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub getPriVelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim testVel As Int32 = 1234
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechGetPriVel.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(CStr(testVel) & vbCr))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Dim valueObject As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Velocity.Name)
            Assert.AreEqual(testVel, eMath.RInt(valueObject.Value))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub getSecVelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim testVel As Int32 = 1234
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechGetSecVel.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(CStr(testVel) & vbCr))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Velocity.Name)
            Assert.AreEqual(testVel, eMath.RInt(valueObject.Value))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub getPriPosCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim testPos As Int32 = 1234
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechGetPriPos.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(CStr(testPos) & vbCr))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Dim encoder As Encoder = CType(SiTechUtil.GetPrimaryServoMotorEncoder(IDevice), Devices.Encoder)
            Assert.AreEqual(testPos, eMath.RInt(encoder.Value))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub getSecPosCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim testPos As Int32 = 1234
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.SiTechGetSecPos.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(CStr(testPos) & vbCr))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryServoMotorEncoder(IDevice), Devices.Encoder)
            Assert.AreEqual(testPos, eMath.RInt(encoder.Value))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    <Test()> Public Sub TestInstantiations()
        Dim SiTechCmds As SiTechCmds = SiTechCmds.GetInstance
        Assert.IsNotNull(SiTechCmds)
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

