#Region "Imports"
#End Region

Public Class Checksum

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

    'Public Shared Function GetInstance() As Checksum
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Checksum = New Checksum
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Checksum
        Return New Checksum
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Shared Function Calc(ByVal bytes() As Byte, ByVal startIx As Int32, ByVal count As Int32) As Byte
        ' use int to calculate checksum: overflow can result if using bytes
        Dim checksum As Int32 = 0
        For ix As Int32 = 0 To count - 1
            checksum += bytes(startIx + ix)
        Next
        Return CByte(checksum Mod 256)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
