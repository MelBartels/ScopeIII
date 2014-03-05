Imports NUnit.Framework

<TestFixture()> Public Class CelestialErrorsCalculatorFacadeTest

    Dim pVariance As Double = Units.ArcsecToRad / 20

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub BasicCall()
        Dim cecf As CelestialErrorsCalculatorFacade = CelestialErrorsCalculatorFacade.GetInstance
        cecf.UseCalculator = True

        Dim az As Double() = cecf.AdaptForCelestialErrors(0, 0, 0, getLatitudeRad, True)
        Assert.IsTrue(az(0) = 0 And az(1) = 0)

        az = cecf.AdaptForCelestialErrors(0, 0, 0, getLatitudeRad, True)
        Assert.IsTrue(az(0) = 0 And az(1) = 0)

        cecf.IncludeRefraction = True
        az = cecf.AdaptForCelestialErrors(0, 0, 0, getLatitudeRad, True)
        Assert.IsFalse(az(0) = 0 And az(1) = 0)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub AllCorrections()
        Dim variance As Double = Units.ArcsecToRad / 1000

        ' get Ra/Dec corrections copying code from the start of CelestialErrorsCalculatorTest.UncorrectedToCorrectedToUncorrected()
        Dim position As Position = Coordinates.Position.GetInstance
        position.SetCoordDeg(180, -59, 180, 0, 180)
        Dim cec As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim cecPosition As Position = Coordinates.Position.GetInstance
        cecPosition.CopyFrom(position)
        cec.CalculateErrors(cecPosition, Nothing, fromDate, toDate, True, True, True, True, getLatitudeRad)
        Dim totalRaRadUncorrectedToCorrected As Double = cec.DeltaRa
        Dim totalDecRadUncorrectedToCorrected As Double = cec.DeltaDec
        Assert.IsTrue(totalRaRadUncorrectedToCorrected <> 0)
        Assert.IsTrue(totalDecRadUncorrectedToCorrected <> 0)

        ' now run the same test using the facade
        Dim cecf As CelestialErrorsCalculatorFacade = CelestialErrorsCalculatorFacade.GetInstance
        cecf.UseCalculator = True
        cecf.FromDate = fromDate()
        cecf.ToDate = toDate()
        cecf.IncludePrecession = True
        cecf.IncludeNutationAnnualAberration = True
        cecf.IncludeRefraction = True
        Dim az As Double() = cecf.AdaptForCelestialErrors(position.RA.Rad, position.Dec.Rad, position.SidT.Rad, 30 * Units.DegToRad, True)
        Assert.AreEqual(totalRaRadUncorrectedToCorrected, az(0) - position.RA.Rad, variance)
        Assert.AreEqual(totalDecRadUncorrectedToCorrected, az(1) - position.Dec.Rad, variance)

        ' 2nd option to call into the facade
        az = cecf.AdaptForCelestialErrors(position, position.GetInstance, fromDate, toDate, True, True, True, True, getLatitudeRad)
        Assert.AreEqual(totalRaRadUncorrectedToCorrected, az(0) - position.RA.Rad, variance)
        Assert.AreEqual(totalDecRadUncorrectedToCorrected, az(1) - position.Dec.Rad, variance)

        cecf.UseCalculator = False
        az = cecf.AdaptForCelestialErrors(position, position.GetInstance, fromDate, toDate, True, True, True, True, getLatitudeRad)
        Assert.AreEqual(0, az(0) - position.RA.Rad, variance)
        Assert.AreEqual(0, az(1) - position.Dec.Rad, variance)

        Assert.IsTrue(True)
    End Sub

    Private Function fromDate() As DateTime
        Return New DateTime(2005, 1, 1)
    End Function

    Private Function toDate() As DateTime
        Return New DateTime(2006, 6, 6)
    End Function

    Private Function getLatitudeRad() As Double
        Return 30 * Units.DegToRad
    End Function

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
