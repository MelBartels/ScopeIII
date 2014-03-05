#Region "Imports"
#End Region

Public Class StatusItemsToStatusByteBitDefsAdapter

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

    'Public Shared Function GetInstance() As StatusItemsToStatusByteBitDefsAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As StatusItemsToStatusByteBitDefsAdapter = New StatusItemsToStatusByteBitDefsAdapter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As StatusItemsToStatusByteBitDefsAdapter
        Return New StatusItemsToStatusByteBitDefsAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Shared Function ConvertToStatusItems(ByVal bits As String) As Byte
        Dim statusItems As Byte
        Dim st As StringTokenizer = StringTokenizer.GetInstance
        st.Tokenize(bits, ",".ToCharArray)
        For Each bit As String In st.Tokens
            Dim isft As ISFT = JRKerrServoStatusByteBitDefs.ISFT.MatchString(bit)
            If isft IsNot Nothing Then
                statusItems = CByte(statusItems + Math.Pow(2, isft.Key))
            End If
        Next
        Return statusItems
    End Function

    Public Shared Function ConvertToString(ByVal statusItems As Byte) As String
        Dim sb As New Text.StringBuilder

        Dim eBits As IEnumerator = JRKerrServoStatusByteBitDefs.ISFT.Enumerator
        While eBits.MoveNext
            Dim isft As ISFT = CType(eBits.Current, BartelsLibrary.ISFT)
            Dim isftByte As Byte = CByte(Math.Pow(2, isft.Key))
            If (statusItems And isftByte).Equals(isftByte) Then
                sb.Append(isft.Name)
                sb.Append(",")
            End If
        End While
        If sb.Length > 0 Then
            sb.Remove(sb.Length - 1, 1)
        End If

        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
