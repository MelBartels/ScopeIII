Imports NUnit.Framework

<TestFixture()> Public Class DelegateTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestDelegate()
        Dim observed As New observed
        Dim observer As New observer
        ' setup
        observer.RegisterDelegate(CType(observed, IDelegateTest))
        ' verify that nothing processed
        Assert.IsFalse(observer.Processed)
        ' execute delegate
        observed.RunDelegate()
        ' verify that event processed
        Assert.IsTrue(observer.Processed)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Public Interface IDelegateTest
        Sub RegisterDelegate(ByRef delegateToRun As [Delegate])
    End Interface
    Private Class Observed : Implements IDelegateTest
        Private pDelegates As New ArrayList
        Public Sub RegisterDelegate(ByRef delegateToRun As [Delegate]) Implements IDelegateTest.RegisterDelegate
            pDelegates.Add(delegateToRun)
        End Sub
        Public Sub RunDelegate()
            For Each [delegate] As [Delegate] In pDelegates
                [delegate].DynamicInvoke(New Object() {"notifying you"})
            Next
        End Sub
    End Class
    ' Observed sends a message to Observer;
    ' this class (Observer) RegisterDelegate calls above class (Observed) RegisterDelegate to add a delegate
    ' declared in this class (TriggerSomethingDelegate) that points to method in this class (TriggerSomething);
    ' when above class (Observed) RunDelegate is invoked, this class (Observer) method TriggerSomething is executed;
    ' Note use of Interface to eliminate explicit reference to Observed in Observer
    Private Class Observer
        Public Processed As Boolean = False
        Delegate Sub TriggerSomethingDelegate(ByVal [Object] As Object)
        Public Sub RegisterDelegate(ByRef IDelegateTest As IDelegateTest)
            IDelegateTest.RegisterDelegate(New TriggerSomethingDelegate(AddressOf TriggerSomething))
        End Sub
        Private Sub TriggerSomething(ByVal [Object] As Object)
            If CStr([Object]).Equals("notifying you") Then
                Processed = True
            End If
        End Sub
    End Class

End Class
