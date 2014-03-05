''' -----------------------------------------------------------------------------
''' Project	 : Refraction
''' Class	 : Refraction.Refract
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' refraction makes an object appear higher in the sky than it really is when close to the horizon;
''' if you are looking at the horizon, then you will be seeing an object that otherwise is -34.5' below horizon;
''' translate site->sky as 0 -> -34.5 and sky->site as -34.5 -> 0
''' causes tracking rate to slow down the closer the scope is to the horizon;
'''
''' interpolate:
''' 1. find points that the angle fits between
'''    ex: angle of 10 has end point of r(10,0) and beginning point of r(9,0)
''' 2. get position between end points
'''    position = (a-bp)/(ep-bp)
'''    ex: a=1, bp=2, ep=0
'''        position = (1-2)/(0-2) = .5
''' 3. scope->sky refraction = amount of refraction at beginning point +
'''                            position (amount of refract at end point - amount of refract at beg point)
'''    r = br + p*(er-br), r = br + (a-bp)/(ep-bp)*(er-br), r = br + (a-bp)*(er-br)/(ep-bp)
'''    ex: br=18, er=34.5
'''        r = 18 + .5*(34.5-18) = 26.25 arcmin
''' 4. corrected angle = angle - refraction
'''    ex: c = a-r = c = 60 arcmin - 26.25 arcmin = 33.75 arcmin
'''
''' to reverse (sky->scope): have corrected angle of c, find altitude of a;
'''    ex: c = 60 arcmin - 26.25 arcmin = 33.75 arcmin, solve for a:
''' 1. c = a - r, a = c + r, a = (c+br)(ep-bp) + (a-bp)*(er-br)/(ep-bp),
'''    a(ep-bp) = c*ep - c*bp + br*ep - br*bp + a*er - a*br - bp*er + bp*br,
'''    a*ep - a*bp - a*er + a*br = c*ep - c*bp + br*ep - br*bp - bp*er + bp*br,
'''    a*(ep-bp-er+br) = bp(-c-br-er+br) + ep(c+br),
'''    a = (bp(-c-er) + ep(c+br)) / (ep-bp-er+br),
'''    ex: using example from above, convert all units to armin...
'''        c=33.75 arcmin
'''        br=18
'''        er=34.5
'''        bp=120
'''        ep=0
'''    a = (120(-33.75-34.5) + 0) / (0-120-34.5+18),
'''    a = 120*-68.25 / -136.5,
'''    a = 60 armin
''' 
''' (if refract added to angle, eg, corrected angle = angle + refraction, ie, c=a+r,
'''  then use the following to back out the correction:
'''  to reverse: have corrected angle of ca, find altitude of a
'''     ex: ca = 1deg + 26.25arcmin = 86.25arcmin, solve for a
'''  1. ca = a + r, a = ca - r, a = ca - br - (a-bp)*(er-br)/(ep-bp),
'''     a*(ep-bp)+(a-bp)*(er-br) = (ca-br)*(ep-bp),
'''     a*ep-a*bp+a*er-a*br-bp*er+bp*br = ca*ep-ca*bp-br*ep+br*bp,
'''     a*(ep-bp+er-br) = ca*ep-ca*bp-br*ep+br*bp+bp*er-bp*br,
'''     a*(ep-bp+er-br) = ca*(ep-bp)-br*ep+bp*er,
'''     a = (ca*(ep-bp)-br*ep+bp*er) / (ep-bp+er-br),
'''     ex: convert all units to armin...
'''         a = 86.25-18-(34.5-18)(60-120)/(0-120) = 86.25-18-8.25 = 60 (from 1st line)
'''         a = (86.25*(0-120)-18*0+120*34.5)/(0-120+34.5-18) = (-10350+4140)/(-103.5) = 60)
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	2/18/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Refract

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Private Const MaxRefractIx As Int32 = 12
    Private Const maxRefractArcminToAdd As Double = 34.5
    Private Const maxRefractArcminToRemove As Double = 42.75
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Coordinate As Coordinate
#End Region

#Region "Private and Protected Members"
    Private pmaxRefractRadToAdd As Double
    Private pmaxRefractRadToRemove As Double

    ' to compute site->sky refraction, subtract interpolation of r table from altitude
    ' to compute sky->site refraction, add interpolation of r table to altitude
    Private pR(MaxRefractIx, 1) As Double

    ' work vars
    Private pAltDeg As Double
    Private pBp As Double
    Private pEp As Double
    Private pBr As Double
    Private pEr As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Refract
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Coordinate.Rad = New Refract
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        ' table of refraction per altitude angle:
        ' pR(,0) is altitude angle in degrees
        ' pR(,1) is refraction in arcminutes of corresponding altitude angles
        pR(0, 0) = 90 : pR(0, 1) = 0
        pR(1, 0) = 60 : pR(1, 1) = 0.55
        pR(2, 0) = 30 : pR(2, 1) = 1.7
        pR(3, 0) = 20 : pR(3, 1) = 2.6
        pR(4, 0) = 15 : pR(4, 1) = 3.5
        pR(5, 0) = 10 : pR(5, 1) = 5.2
        pR(6, 0) = 8 : pR(6, 1) = 6.4
        pR(7, 0) = 6 : pR(7, 1) = 8.3
        pR(8, 0) = 4 : pR(8, 1) = 11.5
        pR(9, 0) = 2 : pR(9, 1) = 18
        pR(10, 0) = 0 : pR(10, 1) = maxRefractArcminToAdd
        ' to allow for sky->scope interpolation when scope->sky results in negative elevation
        pR(11, 0) = -1 : pR(11, 1) = maxRefractArcminToRemove

        pmaxRefractRadToAdd = maxRefractArcminToAdd * Units.ArcminToRad
        pmaxRefractRadToRemove = maxRefractArcminToRemove * Units.ArcminToRad
        Coordinate = Coordinates.Coordinate.GetInstance
    End Sub

    Public Shared Function GetInstance() As Refract
        Return New Refract
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' This function calcs the refraction that lowers a given altitude.
    ''' Eg, an object that appears on the horizon is actually 34.5 arcmin below the horizon.
    ''' Refraction for the altitude of 0 is therefore computed at 34.5 arcmin.
    ''' </summary>
    ''' <param name="alt"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/18/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Calc(ByVal alt As Double) As Coordinate
        If (alt >= Units.QtrRev) Then
            Coordinate.Rad = 0
            Return Coordinate
        End If

        setWorkVars(alt)
        Dim refractArcminToAdd As Double = pBr + (pAltDeg - pBp) * (pEr - pBr) / (pEp - pBp)
        ' table gives values in arcmin, so convert to radians
        Coordinate.Rad = refractArcminToAdd * Units.ArcminToRad
        If Coordinate.Rad > pmaxRefractRadToAdd Then
            Coordinate.Rad = pmaxRefractRadToAdd
        End If
        Return Coordinate
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' This function calcs the refraction that increases a given altitude.
    ''' Eg, an altitude of 34.5 arcmin below the horizon actually appears on the horizon.
    ''' Refraction for the altitude of -34.5 arcmin is therefore computed at 34.5 arcmin.
    ''' </summary>
    ''' <param name="alt"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/18/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function CalcRefractionToBackOut(ByVal alt As Double) As Coordinate
        If (alt >= Units.QtrRev) Then
            Coordinate.Rad = 0
            Return Coordinate
        End If

        setWorkVars(alt)
        ' convert deg to arcmin
        pAltDeg *= 60
        pBp *= 60
        pEp *= 60
        ' 'pA' = corrected altitude
        Dim a1 As Double = (pBp * (-pAltDeg - pEr) + pEp * (pAltDeg + pBr)) / (pEp - pBp - pEr + pBr)
        ' table gives values in arcmin, so convert to radians
        Coordinate.Rad = a1 * Units.ArcminToRad - alt
        If Coordinate.Rad > pmaxRefractRadToRemove Then
            Coordinate.Rad = pmaxRefractRadToRemove
        End If
        Return Coordinate
    End Function
#End Region

#Region "Private and Protected Methods"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Utility function called by CalcRefract...() functions.
    ''' </summary>
    ''' <param name="alt"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/18/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub setWorkVars(ByVal alt As Double)
        ' alt is in radians; convert to degrees for use with refraction table
        pAltDeg = alt * Units.RadToDeg
        'Debug.WriteLine("refraction altitude " & pAltDeg & "d")
        Dim ix As Int32
        For ix = 0 To MaxRefractIx - 1
            If pAltDeg > pR(ix, 0) Then
                Exit For
            End If
        Next
        pBp = pR(ix - 1, 0)
        pEp = pR(ix, 0)
        pBr = pR(ix - 1, 1)
        pEr = pR(ix, 1)
    End Sub
#End Region

End Class