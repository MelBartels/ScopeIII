Imports NUnit.Framework

<TestFixture()> Public Class ReceiveInspectorTests

    Private pIIO As IIO
    Private pTestMsg As String = "hello"

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestReceiveInspector()
        pIIO = IOBuilder.GetInstance.Build(CType(IOType.SerialPort, ISFT))
        pIIO.Open()

        ' create an observer of the inspector and add it (defined private class below)
        Dim receiveInspectorObserver As TestIObserver = TestIObserver.GetInstance
        pIIO.ReceiveInspector.InspectorObservers.Attach(CType(receiveInspectorObserver, IObserver))

        ' send something by adding to the receive queue and see if the inspector picks it up
        pIIO.ReceiveInspector.Inspect(ReceiveInspectParms.GetInstance(pTestMsg.Length, False, Nothing, 500))
        Assert.AreSame(ReceiveInspectorState.InProcess, pIIO.ReceiveInspector.State)
        pIIO.QueueReceiveBytes(Encoder.StringtoBytes(pTestMsg))
        'IO.DLL!ScopeIII.IO.ReceiveInspectorTests.ReceiveInspectorObserver.ProcessMsg(Object object = "hello") Line 93	Basic
        'Common.DLL!ScopeIII.ObservableImp.Notify(Object object = "hello") Line 61 + 0xc bytes	Basic
        'IO.DLL!ScopeIII.IO.ReceiveInspector.notifyInspectorObservers() Line 183 + 0x1d bytes	Basic
        'IO.DLL!ScopeIII.IO.ReceiveInspector.ProcessMsg(Object object = "hello") Line 145 + 0x9 bytes	Basic
        'Common.DLL!ScopeIII.ObservableImp.Notify(Object object = "hello") Line 61 + 0xc bytes	Basic
        'IO.DLL!ScopeIII.IO.IOBase.NotifyReceiveObservers(String data = "hello") Line 226 + 0x11 bytes	Basic
        'IO.DLL!ScopeIII.IO.IOBase.QueueReceiveBytes(Byte() bytes = {Length=5}) Line 220 + 0x1e bytes	Basic
        Assert.IsTrue(pTestMsg.Equals(receiveInspectorObserver.Msg))
        Assert.AreSame(ReceiveInspectorState.ReadCorrectNumberOfBytes, pIIO.ReceiveInspector.State)

        ' force failure by asking for too many bytes
        receiveInspectorObserver.Msg = "changed"
        pIIO.ReceiveInspector.Inspect(ReceiveInspectParms.GetInstance(2 * pTestMsg.Length, False, Nothing, 500))
        pIIO.QueueReceiveBytes(Encoder.StringtoBytes(pTestMsg))
        ' wait long enough for test environment to record the timer's expiration
        Threading.Thread.Sleep(1000)
        ' returned msg should be msg sent, state should be timed out, and bytes received = test msg length
        Assert.IsTrue(pTestMsg.Equals(receiveInspectorObserver.Msg))
        Assert.AreSame(ReceiveInspectorState.TimedOut, pIIO.ReceiveInspector.State)
        Assert.AreEqual(pTestMsg.Length, pIIO.ReceiveInspector.BytesRead)

        ' test for terminating char
        receiveInspectorObserver.Msg = "changed again"
        pIIO.ReceiveInspector.Inspect(ReceiveInspectParms.GetInstance(pTestMsg.Length, True, Microsoft.VisualBasic.AscW("l"), 500))
        pIIO.QueueReceiveBytes(Encoder.StringtoBytes(pTestMsg))
        Assert.IsTrue(pTestMsg.Equals(receiveInspectorObserver.Msg))
        Assert.AreSame(ReceiveInspectorState.TerminatedCharFound, pIIO.ReceiveInspector.State)

        ' test for too many chars received
        receiveInspectorObserver.Msg = "changed yet again"
        pIIO.ReceiveInspector.Inspect(ReceiveInspectParms.GetInstance(pTestMsg.Length - 1, False, Nothing, 500))
        pIIO.QueueReceiveBytes(Encoder.StringtoBytes(pTestMsg))
        Assert.IsTrue(pTestMsg.Equals(receiveInspectorObserver.Msg))
        Assert.AreSame(ReceiveInspectorState.ReadTooManyBytes, pIIO.ReceiveInspector.State)

        ' test for delayed send/observe
        receiveInspectorObserver.Msg = "changed yet once again"

        Dim delaySend As New Timers.Timer(500)
        AddHandler delaySend.Elapsed, AddressOf delayedSend
        delaySend.AutoReset = False
        delaySend.Start()

        pIIO.ReceiveInspector.Inspect(ReceiveInspectParms.GetInstance(pTestMsg.Length, False, Nothing, 1000))
        ' verify that nothing's fired off yet
        Assert.AreSame(ReceiveInspectorState.InProcess, pIIO.ReceiveInspector.State)
        ' wait for response from observer of inspector
        Threading.Thread.Sleep(1000)
        Assert.IsTrue(pTestMsg.Equals(receiveInspectorObserver.Msg))
        Assert.AreSame(ReceiveInspectorState.ReadCorrectNumberOfBytes, pIIO.ReceiveInspector.State)

        ' test Shutdown
        pIIO.Shutdown()
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub createStringFromQueue()
        Dim bytes() As Byte = New Byte() {Asc("h"), Asc("e"), Asc("l"), Asc("l"), Asc("o")}
        Dim rif As New ReceiveInspectorFake
        rif.LoadReadBuffer(bytes)
        Assert.AreEqual("hello", rif.CreateStringFromQueue)

        Assert.IsTrue(True)
    End Sub

    Private Sub delayedSend(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pIIO.QueueReceiveBytes(Encoder.StringtoBytes(pTestMsg))
    End Sub

    Class ReceiveInspectorFake : Inherits ReceiveInspector
        Public Sub LoadReadBuffer(ByVal bytes() As Byte)
            Array.Copy(bytes, pReadBuffer, bytes.Length)
            pEndReadBufferPtr = bytes.Length - 1
        End Sub
        Public Overloads Function CreateStringFromQueue() As String
            Return MyBase.createStringFromQueue
        End Function
    End Class

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
