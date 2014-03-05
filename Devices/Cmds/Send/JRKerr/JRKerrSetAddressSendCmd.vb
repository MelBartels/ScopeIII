#Region "Imports"
#End Region

Public Class JRKerrSetAddressSendCmd
    Inherits DeviceCmdBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ResponseDelimiters As String = vbCr
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

    'Public Shared Function GetInstance() As JRKerrSetAddressSendCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrSetAddressSendCmd = New JRKerrSetAddressSendCmd
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrSetAddressSendCmd
        Return New JRKerrSetAddressSendCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function CreateXmtBytes() As Byte()
        Return BartelsLibrary.Encoder.StringtoBytes(CmdMsgParms.Cmd)
    End Function

    Public Overrides Function ProcessMsg(ByVal msg As String, ByVal bytesReceived As Integer, ByRef state As ISFT) As Boolean
        MyBase.ProcessMsg(msg, bytesReceived, state)

        If state Is ReceiveInspectorState.ReadCorrectNumberOfBytes AndAlso msg.Equals(CmdReplyParms.Reply) Then
            Dim msgBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(msg)
            If JRKerrUtil.VerifyChecksum(msgbytes) Then
                ' get back 2 bytes: status, cksum
                Dim status As Byte = msgBytes(0)
                JRKerrUtil.GetJRKerrServoStatus(IDevice).Status = status
                IDevice.SetIOStatus(IOStatus.ValidResponse.Description)
                Return True
            Else
                IDevice.SetIOStatus(IOStatus.ChecksumFailed.Description)
                Return False
            End If
        Else
            IDevice.SetIOStatus(IOStatus.ResponseFailed.Description)
            Return False
        End If
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
