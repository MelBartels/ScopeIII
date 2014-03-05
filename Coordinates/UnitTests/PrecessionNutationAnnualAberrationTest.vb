Imports NUnit.Framework

<TestFixture()> Public Class PrecessionNutationAnnualAberrationTest

    Private Const AllowedErrorRad As Double = Units.ArcsecToRad / 1000

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestBasicVars()
        Dim dateTime As New DateTime(1987, 4, 10)
        Dim JD As Double = Time.GetInstance.CalcJD(dateTime, 0)
        Assert.AreEqual(2446895.5, JD)
        Dim CelestialCoordinateCalcs As CelestialCoordinateCalcs = Coordinates.CelestialCoordinateCalcs.GetInstance
        CelestialCoordinateCalcs.CalcBaseVars(JD)
        Dim varianceArcsec As Double = 0.1
        Assert.IsTrue(Math.Abs(-3.788 - CelestialCoordinateCalcs.NutationLongitude) < varianceArcsec)
        Assert.IsTrue(Math.Abs(9.443 - CelestialCoordinateCalcs.NutationObliquity) < varianceArcsec)
        Dim ObliquityEcliptic As Coordinate = Coordinate.GetInstance
        ObliquityEcliptic.Rad = CelestialCoordinateCalcs.ObliquityEcliptic
        Dim coordExpType As ISFT = Coordinates.CoordExpType.DMS
        DebugTrace.WriteLine(ObliquityEcliptic.ToString(coordExpType))
        Assert.AreEqual("+23:26:37", ObliquityEcliptic.ToString(coordExpType))
    End Sub

    <Test()> Public Sub TestPrecessionGeneral()
        Dim variance As Double = Units.ArcsecToRad
        ' example: change over 5 years (from 2000 to 2005) using position 00:00:00 00:00:00 is 00:00:15.375 00:01:40
        Dim precessRA As Double = 15.375 * Units.SecToRad
        Dim precessDec As Double = 1 * Units.ArcminToRad + 40 * Units.ArcsecToRad
        Dim position As Position = Coordinates.Position.GetInstance
        Dim precession As Precession = Coordinates.Precession.GetInstance
        precession.UseRigorousCalc = False
        precession.Calc(position, 5)
        Dim errorRA As Double = precessRA - precession.DeltaRa
        Dim errorDec As Double = precessDec - precession.DeltaDec
        Assert.IsTrue(Math.Abs(errorRA) < variance)
        Assert.IsTrue(Math.Abs(errorDec) < variance)
        precession.UseRigorousCalc = True
        precession.Calc(position, 5)
        errorRA = precessRA - precession.DeltaRa
        errorDec = precessDec - precession.DeltaDec
        Assert.IsTrue(Math.Abs(errorRA) < variance)
        Assert.IsTrue(Math.Abs(errorDec) < variance)

        'from communication w/ Don Ware:
        'As an example:  Alpha Andromeda
        'J2000.0  from Yale Bright Star and Hipparcos
        'Ra   00:08:23
        'Dec 29:05:26
        'After above precession:
        'Ra   00:08:39
        'Dec 29:07:06
        position.RA.Rad = 8 * Units.MinToRad + 23 * Units.SecToRad
        position.Dec.Rad = 29 * Units.DegToRad + 5 * Units.ArcminToRad + 26 * Units.ArcsecToRad
        precession.UseRigorousCalc = False
        precession.Calc(position, 5)
        position.RA.Rad += precession.DeltaRa
        position.Dec.Rad += precession.DeltaDec
        Dim coordExp As ICoordExp = HMS.GetInstance
        Assert.IsTrue(coordExp.ToString(position.RA.Rad).Equals("00:08:39"))
        coordExp = DMS.GetInstance
        Assert.IsTrue(coordExp.ToString(position.Dec.Rad).Equals("+29:07:06"))
    End Sub

    <Test()> Public Sub TestPrecessionQuickAndRigorousRegulus()
        Dim variance As Double = Units.ArcsecToRad
        ' example from Astronomical Algorithms, 2nd edition, Meeus
        Dim position As Position = Coordinates.Position.GetInstance
        Dim precession As Precession = Coordinates.Precession.GetInstance
        precession.UseRigorousCalc = False

        Dim RegulusJ2000RaRad As Double = 10 * Units.HrToRad + 8 * Units.MinToRad + 22.3 * Units.SecToRad
        Dim RegulusJ2000DecRad As Double = 11 * Units.DegToRad + 58 * Units.ArcminToRad + 2 * Units.ArcsecToRad

        Dim RegulusJ1978RaRad As Double = 10 * Units.HrToRad + 7 * Units.MinToRad + 12.1 * Units.SecToRad
        Dim RegulusJ1978DecRad As Double = 12 * Units.DegToRad + 4 * Units.ArcminToRad + 31 * Units.ArcsecToRad

        Dim RegulusRaProperMotionRadYr As Double = -0.0169 * Units.SecToRad
        Dim RegulusDecProperMotionRadYr As Double = 0.006 * Units.ArcsecToRad

        Dim yr As Double = -22
        position.RA.Rad = RegulusJ2000RaRad + RegulusRaProperMotionRadYr * yr
        position.Dec.Rad = RegulusJ2000DecRad + RegulusDecProperMotionRadYr * yr

        precession.Calc(position, yr)

        Debug.WriteLine("Ra deviation from book answer " & (RegulusJ1978RaRad - (position.RA.Rad + precession.DeltaRa)) * Units.RadToArcsec & "arcsec")
        Debug.WriteLine("Dec deviation from book answer " & (RegulusJ1978DecRad - (position.Dec.Rad + precession.DeltaDec)) * Units.RadToArcsec & "arcsec")
        Assert.AreEqual(RegulusJ1978RaRad, position.RA.Rad + precession.DeltaRa, variance)
        Assert.AreEqual(RegulusJ1978DecRad, position.Dec.Rad + precession.DeltaDec, variance)

        precession.UseRigorousCalc = True

        position.RA.Rad = RegulusJ2000RaRad + RegulusRaProperMotionRadYr * yr
        position.Dec.Rad = RegulusJ2000DecRad + RegulusDecProperMotionRadYr * yr

        precession.Calc(position, yr)

        Debug.WriteLine("Ra deviation from book answer " & (RegulusJ1978RaRad - (position.RA.Rad + precession.DeltaRa)) * Units.RadToArcsec & "arcsec")
        Debug.WriteLine("Dec deviation from book answer " & (RegulusJ1978DecRad - (position.Dec.Rad + precession.DeltaDec)) * Units.RadToArcsec & "arcsec")
        Assert.AreEqual(RegulusJ1978RaRad, position.RA.Rad + precession.DeltaRa, variance)
        Assert.AreEqual(RegulusJ1978DecRad, position.Dec.Rad + precession.DeltaDec, variance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestPrecessionRigorousThetaPersei()
        Dim variance As Double = Units.ArcsecToRad
        ' example from Astronomical Algorithms, 2nd edition, Meeus
        Dim position As Position = Coordinates.Position.GetInstance
        Dim precession As Precession = Coordinates.Precession.GetInstance
        precession.UseRigorousCalc = True

        Dim ThetaPerseiJ2000RaRad As Double = 2 * Units.HrToRad + 44 * Units.MinToRad + 11.986 * Units.SecToRad
        Dim ThetaPerseiJ2000DecRad As Double = 49 * Units.DegToRad + 13 * Units.ArcminToRad + 42.48 * Units.ArcsecToRad

        Dim ThetaPerseiJ2028RaRad As Double = 2 * Units.HrToRad + 46 * Units.MinToRad + 11.331 * Units.SecToRad
        Dim ThetaPerseiJ2028DecRad As Double = 49 * Units.DegToRad + 20 * Units.ArcminToRad + 54.54 * Units.ArcsecToRad

        Dim ThetaPerseiRaProperMotionRadYr As Double = 0.03425 * Units.SecToRad
        Dim ThetaPerseiDecProperMotionRadYr As Double = -0.0895 * Units.ArcsecToRad

        Dim yr As Double = 28.86705
        position.RA.Rad = ThetaPerseiJ2000RaRad + ThetaPerseiRaProperMotionRadYr * yr
        position.Dec.Rad = ThetaPerseiJ2000DecRad + ThetaPerseiDecProperMotionRadYr * yr

        precession.Calc(position, yr)

        Debug.WriteLine("Ra deviation from book answer " & (ThetaPerseiJ2028RaRad - (position.RA.Rad + precession.DeltaRa)) * Units.RadToArcsec & "arcsec")
        Debug.WriteLine("Dec deviation from book answer " & (ThetaPerseiJ2028DecRad - (position.Dec.Rad + precession.DeltaDec)) * Units.RadToArcsec & "arcsec")
        Assert.AreEqual(ThetaPerseiJ2028RaRad, position.RA.Rad + precession.DeltaRa, variance)
        Assert.AreEqual(ThetaPerseiJ2028DecRad, position.Dec.Rad + precession.DeltaDec, variance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestPrecessionRigorousPolaris()
        Dim variance As Double = Units.ArcsecToRad
        ' high Dec example from Astronomical Algorithms, 2nd edition, Meeus
        Dim position As Position = Coordinates.Position.GetInstance
        Dim precession As Precession = Coordinates.Precession.GetInstance
        precession.UseRigorousCalc = True

        Dim PolarisJ2000RaRad As Double = 2 * Units.HrToRad + 31 * Units.MinToRad + 48.704 * Units.SecToRad
        Dim PolarisJ2000DecRad As Double = 89 * Units.DegToRad + 15 * Units.ArcminToRad + 50.72 * Units.ArcsecToRad

        Dim PolarisJ2050RaRad As Double = 3 * Units.HrToRad + 48 * Units.MinToRad + 16.43 * Units.SecToRad
        Dim PolarisJ2050DecRad As Double = 89 * Units.DegToRad + 27 * Units.ArcminToRad + 15.38 * Units.ArcsecToRad

        Dim PolarisJ2100RaRad As Double = 5 * Units.HrToRad + 53 * Units.MinToRad + 29.17 * Units.SecToRad
        Dim PolarisJ2100DecRad As Double = 89 * Units.DegToRad + 32 * Units.ArcminToRad + 22.18 * Units.ArcsecToRad

        Dim PolarisRaProperMotionRadYr As Double = 0.19877 * Units.SecToRad
        Dim PolarisDecProperMotionRadYr As Double = -0.0152 * Units.ArcsecToRad

        Dim yr As Double = 50
        position.RA.Rad = PolarisJ2000RaRad + PolarisRaProperMotionRadYr * yr
        position.Dec.Rad = PolarisJ2000DecRad + PolarisDecProperMotionRadYr * yr

        precession.Calc(position, yr)

        Debug.WriteLine("Ra deviation from book answer " & (PolarisJ2050RaRad - (position.RA.Rad + precession.DeltaRa)) * Units.RadToArcsec & "arcsec")
        Debug.WriteLine("Dec deviation from book answer " & (PolarisJ2050DecRad - (position.Dec.Rad + precession.DeltaDec)) * Units.RadToArcsec & "arcsec")
        Assert.AreEqual(PolarisJ2050RaRad, position.RA.Rad + precession.DeltaRa, variance)
        Assert.AreEqual(PolarisJ2050DecRad, position.Dec.Rad + precession.DeltaDec, variance)

        yr = 100
        position.RA.Rad = PolarisJ2000RaRad + PolarisRaProperMotionRadYr * yr
        position.Dec.Rad = PolarisJ2000DecRad + PolarisDecProperMotionRadYr * yr

        precession.Calc(position, yr)

        Dim RaVariance As Double = Units.ArcsecToRad * 12
        Debug.WriteLine("Ra deviation from book answer " & (PolarisJ2100RaRad - (position.RA.Rad + precession.DeltaRa)) * Units.RadToArcsec & "arcsec")
        Debug.WriteLine("Dec deviation from book answer " & (PolarisJ2100DecRad - (position.Dec.Rad + precession.DeltaDec)) * Units.RadToArcsec & "arcsec")
        Assert.AreEqual(PolarisJ2100RaRad, position.RA.Rad + precession.DeltaRa, RaVariance)
        Assert.AreEqual(PolarisJ2100DecRad, position.Dec.Rad + precession.DeltaDec, variance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestPrecessionRigorousVsQuick()
        Dim variance As Double = Units.ArcsecToRad
        Dim precession As Precession = Coordinates.Precession.GetInstance
        Dim yr As Double = 10
        Dim position As Position = Coordinates.Position.GetInstance

        Dim stepSizeRad As Double = 5 * Units.DegToRad
        For RaRad As Double = 0 To Units.OneRev Step stepSizeRad
            For DecRad As Double = 0 To Units.QtrRev Step stepSizeRad
                position.RA.Rad = RaRad
                position.Dec.Rad = DecRad

                precession.UseRigorousCalc = False
                precession.Calc(position, yr)
                Dim precessionQuickRa As Double = precession.DeltaRa
                Dim precessionQuickDec As Double = precession.DeltaDec

                precession.UseRigorousCalc = True
                precession.Calc(position, yr)
                Dim precessionRigorousRa As Double = precession.DeltaRa
                Dim precessionRigorousDec As Double = precession.DeltaDec

                Dim sb As New Text.StringBuilder
                sb.Append("Ra=")
                sb.Append(RaRad * Units.RadToDeg)
                sb.Append("deg, discrepancy=")
                sb.Append((precessionQuickRa - precessionRigorousRa) * Units.RadToArcsec)
                sb.Append("arcsec; Dec=")
                sb.Append(DecRad * Units.RadToDeg)
                sb.Append("deg, discrepancy=")
                sb.Append((precessionQuickDec - precessionRigorousDec) * Units.RadToArcsec)
                sb.Append("arcsec")
                If Math.Abs(precessionQuickRa - precessionRigorousRa) > Units.ArcminToRad Then
                    sb.Append("; *** Ra difference exceeds 1 arcmin (precessionQuickRa=")
                    sb.Append(precessionQuickRa * Units.RadToDeg)
                    sb.Append("deg, precessionRigorousRa=")
                    sb.Append(precessionRigorousRa * Units.RadToDeg)
                    sb.Append("deg)")
                End If
                If Math.Abs(precessionQuickDec - precessionRigorousDec) > Units.ArcminToRad Then
                    sb.Append("; *** Dec difference exceeds 1 arcmin (precessionQuickDec=")
                    sb.Append(precessionQuickDec * Units.RadToDeg)
                    sb.Append("deg, precessionRigorousDec=")
                    sb.Append(precessionRigorousDec * Units.RadToDeg)
                    sb.Append("deg)")
                End If
                Debug.WriteLine(sb.ToString)

            Next
        Next

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestPrecessionRigorousUndo()
        Dim variance As Double = Units.ArcsecToRad
        Dim precession As Precession = Coordinates.Precession.GetInstance
        Dim yr As Double = 10
        Dim position As Position = Coordinates.Position.GetInstance

        Dim stepSizeRad As Double = 3 * Units.DegToRad
        For RaRad As Double = 0 To Units.OneRev Step stepSizeRad
            For DecRad As Double = 0 To Units.QtrRev Step stepSizeRad
                position.RA.Rad = RaRad
                position.Dec.Rad = DecRad

                precession.UseRigorousCalc = True
                precession.Calc(position, yr)
                Dim precessionRigorousRa As Double = precession.DeltaRa
                Dim precessionRigorousDec As Double = precession.DeltaDec

                ' undo the precession and compare to original
                position.RA.Rad = RaRad + precessionRigorousRa
                position.Dec.Rad = DecRad + precessionRigorousDec
                precession.UseRigorousCalc = True
                precession.Calc(position, -yr, Units.JDYear + yr)
                Dim undoPrecessionRigorousRa As Double = precession.DeltaRa
                Dim undoPrecessionRigorousDec As Double = precession.DeltaDec

                Dim sb As New Text.StringBuilder
                sb.Append("Ra=")
                sb.Append(RaRad * Units.RadToDeg)
                sb.Append("deg, Dec=")
                sb.Append(DecRad * Units.RadToDeg)
                sb.Append("deg: undo diff Ra=")
                sb.Append((precessionRigorousRa + undoPrecessionRigorousRa) * Units.RadToArcsec)
                sb.Append("arcsec, Dec=")
                sb.Append((precessionRigorousDec + undoPrecessionRigorousDec) * Units.RadToArcsec)
                If Math.Abs(precessionRigorousRa + undoPrecessionRigorousRa) > Units.ArcsecToRad Then
                    sb.Append("; *** Ra undo difference exceeds 1 arcsec")
                End If
                If Math.Abs(precessionRigorousDec + undoPrecessionRigorousDec) > Units.ArcsecToRad Then
                    sb.Append("; *** Dec undo difference exceeds 1 arcsec")
                End If
                Debug.WriteLine(sb.ToString)

                Assert.AreEqual(-precessionRigorousRa, undoPrecessionRigorousRa, AllowedErrorRad)
                Assert.AreEqual(-precessionRigorousDec, undoPrecessionRigorousDec, AllowedErrorRad)
            Next
        Next

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestNutation()
        Dim nutationAnnualAberration As NutationAnnualAberration = Coordinates.NutationAnnualAberration.GetInstance
        Dim position As Position = Coordinates.Position.GetInstance

        ' nutation test #1 from 1982 edition
        Dim testJD As Double = 2443825.69
        position.RA.Rad = 40.687 * Units.DegToRad
        position.Dec.Rad = 49.14 * Units.DegToRad
        nutationAnnualAberration.Calc(position, testJD)

        ' should be 4.059, -7.096
        Assert.AreEqual(4.0700564022889383, nutationAnnualAberration.NutationDeltaRa * Units.RadToArcsec)
        Assert.AreEqual(-7.0820120507016471, nutationAnnualAberration.NutationDeltaDec * Units.RadToArcsec)

        ' nutation test #2 from 2000 edition (2028-11-13)
        testJD = 2462088.69
        position.RA.Rad = 41.547214 * Units.DegToRad
        position.Dec.Rad = 49.348483 * Units.DegToRad
        nutationAnnualAberration.Calc(position, testJD)

        ' should be 15.843, 6.219
        Assert.AreEqual(16.093317999610889, nutationAnnualAberration.NutationDeltaRa * Units.RadToArcsec)
        Assert.AreEqual(6.243784286635532, nutationAnnualAberration.NutationDeltaDec * Units.RadToArcsec)
    End Sub

    ' same test setup as TestNutation
    <Test()> Public Sub TestAnnualAberration()
        Dim tolerance As Double = 0.0001
        Dim nutationAnnualAberration As NutationAnnualAberration = Coordinates.NutationAnnualAberration.GetInstance
        Dim position As Position = Coordinates.Position.GetInstance

        ' annual aberration test #1 from 1982 edition
        Dim testJD As Double = 2443825.69
        position.RA.Rad = 40.687 * Units.DegToRad
        position.Dec.Rad = 49.14 * Units.DegToRad
        nutationAnnualAberration.Calc(position, testJD)

        ' should be 29.619, 6.554
        Assert.AreEqual(29.8806, nutationAnnualAberration.AnnualAberrationDeltaRa * Units.RadToArcsec, tolerance)
        Assert.AreEqual(6.76153, nutationAnnualAberration.AnnualAberrationDeltaDec * Units.RadToArcsec, tolerance)

        ' annual aberration test #2 from 2000 edition
        testJD = 2462088.69
        position.RA.Rad = 41.547214 * Units.DegToRad
        position.Dec.Rad = 49.348483 * Units.DegToRad
        nutationAnnualAberration.Calc(position, testJD)

        ' should be 30.045, 6.697
        Assert.AreEqual(30.045, nutationAnnualAberration.AnnualAberrationDeltaRa * Units.RadToArcsec, tolerance)
        Assert.AreEqual(6.6967, nutationAnnualAberration.AnnualAberrationDeltaDec * Units.RadToArcsec, tolerance)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
