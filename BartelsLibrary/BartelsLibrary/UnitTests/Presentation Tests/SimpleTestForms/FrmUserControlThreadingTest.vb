Imports NUnit.Framework

<TestFixture()> Public Class FrmUserControlThreadingTest

    Private controlToInvokeOn As Windows.Forms.Control

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestShowDialog()
        Me.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub UserControlThreadingTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetControlToInvokeOn()

        Dim closeFormTimer As New Timers.Timer(10000)
        AddHandler closeFormTimer.Elapsed, AddressOf closeForm
        closeFormTimer.AutoReset = False
        closeFormTimer.Start()
    End Sub

    Private Sub btnMainThread_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainThread.Click
        Try
            Dim userCntrlThread As New Threading.Thread(AddressOf AddUserControl)
            userCntrlThread.Start()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("btnMainThread " & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCtrlThread.Click
        controlToInvokeOn.Invoke(New Windows.Forms.MethodInvoker(AddressOf AddUserControl))
    End Sub

    Private Sub AddUserControl()
        Try
            Me.Controls.Add(New UserControlInvokeTest())
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("AddUserControl " & ex.Message)
        End Try
    End Sub

    ' copied from MVPPresenterBase.SetControlToInvokeOn()
    Private Sub SetControlToInvokeOn()
        controlToInvokeOn = Me
        For Each control As Windows.Forms.Control In controlToInvokeOn.Controls
            If control.InvokeRequired Then
                controlToInvokeOn = control
                Exit Sub
            End If
        Next
    End Sub

    Private Sub closeForm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New Windows.Forms.MethodInvoker(AddressOf Close))
        Else
            Close()
        End If
    End Sub

End Class