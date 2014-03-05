Public Class UserCtrlAxisEncoderTranslator
    Inherits MVPUserCtrlGaugesBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents lblTotalTicks As System.Windows.Forms.Label
    Friend WithEvents lblGearRatio As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownTotalTicks As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownGearRatio As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmBxAxisSelect As System.Windows.Forms.ComboBox
    Friend WithEvents UserCtrlGaugeCoord As ScopeIII.Forms.UserCtrlGaugeCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblTotalTicks = New System.Windows.Forms.Label
        Me.lblGearRatio = New System.Windows.Forms.Label
        Me.NumericUpDownTotalTicks = New System.Windows.Forms.NumericUpDown
        Me.NumericUpDownGearRatio = New System.Windows.Forms.NumericUpDown
        Me.cmBxAxisSelect = New System.Windows.Forms.ComboBox
        Me.UserCtrlGaugeCoord = New ScopeIII.Forms.UserCtrlGaugeCoord
        CType(Me.NumericUpDownTotalTicks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownGearRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTotalTicks
        '
        Me.lblTotalTicks.AutoSize = True
        Me.lblTotalTicks.Location = New System.Drawing.Point(88, 230)
        Me.lblTotalTicks.Name = "lblTotalTicks"
        Me.lblTotalTicks.Size = New System.Drawing.Size(60, 13)
        Me.lblTotalTicks.TabIndex = 1
        Me.lblTotalTicks.Text = "Total Ticks"
        Me.lblTotalTicks.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGearRatio
        '
        Me.lblGearRatio.AutoSize = True
        Me.lblGearRatio.Location = New System.Drawing.Point(10, 230)
        Me.lblGearRatio.Name = "lblGearRatio"
        Me.lblGearRatio.Size = New System.Drawing.Size(58, 13)
        Me.lblGearRatio.TabIndex = 3
        Me.lblGearRatio.Text = "Gear Ratio"
        Me.lblGearRatio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NumericUpDownTotalTicks
        '
        Me.NumericUpDownTotalTicks.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.NumericUpDownTotalTicks.Location = New System.Drawing.Point(91, 246)
        Me.NumericUpDownTotalTicks.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.NumericUpDownTotalTicks.Name = "NumericUpDownTotalTicks"
        Me.NumericUpDownTotalTicks.Size = New System.Drawing.Size(88, 20)
        Me.NumericUpDownTotalTicks.TabIndex = 9
        '
        'NumericUpDownGearRatio
        '
        Me.NumericUpDownGearRatio.DecimalPlaces = 3
        Me.NumericUpDownGearRatio.Location = New System.Drawing.Point(13, 246)
        Me.NumericUpDownGearRatio.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDownGearRatio.Name = "NumericUpDownGearRatio"
        Me.NumericUpDownGearRatio.Size = New System.Drawing.Size(55, 20)
        Me.NumericUpDownGearRatio.TabIndex = 10
        '
        'cmBxAxisSelect
        '
        Me.cmBxAxisSelect.FormattingEnabled = True
        Me.cmBxAxisSelect.Location = New System.Drawing.Point(12, 206)
        Me.cmBxAxisSelect.Name = "cmBxAxisSelect"
        Me.cmBxAxisSelect.Size = New System.Drawing.Size(121, 21)
        Me.cmBxAxisSelect.TabIndex = 11
        '
        'UserCtrlGaugeCoord
        '
        Me.UserCtrlGaugeCoord.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGaugeCoord.Name = "UserCtrlGaugeCoord"
        Me.UserCtrlGaugeCoord.Size = New System.Drawing.Size(184, 200)
        Me.UserCtrlGaugeCoord.TabIndex = 0
        '
        'UserCtrlAxisEncoderTranslator
        '
        Me.Controls.Add(Me.cmBxAxisSelect)
        Me.Controls.Add(Me.NumericUpDownGearRatio)
        Me.Controls.Add(Me.NumericUpDownTotalTicks)
        Me.Controls.Add(Me.lblGearRatio)
        Me.Controls.Add(Me.lblTotalTicks)
        Me.Controls.Add(Me.UserCtrlGaugeCoord)
        Me.Name = "UserCtrlAxisEncoderTranslator"
        Me.Size = New System.Drawing.Size(185, 275)
        CType(Me.NumericUpDownTotalTicks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownGearRatio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AxisSelected(ByVal coordName As Object)
    Public Event GearRatio()
    Public Event TotalTicks()

    Public Property GearRatioValue() As Decimal
        Get
            Return NumericUpDownGearRatio.Value
        End Get
        Set(ByVal value As Decimal)
            NumericUpDownGearRatio.Value = value
        End Set
    End Property

    Public Property TotalTicksValue() As Decimal
        Get
            Return NumericUpDownTotalTicks.Value
        End Get
        Set(ByVal value As Decimal)
            NumericUpDownTotalTicks.Value = value
        End Set
    End Property

    Public Property AxisSelectDataSource() As Object
        Get
            Return cmBxAxisSelect.DataSource
        End Get
        Set(ByVal Value As Object)
            cmBxAxisSelect.DataSource = Value
        End Set
    End Property

    Public Overrides Sub NewSize(ByVal width As Int32, ByVal height As Int32)
        Dim size As Int32 = width
        If height < width Then
            size = height
        End If
        UserCtrlGaugeCoord.Size = New Drawing.Size(size, size)
    End Sub

    Protected Overridable Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        NewSize(CType(sender, System.Windows.Forms.UserControl).Width, CType(sender, System.Windows.Forms.UserControl).Height)
    End Sub

    Private Sub NumericUpDownGearRatio_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDownGearRatio.ValueChanged
        RaiseEvent GearRatio()
    End Sub

    Private Sub NumericUpDownTotalTicks_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDownTotalTicks.ValueChanged
        RaiseEvent TotalTicks()
    End Sub

    Private Sub cmBxAxisSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmBxAxisSelect.SelectedIndexChanged
        RaiseEvent AxisSelected(cmBxAxisSelect.SelectedItem)
    End Sub
End Class
