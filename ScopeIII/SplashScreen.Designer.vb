<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
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
    Friend WithEvents ApplicationTitle As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Copyright As System.Windows.Forms.Label
    Friend WithEvents MainLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DetailsLayoutPanel As System.Windows.Forms.TableLayoutPanel

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreen))
        Me.MainLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.ApplicationTitle = New System.Windows.Forms.Label
        Me.DetailsLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Version = New System.Windows.Forms.Label
        Me.Copyright = New System.Windows.Forms.Label
        Me.PanelOK = New System.Windows.Forms.Panel
        Me.btnOK = New System.Windows.Forms.Button
        Me.MainLayoutPanel.SuspendLayout()
        Me.DetailsLayoutPanel.SuspendLayout()
        Me.PanelOK.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainLayoutPanel
        '
        Me.MainLayoutPanel.BackgroundImage = CType(resources.GetObject("MainLayoutPanel.BackgroundImage"), System.Drawing.Image)
        Me.MainLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MainLayoutPanel.ColumnCount = 2
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 217.0!))
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126.0!))
        Me.MainLayoutPanel.Controls.Add(Me.ApplicationTitle, 1, 0)
        Me.MainLayoutPanel.Controls.Add(Me.DetailsLayoutPanel, 1, 1)
        Me.MainLayoutPanel.Controls.Add(Me.PanelOK, 1, 2)
        Me.MainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainLayoutPanel.Name = "MainLayoutPanel"
        Me.MainLayoutPanel.RowCount = 1
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 206.0!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57.0!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18.0!))
        Me.MainLayoutPanel.Size = New System.Drawing.Size(496, 303)
        Me.MainLayoutPanel.TabIndex = 0
        '
        'ApplicationTitle
        '
        Me.ApplicationTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ApplicationTitle.BackColor = System.Drawing.Color.Transparent
        Me.ApplicationTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ApplicationTitle.Location = New System.Drawing.Point(220, 0)
        Me.ApplicationTitle.Name = "ApplicationTitle"
        Me.ApplicationTitle.Size = New System.Drawing.Size(272, 206)
        Me.ApplicationTitle.TabIndex = 0
        Me.ApplicationTitle.Text = "ScopeIII"
        Me.ApplicationTitle.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'DetailsLayoutPanel
        '
        Me.DetailsLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DetailsLayoutPanel.BackColor = System.Drawing.Color.Transparent
        Me.DetailsLayoutPanel.ColumnCount = 1
        Me.DetailsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267.0!))
        Me.DetailsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
        Me.DetailsLayoutPanel.Controls.Add(Me.Version, 0, 0)
        Me.DetailsLayoutPanel.Controls.Add(Me.Copyright, 0, 1)
        Me.DetailsLayoutPanel.Location = New System.Drawing.Point(223, 211)
        Me.DetailsLayoutPanel.Name = "DetailsLayoutPanel"
        Me.DetailsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.DetailsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.DetailsLayoutPanel.Size = New System.Drawing.Size(267, 47)
        Me.DetailsLayoutPanel.TabIndex = 1
        '
        'Version
        '
        Me.Version.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Version.BackColor = System.Drawing.Color.Transparent
        Me.Version.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.Location = New System.Drawing.Point(6, 1)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(255, 20)
        Me.Version.TabIndex = 1
        Me.Version.Text = "Version {0}.{1:00}"
        Me.Version.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Copyright
        '
        Me.Copyright.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Copyright.BackColor = System.Drawing.Color.Transparent
        Me.Copyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Copyright.Location = New System.Drawing.Point(7, 24)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New System.Drawing.Size(252, 21)
        Me.Copyright.TabIndex = 2
        Me.Copyright.Text = "Copyright"
        Me.Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelOK
        '
        Me.PanelOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PanelOK.BackColor = System.Drawing.Color.Transparent
        Me.PanelOK.Controls.Add(Me.btnOK)
        Me.PanelOK.Location = New System.Drawing.Point(224, 266)
        Me.PanelOK.Name = "PanelOK"
        Me.PanelOK.Size = New System.Drawing.Size(265, 34)
        Me.PanelOK.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnOK.Location = New System.Drawing.Point(182, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(496, 303)
        Me.ControlBox = False
        Me.Controls.Add(Me.MainLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SplashScreen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.MainLayoutPanel.ResumeLayout(False)
        Me.DetailsLayoutPanel.ResumeLayout(False)
        Me.PanelOK.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelOK As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button

End Class
