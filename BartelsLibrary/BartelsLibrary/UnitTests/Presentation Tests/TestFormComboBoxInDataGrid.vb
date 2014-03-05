Imports NUnit.Framework

<TestFixture()> Public Class TestFormComboBoxInDataGrid

    Public Sub New()
        MyBase.New()
    End Sub

    Private pFormComboBoxInDataGrid As FrmComboBoxInDataGrid

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub ShowForm()
        Dim frmThread As New Threading.Thread(AddressOf showFrm)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(60000)
        AddHandler frmTimer.Elapsed, AddressOf killFrm
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private Sub showFrm()
        pFormComboBoxInDataGrid = New FrmComboBoxInDataGrid
        System.Windows.Forms.Application.Run(pFormComboBoxInDataGrid)
    End Sub

    Private Sub killFrm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFormComboBoxInDataGrid.InvokeRequired Then
            pFormComboBoxInDataGrid.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFormComboBoxInDataGrid.Close))
        Else
            pFormComboBoxInDataGrid.Close()
        End If
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
