Public Class FieldRotation

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public FR As Coordinate
    ' rate of field rotation in radians per second
    Public FieldRotationRateRadSec As Double
    Public ICoordXform As ICoordXform
#End Region

#Region "Private and Protected Members"
    Dim pPrevFR As Double
    Dim pFRPrevSidT As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As FieldRotation
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As FieldRotation = New FieldRotation
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
        FR = Coordinate.GetInstance
        ICoordXform = CoordXformFactory.GetInstance.Build(CoordXformType.ConvertTrig)
    End Sub

    Public Shared Function GetInstance() As FieldRotation
        Return New FieldRotation
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function CalcAngleViaTrig(ByRef position As Position, ByVal haOff As Double, ByRef equatorialLatitude As Coordinate) As Double
        Dim a As Double
        Dim sinHA As Double

        position.Dec.CheckHoldSinCos()
        equatorialLatitude.CheckHoldSinCos()

        position.HA.Rad = position.SidT.Rad - haOff - position.RA.Rad
        sinHA = Math.Sin(position.HA.Rad)
        a = (equatorialLatitude.SinRad / equatorialLatitude.CosRad) * position.Dec.CosRad - position.Dec.SinRad * Math.Cos(position.HA.Rad)
        If a < 0 Then
            FR.Rad = Math.Atan(sinHA / a) + Units.HalfRev
        ElseIf a.Equals(0.0) Then
            If sinHA < 0 Then
                FR.Rad = -Units.HalfRev
            ElseIf sinHA.Equals(0.0) Then
                FR.Rad = 0
            Else
                FR.Rad = Units.HalfRev
            End If
        Else
            FR.Rad = Math.Atan(sinHA / a)
        End If

        FR.Rad = eMath.ValidRad(FR.Rad)
        Return FR.Rad
    End Function

    ' requires altaz coords
    '
    ' note: these two CalcFieldRotationRate functions match with large hour angle offsets of the scope's zenith and very high altitude
    ' angles if latitude is found by pointing at scope's zenith as opposed to pointing at scope's equatorial pole
    ' 
    ' From: "MLThiebaux" <mlt@ns.sympatico.ca>
    ' We can think of this system as an equatorial mounting with the polar axis grossly misoriented.
    ' Say the polar axis is pointing at a fixed point t in the sky with declination t. (If Dobsonian, read azimuth axis instead of polar
    ' axis). Suppose we are tracking a star with declination s.  Let h be the hour angle of the star minus the fixed hour angle of t.
    ' h is increasing at the constant rate w = 15 deg/hr.
    ' Let a = cos(s)tan(t) and b = sin(s).
    ' Then the apparent rotation rate in the field of view of the telescope is w(b-AcosH)/[(sinH)^2 +(a-BcosH)^2)].
    ' Note that the rate is time-varying
    ' A positive rate corresponds to a counter-clockwise rotation in a 2-mirror telescope.
    Public Function CalcRateSidTrackViaFormula(ByRef position As Position, ByVal haOff As Double, ByRef equatorialLatitude As Coordinate) As Double
        Dim t As Double
        Dim s As Double
        Dim h As Double
        Dim a As Double
        Dim b As Double

        Dim tempPosition As Position = PositionArraySingleton.GetInstance.GetPosition
        tempPosition.CopyFrom(position)
        ICoordXform.Position = position
        ICoordXform.Site.Latitude.Rad = equatorialLatitude.Rad

        ' get dec of scope's zenith point (hour angle of scope's zenith point already calculated and in pHaOff)
        ICoordXform.Position.Alt.Rad = Units.QtrRev
        ICoordXform.Position.Az.Rad = 0
        ICoordXform.GetEquat()
        t = ICoordXform.Position.Dec.Rad

        ' get hour angle of target
        ICoordXform.Position.Alt.Rad = tempPosition.Alt.Rad
        ICoordXform.Position.Az.Rad = tempPosition.Az.Rad
        ICoordXform.GetEquat()
        s = ICoordXform.Position.Dec.Rad
        h = ICoordXform.Position.SidT.Rad - ICoordXform.Position.RA.Rad
        h -= haOff
        h = eMath.ValidRadPi(h)

        a = Math.Cos(s) * Math.Tan(t)
        b = Math.Sin(s)

        ' formula produces rotation rate as a ratio compared to sidereal tracking rate, ie, 1.0 = 15"/sec, 2.0 is twice sid track rate
        Dim FieldRotationRateToSidTrackRateRatio As Double = (b - a * Math.Cos(h)) / ((Math.Sin(h) * Math.Sin(h) + (a - b * Math.Cos(h)) * (a - b * Math.Cos(h))))
        ' .SecToRad is the correct conversion as 15" = 1 sec
        FieldRotationRateRadSec = -FieldRotationRateToSidTrackRateRatio * Units.SecToRad

        position.CopyFrom(tempPosition)
        tempPosition.Available = True

        FR.Rad = CalcAngleViaTrig(position, haOff, equatorialLatitude)
        Return FieldRotationRateRadSec
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' calculate the field rotation rate by dividing the field rotation angle difference obtained by calculating the 
    ''' field rotation angle just before and just after the desired time
    ''' </summary>
    ''' <param name="position"></param>
    ''' <param name="haOff"></param>
    ''' <param name="equatorialLatitude"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	3/1/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function CalcRateSidTrackViaDeltaFR(ByRef position As Position, ByVal haOff As Double, ByRef equatorialLatitude As Coordinate) As Double
        ' start 1/2 way before desired midpoint
        Dim holdSidT As Double = position.SidT.Rad
        position.SidT.Rad -= Units.SecToRad / 2
        Dim holdFRPrevSidT As Double = pFRPrevSidT

        CalcAngleViaTrig(position, haOff, equatorialLatitude)
        GetFieldRotationRate(position)

        ' 2nd time 1/2 way after desired midpoint
        position.SidT.Rad += Units.SecToRad
        CalcAngleViaTrig(position, haOff, equatorialLatitude)
        GetFieldRotationRate(position)

        position.SidT.Rad = holdSidT
        pFRPrevSidT = holdFRPrevSidT

        Return FieldRotationRateRadSec
    End Function

    ''' <summary>
    ''' calculate the field rotation rate given pre and post positions
    ''' </summary>
    ''' <param name="prePosition"></param>
    ''' <param name="postPosition"></param>
    ''' <param name="haOff"></param>
    ''' <param name="equatorialLatitude"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalcRateSidTrackViaDeltaFR(ByRef prePosition As Position, ByRef postPosition As Position, ByVal haOff As Double, ByRef equatorialLatitude As Coordinate) As Double
        Dim holdFRPrevSidT As Double = pFRPrevSidT
        CalcAngleViaTrig(prePosition, haOff, equatorialLatitude)
        GetFieldRotationRate(prePosition)
        CalcAngleViaTrig(postPosition, haOff, equatorialLatitude)
        GetFieldRotationRate(postPosition)
        pFRPrevSidT = holdFRPrevSidT
        Return FieldRotationRateRadSec
    End Function

    ''' <summary>
    ''' calculate the field rotation rate given pre and post positions
    ''' </summary>
    ''' <param name="prePosition"></param>
    ''' <param name="postPosition"></param>
    ''' <param name="haOff"></param>
    ''' <param name="equatorialLatitude"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalcRateSidTrackViaFormula(ByRef prePosition As Position, ByRef postPosition As Position, ByVal haOff As Double, ByRef equatorialLatitude As Coordinate) As Double
        Dim holdFRPrevSidT As Double = pFRPrevSidT
        CalcRateSidTrackViaFormula(prePosition, haOff, equatorialLatitude)
        GetFieldRotationRate(prePosition)
        CalcRateSidTrackViaFormula(postPosition, haOff, equatorialLatitude)
        GetFieldRotationRate(postPosition)
        pFRPrevSidT = holdFRPrevSidT
        Return FieldRotationRateRadSec
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' rate of field rotation in radians per second: calculates rate based on last two calls to CalcAngleViaTrig(),
    ''' assuming that sidT has changed between calls, rate can be very high, though the rate cannot be sustained
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	3/1/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub GetFieldRotationRate(ByRef position As Position)
        If position.SidT.Rad <> pFRPrevSidT Then
            FieldRotationRateRadSec = eMath.ValidRadPi((FR.Rad - pPrevFR) / ((position.SidT.Rad - pFRPrevSidT) / Units.SecToRad))
        Else
            FieldRotationRateRadSec = 0
        End If
        pPrevFR = FR.Rad
        pFRPrevSidT = position.SidT.Rad
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
