Public Class FrmTestMVPUserCtrl
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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TestMVPUserCtrlGraphics1 As BartelsLibrary.TestMVPUserCtrlGraphics
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTestMVPUserCtrl))
        Me.Button1 = New System.Windows.Forms.Button
        Me.TestMVPUserCtrlGraphics1 = New BartelsLibrary.TestMVPUserCtrlGraphics
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(64, 160)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "OK"
        '
        'TestMVPUserCtrlGraphics1
        '
        Me.TestMVPUserCtrlGraphics1.IRenderer = Nothing
        Me.TestMVPUserCtrlGraphics1.Location = New System.Drawing.Point(0, 0)
        Me.TestMVPUserCtrlGraphics1.Name = "TestMVPUserCtrlGraphics1"
        Me.TestMVPUserCtrlGraphics1.Size = New System.Drawing.Size(150, 150)
        Me.TestMVPUserCtrlGraphics1.TabIndex = 2
        '
        'FrmTestMVPUserCtrl
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(152, 190)
        Me.Controls.Add(Me.TestMVPUserCtrlGraphics1)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmTestMVPUserCtrl"
        Me.Text = "FrmTestMVPUserCtrlGraphics"
        Me.ResumeLayout(False)

    End Sub

#End Region

    ' calculated manually by inspecting the form's properties
    Dim xMargin As Int32 = 10
    Dim yMargin As Int32 = 74

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub newSize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Dim newSize As New Drawing.Size(Me.Width - xMargin, Me.Height - yMargin)
        If newSize.Width < 1 Then
            newSize.Width = 1
        End If
        If newSize.Height < 1 Then
            newSize.Height = 1
        End If
        TestMVPUserCtrlGraphics1.Size = newSize
    End Sub

End Class
