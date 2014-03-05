#Region "Imports"
#End Region

Public MustInherit Class DeviceCmdBase
    Implements IDeviceCmd, ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIDevice As IDevice
    Private pCmdMsgParms As CmdMsgParms
    Private pCmdReplyParms As CmdReplyParms
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceCmdBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceCmdBase = New DeviceCmdBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As DeviceCmdBase
    '    Return New DeviceCmdBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IDevice() As IDevice Implements IDeviceCmd.IDevice
        Get
            Return pIDevice
        End Get
        Set(ByVal Value As IDevice)
            pIDevice = Value
        End Set
    End Property

    Public Property CmdMsgParms() As CmdMsgParms Implements IDeviceCmd.CmdMsgParms
        Get
            Return pCmdMsgParms
        End Get
        Set(ByVal value As CmdMsgParms)
            pCmdMsgParms = value
        End Set
    End Property

    Public Property CmdReplyParms() As CmdReplyParms Implements IDeviceCmd.CmdReplyParms
        Get
            Return pCmdReplyParms
        End Get
        Set(ByVal value As CmdReplyParms)
            pCmdReplyParms = value
        End Set
    End Property

    Public MustOverride Function CreateXmtBytes() As Byte() Implements IDeviceCmd.CreateXmtBytes

    Public Function Execute(ByRef IDevice As IDevice, ByRef IIO As IIO) As Boolean Implements IDeviceCmd.Execute
        Me.IDevice = IDevice
        ' setup the receive inspector to receive the command's results
        IIO.ReceiveInspector.Inspect(CmdReplyParms.ReceiveInspectParms)
        ' xmt the command
        If IIO.Send(CreateXmtBytes) Then
            ' set status
            IDevice.SetIOStatus(IOStatus.CmdSent.Description)
            ' data received by ProcessMsg() which calls IDeviceCmd.ProcessMsg() below
            Return True
        End If
        IDevice.SetIOStatus(IOStatus.CmdFailed.Description)
        Return False
    End Function

    Public Overridable Function ProcessMsg(ByVal msg As String, ByVal bytesReceived As Integer, ByRef state As ISFT) As Boolean Implements IDeviceCmd.ProcessMsg
        Debug.WriteLine("DeviceCmdBase processing msg: " & msg)
        Debug.WriteLine("receive bytes read count: " & bytesReceived)
        Debug.WriteLine("receive state: " & state.Description)

        Return True
    End Function

    Public Function Clone() As Object Implements System.ICloneable.Clone, IDeviceCmd.Clone
        Dim IDeviceCmd As IDeviceCmd = CType(Me.MemberwiseClone, Devices.IDeviceCmd)
        IDeviceCmd.IDevice = IDevice
        If CmdMsgParms IsNot Nothing Then
            IDeviceCmd.CmdMsgParms = CType(CmdMsgParms.Clone, Devices.CmdMsgParms)
        End If
        If CmdReplyParms IsNot Nothing Then
            IDeviceCmd.CmdReplyParms = CType(CmdReplyParms.Clone, Devices.CmdReplyParms)
        End If
        Return IDeviceCmd
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
