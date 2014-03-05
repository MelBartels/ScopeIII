Public Class FrmDataFileConverter

    Public Event SelectFile()

    Public Sub DisplayText(ByVal msg As String)
        If txBxDisplay.InvokeRequired Then
            txBxDisplay.Invoke(New BartelsLibrary.DelegateSigs.DelegateStr(AddressOf DisplayText), New Object() {msg})
        Else
            txBxDisplay.AppendText(msg)
            txBxDisplay.SelectionStart = txBxDisplay.Text.Length
            txBxDisplay.ScrollToCaret()
        End If
    End Sub

    Private Sub FrmDataFileConverter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblExplainText()
    End Sub

    Private Sub lblExplainText()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("Converts from format")
        sb.AppendLine(String.Empty)
        sb.AppendLine("(empty line)")
        sb.AppendLine("NGC 8")
        sb.AppendLine("00h 08m 45.7s +23° 50' 16"" Peg ** ")
        sb.AppendLine(String.Empty)
        sb.AppendLine("to Scope datafile format")
        sb.AppendLine(String.Empty)
        sb.Append("00 08 45.7 23 50 16 Peg NGC 8")
        lblExplain.Text = sb.ToString
    End Sub

    Private Sub btnSelectFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFile.Click
        RaiseEvent SelectFile()
    End Sub
End Class
