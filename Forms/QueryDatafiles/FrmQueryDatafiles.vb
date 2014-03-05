Public Class FrmQueryDatafiles
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
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txBxFilename As System.Windows.Forms.TextBox
    Friend WithEvents UserCtrlObjectLibrary As ScopeIII.Forms.UserCtrlObjectLibrary
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmQueryDatafiles))
        Me.btnQuit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.txBxFilename = New System.Windows.Forms.TextBox
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.UserCtrlObjectLibrary = New ScopeIII.Forms.UserCtrlObjectLibrary
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnQuit
        '
        Me.btnQuit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuit.Location = New System.Drawing.Point(484, 5)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(66, 23)
        Me.btnQuit.TabIndex = 13
        Me.btnQuit.Text = "Quit"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(428, 119)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(122, 23)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "Save All Objects To ..."
        '
        'txBxFilename
        '
        Me.txBxFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txBxFilename.Location = New System.Drawing.Point(428, 146)
        Me.txBxFilename.Name = "txBxFilename"
        Me.txBxFilename.Size = New System.Drawing.Size(122, 20)
        Me.txBxFilename.TabIndex = 14
        Me.txBxFilename.Text = "library.dat"
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'UserCtrlObjectLibrary
        '
        Me.UserCtrlObjectLibrary.Counts = "file, object counts"
        Me.UserCtrlObjectLibrary.DatafilesDataSource = Nothing
        Me.UserCtrlObjectLibrary.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlObjectLibrary.Name = "UserCtrlObjectLibrary"
        Me.UserCtrlObjectLibrary.NameFilter = ""
        Me.UserCtrlObjectLibrary.Size = New System.Drawing.Size(565, 539)
        Me.UserCtrlObjectLibrary.SourceFilterDataSource = Nothing
        Me.UserCtrlObjectLibrary.SourceFilterSelectedItem = Nothing
        Me.UserCtrlObjectLibrary.TabIndex = 16
        '
        'FrmQueryDatafiles
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnQuit
        Me.ClientSize = New System.Drawing.Size(562, 540)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txBxFilename)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.UserCtrlObjectLibrary)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmQueryDatafiles"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Query Datafiles"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event Save()

    Public Property Filename() As String
        Get
            Return txBxFilename.Text
        End Get
        Set(ByVal Value As String)
            txBxFilename.Text = Value
        End Set
    End Property

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        RaiseEvent Save()
    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

End Class
