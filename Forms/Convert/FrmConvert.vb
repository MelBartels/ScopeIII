Imports BartelsLibrary.DelegateSigs

Public Class FrmConvert
    Inherits MVPViewContainsGaugeCoordBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        UserCtrlGauge2AxisCoordSite.GaugeLayout = BartelsLibrary.Layout.Vertical
        UserCtrlGauge3AxisCoordEquat.GaugeLayout = BartelsLibrary.Layout.Vertical
        UserCtrlGauge3AxisCoordSiteAxes.GaugeLayout = BartelsLibrary.Layout.Vertical
        UserCtrlGauge3AxisCoordScopeAxes.GaugeLayout = BartelsLibrary.Layout.Vertical

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
    Friend WithEvents cmbBxRates As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBxAlignment As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnOneTwo As System.Windows.Forms.Button
    Friend WithEvents MVPUserCtrl2MeasurementsGaugeBase As BartelsLibrary.MVPUserCtrl2MeasurementsGaugeBase
    Friend WithEvents UserCtrlGauge3AxisCoordTilt As ScopeIII.Forms.UserCtrlGauge3AxisCoord
    Friend WithEvents chBxCelestialGrid As System.Windows.Forms.CheckBox
    Friend WithEvents chBxSiteGrid As System.Windows.Forms.CheckBox
    Friend WithEvents UserCtrlGauge2AxisCoordSite As ScopeIII.Forms.UserCtrlGauge2AxisCoord
    Friend WithEvents txBxGridLinesPerQuadrant As System.Windows.Forms.TextBox
    Friend WithEvents lblGridLinesPer90Deg As System.Windows.Forms.Label
    Friend WithEvents chBxScopeAltazGrid As System.Windows.Forms.CheckBox
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents chBxDisplayInits As System.Windows.Forms.CheckBox
    Friend WithEvents pnlScopePilot As System.Windows.Forms.Panel
    Friend WithEvents btnQueryDatafiles As System.Windows.Forms.Button
    Friend WithEvents btnCelestialErrors As System.Windows.Forms.Button
    Friend WithEvents btnTrack As System.Windows.Forms.Button
    Friend WithEvents UserCtrlGauge3AxisCoordEquat As ScopeIII.Forms.UserCtrlGauge3AxisCoord
    Friend WithEvents UserCtrlGauge3AxisCoordSiteAxes As ScopeIII.Forms.UserCtrlGauge3AxisCoord
    Friend WithEvents pnlActions As System.Windows.Forms.Panel
    Friend WithEvents chBxKingRate As System.Windows.Forms.CheckBox
    Friend WithEvents UserCtrlGauge3AxisCoordScopeAxes As ScopeIII.Forms.UserCtrlGauge3AxisCoord
    Friend WithEvents lblSecAxisTrackRate As System.Windows.Forms.Label
    Friend WithEvents lblPriAxisTrackRate As System.Windows.Forms.Label
    Friend WithEvents lblTierAxisTrackRate As System.Windows.Forms.Label
    Friend WithEvents pnlTrackRates As System.Windows.Forms.Panel
    Friend WithEvents chBxAnalyzeTrackRatePri As System.Windows.Forms.CheckBox
    Friend WithEvents chBxAnalyzeFieldRotationAngle As System.Windows.Forms.CheckBox
    Friend WithEvents lblMinValue As System.Windows.Forms.Label
    Friend WithEvents txBxMinValue As System.Windows.Forms.TextBox
    Friend WithEvents chBxAnalyzeTrackRateSec As System.Windows.Forms.CheckBox
    Friend WithEvents lblMaxValue As System.Windows.Forms.Label
    Friend WithEvents txBxMaxValue As System.Windows.Forms.TextBox
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPageActions As System.Windows.Forms.TabPage
    Friend WithEvents TabPageAlignment As System.Windows.Forms.TabPage
    Friend WithEvents TabPageGrids As System.Windows.Forms.TabPage
    Friend WithEvents TabPageAnalyze As System.Windows.Forms.TabPage
    Friend WithEvents PnlAlignment As System.Windows.Forms.Panel
    Friend WithEvents PnlGrids As System.Windows.Forms.Panel
    Friend WithEvents PnlAnalyze As System.Windows.Forms.Panel
    Friend WithEvents TabPageTilts As System.Windows.Forms.TabPage
    Friend WithEvents PnlTilts As System.Windows.Forms.Panel
    Friend WithEvents TabControlGauges As System.Windows.Forms.TabControl
    Friend WithEvents TabPageSite As System.Windows.Forms.TabPage
    Friend WithEvents TabPageEquat As System.Windows.Forms.TabPage
    Friend WithEvents TabPageAltaz As System.Windows.Forms.TabPage
    Friend WithEvents TabPageScope As System.Windows.Forms.TabPage
    Friend WithEvents chBxMeridianFlipPointingWest As System.Windows.Forms.CheckBox
    Friend WithEvents chBxMeridianFlipPointingEast As System.Windows.Forms.CheckBox
    Friend WithEvents lblMeridianFlip As System.Windows.Forms.Label
    Friend WithEvents TabPageTrackRates As System.Windows.Forms.TabPage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmConvert))
        Me.cmbBxRates = New System.Windows.Forms.ComboBox
        Me.cmbBxAlignment = New System.Windows.Forms.ComboBox
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnOneTwo = New System.Windows.Forms.Button
        Me.MVPUserCtrl2MeasurementsGaugeBase = New BartelsLibrary.MVPUserCtrl2MeasurementsGaugeBase
        Me.chBxDisplayInits = New System.Windows.Forms.CheckBox
        Me.chBxScopeAltazGrid = New System.Windows.Forms.CheckBox
        Me.lblGridLinesPer90Deg = New System.Windows.Forms.Label
        Me.txBxGridLinesPerQuadrant = New System.Windows.Forms.TextBox
        Me.chBxCelestialGrid = New System.Windows.Forms.CheckBox
        Me.chBxSiteGrid = New System.Windows.Forms.CheckBox
        Me.btnQueryDatafiles = New System.Windows.Forms.Button
        Me.btnQuit = New System.Windows.Forms.Button
        Me.pnlScopePilot = New System.Windows.Forms.Panel
        Me.lblMaxValue = New System.Windows.Forms.Label
        Me.txBxMaxValue = New System.Windows.Forms.TextBox
        Me.lblMinValue = New System.Windows.Forms.Label
        Me.txBxMinValue = New System.Windows.Forms.TextBox
        Me.chBxAnalyzeTrackRateSec = New System.Windows.Forms.CheckBox
        Me.chBxAnalyzeFieldRotationAngle = New System.Windows.Forms.CheckBox
        Me.chBxAnalyzeTrackRatePri = New System.Windows.Forms.CheckBox
        Me.btnCelestialErrors = New System.Windows.Forms.Button
        Me.btnTrack = New System.Windows.Forms.Button
        Me.pnlActions = New System.Windows.Forms.Panel
        Me.lblTierAxisTrackRate = New System.Windows.Forms.Label
        Me.lblPriAxisTrackRate = New System.Windows.Forms.Label
        Me.lblSecAxisTrackRate = New System.Windows.Forms.Label
        Me.chBxKingRate = New System.Windows.Forms.CheckBox
        Me.pnlTrackRates = New System.Windows.Forms.Panel
        Me.TabControlMain = New System.Windows.Forms.TabControl
        Me.TabPageActions = New System.Windows.Forms.TabPage
        Me.TabPageAlignment = New System.Windows.Forms.TabPage
        Me.PnlAlignment = New System.Windows.Forms.Panel
        Me.lblMeridianFlip = New System.Windows.Forms.Label
        Me.chBxMeridianFlipPointingWest = New System.Windows.Forms.CheckBox
        Me.chBxMeridianFlipPointingEast = New System.Windows.Forms.CheckBox
        Me.TabPageTrackRates = New System.Windows.Forms.TabPage
        Me.TabPageGrids = New System.Windows.Forms.TabPage
        Me.PnlGrids = New System.Windows.Forms.Panel
        Me.TabPageAnalyze = New System.Windows.Forms.TabPage
        Me.PnlAnalyze = New System.Windows.Forms.Panel
        Me.TabPageTilts = New System.Windows.Forms.TabPage
        Me.PnlTilts = New System.Windows.Forms.Panel
        Me.TabControlGauges = New System.Windows.Forms.TabControl
        Me.TabPageSite = New System.Windows.Forms.TabPage
        Me.TabPageEquat = New System.Windows.Forms.TabPage
        Me.TabPageAltaz = New System.Windows.Forms.TabPage
        Me.TabPageScope = New System.Windows.Forms.TabPage
        Me.UserCtrlGauge2AxisCoordSite = New ScopeIII.Forms.UserCtrlGauge2AxisCoord
        Me.UserCtrlGauge3AxisCoordEquat = New ScopeIII.Forms.UserCtrlGauge3AxisCoord
        Me.UserCtrlGauge3AxisCoordSiteAxes = New ScopeIII.Forms.UserCtrlGauge3AxisCoord
        Me.UserCtrlGauge3AxisCoordScopeAxes = New ScopeIII.Forms.UserCtrlGauge3AxisCoord
        Me.UserCtrlGauge3AxisCoordTilt = New ScopeIII.Forms.UserCtrlGauge3AxisCoord
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlScopePilot.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.pnlTrackRates.SuspendLayout()
        Me.TabControlMain.SuspendLayout()
        Me.TabPageActions.SuspendLayout()
        Me.TabPageAlignment.SuspendLayout()
        Me.PnlAlignment.SuspendLayout()
        Me.TabPageTrackRates.SuspendLayout()
        Me.TabPageGrids.SuspendLayout()
        Me.PnlGrids.SuspendLayout()
        Me.TabPageAnalyze.SuspendLayout()
        Me.PnlAnalyze.SuspendLayout()
        Me.TabPageTilts.SuspendLayout()
        Me.PnlTilts.SuspendLayout()
        Me.TabControlGauges.SuspendLayout()
        Me.TabPageSite.SuspendLayout()
        Me.TabPageEquat.SuspendLayout()
        Me.TabPageAltaz.SuspendLayout()
        Me.TabPageScope.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbBxRates
        '
        Me.cmbBxRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBxRates.Location = New System.Drawing.Point(14, 37)
        Me.cmbBxRates.Name = "cmbBxRates"
        Me.cmbBxRates.Size = New System.Drawing.Size(178, 21)
        Me.cmbBxRates.TabIndex = 1
        '
        'cmbBxAlignment
        '
        Me.cmbBxAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBxAlignment.Location = New System.Drawing.Point(14, 10)
        Me.cmbBxAlignment.Name = "cmbBxAlignment"
        Me.cmbBxAlignment.Size = New System.Drawing.Size(178, 21)
        Me.cmbBxAlignment.TabIndex = 0
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
        'btnOneTwo
        '
        Me.btnOneTwo.BackColor = System.Drawing.SystemColors.Control
        Me.btnOneTwo.Enabled = False
        Me.btnOneTwo.Location = New System.Drawing.Point(230, 36)
        Me.btnOneTwo.Name = "btnOneTwo"
        Me.btnOneTwo.Size = New System.Drawing.Size(86, 21)
        Me.btnOneTwo.TabIndex = 3
        Me.btnOneTwo.Text = "Edit Inits"
        Me.btnOneTwo.UseVisualStyleBackColor = False
        '
        'MVPUserCtrl2MeasurementsGaugeBase
        '
        Me.MVPUserCtrl2MeasurementsGaugeBase.BackColor = System.Drawing.Color.RoyalBlue
        Me.MVPUserCtrl2MeasurementsGaugeBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MVPUserCtrl2MeasurementsGaugeBase.IRenderer = Nothing
        Me.MVPUserCtrl2MeasurementsGaugeBase.Location = New System.Drawing.Point(0, 0)
        Me.MVPUserCtrl2MeasurementsGaugeBase.Name = "MVPUserCtrl2MeasurementsGaugeBase"
        Me.MVPUserCtrl2MeasurementsGaugeBase.Size = New System.Drawing.Size(530, 530)
        Me.MVPUserCtrl2MeasurementsGaugeBase.TabIndex = 21
        '
        'chBxDisplayInits
        '
        Me.chBxDisplayInits.BackColor = System.Drawing.SystemColors.Control
        Me.chBxDisplayInits.Location = New System.Drawing.Point(230, 10)
        Me.chBxDisplayInits.Name = "chBxDisplayInits"
        Me.chBxDisplayInits.Size = New System.Drawing.Size(84, 18)
        Me.chBxDisplayInits.TabIndex = 2
        Me.chBxDisplayInits.Text = "Display Inits"
        Me.chBxDisplayInits.UseVisualStyleBackColor = False
        '
        'chBxScopeAltazGrid
        '
        Me.chBxScopeAltazGrid.BackColor = System.Drawing.SystemColors.Control
        Me.chBxScopeAltazGrid.Location = New System.Drawing.Point(107, 45)
        Me.chBxScopeAltazGrid.Name = "chBxScopeAltazGrid"
        Me.chBxScopeAltazGrid.Size = New System.Drawing.Size(127, 20)
        Me.chBxScopeAltazGrid.TabIndex = 6
        Me.chBxScopeAltazGrid.Text = "Scope Altaz Grid"
        Me.chBxScopeAltazGrid.UseVisualStyleBackColor = False
        '
        'lblGridLinesPer90Deg
        '
        Me.lblGridLinesPer90Deg.BackColor = System.Drawing.SystemColors.Control
        Me.lblGridLinesPer90Deg.Location = New System.Drawing.Point(310, 28)
        Me.lblGridLinesPer90Deg.Name = "lblGridLinesPer90Deg"
        Me.lblGridLinesPer90Deg.Size = New System.Drawing.Size(104, 18)
        Me.lblGridLinesPer90Deg.TabIndex = 18
        Me.lblGridLinesPer90Deg.Text = "Grid lines per 90 deg"
        Me.lblGridLinesPer90Deg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txBxGridLinesPerQuadrant
        '
        Me.txBxGridLinesPerQuadrant.Location = New System.Drawing.Point(285, 27)
        Me.txBxGridLinesPerQuadrant.Name = "txBxGridLinesPerQuadrant"
        Me.txBxGridLinesPerQuadrant.Size = New System.Drawing.Size(22, 20)
        Me.txBxGridLinesPerQuadrant.TabIndex = 7
        Me.txBxGridLinesPerQuadrant.Text = "3"
        Me.txBxGridLinesPerQuadrant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chBxCelestialGrid
        '
        Me.chBxCelestialGrid.BackColor = System.Drawing.SystemColors.Control
        Me.chBxCelestialGrid.Location = New System.Drawing.Point(107, 27)
        Me.chBxCelestialGrid.Name = "chBxCelestialGrid"
        Me.chBxCelestialGrid.Size = New System.Drawing.Size(127, 20)
        Me.chBxCelestialGrid.TabIndex = 5
        Me.chBxCelestialGrid.Text = "Celestial Grid"
        Me.chBxCelestialGrid.UseVisualStyleBackColor = False
        '
        'chBxSiteGrid
        '
        Me.chBxSiteGrid.BackColor = System.Drawing.SystemColors.Control
        Me.chBxSiteGrid.Location = New System.Drawing.Point(107, 9)
        Me.chBxSiteGrid.Name = "chBxSiteGrid"
        Me.chBxSiteGrid.Size = New System.Drawing.Size(127, 20)
        Me.chBxSiteGrid.TabIndex = 4
        Me.chBxSiteGrid.Text = "Site Grid"
        Me.chBxSiteGrid.UseVisualStyleBackColor = False
        '
        'btnQueryDatafiles
        '
        Me.btnQueryDatafiles.AutoSize = True
        Me.btnQueryDatafiles.BackColor = System.Drawing.SystemColors.Control
        Me.btnQueryDatafiles.ForeColor = System.Drawing.Color.Black
        Me.btnQueryDatafiles.Location = New System.Drawing.Point(216, 23)
        Me.btnQueryDatafiles.Name = "btnQueryDatafiles"
        Me.btnQueryDatafiles.Size = New System.Drawing.Size(86, 23)
        Me.btnQueryDatafiles.TabIndex = 2
        Me.btnQueryDatafiles.Text = "Object Library"
        Me.btnQueryDatafiles.UseVisualStyleBackColor = False
        '
        'btnQuit
        '
        Me.btnQuit.AutoSize = True
        Me.btnQuit.BackColor = System.Drawing.SystemColors.Control
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuit.ForeColor = System.Drawing.Color.Black
        Me.btnQuit.Location = New System.Drawing.Point(713, 2)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(67, 23)
        Me.btnQuit.TabIndex = 0
        Me.btnQuit.Text = "Quit"
        Me.btnQuit.UseVisualStyleBackColor = False
        '
        'pnlScopePilot
        '
        Me.pnlScopePilot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlScopePilot.BackColor = System.Drawing.SystemColors.Control
        Me.pnlScopePilot.Controls.Add(Me.MVPUserCtrl2MeasurementsGaugeBase)
        Me.pnlScopePilot.Location = New System.Drawing.Point(251, 103)
        Me.pnlScopePilot.Name = "pnlScopePilot"
        Me.pnlScopePilot.Size = New System.Drawing.Size(530, 530)
        Me.pnlScopePilot.TabIndex = 40
        '
        'lblMaxValue
        '
        Me.lblMaxValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblMaxValue.Location = New System.Drawing.Point(360, 37)
        Me.lblMaxValue.Name = "lblMaxValue"
        Me.lblMaxValue.Size = New System.Drawing.Size(54, 18)
        Me.lblMaxValue.TabIndex = 25
        Me.lblMaxValue.Text = """/s max"
        Me.lblMaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txBxMaxValue
        '
        Me.txBxMaxValue.Location = New System.Drawing.Point(330, 36)
        Me.txBxMaxValue.Name = "txBxMaxValue"
        Me.txBxMaxValue.Size = New System.Drawing.Size(27, 20)
        Me.txBxMaxValue.TabIndex = 24
        Me.txBxMaxValue.Text = "15.2"
        Me.txBxMaxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMinValue
        '
        Me.lblMinValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblMinValue.Location = New System.Drawing.Point(360, 15)
        Me.lblMinValue.Name = "lblMinValue"
        Me.lblMinValue.Size = New System.Drawing.Size(54, 18)
        Me.lblMinValue.TabIndex = 23
        Me.lblMinValue.Text = """/s min"
        Me.lblMinValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txBxMinValue
        '
        Me.txBxMinValue.Location = New System.Drawing.Point(330, 14)
        Me.txBxMinValue.Name = "txBxMinValue"
        Me.txBxMinValue.Size = New System.Drawing.Size(27, 20)
        Me.txBxMinValue.TabIndex = 22
        Me.txBxMinValue.Text = "14.2"
        Me.txBxMinValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chBxAnalyzeTrackRateSec
        '
        Me.chBxAnalyzeTrackRateSec.BackColor = System.Drawing.SystemColors.Control
        Me.chBxAnalyzeTrackRateSec.Location = New System.Drawing.Point(109, 26)
        Me.chBxAnalyzeTrackRateSec.Name = "chBxAnalyzeTrackRateSec"
        Me.chBxAnalyzeTrackRateSec.Size = New System.Drawing.Size(175, 20)
        Me.chBxAnalyzeTrackRateSec.TabIndex = 21
        Me.chBxAnalyzeTrackRateSec.Text = "Analyze Secondary Track Rate"
        Me.chBxAnalyzeTrackRateSec.UseVisualStyleBackColor = False
        '
        'chBxAnalyzeFieldRotationAngle
        '
        Me.chBxAnalyzeFieldRotationAngle.BackColor = System.Drawing.SystemColors.Control
        Me.chBxAnalyzeFieldRotationAngle.Location = New System.Drawing.Point(109, 43)
        Me.chBxAnalyzeFieldRotationAngle.Name = "chBxAnalyzeFieldRotationAngle"
        Me.chBxAnalyzeFieldRotationAngle.Size = New System.Drawing.Size(175, 20)
        Me.chBxAnalyzeFieldRotationAngle.TabIndex = 20
        Me.chBxAnalyzeFieldRotationAngle.Text = "Analyze Field Rotation Angle"
        Me.chBxAnalyzeFieldRotationAngle.UseVisualStyleBackColor = False
        '
        'chBxAnalyzeTrackRatePri
        '
        Me.chBxAnalyzeTrackRatePri.BackColor = System.Drawing.SystemColors.Control
        Me.chBxAnalyzeTrackRatePri.Location = New System.Drawing.Point(109, 8)
        Me.chBxAnalyzeTrackRatePri.Name = "chBxAnalyzeTrackRatePri"
        Me.chBxAnalyzeTrackRatePri.Size = New System.Drawing.Size(175, 20)
        Me.chBxAnalyzeTrackRatePri.TabIndex = 19
        Me.chBxAnalyzeTrackRatePri.Text = "Analyze Primary Track Rate"
        Me.chBxAnalyzeTrackRatePri.UseVisualStyleBackColor = False
        '
        'btnCelestialErrors
        '
        Me.btnCelestialErrors.AutoSize = True
        Me.btnCelestialErrors.BackColor = System.Drawing.SystemColors.Control
        Me.btnCelestialErrors.ForeColor = System.Drawing.Color.Black
        Me.btnCelestialErrors.Location = New System.Drawing.Point(308, 23)
        Me.btnCelestialErrors.Name = "btnCelestialErrors"
        Me.btnCelestialErrors.Size = New System.Drawing.Size(86, 23)
        Me.btnCelestialErrors.TabIndex = 3
        Me.btnCelestialErrors.Text = "Celestial Errors"
        Me.btnCelestialErrors.UseVisualStyleBackColor = False
        '
        'btnTrack
        '
        Me.btnTrack.AutoSize = True
        Me.btnTrack.BackColor = System.Drawing.SystemColors.Control
        Me.btnTrack.ForeColor = System.Drawing.Color.Black
        Me.btnTrack.Location = New System.Drawing.Point(124, 23)
        Me.btnTrack.Name = "btnTrack"
        Me.btnTrack.Size = New System.Drawing.Size(86, 23)
        Me.btnTrack.TabIndex = 1
        Me.btnTrack.Text = "Tracking On"
        Me.btnTrack.UseVisualStyleBackColor = False
        '
        'pnlActions
        '
        Me.pnlActions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlActions.BackgroundImage = CType(resources.GetObject("pnlActions.BackgroundImage"), System.Drawing.Image)
        Me.pnlActions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlActions.Controls.Add(Me.btnTrack)
        Me.pnlActions.Controls.Add(Me.btnQueryDatafiles)
        Me.pnlActions.Controls.Add(Me.btnCelestialErrors)
        Me.pnlActions.Location = New System.Drawing.Point(0, 0)
        Me.pnlActions.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(520, 71)
        Me.pnlActions.TabIndex = 47
        '
        'lblTierAxisTrackRate
        '
        Me.lblTierAxisTrackRate.Image = CType(resources.GetObject("lblTierAxisTrackRate.Image"), System.Drawing.Image)
        Me.lblTierAxisTrackRate.Location = New System.Drawing.Point(69, 46)
        Me.lblTierAxisTrackRate.Name = "lblTierAxisTrackRate"
        Me.lblTierAxisTrackRate.Size = New System.Drawing.Size(301, 25)
        Me.lblTierAxisTrackRate.TabIndex = 17
        Me.lblTierAxisTrackRate.Text = "Tiertiary Axis"
        Me.lblTierAxisTrackRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPriAxisTrackRate
        '
        Me.lblPriAxisTrackRate.Image = CType(resources.GetObject("lblPriAxisTrackRate.Image"), System.Drawing.Image)
        Me.lblPriAxisTrackRate.Location = New System.Drawing.Point(69, -2)
        Me.lblPriAxisTrackRate.Name = "lblPriAxisTrackRate"
        Me.lblPriAxisTrackRate.Size = New System.Drawing.Size(301, 25)
        Me.lblPriAxisTrackRate.TabIndex = 7
        Me.lblPriAxisTrackRate.Text = "Primary Axis"
        Me.lblPriAxisTrackRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSecAxisTrackRate
        '
        Me.lblSecAxisTrackRate.Image = CType(resources.GetObject("lblSecAxisTrackRate.Image"), System.Drawing.Image)
        Me.lblSecAxisTrackRate.Location = New System.Drawing.Point(69, 22)
        Me.lblSecAxisTrackRate.Name = "lblSecAxisTrackRate"
        Me.lblSecAxisTrackRate.Size = New System.Drawing.Size(301, 25)
        Me.lblSecAxisTrackRate.TabIndex = 8
        Me.lblSecAxisTrackRate.Text = "Secondary Axis"
        Me.lblSecAxisTrackRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chBxKingRate
        '
        Me.chBxKingRate.AutoSize = True
        Me.chBxKingRate.BackColor = System.Drawing.SystemColors.Control
        Me.chBxKingRate.Location = New System.Drawing.Point(376, 5)
        Me.chBxKingRate.Name = "chBxKingRate"
        Me.chBxKingRate.Size = New System.Drawing.Size(73, 17)
        Me.chBxKingRate.TabIndex = 8
        Me.chBxKingRate.Text = "King Rate"
        Me.chBxKingRate.UseVisualStyleBackColor = False
        '
        'pnlTrackRates
        '
        Me.pnlTrackRates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTrackRates.BackColor = System.Drawing.SystemColors.Control
        Me.pnlTrackRates.BackgroundImage = CType(resources.GetObject("pnlTrackRates.BackgroundImage"), System.Drawing.Image)
        Me.pnlTrackRates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTrackRates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTrackRates.Controls.Add(Me.lblPriAxisTrackRate)
        Me.pnlTrackRates.Controls.Add(Me.lblTierAxisTrackRate)
        Me.pnlTrackRates.Controls.Add(Me.lblSecAxisTrackRate)
        Me.pnlTrackRates.Controls.Add(Me.chBxKingRate)
        Me.pnlTrackRates.Location = New System.Drawing.Point(0, 0)
        Me.pnlTrackRates.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlTrackRates.Name = "pnlTrackRates"
        Me.pnlTrackRates.Size = New System.Drawing.Size(520, 71)
        Me.pnlTrackRates.TabIndex = 25
        '
        'TabControlMain
        '
        Me.TabControlMain.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TabControlMain.Controls.Add(Me.TabPageActions)
        Me.TabControlMain.Controls.Add(Me.TabPageAlignment)
        Me.TabControlMain.Controls.Add(Me.TabPageTrackRates)
        Me.TabControlMain.Controls.Add(Me.TabPageGrids)
        Me.TabControlMain.Controls.Add(Me.TabPageAnalyze)
        Me.TabControlMain.Controls.Add(Me.TabPageTilts)
        Me.TabControlMain.Location = New System.Drawing.Point(255, 1)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(528, 100)
        Me.TabControlMain.TabIndex = 48
        '
        'TabPageActions
        '
        Me.TabPageActions.Controls.Add(Me.pnlActions)
        Me.TabPageActions.Location = New System.Drawing.Point(4, 25)
        Me.TabPageActions.Name = "TabPageActions"
        Me.TabPageActions.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageActions.Size = New System.Drawing.Size(520, 71)
        Me.TabPageActions.TabIndex = 0
        Me.TabPageActions.Text = "Actions"
        Me.TabPageActions.UseVisualStyleBackColor = True
        '
        'TabPageAlignment
        '
        Me.TabPageAlignment.Controls.Add(Me.PnlAlignment)
        Me.TabPageAlignment.Location = New System.Drawing.Point(4, 25)
        Me.TabPageAlignment.Name = "TabPageAlignment"
        Me.TabPageAlignment.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAlignment.Size = New System.Drawing.Size(520, 71)
        Me.TabPageAlignment.TabIndex = 1
        Me.TabPageAlignment.Text = "Alignment"
        Me.TabPageAlignment.UseVisualStyleBackColor = True
        '
        'PnlAlignment
        '
        Me.PnlAlignment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlAlignment.BackgroundImage = CType(resources.GetObject("PnlAlignment.BackgroundImage"), System.Drawing.Image)
        Me.PnlAlignment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlAlignment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlAlignment.Controls.Add(Me.lblMeridianFlip)
        Me.PnlAlignment.Controls.Add(Me.chBxMeridianFlipPointingWest)
        Me.PnlAlignment.Controls.Add(Me.chBxMeridianFlipPointingEast)
        Me.PnlAlignment.Controls.Add(Me.cmbBxAlignment)
        Me.PnlAlignment.Controls.Add(Me.btnOneTwo)
        Me.PnlAlignment.Controls.Add(Me.cmbBxRates)
        Me.PnlAlignment.Controls.Add(Me.chBxDisplayInits)
        Me.PnlAlignment.Location = New System.Drawing.Point(0, 0)
        Me.PnlAlignment.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlAlignment.Name = "PnlAlignment"
        Me.PnlAlignment.Size = New System.Drawing.Size(520, 71)
        Me.PnlAlignment.TabIndex = 0
        '
        'lblMeridianFlip
        '
        Me.lblMeridianFlip.Location = New System.Drawing.Point(356, 5)
        Me.lblMeridianFlip.Name = "lblMeridianFlip"
        Me.lblMeridianFlip.Size = New System.Drawing.Size(149, 23)
        Me.lblMeridianFlip.TabIndex = 7
        Me.lblMeridianFlip.Text = "Meridian Flip"
        Me.lblMeridianFlip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chBxMeridianFlipPointingWest
        '
        Me.chBxMeridianFlipPointingWest.BackColor = System.Drawing.SystemColors.Control
        Me.chBxMeridianFlipPointingWest.Location = New System.Drawing.Point(356, 27)
        Me.chBxMeridianFlipPointingWest.Name = "chBxMeridianFlipPointingWest"
        Me.chBxMeridianFlipPointingWest.Size = New System.Drawing.Size(149, 18)
        Me.chBxMeridianFlipPointingWest.TabIndex = 6
        Me.chBxMeridianFlipPointingWest.Text = "on east side pointing west"
        Me.chBxMeridianFlipPointingWest.UseVisualStyleBackColor = False
        '
        'chBxMeridianFlipPointingEast
        '
        Me.chBxMeridianFlipPointingEast.BackColor = System.Drawing.SystemColors.Control
        Me.chBxMeridianFlipPointingEast.Location = New System.Drawing.Point(356, 45)
        Me.chBxMeridianFlipPointingEast.Name = "chBxMeridianFlipPointingEast"
        Me.chBxMeridianFlipPointingEast.Size = New System.Drawing.Size(149, 18)
        Me.chBxMeridianFlipPointingEast.TabIndex = 5
        Me.chBxMeridianFlipPointingEast.Text = "on west side pointing east"
        Me.chBxMeridianFlipPointingEast.UseVisualStyleBackColor = False
        '
        'TabPageTrackRates
        '
        Me.TabPageTrackRates.Controls.Add(Me.pnlTrackRates)
        Me.TabPageTrackRates.Location = New System.Drawing.Point(4, 25)
        Me.TabPageTrackRates.Name = "TabPageTrackRates"
        Me.TabPageTrackRates.Size = New System.Drawing.Size(520, 71)
        Me.TabPageTrackRates.TabIndex = 5
        Me.TabPageTrackRates.Text = "Track Rates"
        Me.TabPageTrackRates.UseVisualStyleBackColor = True
        '
        'TabPageGrids
        '
        Me.TabPageGrids.Controls.Add(Me.PnlGrids)
        Me.TabPageGrids.Location = New System.Drawing.Point(4, 25)
        Me.TabPageGrids.Name = "TabPageGrids"
        Me.TabPageGrids.Size = New System.Drawing.Size(520, 71)
        Me.TabPageGrids.TabIndex = 2
        Me.TabPageGrids.Text = "Grids"
        Me.TabPageGrids.UseVisualStyleBackColor = True
        '
        'PnlGrids
        '
        Me.PnlGrids.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlGrids.BackgroundImage = CType(resources.GetObject("PnlGrids.BackgroundImage"), System.Drawing.Image)
        Me.PnlGrids.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlGrids.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlGrids.Controls.Add(Me.chBxSiteGrid)
        Me.PnlGrids.Controls.Add(Me.chBxScopeAltazGrid)
        Me.PnlGrids.Controls.Add(Me.chBxCelestialGrid)
        Me.PnlGrids.Controls.Add(Me.txBxGridLinesPerQuadrant)
        Me.PnlGrids.Controls.Add(Me.lblGridLinesPer90Deg)
        Me.PnlGrids.Location = New System.Drawing.Point(0, 0)
        Me.PnlGrids.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlGrids.Name = "PnlGrids"
        Me.PnlGrids.Size = New System.Drawing.Size(520, 71)
        Me.PnlGrids.TabIndex = 0
        '
        'TabPageAnalyze
        '
        Me.TabPageAnalyze.Controls.Add(Me.PnlAnalyze)
        Me.TabPageAnalyze.Location = New System.Drawing.Point(4, 25)
        Me.TabPageAnalyze.Name = "TabPageAnalyze"
        Me.TabPageAnalyze.Size = New System.Drawing.Size(520, 71)
        Me.TabPageAnalyze.TabIndex = 3
        Me.TabPageAnalyze.Text = "Analyze"
        Me.TabPageAnalyze.UseVisualStyleBackColor = True
        '
        'PnlAnalyze
        '
        Me.PnlAnalyze.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlAnalyze.BackgroundImage = CType(resources.GetObject("PnlAnalyze.BackgroundImage"), System.Drawing.Image)
        Me.PnlAnalyze.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlAnalyze.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlAnalyze.Controls.Add(Me.chBxAnalyzeTrackRateSec)
        Me.PnlAnalyze.Controls.Add(Me.lblMaxValue)
        Me.PnlAnalyze.Controls.Add(Me.chBxAnalyzeFieldRotationAngle)
        Me.PnlAnalyze.Controls.Add(Me.lblMinValue)
        Me.PnlAnalyze.Controls.Add(Me.chBxAnalyzeTrackRatePri)
        Me.PnlAnalyze.Controls.Add(Me.txBxMaxValue)
        Me.PnlAnalyze.Controls.Add(Me.txBxMinValue)
        Me.PnlAnalyze.Location = New System.Drawing.Point(0, 0)
        Me.PnlAnalyze.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlAnalyze.Name = "PnlAnalyze"
        Me.PnlAnalyze.Size = New System.Drawing.Size(520, 71)
        Me.PnlAnalyze.TabIndex = 0
        '
        'TabPageTilts
        '
        Me.TabPageTilts.Controls.Add(Me.PnlTilts)
        Me.TabPageTilts.Location = New System.Drawing.Point(4, 25)
        Me.TabPageTilts.Name = "TabPageTilts"
        Me.TabPageTilts.Size = New System.Drawing.Size(520, 71)
        Me.TabPageTilts.TabIndex = 4
        Me.TabPageTilts.Text = "Tilts"
        Me.TabPageTilts.UseVisualStyleBackColor = True
        '
        'PnlTilts
        '
        Me.PnlTilts.Controls.Add(Me.UserCtrlGauge3AxisCoordTilt)
        Me.PnlTilts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlTilts.Location = New System.Drawing.Point(0, 0)
        Me.PnlTilts.Name = "PnlTilts"
        Me.PnlTilts.Size = New System.Drawing.Size(520, 71)
        Me.PnlTilts.TabIndex = 0
        '
        'TabControlGauges
        '
        Me.TabControlGauges.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabControlGauges.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TabControlGauges.Controls.Add(Me.TabPageSite)
        Me.TabControlGauges.Controls.Add(Me.TabPageEquat)
        Me.TabControlGauges.Controls.Add(Me.TabPageAltaz)
        Me.TabControlGauges.Controls.Add(Me.TabPageScope)
        Me.TabControlGauges.Location = New System.Drawing.Point(0, 1)
        Me.TabControlGauges.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControlGauges.Name = "TabControlGauges"
        Me.TabControlGauges.SelectedIndex = 0
        Me.TabControlGauges.Size = New System.Drawing.Size(252, 632)
        Me.TabControlGauges.TabIndex = 49
        '
        'TabPageSite
        '
        Me.TabPageSite.Controls.Add(Me.UserCtrlGauge2AxisCoordSite)
        Me.TabPageSite.Location = New System.Drawing.Point(4, 25)
        Me.TabPageSite.Name = "TabPageSite"
        Me.TabPageSite.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSite.Size = New System.Drawing.Size(244, 603)
        Me.TabPageSite.TabIndex = 0
        Me.TabPageSite.Text = "Site"
        Me.TabPageSite.UseVisualStyleBackColor = True
        '
        'TabPageEquat
        '
        Me.TabPageEquat.Controls.Add(Me.UserCtrlGauge3AxisCoordEquat)
        Me.TabPageEquat.Location = New System.Drawing.Point(4, 25)
        Me.TabPageEquat.Name = "TabPageEquat"
        Me.TabPageEquat.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEquat.Size = New System.Drawing.Size(244, 603)
        Me.TabPageEquat.TabIndex = 1
        Me.TabPageEquat.Text = "Equatorial"
        Me.TabPageEquat.UseVisualStyleBackColor = True
        '
        'TabPageAltaz
        '
        Me.TabPageAltaz.Controls.Add(Me.UserCtrlGauge3AxisCoordSiteAxes)
        Me.TabPageAltaz.Location = New System.Drawing.Point(4, 25)
        Me.TabPageAltaz.Name = "TabPageAltaz"
        Me.TabPageAltaz.Size = New System.Drawing.Size(244, 603)
        Me.TabPageAltaz.TabIndex = 2
        Me.TabPageAltaz.Text = "Altazimuth"
        Me.TabPageAltaz.UseVisualStyleBackColor = True
        '
        'TabPageScope
        '
        Me.TabPageScope.Controls.Add(Me.UserCtrlGauge3AxisCoordScopeAxes)
        Me.TabPageScope.Location = New System.Drawing.Point(4, 25)
        Me.TabPageScope.Name = "TabPageScope"
        Me.TabPageScope.Size = New System.Drawing.Size(244, 603)
        Me.TabPageScope.TabIndex = 3
        Me.TabPageScope.Text = "Scope"
        Me.TabPageScope.UseVisualStyleBackColor = True
        '
        'UserCtrlGauge2AxisCoordSite
        '
        Me.UserCtrlGauge2AxisCoordSite.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlGauge2AxisCoordSite.BackColor = System.Drawing.SystemColors.Control
        Me.UserCtrlGauge2AxisCoordSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UserCtrlGauge2AxisCoordSite.GaugeLayout = Nothing
        Me.UserCtrlGauge2AxisCoordSite.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGauge2AxisCoordSite.Margin = New System.Windows.Forms.Padding(0)
        Me.UserCtrlGauge2AxisCoordSite.Name = "UserCtrlGauge2AxisCoordSite"
        Me.UserCtrlGauge2AxisCoordSite.Size = New System.Drawing.Size(244, 603)
        Me.UserCtrlGauge2AxisCoordSite.TabIndex = 16
        '
        'UserCtrlGauge3AxisCoordEquat
        '
        Me.UserCtrlGauge3AxisCoordEquat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlGauge3AxisCoordEquat.BackColor = System.Drawing.SystemColors.Control
        Me.UserCtrlGauge3AxisCoordEquat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UserCtrlGauge3AxisCoordEquat.GaugeLayout = Nothing
        Me.UserCtrlGauge3AxisCoordEquat.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGauge3AxisCoordEquat.Margin = New System.Windows.Forms.Padding(0)
        Me.UserCtrlGauge3AxisCoordEquat.Name = "UserCtrlGauge3AxisCoordEquat"
        Me.UserCtrlGauge3AxisCoordEquat.Size = New System.Drawing.Size(244, 603)
        Me.UserCtrlGauge3AxisCoordEquat.TabIndex = 22
        '
        'UserCtrlGauge3AxisCoordSiteAxes
        '
        Me.UserCtrlGauge3AxisCoordSiteAxes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlGauge3AxisCoordSiteAxes.BackColor = System.Drawing.SystemColors.Control
        Me.UserCtrlGauge3AxisCoordSiteAxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UserCtrlGauge3AxisCoordSiteAxes.GaugeLayout = Nothing
        Me.UserCtrlGauge3AxisCoordSiteAxes.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGauge3AxisCoordSiteAxes.Margin = New System.Windows.Forms.Padding(0)
        Me.UserCtrlGauge3AxisCoordSiteAxes.Name = "UserCtrlGauge3AxisCoordSiteAxes"
        Me.UserCtrlGauge3AxisCoordSiteAxes.Size = New System.Drawing.Size(244, 603)
        Me.UserCtrlGauge3AxisCoordSiteAxes.TabIndex = 24
        '
        'UserCtrlGauge3AxisCoordScopeAxes
        '
        Me.UserCtrlGauge3AxisCoordScopeAxes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlGauge3AxisCoordScopeAxes.BackColor = System.Drawing.SystemColors.Control
        Me.UserCtrlGauge3AxisCoordScopeAxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UserCtrlGauge3AxisCoordScopeAxes.GaugeLayout = Nothing
        Me.UserCtrlGauge3AxisCoordScopeAxes.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGauge3AxisCoordScopeAxes.Margin = New System.Windows.Forms.Padding(0)
        Me.UserCtrlGauge3AxisCoordScopeAxes.Name = "UserCtrlGauge3AxisCoordScopeAxes"
        Me.UserCtrlGauge3AxisCoordScopeAxes.Size = New System.Drawing.Size(244, 603)
        Me.UserCtrlGauge3AxisCoordScopeAxes.TabIndex = 23
        '
        'UserCtrlGauge3AxisCoordTilt
        '
        Me.UserCtrlGauge3AxisCoordTilt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlGauge3AxisCoordTilt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UserCtrlGauge3AxisCoordTilt.GaugeLayout = Nothing
        Me.UserCtrlGauge3AxisCoordTilt.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGauge3AxisCoordTilt.Margin = New System.Windows.Forms.Padding(0)
        Me.UserCtrlGauge3AxisCoordTilt.Name = "UserCtrlGauge3AxisCoordTilt"
        Me.UserCtrlGauge3AxisCoordTilt.Size = New System.Drawing.Size(520, 71)
        Me.UserCtrlGauge3AxisCoordTilt.TabIndex = 0
        '
        'FrmConvert
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.RoyalBlue
        Me.CancelButton = Me.btnQuit
        Me.ClientSize = New System.Drawing.Size(781, 636)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.pnlScopePilot)
        Me.Controls.Add(Me.TabControlGauges)
        Me.Controls.Add(Me.TabControlMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmConvert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Convert Coordinates"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlScopePilot.ResumeLayout(False)
        Me.pnlActions.ResumeLayout(False)
        Me.pnlActions.PerformLayout()
        Me.pnlTrackRates.ResumeLayout(False)
        Me.pnlTrackRates.PerformLayout()
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPageActions.ResumeLayout(False)
        Me.TabPageAlignment.ResumeLayout(False)
        Me.PnlAlignment.ResumeLayout(False)
        Me.TabPageTrackRates.ResumeLayout(False)
        Me.TabPageGrids.ResumeLayout(False)
        Me.PnlGrids.ResumeLayout(False)
        Me.PnlGrids.PerformLayout()
        Me.TabPageAnalyze.ResumeLayout(False)
        Me.PnlAnalyze.ResumeLayout(False)
        Me.PnlAnalyze.PerformLayout()
        Me.TabPageTilts.ResumeLayout(False)
        Me.PnlTilts.ResumeLayout(False)
        Me.TabControlGauges.ResumeLayout(False)
        Me.TabPageSite.ResumeLayout(False)
        Me.TabPageEquat.ResumeLayout(False)
        Me.TabPageAltaz.ResumeLayout(False)
        Me.TabPageScope.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event ClosingForm()
    Public Event RatesChanged()
    Public Event AlignmentChanged()
    Public Event OneTwoClick()
    Public Event DisplayInitsCheckChanged()
    Public Event QueryDatafiles()
    Public Event CelestialErrors()
    Public Event MeridianFlipStateChanged(ByRef state As ISFT)
    Public Event SiteGrid(ByVal checked As Boolean)
    Public Event CelestialGrid(ByVal checked As Boolean)
    Public Event ScopeAltazGrid(ByVal checked As Boolean)
    Public Event GridLinesPerQuadrant(ByVal count As Int32)
    Public Event Track(ByVal checked As Boolean)
    Public Event AnalyzeTrackRatePri(ByVal checked As Boolean)
    Public Event MinValue(ByVal value As Double)
    Public Event MaxValue(ByVal value As Double)
    Public Event AnalyzeTrackRateSec(ByVal checked As Boolean)
    Public Event AnalyzeFieldRotationAngle(ByVal checked As Boolean)
    Public Event KingRate(ByVal checked As Boolean)

    Private pTrackState As Boolean

    Public Property DisplayInits() As Boolean
        Get
            Return chBxDisplayInits.Checked
        End Get
        Set(ByVal value As Boolean)
            chBxDisplayInits.Checked = value
        End Set
    End Property

    Public Property CmbBxRatesSelectedIndex() As Integer
        Get
            Return cmbBxRates.SelectedIndex
        End Get
        Set(ByVal Value As Integer)
            cmbBxRates.SelectedIndex = Value
        End Set
    End Property

    Public Property CmbBxAlignmentSelectedIndex() As Integer
        Get
            Return cmbBxAlignment.SelectedIndex
        End Get
        Set(ByVal Value As Integer)
            cmbBxAlignment.SelectedIndex = Value
        End Set
    End Property

    Public Property CmbBxRatesSelectedItem() As Object
        Get
            Return cmbBxRates.SelectedItem
        End Get
        Set(ByVal Value As Object)
            cmbBxRates.SelectedItem = Value
        End Set
    End Property

    Public Property CmbBxAlignmentSelectedItem() As Object
        Get
            Return cmbBxAlignment.SelectedItem
        End Get
        Set(ByVal Value As Object)
            cmbBxAlignment.SelectedItem = Value
        End Set
    End Property

    Public Property CmbBxRatesDataSource() As Object
        Get
            Return cmbBxRates.DataSource
        End Get
        Set(ByVal Value As Object)
            cmbBxRates.DataSource = Value
        End Set
    End Property

    Public Property CmbBxAlignmentDataSource() As Object
        Get
            Return cmbBxAlignment.DataSource
        End Get
        Set(ByVal Value As Object)
            cmbBxAlignment.DataSource = Value
            SetAlignmentToolTip()
        End Set
    End Property

    Public Property LblPriAxisTrackRateText() As String
        Get
            Return lblPriAxisTrackRate.Text
        End Get
        Set(ByVal Value As String)
            updatePriTrackRate(Value)
        End Set
    End Property

    Public Property LblSecAxisTrackRateText() As String
        Get
            Return lblSecAxisTrackRate.Text
        End Get
        Set(ByVal Value As String)
            updateSecTrackRate(Value)
        End Set
    End Property

    Public Property LblTierAxisTrackRateText() As String
        Get
            Return lblTierAxisTrackRate.Text
        End Get
        Set(ByVal Value As String)
            updateTierTrackRate(Value)
        End Set
    End Property

    Public Property SiteGridChecked() As Boolean
        Get
            Return chBxSiteGrid.Checked
        End Get
        Set(ByVal value As Boolean)
            chBxSiteGrid.Checked = value
        End Set
    End Property

    Public Property CelestialGridChecked() As Boolean
        Get
            Return chBxCelestialGrid.Checked
        End Get
        Set(ByVal value As Boolean)
            chBxCelestialGrid.Checked = value
        End Set
    End Property

    Public Property ScopeAltazGridChecked() As Boolean
        Get
            Return chBxScopeAltazGrid.Checked
        End Get
        Set(ByVal value As Boolean)
            chBxScopeAltazGrid.Checked = value
        End Set
    End Property

    Public Property KingRateGridChecked() As Boolean
        Get
            Return chBxKingRate.Checked
        End Get
        Set(ByVal value As Boolean)
            chBxKingRate.Checked = value
        End Set
    End Property

    Public Property ScopeAltazGridButtonColor() As Drawing.Color
        Get
            Return chBxScopeAltazGrid.ForeColor
        End Get
        Set(ByVal value As Drawing.Color)
            chBxScopeAltazGrid.ForeColor = value
        End Set
    End Property

    Public Property CelestialGridButtonColor() As Drawing.Color
        Get
            Return chBxCelestialGrid.ForeColor
        End Get
        Set(ByVal value As Drawing.Color)
            chBxCelestialGrid.ForeColor = value
        End Set
    End Property

    Public Property SiteGridButtonColor() As Drawing.Color
        Get
            Return chBxSiteGrid.ForeColor
        End Get
        Set(ByVal value As Drawing.Color)
            chBxSiteGrid.ForeColor = value
        End Set
    End Property

    Public Property GridLinesPerQuadrantText() As String
        Get
            Return txBxGridLinesPerQuadrant.Text
        End Get
        Set(ByVal value As String)
            txBxGridLinesPerQuadrant.Text = value
        End Set
    End Property

    Public Property AnalyzeMinValue() As Double
        Get
            Return Double.Parse(txBxMinValue.Text)
        End Get
        Set(ByVal value As Double)
            txBxMinValue.Text = CStr(value)
        End Set
    End Property

    Public Property AnalyzeMaxValue() As Double
        Get
            Return Double.Parse(txBxMaxValue.Text)
        End Get
        Set(ByVal value As Double)
            txBxMaxValue.Text = CStr(value)
        End Set
    End Property

    Public Sub SetAlignmentToolTip()
        ToolTip.SetToolTip(cmbBxAlignment, AlignmentStyle.ISFT.MatchKey(cmbBxAlignment.SelectedIndex).Description)
        ToolTip.IsBalloon = True
    End Sub

    Public Sub ConvertMatrixEnabled(ByVal enabled As Boolean)
        btnOneTwo.Enabled = enabled
        chBxDisplayInits.Enabled = enabled
    End Sub

    Public Sub KingRateEnabled(ByVal enabled As Boolean)
        chBxKingRate.Enabled = enabled
    End Sub

    ' MVPViewContainsGaugeCoordBase also handles MyBase.Load
    Private Sub FrmConvert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserCtrlGauge3AxisCoordTilt.Width += 1
    End Sub

    Private Sub FrmConvert_Closing(ByVal sender As System.Object, ByVal e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RaiseEvent ClosingForm()
    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        MyBase.Close()
    End Sub

    Private Sub btnOneTwo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOneTwo.Click
        RaiseEvent OneTwoClick()
    End Sub

    Private Sub chBxDisplayInits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxDisplayInits.CheckedChanged
        RaiseEvent DisplayInitsCheckChanged()
    End Sub

    Private Sub cmbBxRatesSelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBxRates.SelectionChangeCommitted
        RaiseEvent RatesChanged()
    End Sub

    Private Sub cmbBxAlignmentSelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBxAlignment.SelectionChangeCommitted
        RaiseEvent AlignmentChanged()
    End Sub

    Private pMeridianFlipUpdating As Boolean

    Private Sub chBxMeridianFlipPointingWest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxMeridianFlipPointingWest.CheckedChanged
        If Not pMeridianFlipUpdating Then
            pMeridianFlipUpdating = True
            chBxMeridianFlipPointingEast.Checked = False
            If chBxMeridianFlipPointingWest.Checked Then
                RaiseEvent MeridianFlipStateChanged(CType(MeridianFlipState.PointingWest, ISFT))
            Else
                RaiseEvent MeridianFlipStateChanged(Nothing)
            End If
            pMeridianFlipUpdating = False
        End If
    End Sub

    Private Sub chBxMeridianFlipPointingEast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxMeridianFlipPointingEast.CheckedChanged
        If Not pMeridianFlipUpdating Then
            pMeridianFlipUpdating = True
            chBxMeridianFlipPointingWest.Checked = False
            If chBxMeridianFlipPointingEast.Checked Then
                RaiseEvent MeridianFlipStateChanged(CType(MeridianFlipState.PointingEast, ISFT))
            Else
                RaiseEvent MeridianFlipStateChanged(Nothing)
            End If
            pMeridianFlipUpdating = False
        End If
    End Sub

    Private Sub chBxSiteGrid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxSiteGrid.CheckedChanged
        RaiseEvent SiteGrid(chBxSiteGrid.Checked)
    End Sub

    Private Sub chBxCelestialGrid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxCelestialGrid.CheckedChanged
        RaiseEvent CelestialGrid(chBxCelestialGrid.Checked)
    End Sub

    Private Sub chBxScopeAltazGrid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxScopeAltazGrid.CheckedChanged
        RaiseEvent ScopeAltazGrid(chBxScopeAltazGrid.Checked)
    End Sub

    Private Sub txBxGridLinesPerQuadrant_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txBxGridLinesPerQuadrant.Leave
        ValidateTextBoxAsRad()
    End Sub

    Private Sub chBxKingRate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxKingRate.CheckedChanged
        RaiseEvent KingRate(chBxKingRate.Checked)
    End Sub

    Private Sub btnQueryDatafiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQueryDatafiles.Click
        RaiseEvent QueryDatafiles()
    End Sub

    Private Sub btnCelestialErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCelestialErrors.Click
        RaiseEvent CelestialErrors()
    End Sub

    Private Sub btnTrack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrack.Click
        pTrackState = Not pTrackState
        If pTrackState Then
            btnTrack.Text = "Tracking Off"
        Else
            btnTrack.Text = "Tracking On"
        End If
        RaiseEvent Track(pTrackState)
    End Sub

    Private Sub chBxAnalyzeTrackRatePri_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxAnalyzeTrackRatePri.CheckedChanged
        RaiseEvent AnalyzeTrackRatePri(chBxAnalyzeTrackRatePri.Checked)
    End Sub

    Private Sub txBxMaxValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txBxMaxValue.TextChanged
        Dim value As Double
        If ScopeIII.Forms.Validate.GetInstance.ValidateTextBoxAsDouble(ErrorProvider, txBxMaxValue, value) Then
            RaiseEvent MaxValue(value)
        End If
    End Sub

    Private Sub txBxMinValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txBxMinValue.TextChanged
        Dim value As Double
        If ScopeIII.Forms.Validate.GetInstance.ValidateTextBoxAsDouble(ErrorProvider, txBxMinValue, value) Then
            RaiseEvent MinValue(value)
        End If
    End Sub

    Private Sub chBxAnalyzeTrackRateSec_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxAnalyzeTrackRateSec.CheckedChanged
        RaiseEvent AnalyzeTrackRateSec(chBxAnalyzeTrackRateSec.Checked)
    End Sub

    Private Sub chBxAnalyzeFieldRotationAngle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxAnalyzeFieldRotationAngle.CheckedChanged
        RaiseEvent AnalyzeFieldRotationAngle(chBxAnalyzeFieldRotationAngle.Checked)
    End Sub

    Protected Overridable Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

    End Sub

    Private Sub ValidateTextBoxAsRad()
        Dim count As Int32
        If ScopeIII.Forms.Validate.GetInstance.ValidateTextBoxAsInt_1_To_90(ErrorProvider, txBxGridLinesPerQuadrant, count) Then
            RaiseEvent GridLinesPerQuadrant(count)
        End If
    End Sub

    Private Sub updatePriTrackRate(ByVal value As String)
        If lblPriAxisTrackRate.InvokeRequired Then
            lblPriAxisTrackRate.Invoke(New DelegateStr(AddressOf updatePriTrackRate), New Object() {value})
        Else
            lblPriAxisTrackRate.Text = value
        End If
    End Sub

    Private Sub updateSecTrackRate(ByVal value As String)
        If lblSecAxisTrackRate.InvokeRequired Then
            lblSecAxisTrackRate.Invoke(New DelegateStr(AddressOf updateSecTrackRate), New Object() {value})
        Else
            lblSecAxisTrackRate.Text = value
        End If
    End Sub

    Private Sub updateTierTrackRate(ByVal value As String)
        If lblTierAxisTrackRate.InvokeRequired Then
            lblTierAxisTrackRate.Invoke(New DelegateStr(AddressOf updateTierTrackRate), New Object() {value})
        Else
            lblTierAxisTrackRate.Text = value
        End If
    End Sub
End Class
