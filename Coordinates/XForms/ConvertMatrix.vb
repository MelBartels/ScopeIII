''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.ConvertMatrix
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Coordinate translation routines using Taki's matrix method.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	3/8/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class ConvertMatrix
    Inherits CoordXformBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const NumberOfInits As Int32 = 3
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public One As Position
    Public Two As Position
    Public Three As Position

    Public FabErrors As FabErrors

    Public ConvertSubrSelect As ISFT
#End Region

#Region "Private and Protected Members"
    ' storage arrays for matrix multiplication
    ' in VB, assigns 0..3 for total of 4
    Dim pQQ(3, 3) As Double
    Dim pVV(3, 3) As Double
    Dim pRR(3, 3) As Double
    Dim pXX(3, 3) As Double
    Dim pYY(3, 3) As Double

    ' working vars
    Dim pF As Double
    Dim pH As Double
    Dim pW As Double

    ' count of iterations needed in subrT()
    Dim pSubrTCount As Int32
    ' count of iterations needed in subrL()
    Dim pSubrLCount As Int32

#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ConvertMatrix
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ConvertMatrix = New ConvertMatrix
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        One = PositionArraySingleton.GetInstance.GetPosition("One")
        Two = PositionArraySingleton.GetInstance.GetPosition("Two")
        Three = PositionArraySingleton.GetInstance.GetPosition("Three")

        FabErrors = Coordinates.FabErrors.GetInstance

        ConvertSubrSelect = CType(ConvertSubrType.BellTaki, ISFT)

        pXposition.Dec.CheckHoldSinCos()
    End Sub

    Public Shared Function GetInstance() As ConvertMatrix
        Return New ConvertMatrix
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function GetAltaz() As Boolean
        Dim i As Int32
        Dim j As Int32
        Dim b As Double

        pXposition.Dec.CheckHoldSinCos()

        ' b is CCW so this HA formula is written backwards
        b = pXposition.RA.Rad - pXposition.SidT.Rad
        ' convert to rectangular coordinates and put in pXX
        pXX(1, 1) = pXposition.Dec.CosRad * Math.Cos(b)
        pXX(2, 1) = pXposition.Dec.CosRad * Math.Sin(b)
        pXX(3, 1) = pXposition.Dec.SinRad
        pYY(1, 1) = 0
        pYY(2, 1) = 0
        pYY(3, 1) = 0
        ' mutiply pXX by transform matrix pRR to get equatorial rectangular coordinates
        For i = 1 To 3
            For j = 1 To 3
                pYY(i, 1) += (pRR(i, j) * pXX(j, 1))
            Next
        Next
        ' convert to celestial coordinates
        angleSubr()
        ' modify for non-zero Z1Z2Z3 mount error values
        subrSwitcher()
        angleSubr()
        pXposition.Alt.Rad = pH
        ' convert azimuth from CCW to CW
        pXposition.Az.Rad = eMath.ReverseRad(eMath.ValidRad(pF))
        ' if flipped, then restore 'true' or actual altaz coordinates since input equat and the subsequent
        ' coordinate translation is not aware of meridian flip and always results in not flipped coordinate values
        TranslateAltazAcrossPoleBasedOnMeridianFlip()
        ' adjust altitude: this should occur after meridian flip adjustment - see notes in Mounting.MeridianFlip
        pXposition.Alt.Rad -= FabErrors.Z3.Rad
    End Function

    Public Overrides Function GetEquat() As Boolean
        Dim i As Int32
        Dim j As Int32
        Dim holdAlt As Double
        Dim holdAz As Double

        holdAlt = pXposition.Alt.Rad
        holdAz = pXposition.Az.Rad
        pXposition.Alt.Rad += FabErrors.Z3.Rad
        ' return to equivalent not flipped altaz values for purposes of coordinate translation
        TranslateAltazAcrossPoleBasedOnMeridianFlip()
        pH = pXposition.Alt.Rad
        ' convert from CW to CCW az
        pF = eMath.ReverseRad(pXposition.Az.Rad)
        pXposition.Alt.Rad = holdAlt
        pXposition.Az.Rad = holdAz

        subrA()
        pXX(1, 1) = pYY(1, 0)
        pXX(2, 1) = pYY(2, 0)
        pXX(3, 1) = pYY(3, 0)
        pYY(1, 1) = 0
        pYY(2, 1) = 0
        pYY(3, 1) = 0
        For i = 1 To 3
            For j = 1 To 3
                pYY(i, 1) += (pQQ(i, j) * pXX(j, 1))
            Next
        Next
        angleSubr()
        pF += pXposition.SidT.Rad
        pXposition.RA.Rad = eMath.ValidRad(pF)
        pXposition.Dec.Rad = pH
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' to use, put values to init into current, then call initMatrix(x) with x = desired init
    ''' function performs all possible inits from the beginning: for example, need only call initMatrix(1) once 
    ''' to also init two and three
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	2/22/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub InitMatrix(ByVal init As Int32)

        Dim tempPosition As Position = PositionArraySingleton.GetInstance.GetPosition

        If init.Equals(3) AndAlso One.Init AndAlso Two.Init Then
            tempPosition.CopyCoordsFrom(pXposition)
            pXposition.CopyCoordsFrom(One)
            arrayAssignInit(1)
            pXposition.CopyCoordsFrom(Two)
            arrayAssignInit(2)
            pXposition.CopyCoordsFrom(tempPosition)
            arrayAssignInit(3)
            transformMatrix()
            Three.CopyCoordsFrom(pXposition)
            Three.Init = True
        ElseIf init.Equals(2) AndAlso One.Init AndAlso Two.Init AndAlso Three.Init Then
            tempPosition.CopyCoordsFrom(pXposition)
            pXposition.CopyCoordsFrom(One)
            arrayAssignInit(1)
            pXposition.CopyCoordsFrom(tempPosition)
            arrayAssignInit(2)
            tempPosition.CopyCoordsFrom(pXposition)
            pXposition.CopyCoordsFrom(Three)
            arrayAssignInit(3)
            pXposition.CopyCoordsFrom(tempPosition)
            transformMatrix()
            Two.CopyCoordsFrom(pXposition)
            Two.Init = True
        ElseIf init.Equals(2) AndAlso One.Init AndAlso Not Three.Init Then
            tempPosition.CopyCoordsFrom(pXposition)
            pXposition.CopyCoordsFrom(One)
            arrayAssignInit(1)
            pXposition.CopyCoordsFrom(tempPosition)
            arrayAssignInit(2)
            generateThirdInit()
            transformMatrix()
            Two.CopyCoordsFrom(pXposition)
            Two.Init = True
        ElseIf init.Equals(1) AndAlso One.Init AndAlso Two.Init AndAlso Three.Init Then
            arrayAssignInit(1)
            tempPosition.CopyCoordsFrom(pXposition)
            pXposition.CopyCoordsFrom(Two)
            arrayAssignInit(2)
            pXposition.CopyCoordsFrom(Three)
            arrayAssignInit(3)
            pXposition.CopyCoordsFrom(tempPosition)
            transformMatrix()
            One.CopyCoordsFrom(pXposition)
            One.Init = True
        ElseIf init.Equals(1) AndAlso Two.Init AndAlso Not Three.Init Then
            arrayAssignInit(1)
            tempPosition.CopyCoordsFrom(pXposition)
            pXposition.CopyCoordsFrom(Two)
            arrayAssignInit(2)
            pXposition.CopyCoordsFrom(tempPosition)
            generateThirdInit()
            transformMatrix()
            One.CopyCoordsFrom(pXposition)
            One.Init = True
        ElseIf init.Equals(1) AndAlso Not Two.Init Then
            arrayAssignInit(1)
            One.CopyCoordsFrom(pXposition)
            One.Init = True
        Else
            DebugTrace.WriteLine("initMatrix() failure: init=" _
            & init _
            & ", one.init=" _
            & One.Init _
            & ", two.init=" _
            & Two.Init _
            & ", three.init=" _
            & Three.Init)
        End If

        tempPosition.Available = True
    End Sub

    ' recover ra,dec,az,alt,sidt from the transform matrices;
    ' use to deduce the autogenerated 3rd init
    Public Function RecoverInit(ByVal init As Int32) As Position
        If init < 1 OrElse init > 3 Then
            Return Nothing
        End If

        Dim position As Position = PositionArray.GetInstance.GetPosition

        position.Dec.Rad = Math.Asin(pXX(3, init))

        Select Case init
            Case 1
                If One.Init Then
                    position.SidT.Rad = One.SidT.Rad
                End If
            Case 2
                If Two.Init Then
                    position.SidT.Rad = Two.SidT.Rad
                End If
            Case 3
                If Three.Init Then
                    position.SidT.Rad = Three.SidT.Rad
                End If
        End Select

        Dim equatAcos As Double = Math.Acos(pXX(1, init) / Math.Cos(position.Dec.Rad))
        Dim equatAsin As Double = Math.Asin(pXX(2, init) / Math.Cos(position.Dec.Rad))
        Dim equatHA As Double
        If equatAsin < 0 Then
            If equatAsin = -equatAcos Then
                equatHA = equatAsin
            Else
                equatHA = -equatAcos
            End If
        Else
            equatHA = equatAcos
        End If
        position.RA.Rad = eMath.ValidRad(equatHA + position.SidT.Rad)

        position.Alt.Rad = Math.Asin(pYY(3, init))

        Dim altazAcos As Double = Math.Acos(pYY(1, init) / Math.Cos(position.Alt.Rad))
        Dim altazAsin As Double = Math.Asin(pYY(2, init) / Math.Cos(position.Alt.Rad))
        Dim altazHA As Double
        If altazAsin < 0 Then
            If altazAsin = -altazAcos Then
                altazHA = altazAsin
            Else
                altazHA = -altazAcos
            End If
        Else
            altazHA = altazAcos
        End If
        position.Az.Rad = eMath.ValidRad(eMath.ReverseRad(altazHA))

        Return position
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub zeroArrays()
        Dim i As Int32
        Dim j As Int32
        For i = 0 To 3
            For j = 0 To 3
                pQQ(i, j) = 0
                pVV(i, j) = 0
                pRR(i, j) = 0
                pXX(i, j) = 0
                pYY(i, j) = 0
            Next
        Next
    End Sub

    Private Sub arrayAssignInit(ByVal init As Int32)
        Dim b As Double

        If init.Equals(1) Then
            zeroArrays()
        End If

        pXposition.Dec.CheckHoldSinCos()

        ' b is CCW so ha formula backwards
        b = pXposition.RA.Rad - pXposition.SidT.Rad
        ' pXX is telescope matrix convert parameters into rectangular (cartesian) coordinates
        pXX(1, init) = pXposition.Dec.CosRad * Math.Cos(b)
        pXX(2, init) = pXposition.Dec.CosRad * Math.Sin(b)
        pXX(3, init) = pXposition.Dec.SinRad
        ' pF is CCW
        pF = eMath.ReverseRad(pXposition.Az.Rad)
        pH = pXposition.Alt.Rad + FabErrors.Z3.Rad
        subrA()
        ' pYY is celestial matrix convert parameters into rectangular (cartesian) coordinates
        pYY(1, init) = pYY(1, 0)
        pYY(2, init) = pYY(2, 0)
        pYY(3, init) = pYY(3, 0)
    End Sub

    Private Sub generateThirdInit()
        Dim i As Int32
        Dim a As Double

        ' generate 3rd initialization point from the first two using vector product formula
        pXX(1, 3) = pXX(2, 1) * pXX(3, 2) - pXX(3, 1) * pXX(2, 2)
        pXX(2, 3) = pXX(3, 1) * pXX(1, 2) - pXX(1, 1) * pXX(3, 2)
        pXX(3, 3) = pXX(1, 1) * pXX(2, 2) - pXX(2, 1) * pXX(1, 2)
        a = Math.Sqrt(pXX(1, 3) * pXX(1, 3) + pXX(2, 3) * pXX(2, 3) + pXX(3, 3) * pXX(3, 3))
        For i = 1 To 3
            If a.Equals(0.0) Then
                pXX(i, 3) = Double.MaxValue
            Else
                pXX(i, 3) /= a
            End If
        Next

        pYY(1, 3) = pYY(2, 1) * pYY(3, 2) - pYY(3, 1) * pYY(2, 2)
        pYY(2, 3) = pYY(3, 1) * pYY(1, 2) - pYY(1, 1) * pYY(3, 2)
        pYY(3, 3) = pYY(1, 1) * pYY(2, 2) - pYY(2, 1) * pYY(1, 2)
        a = Math.Sqrt(pYY(1, 3) * pYY(1, 3) + pYY(2, 3) * pYY(2, 3) + pYY(3, 3) * pYY(3, 3))
        For i = 1 To 3
            If a.Equals(0.0) Then
                pYY(i, 3) = Double.MaxValue
            Else
                pYY(i, 3) /= a
            End If
        Next
    End Sub

    Private Sub transformMatrix()
        Dim i As Int32
        Dim j As Int32
        Dim l As Int32
        Dim m As Int32
        Dim n As Int32

        Dim e As Double

        For i = 1 To 3
            For j = 1 To 3
                pVV(i, j) = pXX(i, j)
            Next
        Next

        ' get determinate from copied into array pVV
        determinateSubr()
        ' save it
        e = pW

        For m = 1 To 3
            For i = 1 To 3
                For j = 1 To 3
                    pVV(i, j) = pXX(i, j)
                Next
            Next
            For n = 1 To 3
                pVV(1, m) = 0
                pVV(2, m) = 0
                pVV(3, m) = 0
                pVV(n, m) = 1
                determinateSubr()
                If e.Equals(0.0) Then
                    pQQ(m, n) = Double.MaxValue
                Else
                    pQQ(m, n) = pW / e
                End If
            Next
        Next

        For i = 1 To 3
            For j = 1 To 3
                pRR(i, j) = 0
            Next
        Next

        For i = 1 To 3
            For j = 1 To 3
                For l = 1 To 3
                    pRR(i, j) += (pYY(i, l) * pQQ(l, j))
                Next
            Next
        Next

        For m = 1 To 3
            For i = 1 To 3
                For j = 1 To 3
                    pVV(i, j) = pRR(i, j)
                Next
            Next
            determinateSubr()
            e = pW
            For n = 1 To 3
                pVV(1, m) = 0
                pVV(2, m) = 0
                pVV(3, m) = 0
                pVV(n, m) = 1
                determinateSubr()
                If e.Equals(0.0) Then
                    pQQ(m, n) = Double.MaxValue
                Else
                    pQQ(m, n) = pW / e
                End If
            Next
        Next
    End Sub

    Private Sub subrA()
        Dim cosF As Double
        Dim cosH As Double
        Dim cosz1 As Double
        Dim cosz2 As Double
        Dim sinF As Double
        Dim sinH As Double
        Dim sinz1 As Double
        Dim sinz2 As Double

        cosF = Math.Cos(pF)
        cosH = Math.Cos(pH)
        sinF = Math.Sin(pF)
        sinH = Math.Sin(pH)

        If FabErrors.Z12NonZero = True Then
            cosz1 = Math.Cos(FabErrors.Z1.Rad)
            cosz2 = Math.Cos(FabErrors.Z2.Rad)
            sinz1 = Math.Sin(FabErrors.Z1.Rad)
            sinz2 = Math.Sin(FabErrors.Z2.Rad)
            pYY(1, 0) = cosF * cosH * cosz2 - sinF * cosz1 * sinz2 + sinF * sinH * sinz1 * cosz2
            pYY(2, 0) = sinF * cosH * cosz2 + cosF * sinz2 * cosz1 - cosF * sinH * sinz1 * cosz2
            pYY(3, 0) = sinH * cosz1 * cosz2 + sinz1 * sinz2
        Else
            pYY(1, 0) = cosF * cosH
            pYY(2, 0) = sinF * cosH
            pYY(3, 0) = sinH
        End If
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' per Taki's eq 5.3-4
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/22/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub subrS(ByVal cosF As Double, ByVal cosH As Double, ByVal sinF As Double, ByVal sinH As Double)
        pYY(1, 1) = cosH * cosF + FabErrors.Z2.Rad * sinF - FabErrors.Z1.Rad * sinH * sinF
        pYY(2, 1) = cosH * sinF - FabErrors.Z2.Rad * cosF - FabErrors.Z1.Rad * sinH * cosF
        pYY(3, 1) = sinH
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' per Taki's eq 5.3-2
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/22/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub subrB(ByVal cosF As Double, ByVal cosH As Double, ByVal sinF As Double, ByVal sinH As Double, ByVal cosz1 As Double, ByVal cosz2 As Double, ByVal sinz1 As Double, ByVal sinz2 As Double)
        pYY(1, 1) = (cosH * cosF + sinF * cosz1 * sinz2 - sinH * sinF * sinz1 * cosz2) / cosz2
        pYY(2, 1) = (cosH * sinF - cosF * cosz1 * sinz2 + sinH * cosF * sinz1 * cosz2) / cosz2
        pYY(3, 1) = (sinH - sinz1 * sinz2) / (cosz1 * cosz2)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' per Taki's eq 5.3-5/6 (Taki says 2 loops sufficient for z errors of 1 deg),
    ''' FabErrors.Z1.rad=1, FabErrors.Z2.rad=-1, faberrors.z3=1, alt/az=88/100 loops needed 6 FabErrors.Z1.rad=2, FabErrors.Z2.rad=-2, faberrors.z3=0, alt/az=90/100 loops needed 22
    ''' will not converge if .dec or .alt = 90 deg and FabErrors.Z1.rad2 non-zero and equat init adopted (could be because of poor initial guess by subrB())
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/22/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub subrT(ByVal cosF As Double, ByVal cosH As Double, ByVal sinF As Double, ByVal sinH As Double, ByVal cosz1 As Double, ByVal cosz2 As Double, ByVal sinz1 As Double, ByVal sinz2 As Double)
        Dim cosF1 As Double
        Dim sinF1 As Double
        Const MaxLoopCount As Int32 = 25
        Dim last As AZdouble = AZdouble.GetInstance
        Dim err As AZdouble = AZdouble.GetInstance
        Dim holdF As Double = pF
        Dim holdH As Double = pH

        ' so as to not make the err. = invalid later
        last.A = Double.MaxValue / 2
        last.Z = Double.MaxValue / 2
        pSubrTCount = 0

        ' start with best guess using Taki's 'subroutine b'
        subrB(cosF, cosH, sinF, sinH, cosz1, cosz2, sinz1, sinz2)
        Do
            angleSubr()

            err.A = Math.Abs(last.A - pH)
            err.Z = Math.Abs(last.Z - pF)

            'DebugTrace.Writeline(pH * Units.RadToDeg _
            '& "   " _
            '& pF * Units.RadToDeg _
            '& "   " _
            '& err.A * Units.RadToArcmin _
            '& "   " _
            '& err.Z * Units.RadToArcmin)

            last.A = pH
            last.Z = pF

            cosF1 = Math.Cos(pF)
            sinF1 = Math.Sin(pF)

            pYY(1, 1) = (cosH * cosF + sinF1 * cosz1 * sinz2 - (sinH - sinz1 * sinz2) * sinF1 * sinz1 / cosz1) / cosz2
            pYY(2, 1) = (cosH * sinF - cosF1 * cosz1 * sinz2 + (sinH - sinz1 * sinz2) * cosF1 * sinz1 / cosz1) / cosz2
            pYY(3, 1) = (sinH - sinz1 * sinz2) / (cosz1 * cosz2)

            pSubrTCount += 1
            If pSubrTCount > MaxLoopCount Then
                'DebugTrace.WriteLine("switching from subrT() to subrL()...")
                pF = holdF
                pH = holdH
                subrL(cosF, cosH, sinF, sinH, cosz1, cosz2, sinz1, sinz2)
            End If
        Loop While err.A > Units.TenthsArcsecToRad OrElse err.Z > Units.TenthsArcsecToRad

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' use apparent alt derivation from Larry Bell, apparent az from Taki's iterative solution
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/22/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub subrU(ByVal cosF As Double, ByVal cosH As Double, ByVal sinF As Double, ByVal sinH As Double, ByVal cosz1 As Double, ByVal cosz2 As Double, ByVal sinz1 As Double, ByVal sinz2 As Double)
        Dim apparentAlt As Double

        apparentAlt = getApparentAlt(cosz1, cosz2, sinz1, sinz2)

        subrT(cosF, cosH, sinF, sinH, cosz1, cosz2, sinz1, sinz2)
        angleSubr()

        cosH = Math.Cos(apparentAlt)
        sinH = Math.Sin(apparentAlt)
        cosF = Math.Cos(pF)
        sinF = Math.Sin(pF)

        pYY(1, 1) = cosF * cosH
        pYY(2, 1) = sinF * cosH
        pYY(3, 1) = sinH
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' per Larry Bell's derivation
    ''' FabErrors.Z1.rad rotation done between alt and az rotations so no closed algebraic solution, instead, search iteratively
    ''' 'pH' is alt, 'pF' is az
    ''' apparent coordinates are what the encoders see, and are our goal
    ''' </summary>
    ''' <param name="cosz1"></param>
    ''' <param name="cosz2"></param>
    ''' <param name="sinz1"></param>
    ''' <param name="sinz2"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/22/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function getApparentAlt(ByVal cosz1 As Double, ByVal cosz2 As Double, ByVal sinz1 As Double, ByVal sinz2 As Double) As Double
        Dim v1 As Double

        v1 = (Math.Sin(pH) - sinz1 * sinz2) * cosz1 * (cosz2 / ((sinz1 * sinz1 - 1) * (sinz2 * sinz2 - 1)))
        v1 = eMath.BoundsSinCos(v1)
        Return Math.Asin(v1)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' from Larry Bell's derivation of iterative solution to FabErrors.Z1.rad, FabErrors.Z2.rad
    ''' </summary>
    ''' <param name="cosF"></param>
    ''' <param name="cosH"></param>
    ''' <param name="sinF"></param>
    ''' <param name="sinH"></param>
    ''' <param name="cosz1"></param>
    ''' <param name="cosz2"></param>
    ''' <param name="sinz1"></param>
    ''' <param name="sinz2"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/22/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub subrL(ByVal cosF As Double, ByVal cosH As Double, ByVal sinF As Double, ByVal sinH As Double, ByVal cosz1 As Double, ByVal cosz2 As Double, ByVal sinz1 As Double, ByVal sinz2 As Double)
        Dim trueAz As Double
        Dim tanTrueAz As Double
        Dim apparentAlt As Double
        Dim apparentAz As Double
        Dim bestApparentAz As Double
        Dim ee As Double
        Dim ff As Double
        Dim gg As Double
        Dim hh As Double
        Dim goalSeek As Double
        Dim holdGoalSeek As Double
        Dim incr As Double
        Dim minIncr As Double
        Dim dir As Boolean

        trueAz = pF
        tanTrueAz = Math.Tan(trueAz)

        apparentAlt = getApparentAlt(cosz1, cosz2, sinz1, sinz2)

        ee = Math.Cos(apparentAlt)
        ff = Math.Sin(apparentAlt)
        gg = cosz2 * sinz1 * ff * tanTrueAz - tanTrueAz * sinz2 * cosz1 - cosz2 * ee
        hh = sinz2 * cosz1 - cosz2 * sinz1 * ff - tanTrueAz * cosz2 * ee

        ' start with best guess using Taki's 'subroutine b' for apparentAz
        subrB(cosF, cosH, sinF, sinH, cosz1, cosz2, sinz1, sinz2)
        angleSubr()
        apparentAz = pF

        ' iteratively solve for best apparent azimuth by searching for a goal of 0 for goalSeek
        bestApparentAz = apparentAz
        holdGoalSeek = Double.MaxValue
        ' change long standing incr = Units.ArcsecToRad * 2 to a much larger value for speed 
        'incr = Units.ArcsecToRad * 2
        incr = Units.ArcminToDeg
        minIncr = Units.ArcsecToRad
        dir = True
        pSubrLCount = 0
        Do
            If dir = True Then
                apparentAz += incr
            Else
                apparentAz -= incr
            End If

            goalSeek = gg * Math.Sin(apparentAz) - hh * Math.Cos(apparentAz)

            'DebugTrace.Writeline("goalSeek " _
            '& goalSeek * 1000000 _
            '& " dir " _
            '& dir)

            If Math.Abs(goalSeek) <= Math.Abs(holdGoalSeek) Then
                bestApparentAz = apparentAz
                ' DebugTrace.Writeline("bestApparentAz " + bestApparentAz*units.RadToDeg)
            Else
                ' GoakSeek getting worse, so reverse direction and cut increment by half
                incr /= 2
                dir = Not dir
            End If
            holdGoalSeek = goalSeek
            pSubrLCount += 1
        Loop While incr >= minIncr

        cosF = Math.Cos(bestApparentAz)
        sinF = Math.Sin(bestApparentAz)
        cosH = Math.Cos(apparentAlt)
        sinH = Math.Sin(apparentAlt)

        pYY(1, 1) = cosF * cosH
        pYY(2, 1) = sinF * cosH
        pYY(3, 1) = sinH
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 'pH' is alt, 'pF' is az
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/22/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub subrSwitcher()
        Dim cosF As Double
        Dim cosH As Double
        Dim sinF As Double
        Dim sinH As Double
        Dim cosz1 As Double
        Dim cosz2 As Double
        Dim sinz1 As Double
        Dim sinz2 As Double

        cosF = Math.Cos(pF)
        cosH = Math.Cos(pH)
        sinF = Math.Sin(pF)
        sinH = Math.Sin(pH)

        If FabErrors.Z12NonZero Then
            cosz1 = Math.Cos(FabErrors.Z1.Rad)
            cosz2 = Math.Cos(FabErrors.Z2.Rad)
            sinz1 = Math.Sin(FabErrors.Z1.Rad)
            sinz2 = Math.Sin(FabErrors.Z2.Rad)
            If ConvertSubrSelect Is ConvertSubrType.TakiSimple Then
                subrS(cosF, cosH, sinF, sinH)
            ElseIf ConvertSubrSelect Is ConvertSubrType.TakiSmallAngle Then
                subrB(cosF, cosH, sinF, sinH, cosz1, cosz2, sinz1, sinz2)
            ElseIf ConvertSubrSelect Is ConvertSubrType.BellIterative Then
                subrL(cosF, cosH, sinF, sinH, cosz1, cosz2, sinz1, sinz2)
            ElseIf ConvertSubrSelect Is ConvertSubrType.TakiIterative Then
                subrT(cosF, cosH, sinF, sinH, cosz1, cosz2, sinz1, sinz2)
            ElseIf ConvertSubrSelect Is ConvertSubrType.BellTaki Then
                subrU(cosF, cosH, sinF, sinH, cosz1, cosz2, sinz1, sinz2)
            End If
        Else
            pYY(1, 1) = cosF * cosH
            pYY(2, 1) = sinF * cosH
            pYY(3, 1) = sinH
        End If
    End Sub

    Private Sub determinateSubr()
        pW = pVV(1, 1) * pVV(2, 2) * pVV(3, 3) + pVV(1, 2) * pVV(2, 3) * pVV(3, 1) _
           + pVV(1, 3) * pVV(3, 2) * pVV(2, 1) - pVV(1, 3) * pVV(2, 2) * pVV(3, 1) _
           - pVV(1, 1) * pVV(3, 2) * pVV(2, 3) - pVV(1, 2) * pVV(2, 1) * pVV(3, 3)
    End Sub

    Private Sub angleSubr()
        Dim c As Double

        c = Math.Sqrt(pYY(1, 1) * pYY(1, 1) + pYY(2, 1) * pYY(2, 1))

        If c.Equals(0.0) AndAlso pYY(3, 1) > 0 Then
            pH = Units.QtrRev
        ElseIf c.Equals(0.0) AndAlso pYY(3, 1) < 0 Then
            pH = -Units.QtrRev
        ElseIf Not c.Equals(0.0) Then
            pH = Math.Atan(pYY(3, 1) / c)
        Else
            DebugTrace.WriteLine("undetermined pH in convertMatrix.angleSubr()")
            pH = 0
        End If

        If c.Equals(0.0) Then
            ' pF should be indeterminate: Taki program listing is pF = 1000 degrees (maybe to note this situation?)
            DebugTrace.WriteLine("undetermined pF in convertMatrix.angleSubr()")
            pF = 0
        ElseIf Not c.Equals(0.0) AndAlso pYY(1, 1).Equals(0.0) AndAlso pYY(2, 1) > 0 Then
            pF = Units.QtrRev
        ElseIf Not c.Equals(0.0) AndAlso pYY(1, 1).Equals(0.0) AndAlso pYY(2, 1) < 0 Then
            pF = eMath.ReverseRad(Units.QtrRev)
        ElseIf (pYY(1, 1) > 0) Then
            pF = Math.Atan(pYY(2, 1) / pYY(1, 1))
        ElseIf (pYY(1, 1) < 0) Then
            pF = Math.Atan(pYY(2, 1) / pYY(1, 1)) + Units.HalfRev
        Else
            DebugTrace.WriteLine("undetermined pF in convertMatrix.angleSubr()")
            pF = 0
        End If

        pF = eMath.ValidRad(pF)
    End Sub
#End Region

End Class