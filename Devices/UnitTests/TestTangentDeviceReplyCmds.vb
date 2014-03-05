Imports NUnit.Framework

<TestFixture()> Public Class TestTangentDeviceReplyCmds

    Delegate Sub cmdDelegate(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)

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

    Private Sub queryCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim te As EncodersBox = CType(IDevice, EncodersBox)
        Dim IDeviceReplyCmd As IDeviceReplyCmd = DeviceCmdAndReplyTemplateDefaults.TangentEncodersQuery.IDeviceReplyCmd
        Dim priTestValue As Int32 = 1234
        Dim secTestValue As Int32 = 4321
        te.SetValues(CStr(priTestValue), CStr(secTestValue))

        Dim cmd As String = DeviceCmdAndReplyTemplateDefaults.TangentEncodersQuery.IDeviceCmd.CmdMsgParms.Cmd
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd))
        ' "+01234	+04321 "
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        Dim expectedMsg As String = BartelsLibrary.Constants.Plus & Format(priTestValue, "00000") & vbTab & BartelsLibrary.Constants.Plus & Format(secTestValue, "00000") & vbCr
        Assert.AreEqual(expectedMsg, xmtMsg)
    End Sub

    Private Sub resetRCmd(ByVal IDevice As IDevice, ByVal IOStatusObserver As IObserver, ByVal IOXmtObserver As IObserver, ByVal DeviceReceiveCmdFacade As DeviceReceiveCmdFacade)
        Dim te As EncodersBox = CType(IDevice, EncodersBox)
        Dim IDeviceReplyCmd As IDeviceReplyCmd = DeviceCmdAndReplyTemplateDefaults.TangentEncodersResetR.IDeviceReplyCmd
        Dim cmd As String = "R20000" & vbTab & "20000" & vbCr
        DeviceReceiveCmdFacade.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(cmd))
        Dim xmtMsg As String = CType(IOXmtObserver, IOXmtObserver).Msg
        ' "R"
        Dim expectedMsg As String = DeviceCmdAndReplyTemplateDefaults.TangentEncodersResetR.IDeviceReplyCmd.CmdReplyParms.Reply
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

