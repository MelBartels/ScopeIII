Imports BartelsLibrary.DelegateSigs

Public Class FrmPrecessDatafiles
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
    Friend WithEvents btnPrecess As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents pnlUserCtrlSource As System.Windows.Forms.Panel
    Friend WithEvents lblSource As System.Windows.Forms.Label
    Friend WithEvents lblDestination As System.Windows.Forms.Label
    Friend WithEvents pnlUserCtrlDestination As System.Windows.Forms.Panel
    Friend WithEvents lblDestinationDirectory As System.Windows.Forms.Label
    Public WithEvents txBxDestinationEpoch As System.Windows.Forms.TextBox
    Friend WithEvents lblEpoch As System.Windows.Forms.Label
    Public WithEvents btnDestinationDirectory As System.Windows.Forms.Button
    Friend WithEvents UserCtrlDatafile As ScopeIII.Forms.UserCtrlDatafile
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPrecessDatafiles))
        Me.btnCancel = New System.Windows.Forms.Button
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnPrecess = New System.Windows.Forms.Button
        Me.lblTitle = New System.Windows.Forms.Label
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlUserCtrlSource = New System.Windows.Forms.Panel
        Me.UserCtrlDatafile = New ScopeIII.Forms.UserCtrlDatafile
        Me.lblSource = New System.Windows.Forms.Label
        Me.lblDestination = New System.Windows.Forms.Label
        Me.pnlUserCtrlDestination = New System.Windows.Forms.Panel
        Me.lblDestinationDirectory = New System.Windows.Forms.Label
        Me.txBxDestinationEpoch = New System.Windows.Forms.TextBox
        Me.lblEpoch = New System.Windows.Forms.Label
        Me.btnDestinationDirectory = New System.Windows.Forms.Button
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUserCtrlSource.SuspendLayout()
        Me.pnlUserCtrlDestination.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(394, 253)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 90
        Me.btnCancel.Text = "Cancel"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'btnPrecess
        '
        Me.btnPrecess.Location = New System.Drawing.Point(298, 253)
        Me.btnPrecess.Name = "btnPrecess"
        Me.btnPrecess.Size = New System.Drawing.Size(75, 23)
        Me.btnPrecess.TabIndex = 89
        Me.btnPrecess.Text = "Precess"
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(44, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(392, 23)
        Me.lblTitle.TabIndex = 101
        Me.lblTitle.Text = "Precess a Directory of Datafiles or Precess Particular Datafiles"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'pnlUserCtrlSource
        '
        Me.pnlUserCtrlSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlUserCtrlSource.Controls.Add(Me.UserCtrlDatafile)
        Me.pnlUserCtrlSource.Location = New System.Drawing.Point(12, 56)
        Me.pnlUserCtrlSource.Name = "pnlUserCtrlSource"
        Me.pnlUserCtrlSource.Size = New System.Drawing.Size(458, 101)
        Me.pnlUserCtrlSource.TabIndex = 103
        '
        'UserCtrlDatafile
        '
        Me.UserCtrlDatafile.EpochText = ""
        Me.UserCtrlDatafile.Location = New System.Drawing.Point(3, 8)
        Me.UserCtrlDatafile.Name = "UserCtrlDatafile"
        Me.UserCtrlDatafile.SelectedDatafiles = "(selected datafiles)"
        Me.UserCtrlDatafile.SelectedDirectory = "(selected directory)"
        Me.UserCtrlDatafile.Size = New System.Drawing.Size(447, 86)
        Me.UserCtrlDatafile.TabIndex = 0
        '
        'lblSource
        '
        Me.lblSource.Location = New System.Drawing.Point(32, 48)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.Size = New System.Drawing.Size(45, 15)
        Me.lblSource.TabIndex = 104
        Me.lblSource.Text = "Source"
        '
        'lblDestination
        '
        Me.lblDestination.Location = New System.Drawing.Point(30, 171)
        Me.lblDestination.Name = "lblDestination"
        Me.lblDestination.Size = New System.Drawing.Size(64, 18)
        Me.lblDestination.TabIndex = 106
        Me.lblDestination.Text = "Destination"
        '
        'pnlUserCtrlDestination
        '
        Me.pnlUserCtrlDestination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlUserCtrlDestination.Controls.Add(Me.lblDestinationDirectory)
        Me.pnlUserCtrlDestination.Controls.Add(Me.txBxDestinationEpoch)
        Me.pnlUserCtrlDestination.Controls.Add(Me.lblEpoch)
        Me.pnlUserCtrlDestination.Controls.Add(Me.btnDestinationDirectory)
        Me.pnlUserCtrlDestination.Location = New System.Drawing.Point(12, 179)
        Me.pnlUserCtrlDestination.Name = "pnlUserCtrlDestination"
        Me.pnlUserCtrlDestination.Size = New System.Drawing.Size(458, 66)
        Me.pnlUserCtrlDestination.TabIndex = 105
        '
        'lblDestinationDirectory
        '
        Me.lblDestinationDirectory.AutoEllipsis = True
        Me.lblDestinationDirectory.Location = New System.Drawing.Point(184, 11)
        Me.lblDestinationDirectory.Name = "lblDestinationDirectory"
        Me.lblDestinationDirectory.Size = New System.Drawing.Size(260, 23)
        Me.lblDestinationDirectory.TabIndex = 120
        Me.lblDestinationDirectory.Text = "(destination directory)"
        Me.lblDestinationDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txBxDestinationEpoch
        '
        Me.txBxDestinationEpoch.Location = New System.Drawing.Point(187, 34)
        Me.txBxDestinationEpoch.Name = "txBxDestinationEpoch"
        Me.txBxDestinationEpoch.Size = New System.Drawing.Size(48, 20)
        Me.txBxDestinationEpoch.TabIndex = 118
        '
        'lblEpoch
        '
        Me.lblEpoch.AutoSize = True
        Me.lblEpoch.Location = New System.Drawing.Point(79, 37)
        Me.lblEpoch.Name = "lblEpoch"
        Me.lblEpoch.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblEpoch.Size = New System.Drawing.Size(94, 13)
        Me.lblEpoch.TabIndex = 119
        Me.lblEpoch.Text = "Destination Epoch"
        Me.lblEpoch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnDestinationDirectory
        '
        Me.btnDestinationDirectory.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnDestinationDirectory.Image = CType(resources.GetObject("btnDestinationDirectory.Image"), System.Drawing.Image)
        Me.btnDestinationDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDestinationDirectory.Location = New System.Drawing.Point(3, 11)
        Me.btnDestinationDirectory.Name = "btnDestinationDirectory"
        Me.btnDestinationDirectory.Size = New System.Drawing.Size(175, 23)
        Me.btnDestinationDirectory.TabIndex = 117
        Me.btnDestinationDirectory.Text = "Destination Directory"
        Me.btnDestinationDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDestinationDirectory.UseVisualStyleBackColor = False
        '
        'FrmPrecessDatafiles
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(481, 288)
        Me.Controls.Add(Me.lblDestination)
        Me.Controls.Add(Me.pnlUserCtrlDestination)
        Me.Controls.Add(Me.lblSource)
        Me.Controls.Add(Me.pnlUserCtrlSource)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnPrecess)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmPrecessDatafiles"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Precess Datafiles"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUserCtrlSource.ResumeLayout(False)
        Me.pnlUserCtrlDestination.ResumeLayout(False)
        Me.pnlUserCtrlDestination.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event DestinationDirectorySelected()
    Public Event Precess()
    Public Event Cancel()

    Public Property DestinationDirectory() As String
        Get
            Return lblDestinationDirectory.Text
        End Get
        Set(ByVal Value As String)
            setLblDestinationDirectory(Value)
        End Set
    End Property

    Public Property DestinationEpochText() As String
        Get
            Return txBxDestinationEpoch.Text
        End Get
        Set(ByVal Value As String)
            setDestinationEpochText(Value)
        End Set
    End Property

    Public Function ValidateDestinationEpoch() As Boolean
        Return ScopeIII.Forms.Validate.GetInstance.ValidateEpoch(_ErrorProvider, txBxDestinationEpoch)
    End Function

    Private Sub btnDestinationDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDestinationDirectory.Click
        RaiseEvent DestinationDirectorySelected()
    End Sub

    Private Sub setLblDestinationDirectory(ByVal value As String)
        If lblDestinationDirectory.InvokeRequired Then
            lblDestinationDirectory.Invoke(New DelegateStr(AddressOf setLblDestinationDirectory), New Object() {value})
        Else
            lblDestinationDirectory.Text = value
        End If
    End Sub

    Private Sub setDestinationEpochText(ByVal value As String)
        If txBxDestinationEpoch.InvokeRequired Then
            txBxDestinationEpoch.Invoke(New DelegateStr(AddressOf setDestinationEpochText), New Object() {value})
        Else
            txBxDestinationEpoch.Text = value
        End If
    End Sub

    Private Sub btnPrecess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrecess.Click
        RaiseEvent Precess()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent Cancel()
    End Sub

End Class
