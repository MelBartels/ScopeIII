Imports NUnit.Framework

<TestFixture()> Public Class TestMVPUserCtrl

    Private pTestMVPUserCtrlPresenter As TestMVPUserCtrlPresenter

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

    Private Sub showFrm()
        pTestMVPUserCtrlPresenter = TestMVPUserCtrlPresenter.GetInstance
        pTestMVPUserCtrlPresenter.IMVPView = New FrmTestMVPUserCtrl
        pTestMVPUserCtrlPresenter.DataModel = TestMVPRenderer.GetInstance
        pTestMVPUserCtrlPresenter.ShowDialog()
    End Sub

    Private Sub killFrm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pTestMVPUserCtrlPresenter.Close()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
