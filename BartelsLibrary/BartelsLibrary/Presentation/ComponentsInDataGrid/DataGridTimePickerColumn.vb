Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class DataGridTimePickerColumn
    Inherits DataGridColumnStyle

    Private timePicker As New DateTimePicker
    Private isEditing As Boolean

    Public Sub New()
        timePicker.Visible = False
    End Sub

    Protected Overrides Sub Abort(ByVal rowNum As Integer)
        isEditing = False
        RemoveHandler timePicker.ValueChanged, AddressOf ComboBoxValueChanged
        Invalidate()
    End Sub

    Protected Overrides Function Commit(ByVal dataSource As CurrencyManager, ByVal rowNum As Integer) As Boolean
        timePicker.Bounds = Rectangle.Empty

        AddHandler timePicker.ValueChanged, AddressOf ComboBoxValueChanged

        If Not isEditing Then
            Return True
        End If
        isEditing = False

        Try
            Dim value As DateTime = timePicker.Value
            SetColumnValueAtRow(dataSource, rowNum, value)
        Catch
        End Try

        Invalidate()

        Return True
    End Function

    Protected Overloads Overrides Sub Edit(ByVal [source] As CurrencyManager, ByVal rowNum As Integer, ByVal bounds As Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
        Dim value As DateTime = CType(GetColumnValueAtRow([source], rowNum), DateTime)

        If cellIsVisible Then
            timePicker.Bounds = New Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4)

            timePicker.Value = value
            timePicker.Visible = True
            AddHandler timePicker.ValueChanged, AddressOf ComboBoxValueChanged
        Else
            timePicker.Value = value
            timePicker.Visible = False
        End If

        If timePicker.Visible Then
            DataGridTableStyle.DataGrid.Invalidate(bounds)
        End If
    End Sub

    Protected Overrides Function GetPreferredSize(ByVal g As Graphics, ByVal value As Object) As Size
        Return New Size(100, timePicker.PreferredHeight + 4)
    End Function

    Protected Overrides Function GetMinimumHeight() As Integer
        Return timePicker.PreferredHeight + 4
    End Function

    Protected Overrides Function GetPreferredHeight(ByVal g As Graphics, ByVal value As Object) As Integer
        Return timePicker.PreferredHeight + 4
    End Function

    Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal [source] As CurrencyManager, ByVal rowNum As Integer)
        Paint(g, bounds, [source], rowNum, False)
    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal [source] As CurrencyManager, ByVal rowNum As Integer, ByVal alignToRight As Boolean)
        Paint(g, bounds, [source], rowNum, Brushes.Red, Brushes.Blue, alignToRight)
    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal [source] As CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As Brush, ByVal foreBrush As Brush, ByVal alignToRight As Boolean)
        Dim [date] As DateTime = CType(GetColumnValueAtRow([source], rowNum), DateTime)
        Dim rect As Rectangle = bounds
        g.FillRectangle(backBrush, rect)
        rect.Offset(0, 2)
        rect.Height -= 2
        g.DrawString([date].ToString("d"), Me.DataGridTableStyle.DataGrid.Font, foreBrush, RectangleF.FromLTRB(rect.X, rect.Y, rect.Right, rect.Bottom))
    End Sub

    Protected Overrides Sub SetDataGridInColumn(ByVal value As DataGrid)
        MyBase.SetDataGridInColumn(value)
        If timePicker.Parent IsNot Nothing Then
            timePicker.Parent.Controls.Remove(timePicker)
        End If
        If value IsNot Nothing Then
            value.Controls.Add(timePicker)
        End If
    End Sub

    Private Sub ComboBoxValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        Me.isEditing = True
        MyBase.ColumnStartedEditing(timePicker)
    End Sub
End Class

