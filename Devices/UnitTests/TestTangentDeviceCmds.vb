Imports NUnit.Framework

<TestFixture()> Public Class TestTangentDeviceCmds

    Delegate Sub cmdDelegate(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal priValueObserver As IObserver, ByVal secValueObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestTangentQuery()
        cmdShell(New cmdDelegate(AddressOf queryCmd))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestTangentReset()
        cmdShell(New cmdDelegate(AddressOf resetRCmd))
        Assert.IsTrue(True)
    End Sub

    Private Sub cmdShell(ByVal cmdDelegate As cmdDelegate)
        ' build the device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        ' add IO status observer
        Dim IOStatusObserver As IObserver = New TestIOStatusObserver
        IDevice.GetDeviceTemplate.StatusObserver.Attach(IOStatusObserver)
        ' add sensor status observers   
        Dim te As EncodersBox = CType(IDevice, EncodersBox)
        Dim priValueObserver As IObserver = New TestSensorStatusObserver
        Dim encoder As IDevice = CType(IDevice.GetDevicesByName(te.PriAxisName).Item(0), IDevice)
        encoder.GetDeviceTemplate.StatusObserver.Attach(priValueObserver)
        Dim secValueObserver As IObserver = New TestSensorStatusObserver
        encoder = CType(IDevice.GetDevicesByName(te.SecAxisName).Item(0), IDevice)
        encoder.GetDeviceTemplate.StatusObserver.Attach(secValueObserver)

        ' create the facade, which will fire off various commands (here, a single command for testing purposes)
        Dim DeviceCmdFacade As DeviceCmdFacade = ScopeIII.Devices.DeviceCmdFacade.GetInstance
        DeviceCmdFacade.IDevice = IDevice
        If DeviceCmdFacade.BuildIIO(IDevice) Then
            Try
                DeviceCmdFacade.StartIOListening()
                ' test that IOBuild succeeded
                Assert.IsTrue(CType(IOStatusObserver, TestIOStatusObserver).msg.StartsWith(IOStatus.Build.Description))
                CType(IOStatusObserver, TestIOStatusObserver).msg = Nothing

                If DeviceCmdFacade.IIOOpen Then
                    ' test that IOOpen worked
                    Assert.IsTrue(IOStatus.Open.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
                    ' add the xmt observer
                    Dim IOXmtObserver As IObserver = New IOXmtObserver
                    DeviceCmdFacade.IIO.TransmitObservers.Attach(IOXmtObserver)
                    ' call the command
                    cmdDelegate.Invoke(IDevice, IOStatusObserver, IOXmtObserver, priValueObserver, secValueObserver, DeviceCmdFacade)
                Else
                    Assert.Fail(IOStatus.OpenFailed.Description)
                End If

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

        Assert.IsTrue(True)
    End Sub

    Private Sub queryCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal priValueObserver As IObserver, ByVal secValueObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        Dim te As EncodersBox = CType(IDevice, EncodersBox)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.TangentEncodersQuery.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' insert data into the receive queue as if it were received from an actual device
            Dim priTestValue As Int32 = 1234
            Dim secTestValue As Int32 = 4321
            Dim testString As String = BartelsLibrary.Constants.Plus & priTestValue & vbTab & BartelsLibrary.Constants.Plus & secTestValue & vbCr
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(testString))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results:
            ' IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' pri encoder value and status
            Dim value As EncoderValue = CType(CType(te.GetDevicesByName(te.PriAxisName).Item(0), Encoder).GetProperty(GetType(EncoderValue).Name), EncoderValue)
            Assert.AreEqual(priTestValue, eMath.RInt(value.Value))
            Assert.IsTrue(SensorStatus.ValidRead.Description.Equals(CType(priValueObserver, TestSensorStatusObserver).msg))
            ' sec encoder value and status
            value = CType(CType(te.GetDevicesByName(te.SecAxisName).Item(0), Encoder).GetProperty(GetType(EncoderValue).Name), EncoderValue)
            Assert.AreEqual(secTestValue, eMath.RInt(value.Value))
            Assert.IsTrue(SensorStatus.ValidRead.Description.Equals(CType(secValueObserver, TestSensorStatusObserver).msg))
            ' ReceiveInspector rejects subsequent messages because ReceiveInspectorState is not InProcess and therefore does not notify its observers
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes("melbiewon"))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    Private Sub resetRCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal priValueObserver As IObserver, ByVal secValueObserver As IObserver, ByVal DeviceCmdFacade As DeviceCmdFacade)
        ' set new counts per revolution for the reset command 
        CType(IDevice, EncodersBox).SetCountsPerRevolution(12345, 123)
        Dim IDeviceCmd As IDeviceCmd = DeviceCmdAndReplyTemplateDefaults.TangentEncodersResetR.IDeviceCmd
        If DeviceCmdFacade.Execute(IDeviceCmd) Then
            ' test that something was xmt'd
            Assert.IsTrue(IOStatus.CmdSent.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
            ' test what was xmt'd
            Assert.AreEqual(IDeviceCmd.CmdMsgParms.ExpectedByteCount, CType(IOXmtObserver, IOXmtObserver).Msg.Length)
            ' insert data into the receive queue as if it were received from an actual device
            DeviceCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(IDeviceCmd.CmdMsgParms.Cmd))
            ' wait for response
            Threading.Thread.Sleep(ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds)
            ' test results: TangentEncodersResetRSendCmd.ProcessMsg will absorb a 'R' if successful reset and set the IO status
            Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIOStatusObserver).msg))
        Else
            Assert.Fail(IOStatus.CmdFailed.Description)
        End If
    End Sub

    <Test()> Public Sub TestCmdsFromCmdSet()
        ' build the device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        Dim cmdProtocolName As String = CType(IDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name), DevPropDeviceCmdSet).Name
        Assert.AreEqual("TangentEncodersWithResetR", cmdProtocolName)
        Dim cmdSet As ISFT = DeviceCmdSet.ISFT.MatchString(cmdProtocolName)
        Assert.IsInstanceOfType(GetType(TangentEncodersWithResetRCmds), cmdSet.Tag)
        Dim cmds As ISFTFacade = CType(cmdSet.Tag, ISFTFacade)
        Assert.IsNotNull(cmds)
        Assert.IsTrue(cmds.FirstItem.FacadeName.Equals(GetType(TangentEncodersWithResetRCmds).FullName))
        ' get my command set from IDevice
        Assert.IsTrue(IDevice.GetCmdSet.FirstItem.FacadeName.Equals(GetType(TangentEncodersWithResetRCmds).FullName))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestInstantiations()
        Dim tangentEncodersQueryCmds As TangentEncodersQueryCmds = tangentEncodersQueryCmds.GetInstance
        Assert.IsNotNull(tangentEncodersQueryCmds)
        Dim TangentEncodersWithResetRCmds As TangentEncodersWithResetRCmds = TangentEncodersWithResetRCmds.GetInstance
        Assert.IsNotNull(TangentEncodersWithResetRCmds)
        Dim TangentEncodersWithResetZCmds As TangentEncodersWithResetZCmds = TangentEncodersWithResetZCmds.GetInstance
        Assert.IsNotNull(TangentEncodersWithResetZCmds)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class TestIOStatusObserver : Implements IObserver
        Public msg As String
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Me.msg = CStr([object])
        End Function
    End Class

    Private Class TestSensorStatusObserver : Implements IObserver
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

