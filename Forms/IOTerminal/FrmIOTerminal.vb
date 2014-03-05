Public Class FrmIOTerminal
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
    Friend WithEvents UserCtrlTerminal As ScopeIII.Forms.UserCtrlTerminal
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmIOTerminal))
        Me.btnQuit = New System.Windows.Forms.Button
        Me.UserCtrlTerminal = New ScopeIII.Forms.UserCtrlTerminal
        Me.SuspendLayout()
        '
        'btnQuit
        '
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuit.Location = New System.Drawing.Point(350, 304)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 5
        Me.btnQuit.Text = "Quit"
        '
        'UserCtrlTerminal
        '
        Me.UserCtrlTerminal.Location = New System.Drawing.Point(8, 8)
        Me.UserCtrlTerminal.Name = "UserCtrlTerminal"
        Me.UserCtrlTerminal.Size = New System.Drawing.Size(427, 292)
        Me.UserCtrlTerminal.TabIndex = 6
        '
        'FrmIOTerminal
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(433, 335)
        Me.Controls.Add(Me.UserCtrlTerminal)
        Me.Controls.Add(Me.btnQuit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmIOTerminal"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Terminal Window"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event CloseForm()

    Private Sub form_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        RaiseEvent CloseForm()
    End Sub

End Class
