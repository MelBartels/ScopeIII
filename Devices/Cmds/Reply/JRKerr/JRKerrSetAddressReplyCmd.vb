#Region "Imports"
#End Region

Public Class JRKerrSetAddressReplyCmd
    Inherits DeviceReplyCmdBase

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

    'Public Shared Function GetInstance() As JRKerrSetAddressReplyCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrSetAddressReplyCmd = New JRKerrSetAddressReplyCmd
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrSetAddressReplyCmd
        Return New JRKerrSetAddressReplyCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function ReplyAction(ByRef IDevice As IDevice, ByRef IIO As IIO, ByRef cmd As String, ByRef cmdMsg As String) As Boolean
        MyBase.ReplyAction(IDevice, IIO, cmd, cmdMsg)

        ' update module address to passed in address (the data byte in the set address command)
        Dim cmdBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(cmd)
        IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name).Value = CStr(cmdBytes(3))

        Dim replyBytes() As Byte = JRKerrUtil.EncodeStatusBytes(IDevice)
        Return IIO.Send(BartelsLibrary.Encoder.BytesToString(replyBytes))
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
