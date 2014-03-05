Public Interface IDeviceCmdMsgDiscriminator
    Function FindCmd(ByVal deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Int32) As Boolean
End Interface
