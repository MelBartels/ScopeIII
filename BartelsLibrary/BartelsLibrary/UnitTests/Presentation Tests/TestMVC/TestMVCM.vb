Imports NUnit.Framework

<TestFixture()> Public Class TestMVCM

    Private pFrmTestMVCMController As frmTestMVCMController

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFormAndController()
        Dim frmThread As New Threading.Thread(AddressOf showFrm)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(10000)
        AddHandler frmTimer.Elapsed, AddressOf killFrm
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private Sub showFrm()
        ' build form, then controller
        Dim frmTestMVCM As New FrmTestMVCM
        frmTestMVCM.GetControllerFactoryBuildAndInit()
        Assert.IsNotNull(frmTestMVCM.GetControllerReference)
        Assert.AreSame(frmTestMVCM, CType(frmTestMVCM.GetControllerReference, frmTestMVCMController).Frm)

        ' build controller, then form
        pFrmTestMVCMController = frmTestMVCMController.GetInstance
        Dim frmTestMVCM2 As New FrmTestMVCM
        pFrmTestMVCMController.Init(CType(frmTestMVCM2, ViewWin), Nothing, Nothing)
        Assert.AreSame(frmTestMVCM2, pFrmTestMVCMController.Frm)
        pFrmTestMVCMController.Frm.ShowDialog()
    End Sub

    Private Sub killFrm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmTestMVCMController.Frm.InvokeRequired Then
            pFrmTestMVCMController.Frm.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmTestMVCMController.Frm.Close))
        Else
            pFrmTestMVCMController.Frm.Close()
        End If
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
