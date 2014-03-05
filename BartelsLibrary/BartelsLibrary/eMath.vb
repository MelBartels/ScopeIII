Public Class eMath

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

    'Public Shared Function GetInstance() As eMath
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As eMath = New eMath
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As eMath
        Return New eMath
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Shared Function RInt(ByVal num As Double) As Int32
        If num < 0 Then
            Return -CInt(Int(-num + 0.5))
        End If
        Return CInt(Int(num + 0.5))
    End Function

    Public Shared Function RInt(ByVal num As String) As Int32
        Return RInt(CDbl(num))
    End Function

    Public Shared Function ResolveNumToPrecision(ByVal num As Double, ByVal precision As Double) As Double
        Return num - Math.IEEERemainder(num, precision)
    End Function

    Public Shared Function Fractional(ByVal num As Double) As Double
        Return num - Math.Floor(num)
    End Function

    Public Shared Function Sqr(ByVal num As Double) As Double
        Return num * num
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' bring a number in radians within the bounds of 0 to 2*Pi
    ''' </summary>
    ''' <param name="rad"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/20/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ValidRad(ByVal rad As Double) As Double
        Dim thisRad As Double = rad Mod Units.OneRev
        If thisRad < 0 Then
            thisRad += Units.OneRev
        End If
        Return thisRad
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''  bring a number in radians within the bounds of 0 to 2*Pi but then adjust
    '''  the return value to be between -Pi to +Pi
    ''' </summary>
    ''' <param name="rad"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/20/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ValidRadPi(ByVal rad As Double) As Double
        Dim thisRad As Double = ValidRad(rad)
        If thisRad > Units.HalfRev Then
            thisRad -= Units.OneRev
        End If
        Return thisRad
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''  bring a number in radians within the bounds of 0 to 2*Pi but then adjust
    ''' the return value to be between -Pi/2 to +Pi/2
    ''' </summary>
    ''' <param name="rad"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	5/7/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ValidRadHalfPi(ByVal rad As Double) As Double
        Dim thisRad As Double = ValidRad(rad)
        If thisRad >= Units.ThreeFourthsRev Then
            thisRad -= Units.OneRev
        ElseIf thisRad >= Units.HalfRev Then
            thisRad = -thisRad + Units.HalfRev
        ElseIf thisRad >= Units.QtrRev Then
            thisRad = Units.HalfRev - thisRad
        End If
        Return thisRad
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Reverses a radian value, eg, 3/4 rev is converted to 1/4 rev
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	5/15/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Shared Function ReverseRad(ByVal value As Double) As Double
        Return Units.OneRev - value
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Reverses a radian value within Pi bounds, eg, 1/4 rev is converted to -1/4 rev
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	5/15/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Shared Function ReverseRadPi(ByVal value As Double) As Double
        Return ValidRadPi(Units.OneRev - value)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' reverses if conditional is true
    ''' </summary>
    ''' <param name="reverse"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	5/15/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Shared Function ReverseRad(ByVal reverse As Boolean, ByVal value As Double) As Double
        If reverse Then
            Return ReverseRad(value)
        End If
        Return value
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' reverses if conditional is true
    ''' </summary>
    ''' <param name="reverse"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	5/15/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Shared Function ReverseRadPi(ByVal reverse As Boolean, ByVal value As Double) As Double
        If reverse Then
            Return ValidRadPi(Units.OneRev - value)
        End If
        Return value
    End Function

    ''' <summary>
    ''' quadrant 1 is 0-90 
    ''' quadrant 2 is 90-180
    ''' quadrant 3 is 180-270  
    ''' quadrant 4 is 270-360 
    ''' </summary>
    ''' <param name="radian"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Quadrant(ByVal radian As Double) As Int32
        radian = ValidRad(radian)
        If radian <= Units.QtrRev Then
            Return 1
        End If
        If radian <= Units.HalfRev Then
            Return 2
        End If
        If radian <= Units.ThreeFourthsRev Then
            Return 3
        End If
        If radian <= Units.OneRev Then
            Return 4
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' bring a number within legal bounds of sine and cosine (between -1 and 1)
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/20/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function BoundsSinCos(ByVal value As Double) As Double
        Dim thisValue As Double = value
        If value > 1 Then
            thisValue = 1
        ElseIf value < -1 Then
            thisValue = -1
        End If
        Return thisValue
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' cotangent
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/20/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function Cot(ByVal value As Double) As Double
        Return 1 / Math.Tan(value)
    End Function

    Public Shared Function Randomize(ByVal lowerBound As Double, ByVal upperbound As Double) As Double
        Return (upperbound - lowerBound) * Rnd() + lowerBound
    End Function

    ' 0 deg is directly upward;
    ' returns within 0-360deg range
    Public Shared Function AngleRadFromPoints(ByRef fromPoint As Drawing.Point, ByRef toPoint As Drawing.Point) As Double
        Dim angleRad As Double = Math.Atan(Math.Abs(toPoint.X - fromPoint.X) / Math.Abs(toPoint.Y - fromPoint.Y))
        If Double.IsNaN(angleRad) Then
            angleRad = 0
        End If

        If toPoint.X >= fromPoint.X AndAlso toPoint.Y <= fromPoint.Y Then
            Return angleRad
        End If
        If toPoint.X >= fromPoint.X AndAlso toPoint.Y >= fromPoint.Y Then
            Return Units.HalfRev - angleRad
        End If
        If toPoint.X <= fromPoint.X AndAlso toPoint.Y >= fromPoint.Y Then
            Return Units.HalfRev + angleRad
        End If
        If toPoint.X <= fromPoint.X AndAlso toPoint.Y <= fromPoint.Y Then
            Return Units.OneRev - angleRad
        End If
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class