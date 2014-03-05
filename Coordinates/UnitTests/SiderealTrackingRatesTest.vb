Imports NUnit.Framework

<TestFixture()> Public Class SiderealTrackingRatesTest

    Dim pRates As IRates
    Dim pSidT As Double

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        pSidT = Time.GetInstance.CalcSidTNow()
    End Sub

    <Test()> Public Sub TestRatesFactory()
        Dim eRates As System.Collections.IEnumerator = Rates.ISFT.Enumerator
        While eRates.MoveNext()
            Assert.IsNotNull(RatesFactory.GetInstance.Build(CType(eRates.Current, ISFT)))
        End While
    End Sub

    <Test()> Public Sub TestRatesEquatAlign()
        Dim decRate As Double = 0
        Dim raRate As Double = 15 * Units.ArcsecToRad
        Dim FRRate As Double = 0
        Dim variance As Double = Units.ArcsecToRad / 1000

        pRates = FormulaRates.GetInstance
        EquatAlignSubr(True)
        Assert.AreEqual(raRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(decRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, variance)
        EquatAlignSubr(False)
        Assert.AreEqual(-raRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-decRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, variance)

        pRates = TrigRates.GetInstance
        EquatAlignSubr(True)
        Assert.AreEqual(raRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(decRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, variance)
        EquatAlignSubr(False)
        Assert.AreEqual(-raRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-decRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, variance)

        pRates = MatrixRates.GetInstance
        EquatAlignSubr(True)
        Assert.AreEqual(raRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(decRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, variance)
        EquatAlignSubr(False)
        Assert.AreEqual(-raRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-decRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, variance)
    End Sub

    <Test()> Public Sub TestRatesAltazAlign()
        Dim altRate As Double
        Dim azRate As Double
        Dim FRRate As Double
        Dim variance As Double = Units.ArcsecToRad / 1000

        pRates = FormulaRates.GetInstance
        AltazAlignSubr(True)
        altRate = pRates.SecAxisTrackRate.RateRadPerSidSec
        azRate = pRates.PriAxisTrackRate.RateRadPerSidSec
        FRRate = pRates.TierAxisTrackRate.RateRadPerSidSec

        pRates = TrigRates.GetInstance
        AltazAlignSubr(True)
        Assert.AreEqual(altRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(azRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, variance)
        AltazAlignSubr(False)
        Assert.AreEqual(-altRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-azRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, variance)

        Dim FRvariance As Double = Units.ArcsecToRad / 100
        pRates = MatrixRates.GetInstance
        AltazAlignSubr(True)
        Assert.AreEqual(altRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(azRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, FRvariance)
        AltazAlignSubr(False)
        Assert.AreEqual(-altRate, pRates.SecAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-azRate, pRates.PriAxisTrackRate.RateRadPerSidSec, variance)
        Assert.AreEqual(-FRRate, pRates.TierAxisTrackRate.RateRadPerSidSec, FRvariance)
    End Sub

    <Test()> Public Sub TestAlignmentStyleArray()
        Dim alignmentStyleArray As AlignmentStyleArray = Coordinates.AlignmentStyleArray.GetInstance
        alignmentStyleArray.BuildArray(CType(Rates.FormulaRates, ISFT))
        Assert.AreEqual(2, alignmentStyleArray.Array.Count)
    End Sub

    <TearDown()> Public Sub Dispose()
        pRates = Nothing
    End Sub

    Private Sub EquatAlignSubr(ByVal northernHemisphere As Boolean)
        InitStateFactory.GetInstance.SetInit(pRates.InitStateTemplate, CType(InitStateType.Equatorial, ISFT))

        pRates.InitStateTemplate.ICoordXform.Position.SidT.Rad = pSidT

        If northernHemisphere Then
            pRates.InitStateTemplate.ICoordXform.Site.Latitude.Rad = Units.QtrRev
        Else
            pRates.InitStateTemplate.ICoordXform.Site.Latitude.Rad = -Units.QtrRev
        End If

        pRates.InitStateTemplate.ICoordXform.Site.Longitude.Rad = 120 * Units.DegToRad
        pRates.Init()
        pRates.InitStateTemplate.ICoordXform.Position.Alt.Rad = 0
        pRates.InitStateTemplate.ICoordXform.Position.Az.Rad = Units.HalfRev
        pRates.CalcRates()
    End Sub

    Private Sub AltazAlignSubr(ByVal northernHemisphere As Boolean)
        InitStateFactory.GetInstance.SetInit(pRates.InitStateTemplate, CType(InitStateType.Altazimuth, ISFT))

        pRates.InitStateTemplate.ICoordXform.Position.SidT.Rad = pSidT

        If northernHemisphere Then
            pRates.InitStateTemplate.ICoordXform.Site.Latitude.Rad = 45 * Units.DegToRad
        Else
            pRates.InitStateTemplate.ICoordXform.Site.Latitude.Rad = -45 * Units.DegToRad
        End If

        pRates.InitStateTemplate.ICoordXform.Site.Longitude.Rad = 120 * Units.DegToRad
        pRates.Init()
        pRates.InitStateTemplate.ICoordXform.Position.Alt.Rad = 45 * Units.DegToRad
        pRates.InitStateTemplate.ICoordXform.Position.Az.Rad = 160 * Units.DegToRad
        pRates.CalcRates()
    End Sub
End Class
