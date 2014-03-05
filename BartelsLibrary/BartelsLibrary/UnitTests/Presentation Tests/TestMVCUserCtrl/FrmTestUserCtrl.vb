Public Class FrmTestUserCtrl
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        xMargin = Width - TestUserCtrl1.Width
        yMargin = Height - TestUserCtrl1.Height

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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TestUserCtrl1 As BartelsLibrary.TestUserCtrl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTestUserCtrl))
        Me.Button1 = New System.Windows.Forms.Button
        Me.TestUserCtrl1 = New BartelsLibrary.TestUserCtrl
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(216, 320)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "OK"
        '
        'TestUserCtrl1
        '
        Me.TestUserCtrl1.Location = New System.Drawing.Point(0, 8)
        Me.TestUserCtrl1.Name = "TestUserCtrl1"
        Me.TestUserCtrl1.Size = New System.Drawing.Size(300, 300)
        Me.TestUserCtrl1.TabIndex = 2
        '
        'FrmTestUserCtrl
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(304, 350)
        Me.Controls.Add(Me.TestUserCtrl1)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmTestUserCtrl"
        Me.Text = "FrmTestUserCtrl"
        Me.ResumeLayout(False)

    End Sub

#End Region

    ' following put here for NUnit testing purposes: normally found in the form's controller

    Public TestUserCtrlController As TestUserCtrlController
    Public WithEvents FrmTestUserCtrl As frmTestUserCtrl
    Private xMargin As Int32
    Private yMargin As Int32

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub resizer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        If TestUserCtrlController IsNot Nothing Then
            Dim width As Int32 = CType(sender, FrmTestUserCtrl).Width - xMargin
            Dim height As Int32 = CType(sender, FrmTestUserCtrl).Height - yMargin
            TestUserCtrlController.GraphicsSize = New Drawing.Size(width, height)
        End If
    End Sub

End Class
