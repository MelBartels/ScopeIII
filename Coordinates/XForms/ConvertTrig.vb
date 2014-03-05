''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.ConvertTrig
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Coordinate translation using standard spherical trig equations.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	4/25/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class ConvertTrig
    Inherits CoordXformBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ConvertTrig
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ConvertTrig = New ConvertTrig
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ConvertTrig
        Return New ConvertTrig
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' set site latitude, position ra, dec, sidT before calling;
    ''' see notes for CoordXformBase: if southern hemisphere, then flip dec bef. calc, 
    ''' then flip az after: necessary to conform to coordinate scheme for southern hemisphere
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/27/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overrides Function GetAltaz() As Boolean
        Dim latitudeRad As Double = Math.Abs(pSite.Latitude.Rad)
        Dim decRad As Double = pXposition.Dec.Rad
        If pSite.Latitude.Rad < 0 Then
            decRad = -decRad
        End If
        ' bring Dec to within -90 to +90 deg, otherwise trig formulae may fail
        Dim holdRaRad As Double = pXposition.RA.Rad
        If decRad > Units.QtrRev OrElse decRad < -Units.QtrRev Then
            decRad = Units.HalfRev - decRad
            pXposition.RA.Rad = eMath.ValidRad(pXposition.RA.Rad + Units.HalfRev)
        End If

        pXposition.HA.Rad = pXposition.SidT.Rad - pXposition.RA.Rad
        ' depends on HA
        pXposition.Alt.Rad = convertSecAxis(latitudeRad, pXposition.HA.Rad, decRad)
        ' depends on HA and Alt calculations
        pXposition.Az.Rad = ConvertPriAxis(latitudeRad, pXposition.Alt.Rad, pXposition.HA.Rad, decRad)
        If pSite.Latitude.Rad < 0 Then
            pXposition.Az.Rad = eMath.ReverseRad(pXposition.Az.Rad)
        End If

        pXposition.RA.Rad = holdRaRad

        ' if flipped, then restore 'true' or actual altaz coordinates since input equat and the subsequent
        ' coordinate translation is not aware of meridian flip and always results in not flipped coordinate values
        TranslateAltazAcrossPoleBasedOnMeridianFlip()
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' set site latitude, position alt, az, and sidT before calling;
    ''' see notes for CoordXformBase: if southern hemisphere, then flip az bef. calc, 
    ''' then flip dec after: necessary to conform to coordinate scheme for southern hemisphere
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    '''     ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/20/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overrides Function GetEquat() As Boolean
        ' return to equivalent not flipped altaz values for purposes of coordinate translation
        TranslateAltazAcrossPoleBasedOnMeridianFlip()

        ' bring altitude to within -90 to +90 deg, otherwise trig formulae may fail
        Dim holdAzRad As Double = pXposition.Az.Rad
        Dim holdAltRad As Double = pXposition.Alt.Rad
        If pXposition.Alt.Rad > Units.QtrRev OrElse pXposition.Alt.Rad < -Units.QtrRev Then
            pXposition.Alt.Rad = Units.HalfRev - pXposition.Alt.Rad
            pXposition.Az.Rad = eMath.ValidRad(pXposition.Az.Rad + Units.HalfRev)
        End If

        Dim latitudeRad As Double = Math.Abs(pSite.Latitude.Rad)
        Dim azRad As Double = pXposition.Az.Rad
        If pSite.Latitude.Rad < 0 Then
            azRad = eMath.ReverseRad(azRad)
        End If
        pXposition.Dec.Rad = convertSecAxis(latitudeRad, azRad, pXposition.Alt.Rad)
        ' depends on Dec
        pXposition.HA.Rad = ConvertPriAxis(latitudeRad, pXposition.Dec.Rad, azRad, pXposition.Alt.Rad)
        ' depends on Dec and HA
        pXposition.RA.Rad = eMath.ValidRad(pXposition.SidT.Rad - pXposition.HA.Rad)
        If pSite.Latitude.Rad < 0 Then
            pXposition.Dec.Rad = -pXposition.Dec.Rad
        End If

        pXposition.Az.Rad = holdAzRad
        pXposition.Alt.Rad = holdAltRad
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function convertSecAxis(ByVal lat As Double, ByVal fromPri As Double, ByVal fromSec As Double) As Double
        Dim sinToSec As Double = Math.Sin(fromSec) * Math.Sin(lat) + Math.Cos(fromSec) * Math.Cos(lat) * Math.Cos(fromPri)
        sinToSec = eMath.BoundsSinCos(sinToSec)
        Return Math.Asin(sinToSec)
    End Function

    Private Function ConvertPriAxis(ByVal lat As Double, ByVal toSec As Double, ByVal fromPri As Double, ByVal fromSec As Double) As Double
        ' avoid dividing by zero (Math.Cos(lat)) when lat = 90 or -90
        If lat.Equals(Units.QtrRev) Then
            Return eMath.ValidRad(fromPri + Units.HalfRev)
        End If

        Dim cosToPri As Double = (Math.Sin(fromSec) - Math.Sin(lat) * Math.Sin(toSec)) / (Math.Cos(lat) * Math.Cos(toSec))
        cosToPri = eMath.BoundsSinCos(cosToPri)
        Dim toPri As Double = Math.Acos(cosToPri)
        ' heading east or west of 0 pt?
        If Math.Sin(fromPri) > 0 Then
            toPri = eMath.ReverseRad(toPri)
        End If
        Return toPri
    End Function
#End Region

End Class