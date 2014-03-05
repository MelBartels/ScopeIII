#Region "Imports"
#End Region

Public Class JRKerrUpdateDeviceCmdAndReplyTemplate
    Implements IUpdateDeviceCmdAndReplyTemplate

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

    'Public Shared Function GetInstance() As JRKerrUpdateDeviceCmdAndReplyTemplate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrUpdateDeviceCmdAndReplyTemplate = New JRKerrUpdateDeviceCmdAndReplyTemplate
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrUpdateDeviceCmdAndReplyTemplate
        Return New JRKerrUpdateDeviceCmdAndReplyTemplate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Action(ByVal deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate, ByVal IDevice As IDevice) Implements IUpdateDeviceCmdAndReplyTemplate.Action
        ' skip hard reset and set address cmds
        Dim cmd As ISFT = IDevice.DeviceCmdsFacade.GetCmd(deviceCmdAndReplyTemplate)
        If cmd Is JRKerrCmds.NmcHardReset OrElse cmd Is JRKerrCmds.SetAddress Then
            Exit Sub
        End If

        ' get cmd bytes
        Dim cmdMsgParms As CmdMsgParms = deviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms
        Dim cmdBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd)

        ' update address
        JRKerrUtil.SetAddress(IDevice, cmdBytes)

        ' update status cmd, including reply
        If cmd Is JRKerrCmds.ReadStatus Then
            ' notify receiver to expect one data byte, which will be the status items
            Dim JRKerrServoStatus As JRKerrServoStatus = JRKerrUtil.GetJRKerrServoStatus(IDevice)
            cmdBytes(3) = JRKerrServoStatus.StatusItems
            Dim replyBytes() As Byte = JRKerrUtil.EncodeStatusBytes(IDevice)
            deviceCmdAndReplyTemplate.IDeviceCmd.CmdReplyParms.Reply = BartelsLibrary.Encoder.BytesToString(replyBytes)
            deviceCmdAndReplyTemplate.IDeviceCmd.CmdReplyParms.ReceiveInspectParms.ExpectedByteCount = replyBytes.Length
        End If

        ' update checksum
        JRKerrUtil.SetCmdChecksum(cmdBytes)

        ' copy back results
        cmdMsgParms.Cmd = BartelsLibrary.Encoder.BytesToString(cmdBytes)
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
