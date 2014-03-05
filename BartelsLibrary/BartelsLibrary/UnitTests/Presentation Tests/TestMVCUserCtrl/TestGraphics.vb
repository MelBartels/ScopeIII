Imports NUnit.Framework

'<Ignore("NUnit test for dev purposes, not for regression testing."), TestFixture()> Public Class frmTestUserCtrlTest
<TestFixture()> Public Class TestGraphics

    Private pFrmTestUserCtrl As frmTestUserCtrl

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestUserCtrl()
        Dim frmThread As New Threading.Thread(AddressOf showFrmTestUserCtrl)
        frmThread.Start()
        Dim frmTimer As New Timers.Timer(10000)
        AddHandler frmTimer.Elapsed, AddressOf killfrmTestUserCtrl
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private Sub showFrmTestUserCtrl()
        pFrmTestUserCtrl = New frmTestUserCtrl
        pFrmTestUserCtrl.FrmTestUserCtrl = pFrmTestUserCtrl
        pFrmTestUserCtrl.TestUserCtrl1.GetUserCtrlControllerFactoryBuildAndInit()
        pFrmTestUserCtrl.TestUserCtrlController = CType(pFrmTestUserCtrl.TestUserCtrl1.GetUserCtrlControllerReference, TestUserCtrlController)
        pFrmTestUserCtrl.TestUserCtrlController.IRenderer = TestRenderer.GetInstance
        pFrmTestUserCtrl.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killfrmTestUserCtrl(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmTestUserCtrl.InvokeRequired Then
            pFrmTestUserCtrl.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmTestUserCtrl.Close))
        Else
            pFrmTestUserCtrl.Close()
        End If
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
