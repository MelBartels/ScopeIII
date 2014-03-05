Imports BartelsLibrary.DelegateSigs

Public Class FrmValueObserver
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
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents txBx As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmValueObserver))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblValue = New System.Windows.Forms.Label
        Me.txBx = New System.Windows.Forms.TextBox
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(160, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'lblValue
        '
        Me.lblValue.Location = New System.Drawing.Point(8, 0)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(48, 20)
        Me.lblValue.TabIndex = 3
        Me.lblValue.Text = "Value"
        Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txBx
        '
        Me.txBx.Location = New System.Drawing.Point(56, 0)
        Me.txBx.Name = "txBx"
        Me.txBx.Size = New System.Drawing.Size(96, 20)
        Me.txBx.TabIndex = 4
        Me.txBx.Text = "value"
        '
        'FrmValueObserver
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(238, 20)
        Me.Controls.Add(Me.txBx)
        Me.Controls.Add(Me.lblValue)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmValueObserver"
        Me.Text = "ScopeIII Value Observer"
        Me.TopMost = False
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Property Value() As Double
        Get
            Dim result As Double = 0
            Double.TryParse(txBx.Text, result)
            Return result
        End Get
        Set(ByVal Value As Double)
            If txBx.InvokeRequired Then
                txBx.Invoke(New DelegateStr(AddressOf invokeTxBxText), New Object() {Value.ToString})
            Else
                invokeTxBxText(Value.ToString)
            End If
        End Set
    End Property

    Private Sub invokeTxBxText(ByVal [string] As String)
        txBx.Text = [string]
    End Sub
End Class
