Imports NUnit.Framework

<TestFixture()> Public Class ObserverWithIDTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestObserverWithID()
        Dim testObservingID As String = "testObservingID"
        Dim testObservedID As String = "testObservedID"
        Dim testMsg As String = "testMsg"
        Dim testIObserver As TestIObserver = testIObserver.GetInstance

        Dim observerWithID As ObserverWithID = observerWithID.GetInstance.Build(testObservingID, testObservedID, CType(testIObserver, IObserver))
        Assert.AreEqual(testObservingID, observerWithID.ObservingID)

        ' call observer directly
        observerWithID.ProcessMsg(CObj(testMsg))
        Assert.AreEqual(testObservedID & observerWithID.IDSeparator & testMsg, testIObserver.Msg)

        ' call observer from an IObservable, testing the whole chain
        testIObserver.Msg = String.Empty
        Dim observable As IObservable = ObservableImp.GetInstance
        observable.Attach(CType(observerWithID, IObserver))
        observable.Notify(CObj(testMsg))
        Assert.AreEqual(testObservedID & observerWithID.IDSeparator & testMsg, testIObserver.Msg)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
