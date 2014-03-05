#Region "Imports"
#End Region

Public Class TangentEncodersResetRSendCmd
    Inherits DeviceCmdBase

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

    'Public Shared Function GetInstance() As TangentEncodersResetRSendCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TangentEncodersResetRSendCmd = New TangentEncodersResetRSendCmd
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As TangentEncodersResetRSendCmd
        Return New TangentEncodersResetRSendCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function CreateXmtBytes() As Byte()
        Dim dt As EncodersBox = CType(IDevice, EncodersBox)

        Dim encoder As Encoder = CType(IDevice.GetDevicesByName(dt.PriAxisName).Item(0), Encoder)
        Dim priCounts As Double = CType(encoder.GetProperty(GetType(EncoderValue).Name), EncoderValue).Range

        encoder = CType(IDevice.GetDevicesByName(dt.SecAxisName).Item(0), Encoder)
        Dim secCounts As Double = CType(encoder.GetProperty(GetType(EncoderValue).Name), EncoderValue).Range

        Dim sb As New Text.StringBuilder
        sb.Append(CmdMsgParms.Cmd)
        sb.Append(Format(priCounts, "00000"))
        sb.Append(vbTab)
        sb.Append(Format(secCounts, "00000"))
        sb.Append(vbCr)

        Return BartelsLibrary.Encoder.StringtoBytes(sb.ToString)
    End Function

    Public Overrides Function ProcessMsg(ByVal msg As String, ByVal bytesReceived As Integer, ByRef state As ISFT) As Boolean
        MyBase.ProcessMsg(msg, bytesReceived, state)

        ' expect 'R' after successful reset
        If state Is ReceiveInspectorState.ReadCorrectNumberOfBytes AndAlso msg.Equals(CmdReplyParms.Reply) Then
            IDevice.SetIOStatus(IOStatus.ValidResponse.Description)
            Return True
        Else
            IDevice.SetIOStatus(IOStatus.ResponseFailed.Description)
            Return False
        End If

    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
