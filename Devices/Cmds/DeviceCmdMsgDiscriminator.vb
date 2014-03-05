#Region "Imports"
#End Region

Public Class DeviceCmdMsgDiscriminator
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

    'Public Shared Function GetInstance() As DeviceCmdMsgDiscriminator
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceCmdMsgDiscriminator = New DeviceCmdMsgDiscriminator
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceCmdMsgDiscriminator
        Return New DeviceCmdMsgDiscriminator
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function FindCmd(ByVal deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Int32) As Boolean Implements IDeviceCmdMsgDiscriminator.FindCmd
        Dim cmdMsgParms As CmdMsgParms = deviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms
        Dim cmdName As String = cmdMsgParms.Cmd
        ' check fit then match
        If Not cmdNameFitsIntoQueue(cmdName, cmdQueueArray, cmdQueueArrayIx) Then
            Return False
        End If
        If Not cmdNameMatchesQueue(cmdName, cmdQueueArray, cmdQueueArrayIx) Then
            Return False
        End If
        ' if end byte, check fit then match
        If cmdMsgParms.EndByteImmediatelyFollowsCmd Then
            If Not roomForCmdEndByte(cmdName, cmdQueueArray, cmdQueueArrayIx) Then
                Return False
            End If
            If Not matchEndByte(cmdMsgParms, cmdName, cmdQueueArray, cmdQueueArrayIx) Then
                Return False
            End If
        End If

        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Function cmdNameFitsIntoQueue(ByVal cmdName As String, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Boolean
        Return cmdName.Length <= cmdQueueArray.Length - cmdQueueArrayIx
    End Function

    Protected Function roomForCmdEndByte(ByVal cmdName As String, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Boolean
        Return cmdName.Length + 1 <= cmdQueueArray.Length - cmdQueueArrayIx
    End Function

    ' works for cmds composed of bytes too
    Protected Function cmdNameMatchesQueue(ByVal cmdName As String, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Boolean
        For ix As Int32 = 0 To cmdName.Length - 1
            If Not cmdName.Substring(ix, 1).Equals(CStr(cmdQueueArray.GetValue(ix + cmdQueueArrayIx))) Then
                Return False
            End If
        Next
        Return True
    End Function

    Protected Function matchEndByte(ByVal cmdMsgParms As CmdMsgParms, ByVal cmdName As String, ByVal cmdQueueArray As Array, ByVal cmdQueueArrayIx As Integer) As Boolean
        ' not CStr(cmdMsgParms.EndByte) because the string representation of the byte will be returned, eg, <return> = "13"
        Return CInt(cmdMsgParms.EndByte).Equals(AscW(CStr(cmdQueueArray.GetValue(cmdName.Length + cmdQueueArrayIx))))
    End Function
#End Region

End Class
