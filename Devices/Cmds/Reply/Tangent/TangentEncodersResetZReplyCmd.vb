#Region "Imports"
#End Region

Public Class TangentEncodersResetZReplyCmd
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

    'Public Shared Function GetInstance() As TangentEncodersResetZReplyCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TangentEncodersResetZReplyCmd = New TangentEncodersResetZReplyCmd
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As TangentEncodersResetZReplyCmd
        Return New TangentEncodersResetZReplyCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function ReplyAction(ByRef IDevice As IDevice, ByRef IIO As IIO, ByRef cmd As String, ByRef cmdMsg As String) As Boolean
        MyBase.ReplyAction(IDevice, IIO, cmd, cmdMsg)

        ' cmdMsg ~ "+12345<tab>+12345<return>"
        ' a valid Reset command (R+20000<tab>+20000<return>) in hex looks like:
        ' "x52 x2B x32 x30 x30 x30 x30 x9 x2B x32 x30 x30 x30 x30 xD "

        Dim st As StringTokenizer = StringTokenizer.GetInstance
        st.Tokenize(cmdMsg, TangentEncodersQuerySendCmd.ResponseDelimiters.ToCharArray)
        CType(IDevice, EncodersBox).SetCountsPerRevolution(eMath.RInt(st.NextToken), eMath.RInt(st.NextToken))

        ' return '*'
        Return IIO.Send(CmdReplyParms.Reply)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
