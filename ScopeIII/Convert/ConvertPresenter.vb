#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class ConvertPresenter
    Inherits MVPPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Private PriAxisLabel As String = Axis.PriAxis.Description & eString.TrackRateLiteral
    Private SecAxisLabel As String = Axis.SecAxis.Description & eString.TrackRateLiteral
    Private TieAxisLabel As String = Axis.TieAxis.Description & eString.TrackRateLiteral
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Member"
    Public StartingLongitudeRad As Double = 0
    Public StartingLatitudeRad As Double = 45 * Units.DegToRad
    Public StartingTiltPriRad As Double = 135 * Units.DegToRad
    Public StartingTiltSecRad As Double = 15 * Units.DegToRad
    Public StartingTiltTierRad As Double = 5 * Units.DegToRad
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmConvert As FrmConvert
    Private WithEvents pUserCtrlGaugePosition As UserCtrlGaugePosition
    Private WithEvents pUserCtrlGauge2AxisCoordSite As UserCtrlGauge2AxisCoord

    Private pUserCtrlGaugePositionPresenter As UserCtrlGaugePositionPresenter
    Private pUserCtrlGauge2AxisCoordSitePresenter As UserCtrlGauge2AxisCoordPresenter
    Private pOneTwoPresenter As OneTwoPresenter

    Private pLastCmbBxAlignmentSelectedIndex As Int32
    Private pTime As Time
    Private pScopeRatesFacade As RatesFacade
    Private pCelestialRatesFacade As RatesFacade
    Private pPostInitCalcs As PostInitCalcs
    Private pEmath As eMath

#Region "Scope Pilot"
    Private pMvpUserCtrl2MeasurementsGaugeBase As MVPUserCtrl2MeasurementsGaugeBase

    Private pIGauge3AxisCoordPresenterCenter As IGauge3AxisCoordPresenter

    Private pScopePilotObjectToRender As ScopePilotObjectToRender
    Private pSPRenderer As ScopePilotRenderer

    Private pSiteCoordFrameworkRenderer As CoordFrameworkRenderer
    Private pCelestialCoordFrameworkRenderer As CoordFrameworkRenderer
    Private pScopeEquatCoordFrameworkRenderer As CoordFrameworkRenderer
    Private pScopeAltazCoordFrameworkRenderer As CoordFrameworkRenderer
    Private pCoordFrameworkRenderers As ArrayList

    Private Delegate Function delegateConvertCoords(ByVal priRad As Double, ByVal secRad As Double) As AZdouble
#End Region
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
    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements Common.IObserver.ProcessMsg
        Select Case CType([object], Coordinate).Name
            Case CoordName.RA.Description, CoordName.Dec.Description
                If pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.PriCoordUpdatedByMe _
                OrElse pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.SecCoordUpdatedByMe Then
                    GetAltaz()
                    updateCoordDisplays()
                    Return True
                End If
            Case CoordName.Alt.Description, CoordName.Az.Description
                If pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.PriCoordUpdatedByMe _
                OrElse pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.SecCoordUpdatedByMe Then
                    GetEquat()
                    updateCoordDisplays()
                    Return True
                End If
            Case CoordName.SidT.Description
                If pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT.CoordUpdatedByMe Then
                    GetEquat()
                    updateCoordDisplays()
                    Return True
                End If
            Case CoordName.TiltPri.Description, CoordName.TiltSec.Description, CoordName.TiltTier.Description
                updateOrthoProjectPoint()
                Return True
            Case CoordName.Latitude.Description, CoordName.Longitude.Description
                If pUserCtrlGauge2AxisCoordSitePresenter.PriCoordUpdatedByMe _
                OrElse pUserCtrlGauge2AxisCoordSitePresenter.SecCoordUpdatedByMe Then
                    setRatesSiteFromSitePresenter()
                    If pScopeRatesFacade.Rates.InitStateTemplate.InitStateType Is InitStateType.Celestial Then
                        initAndGetEquatCelestial()
                    Else
                        initAndGetEquat()
                    End If
                    Return True
                End If
        End Select

        Return False
    End Function
#End Region

#Region "Private and Protected Methods"

    Protected Overrides Sub init()
        MyBase.init()

        pFrmConvert = CType(IMVPView, FrmConvert)
        AddHandler pFrmConvert.UpdateToCurrentSidT, AddressOf UpdateToCurrentSidT
        AddHandler pFrmConvert.OneTwoClick, AddressOf oneTwoClick
        AddHandler pFrmConvert.DisplayInitsCheckChanged, AddressOf displayInitsCheckChanged
        AddHandler pFrmConvert.RatesChanged, AddressOf ratesChanged
        AddHandler pFrmConvert.AlignmentChanged, AddressOf alignmentChanged
        AddHandler pFrmConvert.GridLinesPerQuadrant, AddressOf gridLinesPerQuadrant
        AddHandler pFrmConvert.SiteGrid, AddressOf siteGrid
        AddHandler pFrmConvert.CelestialGrid, AddressOf celestialGrid
        AddHandler pFrmConvert.ScopeAltazGrid, AddressOf scopeAltazGrid
        AddHandler pFrmConvert.ScopeEquatGrid, AddressOf scopeEquatGrid

        With Settings.GetInstance
            pFrmConvert.SiteGridButtonColor = .ScopePilotSiteRendererForegroundColor
            pFrmConvert.CelestialGridButtonColor = .ScopePilotCelestialRendererForegroundColor
            pFrmConvert.ScopeEquatGridButtonColor = .ScopePilotEquatRendererForegroundColor
            pFrmConvert.ScopeAltazGridButtonColor = .ScopePilotAltazRendererForegroundColor
        End With

        ' position
        pUserCtrlGaugePositionPresenter = UserCtrlGaugePositionPresenter.GetInstance
        pUserCtrlGaugePositionPresenter.IMVPUserCtrl = pFrmConvert.UserCtrlGaugePosition
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.SetAxisNames(CoordName.RA.Description, CoordName.Dec.Description)
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.SetCoordinateLabelColors(CType(AxisColors.Primary.Tag, Drawing.Color), CType(AxisColors.Secondary.Tag, Drawing.Color))
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.SetExpCoordTypes(CType(CoordExpType.FormattedHMS, ISFT), CType(CoordExpType.FormattedDMS, ISFT))
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.SetRenderers(HourCircGaugeRenderer.GetInstance, DeclinationCircGaugeRenderer.GetInstance)
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.CoordinateSecObservableImp.Attach(Me)

        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.SetAxisNames(CoordName.Az.Description, CoordName.Alt.Description)
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.SetCoordinateLabelColors(CType(AxisColors.Primary.Tag, Drawing.Color), CType(AxisColors.Secondary.Tag, Drawing.Color))
        defaultAzGauge()
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.CoordinateSecObservableImp.Attach(Me)

        pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT.SetCoordinateName(CoordName.SidT.Description)
        pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT.CoordExpType = CType(CoordExpType.FormattedHMS, ISFT)
        pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT.CoordinateObservableImp.Attach(Me)

        ' long/lat
        pUserCtrlGauge2AxisCoordSitePresenter = UserCtrlGauge2AxisCoordPresenter.GetInstance
        pUserCtrlGauge2AxisCoordSitePresenter.IMVPUserCtrl = pFrmConvert.UserCtrlGauge2AxisCoordSite
        pUserCtrlGauge2AxisCoordSitePresenter.SetAxisNames(CoordName.Longitude.Description, CoordName.Latitude.Description)
        pUserCtrlGauge2AxisCoordSitePresenter.SetCoordinateLabelColors(CType(AxisColors.Primary.Tag, Drawing.Color), CType(AxisColors.Secondary.Tag, Drawing.Color))
        pUserCtrlGauge2AxisCoordSitePresenter.SetRenderers(DegreeCircGaugeRenderer.GetInstance, DeclinationCircGaugeRenderer.GetInstance)
        pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGauge2AxisCoordSitePresenter.CoordinateSecObservableImp.Attach(Me)

        BuildRatesComboBoxDataModel()
        BuildAlignmentComboBoxDataModel()

        pTime = Time.GetInstance
        pEmath = eMath.GetInstance
        pPostInitCalcs = Coordinates.PostInitCalcs.GetInstance

        ' build rates
        pScopeRatesFacade = RatesFacade.GetInstance
        pScopeRatesFacade.Build(CType(Rates.FormulaRates, ISFT))
        pScopeRatesFacade.SetInit(CType(InitStateType.Equatorial, ISFT))

        pCelestialRatesFacade = RatesFacade.GetInstance
        pCelestialRatesFacade.Build(CType(Rates.TrigRates, ISFT))
        pCelestialRatesFacade.SetInit(CType(InitStateType.Equatorial, ISFT))
        pCelestialRatesFacade.Init()

        pUserCtrlGauge2AxisCoordSitePresenter.DisplayCoordinates(StartingLongitudeRad, StartingLatitudeRad)
        setRatesSiteFromSitePresenter()
        initAndGetEquat()

        ' starting values
        pFrmConvert.CmbBxRatesSelectedIndex = Rates.FormulaRates.Key
        pFrmConvert.ConvertMatrixVisible(False)

        initScopePilot()
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub initScopePilot()
        pMvpUserCtrl2MeasurementsGaugeBase = pFrmConvert.MVPUserCtrl2MeasurementsGaugeBase
        AddHandler pMvpUserCtrl2MeasurementsGaugeBase.MeasurementsToPoint, AddressOf measurementsToPoint

        ' renderers

        pSPRenderer = ScopePilotRenderer.GetInstance
        pScopePilotObjectToRender = ScopePilotObjectToRender.GetInstance

        pCoordFrameworkRenderers = New ArrayList

        pSiteCoordFrameworkRenderer = CoordFrameworkRenderer.GetInstance
        pCoordFrameworkRenderers.Add(pSiteCoordFrameworkRenderer)

        pCelestialCoordFrameworkRenderer = CoordFrameworkRenderer.GetInstance
        pCelestialCoordFrameworkRenderer.ConvertCoords = New delegateConvertCoords(AddressOf convertCelestialEquatToAltaz)
        pCoordFrameworkRenderers.Add(pCelestialCoordFrameworkRenderer)

        pScopeEquatCoordFrameworkRenderer = CoordFrameworkRenderer.GetInstance
        pCoordFrameworkRenderers.Add(pScopeEquatCoordFrameworkRenderer)
        pScopeEquatCoordFrameworkRenderer.ConvertCoords = New delegateConvertCoords(AddressOf convertScopeEquatToAltaz)
        pScopeEquatCoordFrameworkRenderer.ICoordExpPri = CoordExpFactory.GetInstance.Build(CoordExpType.WholeNumHour)

        pScopeAltazCoordFrameworkRenderer = CoordFrameworkRenderer.GetInstance
        pScopeAltazCoordFrameworkRenderer.ConvertCoords = New delegateConvertCoords(AddressOf convertSiteToCelestialToScopeAltaz)
        pCoordFrameworkRenderers.Add(pScopeAltazCoordFrameworkRenderer)

        With Settings.GetInstance
            pSiteCoordFrameworkRenderer.BackColor = .ScopePilotSiteRendererBackgroundColor
            pSiteCoordFrameworkRenderer.ForeColor = .ScopePilotSiteRendererForegroundColor
            pCelestialCoordFrameworkRenderer.BackColor = .ScopePilotCelestialRendererBackgroundColor
            pCelestialCoordFrameworkRenderer.ForeColor = .ScopePilotCelestialRendererForegroundColor
            pScopeEquatCoordFrameworkRenderer.BackColor = .ScopePilotEquatRendererBackgroundColor
            pScopeEquatCoordFrameworkRenderer.ForeColor = .ScopePilotEquatRendererForegroundColor
            pScopeAltazCoordFrameworkRenderer.BackColor = .ScopePilotAltazRendererBackgroundColor
            pScopeAltazCoordFrameworkRenderer.ForeColor = .ScopePilotAltazRendererForegroundColor
        End With

        ' default is site only rendering
        pSPRenderer.AddCoordFrameworkRenderer(pSiteCoordFrameworkRenderer)
        pMvpUserCtrl2MeasurementsGaugeBase.IRenderer = pSPRenderer

        ' tilt gauges

        pIGauge3AxisCoordPresenterCenter = UserCtrlGauge3AxisCoordPresenter.GetInstance
        CType(pIGauge3AxisCoordPresenterCenter, UserCtrlGauge3AxisCoordPresenter).IMVPUserCtrl = pFrmConvert.UserCtrlGauge3AxisCoordCenter
        pIGauge3AxisCoordPresenterCenter.SetAxisNames(CoordName.TiltPri.Description, CoordName.TiltSec.Description, CoordName.TiltTier.Description)
        pIGauge3AxisCoordPresenterCenter.SetExpCoordTypes(CoordExpType.WholeNumDegree, CoordExpType.WholeNumNegDegree, CoordExpType.WholeNumNegDegree)
        pIGauge3AxisCoordPresenterCenter.SetCoordinateLabelColors(CType(AxisColors.Primary.Tag, Drawing.Color), CType(AxisColors.Secondary.Tag, Drawing.Color), CType(AxisColors.Tiertiary.Tag, Drawing.Color))
        pIGauge3AxisCoordPresenterCenter.SetRenderers(DegreeNegSliderRenderer.GetInstance, DegreeNegSliderRenderer.GetInstance, DegreeNegSliderRenderer.GetInstance)
        pIGauge3AxisCoordPresenterCenter.CoordinatePriObservableImp.Attach(Me)
        pIGauge3AxisCoordPresenterCenter.CoordinateSecObservableImp.Attach(Me)
        pIGauge3AxisCoordPresenterCenter.CoordinateTierObservableImp.Attach(Me)

        pIGauge3AxisCoordPresenterCenter.DisplayCoordinates(StartingTiltPriRad, StartingTiltSecRad, StartingTiltTierRad)
        buildObjectToRender()
        render()
    End Sub

    Private Sub displayInitsCheckChanged()
        buildObjectToRender()
        render()
    End Sub

    ' if OK from OneTwoPresenter, then change alignment to celestial and re-init
    Private Sub oneTwoClick()
        If showOneTwoPresenter() Then
            pFrmConvert.CmbBxAlignmentSelectedItem = AlignmentStyle.CelestialAligned.Name
            pFrmConvert.SetAlignmentToolTip()
            If copyOneTwoPresenterFabErrorsToConvertMatrix() Then
                setRatesAndInit()
                updateLatUsingPostInitCalcs()
            End If
        End If
    End Sub

    Protected Sub updateLatUsingPostInitCalcs()
        pPostInitCalcs.ICoordXform = pScopeRatesFacade.ICoordXForm
        pPostInitCalcs.UpdatePostInitVars = True
        pPostInitCalcs.CheckForPostInitVars()

        Dim latRadEquatPole As Double = pPostInitCalcs.LatitudeBasedOnScopeAtEquatPole.Rad
        Dim latRadScopePole As Double = pPostInitCalcs.LatitudeBasedOnScopeAtScopePole.Rad
        Dim latRadAvg As Double = (latRadEquatPole + latRadScopePole) / 2
        pUserCtrlGauge2AxisCoordSitePresenter.DisplayCoordinates(pPostInitCalcs.LongitudeBasedOnScopeAtScopePole, latRadAvg)
        setRatesSiteFromSitePresenter()
    End Sub

    Protected Sub ratesChanged()
        BuildAlignmentComboBoxDataModel()
        setRatesAndInit()
    End Sub

    Protected Sub alignmentChanged()
        setRatesAndInit()
    End Sub

    Private Function showOneTwoPresenter() As Boolean
        If pOneTwoPresenter Is Nothing Then
            pOneTwoPresenter = OneTwoPresenter.GetInstance
            pOneTwoPresenter.IMVPView = New FrmEnterOneTwo
        End If

        Dim restoreDataModel As Object = pOneTwoPresenter.DataModel

        copyConvertMatrixOneTwoThreeFabErrors()
        pOneTwoPresenter.ShowDialog()

        If CType(pOneTwoPresenter.IMVPView, System.Windows.Forms.Form).DialogResult.Equals(DialogResult.OK) Then
            Return True
        Else
            pOneTwoPresenter.DataModel = restoreDataModel
            Return False
        End If
    End Function

    Private Function validateOneTwoPresenter(ByRef OneTwoPresenter As OneTwoPresenter) As Boolean
        If OneTwoPresenter Is Nothing Then
            Return False
        End If
        If OneTwoPresenter.One Is Nothing OrElse OneTwoPresenter.Two Is Nothing Then
            Return False
        End If
        If OneTwoPresenter.One.RA.Rad.Equals(OneTwoPresenter.Two.RA.Rad) _
        AndAlso OneTwoPresenter.One.Dec.Rad.Equals(OneTwoPresenter.Two.Dec.Rad) _
        AndAlso OneTwoPresenter.One.Az.Rad.Equals(OneTwoPresenter.Two.Az.Rad) _
        AndAlso OneTwoPresenter.One.Alt.Rad.Equals(OneTwoPresenter.Two.Alt.Rad) Then
            Return False
        End If
        Return True
    End Function

    Private Function copyOneTwoPresenterFabErrorsToConvertMatrix() As Boolean
        If validateOneTwoPresenter(pOneTwoPresenter) Then
            Dim one As Position = pOneTwoPresenter.One
            Dim two As Position = pOneTwoPresenter.Two
            one.Init = True
            two.Init = True
            one.Available = False
            two.Available = False

            Dim FabErrors As FabErrors
            If pOneTwoPresenter.UseCorrections Then
                FabErrors = pOneTwoPresenter.FabErrors
            Else
                FabErrors = Nothing
            End If

            pScopeRatesFacade.CopyFromOneTwoThreeFabErrors(one, two, Nothing, FabErrors)
            Return True
        Else
            AppMsgBox.Show("Positions 'One' and 'Two' match.  Cannot convert.")
        End If

        Return False
    End Function

    Private Sub copyConvertMatrixOneTwoThreeFabErrors()
        Dim one As Position = PositionArraySingleton.GetInstance.GetPosition("One")
        Dim two As Position = PositionArraySingleton.GetInstance.GetPosition("Two")
        Dim FabErrors As FabErrors = Coordinates.FabErrors.GetInstance

        pScopeRatesFacade.CopyToOneTwoThreeFabErrors(one, two, Nothing, FabErrors)
        pOneTwoPresenter.DataModel = New Object() {one, two, True, FabErrors}
    End Sub

    Private Sub setRatesAndInit()
        pScopeRatesFacade.Build(Rates.GetInstance.FirstItem.MatchKey(pFrmConvert.CmbBxRatesSelectedIndex))
        setRatesSiteFromSitePresenter()
        pFrmConvert.ConvertMatrixVisible(pFrmConvert.CmbBxRatesSelectedIndex.Equals(Rates.MatrixRates.Key))
        setInit(AlignmentStyle.GetInstance.SetSelectedItem(CType(pFrmConvert.CmbBxAlignmentSelectedItem, String)).SelectedItem)
        initAndGetEquat()
    End Sub

    Private Sub setRatesSiteFromSitePresenter()
        pScopeRatesFacade.Site.Longitude.Rad = pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePri.Rad
        pScopeRatesFacade.Site.Latitude.Rad = pUserCtrlGauge2AxisCoordSitePresenter.CoordinateSec.Rad

        pCelestialRatesFacade.Site.Longitude.Rad = pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePri.Rad
        pCelestialRatesFacade.Site.Latitude.Rad = pUserCtrlGauge2AxisCoordSitePresenter.CoordinateSec.Rad
    End Sub

    Private Sub setInit(ByRef selectedAlignment As ISFT)
        ' could be any Rates. includes MatrixRates
        If selectedAlignment Is AlignmentStyle.PolarAligned Then
            setInitCelestialScope(CType(InitStateType.Equatorial, ISFT))
            ' point telescope primary axis at celestial pole
            setLatCelestialScopeTo90Deg()

        ElseIf selectedAlignment Is AlignmentStyle.AltazSiteAligned Then
            setInitCelestialScope(CType(InitStateType.Altazimuth, ISFT))

            ' else must be MatrixRates 
        ElseIf selectedAlignment Is AlignmentStyle.CelestialAligned _
        AndAlso pFrmConvert.CmbBxRatesSelectedIndex.Equals(Rates.MatrixRates.Key) Then
            setInitCelestialScope(CType(InitStateType.Celestial, ISFT))
            ' try to get OneTwo from OneTwoPresenter, if not, then use last alignment to init from 
            If pOneTwoPresenter Is Nothing OrElse Not validateOneTwoPresenter(pOneTwoPresenter) Then
                If pLastCmbBxAlignmentSelectedIndex.Equals(AlignmentStyle.PolarAligned.Key) Then
                    setInitCelestialScope(CType(InitStateType.Equatorial, ISFT))
                Else
                    setInitCelestialScope(CType(InitStateType.Altazimuth, ISFT))
                End If
            Else
                ' copies OneTwoPresenter's FabErrors also
                copyOneTwoPresenterFabErrorsToConvertMatrix()
            End If
            ' if switching AlignmentStyles and new style not CelestialAligned, then reset Z123 to 0s
            If Not pLastCmbBxAlignmentSelectedIndex.Equals(pFrmConvert.CmbBxAlignmentSelectedIndex) _
            AndAlso selectedAlignment IsNot AlignmentStyle.CelestialAligned Then
                pScopeRatesFacade.FabErrors.SetFabErrorsDeg(0, 0, 0)
            End If

        Else
            ExceptionService.Notify(eString.UnknownSelectedAlignment)
        End If
    End Sub

    Private Sub setInitCelestialScope(ByRef initStateType As ISFT)
        pScopeRatesFacade.SetInit(initStateType)
        pCelestialRatesFacade.SetInit(initStateType)
    End Sub

    Private Sub setLatCelestialScopeTo90Deg()
        pUserCtrlGauge2AxisCoordSitePresenter.DisplayCoordinates(pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePri.Rad, Units.QtrRev)
        setRatesSiteFromSitePresenter()
    End Sub

    Private Function convertCelestialEquatToAltaz(ByVal priRad As Double, ByVal secRad As Double) As AZdouble
        Return convertEquatToAltaz(pCelestialRatesFacade, priRad, secRad)
    End Function

    Private Function convertScopeEquatToAltaz(ByVal priRad As Double, ByVal secRad As Double) As AZdouble
        Return convertEquatToAltaz(pScopeRatesFacade, priRad, secRad)
    End Function

    Private Function convertEquatToAltaz(ByRef ratesFacade As RatesFacade, ByVal priRad As Double, ByVal secRad As Double) As AZdouble
        ratesFacade.Position.RA.Rad = priRad
        ratesFacade.Position.Dec.Rad = secRad
        ratesFacade.Position.SidT.Rad = getSidT()

        ratesFacade.GetAltaz()

        Dim az As AZdouble = AZdouble.GetInstance
        az.Z = ratesFacade.Position.Az.Rad
        az.A = ratesFacade.Position.Alt.Rad
        Return az
    End Function

    ' use celestial's trig to get equat from input altaz, then use scope's convert method (matrix, if so indicated by user) to 
    ' convert the equat back to altaz; if matrix, result is that Z123 errors change initial altaz into final adjusted altaz coordinates
    Private Function convertSiteToCelestialToScopeAltaz(ByVal priRad As Double, ByVal secRad As Double) As AZdouble
        pCelestialRatesFacade.Position.Az.Rad = priRad
        pCelestialRatesFacade.Position.Alt.Rad = secRad
        pCelestialRatesFacade.Position.SidT.Rad = getSidT()

        pCelestialRatesFacade.GetEquat()

        Return convertScopeEquatToAltaz(pCelestialRatesFacade.Position.RA.Rad, pCelestialRatesFacade.Position.Dec.Rad)
    End Function

    Private Sub GetEquat()
        pScopeRatesFacade.Position.Az.Rad = pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.CoordinatePri.Rad
        pScopeRatesFacade.Position.Alt.Rad = pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.CoordinateSec.Rad
        pScopeRatesFacade.Position.SidT.Rad = getSidT()

        pScopeRatesFacade.GetEquat()
        pScopeRatesFacade.CalcRates()
    End Sub

    Private Sub GetAltaz()
        pScopeRatesFacade.Position.RA.Rad = pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.CoordinatePri.Rad
        pScopeRatesFacade.Position.Dec.Rad = pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.CoordinateSec.Rad
        pScopeRatesFacade.Position.SidT.Rad = getSidT()

        pScopeRatesFacade.GetAltaz()
        pScopeRatesFacade.CalcRates()
    End Sub

    Private Sub initAndGetAltaz()
        pScopeRatesFacade.Init()
        GetAltaz()
        updateCoordDisplays()
    End Sub

    Private Sub initAndGetEquat()
        pScopeRatesFacade.Init()
        GetEquat()
        updateCoordDisplays()
    End Sub

    ' switch to altaz, then init, then recover Z123 errors and finally switch back to original celestial init state
    Private Sub initAndGetEquatCelestial()
        pScopeRatesFacade.SetInit(CType(InitStateType.Altazimuth, ISFT))
        pScopeRatesFacade.Init()

        If pOneTwoPresenter.UseCorrections Then
            pScopeRatesFacade.FabErrors.CopyFrom(pOneTwoPresenter.FabErrors)
        End If

        pScopeRatesFacade.SetInit(CType(InitStateType.Celestial, ISFT))
        GetEquat()
        updateCoordDisplays()
    End Sub

    Private Sub BuildRatesComboBoxDataModel()
        pFrmConvert.CmbBxRatesDataSource = Rates.GetInstance.FirstItem.DataSource
    End Sub

    Private Sub BuildAlignmentComboBoxDataModel()
        Dim holdSelectedIndex As Int32 = pFrmConvert.CmbBxAlignmentSelectedIndex

        Dim alignmentStyleArray As AlignmentStyleArray = Coordinates.AlignmentStyleArray.GetInstance
        ' .BuildArray adds CelestialAligned if MatrixRates is passed in
        alignmentStyleArray.BuildArray(Rates.GetInstance.FirstItem.MatchKey(pFrmConvert.CmbBxRatesSelectedIndex))
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
        pLastCmbBxAlignmentSelectedIndex = pFrmConvert.CmbBxAlignmentSelectedIndex
    End Sub

    Private Sub UpdateToCurrentSidT()
        pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT.DisplayCoordinate(calcSidTNow)
        GetEquat()
        updateCoordDisplays()
    End Sub

    ' sidereal time is RA on local meridian, taking into account longitude
    Private Function getSidT() As Double
        Return pEmath.ValidRad(pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT.Coordinate.Rad - _
                               pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePri.Rad)
    End Function

    ' use Site's longitude to set sidereal time's longitude
    Private Function calcSidTNow() As Double
        pTime.Longitude.Rad = pUserCtrlGauge2AxisCoordSitePresenter.CoordinatePri.Rad
        Return pTime.CalcSidTNow
    End Function

    Private Sub updateCoordDisplays()
        Dim position As Position = pScopeRatesFacade.Position

        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat.DisplayCoordinates(position.RA.Rad, position.Dec.Rad)
        position.Az.Rad = pEmath.ValidRad(position.Az.Rad)
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.DisplayCoordinates(position.Az.Rad, position.Alt.Rad)
        ' not SidT as position's SidT's calculation includes site longitude
        'pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT.DisplayCoordinate(position.SidT.Rad)

        pFrmConvert.LblPriAxisTrackRateText = PriAxisLabel & formatRateArcsecSidSec(pScopeRatesFacade.PriAxisRadPerSidSec) & " arcsec/sec"
        pFrmConvert.LblSecAxisTrackRateText = SecAxisLabel & formatRateArcsecSidSec(pScopeRatesFacade.SecAxisRadPerSidSec) & " arcsec/sec"
        pFrmConvert.LblTiertiaryAxisTrackRateText = TieAxisLabel & formatRateDegSidSec(pScopeRatesFacade.FieldRotationRadPerSidSec) & " deg/sec"

        updateOrthoProjectPoint()
    End Sub

    Private Function formatRateArcsecSidSec(ByVal rateRadPerSidSec As Double) As String
        Return Format(rateRadPerSidSec * Units.RadToArcsec, "##0.###")
    End Function

    Protected Function formatRateDegSidSec(ByVal rateRadPerSidSec As Double) As String
        Return Format(rateRadPerSidSec * Units.RadToDeg, "##0.###")
    End Function

    Private Sub gridLinesPerQuadrant(ByVal count As Int32)
        Dim resolutionDeg As Double = 90 / count

        Array.ForEach(pCoordFrameworkRenderers.ToArray, _
        AddressOf New ActionPredicateWrapper(Of Object, Double)(AddressOf setGridLinesPerQuadrant, resolutionDeg).Action)

        render()
    End Sub

    Private Sub setGridLinesPerQuadrant(ByVal CoordFrameworkRenderer As Object, ByVal resolutionDeg As Double)
        CType(CoordFrameworkRenderer, CoordFrameworkRenderer).GridResolutionDeg = resolutionDeg
    End Sub

    Private Sub siteGrid(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddCoordFrameworkRenderer(pSiteCoordFrameworkRenderer)
        Else
            pSPRenderer.RemoveCoordFrameworkRenderer(pSiteCoordFrameworkRenderer)
        End If
        render()
    End Sub

    Private Sub celestialGrid(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddCoordFrameworkRenderer(pCelestialCoordFrameworkRenderer)
        Else
            pSPRenderer.RemoveCoordFrameworkRenderer(pCelestialCoordFrameworkRenderer)
        End If
        render()
    End Sub

    Private Sub scopeEquatGrid(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddCoordFrameworkRenderer(pScopeEquatCoordFrameworkRenderer)
        Else
            pSPRenderer.RemoveCoordFrameworkRenderer(pScopeEquatCoordFrameworkRenderer)
        End If
        render()
    End Sub

    Private Sub scopeAltazGrid(ByVal checked As Boolean)
        If checked Then
            pSPRenderer.AddCoordFrameworkRenderer(pScopeAltazCoordFrameworkRenderer)
        Else
            pSPRenderer.RemoveCoordFrameworkRenderer(pScopeAltazCoordFrameworkRenderer)
        End If
        render()
    End Sub

#Region "Scope Pilot"
    Private Sub render()
        pMvpUserCtrl2MeasurementsGaugeBase.Render()
    End Sub

    Private Sub measurementsToPoint(ByRef [object] As Object)
        Dim meas As Double() = CType([object], Double())
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.DisplayCoordinates(meas(0), meas(1))
        GetEquat()
        updateCoordDisplays()
    End Sub

    Private Sub defaultAzGauge()
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.SetExpCoordTypes(CoordExpType.FormattedDegree, CoordExpType.FormattedDegree)
        pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.SetRenderers(DegreeCircGaugeRenderer.GetInstance, DegreeNegCircGaugeRenderer.GetInstance)
    End Sub

    Private Sub buildObjectToRender()
        pScopePilotObjectToRender.PriRad = pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.CoordinatePri.Rad
        pScopePilotObjectToRender.SecRad = pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz.CoordinateSec.Rad
        pScopePilotObjectToRender.TiltPriRad = pIGauge3AxisCoordPresenterCenter.CoordinatePri.Rad
        pScopePilotObjectToRender.TiltSecRad = pIGauge3AxisCoordPresenterCenter.CoordinateSec.Rad
        pScopePilotObjectToRender.TiltTierRad = pIGauge3AxisCoordPresenterCenter.CoordinateTier.Rad
        pScopePilotObjectToRender.PrimaryColor = CType(AxisColors.Primary.Tag, Drawing.Color)
        pScopePilotObjectToRender.SecondaryColor = CType(AxisColors.Secondary.Tag, Drawing.Color)

        pScopePilotObjectToRender.DisplayInits = _
                pFrmConvert.DisplayInits _
                AndAlso pScopeRatesFacade IsNot Nothing _
                AndAlso pScopeRatesFacade.ConvertMatrix IsNot Nothing

        If pScopePilotObjectToRender.DisplayInits Then
            pScopePilotObjectToRender.Init1PriRad = pScopeRatesFacade.ConvertMatrix.One.Az.Rad
            pScopePilotObjectToRender.Init1SecRad = pScopeRatesFacade.ConvertMatrix.One.Alt.Rad
            pScopePilotObjectToRender.Init2PriRad = pScopeRatesFacade.ConvertMatrix.Two.Az.Rad
            pScopePilotObjectToRender.Init2SecRad = pScopeRatesFacade.ConvertMatrix.Two.Alt.Rad
        End If

        pMvpUserCtrl2MeasurementsGaugeBase.IRenderer.ObjectToRender = pScopePilotObjectToRender
    End Sub

    Private Sub updateOrthoProjectPoint()
        If pScopePilotObjectToRender IsNot Nothing Then
            buildObjectToRender()
            render()
        End If
    End Sub
#End Region
#End Region

End Class
