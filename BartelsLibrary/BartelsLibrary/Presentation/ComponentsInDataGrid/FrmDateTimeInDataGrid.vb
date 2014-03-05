Imports NUnit.Framework
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class FrmDateTimeInDataGrid
    Inherits Form

    Private namesDataTable As DataTable
    Private myGrid As DataGrid = New DataGrid

    Public Sub New()
        InitForm()

        namesDataTable = New DataTable("NamesTable")

        namesDataTable.Columns.Add(New DataColumn("Name"))

        Dim dateColumn As DataColumn = New DataColumn("Date", GetType(DateTime))
        namesDataTable.Columns.Add(dateColumn)

        Dim namesDataSet As DataSet = New DataSet
        namesDataSet.Tables.Add(namesDataTable)

        myGrid.DataSource = namesDataSet
        myGrid.DataMember = "NamesTable"

        AddGridStyle()
        AddData()
    End Sub

    Private Sub AddGridStyle()
        Dim myGridStyle As DataGridTableStyle = New DataGridTableStyle
        myGridStyle.MappingName = "NamesTable"

        Dim nameColumnStyle As DataGridTextBoxColumn = New DataGridTextBoxColumn
        nameColumnStyle.MappingName = "Name"
        nameColumnStyle.HeaderText = "Name"
        myGridStyle.GridColumnStyles.Add(nameColumnStyle)

        Dim timePickerColumnStyle As DataGridTimePickerColumn = New DataGridTimePickerColumn
        timePickerColumnStyle.MappingName = "Date"
        timePickerColumnStyle.HeaderText = "Date"
        timePickerColumnStyle.Width = 100
        myGridStyle.GridColumnStyles.Add(timePickerColumnStyle)

        myGrid.TableStyles.Add(myGridStyle)
    End Sub

    Private Sub AddData()
        Dim dRow As DataRow = namesDataTable.NewRow()
        dRow("Name") = "Name 1"
        dRow("Date") = New DateTime(2001, 12, 1)
        namesDataTable.Rows.Add(dRow)

        dRow = namesDataTable.NewRow()
        dRow("Name") = "Name 2"
        dRow("Date") = New DateTime(2001, 12, 4)
        namesDataTable.Rows.Add(dRow)

        dRow = namesDataTable.NewRow()
        dRow("Name") = "Name 3"
        dRow("Date") = New DateTime(2001, 12, 29)
        namesDataTable.Rows.Add(dRow)

        dRow = namesDataTable.NewRow()
        dRow("Name") = "Name 4"
        dRow("Date") = New DateTime(2001, 12, 13)
        namesDataTable.Rows.Add(dRow)

        dRow = namesDataTable.NewRow()
        dRow("Name") = "Name 5"
        dRow("Date") = New DateTime(2001, 12, 21)
        namesDataTable.Rows.Add(dRow)

        namesDataTable.AcceptChanges()
    End Sub

    Private Sub InitForm()
        Me.Size = New Size(500, 500)
        myGrid.Size = New Size(350, 250)
        myGrid.TabStop = True
        myGrid.TabIndex = 1
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Controls.Add(myGrid)
    End Sub

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDateTimeInDataGrid))
        Me.SuspendLayout()
        '
        'FrmDateTimeInDataGrid
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmDateTimeInDataGrid"
        Me.ResumeLayout(False)

    End Sub
End Class
