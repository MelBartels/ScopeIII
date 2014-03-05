Public Class FrmArcGaugeOK
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
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents MvpUserCtrlGaugeBase As BartelsLibrary.MVPUserCtrlGaugeBase
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmArcGaugeOK))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnOK = New System.Windows.Forms.Button
        Me.MvpUserCtrlGaugeBase = New BartelsLibrary.MVPUserCtrlGaugeBase
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
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(87, 80)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        '
        'MvpUserCtrlGaugeBase
        '
        Me.MvpUserCtrlGaugeBase.IRenderer = Nothing
        Me.MvpUserCtrlGaugeBase.Location = New System.Drawing.Point(0, 0)
        Me.MvpUserCtrlGaugeBase.Name = "MvpUserCtrlGaugeBase"
        Me.MvpUserCtrlGaugeBase.Size = New System.Drawing.Size(248, 72)
        Me.MvpUserCtrlGaugeBase.TabIndex = 4
        '
        'FrmArcGaugeOK
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(248, 110)
        Me.Controls.Add(Me.MvpUserCtrlGaugeBase)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmArcGaugeOK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII ArcGauge"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event MeasurementToPoint(ByVal value As Double)

    Protected Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim width As Int32 = MyBase.Width - 8
        If width < 1 Then
            width = 1
        End If
        Dim height As Int32 = MyBase.Height - 70
        If height < 1 Then
            height = 1
        End If
        MvpUserCtrlGaugeBase.NewSize(width, height)
    End Sub

    Private Sub gaugeMeasurementToPoint(ByVal value As Double) Handles MVPUserCtrlGaugeBase.MeasurementToPoint
        RaiseEvent MeasurementToPoint(value)
    End Sub

    Private Sub FrmArcGaugeOK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        resizer(Nothing, Nothing)
    End Sub
End Class
