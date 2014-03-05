Public Class FrmTestThreading

    Public MainTestThreadingSubForm As MainTestThreadingSubForm = MainTestThreadingSubForm.GetInstance

    Private Sub FrmTestThreading_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.DataSource = [Enum].GetNames(GetType(Threading.ApartmentState))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MainTestThreadingSubForm.ApartmentState = setThreadingModel()
        MainTestThreadingSubForm.TestForm = New FrmTestListView
        MainTestThreadingSubForm.Main()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MainTestThreadingSubForm.ApartmentState = setThreadingModel()
        MainTestThreadingSubForm.TestForm = New FrmTestDataGridView
        MainTestThreadingSubForm.Main()
    End Sub

    Private Function setThreadingModel() As Threading.ApartmentState
        Return CType([Enum].Parse(GetType(Threading.ApartmentState), CStr(ComboBox1.SelectedItem)), Threading.ApartmentState)
    End Function
End Class
