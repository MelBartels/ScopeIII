Imports NUnit.Framework

<TestFixture()> Public Class TestSiTechDeviceReplyCmds

    Delegate Sub cmdDelegate(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)

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

    Private Sub readControllerCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim encoders As ArrayList = IDevice.GetDevicesByName(SiTechController.MotorEncoderName)
        CType(encoders(1), Encoder).Value = CStr(1)
        CType(encoders(0), Encoder).Value = CStr(2)

        Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryEncoder(IDevice), Devices.Encoder)
        encoder.Value = CStr(3)

        encoder = CType(SiTechUtil.GetPrimaryEncoder(IDevice), Devices.Encoder)
        encoder.Value = CStr(4)

        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechReadController.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd))
        ' least to most significant bytes
        Dim expectedBytes() As Byte = {1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 10}
        Dim expectedString As String = BartelsLibrary.Encoder.BytesToString(expectedBytes)
        Dim recMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Assert.AreEqual(expectedString, recMsg)
    End Sub

    Private Sub setPriAccelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechSetPriAccel.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & testAccel & vbCr))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Dim valueObject As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
        Assert.AreEqual(testAccel, eMath.RInt(valueObject.Value))

        Assert.IsTrue(True)
    End Sub

    Private Sub setSecAccelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechSetSecAccel.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & testAccel & vbCr))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
        Assert.AreEqual(testAccel, eMath.RInt(valueObject.Value))

        Assert.IsTrue(True)
    End Sub

    Private Sub setPriVelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim testVel As Int32 = 1234
        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechSetPriVel.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & testVel & vbCr))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Dim valueObject As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Velocity.Name)
        Assert.AreEqual(testVel, eMath.RInt(valueObject.Value))

        Assert.IsTrue(True)
    End Sub

    Private Sub setSecVelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim testVel As Int32 = 1234
        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechSetSecVel.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & testVel & vbCr))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Velocity.Name)
        Assert.AreEqual(testVel, eMath.RInt(valueObject.Value))

        Assert.IsTrue(True)
    End Sub

    Private Sub setPriPosCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim testPos As Int32 = 1234
        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechSetPriPos.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & testPos & vbCr))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Dim encoder As Encoder = CType(SiTechUtil.GetPrimaryServoMotorEncoder(IDevice), Devices.Encoder)
        Assert.AreEqual(testPos, eMath.RInt(encoder.Value))

        Assert.IsTrue(True)
    End Sub

    Private Sub setSecPosCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim testPos As Int32 = 1234
        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechSetSecPos.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & testPos & vbCr))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryServoMotorEncoder(IDevice), Devices.Encoder)
        Assert.AreEqual(testPos, eMath.RInt(encoder.Value))

        Assert.IsTrue(True)
    End Sub

    Private Sub getPriAccelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim valueObject As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
        valueObject.Value = CStr(testAccel)

        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechGetPriAccel.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & vbCr))
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Dim expectedMsg As String = CStr(testAccel) & vbCr
        Assert.AreEqual(expectedMsg, xmtMsg)
    End Sub

    Private Sub getSecAccelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
        valueObject.Value = CStr(testAccel)

        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechGetSecAccel.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & vbCr))
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Dim expectedMsg As String = CStr(testAccel) & vbCr
        Assert.AreEqual(expectedMsg, xmtMsg)
    End Sub

    Private Sub getPriVelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim testVel As Int32 = 1234
        Dim valueObject As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Velocity.Name)
        valueObject.Value = CStr(testVel)

        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechGetPriVel.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & vbCr))
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Dim expectedMsg As String = CStr(testVel) & vbCr
        Assert.AreEqual(expectedMsg, xmtMsg)
    End Sub

    Private Sub getSecVelCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim testVel As Int32 = 1234
        Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Velocity.Name)
        valueObject.Value = CStr(testVel)

        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechGetSecVel.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & vbCr))
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Dim expectedMsg As String = CStr(testVel) & vbCr
        Assert.AreEqual(expectedMsg, xmtMsg)
    End Sub

    Private Sub getPriPosCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim testPos As Int32 = 1234
        Dim encoder As Encoder = CType(SiTechUtil.GetPrimaryServoMotorEncoder(IDevice), Devices.Encoder)
        encoder.Value = CStr(testPos)

        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechGetPriPos.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & vbCr))
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Dim expectedMsg As String = CStr(testPos) & vbCr
        Assert.AreEqual(expectedMsg, xmtMsg)
    End Sub

    Private Sub getSecPosCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim testPos As Int32 = 1234
        Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryServoMotorEncoder(IDevice), Devices.Encoder)
        encoder.Value = CStr(testPos)

        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechGetSecPos.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & vbCr))
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Dim expectedMsg As String = CStr(testPos) & vbCr
        Assert.AreEqual(expectedMsg, xmtMsg)
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

