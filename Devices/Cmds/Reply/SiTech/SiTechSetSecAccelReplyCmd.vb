#Region "Imports"
#End Region

Public Class SiTechSetSecAccelReplyCmd
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

    'Public Shared Function GetInstance() As SiTechSetSecAccelReplyCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SiTechSetSecAccelReplyCmd = New SiTechSetSecAccelReplyCmd
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SiTechSetSecAccelReplyCmd
        Return New SiTechSetSecAccelReplyCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function ReplyAction(ByRef IDevice As IDevice, ByRef IIO As IIO, ByRef cmd As String, ByRef cmdMsg As String) As Boolean
        MyBase.ReplyAction(IDevice, IIO, cmd, cmdMsg)

        Dim valueObject As ValueObject = SiTechUtil.GetSecondaryServoMotor(IDevice).GetValueObject(ValueObjectNames.Acceleration.Name)
        ' cmdMsg ~ "1000" & VbCr
        Dim accel As Int32
        If Int32.TryParse(cmdMsg, accel) Then
            valueObject.Value = CStr(accel)
            Return True
        End If

        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
