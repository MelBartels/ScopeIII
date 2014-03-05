#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class CmdListComparer
    Implements IComparer(Of ISFT)

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

    'Public Shared Function GetInstance() As CmdListComparer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CmdListComparer = New CmdListComparer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As CmdListComparer
        Return New CmdListComparer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Compare(ByVal x As ISFT, ByVal y As ISFT) As Integer Implements System.Collections.Generic.IComparer(Of ISFT).Compare
        ' failure to Compare, ie, exception thrown (Tag is nothing, types are incorrect), results in InvalidOperationException being thrown
        Dim xDeviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate = CType(x.Tag, DeviceCmdAndReplyTemplate)
        Dim yDeviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate = CType(y.Tag, DeviceCmdAndReplyTemplate)

        Dim xIDeviceCmd As IDeviceCmd = xDeviceCmdAndReplyTemplate.IDeviceCmd
        Dim yIDeviceCmd As IDeviceCmd = yDeviceCmdAndReplyTemplate.IDeviceCmd

        ' descending sort (longest cmd 1st, shortest cmd last)
        Return yIDeviceCmd.CmdMsgParms.Cmd.Length.CompareTo(xIDeviceCmd.CmdMsgParms.Cmd.Length)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
