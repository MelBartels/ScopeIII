Imports NUnit.Framework

<TestFixture()> Public Class KingRateTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestRates()
        ' sidereal tracking rate = 1440/units.SidRate = 1436.0681760162715 
        ' from http://www.brayebrookobservatory.org/BrayObsWebSite/HOMEPAGE/BRAYOBS%20PUBLICATIONS.html 
        ' King Rate is 1436.46 at temperate latitudes, +- 2 hrs meridian, -10 to 30deg dec
        ' King Rate of 1436.46 = tracking rate ratio of 0.99972722945036518 or 14.995908441755478"/sec
        ' using instantaneous corrected tracking rates routines for testing coordinates, RA tr = 14.994"/sec,
        ' if HA is reduced to 1 hr, then RA tr = 14.996"/sec
        Dim RaRad As Double = 6 * Units.HrToRad
        Dim DecRad As Double = 5 * Units.DegToRad
        Dim sidTRad As Double = 7.5 * Units.HrToRad
        Dim latitudeRad As Double = 30 * Units.DegToRad

        Dim krRefracted As Double = KingRate.GetInstance.AtRefractedPole(RaRad, DecRad, sidTRad, latitudeRad)
        Dim kr As Double = KingRate.GetInstance.AtTruePole(RaRad, DecRad, sidTRad, latitudeRad)
        Dim tr As Double = KingRate.GetInstance.SidTrackRatio(kr)

        Assert.AreEqual(1436.4583378470516, krRefracted)
        Assert.AreEqual(1436.4583717529179, kr)
        Assert.AreEqual(14.995925439842333, tr)

        ' southern hemisphere, where the tracking rate will be reversed
        latitudeRad = -latitudeRad
        DecRad = -DecRad

        Dim krSouthern As Double = KingRate.GetInstance.AtTruePole(RaRad, DecRad, sidTRad, latitudeRad)
        Assert.AreEqual(-kr, krSouthern)

        ' directly under pole, 10 deg above horiz: instantaneous corrected tracking rates routines, RA tr = 15.058"/sec
        RaRad = 12 * Units.HrToRad
        DecRad = 70 * Units.DegToRad
        sidTRad = 0
        latitudeRad = 30 * Units.DegToRad
        Dim kru As Double = KingRate.GetInstance.AtTruePole(RaRad, DecRad, sidTRad, latitudeRad)
        Dim tru As Double = KingRate.GetInstance.SidTrackRatio(kru)
        Assert.AreEqual(15.040968507741932, tru)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
