Imports NUnit.Framework

<TestFixture()> Public Class TestFrmProgress
    Implements IObserver

    Private pProgressBarPresenter As ProgressBarPresenter

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFrm()
        Dim frmThread As New Threading.Thread(AddressOf showFrm)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(60000)
        AddHandler frmTimer.Elapsed, AddressOf killFrm
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private pBackgroundWorkThread As Threading.Thread

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        pBackgroundWorkThread = New Threading.Thread(AddressOf backgroundWork)
        pBackgroundWorkThread.Start()
    End Function

    Private Sub backgroundWork()
        Dim max As Int32 = 20
        For ix As Int32 = 0 To max
            pProgressBarPresenter.DataModel = New Object() {ix / max, ix.ToString}
            ' do some stuff to make system busy
            Threading.Thread.Sleep(100)
            For wait As Double = 0 To 1000000
                workOnThisThread()
            Next
        Next
        pProgressBarPresenter.Close()
    End Sub

    Private Sub workOnThisThread()
        ' do some throw away work to make the system busy
        ProgressBarPresenter.GetInstance()
    End Sub

    Private Sub showFrm()
        pProgressBarPresenter = ProgressBarPresenter.GetInstance
        pProgressBarPresenter.IMVPView = New FrmProgressBar
        pProgressBarPresenter.IObservable.Attach(Me)
        pProgressBarPresenter.ShowDialog()
    End Sub

    Private Sub killFrm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pProgressBarPresenter.Close()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class
