#Region "Imports"
#End Region

Public Class SiTechSetSecPosSendCmd
    Inherits DeviceCmdBase

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

    'Public Shared Function GetInstance() As SiTechSetSecPosSendCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SiTechSetSecPosSendCmd = New SiTechSetSecPosSendCmd
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As SiTechSetSecPosSendCmd
        Return New SiTechSetSecPosSendCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function CreateXmtBytes() As Byte()
        Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryServoMotorEncoder(IDevice), Devices.Encoder)
        Return BartelsLibrary.Encoder.StringtoBytes(CmdMsgParms.Cmd & encoder.Value & vbCr)
    End Function

    Public Overrides Function ProcessMsg(ByVal msg As String, ByVal bytesReceived As Integer, ByRef state As ISFT) As Boolean
        MyBase.ProcessMsg(msg, bytesReceived, state)
        ' no response expected, so if response is received, fail it
        IDevice.SetIOStatus(IOStatus.ResponseFailed.Description)
        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
