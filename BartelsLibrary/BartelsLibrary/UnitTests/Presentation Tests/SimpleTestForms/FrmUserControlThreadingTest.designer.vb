<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserControlThreadingTest
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUserControlThreadingTest))
        Me.btnMainThread = New System.Windows.Forms.Button
        Me.btnCtrlThread = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnMainThread
        '
        Me.btnMainThread.Location = New System.Drawing.Point(46, 120)
        Me.btnMainThread.Name = "btnMainThread"
        Me.btnMainThread.Size = New System.Drawing.Size(169, 41)
        Me.btnMainThread.TabIndex = 1
        Me.btnMainThread.Text = "Add User Control (Main Thread) - Throws Exception"
        Me.btnMainThread.UseVisualStyleBackColor = True
        '
        'btnCtrlThread
        '
        Me.btnCtrlThread.Location = New System.Drawing.Point(46, 74)
        Me.btnCtrlThread.Name = "btnCtrlThread"
        Me.btnCtrlThread.Size = New System.Drawing.Size(169, 40)
        Me.btnCtrlThread.TabIndex = 2
        Me.btnCtrlThread.Text = "Add Using Control Thread"
        Me.btnCtrlThread.UseVisualStyleBackColor = True
        '
        'FrmUserControlThreadingTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(260, 188)
        Me.Controls.Add(Me.btnCtrlThread)
        Me.Controls.Add(Me.btnMainThread)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmUserControlThreadingTest"
        Me.Text = "UserControlThreadingTest"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnMainThread As System.Windows.Forms.Button
    Friend WithEvents btnCtrlThread As System.Windows.Forms.Button
End Class
