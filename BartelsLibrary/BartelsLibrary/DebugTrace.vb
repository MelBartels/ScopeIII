#Region "Imports"
#End Region

Public Class DebugTrace

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
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DebugTrace
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As DebugTrace = New DebugTrace
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DebugTrace
    '    Return New DebugTrace
    'End Function
#End Region

#Region "Shared Methods"
    Public Shared Sub Write(ByVal s As String)
        If Not String.IsNullOrEmpty(s) Then
            Debug.Write(s)
        End If
    End Sub

    Public Shared Sub WriteLine(ByVal s As String)
        If Not String.IsNullOrEmpty(s) Then
            Debug.WriteLine(s)
        End If
    End Sub
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
