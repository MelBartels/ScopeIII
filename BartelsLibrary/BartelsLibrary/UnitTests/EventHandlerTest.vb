Imports NUnit.Framework

<TestFixture()> Public Class EventHandlerTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestEventHandler()
        Dim observed As New observed
        Dim observer As New observer
        ' set explicit handler
        observer.Observed = observed
        ' verify that nothing processed
        Assert.IsFalse(observer.Processed)
        ' throw event
        observed.TriggerSomething()
        ' verify that event processed
        Assert.IsTrue(observer.Processed)
    End Sub

    <Test()> Public Sub TestEventHandler2()
        Dim observed2 As New observed2
        Dim observer2 As New observer2
        ' set indirect handler
        observer2.IEventHandler = CType(observed2, IEventHandler)
        ' verify that nothing processed
        Assert.IsFalse(observer2.Processed)
        ' throw event
        observed2.TriggerSomething()
        ' verify that event processed
        Assert.IsTrue(observer2.Processed)
    End Sub

    <Test()> Public Sub TestInheritedEvent()
        Dim observedChild As New observedChild
        Dim observer As New observer
        ' set explicit handler
        observer.Observed = observedChild
        ' verify that nothing processed
        Assert.IsFalse(observer.Processed)
        ' throw event
        observedChild.TriggerSomething()
        ' verify that event processed
        Assert.IsTrue(observer.Processed)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class Observed
        Public Event SomethingHappened(ByVal sender As Object)
        Public Overridable Sub TriggerSomething()
            RaiseEvent SomethingHappened(Me)
        End Sub
    End Class
    Private Class ObservedChild : Inherits Observed
        Public Overrides Sub TriggerSomething()
            ' cannot call an event from a base class, so raise the event from a base class method
            ' then override base class method, do extra stuff, finally call the base class method
            ' that raises the event
            MyBase.TriggerSomething()
        End Sub
    End Class
    Private Class Observer
        Public Processed As Boolean = False
        Public WithEvents Observed As New Observed
        Private Sub EventHandler(ByVal sender As Object) Handles Observed.SomethingHappened
            Processed = True
        End Sub
    End Class

    Public Interface IEventHandler
        Event SomethingHappened(ByVal sender As Object)
    End Interface
    Private Class Observed2 : Implements IEventHandler
        Public Event SomethingHappened(ByVal sender As Object) Implements IEventHandler.SomethingHappened
        Public Sub TriggerSomething()
            RaiseEvent SomethingHappened(Me)
        End Sub
    End Class
    Private Class Observer2
        Public Processed As Boolean = False
        Public WithEvents IEventHandler As IEventHandler
        Private Sub EventHandler(ByVal sender As Object) Handles IEventHandler.SomethingHappened
            Processed = True
        End Sub
    End Class
End Class
