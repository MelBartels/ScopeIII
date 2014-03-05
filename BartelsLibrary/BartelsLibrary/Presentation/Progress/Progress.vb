Imports BartelsLibrary.DelegateSigs

Public Class Progress
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Public WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Public WithEvents btnCancel As System.Windows.Forms.Button
    Public WithEvents txBx As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txBx = New System.Windows.Forms.TextBox
        Me.ProgressBar = New System.Windows.Forms.ProgressBar
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txBx
        '
        Me.txBx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txBx.BackColor = System.Drawing.Color.White
        Me.txBx.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txBx.Location = New System.Drawing.Point(0, 0)
        Me.txBx.Multiline = True
        Me.txBx.Name = "txBx"
        Me.txBx.ReadOnly = True
        Me.txBx.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txBx.Size = New System.Drawing.Size(576, 260)
        Me.txBx.TabIndex = 0
        Me.txBx.TabStop = False
        '
        'ProgressBar
        '
        Me.ProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar.Location = New System.Drawing.Point(12, 274)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(463, 16)
        Me.ProgressBar.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(489, 270)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'Progress
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(576, 302)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.txBx)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Progress"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Progress"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event Cancel()
    Public ObservableImp As ObservableImp = ObservableImp.GetInstance

    Public Sub AddText(ByVal text As String)
        If txBx.InvokeRequired Then
            Invoke(New DelegateStr(AddressOf AddText), New Object() {text})
        Else
            txBx.Text += text + Environment.NewLine
            ' necessary for scrolling to end to work
            txBx.SelectionStart = txBx.Text.Length
            txBx.ScrollToCaret()
        End If
    End Sub

    Private Sub Progress_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ObservableImp.Notify(Constants.FormLoaded)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent Cancel()
    End Sub

End Class
