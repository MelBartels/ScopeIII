Imports NUnit.Framework

<TestFixture()> Public Class TestProgressMediator
    Private maxValue As Int32 = 22
    Private WithEvents pProgressMediator As ProgressMediator
    Private pTestObservable As testObservable

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestProgressMediator()
        pProgressMediator = ProgressMediator.GetInstance
        pProgressMediator.Title += " Unit Test"
        pProgressMediator.ProcessStartup(New DelegateSigs.DelegateObjDoWorkEventArgs(AddressOf callback))
    End Sub

    Private Sub callback(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        pProgressMediator.ProgressBarMaxValue = maxValue
        pProgressMediator.ShowMsg("Starting test...")

        pTestObservable = New testObservable
        ' don't exceed the progress bar's max: there are 2 additional msgs besides the testObservable's loop
        pTestObservable.MaxValue = maxValue - 2
        pTestObservable.ObservableImp.Attach(CType(pProgressMediator, IObserver))
        pTestObservable.LongRunningTask()
        pTestObservable.ObservableImp.Detach(CType(pProgressMediator, IObserver))

        pProgressMediator.ShowMsg("Finished test.")
        Threading.Thread.Sleep(2000)
        pProgressMediator.ProcessShutdown()
    End Sub

    Private Sub progressCancel() Handles pProgressMediator.CancelBackgroundWork
        pTestObservable.Cancel = True
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class testObservable
        Public ObservableImp As ObservableImp = BartelsLibrary.ObservableImp.GetInstance
        Public MaxValue As Int32
        Public Cancel As Boolean
        Public Sub LongRunningTask()
            For ix As Int32 = 0 To MaxValue - 1
                If Cancel Then
                    AppMsgBox.Show("cancelling early")
                    Return
                End If
                ObservableImp.Notify(CStr(ix))
                Threading.Thread.Sleep(100)
            Next
        End Sub
    End Class

End Class
