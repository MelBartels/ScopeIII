#Region "Imports"
#End Region

Public Class JRKerrNmcHardResetSendCmd
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

    'Public Shared Function GetInstance() As JRKerrNmcHardResetSendCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrNmcHardResetSendCmd = New JRKerrNmcHardResetSendCmd
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrNmcHardResetSendCmd
        Return New JRKerrNmcHardResetSendCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function CreateXmtBytes() As Byte()

        Dim flushBytesSize As Int32 = 20
        Dim flushBytes(flushBytesSize - 1) As Byte
        For ix As Int32 = 0 To flushBytesSize - 1
            flushBytes(ix) = 0
        Next
        ' JRKerr code xmts the 0's immediately

        ' declare new bytes array 1+ size greater than cmd msg so as to include checksum
        Dim cmdBytes(CmdMsgParms.Cmd.Length) As Byte
        BartelsLibrary.Encoder.StringtoBytes(CmdMsgParms.Cmd).CopyTo(cmdBytes, 0)
        JRKerrUtil.SetCmdChecksum(cmdBytes)

        ' create xmt byte buffer 
        Dim xmtBytes(flushBytesSize + cmdBytes.Length - 1) As Byte
        ' copy the flush bytes 
        flushBytes.CopyTo(xmtBytes, 0)
        ' copy the cmd 
        cmdBytes.CopyTo(xmtBytes, flushBytesSize)

        Return xmtBytes
        'JRKerr sleeps for 100 milliseconds
    End Function

    Public Overrides Function ProcessMsg(ByVal msg As String, ByVal bytesReceived As Integer, ByRef state As ISFT) As Boolean
        MyBase.ProcessMsg(msg, bytesReceived, state)
        ' no response expected, so if response is received, fail it
        IDevice.SetIOStatus(IOStatus.ResponseFailed.Description)
        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
