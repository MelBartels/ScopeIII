Imports BartelsLibrary.DelegateSigs

Public Class UserCtrlObjectLibrary
    Inherits MVPUserCtrlBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnClearFilters As System.Windows.Forms.Button
    Friend WithEvents cmbBxSourceFilter As System.Windows.Forms.ComboBox
    Friend WithEvents lblSourceFilter As System.Windows.Forms.Label
    Friend WithEvents btnClosest As System.Windows.Forms.Button
    Friend WithEvents txBxNameFilter As System.Windows.Forms.TextBox
    Friend WithEvents lblCounts As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents dgvDatafiles As System.Windows.Forms.DataGridView
    Friend WithEvents UserCtrl2AxisCoord As ScopeIII.Forms.UserCtrl2AxisCoord
    Friend WithEvents DGVBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UserCtrlDatafile As ScopeIII.Forms.UserCtrlDatafile
    Friend WithEvents lblNameFilter As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.btnClearFilters = New System.Windows.Forms.Button
        Me.cmbBxSourceFilter = New System.Windows.Forms.ComboBox
        Me.lblSourceFilter = New System.Windows.Forms.Label
        Me.btnClosest = New System.Windows.Forms.Button
        Me.txBxNameFilter = New System.Windows.Forms.TextBox
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lblCounts = New System.Windows.Forms.Label
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.dgvDatafiles = New System.Windows.Forms.DataGridView
        Me.lblNameFilter = New System.Windows.Forms.Label
        Me.DGVBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.UserCtrlDatafile = New ScopeIII.Forms.UserCtrlDatafile
        Me.UserCtrl2AxisCoord = New ScopeIII.Forms.UserCtrl2AxisCoord
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDatafiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClearFilters
        '
        Me.btnClearFilters.Location = New System.Drawing.Point(98, 145)
        Me.btnClearFilters.Name = "btnClearFilters"
        Me.btnClearFilters.Size = New System.Drawing.Size(106, 23)
        Me.btnClearFilters.TabIndex = 103
        Me.btnClearFilters.Text = "Clear Filters"
        '
        'cmbBxSourceFilter
        '
        Me.cmbBxSourceFilter.Location = New System.Drawing.Point(98, 119)
        Me.cmbBxSourceFilter.Name = "cmbBxSourceFilter"
        Me.cmbBxSourceFilter.Size = New System.Drawing.Size(106, 21)
        Me.cmbBxSourceFilter.TabIndex = 102
        Me.cmbBxSourceFilter.Text = "Source Filter"
        '
        'lblSourceFilter
        '
        Me.lblSourceFilter.Location = New System.Drawing.Point(15, 119)
        Me.lblSourceFilter.Name = "lblSourceFilter"
        Me.lblSourceFilter.Size = New System.Drawing.Size(81, 23)
        Me.lblSourceFilter.TabIndex = 115
        Me.lblSourceFilter.Text = "Filter on Source"
        Me.lblSourceFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClosest
        '
        Me.btnClosest.Location = New System.Drawing.Point(224, 92)
        Me.btnClosest.Name = "btnClosest"
        Me.btnClosest.Size = New System.Drawing.Size(180, 23)
        Me.btnClosest.TabIndex = 106
        Me.btnClosest.Text = "Display 12 Objects Closest To..."
        '
        'txBxNameFilter
        '
        Me.txBxNameFilter.Location = New System.Drawing.Point(98, 95)
        Me.txBxNameFilter.Name = "txBxNameFilter"
        Me.txBxNameFilter.Size = New System.Drawing.Size(106, 20)
        Me.txBxNameFilter.TabIndex = 100
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'lblCounts
        '
        Me.lblCounts.Location = New System.Drawing.Point(411, 93)
        Me.lblCounts.Name = "lblCounts"
        Me.lblCounts.Size = New System.Drawing.Size(140, 23)
        Me.lblCounts.TabIndex = 112
        Me.lblCounts.Text = "file, object counts"
        Me.lblCounts.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'dgvDatafiles
        '
        Me.dgvDatafiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDatafiles.Location = New System.Drawing.Point(16, 174)
        Me.dgvDatafiles.Name = "dgvDatafiles"
        Me.dgvDatafiles.Size = New System.Drawing.Size(535, 351)
        Me.dgvDatafiles.TabIndex = 117
        '
        'lblNameFilter
        '
        Me.lblNameFilter.Location = New System.Drawing.Point(15, 95)
        Me.lblNameFilter.Name = "lblNameFilter"
        Me.lblNameFilter.Size = New System.Drawing.Size(81, 23)
        Me.lblNameFilter.TabIndex = 118
        Me.lblNameFilter.Text = "Filter on Name"
        Me.lblNameFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UserCtrlDatafile
        '
        Me.UserCtrlDatafile.EpochText = ""
        Me.UserCtrlDatafile.Location = New System.Drawing.Point(16, 0)
        Me.UserCtrlDatafile.Name = "UserCtrlDatafile"
        Me.UserCtrlDatafile.SelectedDatafiles = "(selected datafiles)"
        Me.UserCtrlDatafile.SelectedDirectory = "(selected directory)"
        Me.UserCtrlDatafile.Size = New System.Drawing.Size(447, 86)
        Me.UserCtrlDatafile.TabIndex = 119
        '
        'UserCtrl2AxisCoord
        '
        Me.UserCtrl2AxisCoord.Location = New System.Drawing.Point(224, 120)
        Me.UserCtrl2AxisCoord.Name = "UserCtrl2AxisCoord"
        Me.UserCtrl2AxisCoord.Size = New System.Drawing.Size(184, 48)
        Me.UserCtrl2AxisCoord.TabIndex = 116
        '
        'UserCtrlObjectLibrary
        '
        Me.Controls.Add(Me.lblCounts)
        Me.Controls.Add(Me.UserCtrlDatafile)
        Me.Controls.Add(Me.lblNameFilter)
        Me.Controls.Add(Me.dgvDatafiles)
        Me.Controls.Add(Me.UserCtrl2AxisCoord)
        Me.Controls.Add(Me.lblSourceFilter)
        Me.Controls.Add(Me.btnClosest)
        Me.Controls.Add(Me.txBxNameFilter)
        Me.Controls.Add(Me.btnClearFilters)
        Me.Controls.Add(Me.cmbBxSourceFilter)
        Me.Name = "UserCtrlObjectLibrary"
        Me.Size = New System.Drawing.Size(564, 538)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDatafiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event SourceFilterChanged()
    Public Event Closest()
    Public Event SortDatafiles(ByVal name As String)
    Public Event ObjectSelected(ByVal row As Int32)

    Private pDGVDatafileProperties As DGVDatafileProperties

    Public Property SourceFilterSelectedItem() As Object
        Get
            Return cmbBxSourceFilter.SelectedItem
        End Get
        Set(ByVal Value As Object)
            cmbBxSourceFilter.SelectedItem = Value
        End Set
    End Property

    Public Property SourceFilterDataSource() As Object
        Get
            Return cmbBxSourceFilter.DataSource
        End Get
        Set(ByVal Value As Object)
            setCmbBxSourceFilterDataSource(Value)
        End Set
    End Property

    Public Property DatafilesDataSource() As Object
        Get
            Return DGVBindingSource.DataSource
        End Get
        Set(ByVal Value As Object)
            setDataFilesDataSource(Value)
        End Set
    End Property

    Public Property Counts() As String
        Get
            Return lblCounts.Text
        End Get
        Set(ByVal Value As String)
            setCounts(Value)
        End Set
    End Property

    Public Property NameFilter() As String
        Get
            Return txBxNameFilter.Text
        End Get
        Set(ByVal Value As String)
            txBxNameFilter.Text = Value
        End Set
    End Property

    Public Sub SetToolTip()
        ToolTip.SetToolTip(txBxNameFilter, ScopeLibrary.Constants.ObjectNameFilter)
        ' tool tip doesn't display for dgvDatafiles
        ToolTip.IsBalloon = True
    End Sub

    Public Sub RefreshDataGridView()
        dgvDatafiles.Refresh()
    End Sub

    Private Sub setDataFilesDataSource(ByVal value As Object)
        If dgvDatafiles.InvokeRequired Then
            dgvDatafiles.Invoke(New DelegateObj(AddressOf setDataFilesDataSource), New Object() {value})
        Else
            DGVBindingSource.DataSource = value
            dgvDatafiles.DataSource = DGVBindingSource
            If value IsNot Nothing Then
                If dgvDatafiles.Columns IsNot Nothing AndAlso dgvDatafiles.Columns.Count > 0 Then
                    If pDGVDatafileProperties Is Nothing Then
                        pDGVDatafileProperties = Forms.DGVDatafileProperties.GetInstance
                    End If
                    pDGVDatafileProperties.SetProperties(dgvDatafiles)
                End If
                RefreshDataGridView()
            End If
        End If
    End Sub

    Private Sub setCounts(ByVal value As String)
        If lblCounts.InvokeRequired Then
            lblCounts.Invoke(New DelegateStr(AddressOf setCounts), New Object() {value})
        Else
            lblCounts.Text = value
        End If
    End Sub

    Private Sub setCmbBxSourceFilterDataSource(ByVal Value As Object)
        If cmbBxSourceFilter.InvokeRequired Then
            cmbBxSourceFilter.Invoke(New DelegateObj(AddressOf setCmbBxSourceFilterDataSource), New Object() {Value})
        Else
            cmbBxSourceFilter.DataSource = Value
        End If
    End Sub

    Private Sub UserCtrlObjectLibrary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnClosest.Text = "Display " & UserCtrlObjectLibraryPresenter.ResultsSize & " Objects Closest To..."
    End Sub

    Private Sub lblSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent SourceFilterChanged()
    End Sub

    Private Sub txBxNameFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txBxNameFilter.TextChanged
        RaiseEvent SourceFilterChanged()
    End Sub

    Private Sub cmbBxSourceFilterSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBxSourceFilter.SelectedIndexChanged
        RaiseEvent SourceFilterChanged()
    End Sub

    Private Sub btnClearFilters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters.Click
        txBxNameFilter.Text = String.Empty
        cmbBxSourceFilter.SelectedItem = ScopeLibrary.Constants.AllSources

        RaiseEvent SourceFilterChanged()
    End Sub

    Private Sub btnClosest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosest.Click
        RaiseEvent Closest()
    End Sub

    Private Sub dgvDatafiles_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDatafiles.MouseDown

        If dgvDatafiles.HitTest(e.X, e.Y).Type.Equals(DataGridViewHitTestType.ColumnHeader) Then
            Dim col As Int32 = dgvDatafiles.HitTest(e.X, e.Y).ColumnIndex
            RaiseEvent SortDatafiles(dgvDatafiles.Columns(col).Name)

        ElseIf dgvDatafiles.HitTest(e.X, e.Y).Type.Equals(DataGridViewHitTestType.Cell) Then
            Dim row As Int32 = dgvDatafiles.HitTest(e.X, e.Y).RowIndex
            RaiseEvent ObjectSelected(row)

        End If
    End Sub

    Private Sub setDgvDatafilesProperties()
        ' otherwise entry point for this thread needs to be marked as single threaded apartment thanks to ole
        dgvDatafiles.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable

        dgvDatafiles.Columns(DGVColumnNames.RA.Description).Visible = False
        dgvDatafiles.Columns(DGVColumnNames.Dec.Description).Visible = False
        dgvDatafiles.Columns(DGVColumnNames.RADisplay.Description).DisplayIndex = 0
        dgvDatafiles.Columns(DGVColumnNames.RADisplay.Description).HeaderText = CStr(DGVColumnNames.RADisplay.Tag)
        ' .Width is 110
        dgvDatafiles.Columns(DGVColumnNames.RADisplay.Description).Width = 115
        dgvDatafiles.Columns(DGVColumnNames.DecDisplay.Description).DisplayIndex = 1
        dgvDatafiles.Columns(DGVColumnNames.DecDisplay.Description).HeaderText = CStr(DGVColumnNames.DecDisplay.Tag)
        dgvDatafiles.Columns(DGVColumnNames.Name.Description).DisplayIndex = 2
        dgvDatafiles.Columns(DGVColumnNames.Name.Description).HeaderText = CStr(DGVColumnNames.Name.Tag)
        dgvDatafiles.Columns(DGVColumnNames.Name.Description).Width = 135
        dgvDatafiles.Columns(DGVColumnNames.Source.Description).DisplayIndex = 3
        dgvDatafiles.Columns(DGVColumnNames.Source.Description).HeaderText = CStr(DGVColumnNames.Source.Tag)

        ' runs very slowly on large amounts of data
        'dgvDatafiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        'dgvDatafiles.AutoResizeColumns()

    End Sub

End Class
