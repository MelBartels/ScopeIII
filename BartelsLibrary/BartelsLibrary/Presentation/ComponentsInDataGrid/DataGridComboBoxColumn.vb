Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class DataGridComboBoxColumn
    Inherits DataGridColumnStyle

    Private comboBox As New comboBox
    Private isEditing As Boolean

    Public Sub New()
        comboBox.Visible = False
        comboBox.DataSource = [Enum].GetNames(GetType(FrmComboBoxInDataGrid.ComboBoxData))
    End Sub

    Protected Overrides Sub Abort(ByVal rowNum As Integer)
        isEditing = False
        RemoveHandler comboBox.SelectedValueChanged, AddressOf ComboBoxValueChanged
        Invalidate()
    End Sub

    Protected Overrides Function Commit(ByVal dataSource As CurrencyManager, ByVal rowNum As Integer) As Boolean
        comboBox.Bounds = Rectangle.Empty

        AddHandler comboBox.SelectedValueChanged, AddressOf ComboBoxValueChanged

        If Not isEditing Then
            Return True
        End If
        isEditing = False

        Try
            Dim value As Object = comboBox.SelectedItem
            SetColumnValueAtRow(dataSource, rowNum, value)
        Catch
        End Try

        Invalidate()

        Return True
    End Function

    Protected Overloads Overrides Sub Edit(ByVal [source] As CurrencyManager, ByVal rowNum As Integer, ByVal bounds As Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
        Dim value As Object = GetColumnValueAtRow([source], rowNum)

        If cellIsVisible Then
            comboBox.Bounds = New Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4)

            comboBox.SelectedItem = value
            comboBox.Visible = True
            AddHandler comboBox.SelectedValueChanged, AddressOf ComboBoxValueChanged
        Else
            comboBox.SelectedItem = value
            comboBox.Visible = False
        End If

        If comboBox.Visible Then
            DataGridTableStyle.DataGrid.Invalidate(bounds)
        End If
    End Sub

    Protected Overrides Function GetPreferredSize(ByVal g As Graphics, ByVal value As Object) As Size
        Return New Size(100, comboBox.PreferredHeight + 4)
    End Function

    Protected Overrides Function GetMinimumHeight() As Integer
        Return comboBox.PreferredHeight + 4
    End Function

    Protected Overrides Function GetPreferredHeight(ByVal g As Graphics, ByVal value As Object) As Integer
        Return comboBox.PreferredHeight + 4
    End Function

    Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal [source] As CurrencyManager, ByVal rowNum As Integer)
        Paint(g, bounds, [source], rowNum, False)
    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal [source] As CurrencyManager, ByVal rowNum As Integer, ByVal alignToRight As Boolean)
        Paint(g, bounds, [source], rowNum, Brushes.Red, Brushes.Blue, alignToRight)
    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal [source] As CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As Brush, ByVal foreBrush As Brush, ByVal alignToRight As Boolean)
        Dim value As Object = GetColumnValueAtRow([source], rowNum)
        Dim rect As Rectangle = bounds
        g.FillRectangle(backBrush, rect)
        rect.Offset(0, 2)
        rect.Height -= 2
        g.DrawString(value.ToString, Me.DataGridTableStyle.DataGrid.Font, foreBrush, RectangleF.FromLTRB(rect.X, rect.Y, rect.Right, rect.Bottom))
    End Sub

    Protected Overrides Sub SetDataGridInColumn(ByVal value As DataGrid)
        MyBase.SetDataGridInColumn(value)
        If comboBox.Parent IsNot Nothing Then
            comboBox.Parent.Controls.Remove(comboBox)
        End If
        If value IsNot Nothing Then
            value.Controls.Add(comboBox)
        End If
    End Sub

    Private Sub ComboBoxValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        Me.isEditing = True
        MyBase.ColumnStartedEditing(comboBox)
    End Sub
End Class
