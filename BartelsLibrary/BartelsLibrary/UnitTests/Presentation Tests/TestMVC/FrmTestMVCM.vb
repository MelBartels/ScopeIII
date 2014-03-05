Public Class FrmTestMVCM
    Inherits ViewWin

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
    Friend WithEvents DataGrid1 As BartelsLibrary.TestDataGridCustomHeaders

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Button1 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTestMVCM))
        Me.DataGrid1 = New BartelsLibrary.TestDataGridCustomHeaders
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(48, 72)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(192, 168)
        Me.DataGrid1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(104, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Make My Day!"
        '
        'FrmTestMVCM
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGrid1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmTestMVCM"
        Me.Text = "FrmTestMVCM"
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected Overrides Function GetControllerFactory() As ControllerFactory
        Return frmTestMVCMControllerFactory.GetInstance
    End Function

End Class
