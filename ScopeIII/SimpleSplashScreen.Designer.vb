<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SimpleSplashScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SimpleSplashScreen))
        Me.Copyright = New System.Windows.Forms.Label
        Me.ApplicationTitle = New System.Windows.Forms.Label
        Me.Version = New System.Windows.Forms.Label
        Me.pnlIcon = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'Copyright
        '
        Me.Copyright.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Copyright.BackColor = System.Drawing.Color.Transparent
        Me.Copyright.Location = New System.Drawing.Point(207, 10)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New System.Drawing.Size(142, 23)
        Me.Copyright.TabIndex = 5
        Me.Copyright.Text = "Copyright"
        Me.Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ApplicationTitle
        '
        Me.ApplicationTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ApplicationTitle.BackColor = System.Drawing.Color.Transparent
        Me.ApplicationTitle.Location = New System.Drawing.Point(39, 10)
        Me.ApplicationTitle.Name = "ApplicationTitle"
        Me.ApplicationTitle.Size = New System.Drawing.Size(60, 23)
        Me.ApplicationTitle.TabIndex = 3
        Me.ApplicationTitle.Text = "ScopeIII"
        Me.ApplicationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Version
        '
        Me.Version.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Version.BackColor = System.Drawing.Color.Transparent
        Me.Version.Location = New System.Drawing.Point(105, 10)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(96, 23)
        Me.Version.TabIndex = 4
        Me.Version.Text = "Version {0}.{1:00}"
        Me.Version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlIcon
        '
        Me.pnlIcon.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pnlIcon.BackgroundImage = CType(resources.GetObject("pnlIcon.BackgroundImage"), System.Drawing.Image)
        Me.pnlIcon.Location = New System.Drawing.Point(17, 14)
        Me.pnlIcon.Name = "pnlIcon"
        Me.pnlIcon.Size = New System.Drawing.Size(16, 16)
        Me.pnlIcon.TabIndex = 7
        '
        'SimpleSplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(366, 42)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlIcon)
        Me.Controls.Add(Me.Copyright)
        Me.Controls.Add(Me.ApplicationTitle)
        Me.Controls.Add(Me.Version)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SimpleSplashScreen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Copyright As System.Windows.Forms.Label
    Friend WithEvents ApplicationTitle As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents pnlIcon As System.Windows.Forms.Panel

End Class
