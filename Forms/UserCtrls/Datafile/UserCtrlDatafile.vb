Imports BartelsLibrary.DelegateSigs

Public Class UserCtrlDatafile
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
    Public WithEvents btnDatafiles As System.Windows.Forms.Button
    Public WithEvents btnDirectory As System.Windows.Forms.Button
    Public WithEvents txBxEpoch As System.Windows.Forms.TextBox
    Friend WithEvents lblEpoch As System.Windows.Forms.Label
    Friend WithEvents lblSelectedDirectory As System.Windows.Forms.Label
    Friend WithEvents lblSelectedDatafile As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserCtrlDatafile))
        Me.btnDatafiles = New System.Windows.Forms.Button
        Me.btnDirectory = New System.Windows.Forms.Button
        Me.txBxEpoch = New System.Windows.Forms.TextBox
        Me.lblEpoch = New System.Windows.Forms.Label
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lblSelectedDirectory = New System.Windows.Forms.Label
        Me.lblSelectedDatafile = New System.Windows.Forms.Label
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDatafiles
        '
        Me.btnDatafiles.Image = CType(resources.GetObject("btnDatafiles.Image"), System.Drawing.Image)
        Me.btnDatafiles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDatafiles.Location = New System.Drawing.Point(3, 34)
        Me.btnDatafiles.Name = "btnDatafiles"
        Me.btnDatafiles.Size = New System.Drawing.Size(175, 23)
        Me.btnDatafiles.TabIndex = 109
        Me.btnDatafiles.Text = "or Select Individual Datafiles"
        Me.btnDatafiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDatafiles.UseVisualStyleBackColor = False
        '
        'btnDirectory
        '
        Me.btnDirectory.Image = CType(resources.GetObject("btnDirectory.Image"), System.Drawing.Image)
        Me.btnDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDirectory.Location = New System.Drawing.Point(3, 5)
        Me.btnDirectory.Name = "btnDirectory"
        Me.btnDirectory.Size = New System.Drawing.Size(175, 23)
        Me.btnDirectory.TabIndex = 108
        Me.btnDirectory.Text = "Select a Directory of Datafiles"
        Me.btnDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDirectory.UseVisualStyleBackColor = False
        '
        'txBxEpoch
        '
        Me.txBxEpoch.Location = New System.Drawing.Point(187, 60)
        Me.txBxEpoch.Name = "txBxEpoch"
        Me.txBxEpoch.Size = New System.Drawing.Size(48, 20)
        Me.txBxEpoch.TabIndex = 114
        '
        'lblEpoch
        '
        Me.lblEpoch.AutoSize = True
        Me.lblEpoch.Location = New System.Drawing.Point(79, 63)
        Me.lblEpoch.Name = "lblEpoch"
        Me.lblEpoch.Size = New System.Drawing.Size(99, 13)
        Me.lblEpoch.TabIndex = 115
        Me.lblEpoch.Text = "Coordinates' Epoch"
        Me.lblEpoch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'lblSelectedDirectory
        '
        Me.lblSelectedDirectory.AutoEllipsis = True
        Me.lblSelectedDirectory.Location = New System.Drawing.Point(184, 5)
        Me.lblSelectedDirectory.Name = "lblSelectedDirectory"
        Me.lblSelectedDirectory.Size = New System.Drawing.Size(260, 23)
        Me.lblSelectedDirectory.TabIndex = 116
        Me.lblSelectedDirectory.Text = "(selected directory)"
        Me.lblSelectedDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelectedDatafile
        '
        Me.lblSelectedDatafile.AutoEllipsis = True
        Me.lblSelectedDatafile.Location = New System.Drawing.Point(184, 34)
        Me.lblSelectedDatafile.Name = "lblSelectedDatafile"
        Me.lblSelectedDatafile.Size = New System.Drawing.Size(260, 23)
        Me.lblSelectedDatafile.TabIndex = 117
        Me.lblSelectedDatafile.Text = "(selected datafiles)"
        Me.lblSelectedDatafile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'UserCtrlDatafile
        '
        Me.Controls.Add(Me.lblSelectedDatafile)
        Me.Controls.Add(Me.lblSelectedDirectory)
        Me.Controls.Add(Me.txBxEpoch)
        Me.Controls.Add(Me.lblEpoch)
        Me.Controls.Add(Me.btnDatafiles)
        Me.Controls.Add(Me.btnDirectory)
        Me.Name = "UserCtrlDatafile"
        Me.Size = New System.Drawing.Size(447, 86)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event DirectorySelected()
    Public Event DatafilesSelected()

    Public Property SelectedDirectory() As String
        Get
            Return lblSelectedDirectory.Text
        End Get
        Set(ByVal Value As String)
            setLblSelectedDirectory(Value)
        End Set
    End Property

    Public Property SelectedDatafiles() As String
        Get
            Return lblSelectedDatafile.Text
        End Get
        Set(ByVal Value As String)
            setLblSelectedDatafiles(Value)
        End Set
    End Property

    Public Property EpochText() As String
        Get
            Return txBxEpoch.Text
        End Get
        Set(ByVal Value As String)
            setEpochText(Value)
        End Set
    End Property

    Public Function ValidateEpoch() As Boolean
        Return ScopeIII.Forms.Validate.GetInstance.ValidateEpoch(_ErrorProvider, txBxEpoch)
    End Function

    Private Sub btnDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDirectory.Click
        RaiseEvent DirectorySelected()
    End Sub

    Private Sub btnDatafiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDatafiles.Click
        RaiseEvent DatafilesSelected()
    End Sub

    Private Sub setLblSelectedDirectory(ByVal value As String)
        If lblSelectedDirectory.InvokeRequired Then
            lblSelectedDirectory.Invoke(New DelegateStr(AddressOf setLblSelectedDirectory), New Object() {value})
        Else
            lblSelectedDirectory.Text = value
        End If
    End Sub

    Private Sub setLblSelectedDatafiles(ByVal value As String)
        If lblSelectedDatafile.InvokeRequired Then
            lblSelectedDatafile.Invoke(New DelegateStr(AddressOf setLblSelectedDatafiles), New Object() {value})
        Else
            lblSelectedDatafile.Text = value
        End If
    End Sub

    Private Sub setEpochText(ByVal value As String)
        If txBxEpoch.InvokeRequired Then
            txBxEpoch.Invoke(New DelegateStr(AddressOf setEpochText), New Object() {value})
        Else
            txBxEpoch.Text = value
        End If
    End Sub

End Class
