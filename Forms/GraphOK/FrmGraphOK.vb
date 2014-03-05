Public Class FrmGraphOK
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
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents MVPUserCtrlGraphics As BartelsLibrary.MVPUserCtrlGraphics
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGraphOK))
        Me.btnOK = New System.Windows.Forms.Button
        Me.MVPUserCtrlGraphics = New BartelsLibrary.MVPUserCtrlGraphics
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnOK.Location = New System.Drawing.Point(200, 232)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'MVPUserCtrlGraphics
        '
        Me.MVPUserCtrlGraphics.IRenderer = Nothing
        Me.MVPUserCtrlGraphics.Location = New System.Drawing.Point(0, 0)
        Me.MVPUserCtrlGraphics.Name = "MVPUserCtrlGraphics"
        Me.MVPUserCtrlGraphics.Size = New System.Drawing.Size(150, 150)
        Me.MVPUserCtrlGraphics.TabIndex = 1
        '
        'FrmGraphOK
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.MVPUserCtrlGraphics)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmGraphOK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Graph"
        Me.TopMost = False
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected Const Border As Int32 = 12

    Public Property FormTitle() As String
        Get
            Return Text
        End Get
        Set(ByVal Value As String)
            Text = Value
        End Set
    End Property

    Private Sub FrmGraphOK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        resizer(sender, e)
    End Sub

    Protected Overridable Sub resizer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        If MVPUserCtrlGraphics IsNot Nothing Then
            Dim h As Int32 = btnOK.Location.Y - Border
            If h < 1 Then
                h = 1
            End If
            MVPUserCtrlGraphics.Size = New Drawing.Size(Width, h)
        End If
    End Sub

End Class
