<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControlInvokeTest
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.ShowText = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ShowText
        '
        Me.ShowText.Location = New System.Drawing.Point(46, 13)
        Me.ShowText.Name = "ShowText"
        Me.ShowText.Size = New System.Drawing.Size(169, 40)
        Me.ShowText.TabIndex = 1
        Me.ShowText.Text = "UserControlButton"
        Me.ShowText.UseVisualStyleBackColor = True
        '
        'UserControlTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ShowText)
        Me.Name = "UserControlTest"
        Me.Size = New System.Drawing.Size(259, 67)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ShowText As System.Windows.Forms.Button

End Class
