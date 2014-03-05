' add project reference to System.Runtime.Serialization.Formatters.Soap.dll

Public Class CoordinateParser

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    ' space, colon, radians, degrees, hours, minutes, seconds, LX200 degree symbol
    Public Delimiters As String = " :rdhms°'""" + CoordExpBase.LX200DegSym
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

    'Public Shared Function GetInstance() As CoordinateParser
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordinateParser = New CoordinateParser
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CoordinateParser
        Return New CoordinateParser
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' variants:
    ''' degree, eg, 123.456
    ''' datafile HMS, eg, 05 14 45
    ''' datafile DMS, eg, -08 11 48
    ''' radians, eg, 1.23456
    ''' DMS, eg, -08:11:48
    ''' HMS, eg, 05:14:45
    ''' HMSM, eg, 05:06:17.045
    ''' LX200 signed long deg, eg, "-10^05:02#" (^ stands in for the LX200 degree symbol)
    ''' LX200 signed short deg, eg, "-10^05.5#" (^ stands in for the LX200 degree symbol)
    ''' LX200 long deg, eg, "010^05:02#" (^ stands in for the LX200 degree symbol)
    ''' LX200 short deg, eg, "010^05.5#" (^ stands in for the LX200 degree symbol)
    ''' LX200 long hr, eg, "05:06:17#" 
    ''' LX200 short hr, eg, "05:06#" 
    ''' RA XML, eg, <RightAscension><Sign>+</Sign><Hours>5</Hours><Minutes>6</Minutes><Seconds>16.9999999999989</Seconds></RightAscension>
    ''' Dec XML, eg, <Declination><Sign>+</Sign><Degrees>10</Degrees><Minutes>5</Minutes><Seconds>2</Seconds></Declination>
    ''' GoogleSky RA 00h42m44.30s 
    ''' GoogleSky Dec +41°16'10.0"
    ''' 
    ''' DMS can also be in form of 1[d] 2[m] 3[s] 
    ''' where [d] is optional: d, deg, degree, degrees
    ''' where [m] is optional: m, min, mins, minute, minutes
    ''' where [s] is optional: s, sec, secs, second, seconds
    ''' eg, 1d 2m 3s, or 1 deg 2 min 3 sec
    ''' therefore delimiter list: ' ', ':', 'd', 'm', 's'
    ''' DMS additionally can be in the form of +1°2'3.0"
    ''' therefore add to delimited list: °'"
    ''' 
    ''' HMS can also be in form of 1[h] 2[m] 3[s] 
    ''' where [h] is optional: h, hr, hrs, hour, hours
    ''' where [m] is optional: m, min, mins, minute, minutes
    ''' where [s] is optional: s, sec, secs, second, seconds
    ''' eg, 1h 2m 3s, or 1 hr 2 min 3 sec
    ''' therefore delimiter list: ' ', ':', 'h', 'm', 's'
    ''' 
    ''' ambiguities:
    ''' degrees and radians if delimiter is 'r'
    ''' DMS and HMS where delimiter is ' ', or ':'
    ''' 
    ''' conclusion:
    ''' if at least 'h' present, then decode to HMS ('h' can be anywhere in the string, even at the end)
    ''' if at least 'd' present, or more than 1 number, then decode to DMS 
    ''' if at least 'ra' present, then decode to radians
    ''' else default to decoding to degrees
    ''' 
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	4/18/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Parse(ByVal str As String) As Double
        Dim st As StringTokenizer = StringTokenizer.GetInstance

        ' try in this order:

        ' hours
        If str.ToLower.IndexOf("h") > -1 Then
            st.Tokenize(CorrectCountOfNumbers(str), Delimiters.ToCharArray)
            Return CommonCalcs.GetInstance.ParseCoordinateValueRA(st)
        End If

        ' radians
        If str.ToLower.IndexOf("ra") > -1 Then
            st.Tokenize(str, Delimiters.ToCharArray)
            Return CommonCalcs.GetInstance.ParseCoordinateValueRad(st)
        End If

        ' default to degrees
        st.Tokenize(CorrectCountOfNumbers(str), Delimiters.ToCharArray)
        Return CommonCalcs.GetInstance.ParseCoordinateValueDec(st)
        
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function CorrectCountOfNumbers(ByVal str As String) As String
        Dim st As StringTokenizer = StringTokenizer.GetInstance
        st.Tokenize(str, Delimiters.ToCharArray)
        Dim countDoubles As Int32 = st.GetCountDoubles
        For ix As Int32 = 1 To 3 - countDoubles
            str += " 0"
        Next
        Return str
    End Function
#End Region

End Class
