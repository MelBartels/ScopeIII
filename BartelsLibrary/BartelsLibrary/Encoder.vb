#Region "Imports"
#End Region

Public Class Encoder

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

    'Public Shared Function GetInstance() As Encoder
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Encoder = New Encoder
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Encoder
        Return New Encoder
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Shared Function BytesToString(ByVal bytes() As Byte, ByVal startIx As Int32, ByVal count As Int32) As String
        Dim sb As New Text.StringBuilder
        For ix As Int32 = 0 To count - 1
            sb.Append(ChrW(bytes(startIx + ix)))
        Next
        Return sb.ToString
    End Function

    Public Shared Function BytesToString(ByVal bytes() As Byte) As String
        Return BytesToString(bytes, 0, bytes.Length)
    End Function

    Public Shared Function StringtoBytes(ByVal [string] As String) As Byte()
        Dim bytes([string].Length - 1) As Byte
        For ix As Int32 = 0 To [string].Length - 1
            bytes(ix) = CByte(AscW([string].Substring(ix, 1)))
        Next
        Return bytes
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
