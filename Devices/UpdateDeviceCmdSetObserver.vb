#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class UpdateDeviceCmdSetObserver
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public DeviceCmdsFacade As DeviceCmdsFacade
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UpdateDeviceCmdSetObserver
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UpdateDeviceCmdSetObserver = New UpdateDeviceCmdSetObserver
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UpdateDeviceCmdSetObserver
        Return New UpdateDeviceCmdSetObserver
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements BartelsLibrary.IObserver.ProcessMsg
        Dim cmdProtocolName As String = CStr([object])
        DeviceCmdsFacade.CmdSet = CType(DeviceCmdSet.ISFT.MatchString(cmdProtocolName).Tag, ISFTFacade)
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
