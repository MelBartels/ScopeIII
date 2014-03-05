''' -----------------------------------------------------------------------------
''' Project	 : Common
''' Class	 : Common.StringTokenizer
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Utility or library class.
''' Splits a string into tokens, skipping over empty string tokens.
''' Takes optional list of char delimiters.
''' Includes a PeekNextToken() to return next token without advancing the token marker.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	3/30/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class StringTokenizer

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Str As String
    Public Tokens As String()
    Public Count As Int32
#End Region

#Region "Private and Protected Members"
    Dim pIndex As Int32
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As StringTokenizer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As StringTokenizer = New StringTokenizer
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As StringTokenizer
        Return New StringTokenizer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Tokenize(ByVal str As String) As Boolean
        Me.Str = str
        pIndex = 0
        Tokens = str.Split
        Count = GetCount()
    End Function

    Public Function Tokenize(ByVal str As String, ByVal delimiters As Char()) As Boolean
        Me.Str = str
        pIndex = 0
        Tokens = str.Split(delimiters)
        Count = GetCount()
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return the next non-zero length string.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	3/30/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function NextToken() As String
        Dim str As String = String.Empty

        While str.Length = 0 AndAlso pIndex < Tokens.Length
            str = Tokens(pIndex)
            pIndex += 1
        End While

        Return str
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Peek at the next token.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	3/30/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function PeekNextToken() As String
        Dim str As String = String.Empty

        ' no pIndex increment
        Dim index As Int32 = pIndex
        While str.Length = 0 AndAlso index < Tokens.Length
            str = Tokens(index)
            index += 1
        End While

        Return str
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Allow parsing of tokens that match '+ 34.567' where there's a space between the sign and the double value.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	3/21/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetNextDouble() As Double
        Dim result As Double = 0
        Dim str As String = NextToken()
        ' handle situation where space separates sign from number, eg, + 9   or   - 9
        If str.Equals(Constants.Plus) OrElse str.Equals(Constants.Minus) Then
            Double.TryParse(NextToken, result)
            If str.Equals(Constants.Minus) Then
                result = -result
            End If
            Return result
        End If
        ' otherwise simply parse
        Double.TryParse(str, result)
        Return result
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Allow parsing of tokens that match '+ 34' where there's a space between the sign and the int32 value.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	3/21/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetNextInt32() As Int32
        Dim result As Int32 = 0
        Dim str As String = NextToken()
        ' handle situation where space separates sign from number, eg, + 9   or   - 9
        If str.Equals(Constants.Plus) OrElse str.Equals(Constants.Minus) Then
            Int32.TryParse(NextToken, result)
            If str.Equals(Constants.Minus) Then
                result = -result
            End If
            Return result
        End If
        ' otherwise simply parse
        Int32.TryParse(str, result)
        Return result
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return a concatenation of remaining strings to EOL.
    ''' Will consolidate blanks, eg, "1   x y  z" will return "1 z y z".
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	3/30/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function StringToEOL() As String
        Dim sb As New Text.StringBuilder
        Dim str As String
        Do
            str = NextToken()
            If str.Length > 0 Then
                sb.Append(str)
                sb.Append(Constants.Delimiter)
            End If
        Loop While str.Length > 0
        Return sb.ToString.Trim
    End Function

    Public Function GetCountDoubles() As Int32
        Dim count As Int32

        For Each token As String In Tokens
            Dim d As Double = 0
            If Double.TryParse(token, d) Then
                count += 1
            End If
        Next

        Return count
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function GetCount() As Int32
        Dim count As Int32

        For Each token As String In Tokens
            If token.Length > 0 Then
                count += 1
            End If
        Next

        Return count
    End Function
#End Region

End Class
