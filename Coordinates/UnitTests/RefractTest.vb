Imports NUnit.Framework

<TestFixture()> Public Class RefractTest

    Private pSiteElevDeg As Double
    Private pRefract As Refract
    ' spherical trig coordinate conversions accuracy between 1/100" and 1/1000"
    Private pVariance As Double = Units.ArcsecToRad / 100

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        pSiteElevDeg = 1
        pRefract = Refract.GetInstance
    End Sub

    <Test()> Public Sub TestRefract()
        Dim refractRad As Double = pRefract.Calc(pSiteElevDeg * Units.DegToRad).Rad
        Assert.AreEqual(26.249999999999996, refractRad * Units.RadToArcmin)
    End Sub

    <Test()> Public Sub TestSiteToSkyToSite()
        Dim refractRad As Double = pRefract.Calc(pSiteElevDeg * Units.DegToRad).Rad
        Dim refractedElevDeg As Double = pSiteElevDeg - refractRad * Units.RadToDeg
        Dim newSiteElevDeg As Double = refractedElevDeg + pRefract.CalcRefractionToBackOut(refractedElevDeg * Units.DegToRad).Rad * Units.RadToDeg
        Assert.AreEqual(pSiteElevDeg, newSiteElevDeg)
    End Sub

    <Test()> Public Sub TestRefractionFromSiteSidT()
        Dim desiredRefractRad As Double = 34.5 * Units.ArcminToRad
        Dim latitudeRad As Double = 30 * Units.DegToRad

        Dim refractionFromSiteSidT As RefractionFromSiteSidT = Coordinates.RefractionFromSiteSidT.GetInstance
        ' aim for horizon facing south
        Dim position As Position = Coordinates.Position.GetInstance
        position.SetCoordDeg(90, -60, 0, 0, 90)

        Dim refractRad As Double = refractionFromSiteSidT.Calc(True, position, latitudeRad)

        Assert.AreEqual(desiredRefractRad, refractRad, pVariance)
        ' check recorded position coordinate errors
        Assert.AreEqual(desiredRefractRad, refractionFromSiteSidT.Refract.Coordinate.Rad, pVariance)
        Assert.AreEqual(0, refractionFromSiteSidT.DeltaRa, pVariance)
        ' northern hemisphere facing the southern horizon, Dec error is -34.5
        Assert.AreEqual(-desiredRefractRad, refractionFromSiteSidT.DeltaDec, pVariance)
    End Sub

    <Test()> Public Sub TestRefractionFromSiteSidT_CalcThenBackOut()
        Dim latitudeRad As Double = 30 * Units.DegToRad

        Dim refractionFromSiteSidT As RefractionFromSiteSidT = Coordinates.RefractionFromSiteSidT.GetInstance
        ' northern hemisphere facing south at the horizon
        Dim position As Position = Coordinates.Position.GetInstance
        position.SetCoordDeg(90, -60, 0, 0, 90)

        ' get refracted Ra/Dec
        Dim refractedPosition As Position = Coordinates.Position.GetInstance
        refractedPosition.CopyFrom(position)
        refractionFromSiteSidT.Calc(True, refractedPosition, latitudeRad)
        Dim DeltaRa As Double = refractionFromSiteSidT.DeltaRa
        Dim DeltaDec As Double = refractionFromSiteSidT.DeltaDec
        ' DeltaDec should be negative
        Assert.Greater(0, DeltaDec)
        refractedPosition.RA.Rad += DeltaRa
        refractedPosition.Dec.Rad += DeltaDec
        Assert.Greater(position.Dec.Rad, refractedPosition.Dec.Rad)

        ' get refraction to backout
        refractionFromSiteSidT.Calc(False, refractedPosition, latitudeRad)
        DeltaRa = refractionFromSiteSidT.DeltaRa
        DeltaDec = refractionFromSiteSidT.DeltaDec
        ' DeltaDec should be negative
        Assert.Greater(0, DeltaDec)
        Assert.AreEqual(position.RA.Rad, refractedPosition.RA.Rad - DeltaRa, pVariance)
        Assert.AreEqual(position.Dec.Rad, refractedPosition.Dec.Rad - DeltaDec, pVariance)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
