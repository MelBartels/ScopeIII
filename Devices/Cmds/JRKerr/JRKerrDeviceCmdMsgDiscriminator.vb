#Region "Imports"
#End Region

Public Class JRKerrDeviceCmdMsgDiscriminator
    Implements IDeviceCmdMsgDiscriminator

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

    'Public Shared Function GetInstance() As JRKerrDeviceCmdMsgDiscriminator
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrDeviceCmdMsgDiscriminator = New JRKerrDeviceCmdMsgDiscriminator
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrDeviceCmdMsgDiscriminator
        Return New JRKerrDeviceCmdMsgDiscriminator
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function FindCmd(ByVal deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Int32) As Boolean Implements IDeviceCmdMsgDiscriminator.FindCmd
        Dim cmdMsgParms As CmdMsgParms = deviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms
        If Not cmdFitsIntoQueue(CmdMsgParms, cmdQueueArray, cmdQueueArrayIx) Then
            Return False
        End If
        If Not headerByteFound(CmdMsgParms, cmdQueueArray, cmdQueueArrayIx) Then
            Return False
        End If
        If Not matchAddress(CmdMsgParms, cmdQueueArray, cmdQueueArrayIx) Then
            Return False
        End If
        If Not matchCmdByte(CmdMsgParms, cmdQueueArray, cmdQueueArrayIx) Then
            Return False
        End If
        If Not cmdChecksumOK(cmdMsgParms) Then
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Function cmdFitsIntoQueue(ByVal cmdMsgParms As CmdMsgParms, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Boolean
        Return cmdMsgParms.ExpectedByteCount <= cmdQueueArray.Length - cmdQueueArrayIx
    End Function

    Protected Function headerByteFound(ByVal cmdMsgParms As CmdMsgParms, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Boolean
        Dim queueByte As Byte = getByte(cmdQueueArray, cmdQueueArrayIx + JRKerrUtil.Indeces.Header)
        Return queueByte.Equals(JRKerrUtil.HEADER_BYTE)
    End Function

    Protected Function matchAddress(ByVal cmdMsgParms As CmdMsgParms, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Boolean
        Dim queueByte As Byte = getByte(cmdQueueArray, cmdQueueArrayIx + JRKerrUtil.Indeces.Address)
        If queueByte.Equals(0) OrElse queueByte.Equals(&HFF) Then
            Return True
        End If
        Dim addressByte As Byte = getByte(cmdMsgParms.Cmd, JRKerrUtil.Indeces.Address)
        Return queueByte.Equals(addressByte)
    End Function

    Protected Function matchCmdByte(ByVal cmdMsgParms As CmdMsgParms, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Boolean
        Dim queueByte As Byte = getByte(cmdQueueArray, cmdQueueArrayIx + JRKerrUtil.Indeces.Cmd)
        Dim cmdByte As Byte = getByte(cmdMsgParms.Cmd, JRKerrUtil.Indeces.Cmd)
        Return lowerBits(queueByte).Equals(lowerBits(cmdByte))
    End Function

    Protected Function cmdChecksumOK(ByVal cmdMsgParms As CmdMsgParms) As Boolean
        Dim ok As Boolean = JRKerrUtil.VerifyCmdChecksum(BartelsLibrary.Encoder.StringtoBytes(cmdMsgParms.Cmd))
        If Not ok Then
            Throw New Exception("JRKerr command has bad checksum.")
        End If
        Return ok
    End Function

    Protected Function getByte(ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Byte
        Return getByte(CStr(cmdQueueArray.GetValue(cmdQueueArrayIx)), 0)
    End Function

    Protected Function getByte(ByVal [string] As String, ByVal ix As Integer) As Byte
        Return BartelsLibrary.Encoder.StringtoBytes([string])(ix)
    End Function

    ' strip # of data bytes (which is encoded into the upper 4 bits) from bytes before comparing
    Protected Function lowerBits(ByVal [byte] As Byte) As Byte
        Return CByte([byte] And &HF)
    End Function
#End Region

End Class
