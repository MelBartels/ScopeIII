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
    Friend WithEvents UserCtrlDatafileSource As ScopeIII.Forms.UserCtrlDatafile
    Friend WithEvents pnlUserCtrlSource As System.Windows.Forms.Panel
    Friend WithEvents lblSource As System.Windows.Forms.Label
    Friend WithEvents lblDestination As System.Windows.Forms.Label
    Friend WithEvents pnlUserCtrlDestination As System.Windows.Forms.Panel
    Friend WithEvents UserCtrlDatafileDestination As ScopeIII.Forms.UserCtrlDatafile
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPrecessDatafiles))
        Me.btnCancel = New System.Windows.Forms.Button
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnPrecess = New System.Windows.Forms.Button
        Me.lblTitle = New System.Windows.Forms.Label
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.UserCtrlDatafileSource = New ScopeIII.Forms.UserCtrlDatafile
        Me.pnlUserCtrlSource = New System.Windows.Forms.Panel
        Me.lblSource = New System.Windows.Forms.Label
        Me.lblDestination = New System.Windows.Forms.Label
        Me.pnlUserCtrlDestination = New System.Windows.Forms.Panel
        Me.UserCtrlDatafileDestination = New ScopeIII.Forms.UserCtrlDatafile
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUserCtrlSource.SuspendLayout()
        Me.pnlUserCtrlDestination.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(640, 200)
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
        Me.btnPrecess.Location = New System.Drawing.Point(544, 200)
        Me.btnPrecess.Name = "btnPrecess"
        Me.btnPrecess.Size = New System.Drawing.Size(75, 23)
        Me.btnPrecess.TabIndex = 89
        Me.btnPrecess.Text = "Precess"
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(176, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(392, 23)
        Me.lblTitle.TabIndex = 101
        Me.lblTitle.Text = "Precess a Directory of Datafiles, or, Precess a Particular Datafile"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'UserCtrlDatafileSource
        '
        Me.UserCtrlDatafileSource.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.UserCtrlDatafileSource.EpochText = ""
        Me.UserCtrlDatafileSource.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrlDatafileSource.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlDatafileSource.Name = "UserCtrlDatafileSource"
        Me.UserCtrlDatafileSource.Size = New System.Drawing.Size(336, 160)
        Me.UserCtrlDatafileSource.SourceText = ""
        Me.UserCtrlDatafileSource.TabIndex = 102
        '
        'pnlUserCtrlSource
        '
        Me.pnlUserCtrlSource.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pnlUserCtrlSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlUserCtrlSource.Controls.Add(Me.UserCtrlDatafileSource)
        Me.pnlUserCtrlSource.Location = New System.Drawing.Point(24, 56)
        Me.pnlUserCtrlSource.Name = "pnlUserCtrlSource"
        Me.pnlUserCtrlSource.Size = New System.Drawing.Size(336, 124)
        Me.pnlUserCtrlSource.TabIndex = 103
        '
        'lblSource
        '
        Me.lblSource.Location = New System.Drawing.Point(32, 48)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.Size = New System.Drawing.Size(45, 23)
        Me.lblSource.TabIndex = 104
        Me.lblSource.Text = "Source"
        '
        'lblDestination
        '
        Me.lblDestination.Location = New System.Drawing.Point(392, 48)
        Me.lblDestination.Name = "lblDestination"
        Me.lblDestination.Size = New System.Drawing.Size(64, 23)
        Me.lblDestination.TabIndex = 106
        Me.lblDestination.Text = "Destination"
        '
        'pnlUserCtrlDestination
        '
        Me.pnlUserCtrlDestination.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pnlUserCtrlDestination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlUserCtrlDestination.Controls.Add(Me.UserCtrlDatafileDestination)
        Me.pnlUserCtrlDestination.Location = New System.Drawing.Point(384, 56)
        Me.pnlUserCtrlDestination.Name = "pnlUserCtrlDestination"
        Me.pnlUserCtrlDestination.Size = New System.Drawing.Size(336, 124)
        Me.pnlUserCtrlDestination.TabIndex = 105
        '
        'UserCtrlDatafileDestination
        '
        Me.UserCtrlDatafileDestination.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.UserCtrlDatafileDestination.EpochText = ""
        Me.UserCtrlDatafileDestination.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrlDatafileDestination.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlDatafileDestination.Name = "UserCtrlDatafileDestination"
        Me.UserCtrlDatafileDestination.Size = New System.Drawing.Size(336, 160)
        Me.UserCtrlDatafileDestination.SourceText = ""
        Me.UserCtrlDatafileDestination.TabIndex = 102
        '
        'FrmPrecessDatafiles
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(742, 236)
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
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Overrides Function DefaultPresenter() As IMVPPresenter
        Return initDefaultPresenter(PrecessDatafilesPresenter.getinstance)
    End Function

    Public Event Precess()
    Public Event Cancel()

    Private Sub btnPrecess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrecess.Click
        RaiseEvent Precess()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent Cancel()
    End Sub
End Class
