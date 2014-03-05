#Region "Imports"
#End Region

Public Class TangentEncodersQuerySendCmd
    Inherits DeviceCmdBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ResponseDelimiters As String = vbTab & vbCr
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

    'Public Shared Function GetInstance() As TangentEncodersQuerySendCmd
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TangentEncodersQuerySendCmd = New TangentEncodersQuerySendCmd
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As TangentEncodersQuerySendCmd
        Return New TangentEncodersQuerySendCmd
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function CreateXmtBytes() As Byte()
        Return BartelsLibrary.Encoder.StringtoBytes(CmdMsgParms.Cmd)
    End Function

    Public Overrides Function ProcessMsg(ByVal msg As String, ByVal bytesReceived As Integer, ByRef state As ISFT) As Boolean
        MyBase.ProcessMsg(msg, bytesReceived, state)

        If state Is ReceiveInspectorState.TerminatedCharFound Then
            Dim st As StringTokenizer = StringTokenizer.GetInstance
            st.Tokenize(msg, ResponseDelimiters.ToCharArray)
            CType(IDevice, EncodersBox).SetValues(st.NextToken, st.NextToken)

            IDevice.SetIOStatus(IOStatus.ValidResponse.Description)
            Return True
        Else
            IDevice.SetIOStatus(IOStatus.ResponseFailed.Description)
            Return False
        End If
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
