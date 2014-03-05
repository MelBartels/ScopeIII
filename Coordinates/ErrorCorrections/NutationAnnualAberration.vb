''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.NutationAnnualAberration
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' from Jean Meeus' Astronomical Formulae for Calculators second edition, 1982, pg 69-70, revised per later editions;
''' 
''' Nutation:
''' nutation is variation or fluctuation in rate of precession;
''' caused by difference between ecliptic and earth-moon plane, the latter which precesses 18.6 years;
''' thus, plane of the moon's orbit and the effect of the moon's pull on the earth varies, causing
''' fluctuations in the rate of precession;
''' 
''' AnnualAberration:
''' starlight seems to come from a different direction than if earth at rest: this effect is called annual aberration;
''' diurnal aberration is due to earth's daily rotation and is of .3" value so will be ignored
''' 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	5/28/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class NutationAnnualAberration

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public PreRa As Double
    Public PreDec As Double
    Public NutationDeltaRa As Double
    Public NutationDeltaDec As Double
    Public AnnualAberrationDeltaRa As Double
    Public AnnualAberrationDeltaDec As Double
#End Region

#Region "Private and Protected Members"
    Dim pCelestialCoordinateCalcs As CelestialCoordinateCalcs
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As NutationAnnualAberration
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As NutationAnnualAberration = New NutationAnnualAberration
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pCelestialCoordinateCalcs = CelestialCoordinateCalcs.GetInstance
    End Sub

    Public Shared Function GetInstance() As NutationAnnualAberration
        Return New NutationAnnualAberration
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Calc(ByRef position As Position, ByVal JD As Double) As Boolean
        PreRa = position.RA.Rad
        PreDec = position.Dec.Rad

        pCelestialCoordinateCalcs.CalcBaseVars(JD)
        calcNutationSubr()
        calcAnnualAberrationSubr()

        Return True
    End Function

#End Region

#Region "Private and Protected Methods"
    Private Function calcNutationSubr() As Boolean
        NutationDeltaRa = (Math.Cos(pCelestialCoordinateCalcs.ObliquityEcliptic) + Math.Sin(pCelestialCoordinateCalcs.ObliquityEcliptic) _
        * Math.Sin(PreRa) * Math.Tan(PreDec)) _
        * pCelestialCoordinateCalcs.NutationLongitude _
        - (Math.Cos(PreRa) * Math.Tan(PreDec)) * pCelestialCoordinateCalcs.NutationObliquity

        NutationDeltaRa *= Units.ArcsecToRad

        NutationDeltaDec = Math.Sin(pCelestialCoordinateCalcs.ObliquityEcliptic) * Math.Cos(PreRa) * pCelestialCoordinateCalcs.NutationLongitude _
        + Math.Sin(PreRa) * pCelestialCoordinateCalcs.NutationObliquity

        NutationDeltaDec *= Units.ArcsecToRad

        Return True
    End Function

    Private Function calcAnnualAberrationSubr() As Boolean

        '' from 1982 edition
        'AnnualAberrationDeltaRa = -20.49 * (Math.Cos(PreRa) * Math.Cos(pCelestialCoordinateCalcs.SunTrueLongitude) * Math.Cos(pCelestialCoordinateCalcs.ObliquityEcliptic) _
        '+ Math.Sin(PreRa) * Math.Sin(pCelestialCoordinateCalcs.SunTrueLongitude)) / Math.Cos(PreDec)

        'AnnualAberrationDeltaRa *= Units.ArcsecToRad

        'AnnualAberrationDeltaDec = -20.49 * (Math.Cos(pCelestialCoordinateCalcs.SunTrueLongitude) * Math.Cos(pCelestialCoordinateCalcs.ObliquityEcliptic) _
        '* (Math.Tan(pCelestialCoordinateCalcs.ObliquityEcliptic) * Math.Cos(PreDec) - Math.Sin(PreRa) * Math.Sin(PreDec)) _
        '+ Math.Cos(PreRa) * Math.Sin(PreDec) * Math.Sin(pCelestialCoordinateCalcs.SunTrueLongitude))

        'AnnualAberrationDeltaDec *= Units.ArcsecToRad

        ' from 2000 edition
        AnnualAberrationDeltaRa = -20.49552 * ((Math.Cos(PreRa) * Math.Cos(pCelestialCoordinateCalcs.SunTrueLongitude) * Math.Cos(pCelestialCoordinateCalcs.ObliquityEcliptic) _
        + Math.Sin(PreRa) * Math.Sin(pCelestialCoordinateCalcs.SunTrueLongitude)) / Math.Cos(PreDec)) _
        + pCelestialCoordinateCalcs.EccentricityEarthOrbit * 20.49552 * ((Math.Cos(PreRa) _
        * Math.Cos(pCelestialCoordinateCalcs.LongitudePerihelionEarthOrbit) * Math.Cos(pCelestialCoordinateCalcs.ObliquityEcliptic) _
        + Math.Sin(PreRa) * Math.Sin(pCelestialCoordinateCalcs.LongitudePerihelionEarthOrbit)) / Math.Cos(PreDec))

        AnnualAberrationDeltaRa *= Units.ArcsecToRad

        AnnualAberrationDeltaDec = -20.49552 * (Math.Cos(pCelestialCoordinateCalcs.SunTrueLongitude) _
        * Math.Cos(pCelestialCoordinateCalcs.ObliquityEcliptic) * (Math.Tan(pCelestialCoordinateCalcs.ObliquityEcliptic) * Math.Cos(PreDec) _
        - Math.Sin(PreRa) * Math.Sin(PreDec)) _
        + Math.Cos(PreRa) * Math.Sin(PreDec) * Math.Sin(pCelestialCoordinateCalcs.SunTrueLongitude)) _
        + pCelestialCoordinateCalcs.EccentricityEarthOrbit * 20.49552 * (Math.Cos(pCelestialCoordinateCalcs.LongitudePerihelionEarthOrbit) _
        * Math.Cos(pCelestialCoordinateCalcs.ObliquityEcliptic) * (Math.Tan(pCelestialCoordinateCalcs.ObliquityEcliptic) * Math.Cos(PreDec) _
        - Math.Sin(PreRa) * Math.Sin(PreDec)) + Math.Cos(PreRa) * Math.Sin(PreDec) * Math.Sin(pCelestialCoordinateCalcs.LongitudePerihelionEarthOrbit))

        AnnualAberrationDeltaDec *= Units.ArcsecToRad

        Return True
    End Function
#End Region

End Class
