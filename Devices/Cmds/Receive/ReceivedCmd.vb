#Region "Imports"
#End Region

Public Class ReceivedCmd

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public DeviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate
    Public CmdBeginQueueArrayIx As Int32
    Public CmdEndQueueArrayIx As Int32
    Public CmdMsgBeginQueueArrayIx As Int32
    Public CmdMsgEndQueueArrayIx As Int32
    Public CmdMsgExists As Boolean
    Public CmdMsg As String
    ' 0=correct count, 1=too few chars received, -1=too many chars received
    Public CmdByteCountCompare As Int32
    Public ValidCmdMsg As Boolean
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ReceivedCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark  as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ReceivedCmd = New ReceivedCmd
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ReceivedCmd
        Return New ReceivedCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
