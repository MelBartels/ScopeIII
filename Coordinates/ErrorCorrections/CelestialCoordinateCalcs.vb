''' -----------------------------------------------------------------------------
''' Project	 : Coordinates
''' Class	 : Coordinates.CelestialCoordinateCalcs
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' From Jean Meeus' Astronomical Formulae for Calculators second edition, 1982, revised per later editions;
''' 
''' Angular separation methods: two to calculate the angular separation of equatorial coordinates, two
''' to calculate the angular separation of altazimuth coordinates, and the final to calculate the difference
''' between the equatorial and azimuth angular separations;
''' 
''' Uncorrected values are raw equatorial coordinates;
''' Celestial errors are added to uncorrected values to obtain corrected values, ie, precession correction
''' (a positive value for RA) is added to obtain corrected value, eg, uncorrected RA=0, precession=1, corrected RA=1;
''' Similar for nutation and annual aberration;
''' Refraction is similarly added even though a negative value, eg, uncorrected Dec=-45, refraction=-.5, corrected Dec=-45.5;
''' 
''' Coordinate conversion and celestial errors:
''' given that altaz plots to RA=1,
''' if uncorrected RA=0, and corrections total 1 then corrected RA=1;
''' therefore altaz plots corrected RA=1 and uncorrected RA=0 at same point;
''' 
''' RA plotted        0    1    2
'''    uncorrected    23   0    1    (uncorrected values displaced 1hr east, ie, RA=1 is 1hr east of corrected RA=1)
'''    + correction   1    1    1    (uncorrected value that's corrected will appear 1hr west)
'''    = corrected    0    1    2
''' 
''' Therefore if precession a positive value for RA:
''' object's old RA=0, precession=1, current RA=1;
''' RA plotted        0    1    2
'''    old            23   0    1   (old value is eastward)
'''    + precession   1    1    1
'''    = current      0    1    2
''' Conclude that precession causes objects to drift eastward over time as they acquire larger RA values;
''' 
''' Celestial errors:
''' precession causes equat grid to rotate westward (eg, uncorrected RA=0 with correction=1 will appear at RA=1);
''' refraction causes equat grid to slide upward at horizon (object in sky appears higher, tracking runs slower);
''' 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	2/28/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class CelestialCoordinateCalcs

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public SunMeanLongitude As Double
    Public SunMeanAnomaly As Double
    Public SunEquationOfTheCenter As Double
    Public SunTrueLongitude As Double
    Public SunTrueAnomaly As Double

    Public MoonMeanLongitude As Double
    Public MoonMeanAnomaly As Double

    Public LongitudeMoonAscendingNode As Double
    Public LongitudePerihelionEarthOrbit As Double
    Public EccentricityEarthOrbit As Double
    Public ObliquityEcliptic As Double
    Public MeanObliquityEcliptic As Double

    Public NutationLongitude As Double
    Public NutationObliquity As Double

    Public Angsep As Double
    Public AngSepDiff As Double
#End Region

#Region "Private and Protected Members"
    Private pT As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CelestialCoordinateCalcs
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CelestialCoordinateCalcs = New CelestialCoordinateCalcs
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CelestialCoordinateCalcs
        Return New CelestialCoordinateCalcs
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub CalcBaseVars(ByVal JD As Double)
        pT = (JD - Units.JD2000) / 36525

        SunMeanLongitude = eMath.ValidRad((280.46646 + 36000.76983 * pT + 0.0003032 * pT * pT) * Units.DegToRad)
        MoonMeanLongitude = eMath.ValidRad((218.3165 + 481267.8813 * pT) * Units.DegToRad)
        EccentricityEarthOrbit = 0.016708634 - 0.000042037 * pT - 0.0000001267 * pT * pT
        LongitudePerihelionEarthOrbit = eMath.ValidRad((102.93735 + 1.71946 * pT + 0.00046 * pT * pT) * Units.DegToRad)
        SunMeanAnomaly = eMath.ValidRad((357.52911 + 35999.05029 * pT - 0.0001537 * pT * pT) * Units.DegToRad)
        MoonMeanAnomaly = eMath.ValidRad((134.96298 + 477198.867398 * pT - 0.0086972 * pT * pT) * Units.DegToRad)

        LongitudeMoonAscendingNode = eMath.ValidRad((125.04452 - 1934.136261 * pT + 0.0020708 * pT * pT + pT * pT * pT / 450000) * Units.DegToRad)
        MeanObliquityEcliptic = (23.43929 - 46.815 / 3600 * pT - 0.00059 / 3600 * pT * pT + 0.001813 / 3600 * pT * pT * pT) * Units.DegToRad
        SunEquationOfTheCenter = (1.914602 - 0.004817 * pT - 0.000014 * pT * pT) * Math.Sin(SunMeanAnomaly) + (0.019993 - 0.000101 * pT) * Math.Sin(2 * SunMeanAnomaly) + 0.000289 * Math.Sin(3 * SunMeanAnomaly)
        SunEquationOfTheCenter *= Units.DegToRad
        SunTrueLongitude = eMath.ValidRad(SunMeanLongitude + SunEquationOfTheCenter)

        NutationLongitude = -17.2 * Math.Sin(LongitudeMoonAscendingNode) - 1.32 * Math.Sin(2 * SunMeanLongitude) - 0.23 * Math.Sin(2 * MoonMeanLongitude) + 0.21 * Math.Sin(2 * LongitudeMoonAscendingNode)

        NutationObliquity = 9.2 * Math.Cos(LongitudeMoonAscendingNode) + 0.57 * Math.Cos(2 * SunMeanLongitude) + 0.1 * Math.Cos(2 * MoonMeanLongitude) - 0.09 * Math.Cos(2 * LongitudeMoonAscendingNode)

        ObliquityEcliptic = MeanObliquityEcliptic + NutationObliquity * Units.ArcsecToRad
    End Sub

    Public Function CalcEquatAngularSepViaHrAngle(ByRef a As Position, ByRef z As Position) As Double
        Dim angsep As Double
        Dim aHA As Double
        Dim zHA As Double
        Dim diffHA As Double

        ' hour angles
        aHA = a.SidT.Rad - a.RA.Rad
        zHA = z.SidT.Rad - z.RA.Rad
        diffHA = aHA - zHA

        angsep = Math.Acos(Math.Sin(a.Dec.Rad) * Math.Sin(z.Dec.Rad) + Math.Cos(a.Dec.Rad) * Math.Cos(z.Dec.Rad) * Math.Cos(diffHA))
        Return angsep
    End Function

    Public Function CalcEquatAngularSepViaRa(ByRef a As Position, ByRef z As Position) As Double
        Dim diffRa As Double

        diffRa = eMath.ValidRadPi(z.RA.Rad - a.RA.Rad)
        Angsep = Math.Acos(Math.Sin(a.Dec.Rad) * Math.Sin(z.Dec.Rad) + Math.Cos(a.Dec.Rad) * Math.Cos(z.Dec.Rad) * Math.Cos(diffRa))
        Return Angsep
    End Function

    Public Function CalcEquatAngularSepViaRaLwp(ByRef a As LWPosition, ByRef z As LWPosition) As Double
        Dim diffRa As Double

        diffRa = eMath.ValidRadPi(z.RA - a.RA)
        Angsep = Math.Acos(Math.Sin(a.Dec) * Math.Sin(z.Dec) + Math.Cos(a.Dec) * Math.Cos(z.Dec) * Math.Cos(diffRa))
        Return Angsep
    End Function

    Public Function CalcAltazAngularSep(ByRef a As Position, ByRef z As Position) As Double
        ' cos of angle same as cos of -angle, so doesn't matter if diffAZ is positive or negative
        Dim diffAz As Double = a.Az.Rad - z.Az.Rad
        Angsep = Math.Acos(Math.Sin(a.Alt.Rad) * Math.Sin(z.Alt.Rad) + Math.Cos(a.Alt.Rad) * Math.Cos(z.Alt.Rad) * Math.Cos(diffAz))
        Return Angsep
    End Function

    Public Function AngSepDiffViaHrAngle(ByRef a As Position, ByRef z As Position) As Double
        AngSepDiff = Math.Abs(Math.Abs(CalcEquatAngularSepViaHrAngle(a, z)) - Math.Abs(CalcAltazAngularSep(a, z)))
        Return AngSepDiff
    End Function

    Public Function AngSepDiffViaRa(ByRef a As Position, ByRef z As Position) As Double
        AngSepDiff = Math.Abs(Math.Abs(CalcEquatAngularSepViaRa(a, z)) - Math.Abs(CalcAltazAngularSep(a, z)))
        Return AngSepDiff
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class