Imports NUnit.Framework

<TestFixture()> Public Class ObserverTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub AttachDetachNotifyTests()
        Dim observed As New observed
        Dim observer As New observer
        ' attach
        observed.ObservableImp.Attach(CType(observer, IObserver))
        ' verify that nothing observed
        Assert.IsFalse(observer.Processed)
        ' notify
        observed.TriggerSomething()
        ' verify that msg processed
        Assert.IsTrue(observer.Processed)
        ' reset
        observer.Processed = False
        ' remove observer
        observed.ObservableImp.Detach(CType(observer, IObserver))
        ' trigger again
        observed.TriggerSomething()
        ' verify that nothing was observed
        Assert.IsFalse(observer.Processed)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class Observed
        Public ObservableImp As ObservableImp = ObservableImp.GetInstance
        Public Sub TriggerSomething()
            ObservableImp.Notify(String.Empty)
        End Sub
    End Class
    Private Class Observer
        Implements IObserver
        Public Processed As Boolean = False
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Processed = True
        End Function
    End Class
End Class
