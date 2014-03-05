Public Class FrmConvert
    Inherits MVPViewContainsGaugeCoordBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        UserCtrlGaugePosition.GaugeLayout = ScopeIII.Common.Layout.Vertical
        UserCtrlGauge2AxisCoordSite.GaugeLayout = ScopeIII.Common.Layout.Vertical
        buildGCLs()

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
    Friend WithEvents lblPriAxisTrackRate As System.Windows.Forms.Label
    Friend WithEvents lblSecAxisTrackRate As System.Windows.Forms.Label
    Friend WithEvents lblTiertiaryAxisTrackRate As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnOneTwo As System.Windows.Forms.Button
    Friend WithEvents MVPUserCtrl2MeasurementsGaugeBase As ScopeIII.Common.MVPUserCtrl2MeasurementsGaugeBase
    Friend WithEvents lblTiltTitle As System.Windows.Forms.Label
    Friend WithEvents PnlTilt As System.Windows.Forms.Panel
    Friend WithEvents UserCtrlGauge3AxisCoordCenter As ScopeIII.Forms.UserCtrlGauge3AxisCoord
    Friend WithEvents pnlOrient As System.Windows.Forms.Panel
    Friend WithEvents lblInitTitle As System.Windows.Forms.Label
    Friend WithEvents lblPointedToTitle As System.Windows.Forms.Label
    Friend WithEvents pnlPointedToPosition As System.Windows.Forms.Panel
    Friend WithEvents pnlTrackRates As System.Windows.Forms.Panel
    Friend WithEvents UserCtrlGaugePosition As ScopeIII.Forms.UserCtrlGaugePosition
    Friend WithEvents chBxCelestialGrid As System.Windows.Forms.CheckBox
    Friend WithEvents chBxSiteGrid As System.Windows.Forms.CheckBox
    Friend WithEvents UserCtrlGauge2AxisCoordSite As ScopeIII.Forms.UserCtrlGauge2AxisCoord
    Friend WithEvents txBxGridLinesPerQuadrant As System.Windows.Forms.TextBox
    Friend WithEvents lblGridLinesPerQuadrant As System.Windows.Forms.Label
    Friend WithEvents chBxScopeEquatGrid As System.Windows.Forms.CheckBox
    Friend WithEvents chBxScopeAltazGrid As System.Windows.Forms.CheckBox
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents chBxDisplayInits As System.Windows.Forms.CheckBox
    Friend WithEvents btnUpdateToCurrentSidT As System.Windows.Forms.Button
    Friend WithEvents lblTrackRates As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmConvert))
        Me.cmbBxRates = New System.Windows.Forms.ComboBox
        Me.cmbBxAlignment = New System.Windows.Forms.ComboBox
        Me.lblPriAxisTrackRate = New System.Windows.Forms.Label
        Me.lblSecAxisTrackRate = New System.Windows.Forms.Label
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lblTiertiaryAxisTrackRate = New System.Windows.Forms.Label
        Me.btnOneTwo = New System.Windows.Forms.Button
        Me.MVPUserCtrl2MeasurementsGaugeBase = New ScopeIII.Common.MVPUserCtrl2MeasurementsGaugeBase
        Me.lblTiltTitle = New System.Windows.Forms.Label
        Me.PnlTilt = New System.Windows.Forms.Panel
        Me.UserCtrlGauge3AxisCoordCenter = New ScopeIII.Forms.UserCtrlGauge3AxisCoord
        Me.pnlOrient = New System.Windows.Forms.Panel
        Me.chBxDisplayInits = New System.Windows.Forms.CheckBox
        Me.chBxScopeEquatGrid = New System.Windows.Forms.CheckBox
        Me.chBxScopeAltazGrid = New System.Windows.Forms.CheckBox
        Me.lblGridLinesPerQuadrant = New System.Windows.Forms.Label
        Me.txBxGridLinesPerQuadrant = New System.Windows.Forms.TextBox
        Me.UserCtrlGauge2AxisCoordSite = New ScopeIII.Forms.UserCtrlGauge2AxisCoord
        Me.chBxCelestialGrid = New System.Windows.Forms.CheckBox
        Me.chBxSiteGrid = New System.Windows.Forms.CheckBox
        Me.lblInitTitle = New System.Windows.Forms.Label
        Me.pnlPointedToPosition = New System.Windows.Forms.Panel
        Me.btnUpdateToCurrentSidT = New System.Windows.Forms.Button
        Me.lblPointedToTitle = New System.Windows.Forms.Label
        Me.pnlTrackRates = New System.Windows.Forms.Panel
        Me.lblTrackRates = New System.Windows.Forms.Label
        Me.UserCtrlGaugePosition = New ScopeIII.Forms.UserCtrlGaugePosition
        Me.btnQuit = New System.Windows.Forms.Button
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlTilt.SuspendLayout()
        Me.pnlOrient.SuspendLayout()
        Me.pnlPointedToPosition.SuspendLayout()
        Me.pnlTrackRates.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbBxRates
        '
        Me.cmbBxRates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbBxRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBxRates.Location = New System.Drawing.Point(11, 221)
        Me.cmbBxRates.Name = "cmbBxRates"
        Me.cmbBxRates.Size = New System.Drawing.Size(169, 21)
        Me.cmbBxRates.TabIndex = 0
        '
        'cmbBxAlignment
        '
        Me.cmbBxAlignment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbBxAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBxAlignment.Location = New System.Drawing.Point(11, 194)
        Me.cmbBxAlignment.Name = "cmbBxAlignment"
        Me.cmbBxAlignment.Size = New System.Drawing.Size(169, 21)
        Me.cmbBxAlignment.TabIndex = 1
        '
        'lblPriAxisTrackRate
        '
        Me.lblPriAxisTrackRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPriAxisTrackRate.Image = CType(resources.GetObject("lblPriAxisTrackRate.Image"), System.Drawing.Image)
        Me.lblPriAxisTrackRate.Location = New System.Drawing.Point(3, 8)
        Me.lblPriAxisTrackRate.Name = "lblPriAxisTrackRate"
        Me.lblPriAxisTrackRate.Size = New System.Drawing.Size(312, 24)
        Me.lblPriAxisTrackRate.TabIndex = 7
        Me.lblPriAxisTrackRate.Text = "Primary Axis (Az/RA) Track Rate:"
        Me.lblPriAxisTrackRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSecAxisTrackRate
        '
        Me.lblSecAxisTrackRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSecAxisTrackRate.Image = CType(resources.GetObject("lblSecAxisTrackRate.Image"), System.Drawing.Image)
        Me.lblSecAxisTrackRate.Location = New System.Drawing.Point(3, 32)
        Me.lblSecAxisTrackRate.Name = "lblSecAxisTrackRate"
        Me.lblSecAxisTrackRate.Size = New System.Drawing.Size(312, 24)
        Me.lblSecAxisTrackRate.TabIndex = 8
        Me.lblSecAxisTrackRate.Text = "Secondary Axis (Alt/Dec) Track Rate:"
        Me.lblSecAxisTrackRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'lblTiertiaryAxisTrackRate
        '
        Me.lblTiertiaryAxisTrackRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTiertiaryAxisTrackRate.Image = CType(resources.GetObject("lblTiertiaryAxisTrackRate.Image"), System.Drawing.Image)
        Me.lblTiertiaryAxisTrackRate.Location = New System.Drawing.Point(3, 56)
        Me.lblTiertiaryAxisTrackRate.Name = "lblTiertiaryAxisTrackRate"
        Me.lblTiertiaryAxisTrackRate.Size = New System.Drawing.Size(312, 24)
        Me.lblTiertiaryAxisTrackRate.TabIndex = 17
        Me.lblTiertiaryAxisTrackRate.Text = "Tiertiary Axis (FieldRotation) Track Rate:"
        Me.lblTiertiaryAxisTrackRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnOneTwo
        '
        Me.btnOneTwo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOneTwo.Location = New System.Drawing.Point(11, 248)
        Me.btnOneTwo.Name = "btnOneTwo"
        Me.btnOneTwo.Size = New System.Drawing.Size(80, 25)
        Me.btnOneTwo.TabIndex = 4
        Me.btnOneTwo.Text = "Edit Inits"
        '
        'MVPUserCtrl2MeasurementsGaugeBase
        '
        Me.MVPUserCtrl2MeasurementsGaugeBase.IMVPUserCtrlPresenter = Nothing
        Me.MVPUserCtrl2MeasurementsGaugeBase.IRenderer = Nothing
        Me.MVPUserCtrl2MeasurementsGaugeBase.Location = New System.Drawing.Point(212, 97)
        Me.MVPUserCtrl2MeasurementsGaugeBase.Name = "MVPUserCtrl2MeasurementsGaugeBase"
        Me.MVPUserCtrl2MeasurementsGaugeBase.Size = New System.Drawing.Size(318, 318)
        Me.MVPUserCtrl2MeasurementsGaugeBase.TabIndex = 21
        '
        'lblTiltTitle
        '
        Me.lblTiltTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTiltTitle.Location = New System.Drawing.Point(223, 420)
        Me.lblTiltTitle.Name = "lblTiltTitle"
        Me.lblTiltTitle.Size = New System.Drawing.Size(79, 16)
        Me.lblTiltTitle.TabIndex = 23
        Me.lblTiltTitle.Text = "Tilt Projection"
        '
        'PnlTilt
        '
        Me.PnlTilt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTilt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlTilt.Controls.Add(Me.UserCtrlGauge3AxisCoordCenter)
        Me.PnlTilt.Location = New System.Drawing.Point(212, 426)
        Me.PnlTilt.Name = "PnlTilt"
        Me.PnlTilt.Size = New System.Drawing.Size(516, 79)
        Me.PnlTilt.TabIndex = 22
        '
        'UserCtrlGauge3AxisCoordCenter
        '
        Me.UserCtrlGauge3AxisCoordCenter.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlGauge3AxisCoordCenter.GaugeLayout = Nothing
        Me.UserCtrlGauge3AxisCoordCenter.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrlGauge3AxisCoordCenter.Location = New System.Drawing.Point(-2, 6)
        Me.UserCtrlGauge3AxisCoordCenter.Name = "UserCtrlGauge3AxisCoordCenter"
        Me.UserCtrlGauge3AxisCoordCenter.Size = New System.Drawing.Size(511, 68)
        Me.UserCtrlGauge3AxisCoordCenter.TabIndex = 5
        '
        'pnlOrient
        '
        Me.pnlOrient.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlOrient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlOrient.Controls.Add(Me.chBxDisplayInits)
        Me.pnlOrient.Controls.Add(Me.chBxScopeEquatGrid)
        Me.pnlOrient.Controls.Add(Me.chBxScopeAltazGrid)
        Me.pnlOrient.Controls.Add(Me.lblGridLinesPerQuadrant)
        Me.pnlOrient.Controls.Add(Me.txBxGridLinesPerQuadrant)
        Me.pnlOrient.Controls.Add(Me.UserCtrlGauge2AxisCoordSite)
        Me.pnlOrient.Controls.Add(Me.chBxCelestialGrid)
        Me.pnlOrient.Controls.Add(Me.chBxSiteGrid)
        Me.pnlOrient.Controls.Add(Me.cmbBxAlignment)
        Me.pnlOrient.Controls.Add(Me.cmbBxRates)
        Me.pnlOrient.Controls.Add(Me.btnOneTwo)
        Me.pnlOrient.Location = New System.Drawing.Point(538, 46)
        Me.pnlOrient.Name = "pnlOrient"
        Me.pnlOrient.Size = New System.Drawing.Size(192, 369)
        Me.pnlOrient.TabIndex = 27
        '
        'chBxDisplayInits
        '
        Me.chBxDisplayInits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chBxDisplayInits.Location = New System.Drawing.Point(100, 248)
        Me.chBxDisplayInits.Name = "chBxDisplayInits"
        Me.chBxDisplayInits.Size = New System.Drawing.Size(85, 25)
        Me.chBxDisplayInits.TabIndex = 34
        Me.chBxDisplayInits.Text = "Display Inits"
        Me.chBxDisplayInits.UseVisualStyleBackColor = True
        '
        'chBxScopeEquatGrid
        '
        Me.chBxScopeEquatGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chBxScopeEquatGrid.Location = New System.Drawing.Point(100, 281)
        Me.chBxScopeEquatGrid.Name = "chBxScopeEquatGrid"
        Me.chBxScopeEquatGrid.Size = New System.Drawing.Size(88, 25)
        Me.chBxScopeEquatGrid.TabIndex = 33
        Me.chBxScopeEquatGrid.Text = "Scope Equat"
        Me.chBxScopeEquatGrid.UseVisualStyleBackColor = True
        '
        'chBxScopeAltazGrid
        '
        Me.chBxScopeAltazGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chBxScopeAltazGrid.Location = New System.Drawing.Point(11, 281)
        Me.chBxScopeAltazGrid.Name = "chBxScopeAltazGrid"
        Me.chBxScopeAltazGrid.Size = New System.Drawing.Size(83, 25)
        Me.chBxScopeAltazGrid.TabIndex = 19
        Me.chBxScopeAltazGrid.Text = "Scope Altaz"
        Me.chBxScopeAltazGrid.UseVisualStyleBackColor = True
        '
        'lblGridLinesPerQuadrant
        '
        Me.lblGridLinesPerQuadrant.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGridLinesPerQuadrant.AutoSize = True
        Me.lblGridLinesPerQuadrant.Location = New System.Drawing.Point(3, 346)
        Me.lblGridLinesPerQuadrant.Name = "lblGridLinesPerQuadrant"
        Me.lblGridLinesPerQuadrant.Size = New System.Drawing.Size(149, 13)
        Me.lblGridLinesPerQuadrant.TabIndex = 18
        Me.lblGridLinesPerQuadrant.Text = "Grid lines per 90 deg quadrant"
        '
        'txBxGridLinesPerQuadrant
        '
        Me.txBxGridLinesPerQuadrant.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txBxGridLinesPerQuadrant.Location = New System.Drawing.Point(156, 343)
        Me.txBxGridLinesPerQuadrant.Name = "txBxGridLinesPerQuadrant"
        Me.txBxGridLinesPerQuadrant.Size = New System.Drawing.Size(24, 20)
        Me.txBxGridLinesPerQuadrant.TabIndex = 17
        Me.txBxGridLinesPerQuadrant.Text = "3"
        Me.txBxGridLinesPerQuadrant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UserCtrlGauge2AxisCoordSite
        '
        Me.UserCtrlGauge2AxisCoordSite.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlGauge2AxisCoordSite.GaugeLayout = Nothing
        Me.UserCtrlGauge2AxisCoordSite.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrlGauge2AxisCoordSite.Location = New System.Drawing.Point(6, 3)
        Me.UserCtrlGauge2AxisCoordSite.Name = "UserCtrlGauge2AxisCoordSite"
        Me.UserCtrlGauge2AxisCoordSite.Size = New System.Drawing.Size(182, 185)
        Me.UserCtrlGauge2AxisCoordSite.TabIndex = 16
        '
        'chBxCelestialGrid
        '
        Me.chBxCelestialGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chBxCelestialGrid.Location = New System.Drawing.Point(100, 312)
        Me.chBxCelestialGrid.Name = "chBxCelestialGrid"
        Me.chBxCelestialGrid.Size = New System.Drawing.Size(85, 25)
        Me.chBxCelestialGrid.TabIndex = 15
        Me.chBxCelestialGrid.Text = "Celestial"
        Me.chBxCelestialGrid.UseVisualStyleBackColor = True
        '
        'chBxSiteGrid
        '
        Me.chBxSiteGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chBxSiteGrid.Checked = True
        Me.chBxSiteGrid.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chBxSiteGrid.Location = New System.Drawing.Point(11, 312)
        Me.chBxSiteGrid.Name = "chBxSiteGrid"
        Me.chBxSiteGrid.Size = New System.Drawing.Size(83, 25)
        Me.chBxSiteGrid.TabIndex = 14
        Me.chBxSiteGrid.Text = "Site"
        Me.chBxSiteGrid.UseVisualStyleBackColor = True
        '
        'lblInitTitle
        '
        Me.lblInitTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInitTitle.Location = New System.Drawing.Point(548, 38)
        Me.lblInitTitle.Name = "lblInitTitle"
        Me.lblInitTitle.Size = New System.Drawing.Size(58, 17)
        Me.lblInitTitle.TabIndex = 28
        Me.lblInitTitle.Text = "Orientation"
        '
        'pnlPointedToPosition
        '
        Me.pnlPointedToPosition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlPointedToPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPointedToPosition.Controls.Add(Me.btnUpdateToCurrentSidT)
        Me.pnlPointedToPosition.Location = New System.Drawing.Point(12, 12)
        Me.pnlPointedToPosition.Name = "pnlPointedToPosition"
        Me.pnlPointedToPosition.Size = New System.Drawing.Size(192, 493)
        Me.pnlPointedToPosition.TabIndex = 29
        '
        'btnUpdateToCurrentSidT
        '
        Me.btnUpdateToCurrentSidT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateToCurrentSidT.Location = New System.Drawing.Point(11, 462)
        Me.btnUpdateToCurrentSidT.Name = "btnUpdateToCurrentSidT"
        Me.btnUpdateToCurrentSidT.Size = New System.Drawing.Size(172, 23)
        Me.btnUpdateToCurrentSidT.TabIndex = 20
        Me.btnUpdateToCurrentSidT.Text = "Update To Current Sidereal Time"
        Me.btnUpdateToCurrentSidT.UseVisualStyleBackColor = True
        '
        'lblPointedToTitle
        '
        Me.lblPointedToTitle.Location = New System.Drawing.Point(22, 6)
        Me.lblPointedToTitle.Name = "lblPointedToTitle"
        Me.lblPointedToTitle.Size = New System.Drawing.Size(106, 16)
        Me.lblPointedToTitle.TabIndex = 30
        Me.lblPointedToTitle.Text = "Pointed To Position"
        '
        'pnlTrackRates
        '
        Me.pnlTrackRates.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTrackRates.Controls.Add(Me.lblPriAxisTrackRate)
        Me.pnlTrackRates.Controls.Add(Me.lblSecAxisTrackRate)
        Me.pnlTrackRates.Controls.Add(Me.lblTiertiaryAxisTrackRate)
        Me.pnlTrackRates.Location = New System.Drawing.Point(212, 12)
        Me.pnlTrackRates.Name = "pnlTrackRates"
        Me.pnlTrackRates.Size = New System.Drawing.Size(319, 85)
        Me.pnlTrackRates.TabIndex = 31
        '
        'lblTrackRates
        '
        Me.lblTrackRates.Location = New System.Drawing.Point(222, 4)
        Me.lblTrackRates.Name = "lblTrackRates"
        Me.lblTrackRates.Size = New System.Drawing.Size(80, 16)
        Me.lblTrackRates.TabIndex = 32
        Me.lblTrackRates.Text = "Tracking Rates"
        '
        'UserCtrlGaugePosition
        '
        Me.UserCtrlGaugePosition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlGaugePosition.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrlGaugePosition.Location = New System.Drawing.Point(17, 17)
        Me.UserCtrlGaugePosition.Name = "UserCtrlGaugePosition"
        Me.UserCtrlGaugePosition.Size = New System.Drawing.Size(187, 453)
        Me.UserCtrlGaugePosition.TabIndex = 0
        '
        'btnQuit
        '
        Me.btnQuit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuit.Location = New System.Drawing.Point(650, 12)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(80, 23)
        Me.btnQuit.TabIndex = 35
        Me.btnQuit.Text = "Quit"
        '
        'FrmConvert
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnQuit
        Me.ClientSize = New System.Drawing.Size(741, 511)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.UserCtrlGaugePosition)
        Me.Controls.Add(Me.lblTiltTitle)
        Me.Controls.Add(Me.lblTrackRates)
        Me.Controls.Add(Me.pnlTrackRates)
        Me.Controls.Add(Me.lblPointedToTitle)
        Me.Controls.Add(Me.pnlPointedToPosition)
        Me.Controls.Add(Me.lblInitTitle)
        Me.Controls.Add(Me.pnlOrient)
        Me.Controls.Add(Me.PnlTilt)
        Me.Controls.Add(Me.MVPUserCtrl2MeasurementsGaugeBase)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmConvert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Convert Coordinates and Show Tracking Rates"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlTilt.ResumeLayout(False)
        Me.pnlOrient.ResumeLayout(False)
        Me.pnlOrient.PerformLayout()
        Me.pnlPointedToPosition.ResumeLayout(False)
        Me.pnlTrackRates.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Overrides Function DefaultPresenter() As IMVPPresenter
        Return initDefaultPresenter(ConvertPresenter.GetInstance)
    End Function

    Public Event RatesChanged()
    Public Event AlignmentChanged()
    Public Event OneTwoClick()
    Public Event DisplayInitsCheckChanged()
    Public Event UpdateToCurrentSidT()
    Public Event SiteGrid(ByVal checked As Boolean)
    Public Event CelestialGrid(ByVal checked As Boolean)
    Public Event ScopeAltazGrid(ByVal checked As Boolean)
    Public Event ScopeEquatGrid(ByVal checked As Boolean)
    Public Event GridLinesPerQuadrant(ByVal count As Int32)

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
            lblPriAxisTrackRate.Text = Value
        End Set
    End Property

    Public Property LblSecAxisTrackRateText() As String
        Get
            Return lblSecAxisTrackRate.Text
        End Get
        Set(ByVal Value As String)
            lblSecAxisTrackRate.Text = Value
        End Set
    End Property

    Public Property LblTiertiaryAxisTrackRateText() As String
        Get
            Return lblTiertiaryAxisTrackRate.Text
        End Get
        Set(ByVal Value As String)
            lblTiertiaryAxisTrackRate.Text = Value
        End Set
    End Property

    Public Property ScopeEquatGridButtonColor() As Color
        Get
            Return chBxScopeEquatGrid.ForeColor
        End Get
        Set(ByVal value As Color)
            chBxScopeEquatGrid.ForeColor = value
        End Set
    End Property

    Public Property ScopeAltazGridButtonColor() As Color
        Get
            Return chBxScopeAltazGrid.ForeColor
        End Get
        Set(ByVal value As Color)
            chBxScopeAltazGrid.ForeColor = value
        End Set
    End Property

    Public Property CelestialGridButtonColor() As Color
        Get
            Return chBxCelestialGrid.ForeColor
        End Get
        Set(ByVal value As Color)
            chBxCelestialGrid.ForeColor = value
        End Set
    End Property

    Public Property SiteGridButtonColor() As Color
        Get
            Return chBxSiteGrid.ForeColor
        End Get
        Set(ByVal value As Color)
            chBxSiteGrid.ForeColor = value
        End Set
    End Property

    Public Sub SetAlignmentToolTip()
        ToolTip.SetToolTip(cmbBxAlignment, AlignmentStyle.GetInstance.FirstItem.MatchKey(cmbBxAlignment.SelectedIndex).Description)
        ToolTip.IsBalloon = True
    End Sub

    Public Sub ConvertMatrixVisible(ByVal visible As Boolean)
        btnOneTwo.Visible = visible
        chBxDisplayInits.Visible = visible
    End Sub

    ' MVPViewContainsGaugeCoordBase also handles MyBase.Load
    Private Sub FrmConvert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

    Private Sub chBxSiteGrid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxSiteGrid.CheckedChanged
        RaiseEvent SiteGrid(chBxSiteGrid.Checked)
    End Sub

    Private Sub chBxCelestialGrid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxCelestialGrid.CheckedChanged
        RaiseEvent CelestialGrid(chBxCelestialGrid.Checked)
    End Sub

    Private Sub chBxScopeAltazGrid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxScopeAltazGrid.CheckedChanged
        RaiseEvent ScopeAltazGrid(chBxScopeAltazGrid.Checked)
    End Sub

    Private Sub chBxScopeEquatGrid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxScopeEquatGrid.CheckedChanged
        RaiseEvent ScopeEquatGrid(chBxScopeEquatGrid.Checked)
    End Sub

    Private Sub txBxGridLinesPerQuadrant_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txBxGridLinesPerQuadrant.Leave
        ValidateTextBoxAsRad()
    End Sub

    Private Sub btnUpdateToCurrentSidT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateToCurrentSidT.Click
        RaiseEvent UpdateToCurrentSidT()
    End Sub

    Protected Overridable Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If GCLs.Count > 0 Then
            newLayout()
        End If
    End Sub

    Private Class graphicComponentLayout
        Public Location As Drawing.Point
        Public Size As Drawing.Size
    End Class

    Private GCLs As New Hashtable

    Private Sub buildGCLs()
        Dim gcl As New graphicComponentLayout
        gcl.Location = Me.Location
        gcl.Size = Me.Size
        GCLs.Add(Me.Name, gcl)

        gcl = New graphicComponentLayout
        gcl.Location = MVPUserCtrl2MeasurementsGaugeBase.Location
        gcl.Size = MVPUserCtrl2MeasurementsGaugeBase.Size
        GCLs.Add(MVPUserCtrl2MeasurementsGaugeBase.Name, gcl)

        gcl = New graphicComponentLayout
        gcl.Location = UserCtrlGaugePosition.Location
        gcl.Size = UserCtrlGaugePosition.Size
        GCLs.Add(UserCtrlGaugePosition.Name, gcl)

        gcl = New graphicComponentLayout
        gcl.Location = pnlPointedToPosition.Location
        gcl.Size = pnlPointedToPosition.Size
        GCLs.Add(pnlPointedToPosition.Name, gcl)

        gcl = New graphicComponentLayout
        gcl.Location = PnlTilt.Location
        gcl.Size = PnlTilt.Size
        GCLs.Add(PnlTilt.Name, gcl)

        gcl = New graphicComponentLayout
        gcl.Location = pnlOrient.Location
        gcl.Size = pnlOrient.Size
        GCLs.Add(pnlOrient.Name, gcl)

        gcl = New graphicComponentLayout
        gcl.Location = pnlTrackRates.Location
        gcl.Size = pnlTrackRates.Size
        GCLs.Add(pnlTrackRates.Name, gcl)
    End Sub

    Private Sub newLayout()
        Dim gclMe As graphicComponentLayout = CType(GCLs.Item(Me.Name), graphicComponentLayout)
        ' don't let my width go smaller than starting width
        If Me.Width < gclMe.Size.Width Then
            Me.Width = gclMe.Size.Width
        End If
        ' ditto for height
        If Me.Height < gclMe.Size.Height Then
            Me.Height = gclMe.Size.Height
        End If
        Dim widthRatio As Double = Me.Width / gclMe.Size.Width
        Dim heightRatio As Double = Me.Height / gclMe.Size.Height

        Dim gclSP As graphicComponentLayout = CType(GCLs.Item(MVPUserCtrl2MeasurementsGaugeBase.Name), graphicComponentLayout)
        Dim gclTilt As graphicComponentLayout = CType(GCLs.Item(PnlTilt.Name), graphicComponentLayout)
        Dim gclTrack As graphicComponentLayout = CType(GCLs.Item(pnlTrackRates.Name), graphicComponentLayout)
        Dim gclOrient As graphicComponentLayout = CType(GCLs.Item(pnlOrient.Name), graphicComponentLayout)
        Dim gclPos As graphicComponentLayout = CType(GCLs.Item(UserCtrlGaugePosition.Name), graphicComponentLayout)

        ' Scope Pilot graphic
        Dim maxWidth As Int32 = pnlOrient.Location.X - gclSP.Location.X - 8
        Dim maxHeight As Int32 = PnlTilt.Location.Y - gclSP.Location.Y - 8
        Dim sizeSP As Int32 = maxWidth
        If sizeSP > maxHeight Then
            sizeSP = maxHeight
        End If
        MVPUserCtrl2MeasurementsGaugeBase.Size = New Drawing.Size(sizeSP, sizeSP)

        ' 3 axis tilt control
        UserCtrlGauge3AxisCoordCenter.Width = PnlTilt.Width - 2
        UserCtrlGauge3AxisCoordCenter.Height = PnlTilt.Height - 4

        ' gauge position
        UserCtrlGaugePosition.Height = pnlOrient.Height - (gclOrient.Size.Height - gclPos.Size.Height)
    End Sub

    Private Sub ValidateTextBoxAsRad()
        Dim count As Int32
        If ScopeIII.Forms.Validate.GetInstance.ValidateTextBoxAsInt_1_To_90(ErrorProvider, txBxGridLinesPerQuadrant, count) Then
            RaiseEvent GridLinesPerQuadrant(count)
        End If
    End Sub
End Class
