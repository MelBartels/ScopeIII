Public Class CommonCalcs

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pRad As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CommonCalcs
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CommonCalcs = New CommonCalcs
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CommonCalcs
        Return New CommonCalcs
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function CalcRadDMS(ByVal sign As String, ByVal deg As Double, ByVal min As Double, ByVal sec As Double) As Double
        CalcRadDMS(deg, min, sec)
        If sign.Equals(BartelsLibrary.Constants.Plus) Then
            pRad = Math.Abs(pRad)
        ElseIf sign.Equals(BartelsLibrary.Constants.Minus) Then
            pRad = -Math.Abs(pRad)
        End If
        Return pRad
    End Function

    Public Function CalcRadDMS(ByVal deg As Double, ByVal min As Double, ByVal sec As Double) As Double
        Dim sign As String = BartelsLibrary.Constants.Plus
        If deg < 0 Then
            sign = BartelsLibrary.Constants.Minus
            deg = -deg
        End If
        If min < 0 Then
            sign = BartelsLibrary.Constants.Minus
            min = -min
        End If
        If sec < 0 Then
            sign = BartelsLibrary.Constants.Minus
            sec = -sec
        End If
        pRad = deg * Units.DegToRad + min * Units.ArcminToRad + sec * Units.ArcsecToRad
        If sign.Equals(BartelsLibrary.Constants.Minus) Then
            pRad = -pRad
        End If
        Return pRad
    End Function

    Public Function CalcRadHMS(ByVal sign As String, ByVal hr As Double, ByVal min As Double, ByVal sec As Double) As Double
        CalcRadHMS(hr, min, sec)
        If sign.Equals(BartelsLibrary.Constants.Plus) Then
            pRad = Math.Abs(pRad)
        ElseIf sign.Equals(BartelsLibrary.Constants.Minus) Then
            pRad = -Math.Abs(pRad)
        End If
        Return pRad
    End Function

    Public Function CalcRadHMS(ByVal hr As Double, ByVal min As Double, ByVal sec As Double) As Double
        Dim sign As String = BartelsLibrary.Constants.Plus
        If hr < 0 Then
            sign = BartelsLibrary.Constants.Minus
            hr = -hr
        End If
        If min < 0 Then
            sign = BartelsLibrary.Constants.Minus
            min = -min
        End If
        If sec < 0 Then
            sign = BartelsLibrary.Constants.Minus
            sec = -sec
        End If
        pRad = hr * Units.HrToRad + min * Units.MinToRad + sec * Units.SecToRad
        If sign.Equals(BartelsLibrary.Constants.Minus) Then
            pRad = -pRad
        End If
        Return pRad
    End Function

    Public Function ParseCoordinateValueRA(ByRef st As StringTokenizer) As Double
        Dim sign As String = st.PeekNextToken.Substring(0, 1)

        Dim h As Double = st.GetNextDouble
        Dim m As Double = st.GetNextDouble
        Dim s As Double = st.GetNextDouble

        If sign.Equals(BartelsLibrary.Constants.Plus) OrElse sign.Equals(BartelsLibrary.Constants.Minus) Then
            Return CalcRadHMS(sign, h, m, s)
        Else
            Return CalcRadHMS(h, m, s)
        End If
    End Function

    Public Function ParseCoordinateValueDec(ByRef st As StringTokenizer) As Double
        Dim sign As String = st.PeekNextToken.Substring(0, 1)

        Dim d As Double = st.GetNextDouble
        Dim m As Double = st.GetNextDouble
        Dim s As Double = st.GetNextDouble

        If sign.Equals(BartelsLibrary.Constants.Plus) OrElse sign.Equals(BartelsLibrary.Constants.Minus) Then
            Return CalcRadDMS(sign, d, m, s)
        Else
            Return CalcRadDMS(d, m, s)
        End If
    End Function

    Public Function ParseCoordinateValueRad(ByRef st As StringTokenizer) As Double
        Dim sign As String = st.PeekNextToken.Substring(0, 1)
        If sign.Equals(BartelsLibrary.Constants.Minus) Then
            Return -Math.Abs(st.GetNextDouble)
        Else
            Return st.GetNextDouble
        End If
    End Function

    Public Function ParseCoordinateValueDeg(ByRef st As StringTokenizer) As Double
        Return ParseCoordinateValueRad(st) * Units.DegToRad
    End Function

#End Region

#Region "Private and Protected Methods"
#End Region

End Class
