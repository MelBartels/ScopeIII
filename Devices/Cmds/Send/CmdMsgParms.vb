#Region "Imports"
#End Region

Public Class CmdMsgParms
    Implements ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Cmd As String
    Public ExpectedByteCount As Int32
    Public UseEndByte As Boolean
    Public EndByte As Byte
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CmdMsgParms
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark  as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CmdMsgParms = New CmdMsgParms
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CmdMsgParms
        Return New CmdMsgParms
    End Function

    Public Shared Function GetInstance(ByVal cmd As String, ByVal expectedByteCount As Int32, ByVal useEndByte As Boolean, ByVal endByte As Byte) As CmdMsgParms
        Dim cmdMsgParms As CmdMsgParms = cmdMsgParms.GetInstance
        cmdMsgParms.Build(cmd, expectedByteCount, useEndByte, endByte)
        Return cmdMsgParms
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Build(ByVal cmd As String, ByVal expectedByteCount As Int32, ByVal useEndByte As Boolean, ByVal endByte As Byte)
        Me.Cmd = cmd
        Me.ExpectedByteCount = expectedByteCount
        Me.UseEndByte = useEndByte
        Me.EndByte = endByte
    End Sub

    Public Function EndByteImmediatelyFollowsCmd() As Boolean
        Return UseEndByte AndAlso ExpectedByteCount.Equals(Cmd.Length + 1)
    End Function

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return GetInstance(String.Copy(Cmd), ExpectedByteCount, UseEndByte, EndByte)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
