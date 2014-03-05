Imports NUnit.Framework

<TestFixture()> Public Class TestTwoDevicesListening

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TrySiTechControllers()
        ' build the 1st device
        Dim IDevice1 As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        ' add IO status observer
        Dim IOStatusObserver As IObserver = New TestIOStatusObserver
        IDevice1.GetDeviceTemplate.StatusObserver.Attach(IOStatusObserver)
        ' create the facade, which will fire off various commands (here, a single command for testing purposes)
        Dim DeviceReceiveCmdFacade1 As DeviceReceiveCmdFacade = ScopeIII.Devices.DeviceReceiveCmdFacade.GetInstance
        DeviceReceiveCmdFacade1.IDevice = IDevice1

        ' build the 2nd device
        Dim IDevice2 As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.SiTechController, ISFT))
        ' set it to secondary controller
        IDevice2.GetValueObject(ValueObjectNames.ModuleAddress.Name).Value = CStr(3)
        ' get the accel property
        Dim valueObject2 As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice2).GetValueObject(ValueObjectNames.Acceleration.Name)
        Dim accel2 As Int32 = CInt(valueObject2.Value)
        ' add IO status observer
        IDevice2.GetDeviceTemplate.StatusObserver.Attach(IOStatusObserver)
        ' create the facade, which will fire off various commands (here, a single command for testing purposes)
        Dim DeviceReceiveCmdFacade2 As DeviceReceiveCmdFacade = ScopeIII.Devices.DeviceReceiveCmdFacade.GetInstance
        DeviceReceiveCmdFacade2.IDevice = IDevice2

        ' finish building the receive facades and begin listening
        DeviceReceiveCmdFacade1.BuildIIO(IDevice1)
        DeviceReceiveCmdFacade1.StartIOListening()
        ' test that IOBuild succeeded
        Assert.IsTrue(CType(IOStatusObserver, TestIOStatusObserver).MsgList(0).StartsWith(IOStatus.Build.Description))
        ' share IIO
        Dim IIO As IIO = DeviceReceiveCmdFacade1.IIO
        DeviceReceiveCmdFacade2.IIO = IIO
        DeviceReceiveCmdFacade2.StartIOListening()
        ' open IIO
        DeviceReceiveCmdFacade1.IIOOpen()
        ' see if IIO was opened
        Assert.IsTrue(IIO.isOpened)
        ' test that IOOpen worked
        Assert.IsTrue(IOStatus.Open.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).MsgList(1)))

        ' setup the command
        ' don't set to default value!
        Dim testAccel As Int32 = 1234
        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.SiTechSetPriAccel.IDeviceCmd.CmdMsgParms.Cmd
        ' queue up the command
        DeviceReceiveCmdFacade1.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd & testAccel & vbCr))
        ' verify that command was processed: 1st device will report valid response, then cmd received
        Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).MsgList(2)))
        Assert.IsTrue(IOStatus.CmdReceived.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).MsgList(3)))
        ' the 2nd device will report a failed cmd because it sees an incomplete command that's not processed w/in the timeout period
        Assert.IsTrue(IOStatus.CmdFailed.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).MsgList(4)))
        ' verify that command updated only the first device's value
        Dim valueObject1 As ValueObject = SiTechUtil.GetPrimaryServoMotor(IDevice1).GetValueObject(ValueObjectNames.Acceleration.Name)
        Assert.AreEqual(testAccel, eMath.RInt(valueObject1.Value))
        Assert.AreEqual(accel2, eMath.RInt(valueObject2.Value))

        ' shut er down
        Try
            DeviceReceiveCmdFacade1.ShutdownIO()
            ' test that IO was closed
            Assert.IsTrue(IOStatus.Closed.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).MsgList(5)))
        Catch ex As Exception
            Debug.WriteLine(ExceptionService.BuildExceptionMsg(ex, Nothing))
            Throw ex
        Finally
            If IIO.isOpened Then
                DeviceReceiveCmdFacade1.ShutdownIO()
            End If
        End Try

        Assert.AreEqual(6, CType(IOStatusObserver, TestIOStatusObserver).MsgList.Count)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class TestIOStatusObserver : Implements IObserver
        Public MsgList As New Generic.List(Of String)
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            MsgList.Add(CStr([object]))
        End Function
    End Class
End Class

