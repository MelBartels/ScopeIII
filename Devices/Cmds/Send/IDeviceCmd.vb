Public Interface IDeviceCmd
    Property IDevice() As IDevice
    Property CmdMsgParms() As CmdMsgParms
    Property CmdReplyParms() As CmdReplyParms
    Function CreateXmtBytes() As Byte()
    Function Execute(ByRef IDevice As IDevice, ByRef IIO As IIO) As Boolean
    Function ProcessMsg(ByVal msg As String, ByVal bytesReceived As Int32, ByRef state As ISFT) As Boolean
    Function Clone() As Object
End Interface
