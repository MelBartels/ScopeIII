<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDataFileConverter
    Inherits BartelsLibrary.MVPViewBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDataFileConverter))
        Me.btnQuit = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.lblExplain = New System.Windows.Forms.Label
        Me.txBxDisplay = New System.Windows.Forms.TextBox
        Me.btnSelectFile = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnQuit
        '
        Me.btnQuit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuit.Location = New System.Drawing.Point(269, 12)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 7
        Me.btnQuit.Text = "Quit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'lblExplain
        '
        Me.lblExplain.AutoSize = True
        Me.lblExplain.Location = New System.Drawing.Point(12, 52)
        Me.lblExplain.Name = "lblExplain"
        Me.lblExplain.Size = New System.Drawing.Size(40, 13)
        Me.lblExplain.TabIndex = 6
        Me.lblExplain.Text = "explain"
        '
        'txBxDisplay
        '
        Me.txBxDisplay.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txBxDisplay.Location = New System.Drawing.Point(15, 189)
        Me.txBxDisplay.Multiline = True
        Me.txBxDisplay.Name = "txBxDisplay"
        Me.txBxDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txBxDisplay.Size = New System.Drawing.Size(329, 137)
        Me.txBxDisplay.TabIndex = 5
        '
        'btnSelectFile
        '
        Me.btnSelectFile.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnSelectFile.Location = New System.Drawing.Point(12, 12)
        Me.btnSelectFile.Name = "btnSelectFile"
        Me.btnSelectFile.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectFile.TabIndex = 4
        Me.btnSelectFile.Text = "Select File"
        Me.btnSelectFile.UseVisualStyleBackColor = True
        '
        'FrmDataFileConverter
        '
        Me.CancelButton = Me.btnQuit
        Me.ClientSize = New System.Drawing.Size(356, 338)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.lblExplain)
        Me.Controls.Add(Me.txBxDisplay)
        Me.Controls.Add(Me.btnSelectFile)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmDataFileConverter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DataFileConverter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblExplain As System.Windows.Forms.Label
    Friend WithEvents txBxDisplay As System.Windows.Forms.TextBox
    Friend WithEvents btnSelectFile As System.Windows.Forms.Button

End Class
