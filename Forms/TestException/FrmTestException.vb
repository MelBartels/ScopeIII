Public Class FrmTestException
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
    Friend WithEvents btnFireException As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTestException))
        Me.btnFireException = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnFireException
        '
        Me.btnFireException.Location = New System.Drawing.Point(96, 112)
        Me.btnFireException.Name = "btnFireException"
        Me.btnFireException.Size = New System.Drawing.Size(96, 23)
        Me.btnFireException.TabIndex = 0
        Me.btnFireException.Text = "Fire Exception"
        '
        'FrmTestException
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.btnFireException)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmTestException"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII ExceptionTest"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ExceptionTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub btnFireException_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFireException.Click
        Try
            'Throw New Exception("test exception")
            Dim x As Double = Double.Parse("xxx")
        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try
    End Sub
End Class
