Public Class FrmAddText
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents btnAddText As System.Windows.Forms.Button
    Friend WithEvents txBxAddText As System.Windows.Forms.TextBox

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAddText))
        Me.btnAddText = New System.Windows.Forms.Button
        Me.txBxAddText = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btnAddText
        '
        Me.btnAddText.Location = New System.Drawing.Point(205, 12)
        Me.btnAddText.Name = "btnAddText"
        Me.btnAddText.Size = New System.Drawing.Size(75, 23)
        Me.btnAddText.TabIndex = 1
        Me.btnAddText.Text = "Add Text"
        Me.btnAddText.UseVisualStyleBackColor = True
        '
        'txBxAddText
        '
        Me.txBxAddText.Location = New System.Drawing.Point(12, 12)
        Me.txBxAddText.Name = "txBxAddText"
        Me.txBxAddText.Size = New System.Drawing.Size(175, 20)
        Me.txBxAddText.TabIndex = 2
        Me.txBxAddText.Text = "type some text here"
        '
        'FrmAddText
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 46)
        Me.Controls.Add(Me.txBxAddText)
        Me.Controls.Add(Me.btnAddText)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmAddText"
        Me.Text = "FormAddText"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AddText(ByVal textToAdd As String)

    Private Sub btnAddText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddText.Click
        RaiseEvent AddText(txBxAddText.Text)
    End Sub
End Class
