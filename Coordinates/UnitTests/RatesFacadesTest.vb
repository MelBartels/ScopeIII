Imports NUnit.Framework

<TestFixture()> Public Class RatesFacadesTest

    Private pCelestialCoordinateCalcs As CelestialCoordinateCalcs
    Private pCelestialErrorsCalculatorFacade As CelestialErrorsCalculatorFacade

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        pCelestialCoordinateCalcs = CelestialCoordinateCalcs.GetInstance
        pCelestialErrorsCalculatorFacade = CelestialErrorsCalculatorFacade.GetInstance
    End Sub

    <Test()> Public Sub TestInitRates()
        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.BuildAndInitRates()

        ' expected defaults from InitRates: IRates, CoordXformType, InitStateType and IInit
        Assert.AreSame(GetType(ScopeIII.Coordinates.FormulaRates), CObj(rfs.ScopeRatesFacade.Rates).GetType)
        Assert.AreEqual(CoordXformType.ConvertTrig, rfs.ScopeRatesFacade.ICoordXForm.CoordXformType)
        Assert.AreEqual(InitStateType.Equatorial, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitDoNothing), CObj(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit).GetType)

        Assert.AreSame(GetType(ScopeIII.Coordinates.TrigRates), CObj(rfs.CelestialRatesFacade.Rates).GetType)
        Assert.AreEqual(CoordXformType.ConvertTrig, rfs.CelestialRatesFacade.ICoordXForm.CoordXformType)
        Assert.AreEqual(InitStateType.Equatorial, rfs.CelestialRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitDoNothing), CObj(rfs.CelestialRatesFacade.Rates.InitStateTemplate.IInit).GetType)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestSetInits()
        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.BuildAndInitRates()

        ' FormulaRates and TrigRates use ConvertTrig, which results in IInit of InitDoNothing

        rfs.SetInits(CType(InitStateType.Altazimuth, ISFT))
        Assert.AreEqual(InitStateType.Altazimuth, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreEqual(InitStateType.Altazimuth, rfs.CelestialRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitDoNothing), CObj(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit).GetType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitDoNothing), CObj(rfs.CelestialRatesFacade.Rates.InitStateTemplate.IInit).GetType)

        rfs.SetInits(CType(InitStateType.Equatorial, ISFT))
        Assert.AreEqual(InitStateType.Equatorial, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreEqual(InitStateType.Equatorial, rfs.CelestialRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitDoNothing), CObj(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit).GetType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitDoNothing), CObj(rfs.CelestialRatesFacade.Rates.InitStateTemplate.IInit).GetType)

        rfs.SetInits(CType(InitStateType.None, ISFT))
        Assert.AreEqual(InitStateType.None, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreEqual(InitStateType.None, rfs.CelestialRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.IsNull(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit)
        Assert.IsNull(rfs.CelestialRatesFacade.Rates.InitStateTemplate.IInit)

        rfs.SetInits(CType(InitStateType.Celestial, ISFT))
        Assert.AreEqual(InitStateType.Celestial, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreEqual(InitStateType.Celestial, rfs.CelestialRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitDoNothing), CObj(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit).GetType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitDoNothing), CObj(rfs.CelestialRatesFacade.Rates.InitStateTemplate.IInit).GetType)

        ' MatrixRates, which results in IInit of InitConvertMatrix..
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))

        rfs.SetInits(CType(InitStateType.Altazimuth, ISFT))
        Assert.AreEqual(InitStateType.Altazimuth, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitConvertMatrixAltazimuth), CObj(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit).GetType)

        rfs.SetInits(CType(InitStateType.Equatorial, ISFT))
        Assert.AreEqual(InitStateType.Equatorial, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitConvertMatrixEquatorial), CObj(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit).GetType)

        rfs.SetInits(CType(InitStateType.None, ISFT))
        Assert.AreEqual(InitStateType.None, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.IsNull(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit)

        rfs.SetInits(CType(InitStateType.Celestial, ISFT))
        Assert.AreEqual(InitStateType.Celestial, rfs.ScopeRatesFacade.Rates.InitStateTemplate.InitStateType)
        Assert.AreSame(GetType(ScopeIII.Coordinates.InitConvertMatrixCelestial), CObj(rfs.ScopeRatesFacade.Rates.InitStateTemplate.IInit).GetType)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestLatitude()
        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.BuildAndInitRates()

        Dim latitudeRad As Double = 40 * Units.DegToRad
        Dim polarAligned As Boolean = False
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        Assert.AreEqual(rfs.ScopeRatesFacade.Site.Latitude.Rad, rfs.CelestialRatesFacade.Site.Latitude.Rad)

        polarAligned = True
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        Assert.AreNotEqual(rfs.ScopeRatesFacade.Site.Latitude.Rad, rfs.CelestialRatesFacade.Site.Latitude.Rad)
        Assert.IsTrue(rfs.ScopeRatesFacade.Site.Latitude.Rad > 0)

        ' southern hemisphere
        latitudeRad = -latitudeRad

        polarAligned = False
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        Assert.AreEqual(rfs.ScopeRatesFacade.Site.Latitude.Rad, rfs.CelestialRatesFacade.Site.Latitude.Rad)
        Assert.IsFalse(rfs.ScopeRatesFacade.Site.Latitude.Rad > 0)

        polarAligned = True
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        Assert.AreNotEqual(rfs.ScopeRatesFacade.Site.Latitude.Rad, rfs.CelestialRatesFacade.Site.Latitude.Rad)
        Assert.IsFalse(rfs.ScopeRatesFacade.Site.Latitude.Rad > 0)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestUpdateSidT()
        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.BuildAndInitRates()
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Equatorial, ISFT))
        rfs.UpdateLongitudeLatitude(0, 30 * Units.DegToRad, True)
        rfs.InitRates()

        rfs.CelestialRatesFacade.Position.SidT.Rad = 0
        rfs.ScopeRatesFacade.Position.SidT.Rad = 0

        Dim testSidTRad As Double = 5.1 * Units.HrToRad
        rfs.SetSidT(testSidTRad)

        ' does not update rates' positions
        Assert.AreEqual(0, rfs.CelestialRatesFacade.Position.SidT.Rad)
        Assert.AreEqual(0, rfs.ScopeRatesFacade.Position.SidT.Rad)

        ' does update after Get...
        rfs.CelestialRatesFacade.Position.SidT.Rad = 0
        rfs.ScopeRatesFacade.Position.SidT.Rad = 0
        rfs.InitAndGetAltaz(0, 0)
        Assert.AreEqual(testSidTRad, rfs.CelestialRatesFacade.Position.SidT.Rad)
        Assert.AreEqual(testSidTRad, rfs.ScopeRatesFacade.Position.SidT.Rad)

        rfs.CelestialRatesFacade.Position.SidT.Rad = 0
        rfs.ScopeRatesFacade.Position.SidT.Rad = 0
        rfs.InitAndGetEquat(0, 0)
        Assert.AreEqual(testSidTRad, rfs.CelestialRatesFacade.Position.SidT.Rad)
        Assert.AreEqual(testSidTRad, rfs.ScopeRatesFacade.Position.SidT.Rad)

        Assert.IsTrue(True)
    End Sub

    ' mirrors Coordinates.KingRateTest 
    <Test()> Public Sub TestKingRate()
        Dim testKingRate As Double = 14.995925439842333
        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.BuildAndInitRates()

        ' state: celestial = convertTrig, scope = convertMatrix, equat aligned
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Equatorial, ISFT))

        Dim polarAligned As Boolean = True

        ' northern hemisphere

        Dim latitudeRad As Double = 30 * Units.DegToRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        Dim RaRad As Double = 6 * Units.HrToRad
        Dim DecRad As Double = 5 * Units.DegToRad
        Dim sidTRad As Double = 7.5 * Units.HrToRad
        rfs.GetKingRateRadSec(RaRad, DecRad, sidTRad)
        Assert.AreEqual(testKingRate, rfs.KingRateArcsecSec)

        ' southern hemisphere

        latitudeRad = -latitudeRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()
        DecRad = -DecRad
        rfs.GetKingRateRadSec(RaRad, DecRad, sidTRad)
        Assert.AreEqual(-testKingRate, rfs.KingRateArcsecSec)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestMeridianFlip()
        Dim variance As Double = Units.ArcsecToRad

        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.BuildAndInitRates()

        ' state: celestial = convertTrig, scope = convertMatrix, equat aligned
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Equatorial, ISFT))

        Dim polarAligned As Boolean = True

        ' northern hemisphere

        Dim latitudeRad As Double = 40 * Units.DegToRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        ' equat to altaz

        Dim RaRad As Double = 0
        Dim DecRad As Double = -10 * Units.DegToRad
        Dim AzRad As Double = 180 * Units.DegToRad
        Dim celestialAltRad As Double = 40 * Units.DegToRad
        Dim scopeAltRad As Double = -10 * Units.DegToRad
        ' pole of 90deg - -10deg alt = 100deg from pole; flip across the pole gives alt of 90+100=190deg w/ az rotated 180deg
        Dim meridianFlipScopeAzRad As Double = 0
        Dim meridianFlipScopeAltRad As Double = 190 * Units.DegToRad

        rfs.MeridianFlipChanged(Nothing)
        rfs.GetAltaz(RaRad, DecRad)

        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, variance)
        ' celestial meridian at alt=50
        Assert.AreEqual(celestialAltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, variance)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, variance)
        Assert.AreEqual(scopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, variance)

        rfs.MeridianFlipChanged(CType(MeridianFlipState.PointingEast, ISFT))
        rfs.GetAltaz(RaRad, DecRad)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        ' Current (deg) Ra:   0.0000, Dec: -10.0000, Az:   0.0000, Alt: 190.0000, SidT:   0.0000
        Assert.AreEqual(meridianFlipScopeAzRad, rfs.ScopeRatesFacade.Position.Az.Rad, variance)
        Assert.AreEqual(meridianFlipScopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, variance)

        ' southern hemisphere

        latitudeRad = -latitudeRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        ' equat to altaz

        DecRad = -DecRad

        rfs.MeridianFlipChanged(Nothing)
        rfs.GetAltaz(RaRad, DecRad)

        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, variance)
        ' celestial meridian at alt=50
        Assert.AreEqual(celestialAltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, variance)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, variance)
        Assert.AreEqual(scopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, variance)

        rfs.MeridianFlipChanged(CType(MeridianFlipState.PointingEast, ISFT))
        rfs.GetAltaz(RaRad, DecRad)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        ' Current (deg) Ra:   0.0000, Dec: 10.0000, Az:   0.0000, Alt: 190.0000, SidT:   0.0000
        Assert.AreEqual(meridianFlipScopeAzRad, rfs.ScopeRatesFacade.Position.Az.Rad, variance)
        Assert.AreEqual(meridianFlipScopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, variance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestAltazimuthAlignedXForms()
        Dim variance As Double = Units.ArcsecToRad

        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.BuildAndInitRates()

        ' state: celestial = convertTrig, scope = convertMatrix, altaz aligned
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Altazimuth, ISFT))
        Dim polarAligned As Boolean = False

        Dim stepSizeRad As Double = 45 * Units.DegToRad
        For latitudeRad As Double = -Units.QtrRev To Units.QtrRev - stepSizeRad Step stepSizeRad
            rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
            For SidTRad As Double = 0 To Units.OneRev - stepSizeRad Step stepSizeRad
                rfs.SetSidT(SidTRad)
                For priRad As Double = 0 To Units.OneRev - stepSizeRad Step stepSizeRad
                    For secRad As Double = -Units.HalfRev To Units.HalfRev Step stepSizeRad
                        ' reinit for changing latitude
                        rfs.InitAndGetEquat(priRad, secRad)
                        compareScopeToCelestialPositions(variance, rfs, latitudeRad, SidTRad, priRad, secRad)
                        rfs.InitAndGetAltaz(priRad, secRad)
                        compareScopeToCelestialPositions(variance, rfs, latitudeRad, SidTRad, priRad, secRad)
                    Next
                Next
            Next
        Next

        Assert.IsTrue(True)
    End Sub

    Public Sub compareScopeToCelestialPositions(ByVal variance As Double, ByVal rfs As RatesFacades, ByVal latitudeRad As Double, ByVal SidTRad As Double, ByVal priRad As Double, ByVal secRad As Double)
        rfs.ScopeRatesFacade.Position.RA.Rad = eMath.ValidRad(rfs.ScopeRatesFacade.Position.RA.Rad)
        rfs.ScopeRatesFacade.Position.Az.Rad = eMath.ValidRad(rfs.ScopeRatesFacade.Position.Az.Rad)
        rfs.ScopeRatesFacade.Position.Alt.Rad = eMath.ValidRadPi(rfs.ScopeRatesFacade.Position.Alt.Rad)
        rfs.CelestialRatesFacade.Position.RA.Rad = eMath.ValidRad(rfs.CelestialRatesFacade.Position.RA.Rad)
        rfs.CelestialRatesFacade.Position.Az.Rad = eMath.ValidRad(rfs.CelestialRatesFacade.Position.Az.Rad)
        rfs.CelestialRatesFacade.Position.Alt.Rad = eMath.ValidRadPi(rfs.CelestialRatesFacade.Position.Alt.Rad)

        Assert.AreEqual(rfs.ScopeRatesFacade.Position.SidT.Rad, rfs.CelestialRatesFacade.Position.SidT.Rad, variance)

        If rfs.ScopeRatesFacade.Position.Az.Rad.Equals(rfs.CelestialRatesFacade.Position.Az.Rad) _
        AndAlso rfs.ScopeRatesFacade.Position.Alt.Rad.Equals(rfs.CelestialRatesFacade.Position.Alt.Rad) Then
            Assert.IsTrue(True)
        ElseIf Math.Abs(Math.Abs(rfs.ScopeRatesFacade.Position.Az.Rad) - Math.Abs(rfs.CelestialRatesFacade.Position.Az.Rad)) < variance _
        AndAlso Math.Abs(Math.Abs(rfs.ScopeRatesFacade.Position.Alt.Rad) - Math.Abs(rfs.CelestialRatesFacade.Position.Alt.Rad)) < variance Then
            Assert.IsTrue(True)
        Else
            Debug.WriteLine("positions do not agree: latitude " & latitudeRad * Units.RadToDeg & " SidT " & SidTRad * Units.RadToDeg & " az " & priRad * Units.RadToDeg & " alt " & secRad * Units.RadToDeg & ":")
            Debug.WriteLine("    ScopeRatesFacade " & rfs.ScopeRatesFacade.Position.ShowCoordDeg)
            Debug.WriteLine("    CelestialRatesFacade " & rfs.CelestialRatesFacade.Position.ShowCoordDeg)
            Debug.Write("testing altaz angular separation...")

            Dim angSepRad As Double = pCelestialCoordinateCalcs.CalcAltazAngularSep(rfs.ScopeRatesFacade.Position, rfs.CelestialRatesFacade.Position)
            Debug.Write(angSepRad * Units.RadToArcsec)
            Debug.Write(BartelsLibrary.Constants.Quote)
            If angSepRad < variance Then
                Debug.WriteLine(String.Empty)
                Assert.IsTrue(True)
            Else
                Debug.WriteLine("angular separation test failed")
                Assert.IsTrue(False)
            End If
        End If

        If rfs.ScopeRatesFacade.Position.RA.Rad.Equals(rfs.CelestialRatesFacade.Position.RA.Rad) _
        AndAlso rfs.ScopeRatesFacade.Position.Dec.Rad.Equals(rfs.CelestialRatesFacade.Position.Dec.Rad) Then
            Assert.IsTrue(True)
        ElseIf Math.Abs(Math.Abs(rfs.ScopeRatesFacade.Position.RA.Rad) - Math.Abs(rfs.CelestialRatesFacade.Position.RA.Rad)) < variance _
        AndAlso Math.Abs(Math.Abs(rfs.ScopeRatesFacade.Position.Dec.Rad) - Math.Abs(rfs.CelestialRatesFacade.Position.Dec.Rad)) < variance Then
            Assert.IsTrue(True)
        Else
            Debug.WriteLine("positions do not agree: latitude " & latitudeRad * Units.RadToDeg & " SidT " & SidTRad * Units.RadToDeg & " az " & priRad * Units.RadToDeg & " alt " & secRad * Units.RadToDeg & ":")
            Debug.WriteLine("    ScopeRatesFacade " & rfs.ScopeRatesFacade.Position.ShowCoordDeg)
            Debug.WriteLine("    CelestialRatesFacade " & rfs.CelestialRatesFacade.Position.ShowCoordDeg)
            Debug.Write("testing equat angular separation...")

            Dim angSepRad As Double = pCelestialCoordinateCalcs.CalcEquatAngularSepViaRa(rfs.ScopeRatesFacade.Position, rfs.CelestialRatesFacade.Position)
            Debug.Write(angSepRad * Units.RadToArcsec)
            Debug.Write(BartelsLibrary.Constants.Quote)
            If angSepRad < variance Then
                Debug.WriteLine(String.Empty)
                Assert.IsTrue(True)
            Else
                Debug.WriteLine("angular separation test failed")
                Assert.IsTrue(False)
            End If
        End If
    End Sub

    <Test()> Public Sub TestEquatorialAlignedXForms()
        Dim variance As Double = Units.ArcsecToRad

        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.BuildAndInitRates()

        ' state: celestial = convertTrig, scope = convertMatrix, equat aligned
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Equatorial, ISFT))

        Dim polarAligned As Boolean = True

        ' northern hemisphere

        Dim latitudeRad As Double = 40 * Units.DegToRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        ' equat to altaz

        Dim RaRad As Double = 0
        Dim DecRad As Double = -10 * Units.DegToRad
        Dim AzRad As Double = 180 * Units.DegToRad
        Dim celestialAltRad As Double = 40 * Units.DegToRad
        Dim scopeAltRad As Double = -10 * Units.DegToRad

        rfs.GetAltaz(RaRad, DecRad)

        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, variance)
        ' celestial meridian at alt=50
        Assert.AreEqual(celestialAltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, variance)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, variance)
        Assert.AreEqual(scopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, variance)

        ' altaz to equat

        rfs.GetEquat(AzRad, celestialAltRad)

        Assert.AreEqual(RaRad, rfs.CelestialRatesFacade.Position.RA.Rad, variance)
        Assert.AreEqual(DecRad, rfs.CelestialRatesFacade.Position.Dec.Rad, variance)
        Assert.AreEqual(RaRad, rfs.ScopeRatesFacade.Position.RA.Rad, variance)
        Assert.AreEqual(DecRad, rfs.ScopeRatesFacade.Position.Dec.Rad, variance)

        ' southern hemisphere

        latitudeRad = -latitudeRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        ' equat to altaz

        DecRad = -DecRad

        rfs.GetAltaz(RaRad, DecRad)

        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, variance)
        ' celestial meridian at alt=50
        Assert.AreEqual(celestialAltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, variance)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, variance)
        Assert.AreEqual(scopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, variance)

        ' altaz to equat

        rfs.GetEquat(AzRad, celestialAltRad)

        Assert.AreEqual(RaRad, rfs.CelestialRatesFacade.Position.RA.Rad, variance)
        Assert.AreEqual(DecRad, rfs.CelestialRatesFacade.Position.Dec.Rad, variance)
        Assert.AreEqual(RaRad, rfs.ScopeRatesFacade.Position.RA.Rad, variance)
        Assert.AreEqual(DecRad, rfs.ScopeRatesFacade.Position.Dec.Rad, variance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestRatesFacadesWithCelestialErrors()
        TestCelestialErrorsAltazAligned()
        TestCelestialErrorsPolarAligned()
        TestCelestialErrorsScopeAltazToSiteAltaz()

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestUncorrectedPosition()
        Dim varianceRad As Double = Units.ArcsecToRad

        pCelestialErrorsCalculatorFacade.UseCalculator = True
        pCelestialErrorsCalculatorFacade.IncludeNutationAnnualAberration = False
        pCelestialErrorsCalculatorFacade.IncludePrecession = False
        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.RegisterReferences(Coordinate.GetInstance, Coordinate.GetInstance, pCelestialErrorsCalculatorFacade)
        rfs.BuildAndInitRates()

        ' state: celestial = convertTrig, scope = convertMatrix, altaz aligned
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Altazimuth, ISFT))

        Dim polarAligned As Boolean = False

        Dim latitudeRad As Double = 40 * Units.DegToRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        Dim uncorrectedRaRad As Double = 12.739 * Units.DegToRad
        Dim correctedRaRad As Double = 12.7582 * Units.DegToRad
        Dim uncorrectedDecRad As Double = -39.1481 * Units.DegToRad
        Dim correctedDecRad As Double = -39.2334 * Units.DegToRad
        Dim AzRad As Double = 170 * Units.DegToRad
        Dim AltRad As Double = 10 * Units.DegToRad

        ' corrected "Current (deg) Ra:  12.7582, Dec: -39.2334, Az: 170.0000, Alt:  10.0000, SidT:   0.0000"
        ' uncorrected "Current (deg) Ra:  12.7390, Dec: -39.1480, Az: 170.0000, Alt:  10.0000, SidT:   0.0000"

        ' GetEquat
        rfs.GetEquat(AzRad, AltRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.UncorrectedPosition.ShowCoordDeg)
        Assert.AreEqual(correctedRaRad, rfs.CelestialRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(correctedDecRad, rfs.CelestialRatesFacade.Position.Dec.Rad, varianceRad)
        Assert.AreEqual(correctedRaRad, rfs.ScopeRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(correctedDecRad, rfs.ScopeRatesFacade.Position.Dec.Rad, varianceRad)
        ' test uncorrected
        Assert.AreEqual(uncorrectedRaRad, rfs.UncorrectedPosition.RA.Rad, varianceRad)
        Assert.AreEqual(uncorrectedDecRad, rfs.UncorrectedPosition.Dec.Rad, varianceRad)

        ' GetEquatFromScopeGauges
        rfs.GetEquatFromScopeGauges(AzRad, AltRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.UncorrectedPosition.ShowCoordDeg)
        ' rfs.CelestialRatesFacade is uncorrected
        Assert.AreEqual(uncorrectedRaRad, rfs.CelestialRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(uncorrectedDecRad, rfs.CelestialRatesFacade.Position.Dec.Rad, varianceRad)
        Assert.AreEqual(correctedRaRad, rfs.ScopeRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(correctedDecRad, rfs.ScopeRatesFacade.Position.Dec.Rad, varianceRad)
        ' test uncorrected
        Assert.AreEqual(uncorrectedRaRad, rfs.UncorrectedPosition.RA.Rad, varianceRad)
        Assert.AreEqual(uncorrectedDecRad, rfs.UncorrectedPosition.Dec.Rad, varianceRad)

        ' GetAltaz
        rfs.GetAltaz(correctedRaRad, correctedDecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.UncorrectedPosition.ShowCoordDeg)
        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)
        ' test uncorrected
        Assert.AreEqual(uncorrectedRaRad, rfs.UncorrectedPosition.RA.Rad, varianceRad)
        Assert.AreEqual(uncorrectedDecRad, rfs.UncorrectedPosition.Dec.Rad, varianceRad)

        Assert.IsTrue(True)
    End Sub

    Public Sub TestCelestialErrorsAltazAligned()
        Dim varianceRad As Double = Units.ArcsecToRad
        Dim trackingVarianceArcsec As Double = 0.004

        pCelestialErrorsCalculatorFacade.UseCalculator = True
        pCelestialErrorsCalculatorFacade.IncludeNutationAnnualAberration = False
        pCelestialErrorsCalculatorFacade.IncludePrecession = False

        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.RegisterReferences(Coordinate.GetInstance, Coordinate.GetInstance, pCelestialErrorsCalculatorFacade)
        rfs.BuildAndInitRates()

        ' state: celestial = convertTrig, scope = convertMatrix, altaz aligned
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Altazimuth, ISFT))

        Dim polarAligned As Boolean = False

        ' northern hemisphere

        Dim latitudeRad As Double = 40 * Units.DegToRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        Dim uncorrectedRaRad As Double = 12.739 * Units.DegToRad
        Dim correctedRaRad As Double = 12.7582 * Units.DegToRad
        Dim uncorrectedDecRad As Double = -39.1481 * Units.DegToRad
        Dim correctedDecRad As Double = -39.2334 * Units.DegToRad
        Dim AzRad As Double = 170 * Units.DegToRad
        Dim AltRad As Double = 10 * Units.DegToRad

        ' GetEquat

        pCelestialErrorsCalculatorFacade.IncludeRefraction = False

        rfs.GetEquat(AzRad, AltRad)
        ' uncorrected "Current (deg) Ra:  12.7390, Dec: -39.1481, Az: 170.0000, Alt:  10.0000, SidT:   0.0000"
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(uncorrectedRaRad, rfs.CelestialRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(uncorrectedDecRad, rfs.CelestialRatesFacade.Position.Dec.Rad, varianceRad)
        Assert.AreEqual(uncorrectedRaRad, rfs.ScopeRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(uncorrectedDecRad, rfs.ScopeRatesFacade.Position.Dec.Rad, varianceRad)
        ' pri axis rate
        Assert.AreEqual(11.637213822720986, rfs.ScopeRatesFacade.PriAxisTrackRate.RateRadPerSidSec * Units.RadToArcsec)

        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        rfs.GetEquat(AzRad, AltRad)
        ' corrected "Current (deg) Ra:  12.7582, Dec: -39.2334, Az: 170.0000, Alt:  10.0000, SidT:   0.0000"
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(correctedRaRad, rfs.CelestialRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(correctedDecRad, rfs.CelestialRatesFacade.Position.Dec.Rad, varianceRad)
        Assert.AreEqual(correctedRaRad, rfs.ScopeRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(correctedDecRad, rfs.ScopeRatesFacade.Position.Dec.Rad, varianceRad)
        ' pri axis rate
        Assert.AreEqual(11.619569255742132, rfs.ScopeRatesFacade.PriAxisTrackRate.CorrectedRateRadPerSidSec * Units.RadToArcsec)

        ' GetAltaz

        pCelestialErrorsCalculatorFacade.IncludeRefraction = False

        rfs.GetAltaz(uncorrectedRaRad, uncorrectedDecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)

        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        rfs.GetAltaz(correctedRaRad, correctedDecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)

        ' southern hemisphere

        latitudeRad = -latitudeRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        uncorrectedDecRad = -uncorrectedDecRad
        correctedDecRad = -correctedDecRad
        ' reflect az across 180 deg
        AzRad = Units.OneRev - AzRad

        ' GetEquat

        pCelestialErrorsCalculatorFacade.IncludeRefraction = False

        rfs.GetEquat(AzRad, AltRad)
        ' uncorrected "Current (deg) Ra:  12.7390, Dec: -39.1481, Az: 170.0000, Alt:  10.0000, SidT:   0.0000"
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(uncorrectedRaRad, rfs.CelestialRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(uncorrectedDecRad, rfs.CelestialRatesFacade.Position.Dec.Rad, varianceRad)
        Assert.AreEqual(uncorrectedRaRad, rfs.ScopeRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(uncorrectedDecRad, rfs.ScopeRatesFacade.Position.Dec.Rad, varianceRad)
        ' pri axis rate
        Assert.AreEqual(-11.637213822720986, rfs.ScopeRatesFacade.PriAxisTrackRate.RateRadPerSidSec * Units.RadToArcsec)

        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        rfs.GetEquat(AzRad, AltRad)
        ' corrected "Current (deg) Ra:  12.7582, Dec: -39.2334, Az: 170.0000, Alt:  10.0000, SidT:   0.0000"
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(correctedRaRad, rfs.CelestialRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(correctedDecRad, rfs.CelestialRatesFacade.Position.Dec.Rad, varianceRad)
        Assert.AreEqual(correctedRaRad, rfs.ScopeRatesFacade.Position.RA.Rad, varianceRad)
        Assert.AreEqual(correctedDecRad, rfs.ScopeRatesFacade.Position.Dec.Rad, varianceRad)
        ' pri axis rate
        Assert.AreEqual(-11.619569255742132, rfs.ScopeRatesFacade.PriAxisTrackRate.CorrectedRateRadPerSidSec * Units.RadToArcsec)

        ' GetAltaz

        pCelestialErrorsCalculatorFacade.IncludeRefraction = False

        rfs.GetAltaz(uncorrectedRaRad, uncorrectedDecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)

        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        rfs.GetAltaz(correctedRaRad, correctedDecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(AzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(AzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(AltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)

        Assert.IsTrue(True)
    End Sub

    Public Sub TestCelestialErrorsPolarAligned()
        Dim varianceRad As Double = Units.ArcsecToRad

        pCelestialErrorsCalculatorFacade.UseCalculator = True
        pCelestialErrorsCalculatorFacade.IncludeNutationAnnualAberration = False
        pCelestialErrorsCalculatorFacade.IncludePrecession = False

        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.RegisterReferences(Coordinate.GetInstance, Coordinate.GetInstance, pCelestialErrorsCalculatorFacade)
        rfs.BuildAndInitRates()

        ' state: celestial = convertTrig, scope = convertMatrix, altaz aligned
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Altazimuth, ISFT))

        Dim polarAligned As Boolean = True

        ' northern hemisphere

        Dim latitudeRad As Double = 40 * Units.DegToRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        Dim RaRad As Double = 15 * Units.DegToRad
        Dim DecRad As Double = -30 * Units.DegToRad
        Dim CelestialAzRad As Double = 166.3179 * Units.DegToRad
        Dim CelestialAltRad As Double = 18.6275 * Units.DegToRad
        Dim correctedCelestialAltRad As Double = 18.6749 * Units.DegToRad
        Dim ScopeAzRad As Double = 165.0 * Units.DegToRad
        Dim ScopeAltRad As Double = -30 * Units.DegToRad
        Dim correctedScopeAltRad As Double = -29.2875 * Units.DegToRad

        ' GetAltaz

        pCelestialErrorsCalculatorFacade.IncludeRefraction = False

        rfs.GetAltaz(RaRad, DecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(CelestialAzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(CelestialAltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(ScopeAzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(ScopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)

        ' only altitude is affected because refraction operates in that axis only
        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        rfs.GetAltaz(RaRad, DecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(CelestialAzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(correctedCelestialAltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(ScopeAzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(correctedScopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)

        ' southern hemisphere

        latitudeRad = -latitudeRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        DecRad = -DecRad
        ' reflect az across 180 deg
        CelestialAzRad = Units.OneRev - CelestialAzRad
        ScopeAzRad = Units.OneRev - ScopeAzRad

        ' GetAltaz

        pCelestialErrorsCalculatorFacade.IncludeRefraction = False

        rfs.GetAltaz(RaRad, DecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(CelestialAzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(CelestialAltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(ScopeAzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(ScopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)

        ' only altitude is affected because refraction operates in that axis only
        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        rfs.GetAltaz(RaRad, DecRad)
        Debug.WriteLine(rfs.CelestialRatesFacade.Position.ShowCoordDeg)
        Debug.WriteLine(rfs.ScopeRatesFacade.Position.ShowCoordDeg)
        Assert.AreEqual(CelestialAzRad, rfs.CelestialRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(correctedCelestialAltRad, rfs.CelestialRatesFacade.Position.Alt.Rad, varianceRad)
        Assert.AreEqual(ScopeAzRad, rfs.ScopeRatesFacade.Position.Az.Rad, varianceRad)
        Assert.AreEqual(correctedScopeAltRad, rfs.ScopeRatesFacade.Position.Alt.Rad, varianceRad)

        Assert.IsTrue(True)
    End Sub

    Public Sub TestCelestialErrorsScopeAltazToSiteAltaz()
        Dim varianceRad As Double = Units.ArcsecToRad

        pCelestialErrorsCalculatorFacade.UseCalculator = True
        pCelestialErrorsCalculatorFacade.IncludeNutationAnnualAberration = False
        pCelestialErrorsCalculatorFacade.IncludePrecession = False

        Dim rfs As RatesFacades = RatesFacades.GetInstance
        rfs.RegisterReferences(Coordinate.GetInstance, Coordinate.GetInstance, pCelestialErrorsCalculatorFacade)
        rfs.BuildAndInitRates()

        ' state: celestial = convertTrig, scope = convertMatrix, altaz aligned
        rfs.BuildScopeRatesFacade(CType(Rates.MatrixRates, ISFT))
        rfs.SetInits(CType(InitStateType.Altazimuth, ISFT))

        Dim polarAligned As Boolean = True

        ' northern hemisphere

        Dim latitudeRad As Double = 40 * Units.DegToRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        Dim RaRad As Double = 15 * Units.DegToRad
        Dim DecRad As Double = -30 * Units.DegToRad
        Dim CelestialAzRad As Double = 166.3179 * Units.DegToRad
        Dim CelestialAltRad As Double = 18.6275 * Units.DegToRad
        Dim correctedCelestialAltRad As Double = 18.6749 * Units.DegToRad
        Dim ScopeAzRad As Double = 165.0 * Units.DegToRad
        Dim ScopeAltRad As Double = -30 * Units.DegToRad
        Dim correctedScopeAltRad As Double = -29.2875 * Units.DegToRad

        ' GetAltaz

        pCelestialErrorsCalculatorFacade.IncludeRefraction = False

        Dim az As AZdouble = CType(rfs.ConvertScopeAltazToSiteAltazDelegate.DynamicInvoke(New Object() {ScopeAzRad, ScopeAltRad}), AZdouble)
        Assert.AreEqual(CelestialAzRad, az.Z, varianceRad)
        Assert.AreEqual(CelestialAltRad, az.A, varianceRad)

        ' only altitude is affected because refraction operates in that axis only
        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        az = CType(rfs.ConvertScopeAltazToSiteAltazDelegate.DynamicInvoke(New Object() {ScopeAzRad, ScopeAltRad}), AZdouble)
        Assert.AreEqual(CelestialAzRad, az.Z, varianceRad)
        Assert.AreEqual(correctedCelestialAltRad, az.A, varianceRad)

        ' southern hemisphere

        latitudeRad = -latitudeRad
        rfs.UpdateLongitudeLatitude(0, latitudeRad, polarAligned)
        rfs.InitRates()

        DecRad = -DecRad
        ' reflect az across 180 deg
        CelestialAzRad = Units.OneRev - CelestialAzRad
        ScopeAzRad = Units.OneRev - ScopeAzRad

        ' GetAltaz

        pCelestialErrorsCalculatorFacade.IncludeRefraction = False

        az = CType(rfs.ConvertScopeAltazToSiteAltazDelegate.DynamicInvoke(New Object() {ScopeAzRad, ScopeAltRad}), AZdouble)
        Assert.AreEqual(CelestialAzRad, az.Z, varianceRad)
        Assert.AreEqual(CelestialAltRad, az.A, varianceRad)

        pCelestialErrorsCalculatorFacade.IncludeRefraction = True

        az = CType(rfs.ConvertScopeAltazToSiteAltazDelegate.DynamicInvoke(New Object() {ScopeAzRad, ScopeAltRad}), AZdouble)
        Assert.AreEqual(CelestialAzRad, az.Z, varianceRad)
        Assert.AreEqual(correctedCelestialAltRad, az.A, varianceRad)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
        pCelestialErrorsCalculatorFacade = Nothing
    End Sub
End Class
