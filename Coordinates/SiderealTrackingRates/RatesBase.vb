Public MustInherit Class RatesBase
    Implements IRates

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pTrackRatesDataModel As TrackRatesDataModel
    Protected pInitStateTemplate As InitStateTemplate
    Protected pFR As FieldRotation
    Protected pAlignmentStyles As ArrayList
    Protected pLatitude As Coordinate
    Protected pCorrectedRatesParmsFacade As CorrectedRatesParmsFacade
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RatesBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RatesBase = New RatesBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pTrackRatesDataModel = TrackRatesDataModel.GetInstance
        pFR = FieldRotation.GetInstance
        pLatitude = Coordinate.GetInstance
    End Sub

    'Public Shared Function GetInstance() As RatesBase
    '    Return New RatesBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function InitStateTemplate() As InitStateTemplate Implements IRates.InitStateTemplate
        Return pInitStateTemplate
    End Function

    Public Function Init() As Boolean Implements IRates.Init
        Return pInitStateTemplate.IInit.Init
    End Function

    Public Function PriAxisTrackRate() As TrackRatesDataModel.TrackRate Implements IRates.PriAxisTrackRate
        Return pTrackRatesDataModel.GetPriAxisTrackRate
    End Function

    Public Function SecAxisTrackRate() As TrackRatesDataModel.TrackRate Implements IRates.SecAxisTrackRate
        Return pTrackRatesDataModel.GetSecAxisTrackRate
    End Function

    Public Function TierAxisTrackRate() As TrackRatesDataModel.TrackRate Implements IRates.TierAxisTrackRate
        Return pTrackRatesDataModel.GetTierAxisTrackRate
    End Function

    Public MustOverride Sub CalcRates() Implements IRates.CalcRates

    Public MustOverride Sub CalcCorrectedRates( _
        ByRef celestialErrorsCalculator As CelestialErrorsCalculator, _
        ByRef startingCorrectedEquatPosition As Position, _
        ByRef toPosition As Position, _
        ByRef fromEpoch As Date, _
        ByRef toEpoch As Date, _
        ByVal includePrecession As Boolean, _
        ByVal includeNutationAnnualAberration As Boolean, _
        ByVal includeRefraction As Boolean, _
        ByVal latitudeRad As Double) Implements IRates.CalcCorrectedRates

    Public Function GetFieldRotationAngle() As Double Implements IRates.GetFieldRotationAngle
        Return pFR.CalcAngleViaTrig(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Sub buildCorrectedRatesParmsFacade( _
        ByRef celestialErrorsCalculator As CelestialErrorsCalculator, _
        ByRef startingCorrectedEquatPosition As Position, _
        ByRef toPosition As Position, _
        ByRef fromEpoch As Date, _
        ByRef toEpoch As Date, _
        ByVal includePrecession As Boolean, _
        ByVal includeNutationAnnualAberration As Boolean, _
        ByVal includeRefraction As Boolean, _
        ByVal latitudeRad As Double)

        pCorrectedRatesParmsFacade = CorrectedRatesParmsFacade.GetInstance
        pCorrectedRatesParmsFacade.SetParms( _
                celestialErrorsCalculator, _
                startingCorrectedEquatPosition, _
                toPosition, _
                fromEpoch, _
                toEpoch, _
                includePrecession, _
                includeNutationAnnualAberration, _
                includeRefraction, _
                latitudeRad)
    End Sub

    ' calcRates...() functions precondition: Az,Alt,SidT set in pInitStateTemplate.ICoordXform.Position

    Protected Sub CalcRatesViaFormula()
        calcAltazRatesViaFormula()
        calcFRRateViaFormula()
    End Sub

    Protected Sub CalcRatesAltazViaElapsedTimeFRViaFormula()
        calcAltazRatesViaElapsedTime()
        calcFRRateViaFormula()
    End Sub

    Protected Sub CalcRatesViaElapsedTime()
        calcAltazRatesViaElapsedTime()
        calcFRRateViaDeltaFR()
    End Sub

    ' unlike protected calcRates...() functions that start with altaz, this CalcRates(...) starts with corrected equat coords

    Protected Sub CalcCorrectedRatesViaFormula(ByRef startingCorrectedEquatPosition As Position)
        CalcCorrectedAltazRatesViaFormula(startingCorrectedEquatPosition)
        calcCorrectedFRRateViaFormula(startingCorrectedEquatPosition)
    End Sub

    Protected Sub CalcCorrectedRatesViaElapsedTime(ByRef startingCorrectedEquatPosition As Position)
        CalcCorrectedAltazRatesViaElapsedTime(startingCorrectedEquatPosition)
        calcCorrectedFRRateViaDeltaFR(startingCorrectedEquatPosition)
    End Sub

    Private Sub calcAltazRatesViaFormula()
        Dim holdPosition As Position = storePosition()

        Dim altaz() As Double = returnAltazRatesViaFormula(pInitStateTemplate.ICoordXform.Position, pInitStateTemplate.ICoordXform.Site.Latitude.Rad)
        pTrackRatesDataModel.GetPriAxisTrackRate.RateRadPerSidSec = altaz(0)
        pTrackRatesDataModel.GetSecAxisTrackRate.RateRadPerSidSec = altaz(1)

        ' .GetEquat before sidT updated
        pInitStateTemplate.ICoordXform.GetEquat()
        pInitStateTemplate.ICoordXform.Position.SidT.Rad += Units.SecToRad
        ' .GetAltaz using updated sidT
        pInitStateTemplate.ICoordXform.GetAltaz()
        altaz = returnAltazRatesViaFormula(pInitStateTemplate.ICoordXform.Position, pInitStateTemplate.ICoordXform.Site.Latitude.Rad)
        pTrackRatesDataModel.GetPriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = altaz(0) - pTrackRatesDataModel.GetPriAxisTrackRate.RateRadPerSidSec
        pTrackRatesDataModel.GetSecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = altaz(1) - pTrackRatesDataModel.GetSecAxisTrackRate.RateRadPerSidSec

        restorePosition(holdPosition)
    End Sub

    Private Function returnAltazRatesViaFormula(ByRef position As Position, ByVal latitudeRad As Double) As Double()
        Dim altaz(1) As Double
        Dim reverseDir As Boolean
        If latitudeRad < 0 Then
            latitudeRad = -latitudeRad
            reverseDir = True
        End If
        ' distance traveled during 1 sidereal second
        altaz(0) = 15 * Units.ArcsecToRad * (Math.Sin(latitudeRad) + (eMath.Cot(Units.QtrRev - position.Alt.Rad)) * Math.Cos(Units.HalfRev - position.Az.Rad) * Math.Cos(latitudeRad))
        altaz(1) = 15 * Units.ArcsecToRad * (Math.Sin(Units.HalfRev - position.Az.Rad) * Math.Cos(latitudeRad))
        If reverseDir Then
            altaz(0) = -altaz(0)
            altaz(1) = -altaz(1)
        End If
        Return altaz
    End Function

    Private Sub calcAltazRatesViaElapsedTime()
        Dim holdPosition As Position = storePosition()

        ' get equat coords in preparation to increment sidereal time
        pInitStateTemplate.ICoordXform.GetEquat()
        ' increment time by 1 sidereal second
        pInitStateTemplate.ICoordXform.Position.SidT.Rad += Units.SecToRad
        ' get 2nd set of altaz coord
        pInitStateTemplate.ICoordXform.GetAltaz()
        ' set rates
        pTrackRatesDataModel.GetPriAxisTrackRate.RateRadPerSidSec = pInitStateTemplate.ICoordXform.Position.Az.Rad - holdPosition.Az.Rad
        pTrackRatesDataModel.GetSecAxisTrackRate.RateRadPerSidSec = pInitStateTemplate.ICoordXform.Position.Alt.Rad - holdPosition.Alt.Rad
        ' store updated altaz coord
        Dim t2Position As Position = PositionArray.GetInstance.GetPosition
        t2Position.CopyFrom(pInitStateTemplate.ICoordXform.Position)
        ' increment time by 1 sidereal second
        pInitStateTemplate.ICoordXform.Position.SidT.Rad += Units.SecToRad
        ' get 3rd set of altaz coord
        pInitStateTemplate.ICoordXform.GetAltaz()
        ' set delta rates
        pTrackRatesDataModel.GetPriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec _
            = pInitStateTemplate.ICoordXform.Position.Az.Rad - t2Position.Az.Rad - pTrackRatesDataModel.GetPriAxisTrackRate.RateRadPerSidSec
        pTrackRatesDataModel.GetSecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec _
            = pInitStateTemplate.ICoordXform.Position.Alt.Rad - t2Position.Alt.Rad - pTrackRatesDataModel.GetSecAxisTrackRate.RateRadPerSidSec

        t2Position.Available = True
        restorePosition(holdPosition)
    End Sub

    ' celestial errors will not appear as formulae not aware of refraction
    Private Sub CalcCorrectedAltazRatesViaFormula(ByRef startingCorrectedEquatPosition As Position)
        Dim holdPosition As Position = storePosition()

        setInitStateTemplatePositionToUncorrectedEquatCoords(startingCorrectedEquatPosition)
        pInitStateTemplate.ICoordXform.GetAltaz()
        ' set rates
        Dim rates() As Double = returnAltazRatesViaFormula(pInitStateTemplate.ICoordXform.Position, pInitStateTemplate.ICoordXform.Site.Latitude.Rad)
        pTrackRatesDataModel.GetPriAxisTrackRate.CorrectedRateRadPerSidSec = rates(0)
        pTrackRatesDataModel.GetSecAxisTrackRate.CorrectedRateRadPerSidSec = rates(1)
        ' get uncorrected equat, altaz and rates for a 2nd position that's 1 sidereal second later
        Dim t2Position As Position = getUncorrectedPositionGetAltazFromEquatCoords(startingCorrectedEquatPosition, Units.SecToRad)
        Dim rates2() As Double = returnAltazRatesViaFormula(pInitStateTemplate.ICoordXform.Position, pInitStateTemplate.ICoordXform.Site.Latitude.Rad)
        ' set delta rates
        pTrackRatesDataModel.GetPriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = rates2(0) - rates(0)
        pTrackRatesDataModel.GetSecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = rates2(1) - rates(1)

        t2Position.Available = True
        restorePosition(holdPosition)
    End Sub

    ' altaz of 120/30 (lat=45/sidT=0):
    ' without celestial errors corrections (deg) Ra:  48.6634, Dec:   2.7150, Az:   0.0000, Alt:   0.0000, SidT:   0.0000
    ' with    celestial errors corrections (deg) Ra:  48.7111, Dec:   2.7016, Az:   0.0000, Alt:   0.0000, SidT:   0.0000
    ' altaz of 180/10 (lat=45/sidT=0):
    ' without celestial errors corrections (deg) Ra:   0.0000, Dec: -35.0000, Az:   0.0000, Alt:   0.0000, SidT:   0.0000
    ' with    celestial errors corrections (deg) Ra:   0.0265, Dec: -35.0787, Az:   0.0000, Alt:   0.0000, SidT:   0.0000
    ' explanation of above example: w/ refraction correction, altaz of 180/10 can see further down towards horizon (dec=-35.08 compared to dec=-35)

    ' latitudeRad is always site latitude,
    ' altaz align: pInitStateTemplate.ICoordXform.Site.Latitude < 90 deg, 
    ' equat align: pInitStateTemplate.ICoordXform.Site.Latitude = 90 deg;

    ' strategy: get difference in altaz coords from two sets of corrected equat coords that are separated by a time interval (1 sidereal second)
    '     1) copy equat coords
    '     2) if equat coords are corrected coords, then remove correction from equat coords
    '     3) get altaz coords
    '     4) add sidereal second to original equat coords to create 2nd set of equat coords: 
    '        if equat coords are corrected, then time interval (1 sidereal second) is also corrected; 
    '        difference in corrected equat coords is compressed (principally due to refraction) with subsequent
    '        difference in the altaz coords also being compressed (polar alignment, close to e/s/w horizon)
    '     5) if equat coords are corrected coords, then remove correction from 2nd set of equat coords
    '     6) get 2nd set of altaz coords
    '     7) add 2 sidereal seconds to original equat coords to create 3rd set of equat coords
    '     8) if equat coords are corrected coords, then remove correction from 3rd set of equat coords
    '     9) get 3rd set of altaz coords
    '     10) get difference between first two altaz coords sets
    '     11) get difference between rates from first two altaz coord sets and last two altaz coord sets

    Private Sub CalcCorrectedAltazRatesViaElapsedTime(ByRef startingCorrectedEquatPosition As Position)
        Dim holdPosition As Position = storePosition()

        Dim t1Position As Position = getUncorrectedPositionGetAltazFromEquatCoords(startingCorrectedEquatPosition, 0)
        Dim t2Position As Position = getUncorrectedPositionGetAltazFromEquatCoords(startingCorrectedEquatPosition, Units.SecToRad)
        Dim t3Position As Position = getUncorrectedPositionGetAltazFromEquatCoords(startingCorrectedEquatPosition, 2 * Units.SecToRad)

        pTrackRatesDataModel.GetPriAxisTrackRate.CorrectedRateRadPerSidSec = t2Position.Az.Rad - t1Position.Az.Rad
        pTrackRatesDataModel.GetSecAxisTrackRate.CorrectedRateRadPerSidSec = t2Position.Alt.Rad - t1Position.Alt.Rad
        pTrackRatesDataModel.GetPriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = t3Position.Az.Rad + t1Position.Az.Rad - 2 * t2Position.Az.Rad
        pTrackRatesDataModel.GetSecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = t3Position.Alt.Rad + t1Position.Alt.Rad - 2 * t2Position.Alt.Rad

        t1Position.Available = True
        t2Position.Available = True
        t3Position.Available = True
        restorePosition(holdPosition)
    End Sub

    Private Sub calcFRRateViaFormula()
        Dim holdPosition As Position = storePosition()

        pInitStateTemplate.ICoordXform.GetEquat()
        pTrackRatesDataModel.GetTierAxisTrackRate.RateRadPerSidSec = pFR.CalcRateSidTrackViaFormula(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
        pInitStateTemplate.ICoordXform.Position.SidT.Rad += Units.SecToRad
        ' .CalcRateSidTrackViaFormula uses altaz coords
        pInitStateTemplate.ICoordXform.GetAltaz()

        pTrackRatesDataModel.GetTierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec _
            = pFR.CalcRateSidTrackViaFormula(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude) _
            - pTrackRatesDataModel.GetTierAxisTrackRate.RateRadPerSidSec

        restorePosition(holdPosition)
    End Sub

    Private Sub calcFRRateViaDeltaFR()
        Dim holdPosition As Position = storePosition()

        pInitStateTemplate.ICoordXform.GetEquat()
        pTrackRatesDataModel.GetTierAxisTrackRate.RateRadPerSidSec = pFR.CalcRateSidTrackViaDeltaFR(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
        pInitStateTemplate.ICoordXform.Position.SidT.Rad += Units.SecToRad

        pTrackRatesDataModel.GetTierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec _
            = pFR.CalcRateSidTrackViaDeltaFR(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude) _
            - pTrackRatesDataModel.GetTierAxisTrackRate.RateRadPerSidSec

        restorePosition(holdPosition)
    End Sub

    Dim pUseFRcorrectedSec As Boolean = False

    ' from RatesFacadeTest.TestCompareCorrectedRates() with uncorrected sidT sec:
    'calcCorrectedFRRateViaFormula: corrected rate ("/sec) -7.60024603438291, (2nd rate) -7.59983718047693, delta rates 0.000408853905972517
    'calcCorrectedFRRateViaDeltaFR: corrected rate ("/sec) -7.56651112519646, (2nd rate) -7.56611092728804, delta rates 0.000400197908416101
    'calcCorrectedFRRateViaDeltaFR: corrected rate ("/sec) -7.56651112519646, (2nd rate) -7.56611092728804, delta rates 0.000400197908416101
    'TestCompareCorrectedRates #1 FR delta difference 8.65599755641662E-06"
    'TestCompareCorrectedRates #2 FR delta difference 0"
    'calcCorrectedFRRateViaFormula: corrected rate ("/sec) 11.7817854434424, (2nd rate) 11.7820244477766, delta rates 0.000239004334174332
    'calcCorrectedFRRateViaDeltaFR: corrected rate ("/sec) 11.7915441758809, (2nd rate) 11.7917826217906, delta rates 0.000238445909672191
    'calcCorrectedFRRateViaDeltaFR: corrected rate ("/sec) 11.7915441758809, (2nd rate) 11.7917826217906, delta rates 0.000238445909672191
    'TestCompareCorrectedRates #1 FR delta difference 5.58424502140802E-07"
    'TestCompareCorrectedRates #2 FR delta difference 0"
    'calcCorrectedFRRateViaFormula: corrected rate ("/sec) 66.172111917146, (2nd rate) 66.1721037693355, delta rates -8.14781776078598E-06
    'calcCorrectedFRRateViaDeltaFR: corrected rate ("/sec) 66.1900949224627, (2nd rate) 66.1900757040405, delta rates -1.92184075183247E-05
    'calcCorrectedFRRateViaDeltaFR: corrected rate ("/sec) 66.1900949224627, (2nd rate) 66.1900757040405, delta rates -1.92184075183247E-05
    'TestCompareCorrectedRates #1 FR delta difference 1.10705897575387E-05"
    'TestCompareCorrectedRates #2 FR delta difference 0"

    ' use corrected equats coords with altaz coords derived from them;
    ' get FR at two corrected equat positions separated by 1 sec of sidT;

    Private Sub calcCorrectedFRRateViaFormula(ByRef startingCorrectedEquatPosition As Position)
        If pUseFRcorrectedSec Then
            calcCorrectedFRRateViaFormulaCorrectedSec(startingCorrectedEquatPosition)
        Else
            calcCorrectedFRRateViaFormulaUncorrectedSec(startingCorrectedEquatPosition)
        End If
    End Sub

    Private Sub calcCorrectedFRRateViaDeltaFR(ByRef startingCorrectedEquatPosition As Position)
        If pUseFRcorrectedSec Then
            calcCorrectedFRRateViaDeltaFRCorrectedSec(startingCorrectedEquatPosition)
        Else
            calcCorrectedFRRateViaDeltaFRUncorrectedSec(startingCorrectedEquatPosition)
        End If
    End Sub

    Private Sub calcCorrectedFRRateViaFormulaCorrectedSec(ByRef startingCorrectedEquatPosition As Position)
        Dim holdPosition As Position = storePosition()

        ' get corrected rate using corrected equat coords and their altaz derived coords
        pInitStateTemplate.ICoordXform.Position.CopyFrom(startingCorrectedEquatPosition)
        pInitStateTemplate.ICoordXform.GetAltaz()
        pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec = pFR.CalcRateSidTrackViaFormula(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
        ' advance corrected equat coords by 1 sidereal sec of time
        pInitStateTemplate.ICoordXform.Position.SidT.Rad += Units.SecToRad
        pInitStateTemplate.ICoordXform.GetAltaz()
        Dim FRrate2 As Double = pFR.CalcRateSidTrackViaFormula(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
        pTrackRatesDataModel.GetTierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = FRrate2 - pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec

        'Debug.WriteLine(correctedFRrateToString("calcCorrectedFRRateViaFormula", FRrate2))

        restorePosition(holdPosition)
    End Sub

    ' use corrected equats coords with altaz coords derived from them;
    ' get FR at two corrected equat positions separated by 1 sec of uncorrected sidT;
    Private Sub calcCorrectedFRRateViaFormulaUncorrectedSec(ByRef startingCorrectedEquatPosition As Position)
        Dim holdPosition As Position = storePosition()

        ' get corrected rate using corrected equat coords and their altaz derived coords
        pInitStateTemplate.ICoordXform.Position.CopyFrom(startingCorrectedEquatPosition)
        pInitStateTemplate.ICoordXform.GetAltaz()
        pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec = pFR.CalcRateSidTrackViaFormula(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
        ' advance corrected equat coords by 1 sidereal sec of uncorrected time:
        ' strategy is to get uncorrected from corrected, += sid sec, get corrected equat coords;
        ' specifically, set u2Position to uncorrected equat coords, copy it to pInitStateTemplate's position, add 1 sec sidT,
        ' get corrected equat coords which copies back to pInitStateTemplate's position, then get altaz coords
        Dim u2Position As Position = setToPositionToUncorrectedEquatCoords(PositionArray.GetInstance.GetPosition, startingCorrectedEquatPosition)
        u2Position.SidT.Rad += Units.SecToRad
        setInitStateTemplatePositionToCorrectedEquatCoords(u2Position)
        pInitStateTemplate.ICoordXform.GetAltaz()
        Dim FRrate2 As Double = pFR.CalcRateSidTrackViaFormula(pInitStateTemplate.ICoordXform.Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
        pTrackRatesDataModel.GetTierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = FRrate2 - pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec

        'Debug.WriteLine(correctedFRrateToString("calcCorrectedFRRateViaFormula", FRrate2))

        u2Position.Available = True
        restorePosition(holdPosition)
    End Sub

    Private Sub calcCorrectedFRRateViaDeltaFRCorrectedSec(ByRef startingCorrectedEquatPosition As Position)
        Dim holdPosition As Position = storePosition()

        Dim c1Position As Position = getPositionGetAltazFromEquatCoords(startingCorrectedEquatPosition, -0.5 * Units.SecToRad)
        Dim c2Position As Position = getPositionGetAltazFromEquatCoords(startingCorrectedEquatPosition, 0.5 * Units.SecToRad)
        Dim c3Position As Position = getPositionGetAltazFromEquatCoords(startingCorrectedEquatPosition, 1.5 * Units.SecToRad)

        pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec _
            = pFR.CalcRateSidTrackViaDeltaFR(c1Position, c2Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)

        Dim FRrate2 As Double = pFR.CalcRateSidTrackViaDeltaFR(c2Position, c3Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
        pTrackRatesDataModel.GetTierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = FRrate2 - pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec

        'Debug.WriteLine(c1Position.ShowCoordDeg)
        'Debug.WriteLine(c2Position.ShowCoordDeg)
        'Debug.WriteLine(c3Position.ShowCoordDeg)
        'Debug.WriteLine(correctedFRrateToString("calcCorrectedFRRateViaDeltaFR", FRrate2))

        c1Position.Available = True
        c2Position.Available = True
        c3Position.Available = True
        restorePosition(holdPosition)
    End Sub

    Private Sub calcCorrectedFRRateViaDeltaFRUncorrectedSec(ByRef startingCorrectedEquatPosition As Position)
        Dim holdPosition As Position = storePosition()

        ' get 3 uncorrected equat coords, separated by sidT sec each, starting 1/2 before given sidT
        Dim u0Position As Position = setToPositionToUncorrectedEquatCoords(PositionArray.GetInstance.GetPosition, startingCorrectedEquatPosition)
        ' get 3 corrected equat coords and derived altaz
        Dim c1Position As Position = getCorrectedPositionGetAltazFromEquatCoords(u0Position, -0.5 * Units.SecToRad)
        Dim c2Position As Position = getCorrectedPositionGetAltazFromEquatCoords(u0Position, 0.5 * Units.SecToRad)
        Dim c3Position As Position = getCorrectedPositionGetAltazFromEquatCoords(u0Position, 1.5 * Units.SecToRad)

        pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec _
            = pFR.CalcRateSidTrackViaDeltaFR(c1Position, c2Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)

        Dim FRrate2 As Double = pFR.CalcRateSidTrackViaDeltaFR(c2Position, c3Position, 0, pInitStateTemplate.ICoordXform.Site.Latitude)
        pTrackRatesDataModel.GetTierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec = FRrate2 - pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec

        'Debug.WriteLine(c1Position.ShowCoordDeg)
        'Debug.WriteLine(c2Position.ShowCoordDeg)
        'Debug.WriteLine(c3Position.ShowCoordDeg)
        'Debug.WriteLine(correctedFRrateToString("calcCorrectedFRRateViaDeltaFR", FRrate2))

        u0Position.Available = True
        c1Position.Available = True
        c2Position.Available = True
        c3Position.Available = True
        restorePosition(holdPosition)
    End Sub

    Private Sub setInitStateTemplatePositionToUncorrectedEquatCoords(ByRef fromPosition As Position)
        setToPositionToUncorrectedEquatCoords(pInitStateTemplate.ICoordXform.Position, fromPosition)
    End Sub

    Private Sub setInitStateTemplatePositionToCorrectedEquatCoords(ByRef fromPosition As Position)
        setToPositionToCorrectedEquatCoords(pInitStateTemplate.ICoordXform.Position, fromPosition)
    End Sub

    Private Function setToPositionToUncorrectedEquatCoords(ByRef toPosition As Position, ByRef fromPosition As Position) As Position
        Dim RaDec() As Double = pCorrectedRatesParmsFacade.GetUncorrectedEquat(fromPosition)
        toPosition.RA.Rad = RaDec(0)
        toPosition.Dec.Rad = RaDec(1)
        toPosition.SidT.Rad = fromPosition.SidT.Rad
        Return toPosition
    End Function

    Private Function setToPositionToCorrectedEquatCoords(ByRef toPosition As Position, ByRef fromPosition As Position) As Position
        Dim RaDec() As Double = pCorrectedRatesParmsFacade.GetCorrectedEquat(fromPosition)
        toPosition.RA.Rad = RaDec(0)
        toPosition.Dec.Rad = RaDec(1)
        toPosition.SidT.Rad = fromPosition.SidT.Rad
        Return toPosition
    End Function

    Private Function getPositionGetAltazFromEquatCoords(ByRef fromEquatPosition As Position, ByVal deltaSidTRad As Double) As Position
        pInitStateTemplate.ICoordXform.Position.CopyFrom(fromEquatPosition)
        pInitStateTemplate.ICoordXform.Position.SidT.Rad += deltaSidTRad
        pInitStateTemplate.ICoordXform.GetAltaz()
        Dim rtnPosition As Position = PositionArray.GetInstance.GetPosition
        rtnPosition.CopyFrom(pInitStateTemplate.ICoordXform.Position)
        Return rtnPosition
    End Function

    Private Function getUncorrectedPositionGetAltazFromEquatCoords(ByRef fromEquatPosition As Position, ByVal deltaSidTRad As Double) As Position
        Dim rtnPosition As Position = PositionArray.GetInstance.GetPosition
        rtnPosition.CopyFrom(fromEquatPosition)
        rtnPosition.SidT.Rad += deltaSidTRad
        setInitStateTemplatePositionToUncorrectedEquatCoords(rtnPosition)
        pInitStateTemplate.ICoordXform.GetAltaz()
        rtnPosition.Az.Rad = pInitStateTemplate.ICoordXform.Position.Az.Rad
        rtnPosition.Alt.Rad = pInitStateTemplate.ICoordXform.Position.Alt.Rad
        Return rtnPosition
    End Function

    Private Function getCorrectedPositionGetAltazFromEquatCoords(ByRef uncorrectedPosition As Position, ByVal deltaSidTRad As Double) As Position
        Dim rtnPosition As Position = PositionArray.GetInstance.GetPosition
        rtnPosition.CopyFrom(uncorrectedPosition)
        rtnPosition.SidT.Rad += deltaSidTRad
        setInitStateTemplatePositionToCorrectedEquatCoords(rtnPosition)
        pInitStateTemplate.ICoordXform.GetAltaz()
        rtnPosition.CopyFrom(pInitStateTemplate.ICoordXform.Position)
        Return rtnPosition
    End Function

    Private Function storePosition() As Position
        Dim position As Position = PositionArray.GetInstance.GetPosition
        position.CopyFrom(pInitStateTemplate.ICoordXform.Position)
        Return position
    End Function

    Private Sub restorePosition(ByRef position As Position)
        pInitStateTemplate.ICoordXform.Position.CopyFrom(position)
        position.Available = True
    End Sub

    Private Function correctedFRrateToString(ByVal methodName As String, ByVal FRrate2 As Double) As String
        Dim sb As New Text.StringBuilder
        sb.Append(methodName)
        sb.Append(": corrected rate (""/sec) ")
        sb.Append(pTrackRatesDataModel.GetTierAxisTrackRate.CorrectedRateRadPerSidSec * Units.RadToArcsec)
        sb.Append(", (2nd rate) ")
        sb.Append(FRrate2 * Units.RadToArcsec)
        sb.Append(", delta rates ")
        sb.Append(pTrackRatesDataModel.GetTierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec * Units.RadToArcsec)
        Return sb.ToString
    End Function
#End Region
End Class
