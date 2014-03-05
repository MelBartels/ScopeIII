#Region "Imports"
#End Region

Public Class TangentEncodersQueryReplyCmd
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

    'Public Shared Function GetInstance() As TangentEncodersQueryReplyCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TangentEncodersQueryReplyCmd = New TangentEncodersQueryReplyCmd
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As TangentEncodersQueryReplyCmd
        Return New TangentEncodersQueryReplyCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function ReplyAction(ByRef IDevice As IDevice, ByRef IIO As IIO, ByRef cmd As String, ByRef cmdMsg As String) As Boolean
        MyBase.ReplyAction(IDevice, IIO, cmd, cmdMsg)

        Dim encoderValues() As String = CType(IDevice, EncodersBox).GetValues

        Dim sb As New Text.StringBuilder
        sb.Append(buildEncoderValueString(encoderValues(0)))
        sb.Append(vbTab)
        sb.Append(buildEncoderValueString(encoderValues(1)))
        sb.Append(vbCr)

        Return IIO.Send(sb.ToString)
    End Function
#End Region

#Region "Private and Protected Methods"
    ' positive values look like "50" while negative values look like "-50";
    ' make them look like "+00050" and "-00050"
    Protected Function buildEncoderValueString(ByVal encoderValue As String) As String
        If eMath.RInt(encoderValue) >= 0 Then
            Return BartelsLibrary.Constants.Plus & encoderValue.PadLeft(5, "0"c)
        Else
            Return BartelsLibrary.Constants.Minus & encoderValue.Substring(1).PadLeft(5, "0"c)
        End If
    End Function
#End Region

End Class