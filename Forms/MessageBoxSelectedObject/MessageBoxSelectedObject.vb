Public Class MessageBoxSelectedObject

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

    'Public Shared Function GetInstance() As MessageBoxSelectedObject
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MessageBoxSelectedObject = New MessageBoxSelectedObject
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MessageBoxSelectedObject
        Return New MessageBoxSelectedObject
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Show(ByVal selectedLWPosition As LWPosition) As MsgBoxResult
        Dim sb As New System.Text.StringBuilder
        sb.Append("Object Selected: ")
        sb.Append(selectedLWPosition.Name)
        sb.Append("   ")
        sb.Append(selectedLWPosition.RADisplay)
        sb.Append("   ")
        sb.Append(selectedLWPosition.DecDisplay)

        Return AppMsgBox.Show(sb.ToString)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
