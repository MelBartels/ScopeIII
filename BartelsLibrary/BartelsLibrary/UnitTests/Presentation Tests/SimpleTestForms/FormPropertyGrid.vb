Public Class FrmPropertyGrid
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
    Public WithEvents PropertyGrid As System.Windows.Forms.PropertyGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPropertyGrid))
        Me.PropertyGrid = New System.Windows.Forms.PropertyGrid
        Me.SuspendLayout()
        '
        'PropertyGrid
        '
        Me.PropertyGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.PropertyGrid.Location = New System.Drawing.Point(8, 0)
        Me.PropertyGrid.Name = "PropertyGrid"
        Me.PropertyGrid.Size = New System.Drawing.Size(280, 264)
        Me.PropertyGrid.TabIndex = 0
        '
        'FrmPropertyGrid
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.PropertyGrid)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmPropertyGrid"
        Me.Text = "FormPropertyGrid"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
