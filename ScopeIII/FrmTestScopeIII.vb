Public Class FrmTestScopeIII
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
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents btnTestCfg As System.Windows.Forms.Button
    Friend WithEvents btnTestExceptions As System.Windows.Forms.Button
    Friend WithEvents btnConvert As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnTestEnter2AxisCoord As System.Windows.Forms.Button
    Friend WithEvents btnTestEnterOneTwo As System.Windows.Forms.Button
    Friend WithEvents btnTestEnterPosition As System.Windows.Forms.Button
    Friend WithEvents btnTestEnterSite As System.Windows.Forms.Button
    Friend WithEvents btnTestEnterZ123 As System.Windows.Forms.Button
    Friend WithEvents btnCompareZ12s As System.Windows.Forms.Button
    Friend WithEvents btnPrecessDatafiles As System.Windows.Forms.Button
    Friend WithEvents btnQueryDatafiles As System.Windows.Forms.Button
    Friend WithEvents btnCoordinateErrors As System.Windows.Forms.Button
    Friend WithEvents btnTestSerialPortSettings As System.Windows.Forms.Button
    Friend WithEvents btnIOTerminal As System.Windows.Forms.Button
    Friend WithEvents btnTestIPsettings As System.Windows.Forms.Button
    Friend WithEvents btnRefractionAirMass As System.Windows.Forms.Button
    Friend WithEvents btnTestLoggingSettings As System.Windows.Forms.Button
    Friend WithEvents btnTestDeviceSettings As System.Windows.Forms.Button
    Friend WithEvents btnTestGraphFunction As System.Windows.Forms.Button
    Friend WithEvents btnTestGraphOKPropGrid As System.Windows.Forms.Button
    Friend WithEvents btnTestEnterCoord As System.Windows.Forms.Button
    Friend WithEvents btnGaugeCoord As System.Windows.Forms.Button
    Friend WithEvents btnGauge2AxisCoord As System.Windows.Forms.Button
    Friend WithEvents btnGaugePosition As System.Windows.Forms.Button
    Friend WithEvents btnGauge3AxisCoord As System.Windows.Forms.Button
    Friend WithEvents btnSlider As System.Windows.Forms.Button
    Friend WithEvents btnGauges As System.Windows.Forms.Button
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutScopeIIIToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMsgBox As System.Windows.Forms.Button
    Friend WithEvents btnTestThreading As System.Windows.Forms.Button
    Friend WithEvents btnSaguaro As System.Windows.Forms.Button
    Friend WithEvents btnTestEnter3AxisCoord As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTestScopeIII))
        Me.btnQuit = New System.Windows.Forms.Button
        Me.btnTestCfg = New System.Windows.Forms.Button
        Me.btnTestExceptions = New System.Windows.Forms.Button
        Me.btnConvert = New System.Windows.Forms.Button
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnTestEnter2AxisCoord = New System.Windows.Forms.Button
        Me.btnTestEnterCoord = New System.Windows.Forms.Button
        Me.btnTestEnterOneTwo = New System.Windows.Forms.Button
        Me.btnTestEnterPosition = New System.Windows.Forms.Button
        Me.btnTestEnterSite = New System.Windows.Forms.Button
        Me.btnTestEnterZ123 = New System.Windows.Forms.Button
        Me.btnCompareZ12s = New System.Windows.Forms.Button
        Me.btnPrecessDatafiles = New System.Windows.Forms.Button
        Me.btnQueryDatafiles = New System.Windows.Forms.Button
        Me.btnCoordinateErrors = New System.Windows.Forms.Button
        Me.btnTestGraphOKPropGrid = New System.Windows.Forms.Button
        Me.btnTestGraphFunction = New System.Windows.Forms.Button
        Me.btnTestSerialPortSettings = New System.Windows.Forms.Button
        Me.btnIOTerminal = New System.Windows.Forms.Button
        Me.btnTestIPsettings = New System.Windows.Forms.Button
        Me.btnRefractionAirMass = New System.Windows.Forms.Button
        Me.btnTestLoggingSettings = New System.Windows.Forms.Button
        Me.btnTestDeviceSettings = New System.Windows.Forms.Button
        Me.btnGaugeCoord = New System.Windows.Forms.Button
        Me.btnGauge2AxisCoord = New System.Windows.Forms.Button
        Me.btnGaugePosition = New System.Windows.Forms.Button
        Me.btnGauge3AxisCoord = New System.Windows.Forms.Button
        Me.btnGauges = New System.Windows.Forms.Button
        Me.btnSlider = New System.Windows.Forms.Button
        Me.btnTestEnter3AxisCoord = New System.Windows.Forms.Button
        Me.MenuStrip = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutScopeIIIToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.btnMsgBox = New System.Windows.Forms.Button
        Me.btnTestThreading = New System.Windows.Forms.Button
        Me.btnSaguaro = New System.Windows.Forms.Button
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnQuit
        '
        Me.btnQuit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuit.BackColor = System.Drawing.SystemColors.Control
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuit.Location = New System.Drawing.Point(753, 294)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(80, 23)
        Me.btnQuit.TabIndex = 31
        Me.btnQuit.Text = "Quit"
        Me.btnQuit.UseVisualStyleBackColor = False
        '
        'btnTestCfg
        '
        Me.btnTestCfg.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestCfg.Location = New System.Drawing.Point(192, 68)
        Me.btnTestCfg.Name = "btnTestCfg"
        Me.btnTestCfg.Size = New System.Drawing.Size(136, 23)
        Me.btnTestCfg.TabIndex = 10
        Me.btnTestCfg.Text = "Test Configuration"
        Me.btnTestCfg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestCfg.UseVisualStyleBackColor = False
        '
        'btnTestExceptions
        '
        Me.btnTestExceptions.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestExceptions.Location = New System.Drawing.Point(192, 100)
        Me.btnTestExceptions.Name = "btnTestExceptions"
        Me.btnTestExceptions.Size = New System.Drawing.Size(136, 23)
        Me.btnTestExceptions.TabIndex = 11
        Me.btnTestExceptions.Text = "Test Exceptions"
        Me.btnTestExceptions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestExceptions.UseVisualStyleBackColor = False
        '
        'btnConvert
        '
        Me.btnConvert.BackColor = System.Drawing.SystemColors.Control
        Me.btnConvert.Location = New System.Drawing.Point(24, 36)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(136, 23)
        Me.btnConvert.TabIndex = 1
        Me.btnConvert.Text = "Convert"
        Me.btnConvert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConvert.UseVisualStyleBackColor = False
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'btnTestEnter2AxisCoord
        '
        Me.btnTestEnter2AxisCoord.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestEnter2AxisCoord.Location = New System.Drawing.Point(360, 68)
        Me.btnTestEnter2AxisCoord.Name = "btnTestEnter2AxisCoord"
        Me.btnTestEnter2AxisCoord.Size = New System.Drawing.Size(136, 23)
        Me.btnTestEnter2AxisCoord.TabIndex = 17
        Me.btnTestEnter2AxisCoord.Text = "Test Enter2AxisCoord"
        Me.btnTestEnter2AxisCoord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestEnter2AxisCoord.UseVisualStyleBackColor = False
        '
        'btnTestEnterCoord
        '
        Me.btnTestEnterCoord.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestEnterCoord.Location = New System.Drawing.Point(360, 36)
        Me.btnTestEnterCoord.Name = "btnTestEnterCoord"
        Me.btnTestEnterCoord.Size = New System.Drawing.Size(136, 23)
        Me.btnTestEnterCoord.TabIndex = 16
        Me.btnTestEnterCoord.Text = "Test EnterCoordinate"
        Me.btnTestEnterCoord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestEnterCoord.UseVisualStyleBackColor = False
        '
        'btnTestEnterOneTwo
        '
        Me.btnTestEnterOneTwo.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestEnterOneTwo.Location = New System.Drawing.Point(360, 196)
        Me.btnTestEnterOneTwo.Name = "btnTestEnterOneTwo"
        Me.btnTestEnterOneTwo.Size = New System.Drawing.Size(136, 23)
        Me.btnTestEnterOneTwo.TabIndex = 21
        Me.btnTestEnterOneTwo.Text = "Test EnterOneTwo"
        Me.btnTestEnterOneTwo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestEnterOneTwo.UseVisualStyleBackColor = False
        '
        'btnTestEnterPosition
        '
        Me.btnTestEnterPosition.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestEnterPosition.Location = New System.Drawing.Point(360, 164)
        Me.btnTestEnterPosition.Name = "btnTestEnterPosition"
        Me.btnTestEnterPosition.Size = New System.Drawing.Size(136, 23)
        Me.btnTestEnterPosition.TabIndex = 20
        Me.btnTestEnterPosition.Text = "Test EnterPosition"
        Me.btnTestEnterPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestEnterPosition.UseVisualStyleBackColor = False
        '
        'btnTestEnterSite
        '
        Me.btnTestEnterSite.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestEnterSite.Location = New System.Drawing.Point(360, 132)
        Me.btnTestEnterSite.Name = "btnTestEnterSite"
        Me.btnTestEnterSite.Size = New System.Drawing.Size(136, 23)
        Me.btnTestEnterSite.TabIndex = 19
        Me.btnTestEnterSite.Text = "Test EnterSite"
        Me.btnTestEnterSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestEnterSite.UseVisualStyleBackColor = False
        '
        'btnTestEnterZ123
        '
        Me.btnTestEnterZ123.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestEnterZ123.Location = New System.Drawing.Point(360, 228)
        Me.btnTestEnterZ123.Name = "btnTestEnterZ123"
        Me.btnTestEnterZ123.Size = New System.Drawing.Size(136, 23)
        Me.btnTestEnterZ123.TabIndex = 22
        Me.btnTestEnterZ123.Text = "Test EnterZ123"
        Me.btnTestEnterZ123.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestEnterZ123.UseVisualStyleBackColor = False
        '
        'btnCompareZ12s
        '
        Me.btnCompareZ12s.BackColor = System.Drawing.SystemColors.Control
        Me.btnCompareZ12s.Location = New System.Drawing.Point(24, 68)
        Me.btnCompareZ12s.Name = "btnCompareZ12s"
        Me.btnCompareZ12s.Size = New System.Drawing.Size(136, 23)
        Me.btnCompareZ12s.TabIndex = 3
        Me.btnCompareZ12s.Text = "CompareZ12s"
        Me.btnCompareZ12s.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCompareZ12s.UseVisualStyleBackColor = False
        '
        'btnPrecessDatafiles
        '
        Me.btnPrecessDatafiles.BackColor = System.Drawing.SystemColors.Control
        Me.btnPrecessDatafiles.Location = New System.Drawing.Point(24, 164)
        Me.btnPrecessDatafiles.Name = "btnPrecessDatafiles"
        Me.btnPrecessDatafiles.Size = New System.Drawing.Size(136, 23)
        Me.btnPrecessDatafiles.TabIndex = 6
        Me.btnPrecessDatafiles.Text = "Precess Datafiles"
        Me.btnPrecessDatafiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrecessDatafiles.UseVisualStyleBackColor = False
        '
        'btnQueryDatafiles
        '
        Me.btnQueryDatafiles.BackColor = System.Drawing.SystemColors.Control
        Me.btnQueryDatafiles.Location = New System.Drawing.Point(24, 100)
        Me.btnQueryDatafiles.Name = "btnQueryDatafiles"
        Me.btnQueryDatafiles.Size = New System.Drawing.Size(136, 23)
        Me.btnQueryDatafiles.TabIndex = 5
        Me.btnQueryDatafiles.Text = "Query Datafiles"
        Me.btnQueryDatafiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQueryDatafiles.UseVisualStyleBackColor = False
        '
        'btnCoordinateErrors
        '
        Me.btnCoordinateErrors.BackColor = System.Drawing.SystemColors.Control
        Me.btnCoordinateErrors.Location = New System.Drawing.Point(24, 196)
        Me.btnCoordinateErrors.Name = "btnCoordinateErrors"
        Me.btnCoordinateErrors.Size = New System.Drawing.Size(136, 23)
        Me.btnCoordinateErrors.TabIndex = 7
        Me.btnCoordinateErrors.Text = "Coordinate Errors"
        Me.btnCoordinateErrors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCoordinateErrors.UseVisualStyleBackColor = False
        '
        'btnTestGraphOKPropGrid
        '
        Me.btnTestGraphOKPropGrid.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestGraphOKPropGrid.Location = New System.Drawing.Point(697, 68)
        Me.btnTestGraphOKPropGrid.Name = "btnTestGraphOKPropGrid"
        Me.btnTestGraphOKPropGrid.Size = New System.Drawing.Size(136, 23)
        Me.btnTestGraphOKPropGrid.TabIndex = 30
        Me.btnTestGraphOKPropGrid.Text = "Test Graph PropGrid"
        Me.btnTestGraphOKPropGrid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestGraphOKPropGrid.UseVisualStyleBackColor = False
        '
        'btnTestGraphFunction
        '
        Me.btnTestGraphFunction.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestGraphFunction.Location = New System.Drawing.Point(697, 36)
        Me.btnTestGraphFunction.Name = "btnTestGraphFunction"
        Me.btnTestGraphFunction.Size = New System.Drawing.Size(136, 23)
        Me.btnTestGraphFunction.TabIndex = 29
        Me.btnTestGraphFunction.Text = "Test Graph Function"
        Me.btnTestGraphFunction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestGraphFunction.UseVisualStyleBackColor = False
        '
        'btnTestSerialPortSettings
        '
        Me.btnTestSerialPortSettings.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestSerialPortSettings.Location = New System.Drawing.Point(192, 164)
        Me.btnTestSerialPortSettings.Name = "btnTestSerialPortSettings"
        Me.btnTestSerialPortSettings.Size = New System.Drawing.Size(136, 23)
        Me.btnTestSerialPortSettings.TabIndex = 12
        Me.btnTestSerialPortSettings.Text = "Test SerialPort Settings"
        Me.btnTestSerialPortSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestSerialPortSettings.UseVisualStyleBackColor = False
        '
        'btnIOTerminal
        '
        Me.btnIOTerminal.BackColor = System.Drawing.SystemColors.Control
        Me.btnIOTerminal.Location = New System.Drawing.Point(24, 260)
        Me.btnIOTerminal.Name = "btnIOTerminal"
        Me.btnIOTerminal.Size = New System.Drawing.Size(136, 23)
        Me.btnIOTerminal.TabIndex = 9
        Me.btnIOTerminal.Text = "IO Terminal"
        Me.btnIOTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnIOTerminal.UseVisualStyleBackColor = False
        '
        'btnTestIPsettings
        '
        Me.btnTestIPsettings.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestIPsettings.Location = New System.Drawing.Point(192, 196)
        Me.btnTestIPsettings.Name = "btnTestIPsettings"
        Me.btnTestIPsettings.Size = New System.Drawing.Size(136, 23)
        Me.btnTestIPsettings.TabIndex = 13
        Me.btnTestIPsettings.Text = "Test IP Settings"
        Me.btnTestIPsettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestIPsettings.UseVisualStyleBackColor = False
        '
        'btnRefractionAirMass
        '
        Me.btnRefractionAirMass.BackColor = System.Drawing.SystemColors.Control
        Me.btnRefractionAirMass.Location = New System.Drawing.Point(24, 228)
        Me.btnRefractionAirMass.Name = "btnRefractionAirMass"
        Me.btnRefractionAirMass.Size = New System.Drawing.Size(136, 23)
        Me.btnRefractionAirMass.TabIndex = 8
        Me.btnRefractionAirMass.Text = "Refraction + AirMass"
        Me.btnRefractionAirMass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefractionAirMass.UseVisualStyleBackColor = False
        '
        'btnTestLoggingSettings
        '
        Me.btnTestLoggingSettings.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestLoggingSettings.Location = New System.Drawing.Point(192, 228)
        Me.btnTestLoggingSettings.Name = "btnTestLoggingSettings"
        Me.btnTestLoggingSettings.Size = New System.Drawing.Size(136, 23)
        Me.btnTestLoggingSettings.TabIndex = 14
        Me.btnTestLoggingSettings.Text = "Test Logging Settings"
        Me.btnTestLoggingSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestLoggingSettings.UseVisualStyleBackColor = False
        '
        'btnTestDeviceSettings
        '
        Me.btnTestDeviceSettings.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestDeviceSettings.Location = New System.Drawing.Point(192, 260)
        Me.btnTestDeviceSettings.Name = "btnTestDeviceSettings"
        Me.btnTestDeviceSettings.Size = New System.Drawing.Size(136, 23)
        Me.btnTestDeviceSettings.TabIndex = 15
        Me.btnTestDeviceSettings.Text = "Test Device Settings"
        Me.btnTestDeviceSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestDeviceSettings.UseVisualStyleBackColor = False
        '
        'btnGaugeCoord
        '
        Me.btnGaugeCoord.BackColor = System.Drawing.SystemColors.Control
        Me.btnGaugeCoord.Location = New System.Drawing.Point(528, 100)
        Me.btnGaugeCoord.Name = "btnGaugeCoord"
        Me.btnGaugeCoord.Size = New System.Drawing.Size(136, 23)
        Me.btnGaugeCoord.TabIndex = 25
        Me.btnGaugeCoord.Text = "Gauge Coord "
        Me.btnGaugeCoord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGaugeCoord.UseVisualStyleBackColor = False
        '
        'btnGauge2AxisCoord
        '
        Me.btnGauge2AxisCoord.BackColor = System.Drawing.SystemColors.Control
        Me.btnGauge2AxisCoord.Location = New System.Drawing.Point(528, 132)
        Me.btnGauge2AxisCoord.Name = "btnGauge2AxisCoord"
        Me.btnGauge2AxisCoord.Size = New System.Drawing.Size(136, 23)
        Me.btnGauge2AxisCoord.TabIndex = 26
        Me.btnGauge2AxisCoord.Text = "Gauge 2AxisCoord"
        Me.btnGauge2AxisCoord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGauge2AxisCoord.UseVisualStyleBackColor = False
        '
        'btnGaugePosition
        '
        Me.btnGaugePosition.BackColor = System.Drawing.SystemColors.Control
        Me.btnGaugePosition.Location = New System.Drawing.Point(528, 196)
        Me.btnGaugePosition.Name = "btnGaugePosition"
        Me.btnGaugePosition.Size = New System.Drawing.Size(136, 23)
        Me.btnGaugePosition.TabIndex = 28
        Me.btnGaugePosition.Text = "Gauge Position"
        Me.btnGaugePosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGaugePosition.UseVisualStyleBackColor = False
        '
        'btnGauge3AxisCoord
        '
        Me.btnGauge3AxisCoord.BackColor = System.Drawing.SystemColors.Control
        Me.btnGauge3AxisCoord.Location = New System.Drawing.Point(528, 164)
        Me.btnGauge3AxisCoord.Name = "btnGauge3AxisCoord"
        Me.btnGauge3AxisCoord.Size = New System.Drawing.Size(136, 23)
        Me.btnGauge3AxisCoord.TabIndex = 27
        Me.btnGauge3AxisCoord.Text = "Gauge 3AxisCoord"
        Me.btnGauge3AxisCoord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGauge3AxisCoord.UseVisualStyleBackColor = False
        '
        'btnGauges
        '
        Me.btnGauges.BackColor = System.Drawing.SystemColors.Control
        Me.btnGauges.Location = New System.Drawing.Point(528, 68)
        Me.btnGauges.Name = "btnGauges"
        Me.btnGauges.Size = New System.Drawing.Size(136, 23)
        Me.btnGauges.TabIndex = 24
        Me.btnGauges.Text = "Show Gauges"
        Me.btnGauges.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGauges.UseVisualStyleBackColor = False
        '
        'btnSlider
        '
        Me.btnSlider.BackColor = System.Drawing.SystemColors.Control
        Me.btnSlider.Location = New System.Drawing.Point(528, 36)
        Me.btnSlider.Name = "btnSlider"
        Me.btnSlider.Size = New System.Drawing.Size(136, 23)
        Me.btnSlider.TabIndex = 23
        Me.btnSlider.Text = "Show Slider"
        Me.btnSlider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSlider.UseVisualStyleBackColor = False
        '
        'btnTestEnter3AxisCoord
        '
        Me.btnTestEnter3AxisCoord.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestEnter3AxisCoord.Location = New System.Drawing.Point(360, 100)
        Me.btnTestEnter3AxisCoord.Name = "btnTestEnter3AxisCoord"
        Me.btnTestEnter3AxisCoord.Size = New System.Drawing.Size(136, 23)
        Me.btnTestEnter3AxisCoord.TabIndex = 18
        Me.btnTestEnter3AxisCoord.Text = "Test Enter3AxisCoord"
        Me.btnTestEnter3AxisCoord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestEnter3AxisCoord.UseVisualStyleBackColor = False
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(857, 24)
        Me.MenuStrip.TabIndex = 0
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutScopeIIIToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutScopeIIIToolStripMenuItem
        '
        Me.AboutScopeIIIToolStripMenuItem.Name = "AboutScopeIIIToolStripMenuItem"
        Me.AboutScopeIIIToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.AboutScopeIIIToolStripMenuItem.Text = "About ScopeIII..."
        '
        'btnMsgBox
        '
        Me.btnMsgBox.BackColor = System.Drawing.SystemColors.Control
        Me.btnMsgBox.Location = New System.Drawing.Point(192, 36)
        Me.btnMsgBox.Name = "btnMsgBox"
        Me.btnMsgBox.Size = New System.Drawing.Size(136, 23)
        Me.btnMsgBox.TabIndex = 32
        Me.btnMsgBox.Text = "Test Message Box"
        Me.btnMsgBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMsgBox.UseVisualStyleBackColor = False
        '
        'btnTestThreading
        '
        Me.btnTestThreading.BackColor = System.Drawing.SystemColors.Control
        Me.btnTestThreading.Location = New System.Drawing.Point(192, 129)
        Me.btnTestThreading.Name = "btnTestThreading"
        Me.btnTestThreading.Size = New System.Drawing.Size(136, 23)
        Me.btnTestThreading.TabIndex = 33
        Me.btnTestThreading.Text = "Test Threading"
        Me.btnTestThreading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestThreading.UseVisualStyleBackColor = False
        '
        'btnSaguaro
        '
        Me.btnSaguaro.BackColor = System.Drawing.SystemColors.Control
        Me.btnSaguaro.Location = New System.Drawing.Point(24, 132)
        Me.btnSaguaro.Name = "btnSaguaro"
        Me.btnSaguaro.Size = New System.Drawing.Size(136, 23)
        Me.btnSaguaro.TabIndex = 34
        Me.btnSaguaro.Text = "Saguaro Datafile"
        Me.btnSaguaro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSaguaro.UseVisualStyleBackColor = False
        '
        'FrmTestScopeIII
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnQuit
        Me.ClientSize = New System.Drawing.Size(857, 328)
        Me.Controls.Add(Me.btnSaguaro)
        Me.Controls.Add(Me.btnTestThreading)
        Me.Controls.Add(Me.btnMsgBox)
        Me.Controls.Add(Me.btnTestEnter3AxisCoord)
        Me.Controls.Add(Me.btnSlider)
        Me.Controls.Add(Me.btnGauges)
        Me.Controls.Add(Me.btnGauge3AxisCoord)
        Me.Controls.Add(Me.btnGaugePosition)
        Me.Controls.Add(Me.btnGauge2AxisCoord)
        Me.Controls.Add(Me.btnGaugeCoord)
        Me.Controls.Add(Me.btnTestDeviceSettings)
        Me.Controls.Add(Me.btnTestLoggingSettings)
        Me.Controls.Add(Me.btnRefractionAirMass)
        Me.Controls.Add(Me.btnTestIPsettings)
        Me.Controls.Add(Me.btnIOTerminal)
        Me.Controls.Add(Me.btnTestSerialPortSettings)
        Me.Controls.Add(Me.btnTestGraphFunction)
        Me.Controls.Add(Me.btnTestGraphOKPropGrid)
        Me.Controls.Add(Me.btnCoordinateErrors)
        Me.Controls.Add(Me.btnQueryDatafiles)
        Me.Controls.Add(Me.btnPrecessDatafiles)
        Me.Controls.Add(Me.btnCompareZ12s)
        Me.Controls.Add(Me.btnTestEnterZ123)
        Me.Controls.Add(Me.btnTestEnterSite)
        Me.Controls.Add(Me.btnTestEnterPosition)
        Me.Controls.Add(Me.btnTestEnterOneTwo)
        Me.Controls.Add(Me.btnTestEnterCoord)
        Me.Controls.Add(Me.btnTestEnter2AxisCoord)
        Me.Controls.Add(Me.btnConvert)
        Me.Controls.Add(Me.btnTestExceptions)
        Me.Controls.Add(Me.btnTestCfg)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip
        Me.MaximizeBox = False
        Me.Name = "FrmTestScopeIII"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII© Master Select"
        Me.TopMost = True
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event Convert()
    Public Event CompareZ12s()
    Public Event QueryDatafiles()
    Public Event PrecessDatafiles()
    Public Event Saguaro()
    Public Event CoordinateErrors()
    Public Event RefractionAirMass()
    Public Event IOTerminal()
    Public Event MsgBox()
    Public Event TestCfg()
    Public Event TestExceptions()
    Public Event TestThreading()
    Public Event TestSerialPortSettings()
    Public Event TestIPsettings()
    Public Event TestLoggingSettings()
    Public Event TestDeviceSettings()
    Public Event TestEnterCoord()
    Public Event TestEnter2AxisCoord()
    Public Event TestEnter3AxisCoord()
    Public Event TestEnterSite()
    Public Event TestEnterPosition()
    Public Event TestEnterOneTwo()
    Public Event TestEnterZ123()
    Public Event TestGraphFunction()
    Public Event TestGraphOKPropGrid()
    Public Event Slider()
    Public Event Gauges()
    Public Event GaugeCoord()
    Public Event Gauge2AxisCoord()
    Public Event Gauge3AxisCoord()
    Public Event GaugePosition()

    Public Overrides Function DefaultPresenter() As IMVPPresenter
        Return initDefaultPresenter(TestScopeIIIPresenter.GetInstance)
    End Function

    Private Sub FrmTestScopeIII_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TopMost = False
    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub btnConvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConvert.Click
        RaiseEvent Convert()
    End Sub

    Private Sub btnCompareZ12s_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompareZ12s.Click
        RaiseEvent CompareZ12s()
    End Sub

    Private Sub btnQueryDatafiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQueryDatafiles.Click
        RaiseEvent QueryDatafiles()
    End Sub

    Private Sub btnPrecessDatafiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrecessDatafiles.Click
        RaiseEvent PrecessDatafiles()
    End Sub

    Private Sub btnCoordinateErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCoordinateErrors.Click
        RaiseEvent CoordinateErrors()
    End Sub

    Private Sub btnRefractionAirMass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefractionAirMass.Click
        RaiseEvent RefractionAirMass()
    End Sub

    Private Sub btnIOTerminal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIOTerminal.Click
        RaiseEvent IOTerminal()
    End Sub

    Private Sub btnTestCfg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestCfg.Click
        RaiseEvent TestCfg()
    End Sub

    Private Sub btnTestExceptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestExceptions.Click
        RaiseEvent TestExceptions()
    End Sub

    Private Sub btnTestSerialPortSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestSerialPortSettings.Click
        RaiseEvent TestSerialPortSettings()
    End Sub

    Private Sub btnTestIPsettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestIPsettings.Click
        RaiseEvent TestIPsettings()
    End Sub

    Private Sub btnTestLoggingSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestLoggingSettings.Click
        RaiseEvent TestLoggingSettings()
    End Sub

    Private Sub btnTestDeviceSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestDeviceSettings.Click
        RaiseEvent TestDeviceSettings()
    End Sub

    Private Sub btnTestEnterCoord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEnterCoord.Click
        RaiseEvent TestEnterCoord()
    End Sub

    Private Sub btnTestEnter2AxisCoord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEnter2AxisCoord.Click
        RaiseEvent TestEnter2AxisCoord()
    End Sub

    Private Sub btnTestEnter3AxisCoord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEnter3AxisCoord.Click
        RaiseEvent TestEnter3AxisCoord()
    End Sub

    Private Sub btnTestEnterSite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEnterSite.Click
        RaiseEvent TestEnterSite()
    End Sub

    Private Sub btnTestEnterPosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEnterPosition.Click
        RaiseEvent TestEnterPosition()
    End Sub

    Private Sub btnTestEnterOneTwo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEnterOneTwo.Click
        RaiseEvent TestEnterOneTwo()
    End Sub

    Private Sub btnTestEnterZ123_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEnterZ123.Click
        RaiseEvent TestEnterZ123()
    End Sub

    Private Sub btnTestGraphFunction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestGraphFunction.Click
        RaiseEvent TestGraphFunction()
    End Sub

    Private Sub btnTestGraphOKPropGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestGraphOKPropGrid.Click
        RaiseEvent TestGraphOKPropGrid()
    End Sub

    Private Sub btnSlider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlider.Click
        RaiseEvent Slider()
    End Sub

    Private Sub btnGauges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGauges.Click
        RaiseEvent Gauges()
    End Sub

    Private Sub btnGaugeCoord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGaugeCoord.Click
        RaiseEvent GaugeCoord()
    End Sub

    Private Sub btnGauge2AxisCoord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGauge2AxisCoord.Click
        RaiseEvent Gauge2AxisCoord()
    End Sub

    Private Sub btnGauge3AxisCoord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGauge3AxisCoord.Click
        RaiseEvent Gauge3AxisCoord()
    End Sub

    Private Sub btnGaugePosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGaugePosition.Click
        RaiseEvent GaugePosition()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        MyBase.Close()
    End Sub

    Private Sub AboutScopeIIIToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutScopeIIIToolStripMenuItem.Click
        SplashScreen.ShowDialog()
    End Sub

    Private Sub btnMsgBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMsgBox.Click
        RaiseEvent MsgBox()
    End Sub

    Private Sub btnTestThreading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestThreading.Click
        RaiseEvent TestThreading()
    End Sub

    Private Sub btnSaguaro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaguaro.Click
        RaiseEvent Saguaro()
    End Sub
End Class
