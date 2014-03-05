Public Class HexAdapter

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

    'Public Shared Function GetInstance() As HexAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As HexAdapter = New HexAdapter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As HexAdapter
        Return New HexAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function ConvertToHex(ByRef msg As String, Optional ByVal appendChar As Boolean = True) As String
        Dim sb As New Text.StringBuilder
        For Each c As Char In msg.ToCharArray
            Dim i As Int32 = Microsoft.VisualBasic.AscW(c)
            sb.Append("x")
            sb.Append(Hex(i))
            If appendChar Then
                If i > 31 AndAlso i < 127 Then
                    sb.Append("(")
                    sb.Append(c)
                    sb.Append(")")
                End If
            End If
            sb.Append(" ")
        Next
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
