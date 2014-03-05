Public Class FrmRenderScalarGaugesOK
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
    Friend WithEvents MvpUserCtrlGaugeBase3 As BartelsLibrary.MVPUserCtrlGaugeBase
    Friend WithEvents MvpUserCtrlGaugeBase2 As BartelsLibrary.MVPUserCtrlGaugeBase
    Friend WithEvents MvpUserCtrlGaugeBase1 As BartelsLibrary.MVPUserCtrlGaugeBase
    Friend WithEvents btnOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRenderScalarGaugesOK))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnOK = New System.Windows.Forms.Button
        Me.MvpUserCtrlGaugeBase3 = New BartelsLibrary.MVPUserCtrlGaugeBase
        Me.MvpUserCtrlGaugeBase2 = New BartelsLibrary.MVPUserCtrlGaugeBase
        Me.MvpUserCtrlGaugeBase1 = New BartelsLibrary.MVPUserCtrlGaugeBase
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
        Me.btnOK.Location = New System.Drawing.Point(341, 81)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        '
        'MvpUserCtrlGaugeBase3
        '
        Me.MvpUserCtrlGaugeBase3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MvpUserCtrlGaugeBase3.IRenderer = Nothing
        Me.MvpUserCtrlGaugeBase3.Location = New System.Drawing.Point(506, -1)
        Me.MvpUserCtrlGaugeBase3.Name = "MvpUserCtrlGaugeBase3"
        Me.MvpUserCtrlGaugeBase3.Size = New System.Drawing.Size(248, 72)
        Me.MvpUserCtrlGaugeBase3.TabIndex = 10
        '
        'MvpUserCtrlGaugeBase2
        '
        Me.MvpUserCtrlGaugeBase2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MvpUserCtrlGaugeBase2.IRenderer = Nothing
        Me.MvpUserCtrlGaugeBase2.Location = New System.Drawing.Point(252, -1)
        Me.MvpUserCtrlGaugeBase2.Name = "MvpUserCtrlGaugeBase2"
        Me.MvpUserCtrlGaugeBase2.Size = New System.Drawing.Size(248, 72)
        Me.MvpUserCtrlGaugeBase2.TabIndex = 9
        '
        'MvpUserCtrlGaugeBase1
        '
        Me.MvpUserCtrlGaugeBase1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MvpUserCtrlGaugeBase1.IRenderer = Nothing
        Me.MvpUserCtrlGaugeBase1.Location = New System.Drawing.Point(-2, -1)
        Me.MvpUserCtrlGaugeBase1.Name = "MvpUserCtrlGaugeBase1"
        Me.MvpUserCtrlGaugeBase1.Size = New System.Drawing.Size(248, 72)
        Me.MvpUserCtrlGaugeBase1.TabIndex = 8
        '
        'FrmRenderScalarGaugesOK
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(756, 111)
        Me.Controls.Add(Me.MvpUserCtrlGaugeBase3)
        Me.Controls.Add(Me.MvpUserCtrlGaugeBase2)
        Me.Controls.Add(Me.MvpUserCtrlGaugeBase1)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmRenderScalarGaugesOK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Scalar Gauges"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event MeasurementToPointGauge1(ByVal value As Double)
    Public Event MeasurementToPointGauge2(ByVal value As Double)
    Public Event MeasurementToPointGauge3(ByVal value As Double)

    Protected Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim numberOfCols As Int32 = 3
        Dim ctrlHeight As Int32 = eMath.RInt(MyBase.Height - 70)
        Dim border As Int32 = 8

        Dim startY As Int32 = 0
        MvpUserCtrlGaugeBase1.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols - border), ctrlHeight)
        MvpUserCtrlGaugeBase1.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 0 / numberOfCols), startY)

        MvpUserCtrlGaugeBase2.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols - border), ctrlHeight)
        MvpUserCtrlGaugeBase2.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 1 / numberOfCols), startY)

        MvpUserCtrlGaugeBase3.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols - border), ctrlHeight)
        MvpUserCtrlGaugeBase3.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 2 / numberOfCols), startY)
    End Sub

    Private Sub FrmRenderScalarGaugesOK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        resizer(Nothing, Nothing)
    End Sub

    Private Sub gaugeMeasurementToPoint1(ByVal value As Double) Handles MvpUserCtrlGaugeBase1.MeasurementToPoint
        RaiseEvent MeasurementToPointGauge1(value)
    End Sub

    Private Sub gaugeMeasurementToPoint2(ByVal value As Double) Handles MvpUserCtrlGaugeBase2.MeasurementToPoint
        RaiseEvent MeasurementToPointGauge2(value)
    End Sub

    Private Sub gaugeMeasurementToPoint3(ByVal value As Double) Handles MvpUserCtrlGaugeBase3.MeasurementToPoint
        RaiseEvent MeasurementToPointGauge3(value)
    End Sub
End Class
