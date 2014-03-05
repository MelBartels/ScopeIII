#Region "Imports"
Imports System.Windows.Forms
#End Region

Public Class AppMsgBox

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

    'Public Shared Function GetInstance() As AppMsgBox
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AppMsgBox = New AppMsgBox
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AppMsgBox
        Return New AppMsgBox
    End Function
#End Region

#Region "Shared Methods"
    Public Shared Function Show(ByVal msg As String) As MsgBoxResult
        Return Show(msg, MsgBoxStyle.OkOnly)
    End Function

    Public Shared Function Show(ByVal msg As String, ByVal styles As MsgBoxStyle) As MsgBoxResult
        Return MsgBox(msg, MsgBoxStyle.SystemModal Or styles, My.Application.Info.ProductName)
    End Function
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
