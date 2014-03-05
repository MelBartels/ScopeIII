Imports NUnit.Framework

<TestFixture()> Public Class AstroTimeTest

    Dim pTime As Time

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        pTime = Time.GetInstance
    End Sub

    <Test()> Public Sub TestJD()
        pTime.CalcJD(New DateTime(1987, 4, 10, 19, 21, 0), 0)
        Assert.AreEqual(2446896.30625, pTime.JD)
    End Sub

    <Test()> Public Sub TestSidT()
        'pTime.Longitude.Rad = 120 * Units.DegToRad
        'While True
        pTime.CalcJDNow()
        DebugTrace.WriteLine(CStr(pTime.JD))
        pTime.CalcSidTNow()
        DebugTrace.WriteLine(pTime.SidT.Rad * Units.RadToHr & " " & pTime.SidT.ToString(CType(CoordExpType.HMSM, ISFT)))
        'System.Threading.Thread.Sleep(1000)
        'End While

        '1987 April 10 at 19:21:00 UT
        Dim sidTrad As Double = pTime.CalcSidTGreenwichMean(New DateTime(1987, 4, 10, 19, 21, 0), 0)
        Assert.AreEqual(8.5825248829584986, sidTrad * Units.RadToHr)
    End Sub

    <Test()> Public Sub TestCreateDateTime()
        Dim dt As DateTime = pTime.CreateDateTime(2007.5)
        Assert.AreEqual(2007, dt.Year)
        Assert.AreEqual(7, dt.Month)
        Assert.AreEqual(2, dt.Day)
        Assert.AreEqual(eMath.RInt(0.5 * Units.DayToYear), dt.DayOfYear)
    End Sub

    <Test()> Public Sub TestNowDate()
        Dim dt As DateTime = pTime.NowDate
        Assert.IsTrue(dt.Year >= 2007)
        Assert.IsTrue(dt.DayOfYear >= 1)
        Assert.AreEqual(0, dt.Hour)
        Debug.WriteLine(dt.ToLongDateString & " " & dt.ToLongTimeString)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
