Public Interface IDeviceReplyCmd
    Property IDevice() As IDevice
    Property CmdReplyParms() As CmdReplyParms
    Property IIO() As IIO
    Property Cmd() As String
    Property CmdMsg() As String
    Function ReplyAction(ByRef IDevice As IDevice, ByRef IIO As IIO, ByRef cmd As String, ByRef cmdMsg As String) As Boolean
    Function Clone() As Object
End Interface
