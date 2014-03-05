Imports NUnit.Framework

<TestFixture()> Public Class TestDeviceToIOBridge

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestBridge()
        ' build a device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.TestDevice, ISFT))
        DeviceTemplateBuilder.GetInstance.Build(IDevice)
        ' add IO status observer to the device
        Dim IOStatusObserver As IObserver = TestIObserver.GetInstance
        IDevice.GetDeviceTemplate.StatusObserver.Attach(IOStatusObserver)
        ' create the bridge
        Dim DeviceToIOBridge As DeviceToIOBridge = ScopeIII.Devices.DeviceToIOBridge.GetInstance
        ' create the receive observer
        Dim testReceiveObserver As IObserver = New TestReceiveObserver(IDevice)
        ' set the device
        DeviceToIOBridge.IDevice = IDevice
        ' build IIO
        If DeviceToIOBridge.BuildIIO() Then
            ' sets testReceiveObserver to observe IO received data
            DeviceToIOBridge.InspectorObservers.Attach(testReceiveObserver)
            ' test that IOBuild succeeded
            Assert.IsTrue(CType(IOStatusObserver, TestIObserver).Msg.StartsWith(IOStatus.Build.Description))
            CType(IOStatusObserver, TestIObserver).Msg = Nothing
            If DeviceToIOBridge.IIOOpen Then
                ' test that IOOpen worked
                Assert.IsTrue(IOStatus.Open.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))
                ' setup test string for IO receiving
                Dim testString As String = "hello"
                DeviceToIOBridge.IIO.ReceiveInspector.Inspect(ReceiveInspectParms.GetInstance(testString.Length, False, Nothing, 500))
                ' insert data into the receive queue 
                DeviceToIOBridge.IIO.QueueReceiveBytes(BartelsLibrary.Encoder.StringtoBytes(testString))
                ' wait for response
                Threading.Thread.Sleep(500)
                ' test results by checking the IO status and receive status
                Assert.IsTrue(IOStatus.ValidResponse.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))
                Assert.AreSame(ReceiveInspectorState.ReadCorrectNumberOfBytes, DeviceToIOBridge.IIO.ReceiveInspector.State)
                ' finish off
                DeviceToIOBridge.Shutdown()
                ' test that IO was closed
                Assert.IsTrue(IOStatus.Closed.Description.Equals(CType(IOStatusObserver, TestIObserver).Msg))
                DeviceToIOBridge.InspectorObservers.Detach(testReceiveObserver)
            Else
                Assert.Fail(IOStatus.OpenFailed.Description)
            End If
        Else
            Assert.Fail(IOStatus.BuildFailed.Description)
        End If

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class TestReceiveObserver : Implements IObserver
        Public IDevice As IDevice
        Public Sub New(ByRef IDevice As IDevice)
            Me.IDevice = IDevice
        End Sub
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            ' set to valid if any data received;
            ' this class is attached to IIO.ReceiveInspector.InspectorObservers 
            IDevice.SetIOStatus(IOStatus.ValidResponse.Description)
            Return True
        End Function
    End Class
End Class

