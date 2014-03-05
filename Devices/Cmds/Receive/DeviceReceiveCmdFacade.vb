#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class DeviceReceiveCmdFacade
    Inherits DeviceCmdFacadeBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ProcessReceivedCmds As New Generic.List(Of ReceivedCmd)
#End Region

#Region "Private and Protected Members"
    Private pCmdQueue As Queue
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceReceiveCmdFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceReceiveCmdFacade = New DeviceReceiveCmdFacade
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        CmdQueue = New Queue
    End Sub

    Public Shared Function GetInstance() As DeviceReceiveCmdFacade
        Return New DeviceReceiveCmdFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CmdQueue() As Queue
        Get
            Return pCmdQueue
        End Get
        Set(ByVal value As Queue)
            pCmdQueue = value
        End Set
    End Property

    Public Overloads Sub StartIOListening()
        StartIOListening(Me)
    End Sub

    ' parms passed to show dependencies (except for IDevice.SetIOStatus)

    Public Overrides Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        addReceivedObjectToCmdQueue([object], CmdQueue)
        inspectCmdQueue(getDeviceCmdsFacade.DeviceCmdAndReplyTemplateList, getDeviceCmdsFacade.IDeviceCmdMsgDiscriminator, CmdQueue)
        processCmdQueue(CmdQueue)
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#Region "IDevice facades"
    Protected Overridable Function getDeviceCmdsFacade() As DeviceCmdsFacade
        Return IDevice.DeviceCmdsFacade
    End Function

    Protected Sub setIOStatus(ByVal status As ISFT)
        IDevice.SetIOStatus(status.Description)
    End Sub
#End Region

    Protected Sub addReceivedObjectToCmdQueue(ByRef [object] As Object, ByVal cmdQueue As Queue)
        ' enumerating not thread safe
        ' otherwise in the .ForEach Action: Queue.Synchronized(CmdQueue).Enqueue([char].ToString)
        SyncLock cmdQueue.SyncRoot
            Array.ForEach(CStr([object]).ToCharArray, AddressOf New SubDelegate(Of Char, Queue) _
                          (AddressOf addCharToQueueAsString, cmdQueue).CallDelegate)
        End SyncLock
    End Sub

    Protected Sub addCharToQueueAsString(ByVal [char] As Char, ByVal cmdQueue As Queue)
        cmdQueue.Enqueue([char].ToString)
    End Sub

    Protected Sub inspectCmdQueue(ByRef DeviceCmdAndReplyTemplateList As List(Of DeviceCmdAndReplyTemplate), ByVal IDeviceCmdMsgDiscriminator As IDeviceCmdMsgDiscriminator, ByVal cmdQueue As Queue)
        Dim cmdQueueArray As Array = cmdQueue.ToArray

        scanCmdQueueAppendReceivedCmds(DeviceCmdAndReplyTemplateList, IDeviceCmdMsgDiscriminator, cmdQueueArray)

        Array.ForEach(ProcessReceivedCmds.ToArray, AddressOf New SubDelegate(Of ReceivedCmd, Array) _
            (AddressOf setReceivedCmdsBeginEndIndeces, cmdQueueArray).CallDelegate)

        Array.ForEach(ProcessReceivedCmds.ToArray, AddressOf New SubDelegate(Of ReceivedCmd, Array) _
            (AddressOf setReceivedCmdsMsgExists, cmdQueueArray).CallDelegate)

        Array.ForEach(ProcessReceivedCmds.ToArray, AddressOf New SubDelegate(Of ReceivedCmd, Array) _
            (AddressOf buildReceivedCmdsMsg, cmdQueueArray).CallDelegate)

        Array.ForEach(ProcessReceivedCmds.ToArray, AddressOf New SubDelegate(Of ReceivedCmd, Array) _
            (AddressOf setReceivedCmdsMsgValid, cmdQueueArray).CallDelegate)
    End Sub

    ' commands must be ordered by decreasing cmd msg length, ie, if commands are X, XX, and XXX, then they should be ordered XXX, XX, X;
    ' otherwise X could be executed three times when XXX was intended;
    ' the match is made by comparing commands against entire queue, so if queue=XXX and commands ordered XXX, XX, X, then XXX is matched,
    ' if queue=XXX and commands ordered XX, X, XXX, then XX is matched first, followed by X (XXX is never matched)

    Protected Sub scanCmdQueueAppendReceivedCmds(ByVal deviceCmdAndReplyTemplateList As List(Of DeviceCmdAndReplyTemplate), ByVal IDeviceCmdMsgDiscriminator As IDeviceCmdMsgDiscriminator, ByVal cmdQueueArray As Array)
        Dim cmdQueueArrayIx As Int32 = 0
        While cmdQueueArrayIx < cmdQueueArray.Length
            Dim foundDeviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate = deviceCmdAndReplyTemplateList.Find(AddressOf New FuncDelegate2(Of DeviceCmdAndReplyTemplate, Array, Int32) _
                (AddressOf IDeviceCmdMsgDiscriminator.FindCmd, cmdQueueArray, cmdQueueArrayIx).CallDelegate)
            If foundDeviceCmdAndReplyTemplate Is Nothing Then
                ' advance through queue one char at a time
                cmdQueueArrayIx += 1
            Else
                addFoundDeviceCmdAndReplyTemplatetoProcessReceivedCmds(foundDeviceCmdAndReplyTemplate, cmdQueueArrayIx)
                ' advance through queue skipping over command name + possibly the endbyte
                cmdQueueArrayIx += foundDeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd.Length
                If foundDeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.EndByteImmediatelyFollowsCmd Then
                    cmdQueueArrayIx += 1
                End If
            End If
        End While
    End Sub

    Protected Sub addFoundDeviceCmdAndReplyTemplatetoProcessReceivedCmds(ByVal deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate, ByVal cmdQueueArrayIx As Int32)
        Dim receivedCmd As ReceivedCmd = receivedCmd.GetInstance
        receivedCmd.DeviceCmdAndReplyTemplate = deviceCmdAndReplyTemplate
        receivedCmd.CmdBeginQueueArrayIx = cmdQueueArrayIx
        ProcessReceivedCmds.Add(receivedCmd)
    End Sub

    Protected Sub setReceivedCmdsBeginEndIndeces(ByVal receivedCmd As ReceivedCmd, ByVal cmdQueueArray As Array)
        Dim cmdLength As Int32 = receivedCmd.DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd.Length
        receivedCmd.CmdEndQueueArrayIx = receivedCmd.CmdBeginQueueArrayIx + cmdLength - 1
        receivedCmd.CmdMsgBeginQueueArrayIx = receivedCmd.CmdEndQueueArrayIx + 1

        Dim nextReceivedCmdIx As Int32 = ProcessReceivedCmds.IndexOf(receivedCmd) + 1
        If nextReceivedCmdIx < ProcessReceivedCmds.Count Then
            receivedCmd.CmdMsgEndQueueArrayIx = ProcessReceivedCmds(nextReceivedCmdIx).CmdBeginQueueArrayIx - 1
        Else
            receivedCmd.CmdMsgEndQueueArrayIx = cmdQueueArray.Length - 1
        End If
    End Sub

    Protected Sub setReceivedCmdsMsgExists(ByVal receivedCmd As ReceivedCmd, ByVal cmdQueueArray As Array)
        receivedCmd.CmdMsgExists = receivedCmd.CmdMsgEndQueueArrayIx >= receivedCmd.CmdMsgBeginQueueArrayIx
    End Sub

    Protected Sub buildReceivedCmdsMsg(ByVal receivedCmd As ReceivedCmd, ByVal cmdQueueArray As Array)
        If receivedCmd.CmdMsgExists Then
            Dim sb As New Text.StringBuilder
            For ix As Int32 = receivedCmd.CmdMsgBeginQueueArrayIx To receivedCmd.CmdMsgEndQueueArrayIx
                sb.Append(cmdQueueArray.GetValue(ix).ToString)
            Next
            receivedCmd.CmdMsg = sb.ToString
        End If
    End Sub

    Protected Sub setReceivedCmdsMsgValid(ByVal receivedCmd As ReceivedCmd, ByVal cmdQueueArray As Array)
        Dim cmdMsgParms As CmdMsgParms = receivedCmd.DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms

        ' set received byte count
        Dim byteCount As Int32 = receivedCmd.CmdMsgEndQueueArrayIx - receivedCmd.CmdBeginQueueArrayIx + 1
        receivedCmd.CmdByteCountCompare = cmdMsgParms.ExpectedByteCount.CompareTo(byteCount)

        ' set ValidCmdMsg
        ' if UseEndByte to determine if received cmd is valid, search for EndByte...
        If cmdMsgParms.UseEndByte Then
            For ix As Int32 = receivedCmd.CmdMsgBeginQueueArrayIx To receivedCmd.CmdMsgEndQueueArrayIx
                Dim endByteAsInt As Int32 = eMath.RInt(cmdMsgParms.EndByte)
                Dim desiredValueAsInt As Int32 = AscW(cmdQueueArray.GetValue(ix).ToString)
                If endByteAsInt.Equals(desiredValueAsInt) Then
                    receivedCmd.ValidCmdMsg = True
                    Exit For
                End If
            Next
        Else
            ' else set valid command based on # of bytes: can have extra bytes and still be valid
            receivedCmd.ValidCmdMsg = receivedCmd.CmdByteCountCompare < 1
        End If
    End Sub

    Protected Sub processCmdQueue(ByVal cmdQueue As Queue)
        clearCmdQueueIfNoReceivedCmds(cmdQueue)
        Array.ForEach(ProcessReceivedCmds.ToArray, AddressOf New SubDelegate(Of ReceivedCmd, Queue) _
                      (AddressOf clearCmdQueueExecuteReceivedCmd, cmdQueue).CallDelegate)
        ProcessReceivedCmds.Clear()
    End Sub

    Protected Sub clearCmdQueueIfNoReceivedCmds(ByVal cmdQueue As Queue)
        If ProcessReceivedCmds.Count < 1 Then
            setIOStatus(IOStatus.CmdFailed)
            SyncLock CmdQueue.SyncRoot
                CmdQueue.Clear()
            End SyncLock
        End If
    End Sub

    Protected Sub clearCmdQueueExecuteReceivedCmd(ByVal receivedCmd As ReceivedCmd, ByVal cmdQueue As Queue)
        clearCmdQueue(receivedCmd, cmdQueue)
        executeReceivedCmd(receivedCmd)
    End Sub

    Protected Sub clearCmdQueue(ByRef receivedCmd As ReceivedCmd, ByVal cmdQueue As Queue)
        ' clear the queue unless last command is incomplete
        If receivedCmd.ValidCmdMsg OrElse Not isLastReceivedCmd(receivedCmd) AndAlso isLastReceivedCmdIncomplete() Then
            SyncLock cmdQueue.SyncRoot
                For ix As Int32 = 0 To receivedCmd.CmdMsgEndQueueArrayIx - receivedCmd.CmdBeginQueueArrayIx
                    cmdQueue.Dequeue()
                Next
            End SyncLock
        End If
    End Sub

    Protected Sub executeReceivedCmd(ByRef receivedCmd As ReceivedCmd)
        ' execute the command if valid
        If receivedCmd.ValidCmdMsg Then
            executeCmd(receivedCmd)
            setIOStatus(IOStatus.CmdReceived)
        ElseIf receivedCmd.CmdByteCountCompare.Equals(1) Then
            ' incomplete or unfinished command
            setIOStatus(IOStatus.CmdIncomplete)
        Else
            setIOStatus(IOStatus.CmdFailed)
        End If
    End Sub

    Protected Function executeCmd(ByRef receivedCmd As ReceivedCmd) As Boolean
        DebugTrace.WriteLine("DeviceReceiveCmdFacade.executeCmd " & getDeviceCmdsFacade.GetCmd(receivedCmd.DeviceCmdAndReplyTemplate).Description)
        setIOStatus(IOStatus.ValidResponse)

        Dim IDeviceReplyCmd As IDeviceReplyCmd = receivedCmd.DeviceCmdAndReplyTemplate.IDeviceReplyCmd
        If IDeviceReplyCmd IsNot Nothing Then
            Return IDeviceReplyCmd.ReplyAction(IDevice, IIO, receivedCmd.DeviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms.Cmd, receivedCmd.CmdMsg)
        End If
        Return False
    End Function

    Protected Function isLastReceivedCmd(ByRef receivedCmd As ReceivedCmd) As Boolean
        Return ProcessReceivedCmds.Count > 0 AndAlso receivedCmd Is ProcessReceivedCmds(ProcessReceivedCmds.Count - 1)
    End Function

    Protected Function isLastReceivedCmdIncomplete() As Boolean
        Return ProcessReceivedCmds.Count > 0 AndAlso ProcessReceivedCmds(ProcessReceivedCmds.Count - 1).CmdByteCountCompare.Equals(1)
    End Function
#End Region

End Class
