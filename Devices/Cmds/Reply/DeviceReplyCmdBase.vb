#Region "Imports"
#End Region

Public MustInherit Class DeviceReplyCmdBase
    Implements IDeviceReplyCmd, ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ResponseTerminator As Byte = AscW(vbCrLf)
    Public Const DefaultReplyTimeMilliseconds As Int32 = 500
    Public Const ResponseDelimiter As String = vbTab & vbCr
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIDevice As IDevice
    Private pCmdReplyParms As CmdReplyParms
    Private pIIO As IIO
    Private pCmd As String
    Private pCmdMsg As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceReplyCmdBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceReplyCmdBase = New DeviceReplyCmdBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As DeviceReplyCmdBase
    '    Return New DeviceReplyCmdBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IDevice() As IDevice Implements IDeviceReplyCmd.IDevice
        Get
            Return pIDevice
        End Get
        Set(ByVal Value As IDevice)
            pIDevice = Value
        End Set
    End Property

    Public Property CmdReplyParms() As CmdReplyParms Implements IDeviceReplyCmd.CmdReplyParms
        Get
            Return pCmdReplyParms
        End Get
        Set(ByVal value As CmdReplyParms)
            pCmdReplyParms = value
        End Set
    End Property

    Public Property IIO() As IIO Implements IDeviceReplyCmd.IIO
        Get
            Return pIIO
        End Get
        Set(ByVal value As IIO)
            pIIO = value
        End Set
    End Property

    Public Property Cmd() As String Implements IDeviceReplyCmd.Cmd
        Get
            Return pCmd
        End Get
        Set(ByVal value As String)
            pCmd = value
        End Set
    End Property

    Public Property CmdMsg() As String Implements IDeviceReplyCmd.CmdMsg
        Get
            Return pCmdMsg
        End Get
        Set(ByVal value As String)
            pCmdMsg = value
        End Set
    End Property

    Public Overridable Function ReplyAction(ByRef IDevice As IDevice, ByRef IIO As IIO, ByRef cmd As String, ByRef cmdMsg As String) As Boolean Implements IDeviceReplyCmd.ReplyAction
        Me.IDevice = IDevice
        Me.IIO = IIO
        Me.Cmd = cmd
        Me.CmdMsg = cmdMsg

        DebugTrace.WriteLine("DeviceReplyCmdBase.ReplyAction")
        Return True
    End Function

    Public Function Clone() As Object Implements System.ICloneable.Clone, IDeviceReplyCmd.Clone
        Dim IDeviceReplyCmd As IDeviceReplyCmd = CType(Me.MemberwiseClone, Devices.IDeviceReplyCmd)
        IDeviceReplyCmd.IDevice = IDevice
        If CmdReplyParms IsNot Nothing Then
            IDeviceReplyCmd.CmdReplyParms = CType(CmdReplyParms.Clone, Devices.CmdReplyParms)
        End If
        IDeviceReplyCmd.IIO = IIO
        If Not String.IsNullOrEmpty(Cmd) Then
            IDeviceReplyCmd.Cmd = String.Copy(Cmd)
        End If
        If Not String.IsNullOrEmpty(CmdMsg) Then
            IDeviceReplyCmd.CmdMsg = String.Copy(CmdMsg)
        End If
        Return IDeviceReplyCmd
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
