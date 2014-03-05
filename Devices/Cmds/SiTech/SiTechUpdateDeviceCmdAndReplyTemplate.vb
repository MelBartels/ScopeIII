#Region "Imports"
#End Region

Public Class SiTechUpdateDeviceCmdAndReplyTemplate
    Implements IUpdateDeviceCmdAndReplyTemplate

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

    'Public Shared Function GetInstance() As SiTechUpdateDeviceCmdAndReplyTemplate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SiTechUpdateDeviceCmdAndReplyTemplate = New SiTechUpdateDeviceCmdAndReplyTemplate
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SiTechUpdateDeviceCmdAndReplyTemplate
        Return New SiTechUpdateDeviceCmdAndReplyTemplate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Action(ByVal deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate, ByVal IDevice As IDevice) Implements IUpdateDeviceCmdAndReplyTemplate.Action

        Dim cmdMsgParms As CmdMsgParms = deviceCmdAndReplyTemplate.IDeviceCmd.CmdMsgParms
        Dim firstChar As String = cmdMsgParms.Cmd(0)
        Dim restOfCmd As String = cmdMsgParms.Cmd.Remove(0, 1)

        If SiTechUtil.IsPrimaryController(IDevice) Then
            If firstChar = "T" Then
                cmdMsgParms.Cmd = "X" & restOfCmd
            ElseIf firstChar = "U" Then
                cmdMsgParms.Cmd = "Y" & restOfCmd
            End If
        Else
            If firstChar = "X" Then
                cmdMsgParms.Cmd = "T" & restOfCmd
            ElseIf firstChar = "Y" Then
                cmdMsgParms.Cmd = "U" & restOfCmd
            End If
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
