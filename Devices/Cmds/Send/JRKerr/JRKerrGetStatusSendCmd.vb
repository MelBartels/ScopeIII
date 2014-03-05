#Region "Imports"
#End Region

Public Class JRKerrGetStatusSendCmd
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

    'Public Shared Function GetInstance() As JRKerrGetStatusSendCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrGetStatusSendCmd = New JRKerrGetStatusSendCmd
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrGetStatusSendCmd
        Return New JRKerrGetStatusSendCmd
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

        If state Is ReceiveInspectorState.ReadCorrectNumberOfBytes Then
            If JRKerrUtil.VerifyChecksum(BartelsLibrary.Encoder.StringtoBytes(msg)) Then
                JRKerrUtil.DecodeStatusBytes(IDevice, BartelsLibrary.Encoder.StringtoBytes(msg))
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
