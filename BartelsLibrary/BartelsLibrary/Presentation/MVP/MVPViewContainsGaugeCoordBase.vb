Public Class MVPViewContainsGaugeCoordBase
    Inherits BartelsLibrary.MVPViewBase

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MVPViewContainsGaugeCoordBase))
        Me.SuspendLayout()
        '
        'MVPViewContainsGaugeCoordBase
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MVPViewContainsGaugeCoordBase"
        Me.ResumeLayout(False)

    End Sub

#End Region

    ' force redraw that recalculates gauge width/height 
    Private Sub Frm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyBase.Size = New Drawing.Size(MyBase.Width, MyBase.Height - 1)
    End Sub
End Class
