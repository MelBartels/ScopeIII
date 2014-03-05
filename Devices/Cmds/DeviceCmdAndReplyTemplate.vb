#Region "Imports"
#End Region

Public Class DeviceCmdAndReplyTemplate

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public IDeviceCmd As IDeviceCmd
    Public IDeviceReplyCmd As IDeviceReplyCmd
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceCmdAndReplyTemplate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceCmdAndReplyTemplate = New DeviceCmdAndReplyTemplate
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceCmdAndReplyTemplate
        Return New DeviceCmdAndReplyTemplate
    End Function
#End Region

#Region "Shared Methods"
    Public Shared Function GetInstance(ByRef IDeviceCmd As IDeviceCmd, ByRef IDeviceReplyCmd As IDeviceReplyCmd) As DeviceCmdAndReplyTemplate
        Dim deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate = deviceCmdAndReplyTemplate.GetInstance
        deviceCmdAndReplyTemplate.IDeviceCmd = IDeviceCmd
        deviceCmdAndReplyTemplate.IDeviceReplyCmd = IDeviceReplyCmd
        Return deviceCmdAndReplyTemplate
    End Function
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
