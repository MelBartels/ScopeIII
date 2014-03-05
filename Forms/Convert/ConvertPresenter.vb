#Region "imports"
Imports System.Windows.Forms
Imports System.Threading
Imports BartelsLibrary.DelegateSigs
#End Region

Public Class ConvertPresenter
    Inherits MVPPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Member"
    Public StartingAzRad As Double = 150 * Units.DegToRad
    Public StartingAltRad As Double = 30 * Units.DegToRad

    Public StartingLongitudeRad As Double = 90 * Units.DegToRad
    Public StartingLatitudeRad As Double = 45 * Units.DegToRad

    Public StartingTiltPriRad As Double = 135 * Units.DegToRad
    Public StartingTiltSecRad As Double = 15 * Units.DegToRad
    Public StartingTiltTierRad As Double = 5 * Units.DegToRad

    Public TrackUpdateMillisec As Double = 1000
#End Region

#Region "Private and Protected Members"
    Private rfs As RatesFacades
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ConvertPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ConvertPresenter = New ConvertPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ConvertPresenter
        Return New ConvertPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Select Case CType([object], Coordinate).Name
            Case CoordName.RA.Description
                If pUserCtrlGauge3AxisCoordEquatPresenter.PriCoordUpdatedByMe Then
                    rfs.GetAltaz(pUserCtrlGauge3AxisCoordEquatPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateSec.Rad)
                    updateCoordDisplays()
                    Return True
                End If
            Case CoordName.Dec.Description
                If pUserCtrlGauge3AxisCoordEquatPresenter.SecCoordUpdatedByMe Then
                    rfs.GetAltaz(pUserCtrlGauge3AxisCoordEquatPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateSec.Rad)
                    updateCoordDisplays()
                    Return True
                End If
            Case CoordName.Az.Description
                If pUserCtrlGauge3AxisCoordSiteAxesPresenter.PriCoordUpdatedByMe Then
                    rfs.GetEquat(pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSec.Rad)
                    updateCoordDisplays()
                    Return True
                End If
                If pUserCtrlGauge3AxisCoordScopeAxesPresenter.PriCoordUpdatedByMe Then
                    rfs.GetEquatFromScopeGauges(pUserCtrlGauge3AxisCoordScopeAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordScopeAxesPresenter.CoordinateSec.Rad)
                    updateCoordDisplays()
                    Return True
                End If
            Case CoordName.Alt.Description
                If pUserCtrlGauge3AxisCoordSiteAxesPresenter.SecCoordUpdatedByMe Then
                    rfs.GetEquat(pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSec.Rad)
                    updateCoordDisplays()
                    Return True
                End If
                If pUserCtrlGauge3AxisCoordScopeAxesPresenter.SecCoordUpdatedByMe Then
                    rfs.GetEquatFromScopeGauges(pUserCtrlGauge3AxisCoordScopeAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordScopeAxesPresenter.CoordinateSec.Rad)
                    updateCoordDisplays()
                    Return True
                End If
            Case CoordName.SidT.Description
                If pUserCtrlGauge3AxisCoordEquatPresenter.TierCoordUpdatedByMe Then
                    rfs.GetEquat(pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSec.Rad)
                    updateCoordDisplays()
                    Return True
                End If
            Case CoordName.Latitude.Description, CoordName.Longitude.Description
                If pUserCtrlGauge2AxisCoordSitePresenter.PriCoordUpdatedByMe _
                OrElse pUserCtrlGauge2AxisCoordSitePresenter.SecCoordUpdatedByMe Then
                    updateLongAndLatFromSitePresenter()
                    initAndGetEquat()
                    Return True
                End If
            Case CoordName.TiltPri.Description, CoordName.TiltSec.Description, CoordName.TiltTier.Description
                updateOrthoProjectPoint()
                Return True
        End Select

        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        initFormsAndPresenters()
        buildDataModels()
        initInit()
        initPostInitCalcs()
        initCelestialErrors()
        initFacades()
        rfs.BuildAndInitRates()
        initRenderers()
        startingValues()
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

#End Region

#Region "Forms and Presenters"
    Private WithEvents pFrmConvert As FrmConvert
    Private WithEvents pUserCtrlGauge3AxisCoordEquat As UserCtrlGauge3AxisCoord
    Private WithEvents pUserCtrlGauge3AxisCoordSiteAxes As UserCtrlGauge3AxisCoord
    Private WithEvents pUserCtrlGauge3AxisCoordScopeAxes As UserCtrlGauge3AxisCoord
    Private WithEvents pUserCtrlGauge2AxisCoordSite As UserCtrlGauge2AxisCoord
    Private pMvpUserCtrl2MeasurementsGaugeBase As MVPUserCtrl2MeasurementsGaugeBase

    Private pUserCtrlGauge3AxisCoordEquatPresenter As UserCtrlGauge3AxisCoordPresenter
    Private pUserCtrlGauge3AxisCoordSiteAxesPresenter As UserCtrlGauge3AxisCoordPresenter
    Private pUserCtrlGauge3AxisCoordScopeAxesPresenter As UserCtrlGauge3AxisCoordPresenter
    Private pUserCtrlGauge2AxisCoordSitePresenter As UserCtrlGauge2AxisCoordPresenter
    Private pIGauge3AxisCoordPresenterCenter As IGauge3AxisCoordPresenter

    Private Sub initFormsAndPresenters()
        pFrmConvert = CType(IMVPView, FrmConvert)
        AddHandler pFrmConvert.ClosingForm, AddressOf formClosingHandler
        AddHandler pFrmConvert.QueryDatafiles, AddressOf queryDatafilesHandler
        AddHandler pFrmConvert.OneTwoClick, AddressOf oneTwoClickHandler
        AddHandler pFrmConvert.DisplayInitsCheckChanged, AddressOf displayInitsCheckChangedHandler
        AddHandler pFrmConvert.RatesChanged, AddressOf ratesChangedHandler
        AddHandler pFrmConvert.AlignmentChanged, AddressOf alignmentChangedHandler
        AddHandler pFrmConvert.MeridianFlipStateChanged, AddressOf meridianFlipChangedHandler
        AddHandler pFrmConvert.GridLinesPerQuadrant, AddressOf gridLinesPerQuadrantHandler
        AddHandler pFrmConvert.SiteGrid, AddressOf siteGridHandler
        AddHandler pFrmConvert.CelestialGrid, AddressOf celestialGridHandler
        AddHandler pFrmConvert.ScopeAltazGrid, AddressOf scopeAltazGridHandler
        AddHandler pFrmConvert.KingRate, AddressOf KingRateHandler
        AddHandler pFrmConvert.AnalyzeTrackRatePri, AddressOf analyzeTrackRatePriHandler
        AddHandler pFrmConvert.MinValue, AddressOf analyzeMinValueHandler
        AddHandler pFrmConvert.MaxValue, AddressOf analyzeMaxValueHandler
        AddHandler pFrmConvert.AnalyzeTrackRateSec, AddressOf analyzeTrackRateSecHandler
        AddHandler pFrmConvert.AnalyzeFieldRotationAngle, AddressOf analyzeFieldRotationAngleHandler
        AddHandler pFrmConvert.Track, AddressOf trackCheckedHandler
        AddHandler pFrmConvert.CelestialErrors, AddressOf celestialErrorsHandler

        ' Equat
        pUserCtrlGauge3AxisCoordEquatPresenter = UserCtrlGauge3AxisCoordPresenter.GetInstance
        pUserCtrlGauge3AxisCoordEquatPresenter.IMVPUserCtrl = pFrmConvert.UserCtrlGauge3AxisCoordEquat
        AxisCoordGaugePresenter.GetInstance.Build(CType(pUserCtrlGauge3AxisCoordEquatPresenter, IGauge3AxisCoordPresenter), CoordName.RA, CoordName.Dec, CoordName.SidT)
        pUserCtrlGauge3AxisCoordEquatPresenter.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateSecObservableImp.Attach(Me)
        pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateTierObservableImp.Attach(Me)

        ' SiteAxes
        pUserCtrlGauge3AxisCoordSiteAxesPresenter = UserCtrlGauge3AxisCoordPresenter.GetInstance
        pUserCtrlGauge3AxisCoordSiteAxesPresenter.IMVPUserCtrl = pFrmConvert.UserCtrlGauge3AxisCoordSiteAxes
        AxisCoordGaugePresenter.GetInstance.Build(CType(pUserCtrlGauge3AxisCoordSiteAxesPresenter, IGauge3AxisCoordPresenter), CoordName.Az, CoordName.Alt, CoordName.FieldRotation)
        pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSecObservableImp.Attach(Me)
        ' don't observe field rotation
        'pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateTierObservableImp.Attach(Me)

        ' ScopeAxes
        pUserCtrlGauge3AxisCoordScopeAxesPresenter = UserCtrlGauge3AxisCoordPresenter.GetInstance
        pUserCtrlGauge3AxisCoordScopeAxesPresenter.IMVPUserCtrl = pFrmConvert.UserCtrlGauge3AxisCoordScopeAxes
        AxisCoordGaugePresenter.GetInstance.Build(CType(pUserCtrlGauge3AxisCoordScopeAxesPresenter, IGauge3AxisCoordPresenter), CoordName.Az, CoordName.Alt, CoordName.FieldRotation)
        pUserCtrlGauge3AxisCoordScopeAxesPresenter.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGauge3AxisCoordScopeAxesPresenter.CoordinateSecObservableImp.Attach(Me)
        ' don't observe field rotation
        'pUserCtrlGauge3AxisCoordScopeAxesPresenter.CoordinateTierObservableImp.Attach(Me)

        ' long/lat
        pUserCtrlGauge2AxisCoordSitePresenter = UserCtrlGauge2AxisCoordPresenter.GetInstance
        pUserCtrlGauge2AxisCoordSitePresenter.IMVPUserCtrl = pFrmConvert.UserCtrlGauge2AxisCoordSite
        AxisCoordGaugePresenter.GetInstance.Build(CType(pUserCtrlGauge2AxisCoordSitePresenter, IGauge2AxisCoordPresenter), CoordName.Longitude, CoordName.Latitude)
        pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGauge2AxisCoordSitePresenter.CoordinateSecObservableImp.Attach(Me)

        ' scope pilot
        pMvpUserCtrl2MeasurementsGaugeBase = pFrmConvert.MVPUserCtrl2MeasurementsGaugeBase
        AddHandler pMvpUserCtrl2MeasurementsGaugeBase.MeasurementsToPoint, AddressOf measurementsToPoint

        ' tilt gauges
        pIGauge3AxisCoordPresenterCenter = UserCtrlGauge3AxisCoordPresenter.GetInstance
        CType(pIGauge3AxisCoordPresenterCenter, UserCtrlGauge3AxisCoordPresenter).IMVPUserCtrl = pFrmConvert.UserCtrlGauge3AxisCoordTilt
        AxisCoordGaugePresenter.GetInstance.Build(pIGauge3AxisCoordPresenterCenter, CoordName.TiltPri, CoordName.TiltSec, CoordName.TiltTier)
        pIGauge3AxisCoordPresenterCenter.CoordinatePriObservableImp.Attach(Me)
        pIGauge3AxisCoordPresenterCenter.CoordinateSecObservableImp.Attach(Me)
        pIGauge3AxisCoordPresenterCenter.CoordinateTierObservableImp.Attach(Me)
    End Sub

    Private Sub startingValues()
        pUserCtrlGauge2AxisCoordSitePresenter.DisplayCoordinates(StartingLongitudeRad, StartingLatitudeRad)
        updateLongAndLatFromSitePresenter()
        pFrmConvert.CmbBxRatesSelectedIndex = Rates.FormulaRates.Key
        pFrmConvert.ConvertMatrixEnabled(False)
        pIGauge3AxisCoordPresenterCenter.DisplayCoordinates(StartingTiltPriRad, StartingTiltSecRad, StartingTiltTierRad)
        pUserCtrlGauge3AxisCoordSiteAxesPresenter.DisplayCoordinates(StartingAzRad, StartingAltRad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateTier.Rad)
        pUserCtrlGauge3AxisCoordScopeAxesPresenter.DisplayCoordinates(StartingAzRad, StartingAltRad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateTier.Rad)
        pUserCtrlGauge3AxisCoordEquatPresenter.DisplayCoordinates(0, 0, rfs.CalcSidTNow)
        initAndGetEquat()
        ' start w/ celestial grid displaying 
        pFrmConvert.CelestialGridChecked = True
        pFrmConvert.GridLinesPerQuadrantText = CStr(ScopeLibrary.Settings.GetInstance.ScopePilotGridLinesPerQuadrant)
    End Sub

    Private Sub formClosingHandler()
        If pTracking Then
            pTrackTimer.Stop()
        End If
        If pDisplayingQueryDatafiles Then
            pQueryDatafilesPresenter.Close()
        End If
        CelestialErrorsPresenterMediator.Shutdown()
    End Sub
#End Region

#Region "Data Models"
    Private pLastCmbBxAlignmentSelectedIndex As Int32

    Private Sub buildDataModels()
        buildRatesComboBoxDataModel()
        buildAlignmentComboBoxDataModel()
    End Sub

    Private Sub buildRatesComboBoxDataModel()
        pFrmConvert.CmbBxRatesDataSource = Rates.ISFT.DataSource
    End Sub

    Private Sub buildAlignmentComboBoxDataModel()
        Dim holdSelectedIndex As Int32 = pFrmConvert.CmbBxAlignmentSelectedIndex

        Dim alignmentStyleArray As AlignmentStyleArray = Coordinates.AlignmentStyleArray.GetInstance
        ' .BuildArray adds CelestialAligned if MatrixRates is passed in
        alignmentStyleArray.BuildArray(Rates.ISFT.MatchKey(pFrmConvert.CmbBxRatesSelectedIndex))
        Dim namesArray As New ArrayList
        For Each alignmentStyle As ISFT In alignmentStyleArray.Array
            namesArray.Add(alignmentStyle.Name)
        Next
        pFrmConvert.CmbBxAlignmentDataSource = namesArray

        If holdSelectedIndex.Equals(-1) Then
            ' set starting value
            If Math.Abs(StartingLatitudeRad).Equals(Units.QtrRev) Then
                pFrmConvert.CmbBxAlignmentSelectedIndex = AlignmentStyle.PolarAligned.Key
            Else
                pFrmConvert.CmbBxAlignmentSelectedIndex = AlignmentStyle.AltazSiteAligned.Key
            End If
        ElseIf holdSelectedIndex >= 0 AndAlso holdSelectedIndex < alignmentStyleArray.Array.Count Then
            pFrmConvert.CmbBxAlignmentSelectedIndex = holdSelectedIndex
        End If

        pFrmConvert.SetAlignmentToolTip()
        pFrmConvert.KingRateEnabled(pFrmConvert.CmbBxAlignmentSelectedIndex = AlignmentStyle.PolarAligned.Key)
    End Sub

    Private Sub updateCoordDisplays()
        With rfs.CelestialRatesFacade.Position
            pUserCtrlGauge3AxisCoordEquatPresenter.DisplayCoordinates(.RA.Rad, .Dec.Rad, .SidT.Rad)
            pUserCtrlGauge3AxisCoordSiteAxesPresenter.DisplayCoordinates(eMath.ValidRad(.Az.Rad), .Alt.Rad, .Axis3.Rad)
        End With

        With rfs.ScopeRatesFacade.Position
            pUserCtrlGauge3AxisCoordScopeAxesPresenter.DisplayCoordinates(eMath.ValidRad(.Az.Rad), .Alt.Rad, .Axis3.Rad)
        End With

        displayTrackingRates(rfs.ScopeRatesFacade)
        updateOrthoProjectPoint()
        CelestialErrorsPresenterMediator.UpdateDataModel(rfs.UncorrectedPosition.RA.Rad, rfs.UncorrectedPosition.Dec.Rad, rfs.UncorrectedPosition.SidT.Rad, rfs.ScopeRatesFacade.Site.Latitude.Rad)
    End Sub
#End Region

#Region "Renderers"
    Private pScopePilotObjectToRender As ScopePilotObjectToRender
    Private pSPRenderer As ScopePilotRenderer

    Private pSiteCoordFrameworkRenderer As CoordFrameworkRenderer
    Private pCelestialCoordFrameworkRenderer As CoordFrameworkRenderer
    Private pScopeAltazCoordFrameworkRenderer As CoordFrameworkRenderer
    Private pCoordFrameworkRenderers As ArrayList

    Private pAnalyzeTrackRatePriRenderer As ScopePilotAnalyzeTrackRateRenderer
    Private pAnalyzeKingTrackRateRenderer As ScopePilotAnalyzeTrackRateRenderer
    Private pAnalyzeTrackRateSecRenderer As ScopePilotAnalyzeTrackRateRenderer
    Private pAnalyzeFieldRotationAngleRenderer As ScopePilotAnalyzeFieldRotationAngleRenderer
    Private pAnalysisRenderers As ArrayList

    Private Sub initRenderers()
        pSPRenderer = ScopePilotRenderer.GetInstance
        pScopePilotObjectToRender = ScopePilotObjectToRender.GetInstance

        pCoordFrameworkRenderers = New ArrayList
        pAnalysisRenderers = New ArrayList

        pSiteCoordFrameworkRenderer = CoordFrameworkRenderer.GetInstance
        pCoordFrameworkRenderers.Add(pSiteCoordFrameworkRenderer)

        pCelestialCoordFrameworkRenderer = CoordFrameworkRenderer.GetInstance
        pCelestialCoordFrameworkRenderer.ConvertCoords = rfs.ConvertCelestialEquatToSiteAltazDelegate
        pCelestialCoordFrameworkRenderer.ICoordExpPri = CoordExpFactory.GetInstance.Build(CoordExpType.WholeNumHour)
        pCoordFrameworkRenderers.Add(pCelestialCoordFrameworkRenderer)

        pScopeAltazCoordFrameworkRenderer = CoordFrameworkRenderer.GetInstance
        pScopeAltazCoordFrameworkRenderer.ConvertCoords = rfs.ConvertScopeAltazToSiteAltazDelegate
        pCoordFrameworkRenderers.Add(pScopeAltazCoordFrameworkRenderer)

        pMvpUserCtrl2MeasurementsGaugeBase.IRenderer = pSPRenderer

        With ScopeLibrary.Settings.GetInstance
            pFrmConvert.SiteGridButtonColor = .ScopePilotSiteRendererForegroundColor
            pFrmConvert.CelestialGridButtonColor = .ScopePilotCelestialRendererForegroundColor
            pFrmConvert.ScopeAltazGridButtonColor = .ScopePilotAltazRendererForegroundColor
            pSiteCoordFrameworkRenderer.BackColor = .ScopePilotSiteRendererBackgroundColor
            pSiteCoordFrameworkRenderer.ForeColor = .ScopePilotSiteRendererForegroundColor
            pCelestialCoordFrameworkRenderer.BackColor = .ScopePilotCelestialRendererBackgroundColor
            pCelestialCoordFrameworkRenderer.ForeColor = .ScopePilotCelestialRendererForegroundColor
            pScopeAltazCoordFrameworkRenderer.BackColor = .ScopePilotAltazRendererBackgroundColor
            pScopeAltazCoordFrameworkRenderer.ForeColor = .ScopePilotAltazRendererForegroundColor
        End With

        ' analyzers

        pAnalyzeTrackRatePriRenderer = ScopePilotAnalyzeTrackRateRenderer.GetInstance
        pAnalyzeTrackRatePriRenderer.ConvertCoords = rfs.ConvertCelestialEquatToSiteAltazDelegate
        pAnalyzeTrackRatePriRenderer.Calculator = rfs.TrackRateFromEquatCoords
        pAnalyzeTrackRatePriRenderer.Axis.SelectedItem = Axis.PriAxis
        pAnalyzeTrackRatePriRenderer.MinValue = pFrmConvert.AnalyzeMinValue
        pAnalyzeTrackRatePriRenderer.MaxValue = pFrmConvert.AnalyzeMaxValue
        pAnalysisRenderers.Add(pAnalyzeTrackRatePriRenderer)

        pAnalyzeKingTrackRateRenderer = ScopePilotAnalyzeTrackRateRenderer.GetInstance
        pAnalyzeKingTrackRateRenderer.ConvertCoords = rfs.ConvertCelestialEquatToSiteAltazDelegate
        pAnalyzeKingTrackRateRenderer.Calculator = rfs.KingRateFromEquatCoords
        pAnalyzeKingTrackRateRenderer.Axis.SelectedItem = Axis.PriAxis
        pAnalyzeKingTrackRateRenderer.MinValue = pFrmConvert.AnalyzeMinValue
        pAnalyzeKingTrackRateRenderer.MaxValue = pFrmConvert.AnalyzeMaxValue
        pAnalysisRenderers.Add(pAnalyzeKingTrackRateRenderer)

        pAnalyzeTrackRateSecRenderer = ScopePilotAnalyzeTrackRateRenderer.GetInstance
        pAnalyzeTrackRateSecRenderer.ConvertCoords = rfs.ConvertCelestialEquatToSiteAltazDelegate
        pAnalyzeTrackRateSecRenderer.Calculator = rfs.TrackRateFromEquatCoords
        pAnalyzeTrackRateSecRenderer.Axis.SelectedItem = Axis.SecAxis
        pAnalyzeTrackRateSecRenderer.MinValue = pFrmConvert.AnalyzeMinValue
        pAnalyzeTrackRateSecRenderer.MaxValue = pFrmConvert.AnalyzeMaxValue
        pAnalysisRenderers.Add(pAnalyzeTrackRateSecRenderer)

        pAnalyzeFieldRotationAngleRenderer = ScopePilotAnalyzeFieldRotationAngleRenderer.GetInstance
        pAnalyzeFieldRotationAngleRenderer.ConvertCoords = rfs.ConvertCelestialEquatToSiteAltazDelegate
        pAnalyzeFieldRotationAngleRenderer.Calculator = rfs.FieldRotationAngleFromEquatCoords
        pAnalysisRenderers.Add(pAnalyzeFieldRotationAngleRenderer)
    End Sub

    Private Sub gridLinesPerQuadrantHandler(ByVal gridLinesPerQuadrant As Int32)
        Array.ForEach(pCoordFrameworkRenderers.ToArray, AddressOf New SubDelegate(Of Object, Int32) _
            (AddressOf setGridLinesPerQuadrant, gridLinesPerQuadrant).CallDelegate)
        render()
    End Sub

    Private Sub setGridLinesPerQuadrant(ByVal CoordFrameworkRenderer As Object, ByVal gridLinesPerQuadrant As Int32)
        CType(CoordFrameworkRenderer, CoordFrameworkRenderer).GridLinesPerQuadrant = gridLinesPerQuadrant
    End Sub

    Private Sub siteGridHandler(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddCoordFrameworkRenderer(pSiteCoordFrameworkRenderer)
        Else
            pSPRenderer.RemoveCoordFrameworkRenderer(pSiteCoordFrameworkRenderer)
        End If
        render()
    End Sub

    Private Sub celestialGridHandler(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddCoordFrameworkRenderer(pCelestialCoordFrameworkRenderer)
        Else
            pSPRenderer.RemoveCoordFrameworkRenderer(pCelestialCoordFrameworkRenderer)
        End If
        render()
    End Sub

    Private Sub scopeAltazGridHandler(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddCoordFrameworkRenderer(pScopeAltazCoordFrameworkRenderer)
        Else
            pSPRenderer.RemoveCoordFrameworkRenderer(pScopeAltazCoordFrameworkRenderer)
        End If
        render()
    End Sub

    Private analyzeTrackRatePriHandlerRenderer As IScopePilotAnalyzer

    Private Sub analyzeTrackRatePriHandler(ByVal checked As Boolean)
        If checked Then
            If pDisplayKingRate Then
                analyzeTrackRatePriHandlerRenderer = CType(pAnalyzeKingTrackRateRenderer, IScopePilotAnalyzer)
            Else
                analyzeTrackRatePriHandlerRenderer = CType(pAnalyzeTrackRatePriRenderer, IScopePilotAnalyzer)
            End If
            pSPRenderer.AddIScopePilotAnalyzerRenderer(analyzeTrackRatePriHandlerRenderer)
        Else
            pSPRenderer.RemoveIScopePilotAnalyzerRenderer(analyzeTrackRatePriHandlerRenderer)
        End If
        render()
    End Sub

    Private Sub analyzeTrackRateSecHandler(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddIScopePilotAnalyzerRenderer(CType(pAnalyzeTrackRateSecRenderer, IScopePilotAnalyzer))
        Else
            pSPRenderer.RemoveIScopePilotAnalyzerRenderer(CType(pAnalyzeTrackRateSecRenderer, IScopePilotAnalyzer))
        End If
        render()
    End Sub

    Private Sub analyzeMinValueHandler(ByVal value As Double)
        For Each analyzer As Object In pAnalysisRenderers
            If analyzer.GetType Is GetType(ScopePilotAnalyzeTrackRateRenderer) Then
                CType(analyzer, ScopePilotAnalyzeTrackRateRenderer).MinValue = value
            End If
        Next
        render()
    End Sub

    Private Sub analyzeMaxValueHandler(ByVal value As Double)
        For Each analyzer As Object In pAnalysisRenderers
            If analyzer.GetType Is GetType(ScopePilotAnalyzeTrackRateRenderer) Then
                CType(analyzer, ScopePilotAnalyzeTrackRateRenderer).MaxValue = value
            End If
        Next
        render()
    End Sub

    Private Sub analyzeFieldRotationAngleHandler(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddIScopePilotAnalyzerRenderer(CType(pAnalyzeFieldRotationAngleRenderer, IScopePilotAnalyzer))
        Else
            pSPRenderer.RemoveIScopePilotAnalyzerRenderer(CType(pAnalyzeFieldRotationAngleRenderer, IScopePilotAnalyzer))
        End If
        render()
    End Sub

    Private Sub render()
        pMvpUserCtrl2MeasurementsGaugeBase.Render()
    End Sub

    Private Sub measurementsToPoint(ByRef [object] As Object)
        Dim meas As Double() = CType([object], Double())
        pUserCtrlGauge3AxisCoordSiteAxesPresenter.DisplayCoordinates(meas(0), meas(1), pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateTier.Rad)
        rfs.GetEquat(pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSec.Rad)
        updateCoordDisplays()
    End Sub

    Private Sub buildObjectToRender()
        With pScopePilotObjectToRender
            .PriRad = pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePri.Rad
            .SecRad = pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSec.Rad
            .TiltPriRad = pIGauge3AxisCoordPresenterCenter.CoordinatePri.Rad
            .TiltSecRad = pIGauge3AxisCoordPresenterCenter.CoordinateSec.Rad
            .TiltTierRad = pIGauge3AxisCoordPresenterCenter.CoordinateTier.Rad
            .PrimaryColor = CType(AxisColors.Primary.Tag, Drawing.Color)
            .SecondaryColor = CType(AxisColors.Secondary.Tag, Drawing.Color)

            .DisplayInits = _
                    pFrmConvert.DisplayInits _
                    AndAlso rfs.ScopeRatesFacade IsNot Nothing _
                    AndAlso rfs.ScopeRatesFacade.ConvertMatrix IsNot Nothing

            If .DisplayInits Then
                For init As Int32 = 1 To .Inits.Length
                    ' not .Inits(int32) = rfs.ConvertMatrixInitToSiteAltaz(int32) as AZDouble returned is an object 
                    Dim AZDouble As AZdouble = rfs.ConvertMatrixInitToSiteAltaz(init)
                    .Inits(init - 1).Z = AZDouble.Z
                    .Inits(init - 1).A = AZDouble.A
                Next
            End If

            If pUserCtrlGauge2AxisCoordSitePresenter.CoordinateSec.Rad >= 0 Then
                .Hemisphere.SelectedItem = Hemisphere.Northern
            Else
                .Hemisphere.SelectedItem = Hemisphere.Southern
            End If
        End With

        pMvpUserCtrl2MeasurementsGaugeBase.IRenderer.ObjectToRender = pScopePilotObjectToRender
    End Sub

    Private Sub updateOrthoProjectPoint()
        If pScopePilotObjectToRender IsNot Nothing Then
            buildObjectToRender()
            render()
        End If
    End Sub
#End Region

#Region "OneTwo"
    Private pOneTwoPresenter As OneTwoPresenter
    Private pPostInitCalcs As PostInitCalcs

    Private Sub initPostInitCalcs()
        pOneTwoPresenter = OneTwoPresenter.GetInstance
        pOneTwoPresenter.IMVPView = New FrmEnterOneTwo
        pOneTwoPresenter.LatitudeRad = StartingLatitudeRad

        pPostInitCalcs = Coordinates.PostInitCalcs.GetInstance
    End Sub

    Private Sub displayInitsCheckChangedHandler()
        buildObjectToRender()
        render()
    End Sub

    ' if OK from OneTwoPresenter, then change alignment to celestial and re-init
    Private Sub oneTwoClickHandler()
        If showOneTwoPresenter() Then
            pFrmConvert.CmbBxAlignmentSelectedItem = AlignmentStyle.CelestialAligned.Name
            pFrmConvert.SetAlignmentToolTip()
            If copyOneTwoFabErrorsToConvertMatrix() Then
                buildRates_setInit_initScopeAndGetEquat()
                updateLatUsingPostInitCalcs()
            End If
        End If
    End Sub

    Protected Sub updateLatUsingPostInitCalcs()
        pPostInitCalcs.ICoordXform = rfs.ScopeRatesFacade.ICoordXForm
        pPostInitCalcs.UpdatePostInitVars = True
        pPostInitCalcs.CheckForPostInitVars()

        Dim latitudeRadEquatPole As Double = pPostInitCalcs.LatitudeBasedOnScopeAtEquatPole.Rad
        Dim latitudeRadScopePole As Double = pPostInitCalcs.LatitudeBasedOnScopeAtScopePole.Rad
        Dim latitudeRadAvg As Double = (latitudeRadEquatPole + latitudeRadScopePole) / 2

        Dim sb As New Text.StringBuilder
        sb.Append("Adopt calculated Longitude of ")
        sb.Append(DMS.GetInstance.ToString(pPostInitCalcs.LongitudeBasedOnScopeAtScopePole))
        sb.Append(", Latitude of ")
        sb.Append(DMS.GetInstance.ToString(latitudeRadAvg))
        sb.Append(" ?")
        If AppMsgBox.Show(sb.ToString, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            pUserCtrlGauge2AxisCoordSitePresenter.DisplayCoordinates(pPostInitCalcs.LongitudeBasedOnScopeAtScopePole, latitudeRadAvg)
            updateLongAndLatFromSitePresenter()
        End If
    End Sub

    Private Function showOneTwoPresenter() As Boolean
        If pLastCmbBxAlignmentSelectedIndex = AlignmentStyle.AltazSiteAligned.Key Then
            pOneTwoPresenter.Preset(CType(AlignmentStyle.AltazSiteAligned, ISFT))
        ElseIf pLastCmbBxAlignmentSelectedIndex = AlignmentStyle.PolarAligned.Key Then
            pOneTwoPresenter.Preset(CType(AlignmentStyle.PolarAligned, ISFT))
        End If
        pOneTwoPresenter.LatitudeRad = pUserCtrlGauge2AxisCoordSitePresenter.CoordinateSec.Rad

        pOneTwoPresenter.ShowDialog()
        Return CType(pOneTwoPresenter.IMVPView, System.Windows.Forms.Form).DialogResult.Equals(DialogResult.OK)
    End Function

    Private Function validateOneTwoPresenter(ByRef OneTwoPresenter As OneTwoPresenter) As Boolean
        If OneTwoPresenter Is Nothing Then
            Return False
        End If
        If OneTwoPresenter.DataModel Is Nothing Then
            Return False
        End If
        With CType(OneTwoPresenter.DataModel, OneTwoPresenterDataModel)
            If .One Is Nothing OrElse .Two Is Nothing Then
                Return False
            End If
            If .One.RA.Rad.Equals(.Two.RA.Rad) _
                AndAlso .One.Dec.Rad.Equals(.Two.Dec.Rad) _
                AndAlso .One.Az.Rad.Equals(.Two.Az.Rad) _
                AndAlso .One.Alt.Rad.Equals(.Two.Alt.Rad) Then
                Return False
            End If
        End With

        Return True
    End Function

    Private Function copyOneTwoFabErrorsToConvertMatrix() As Boolean
        If validateOneTwoPresenter(pOneTwoPresenter) Then
            With CType(pOneTwoPresenter.DataModel, OneTwoPresenterDataModel)

                Dim FabErrors As FabErrors = Nothing
                If .UseCorrections Then
                    FabErrors = .FabErrors
                End If

                rfs.ScopeRatesFacade.CopyFromOneTwoThreeFabErrors(.One, .Two, Nothing, FabErrors)
                Return True
            End With
        Else
            AppMsgBox.Show("Positions 'One' and 'Two' match.  Cannot convert.")
        End If

        Return False
    End Function

    Private Function oneTwoEdited() As Boolean
        Return pOneTwoPresenter IsNot Nothing AndAlso validateOneTwoPresenter(pOneTwoPresenter)
    End Function
#End Region

#Region "Track"
    Private pTracking As Boolean
    Private pTrackTimer As Timers.Timer
    Private pFormatRateArcsecSidSec As [Delegate] = New DelegateDblAsStr(AddressOf formatRateArcsecSidSec)
    Private pFormatRateDegSidMin As [Delegate] = New DelegateDblAsStr(AddressOf formatRateDegSidMin)
    Private pFormatDeltaRateArcsecSidSec As [Delegate] = New DelegateDblAsStr(AddressOf formatDeltaRateArcsecSidSec)
    Private pDisplayKingRate As Boolean

    Private Sub trackCheckedHandler(ByVal checked As Boolean)
        If checked Then
            If Not pTracking Then
                pTracking = True
                pTrackTimer = New Timers.Timer(TrackUpdateMillisec)
                AddHandler pTrackTimer.Elapsed, AddressOf track
                pTrackTimer.Start()
            End If
        Else
            If pTracking Then
                pTracking = False
                pTrackTimer.Stop()
            End If
        End If
    End Sub

    Private Sub track(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        pUserCtrlGauge3AxisCoordEquatPresenter.DisplayCoordinates(pUserCtrlGauge3AxisCoordEquatPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateSec.Rad, rfs.CalcSidTNow)
        rfs.GetAltaz(pUserCtrlGauge3AxisCoordEquatPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateSec.Rad)
        updateCoordDisplays()
    End Sub

    ' rates updated in initAndGetAltaz() and initAndGetEquat()
    Private Sub displayTrackingRates(ByVal ratesFacade As RatesFacade)
        If pDisplayKingRate Then
            pFrmConvert.LblPriAxisTrackRateText = displayKingRate()
        Else
            pFrmConvert.LblPriAxisTrackRateText = displayTrackRate(CType(Axis.PriAxis, ISFT), ratesFacade.PriAxisTrackRate, pFormatRateArcsecSidSec, pFormatDeltaRateArcsecSidSec)
        End If
        pFrmConvert.LblSecAxisTrackRateText = displayTrackRate(CType(Axis.SecAxis, ISFT), ratesFacade.SecAxisTrackRate, pFormatRateArcsecSidSec, pFormatDeltaRateArcsecSidSec)
        pFrmConvert.LblTierAxisTrackRateText = displayTrackRate(CType(Axis.TierAxis, ISFT), ratesFacade.TierAxisTrackRate, pFormatRateDegSidMin, pFormatDeltaRateArcsecSidSec)
    End Sub

    Private Function displayTrackRate( _
            ByRef axis As ISFT, _
            ByRef trackRate As TrackRatesDataModel.TrackRate, _
            ByRef rateFormatDelegate As [Delegate], _
            ByRef deltaRateFormatDelegate As [Delegate]) As String

        Dim sb As New Text.StringBuilder
        sb.Append(axis.Description)
        sb.Append(": ")

        If CelestialErrorsPresenterMediator.DisplayingCelestialErrors Then
            sb.Append(CStr(rateFormatDelegate.DynamicInvoke(New Object() {trackRate.CorrectedRateRadPerSidSec})))
        Else
            sb.Append(CStr(rateFormatDelegate.DynamicInvoke(New Object() {trackRate.RateRadPerSidSec})))
        End If

        sb.Append(", ")
        sb.Append(CStr(deltaRateFormatDelegate.DynamicInvoke(New Object() {trackRate.DeltaRateRadPerSidSecPerSidSec})))

        Return sb.ToString
    End Function

    Private Function formatRateArcsecSidSec(ByVal rateRadPerSidSec As Double) As String
        Return Format(rateRadPerSidSec * Units.RadToArcsec, "##0.###") & """/s"
    End Function

    Protected Function formatRateDegSidMin(ByVal rateRadPerSidSec As Double) As String
        Return Format(rateRadPerSidSec * Units.RadToDeg * 60, "##0.###") & "d/m"
    End Function

    Private Function formatDeltaRateArcsecSidSec(ByVal rateRadPerSidSec As Double) As String
        Return BartelsLibrary.Constants.DeltaChar & Format(rateRadPerSidSec * Units.RadToArcsec, "##0.####") & """/s/s"
    End Function

    Private Sub KingRateHandler(ByVal checked As Boolean)
        pDisplayKingRate = checked
        rfs.GetKingRate(pUserCtrlGauge3AxisCoordEquatPresenter.CoordinatePri.Rad, _
                        pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateSec.Rad, _
                        pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateTier.Rad)
        displayTrackingRates(rfs.ScopeRatesFacade)
    End Sub

    Private Function displayKingRate() As String
        Dim sb As New Text.StringBuilder
        sb.Append("King Rate ")
        sb.Append(Format(rfs.KingRateArcsecSec, "##0.###"))
        sb.Append("""/s")
        Return sb.ToString
    End Function
#End Region

#Region "Init"
    Public PolarAligned As Boolean
    Private pAlignmentStyle As AlignmentStyle

    Private Sub initInit()
        pAlignmentStyle = AlignmentStyle.GetInstance
    End Sub

    Private Sub ratesChangedHandler()
        buildAlignmentComboBoxDataModel()
        buildRates_setInit_initScopeAndGetEquat()
        setLastAlignment()
    End Sub

    Private Sub alignmentChangedHandler()
        buildRates_setInit_initScopeAndGetEquat()
        setLastAlignment()
    End Sub

    Private Sub setLastAlignment()
        pLastCmbBxAlignmentSelectedIndex = pFrmConvert.CmbBxAlignmentSelectedIndex
    End Sub

    Private Sub buildRates_setInit_initScopeAndGetEquat()
        rfs.BuildScopeRatesFacade(Rates.ISFT.MatchKey(pFrmConvert.CmbBxRatesSelectedIndex))
        pFrmConvert.ConvertMatrixEnabled(pFrmConvert.CmbBxRatesSelectedIndex.Equals(Rates.MatrixRates.Key))
        setInit(pAlignmentStyle.SetSelectedItem(CStr(pFrmConvert.CmbBxAlignmentSelectedItem)).SelectedItem)
        updateLongAndLatFromSitePresenter()
        initAndGetEquat()
    End Sub

    Private Sub updateLongAndLatFromSitePresenter()
        rfs.UpdateLongitudeLatitude(pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePri.Rad, pUserCtrlGauge2AxisCoordSitePresenter.CoordinateSec.Rad, PolarAligned)
    End Sub

    Private Sub setInit(ByRef selectedAlignment As ISFT)
        If selectedAlignment Is AlignmentStyle.PolarAligned Then
            rfs.SetInits(CType(InitStateType.Equatorial, ISFT))
            PolarAligned = True

        ElseIf selectedAlignment Is AlignmentStyle.AltazSiteAligned Then
            rfs.SetInits(CType(InitStateType.Altazimuth, ISFT))
            PolarAligned = False

        ElseIf selectedAlignment Is AlignmentStyle.CelestialAligned Then
            If Not oneTwoEdited() Then
                oneTwoClickHandler()
            End If
            rfs.SetInits(CType(InitStateType.Celestial, ISFT))
            copyOneTwoFabErrorsToConvertMatrix()

        Else
            ExceptionService.Notify(BartelsLibrary.Constants.UnknownSelectedAlignment)
        End If

        pFrmConvert.KingRateEnabled(PolarAligned)
    End Sub

    Private Sub initAndGetAltaz()
        rfs.InitAndGetAltaz(pUserCtrlGauge3AxisCoordEquatPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateSec.Rad)
        updateCoordDisplays()
    End Sub

    Private Sub initAndGetEquat()
        rfs.InitAndGetEquat(pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSec.Rad)
        updateCoordDisplays()
    End Sub

    Private Sub initFacades()
        rfs = RatesFacades.GetInstance
        rfs.RegisterReferences(pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePri, pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateTier, CelestialErrorsPresenterMediator.CelestialErrorsCalculatorFacade)
        rfs.BuildAndInitRates()
    End Sub

    Private Sub meridianFlipChangedHandler(ByRef state As ISFT)
        rfs.MeridianFlipChanged(state)
        rfs.GetEquat(pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSec.Rad)
        updateCoordDisplays()
    End Sub
#End Region

#Region "Query Datafiles"
    Private pQueryDatafilesPresenter As QueryDatafilesPresenter
    Private pDisplayingQueryDatafiles As Boolean

    Private Sub bringQueryDatafilesFormToFront()
        Dim frm As Windows.Forms.Form = CType(pQueryDatafilesPresenter.IMVPView, Form)
        If frm.InvokeRequired Then
            frm.Invoke(New MethodInvoker(AddressOf bringQueryDatafilesFormToFront))
        Else
            frm.TopMost = True
            frm.Refresh()
            frm.TopMost = False
        End If
    End Sub

    Private Sub queryDatafilesHandler()
        If pDisplayingQueryDatafiles Then
            bringQueryDatafilesFormToFront()
        Else
            pDisplayingQueryDatafiles = True
            Dim queryDatafilesThread As New Thread(AddressOf queryDatafilesWork)
            queryDatafilesThread.Name = "Convert Presenter Query Datafiles"
            queryDatafilesThread.Start()
        End If
    End Sub

    Private Sub queryDatafilesWork()
        pQueryDatafilesPresenter = Forms.QueryDatafilesPresenter.GetInstance
        Dim frmQueryDatafiles As New FrmQueryDatafiles
        frmQueryDatafiles.StartPosition = FormStartPosition.WindowsDefaultLocation
        pQueryDatafilesPresenter.IMVPView = frmQueryDatafiles
        AddHandler pQueryDatafilesPresenter.ObjectSelected, AddressOf objectSelectedHandler
        pQueryDatafilesPresenter.ShowDialog()
        pDisplayingQueryDatafiles = False
    End Sub

    Private Sub objectSelectedHandler(ByVal selectedLWPosition As LWPosition)
        'MessageBoxSelectedObject.GetInstance.Show(selectedLWPosition)

        pUserCtrlGauge3AxisCoordEquatPresenter.DisplayCoordinates(selectedLWPosition.RA, selectedLWPosition.Dec, pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateTier.Rad)
        rfs.GetAltaz(pUserCtrlGauge3AxisCoordEquatPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordEquatPresenter.CoordinateSec.Rad)
        updateCoordDisplays()

        If CelestialErrorsPresenterMediator IsNot Nothing Then
            celestialErrorsToIncludeChanged()
        End If
    End Sub
#End Region

#Region "Celestial Errors"
    Public WithEvents CelestialErrorsPresenterMediator As CelestialErrorsPresenterMediator

    Private Sub initCelestialErrors()
        CelestialErrorsPresenterMediator = Forms.CelestialErrorsPresenterMediator.GetInstance
    End Sub

    Private Sub celestialErrorsHandler()
        AddHandler CelestialErrorsPresenterMediator.ErrorsToIncludeChanged, AddressOf celestialErrorsToIncludeChanged
        CelestialErrorsPresenterMediator.Startup()
    End Sub

    Private Sub celestialErrorsToIncludeChanged()
        render()
        rfs.GetEquat(pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinatePri.Rad, pUserCtrlGauge3AxisCoordSiteAxesPresenter.CoordinateSec.Rad)
        updateCoordDisplays()
    End Sub
#End Region

End Class
