Public Class FrmShowCelestialErrorsInput
    Inherits MVPViewContainsGaugeCoordBase

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
    Friend WithEvents UserCtrlGaugeCoordLat As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoordSidT As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents UserCtrlGauge2AxisCoordEquat As ScopeIII.Forms.UserCtrlGauge2AxisCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmShowCelestialErrorsInput))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnQuit = New System.Windows.Forms.Button
        Me.UserCtrlGaugeCoordLat = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoordSidT = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGauge2AxisCoordEquat = New ScopeIII.Forms.UserCtrlGauge2AxisCoord
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
        'btnQuit
        '
        Me.btnQuit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuit.Location = New System.Drawing.Point(661, 213)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 7
        Me.btnQuit.Text = "Quit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'UserCtrlGaugeCoordLat
        '
        Me.UserCtrlGaugeCoordLat.Location = New System.Drawing.Point(562, 0)
        Me.UserCtrlGaugeCoordLat.Name = "UserCtrlGaugeCoordLat"
        Me.UserCtrlGaugeCoordLat.Size = New System.Drawing.Size(184, 200)
        Me.UserCtrlGaugeCoordLat.TabIndex = 6
        '
        'UserCtrlGaugeCoordSidT
        '
        Me.UserCtrlGaugeCoordSidT.Location = New System.Drawing.Point(372, 0)
        Me.UserCtrlGaugeCoordSidT.Name = "UserCtrlGaugeCoordSidT"
        Me.UserCtrlGaugeCoordSidT.Size = New System.Drawing.Size(184, 200)
        Me.UserCtrlGaugeCoordSidT.TabIndex = 5
        '
        'UserCtrlGauge2AxisCoordEquat
        '
        Me.UserCtrlGauge2AxisCoordEquat.GaugeLayout = Nothing
        Me.UserCtrlGauge2AxisCoordEquat.Location = New System.Drawing.Point(-2, 0)
        Me.UserCtrlGauge2AxisCoordEquat.Name = "UserCtrlGauge2AxisCoordEquat"
        Me.UserCtrlGauge2AxisCoordEquat.Size = New System.Drawing.Size(368, 200)
        Me.UserCtrlGauge2AxisCoordEquat.TabIndex = 4
        '
        'FrmShowCelestialErrorsInput
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnQuit
        Me.ClientSize = New System.Drawing.Size(748, 248)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.UserCtrlGaugeCoordLat)
        Me.Controls.Add(Me.UserCtrlGaugeCoordSidT)
        Me.Controls.Add(Me.UserCtrlGauge2AxisCoordEquat)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmShowCelestialErrorsInput"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Show Celestial Errors Input"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FrmShowCelestialErrorsInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Protected Overridable Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim ctrlWidth As Int32 = eMath.RInt(MyBase.Width / 4)
        Dim ctrlHeight As Int32 = MyBase.Height - 80
        UserCtrlGauge2AxisCoordEquat.Size = New Drawing.Size(ctrlWidth * 2, ctrlHeight)

        UserCtrlGaugeCoordSidT.Size = New Drawing.Size(ctrlWidth, ctrlHeight)
        UserCtrlGaugeCoordSidT.Location = New Drawing.Point(ctrlWidth * 2, 0)

        UserCtrlGaugeCoordLat.Size = New Drawing.Size(ctrlWidth, ctrlHeight)
        UserCtrlGaugeCoordLat.Location = New Drawing.Point(ctrlWidth * 3, 0)
    End Sub
End Class
