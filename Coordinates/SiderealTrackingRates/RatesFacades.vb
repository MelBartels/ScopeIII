#Region "imports"
Imports BartelsLibrary.DelegateSigs
Imports ScopeIII.Coordinates.DelegateSigs
#End Region

Public Class RatesFacades

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Member"
    Public ScopeRatesFacade As RatesFacade
    Public CelestialRatesFacade As RatesFacade
    Public KingRateArcsecSec As Double
    Public ConvertCelestialEquatToSiteAltazDelegate As [Delegate]
    Public ConvertScopeAltazToSiteAltazDelegate As [Delegate]
    Public TrackRateFromEquatCoords As [Delegate]
    Public KingRateFromEquatCoords As [Delegate]
    Public FieldRotationAngleFromEquatCoords As [Delegate]
#End Region

#Region "Private and Protected Members"
    Private pAzConvertEquatToAltaz As AZdouble
    Private pTime As Time
    Private pKingRate As KingRate
    Private pLongitudeCoord As Coordinate
    Private pSidTCoord As Coordinate
    Private pUncorrectedPosition As Position = PositionArray.GetInstance.GetPosition
    'mlb
    Public pCelestialErrorsCalculatorFacade As CelestialErrorsCalculatorFacade
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RatesFacades
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RatesFacades = New RatesFacades
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pTime = Time.GetInstance
        pKingRate = KingRate.GetInstance
        pAzConvertEquatToAltaz = AZdouble.GetInstance
        pCelestialErrorsCalculatorFacade = CelestialErrorsCalculatorFacade.GetInstance
        ConvertCelestialEquatToSiteAltazDelegate = New DelegateDblDblAsAZDouble(AddressOf convertCelestialEquatToSiteAltaz)
        ConvertScopeAltazToSiteAltazDelegate = New DelegateDblDblAsAZDouble(AddressOf convertScopeAltazToSiteAltaz)
        TrackRateFromEquatCoords = New DelegateAxisDblDblAsBool(AddressOf calcTrackRateFromEquatCoords)
        KingRateFromEquatCoords = New DelegateDblDblDblAsDbl(AddressOf GetKingRateRadSec)
        FieldRotationAngleFromEquatCoords = New DelegateDblDblAsDbl(AddressOf getCelestialRatesFacadeFieldRotationAngle)
    End Sub

    Public Shared Function GetInstance() As RatesFacades
        Return New RatesFacades
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property UncorrectedPosition() As Position
        Get
            Return pUncorrectedPosition
        End Get
        Set(ByVal value As Position)
            pUncorrectedPosition = value
        End Set
    End Property

    Public Sub RegisterReferences(ByRef longitudeCoord As Coordinate, ByRef sidTCoord As Coordinate, ByRef celestialErrorsCalculatorFacade As CelestialErrorsCalculatorFacade)
        pLongitudeCoord = longitudeCoord
        pSidTCoord = sidTCoord
        pCelestialErrorsCalculatorFacade = celestialErrorsCalculatorFacade
    End Sub

    Public Sub BuildAndInitRates()
        ' ..Facade.Build calls RatesFactory

        ScopeRatesFacade = RatesFacade.GetInstance
        BuildScopeRatesFacade(CType(Rates.FormulaRates, ISFT))
        ScopeRatesFacade.SetInit(CType(InitStateType.Equatorial, ISFT))

        CelestialRatesFacade = RatesFacade.GetInstance
        CelestialRatesFacade.Build(CType(Rates.TrigRates, ISFT))
        CelestialRatesFacade.SetInit(CType(InitStateType.Equatorial, ISFT))

        InitRates()
    End Sub

    Public Sub InitRates()
        CelestialRatesFacade.Init()
        ScopeRatesFacade.Init()
    End Sub

    Public Sub BuildScopeRatesFacade(ByRef rates As ISFT)
        ScopeRatesFacade.Build(rates)
    End Sub

    Public Sub SetInits(ByRef initStateType As ISFT)
        CelestialRatesFacade.SetInit(InitStateType)
        ScopeRatesFacade.SetInit(InitStateType)
    End Sub

    Public Sub InitAndGetAltaz(ByVal RaRad As Double, ByVal DecRad As Double)
        CelestialRatesFacade.Init()
        ScopeRatesFacade.Init()
        GetAltaz(RaRad, DecRad)
    End Sub

    Public Sub InitAndGetEquat(ByVal AzRad As Double, ByVal AltRad As Double)
        CelestialRatesFacade.Init()
        ScopeRatesFacade.Init()
        GetEquat(AzRad, AltRad)
    End Sub

    Public Sub UpdateLongitudeLatitude(ByVal longitudeRad As Double, ByVal latitudeRad As Double, ByVal polarAligned As Boolean)
        ' celestial
        CelestialRatesFacade.Site.Longitude.Rad = longitudeRad
        CelestialRatesFacade.Site.Latitude.Rad = latitudeRad

        ' scope: if polar, then lat = +-90d
        ScopeRatesFacade.Site.Longitude.Rad = longitudeRad
        If polarAligned Then
            If latitudeRad >= 0 Then
                ScopeRatesFacade.Site.Latitude.Rad = Units.QtrRev
            Else
                ScopeRatesFacade.Site.Latitude.Rad = -Units.QtrRev
            End If
        Else
            ScopeRatesFacade.Site.Latitude.Rad = latitudeRad
        End If
    End Sub

    ' use private coordinate to read SidT from throughout this facade
    Public Sub SetSidT(ByVal SidTRad As Double)
        getSidTCoord.Rad = SidTRad
    End Sub

    ' use Site's longitude to set sidereal time's longitude
    Public Function CalcSidTNow() As Double
        pTime.Longitude.Rad = getLongitudeCoord.Rad
        Return pTime.CalcSidTNow
    End Function

    ' sets UncorrectedPosition
    Public Sub GetEquat(ByVal AzRad As Double, ByVal AltRad As Double)
        setCorrectedEquatFromAltazCoords(AzRad, AltRad, CelestialRatesFacade)
        updateScopeAltazFromEquatCoords(CelestialRatesFacade.Position.RA.Rad, CelestialRatesFacade.Position.Dec.Rad)
        commonXFormStrategy()
        ScopeRatesFacade.Position.RA.Rad = CelestialRatesFacade.Position.RA.Rad
        ScopeRatesFacade.Position.Dec.Rad = CelestialRatesFacade.Position.Dec.Rad
    End Sub

    ' sets UncorrectedPosition
    Public Sub GetEquatFromScopeGauges(ByVal AzRad As Double, ByVal AltRad As Double)
        setCorrectedEquatFromAltazCoords(AzRad, AltRad, ScopeRatesFacade)
        getRatesFacadeAltaz(ScopeRatesFacade.Position.RA.Rad, ScopeRatesFacade.Position.Dec.Rad, CelestialRatesFacade)
        commonXFormStrategy()
    End Sub

    ' sets UncorrectedPosition
    Public Sub GetAltaz(ByVal RaRad As Double, ByVal DecRad As Double)
        getRatesFacadeAltaz(RaRad, DecRad, CelestialRatesFacade)
        getRatesFacadeAltaz(RaRad, DecRad, ScopeRatesFacade)
        commonXFormStrategy()
        ' otherwise displaying coordinates may change as equat coords are uncorrected
        CelestialRatesFacade.Position.RA.Rad = RaRad
        CelestialRatesFacade.Position.Dec.Rad = DecRad
        ScopeRatesFacade.Position.RA.Rad = RaRad
        ScopeRatesFacade.Position.Dec.Rad = DecRad
    End Sub

    Public Sub GetKingRate(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double)
        KingRateArcsecSec = pKingRate.SidTrackRatio(pKingRate.AtTruePole(RaRad, DecRad, SidTRad, CelestialRatesFacade.Site.Latitude.Rad))
    End Sub

    Public Function GetKingRateRadSec(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double) As Double
        GetKingRate(RaRad, DecRad, SidTRad)
        Return KingRateArcsecSec * Units.ArcsecToRad
    End Function

    ' init is 1 based
    Public Function ConvertMatrixInitToSiteAltaz(ByVal init As Int32) As AZdouble
        ' display by scope equat
        Select Case init
            Case 1
                Return convertCelestialEquatToSiteAltaz(ScopeRatesFacade.ConvertMatrix.One.RA.Rad, ScopeRatesFacade.ConvertMatrix.One.Dec.Rad, ScopeRatesFacade.ConvertMatrix.One.SidT.Rad)
            Case 2
                Return convertCelestialEquatToSiteAltaz(ScopeRatesFacade.ConvertMatrix.Two.RA.Rad, ScopeRatesFacade.ConvertMatrix.Two.Dec.Rad, ScopeRatesFacade.ConvertMatrix.Two.SidT.Rad)
            Case 3
                If ScopeRatesFacade.ConvertMatrix.Three.Init Then
                    Return convertCelestialEquatToSiteAltaz(ScopeRatesFacade.ConvertMatrix.Three.RA.Rad, ScopeRatesFacade.ConvertMatrix.Three.Dec.Rad, ScopeRatesFacade.ConvertMatrix.Three.SidT.Rad)
                Else
                    Dim position As Position = ScopeRatesFacade.ConvertMatrix.RecoverInit(3)
                    Return convertCelestialEquatToSiteAltaz(position.RA.Rad, position.Dec.Rad, position.SidT.Rad)
                End If
        End Select
        ' display by scope altaz
        'Select Case init
        '    Case 1
        '        Return convertScopeAltazToSiteAltaz(ScopeRatesFacade.ConvertMatrix.One.Az.Rad, ScopeRatesFacade.ConvertMatrix.One.Alt.Rad)
        '    Case 2
        '        Return convertScopeAltazToSiteAltaz(ScopeRatesFacade.ConvertMatrix.Two.Az.Rad, ScopeRatesFacade.ConvertMatrix.Two.Alt.Rad)
        '    Case 3
        '        If ScopeRatesFacade.ConvertMatrix.Three.Init Then
        '            Return convertScopeAltazToSiteAltaz(ScopeRatesFacade.ConvertMatrix.Three.Az.Rad, ScopeRatesFacade.ConvertMatrix.Three.Alt.Rad)
        '        Else
        '            Dim position As Position = ScopeRatesFacade.ConvertMatrix.RecoverInit(3)
        '            Return convertCelestialEquatToSiteAltaz(position.Az.Rad, position.Alt.Rad, position.SidT.Rad)
        '        End If
        'End Select
        Return Nothing
    End Function

    Public Sub MeridianFlipChanged(ByRef state As ISFT)
        If state Is Nothing Then
            ScopeRatesFacade.ICoordXForm.MeridianFlip.Possible = False
            ScopeRatesFacade.ICoordXForm.MeridianFlip.Required = False
        Else
            ScopeRatesFacade.ICoordXForm.MeridianFlip.Possible = True
            ScopeRatesFacade.ICoordXForm.MeridianFlip.Required = True
            If state Is MeridianFlipState.PointingWest Then
                ScopeRatesFacade.ICoordXForm.MeridianFlip.State = MeridianFlipState.PointingWest
            ElseIf state Is MeridianFlipState.PointingEast Then
                ScopeRatesFacade.ICoordXForm.MeridianFlip.State = MeridianFlipState.PointingEast
            End If
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Function getLongitudeCoord() As Coordinate
        If pLongitudeCoord Is Nothing Then
            pLongitudeCoord = Coordinate.GetInstance
        End If
        Return pLongitudeCoord
    End Function

    Private Function getSidTCoord() As Coordinate
        If pSidTCoord Is Nothing Then
            pSidTCoord = Coordinate.GetInstance
        End If
        Return pSidTCoord
    End Function

    Private Function getCelestialErrorsCalculatorFacade() As CelestialErrorsCalculatorFacade
        If pCelestialErrorsCalculatorFacade Is Nothing Then
            pCelestialErrorsCalculatorFacade = CelestialErrorsCalculatorFacade.GetInstance
        End If
        Return pCelestialErrorsCalculatorFacade
    End Function

    Private Sub commonXFormStrategy()
        calcRatesFromEquatCoords(ScopeRatesFacade)
        setAxis3FieldRotation(ScopeRatesFacade)
        setAxis3FieldRotation(CelestialRatesFacade)
        GetKingRate(CelestialRatesFacade.Position.RA.Rad, CelestialRatesFacade.Position.Dec.Rad, CelestialRatesFacade.Position.SidT.Rad)
    End Sub

    ' sidereal time is RA on local meridian
    Private Function getSidT() As Double
        Return eMath.ValidRad(getSidTCoord.Rad)
    End Function

    ' for displaying the equatorial grid calculated from site altaz; 
    ' includes celestial errors;
    Private Function convertCelestialEquatToSiteAltaz(ByVal RaRad As Double, ByVal DecRad As Double) As AZdouble
        Return convertCelestialEquatToSiteAltaz(RaRad, DecRad, getSidT)
    End Function

    Private Function convertCelestialEquatToSiteAltaz(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double) As AZdouble
        ' have corrected equat coord, so find uncorrected equat values
        Dim RaDec() As Double = getCelestialErrorsCalculatorFacade.AdaptForCelestialErrors( _
                RaRad, _
                DecRad, _
                SidTRad, _
                CelestialRatesFacade.Site.Latitude.Rad, _
                False)

        CelestialRatesFacade.GetAltaz(RaDec(0), RaDec(1), SidTRad)

        pAzConvertEquatToAltaz.Z = CelestialRatesFacade.Position.Az.Rad
        pAzConvertEquatToAltaz.A = CelestialRatesFacade.Position.Alt.Rad
        Return pAzConvertEquatToAltaz
    End Function

    ' for plotting of scope altaz coordinates from site altaz perspective;
    ' starting w/ scope altaz, get equat coord, the common coord for both scope and site;
    ' if ConvertMatrix, then Z123 errors included
    Private Function convertScopeAltazToSiteAltaz(ByVal AzRad As Double, ByVal AltRad As Double) As AZdouble
        Return convertScopeAltazToSiteAltaz(AzRad, AltRad, getSidT)
    End Function

    Private Function convertScopeAltazToSiteAltaz(ByVal AzRad As Double, ByVal AltRad As Double, ByVal SidTRad As Double) As AZdouble
        ScopeRatesFacade.GetEquat(AzRad, AltRad, SidTRad)

        ' adjust equat for celestial errors using site lat;
        Dim RaDec() As Double = getCelestialErrorsCalculatorFacade.AdaptForCelestialErrors( _
                ScopeRatesFacade.Position.RA.Rad, _
                ScopeRatesFacade.Position.Dec.Rad, _
                SidTRad, _
                CelestialRatesFacade.Site.Latitude.Rad, _
                False)

        ' get altaz from site perspective, which will be adjusted for both Z123 and celestial errors
        CelestialRatesFacade.GetAltaz(RaDec(0), RaDec(1), SidTRad)

        pAzConvertEquatToAltaz.Z = CelestialRatesFacade.Position.Az.Rad
        pAzConvertEquatToAltaz.A = CelestialRatesFacade.Position.Alt.Rad
        Return pAzConvertEquatToAltaz
    End Function

    Private Sub setCorrectedEquatFromAltazCoords(ByVal AzRad As Double, ByVal AltRad As Double, ByVal ratesFacade As RatesFacade)
        setCorrectedEquatFromAltazCoords(AzRad, AltRad, getSidT, ratesFacade)
    End Sub

    Private Sub setCorrectedEquatFromAltazCoords(ByVal AzRad As Double, ByVal AltRad As Double, ByVal SidTRad As Double, ByVal ratesFacade As RatesFacade)
        ratesFacade.GetEquat(AzRad, AltRad, SidTRad)
        UncorrectedPosition.CopyFrom(ratesFacade.Position)

        ' have uncorrected equat coord, so find corrected equat values
        Dim RaDec() As Double = getCelestialErrorsCalculatorFacade.AdaptForCelestialErrors( _
                ratesFacade.Position.RA.Rad, _
                ratesFacade.Position.Dec.Rad, _
                ratesFacade.Position.SidT.Rad, _
                ratesFacade.Site.Latitude.Rad, _
                True)

        ratesFacade.Position.RA.Rad = RaDec(0)
        ratesFacade.Position.Dec.Rad = RaDec(1)
    End Sub

    Private Sub getRatesFacadeAltaz(ByVal RaRad As Double, ByVal DecRad As Double, ByVal ratesFacade As RatesFacade)
        getRatesFacadeAltaz(RaRad, DecRad, getSidT, ratesFacade)
    End Sub

    Private Sub getRatesFacadeAltaz(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double, ByVal ratesFacade As RatesFacade)
        ' have corrected equat coord, so find uncorrected equat values
        Dim RaDec() As Double = getCelestialErrorsCalculatorFacade.AdaptForCelestialErrors( _
                RaRad, _
                DecRad, _
                SidTRad, _
                ratesFacade.Site.Latitude.Rad, _
                False)

        ratesFacade.GetAltaz(RaDec(0), RaDec(1), SidTRad)
        UncorrectedPosition.CopyFrom(ratesFacade.Position)
    End Sub

    Private Sub calcRatesFromEquatCoords(ByVal ratesFacade As RatesFacade)
        Dim holdPosition As Position = PositionArray.GetInstance.GetPosition
        holdPosition.CopyFrom(ratesFacade.Position)

        ratesFacade.Position.CopyFrom(CelestialRatesFacade.Position)
        ratesFacade.CalcRates()

        If pCelestialErrorsCalculatorFacade.UseCalculator Then
            ' altaz align: CelestialRatesFacade.Site.Latitude.Rad < 90deg, ScopeRatesFacade.Site.Latitude.Rad < 90deg;
            ' equat align: CelestialRatesFacade.Site.Latitude.Rad < 90deg, ScopeRatesFacade.Site.Latitude.Rad = 90deg;
            ratesFacade.CalcCorrectedRates( _
                    pCelestialErrorsCalculatorFacade.CelestialErrorsCalculator, _
                    CelestialRatesFacade.Position, _
                    pCelestialErrorsCalculatorFacade.ToPosition, _
                    pCelestialErrorsCalculatorFacade.FromDate, _
                    pCelestialErrorsCalculatorFacade.ToDate, _
                    pCelestialErrorsCalculatorFacade.IncludePrecession, _
                    pCelestialErrorsCalculatorFacade.IncludeNutationAnnualAberration, _
                    pCelestialErrorsCalculatorFacade.IncludeRefraction, _
                    CelestialRatesFacade.Site.Latitude.Rad)
        End If

        ratesFacade.Position.CopyFrom(holdPosition)
        holdPosition.Available = True
    End Sub

    Private Function calcTrackRateFromEquatCoords(ByRef axis As Axis, ByVal RaRad As Double, ByVal DecRad As Double) As Double
        Dim holdPosition As Position = PositionArray.GetInstance.GetPosition
        holdPosition.CopyFrom(ScopeRatesFacade.Position)

        ScopeRatesFacade.Position.RA.Rad = RaRad
        ScopeRatesFacade.Position.Dec.Rad = DecRad
        calcRatesFromEquatCoords(ScopeRatesFacade)

        ScopeRatesFacade.Position.CopyFrom(holdPosition)
        holdPosition.Available = True

        Dim tr As TrackRatesDataModel.TrackRate
        If axis.SelectedItem Is axis.PriAxis Then
            tr = ScopeRatesFacade.PriAxisTrackRate
        Else
            tr = ScopeRatesFacade.SecAxisTrackRate
        End If

        If pCelestialErrorsCalculatorFacade.UseCalculator Then
            Return tr.CorrectedRateRadPerSidSec
        Else
            Return tr.RateRadPerSidSec
        End If
    End Function

    Private Sub updateScopeAltazFromEquatCoords(ByVal RaRad As Double, ByVal DecRad As Double)
        updateScopeAltazFromEquatCoords(RaRad, DecRad, getSidT)
    End Sub

    Private Sub updateScopeAltazFromEquatCoords(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double)
        ' have corrected equat coord, so find uncorrected equat values
        Dim RaDec() As Double = getCelestialErrorsCalculatorFacade.AdaptForCelestialErrors( _
                RaRad, _
                DecRad, _
                SidTRad, _
                ScopeRatesFacade.Site.Latitude.Rad, _
                False)

        ScopeRatesFacade.GetAltaz(RaDec(0), RaDec(1), SidTRad)
    End Sub

    Private Sub setAxis3FieldRotation(ByVal ratesFacade As RatesFacade)
        ratesFacade.Position.Axis3.Rad = ratesFacade.Rates.GetFieldRotationAngle
    End Sub

    Private Function getCelestialRatesFacadeFieldRotationAngle(ByVal RaRad As Double, ByVal DecRad As Double) As Double
        Dim holdSidTRad As Double = CelestialRatesFacade.Position.SidT.Rad
        Dim fra As Double = getCelestialRatesFacadeFieldRotationAngle(RaRad, DecRad, getSidT)
        CelestialRatesFacade.Position.SidT.Rad = holdSidTRad
        Return fra
    End Function

    Private Function getCelestialRatesFacadeFieldRotationAngle(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double) As Double
        CelestialRatesFacade.Position.SidT.Rad = getSidT()
        Return CelestialRatesFacade.Rates.GetFieldRotationAngle
    End Function
#End Region
End Class
