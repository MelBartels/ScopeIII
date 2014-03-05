Imports NUnit.Framework

<TestFixture()> Public Class TestFormDateTimeInDataGrid
    Public Sub New()
        MyBase.New()
    End Sub

    Private pFormDateTimeInDataGrid As FrmDateTimeInDataGrid

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
        pFormDateTimeInDataGrid = New FrmDateTimeInDataGrid
        System.Windows.Forms.Application.Run(pFormDateTimeInDataGrid)
    End Sub

    Private Sub killFrm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFormDateTimeInDataGrid.InvokeRequired Then
            pFormDateTimeInDataGrid.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFormDateTimeInDataGrid.Close))
        Else
            pFormDateTimeInDataGrid.Close()
        End If
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
