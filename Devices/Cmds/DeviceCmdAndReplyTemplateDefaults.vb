#Region "Imports"
#End Region

Public Class DeviceCmdAndReplyTemplateDefaults

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

    'Public Shared Function GetInstance() As DeviceCmdAndReplyTemplateDefaults
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceCmdAndReplyTemplateDefaults = New DeviceCmdAndReplyTemplateDefaults
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceCmdAndReplyTemplateDefaults
        Return New DeviceCmdAndReplyTemplateDefaults
    End Function
#End Region

#Region "Shared Methods"

    ' When command is executed, the ReceiveInspector is setup with the CmdReplyParms;

    ' Each time input is received, the received data is parsed into received commands and data, then commands in the the queue are executed;

    ' CmdMsgParms: ExpectedByteCount used by DeviceReceiveCmdFacade's setReceivedCmdsMsgValid to set the received cmd's CmdByteCountCompare;
    '   UseEndByte+ExpectedByteCount used by DeviceReceiveCmdFacade to differentiate between otherwise identical get/set commands where the
    '       set command uses a parm, eg X<return> and X1234<return> where X<return> is the getter and X1234<return> is the setter

    ' CmdReplyParms: ExpectedByteCount used in ReceiveInspector's ProcessMsg and timerExpired to set the ReceiveInspectorState 
    '       and to notify the inspector observers;
    '   UseEndByte + EndByte trumps ExpectedByteCount; 
    '   if no EndByte and ExpectedByteCount not reached, the timer will time out based on TimeoutMilliseconds, sending what's received

#Region "General"
    Public Shared Function GetDefaultCmdMsgParms(ByRef ISFT As ISFT) As CmdMsgParms
        Return CType(ISFT.Tag, DeviceCmdAndReplyTemplate).IDeviceCmd.CmdMsgParms
    End Function

    Public Shared Function GetDefaultCmdReplyParms(ByRef ISFT As ISFT) As CmdReplyParms
        Return CType(ISFT.Tag, DeviceCmdAndReplyTemplate).IDeviceReplyCmd.CmdReplyParms
    End Function

    Public Shared Function GetDefaultCmd(ByRef ISFT As ISFT) As String
        Return GetDefaultCmdMsgParms(ISFT).Cmd
    End Function

    Public Shared Function GetDefaultReply(ByRef ISFT As ISFT) As String
        Return GetDefaultCmdReplyParms(ISFT).Reply
    End Function
#End Region

#Region "Test"
    Public Shared Function TestOne() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = Test1Cmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("XYZ", 3, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(3, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = Test1ReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function TestTwo() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = Test2Cmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("A", 1, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(1, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = Test2ReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function TestThree() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = Test3Cmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("AA", 2, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(2, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = Test3ReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function TestFour() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = Test4Cmd.GetInstance
        ' <bell>, P
        Dim cmdBytes() As Byte = {7, 80}
        Dim cmdString As String = BartelsLibrary.Encoder.BytesToString(cmdBytes)
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance(cmdString, 2, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(2, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = Test4ReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function
#End Region

#Region "Tangent"
    Public Shared Function TangentEncodersQuery() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = TangentEncodersQuerySendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("Q", 1, False, Nothing)
        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(Integer.MaxValue, True, ScopeLibrary.Constants.ByteCrLf, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))

        Dim IDeviceReplyCmd As IDeviceReplyCmd = TangentEncodersQueryReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function TangentEncodersResetR() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = TangentEncodersResetRSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("R", 13, True, ScopeLibrary.Constants.ByteCrLf)
        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance("R", ReceiveInspectParms.GetInstance(1, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))

        Dim IDeviceReplyCmd As IDeviceReplyCmd = TangentEncodersResetRReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function TangentEncodersResetZ() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = TangentEncodersResetZSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("Z", 15, True, ScopeLibrary.Constants.ByteCrLf)
        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance("*", ReceiveInspectParms.GetInstance(1, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))

        Dim IDeviceReplyCmd As IDeviceReplyCmd = TangentEncodersResetZReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function
#End Region

#Region "SiTech"
    Public Shared Function SiTechReadController() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechReadControllerSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("XXS" & vbCr, 4, False, Nothing)
        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(21, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))

        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechReadControllerReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechSetPriAccel() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechSetPriAccelSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("YR", 4, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(0, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechSetPriAccelReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechSetSecAccel() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechSetSecAccelSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("XR", 4, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(0, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechSetSecAccelReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechSetPriVel() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechSetPriVelSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("YS", 4, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(0, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechSetPriVelReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechSetSecVel() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechSetSecVelSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("XS", 4, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(0, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechSetSecVelReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechSetPriPos() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechSetPriPosSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("Y", 3, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(0, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechSetPriPosReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechSetSecPos() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechSetSecPosSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("X", 3, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(0, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechSetSecPosReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechGetPriAccel() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechGetPriAccelSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("YR", 3, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(Integer.MaxValue, True, ScopeLibrary.Constants.ByteCr, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechGetPriAccelReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechGetSecAccel() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechGetSecAccelSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("XR", 3, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(Integer.MaxValue, True, ScopeLibrary.Constants.ByteCr, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechGetSecAccelReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechGetPriVel() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechGetPriVelSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("YS", 3, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(Integer.MaxValue, True, ScopeLibrary.Constants.ByteCr, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechGetPriVelReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechGetSecVel() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechGetSecVelSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("XS", 3, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(Integer.MaxValue, True, ScopeLibrary.Constants.ByteCr, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechGetSecVelReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechGetPriPos() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechGetPriPosSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("Y", 2, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(Integer.MaxValue, True, ScopeLibrary.Constants.ByteCr, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechGetPriPosReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function SiTechGetSecPos() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = SiTechGetSecPosSendCmd.GetInstance
        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance("X", 2, True, ScopeLibrary.Constants.ByteCr)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(Integer.MaxValue, True, ScopeLibrary.Constants.ByteCr, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = SiTechGetSecPosReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function
#End Region

#Region "JRKerr"
    Public Shared Function JRKerrNmcHardReset() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = JRKerrNmcHardResetSendCmd.GetInstance

        Dim cmdBytes(3) As Byte
        cmdBytes(JRKerrUtil.Indeces.Header) = JRKerrUtil.HEADER_BYTE
        ' talk to group address
        cmdBytes(JRKerrUtil.Indeces.Address) = &HFF
        cmdBytes(JRKerrUtil.Indeces.Cmd) = HARD_RESET
        JRKerrUtil.SetCmdChecksum(cmdBytes)

        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance(BartelsLibrary.Encoder.BytesToString(cmdBytes), cmdBytes.Length, False, Nothing)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(Integer.MaxValue, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = JRKerrNmcHardResetReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function JRKerrSetAddress() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = JRKerrSetAddressSendCmd.GetInstance

        ' translate cmd into bytes
        Dim cmdBytes(5) As Byte
        cmdBytes(JRKerrUtil.Indeces.Header) = JRKerrUtil.HEADER_BYTE
        ' default address of 0
        cmdBytes(JRKerrUtil.Indeces.Address) = 0
        ' tell receiver that 2 data bytes will be sent 
        cmdBytes(JRKerrUtil.Indeces.Cmd) = &H20 Or SET_ADDR
        ' module (controller) address updated later
        cmdBytes(3) = 0
        ' set group address
        cmdBytes(4) = &HFF
        ' checksum updated later
        JRKerrUtil.SetCmdChecksum(cmdBytes)

        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance(BartelsLibrary.Encoder.BytesToString(cmdBytes), cmdBytes.Length, False, Nothing)

        Dim replyBytes(1) As Byte
        replyBytes(0) = MOVE_DONE
        JRKerrUtil.SetChecksum(replyBytes)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(BartelsLibrary.Encoder.BytesToString(replyBytes), ReceiveInspectParms.GetInstance(replyBytes.Length, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = JRKerrSetAddressReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function

    Public Shared Function JRKerrReadStatus() As DeviceCmdAndReplyTemplate
        Dim IDeviceCmd As IDeviceCmd = JRKerrGetStatusSendCmd.GetInstance

        ' translate cmd into bytes
        Dim cmdBytes(4) As Byte
        cmdBytes(JRKerrUtil.Indeces.Header) = JRKerrUtil.HEADER_BYTE
        ' address updated later
        cmdBytes(JRKerrUtil.Indeces.Address) = 0
        ' tell receiver that 1 data byte will be sent 
        cmdBytes(JRKerrUtil.Indeces.Cmd) = &H10 Or READ_STAT
        ' instruct receiver to send ID; updated later
        cmdBytes(3) = SEND_ID
        ' checksum updated later
        JRKerrUtil.SetCmdChecksum(cmdBytes)

        IDeviceCmd.CmdMsgParms = CmdMsgParms.GetInstance(BartelsLibrary.Encoder.BytesToString(cmdBytes), cmdBytes.Length, False, Nothing)

        Dim replyBytes(3) As Byte
        ' status
        replyBytes(0) = MOVE_DONE
        ' module version
        replyBytes(1) = 0
        ' module type
        replyBytes(2) = SERVOMODTYPE
        ' checksum
        JRKerrUtil.SetChecksum(replyBytes)

        IDeviceCmd.CmdReplyParms = CmdReplyParms.GetInstance(Nothing, ReceiveInspectParms.GetInstance(replyBytes.Length, False, Nothing, ScopeLibrary.Constants.DeviceDefaultReplyTimeMilliseconds))
        Dim IDeviceReplyCmd As IDeviceReplyCmd = JRKerrGetStatusReplyCmd.GetInstance
        IDeviceReplyCmd.CmdReplyParms = IDeviceCmd.CmdReplyParms

        Return DeviceCmdAndReplyTemplate.GetInstance(IDeviceCmd, IDeviceReplyCmd)
    End Function
#End Region

#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
