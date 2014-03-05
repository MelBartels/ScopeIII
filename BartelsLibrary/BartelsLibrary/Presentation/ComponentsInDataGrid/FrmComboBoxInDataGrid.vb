Imports NUnit.Framework
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class FrmComboBoxInDataGrid
    Inherits Form

    Public Enum ComboBoxData
        A
        B
        C
    End Enum

    Private namesDataTable As DataTable
    Private myGrid As DataGrid = New DataGrid

    Public Sub New()
        InitForm()

        namesDataTable = New DataTable("NamesTable")

        namesDataTable.Columns.Add(New DataColumn("Name"))

        ' ...as type 'Object' so as to swing between ComboBox and enum
        Dim comboBoxColumn As DataColumn = New DataColumn("ComboBox", GetType(Object))

        namesDataTable.Columns.Add(comboBoxColumn)

        Dim namesDataSet As DataSet = New DataSet
        namesDataSet.Tables.Add(namesDataTable)
        myGrid.DataSource = namesDataSet
        myGrid.DataMember = "NamesTable"

        AddGridStyle()
        AddDataComboBox()
    End Sub

    Private Sub AddGridStyle()
        Dim myGridStyle As DataGridTableStyle = New DataGridTableStyle
        myGridStyle.MappingName = "NamesTable"

        Dim nameColumnStyle As DataGridTextBoxColumn = New DataGridTextBoxColumn
        nameColumnStyle.MappingName = "Name"
        nameColumnStyle.HeaderText = "Name"
        myGridStyle.GridColumnStyles.Add(nameColumnStyle)

        Dim comboBoxColumnStyle As DataGridComboBoxColumn = New DataGridComboBoxColumn
        comboBoxColumnStyle.MappingName = "ComboBox"
        comboBoxColumnStyle.HeaderText = "ComboBox"
        comboBoxColumnStyle.Width = 100
        myGridStyle.GridColumnStyles.Add(comboBoxColumnStyle)

        myGrid.TableStyles.Add(myGridStyle)
    End Sub

    Private Sub AddDataComboBox()
        Dim dRow As DataRow = namesDataTable.NewRow()
        dRow("Name") = "Name 1"
        dRow("ComboBox") = ComboBoxData.A
        namesDataTable.Rows.Add(dRow)

        dRow = namesDataTable.NewRow()
        dRow("Name") = "Name 2"
        dRow("ComboBox") = ComboBoxData.B
        namesDataTable.Rows.Add(dRow)

        dRow = namesDataTable.NewRow()
        dRow("Name") = "Name 3"
        dRow("ComboBox") = ComboBoxData.C
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmComboBoxInDataGrid))
        Me.SuspendLayout()
        '
        'FrmComboBoxInDataGrid
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmComboBoxInDataGrid"
        Me.ResumeLayout(False)

    End Sub
End Class
