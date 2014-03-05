Imports NUnit.Framework

<TestFixture()> Public Class RatesFacadeTest

    Private variance As Double = Units.ArcsecToRad / 1000

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestICoordXformType()
        Dim rf As RatesFacade = RatesFacade.GetInstance

        rf.Build(CType(Rates.TrigRates, ISFT))
        rf.SetInit(CType(InitStateType.Equatorial, ISFT))

        Assert.IsNotNull(rf.Rates.InitStateTemplate.ICoordXform)

        ' should not be able to convert to ConvertMatrix if type was built as TrigRates
        Assert.IsNull(rf.ConvertMatrix)
    End Sub

    <Test()> Public Sub TestRates()
        Dim rf As RatesFacade = RatesFacade.GetInstance
        rf.Build(CType(Rates.MatrixRates, ISFT))
        rf.SetInit(CType(InitStateType.Altazimuth, ISFT))

        rf.Site.Latitude.Rad = 40 * Units.DegToRad

        rf.Init()

        ' on celestial meridian facing south
        rf.Position.Alt.Rad = 30 * Units.DegToRad
        rf.Position.Az.Rad = 180 * Units.DegToRad

        ' sid tracking rate (15"/sec) must be adjusted for distance from equat polar plane and for distance from scope pri plane;
        ' GetEquat() necessary to get the Dec value
        rf.GetEquat()
        Dim priRateRadPerSidSec As Double = 15 / Math.Cos(rf.Position.Alt.Rad) * Math.Cos(rf.Position.Dec.Rad) * Units.ArcsecToRad

        rf.CalcRates()

        Assert.AreEqual(priRateRadPerSidSec, rf.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(0.0, rf.SecAxisTrackRate.RateRadPerSidSec, variance)
    End Sub

    ' taki's test ex
    <Test()> Public Sub TestConvertMatrix()
        Dim varianceDeg As Double = 0.01

        Dim rf As RatesFacade = RatesFacade.GetInstance
        rf.Build(CType(Rates.MatrixRates, ISFT))
        rf.SetInit(CType(InitStateType.Celestial, ISFT))

        Dim fabErrors As FabErrors = Coordinates.FabErrors.GetInstance
        fabErrors.SetFabErrorsDeg(-0.04, 0.4, -1.63)

        Dim one As Position = PositionArray.GetInstance.GetPosition
        one.SetCoordDeg(79.172, 45.998, 360 - 39.9, 39.9, 39.2 * Units.SidRate / 4)

        Dim two As Position = PositionArray.GetInstance.GetPosition
        two.SetCoordDeg(37.96, 89.264, 360 - 94.6, 36.2, 40.3 * Units.SidRate / 4)

        rf.CopyFromOneTwoThreeFabErrors(one, two, Nothing, fabErrors)

        rf.Init()

        rf.Position.SetCoordDeg(326.05, 9.88, 360 - 0, 0, 47 * Units.SidRate / 4)
        rf.GetAltaz()

        Assert.AreEqual(360 - 202.54, rf.Position.Az.Rad * Units.RadToDeg, varianceDeg)
        Assert.AreEqual(42.16, rf.Position.Alt.Rad * Units.RadToDeg, varianceDeg)
    End Sub

    <Test()> Public Sub TestCorrectedRates()
        Dim variance As Double = Units.ArcsecToRad / 1000
        Dim rf As RatesFacade = RatesFacade.GetInstance
        rf.Build(CType(Rates.MatrixRates, ISFT))
        ' for polar align
        rf.Site.Latitude.Rad = 90 * Units.DegToRad
        rf.SetInit(CType(InitStateType.Equatorial, ISFT))
        rf.Init()
        ' local site latitude
        Dim siteLatitudeRad As Double = 45 * Units.DegToRad

        rf.Position.SetCoordDeg(0, 0, 120, 30, 0)
        rf.CalcRates()
        Assert.AreEqual(15 * Units.ArcsecToRad, rf.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(0, rf.SecAxisTrackRate.RateRadPerSidSec, variance)

        Dim celestialErrorsCalculator As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim correctedEquatPosition As Position = Position.GetInstance
        Dim toPosition As Position = Position.GetInstance
        correctedEquatPosition.SetCoordDeg(48.7111, 2.7016, 0, 0, 0)
        rf.CalcCorrectedRates(celestialErrorsCalculator, correctedEquatPosition, toPosition, Now, Now, False, False, True, siteLatitudeRad)
        ' tracking rate slows slightly towards horizon
        Assert.AreEqual(14.9892 * Units.ArcsecToRad, rf.PriAxisTrackRate.CorrectedRateRadPerSidSec, variance)
        ' FR should be 0 for polar align
        Assert.AreEqual(0, rf.TierAxisTrackRate.CorrectedRateRadPerSidSec, variance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestCompareRates()
        Dim variance As Double = Units.ArcsecToRad / 1000

        Dim formulaRates As RatesFacade = buildAndCalcRatesForCompareRates(CType(Rates.FormulaRates, ISFT))

        Assert.AreNotEqual(0, formulaRates.Rates.PriAxisTrackRate.RateRadPerSidSec)
        Assert.AreNotEqual(0, formulaRates.Rates.SecAxisTrackRate.RateRadPerSidSec)
        Assert.AreNotEqual(0, formulaRates.Rates.TierAxisTrackRate.RateRadPerSidSec)
        Assert.AreNotEqual(0, formulaRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec)
        Assert.AreNotEqual(0, formulaRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec)
        Assert.AreNotEqual(0, formulaRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec)

        Dim trigRates As RatesFacade = buildAndCalcRatesForCompareRates(CType(Rates.TrigRates, ISFT))

        Assert.AreEqual(formulaRates.Rates.PriAxisTrackRate.RateRadPerSidSec, trigRates.Rates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(formulaRates.Rates.SecAxisTrackRate.RateRadPerSidSec, trigRates.Rates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(formulaRates.Rates.TierAxisTrackRate.RateRadPerSidSec, trigRates.Rates.TierAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(formulaRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, trigRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Assert.AreEqual(formulaRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, trigRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Assert.AreEqual(formulaRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, trigRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)

        Dim matrixRates As RatesFacade = buildAndCalcRatesForCompareRates(CType(Rates.MatrixRates, ISFT))

        Assert.AreEqual(trigRates.Rates.PriAxisTrackRate.RateRadPerSidSec, matrixRates.Rates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(trigRates.Rates.SecAxisTrackRate.RateRadPerSidSec, matrixRates.Rates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(trigRates.Rates.TierAxisTrackRate.RateRadPerSidSec, matrixRates.Rates.TierAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(trigRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, matrixRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Assert.AreEqual(trigRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, matrixRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Assert.AreEqual(trigRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, matrixRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)

        Assert.IsTrue(True)
    End Sub

    Private Function buildAndCalcRatesForCompareRates(ByRef rates As ISFT) As RatesFacade
        Dim rf As RatesFacade = RatesFacade.GetInstance
        rf.Build(rates)
        ' for altaz align
        rf.Site.Latitude.Rad = 40 * Units.DegToRad
        rf.SetInit(CType(InitStateType.Altazimuth, ISFT))
        rf.Init()
        rf.Position.SetCoordDeg(0, 0, 120, 30, 0)
        rf.CalcRates()
        Return rf
    End Function

    <Test()> Public Sub TestCompareCorrectedRates()
        Dim variance As Double = Units.ArcsecToRad / 1000
        Dim FRvariance As Double = Units.ArcsecToRad / 1000

        Dim formulaRates As RatesFacade = buildAndCalcCorrectedRatesForCompareCorrectedRates(CType(Rates.FormulaRates, ISFT))
        Dim trigRates As RatesFacade = buildAndCalcCorrectedRatesForCompareCorrectedRates(CType(Rates.TrigRates, ISFT))
        Dim matrixRates As RatesFacade = buildAndCalcCorrectedRatesForCompareCorrectedRates(CType(Rates.MatrixRates, ISFT))

        TestCompareCorrectedRatesAsserts(variance, FRvariance, formulaRates, trigRates, matrixRates)

        formulaRates = buildAndCalcCorrectedRatesForCompareCorrectedRates2(CType(Rates.FormulaRates, ISFT))
        trigRates = buildAndCalcCorrectedRatesForCompareCorrectedRates2(CType(Rates.TrigRates, ISFT))
        matrixRates = buildAndCalcCorrectedRatesForCompareCorrectedRates2(CType(Rates.MatrixRates, ISFT))

        TestCompareCorrectedRatesAsserts(variance, FRvariance, formulaRates, trigRates, matrixRates)

        formulaRates = buildAndCalcCorrectedRatesForCompareCorrectedRates3(CType(Rates.FormulaRates, ISFT))
        trigRates = buildAndCalcCorrectedRatesForCompareCorrectedRates3(CType(Rates.TrigRates, ISFT))
        matrixRates = buildAndCalcCorrectedRatesForCompareCorrectedRates3(CType(Rates.MatrixRates, ISFT))

        ' sec axis varies .02"/sec
        variance = Units.ArcsecToRad / 10
        TestCompareCorrectedRatesAsserts(variance, FRvariance, formulaRates, trigRates, matrixRates)
    End Sub

    Private Sub TestCompareCorrectedRatesAsserts(ByVal variance As Double, ByVal FRvariance As Double, ByVal formulaRates As RatesFacade, ByVal trigRates As RatesFacade, ByVal matrixRates As RatesFacade)
        Assert.AreNotEqual(0, FormulaRates.Rates.PriAxisTrackRate.CorrectedRateRadPerSidSec)
        Assert.AreNotEqual(0, FormulaRates.Rates.SecAxisTrackRate.CorrectedRateRadPerSidSec)
        Assert.AreNotEqual(0, FormulaRates.Rates.TierAxisTrackRate.CorrectedRateRadPerSidSec)
        Assert.AreNotEqual(0, FormulaRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec)
        Assert.AreNotEqual(0, FormulaRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec)
        Assert.AreNotEqual(0, FormulaRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec)

        ' formula and trig/matrix rates will differ because formula rates are not aware of celestial errors like refraction;
        ' delta rates will be equal though
        Assert.AreNotEqual(FormulaRates.Rates.PriAxisTrackRate.CorrectedRateRadPerSidSec, TrigRates.Rates.PriAxisTrackRate.CorrectedRateRadPerSidSec)
        Assert.AreNotEqual(FormulaRates.Rates.SecAxisTrackRate.CorrectedRateRadPerSidSec, TrigRates.Rates.SecAxisTrackRate.CorrectedRateRadPerSidSec)
        Assert.AreNotEqual(FormulaRates.Rates.TierAxisTrackRate.CorrectedRateRadPerSidSec, TrigRates.Rates.TierAxisTrackRate.CorrectedRateRadPerSidSec)
        Assert.AreEqual(FormulaRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, TrigRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Assert.AreEqual(FormulaRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, TrigRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Debug.WriteLine("TestCompareCorrectedRates #1 FR delta difference " & (formulaRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec - trigRates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec) * Units.RadToArcsec & BartelsLibrary.Constants.Quote)
        Assert.AreEqual(FormulaRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, TrigRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, FRvariance)

        Assert.AreEqual(TrigRates.Rates.PriAxisTrackRate.CorrectedRateRadPerSidSec, MatrixRates.Rates.PriAxisTrackRate.CorrectedRateRadPerSidSec, variance)
        Assert.AreEqual(TrigRates.Rates.SecAxisTrackRate.CorrectedRateRadPerSidSec, MatrixRates.Rates.SecAxisTrackRate.CorrectedRateRadPerSidSec, variance)
        Assert.AreEqual(TrigRates.Rates.TierAxisTrackRate.CorrectedRateRadPerSidSec, MatrixRates.Rates.TierAxisTrackRate.CorrectedRateRadPerSidSec, variance)
        Assert.AreEqual(TrigRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, MatrixRates.Rates.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Assert.AreEqual(TrigRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, MatrixRates.Rates.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Debug.WriteLine("TestCompareCorrectedRates #2 FR delta difference " & (trigRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec - matrixRates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec) * Units.RadToArcsec & BartelsLibrary.Constants.Quote)
        Assert.AreEqual(TrigRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, MatrixRates.Rates.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, FRvariance)
    End Sub

    Private Function buildAndCalcCorrectedRatesForCompareCorrectedRates(ByRef rates As ISFT) As RatesFacade
        Dim celestialErrorsCalculator As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim correctedEquatPosition As Position = Position.GetInstance
        Dim toPosition As Position = Position.GetInstance

        Dim rf As RatesFacade = RatesFacade.GetInstance
        rf.Build(rates)
        ' for altaz align
        rf.Site.Latitude.Rad = 40 * Units.DegToRad
        rf.SetInit(CType(InitStateType.Altazimuth, ISFT))
        rf.Init()
        correctedEquatPosition.SetCoordDeg(120, 30, 0, 0, 0)
        rf.CalcCorrectedRates(celestialErrorsCalculator, correctedEquatPosition, toPosition, Now, Now, False, False, True, rf.Site.Latitude.Rad)
        Return rf
    End Function

    Private Function buildAndCalcCorrectedRatesForCompareCorrectedRates2(ByRef rates As ISFT) As RatesFacade
        Dim celestialErrorsCalculator As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim correctedEquatPosition As Position = Position.GetInstance
        Dim toPosition As Position = Position.GetInstance

        Dim rf As RatesFacade = RatesFacade.GetInstance
        rf.Build(rates)
        ' for altaz align
        rf.Site.Latitude.Rad = 40 * Units.DegToRad
        rf.SetInit(CType(InitStateType.Altazimuth, ISFT))
        rf.Init()
        correctedEquatPosition.SetCoordDeg(15, -30, 0, 0, 0)
        rf.CalcCorrectedRates(celestialErrorsCalculator, correctedEquatPosition, toPosition, Now, Now, False, False, True, rf.Site.Latitude.Rad)
        Return rf
    End Function

    Private Function buildAndCalcCorrectedRatesForCompareCorrectedRates3(ByRef rates As ISFT) As RatesFacade
        Dim celestialErrorsCalculator As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim correctedEquatPosition As Position = Position.GetInstance
        Dim toPosition As Position = Position.GetInstance

        Dim rf As RatesFacade = RatesFacade.GetInstance
        rf.Build(rates)
        ' for altaz align
        rf.Site.Latitude.Rad = 40 * Units.DegToRad
        rf.SetInit(CType(InitStateType.Altazimuth, ISFT))
        rf.Init()
        correctedEquatPosition.SetCoordDeg(0, 30, 0, 0, 0)
        rf.CalcCorrectedRates(celestialErrorsCalculator, correctedEquatPosition, toPosition, Now, Now, False, False, True, rf.Site.Latitude.Rad)
        Return rf
    End Function

    <Test()> Public Sub TestCompareCalcDeltaToCalcCorrectedDelta()
        testCompareCalcDeltaToCalcCorrectedDeltaSubr(CType(Rates.FormulaRates, ISFT))
        testCompareCalcDeltaToCalcCorrectedDeltaSubr(CType(Rates.TrigRates, ISFT))
        testCompareCalcDeltaToCalcCorrectedDeltaSubr(CType(Rates.MatrixRates, ISFT))
    End Sub

    Private Sub testCompareCalcDeltaToCalcCorrectedDeltaSubr(ByRef rates As ISFT)
        Dim variance As Double = Units.ArcsecToRad / 10
        Dim FRvariance As Double = Units.ArcsecToRad
        Dim testPosition As Position = Position.GetInstance
        testPosition.SetCoordDeg(0, 0, 120, 30, 0)

        Dim rf As RatesFacade = RatesFacade.GetInstance
        rf.Build(rates)
        rf.Site.Latitude.Rad = 40 * Units.DegToRad
        rf.SetInit(CType(InitStateType.Altazimuth, ISFT))
        rf.Init()
        rf.Position.CopyFrom(testPosition)
        rf.CalcRates()
        Dim rfDeltas(2) As Double
        rfDeltas(0) = rf.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec
        rfDeltas(1) = rf.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec
        rfDeltas(2) = rf.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec

        Dim celestialErrorsCalculator As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim correctedEquatPosition As Position = Position.GetInstance
        Dim toPosition As Position = Position.GetInstance
        ' local site latitude
        Dim siteLatitudeRad As Double = rf.Site.Latitude.Rad
        rf.Position.CopyFrom(testPosition)
        rf.GetEquat()
        correctedEquatPosition.CopyFrom(rf.Position)
        rf.CalcCorrectedRates(celestialErrorsCalculator, correctedEquatPosition, toPosition, Now, Now, False, False, True, siteLatitudeRad)
        Assert.AreEqual(rfDeltas(0), rf.PriAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Assert.AreEqual(rfDeltas(1), rf.SecAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, variance)
        Debug.WriteLine("testCompareCalcDeltaToCalcCorrectedDeltaSubr FR delta difference " & (rfDeltas(2) - rf.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec) * Units.RadToArcsec & BartelsLibrary.Constants.Quote)
        Assert.AreEqual(rfDeltas(2), rf.TierAxisTrackRate.DeltaRateRadPerSidSecPerSidSec, FRvariance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestTrackRatesDataModel()
        Dim trDM As TrackRatesDataModel = TrackRatesDataModel.GetInstance
        Dim tr As TrackRatesDataModel.TrackRate = trDM.GetTrackRate(CType(CoordName.PriAxis, ISFT))

        Dim testRate As Double = 1
        tr.RateRadPerSidSec = testRate

        Assert.AreEqual(testRate, trDM.GetTrackRate(CType(CoordName.PriAxis, ISFT)).RateRadPerSidSec)
        Assert.AreNotEqual(testRate, trDM.GetTrackRate(CType(CoordName.SecAxis, ISFT)).RateRadPerSidSec)

        Assert.AreEqual(testRate, trDM.GetPriAxisTrackRate.RateRadPerSidSec)
        Assert.AreNotEqual(testRate, trDM.GetSecAxisTrackRate.RateRadPerSidSec)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class
