#Region "Imports"
#End Region

Public Class CmdReplyParms
    Implements ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Reply As String
    Public ReceiveInspectParms As ReceiveInspectParms
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CmdReplyParms
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark  as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CmdReplyParms = New CmdReplyParms
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CmdReplyParms
        Return New CmdReplyParms
    End Function

    Public Shared Function GetInstance(ByVal reply As String, ByVal receiveInspectParms As ReceiveInspectParms) As CmdReplyParms
        Dim cmdReplyParms As CmdReplyParms = cmdReplyParms.GetInstance
        cmdReplyParms.Build(reply, receiveInspectParms)
        Return cmdReplyParms
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Build(ByVal reply As String, ByVal receiveInspectParms As ReceiveInspectParms)
        Me.Reply = reply
        Me.ReceiveInspectParms = receiveInspectParms
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim lReply As String = Nothing
        If Not String.IsNullOrEmpty(Reply) Then
            lReply = String.Copy(Reply)
        End If
        Return GetInstance(lReply, CType(ReceiveInspectParms.Clone, ReceiveInspectParms))
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
