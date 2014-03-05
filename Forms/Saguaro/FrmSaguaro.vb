Imports BartelsLibrary.DelegateSigs

Public Class FrmSaguaro
    Inherits MVPViewBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSaveFile As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents btnLoadFile As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents BindingSource As System.Windows.Forms.BindingSource
    Public WithEvents txBxEpochTo As System.Windows.Forms.TextBox
    Friend WithEvents lblEpochTo As System.Windows.Forms.Label
    Public WithEvents txBxEpochFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblEpochFrom As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSaguaro))
        Me.btnCancel = New System.Windows.Forms.Button
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnSaveFile = New System.Windows.Forms.Button
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnLoadFile = New System.Windows.Forms.Button
        Me.txBxEpochFrom = New System.Windows.Forms.TextBox
        Me.lblEpochFrom = New System.Windows.Forms.Label
        Me.txBxEpochTo = New System.Windows.Forms.TextBox
        Me.lblEpochTo = New System.Windows.Forms.Label
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(734, 13)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 90
        Me.btnCancel.Text = "Cancel"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'btnSaveFile
        '
        Me.btnSaveFile.Location = New System.Drawing.Point(23, 42)
        Me.btnSaveFile.Name = "btnSaveFile"
        Me.btnSaveFile.Size = New System.Drawing.Size(138, 23)
        Me.btnSaveFile.TabIndex = 89
        Me.btnSaveFile.Text = "Save to 'Dat' File Format"
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(23, 83)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(786, 369)
        Me.DataGridView1.TabIndex = 91
        '
        'btnLoadFile
        '
        Me.btnLoadFile.Location = New System.Drawing.Point(23, 13)
        Me.btnLoadFile.Name = "btnLoadFile"
        Me.btnLoadFile.Size = New System.Drawing.Size(138, 23)
        Me.btnLoadFile.TabIndex = 92
        Me.btnLoadFile.Text = "Load Saguaro Datafile"
        Me.btnLoadFile.UseVisualStyleBackColor = True
        '
        'txBxEpochFrom
        '
        Me.txBxEpochFrom.Location = New System.Drawing.Point(308, 13)
        Me.txBxEpochFrom.Name = "txBxEpochFrom"
        Me.txBxEpochFrom.Size = New System.Drawing.Size(48, 20)
        Me.txBxEpochFrom.TabIndex = 116
        '
        'lblEpochFrom
        '
        Me.lblEpochFrom.Location = New System.Drawing.Point(167, 13)
        Me.lblEpochFrom.Name = "lblEpochFrom"
        Me.lblEpochFrom.Size = New System.Drawing.Size(133, 23)
        Me.lblEpochFrom.TabIndex = 117
        Me.lblEpochFrom.Text = "Epoch (Coordinates' Year)"
        Me.lblEpochFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txBxEpochTo
        '
        Me.txBxEpochTo.Location = New System.Drawing.Point(308, 42)
        Me.txBxEpochTo.Name = "txBxEpochTo"
        Me.txBxEpochTo.Size = New System.Drawing.Size(48, 20)
        Me.txBxEpochTo.TabIndex = 118
        '
        'lblEpochTo
        '
        Me.lblEpochTo.Location = New System.Drawing.Point(167, 42)
        Me.lblEpochTo.Name = "lblEpochTo"
        Me.lblEpochTo.Size = New System.Drawing.Size(133, 23)
        Me.lblEpochTo.TabIndex = 119
        Me.lblEpochTo.Text = "Epoch (Coordinates' Year)"
        Me.lblEpochTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmSaguaro
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(827, 464)
        Me.Controls.Add(Me.txBxEpochTo)
        Me.Controls.Add(Me.lblEpochTo)
        Me.Controls.Add(Me.txBxEpochFrom)
        Me.Controls.Add(Me.lblEpochFrom)
        Me.Controls.Add(Me.btnLoadFile)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSaveFile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmSaguaro"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII© View Saguaro Object Datafile"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event LoadFile()
    Public Event SaveFile()

    Private pDGVDatafileProperties As DGVDatafileProperties

    Public Property DataSource() As Object
        Get
            Return BindingSource.DataSource
        End Get
        Set(ByVal value As Object)
            setDataFilesDataSource(value)
        End Set
    End Property

    Public Property EpochFromText() As String
        Get
            Return txBxEpochFrom.Text
        End Get
        Set(ByVal Value As String)
            setEpochFromText(Value)
        End Set
    End Property

    Public Property EpochToText() As String
        Get
            Return txBxEpochTo.Text
        End Get
        Set(ByVal Value As String)
            setEpochToText(Value)
        End Set
    End Property

    Public Function ValidateEpochs() As Boolean
        Return ScopeIII.Forms.Validate.GetInstance.ValidateEpoch(_ErrorProvider, txBxEpochFrom) _
                AndAlso ScopeIII.Forms.Validate.GetInstance.ValidateEpoch(_ErrorProvider, txBxEpochTo)
    End Function

    Public Function EpochDeltaYr() As Double
        Return CDbl(EpochToText) - CDbl(EpochFromText)
    End Function

    Public Sub RefreshDataGridView()
        DataGridView1.Refresh()
    End Sub

    Private Sub setDataFilesDataSource(ByVal value As Object)
        If DataGridView1.InvokeRequired Then
            DataGridView1.Invoke(New DelegateObj(AddressOf setDataFilesDataSource), New Object() {value})
        Else
            BindingSource.DataSource = value
            DataGridView1.DataSource = BindingSource
            If value IsNot Nothing Then
                If DataGridView1.Columns IsNot Nothing AndAlso DataGridView1.Columns.Count > 0 Then
                    If pDGVDatafileProperties Is Nothing Then
                        pDGVDatafileProperties = Forms.DGVDatafileProperties.GetInstance
                    End If
                    pDGVDatafileProperties.SetProperties(DataGridView1)
                End If
                RefreshDataGridView()
            End If
        End If
    End Sub

    Private Sub btnLoadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadFile.Click
        If ValidateEpochs() Then
            RaiseEvent LoadFile()
        End If
    End Sub

    Private Sub btnSaveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFile.Click
        If ValidateEpochs() Then
            RaiseEvent SaveFile()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

    End Sub

    Private Sub setEpochFromText(ByVal value As String)
        If txBxEpochFrom.InvokeRequired Then
            txBxEpochFrom.Invoke(New DelegateStr(AddressOf setEpochFromText), New Object() {value})
        Else
            txBxEpochFrom.Text = value
        End If
    End Sub

    Private Sub setEpochToText(ByVal value As String)
        If txBxEpochTo.InvokeRequired Then
            txBxEpochTo.Invoke(New DelegateStr(AddressOf setEpochToText), New Object() {value})
        Else
            txBxEpochTo.Text = value
        End If
    End Sub
End Class
