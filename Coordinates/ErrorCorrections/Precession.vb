''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.Precession
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' 
''' from http://www.seds.org/~spider/spider/ScholarX/coordpCh.html
''' The star with the largest observed proper motion is 9.7 mag Barnard's Star in Ophiuchus with 10.27 "/y (arc seconds per year).
''' According to F. Schmeidler, only about 500 stars are known to have proper motions of more than 1 "/y.
''' 
''' from http://www.seds.org/~spider/spider/ScholarX/coordpCh.html#precession
''' low precision but quick processing routine for precession
''' high precision routine from Meeus
''' 
''' Precession of the Earth's polar axis is caused by the gravitational pull of the Sun and the Moon on the equatorial
''' bulge of the flattened rotating Earth. It makes the polar axis precess around the pole of the ecliptic,
''' with a period of 25,725 years (the so-called Platonic year).
''' The effect is large enough for changing the equatorial coordinate system significantly in comparatively short times
''' (therefore, Hipparchus was able to discover it around 130 B.C.).
''' Sun and moon together give rise to the lunisolar precession p0, while the other planets contribute the
''' significantly smaller planetary precession p1, which sum up to the general precession p
''' numerical values for these quantities are (from Schmeidler t is the time in tropical years from 2000.0):
''' p0 =  50.3878" + 0.000049" * t
''' p1 = - 0.1055" + 0.000189" * t
''' p  =  50.2910" + 0.000222" * t
'''
''' These values give the annual increase of ecliptical longitude for all stars.
''' The effect on equatorial coordinates is formally more complicated, and approximately given by
''' RA  = m + n * sin RA * tan Dec
''' Dec = n * cos RA
''' (my note: p0,p1,p is per year)
''' where the constants m and n are the precession components given by
''' m = + 46.124" + 0.000279" * t
'''   =   3.0749 s + 0.0000186 s * t
''' n = + 20.043" - 0.000085" * t
'''   =   1.3362s - 0.0000056 s * t
''' 
''' Quick vs rigorous calculations:
''' for 10 yr period -
''' RA=0deg, discrepancy=-0.017451678342158arcsec; Dec=0deg, discrepancy=-0.00524010120891173arcsec
''' RA=0deg, discrepancy=-4.37426810253779arcsec; Dec=87.0000000000001deg, discrepancy=-0.00523773447528501arcsec
''' 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	3/27/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Precession

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public UseRigorousCalc As Boolean
    Public DeltaYr As Double
    Public StartYr As Double
    Public PreRa As Double
    Public PreDec As Double
    Public DeltaRa As Double
    Public DeltaDec As Double
#End Region

#Region "Private and Protected Members"
    Private pEta As Double
    Private pZeta As Double
    Private pTheta As Double
    Private pLastStartYr As Double
    Private pStartJD As Double
    Private pLastT1 As Double
    Private pLastT2 As Double
    Private pSinTheta As Double
    Private pCosTheta As Double
    Private eTime As Time
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Precession
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Precession = New Precession
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        eTime = Time.GetInstance
        UseRigorousCalc = False
    End Sub

    Public Shared Function GetInstance() As Precession
        Return New Precession
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Calc(ByRef position As Position, ByVal deltaYr As Double, Optional ByVal StartYr As Double = Units.JDYear) As Boolean
        PreRa = position.RA.Rad
        PreDec = position.Dec.Rad
        Me.DeltaYr = deltaYr
        Me.StartYr = StartYr

        If UseRigorousCalc Then
            calcRigorousSubr()
        Else
            calcQuickSubr()
        End If

        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function calcQuickSubr() As Boolean
        Dim m As Double
        Dim n As Double

        m = 46.124 + 0.000279 * DeltaYr / 100
        n = 20.043 - 0.0085 * DeltaYr / 100
        DeltaRa = DeltaYr * (m + n * Math.Sin(PreRa) * Math.Tan(PreDec)) * Units.ArcsecToRad
        DeltaDec = DeltaYr * n * Math.Cos(PreRa) * Units.ArcsecToRad

        Return True
    End Function

    Private Function calcRigorousSubr() As Boolean
        Dim t1 As Double
        If StartYr <> pLastStartYr Then
            pStartJD = eTime.CalcJD(eTime.CreateDateTime(StartYr))
            t1 = (pStartJD - Units.JD2000) / 36525
            StartYr = pLastStartYr
        End If
        Dim t2 As Double = DeltaYr / 100

        If t1 <> pLastT1 OrElse t2 <> pLastT2 Then
            If t1.Equals(0) Then
                pEta = (2306.2181 * t2 + 0.30188 * t2 * t2 + 0.017998 * t2 * t2 * t2) * Units.ArcsecToRad
                pZeta = (2306.2181 * t2 + 1.09468 * t2 * t2 + 0.018203 * t2 * t2 * t2) * Units.ArcsecToRad
                pTheta = (2004.3109 * t2 - 0.42665 * t2 * t2 + 0.041883 * t2 * t2 * t2) * Units.ArcsecToRad
            Else
                pEta = ((2306.2181 + 1.39656 * t1 - 0.000139 * t1 * t1) * t2 + (0.30188 - 0.000344 * t1) * t2 * t2 + 0.017998 * t2 * t2 * t2) * Units.ArcsecToRad
                pZeta = ((2306.2181 + 1.39656 * t1 - 0.000139 * t1 * t1) * t2 + (1.09468 + 0.000066 * t1) * t2 * t2 + 0.018203 * t2 * t2 * t2) * Units.ArcsecToRad
                pTheta = ((2004.3109 - 0.8533 * t1 - 0.000217 * t1 * t1) * t2 - (0.42665 + 0.000217 * t1) * t2 * t2 + 0.041883 * t2 * t2 * t2) * Units.ArcsecToRad
            End If
            pSinTheta = Math.Sin(pTheta)
            pCosTheta = Math.Cos(pTheta)
            pLastT1 = t1
            pLastT2 = t2
        End If
        'Debug.WriteLine("eta/z/theta " & eta * Units.RadToArcsec & " " & z * Units.RadToArcsec & " " & theta * Units.RadToArcsec)

        ' if necessary, bring dec within range of +-90deg by rotating Ra by HalfRev 
        If PreDec < -Units.QtrRev OrElse PreDec > Units.QtrRev Then
            PreDec = eMath.ValidRadHalfPi(PreDec)
            PreRa = eMath.ValidRad(PreRa + Units.HalfRev)
        End If

        Dim sinPreDec As Double = Math.Sin(PreDec)
        Dim cosPreDec As Double = Math.Cos(PreDec)
        Dim PreRaPlusEta As Double = PreRa + pEta
        Dim cosPreRaPlusEta As Double = Math.Cos(PreRaPlusEta)
        Dim a As Double = cosPreDec * Math.Sin(PreRaPlusEta)
        Dim b As Double = pCosTheta * cosPreDec * cosPreRaPlusEta - pSinTheta * sinPreDec
        Dim c As Double = pSinTheta * cosPreDec * cosPreRaPlusEta + pCosTheta * sinPreDec
        'Debug.WriteLine("a/b/c " & a & " " & b & " " & c)

        Dim ra As Double = Math.Atan2(a, b) + pZeta
        If Double.IsNaN(ra) Then
            ExceptionService.Notify("Precession.calcSubrRigorous() resulted in ra not-a-number.")
            Return False
        End If
        DeltaRa = eMath.ValidRadPi(ra - PreRa)

        Dim dec As Double
        Dim decCompleted As Boolean = False
        ' use alternative formula if very close to pole
        Dim closeToPoleRad As Double = Units.ArcminToRad
        If PreDec > 0 AndAlso Units.QtrRev - PreDec < closeToPoleRad _
        OrElse PreDec < 0 AndAlso Units.QtrRev + PreDec < closeToPoleRad Then
            Dim AcosParm As Double = Math.Sqrt(a * a + b * b)
            If Math.Abs(AcosParm) <= 1 Then
                dec = Math.Acos(AcosParm)
                decCompleted = True
            End If
        End If
        If Not decCompleted Then
            If Math.Abs(c) <= 1 Then
                dec = Math.Asin(c)
            Else
                dec = PreDec
                DebugTrace.WriteLine("Could not obtain Dec rigorous precession")
            End If
        End If
        If Double.IsNaN(dec) Then
            ExceptionService.Notify("Precession.calcSubrRigorous() resulted in dec not-a-number.")
            Return False
        End If
        DeltaDec = eMath.ValidRadHalfPi(dec - PreDec)

        Return True
    End Function
#End Region

End Class
