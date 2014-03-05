#Region "Imports"
#End Region

Public Class UpdateDeviceCmdsObserver
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pIDevice As IDevice
    Protected pUpdateDeviceCmdAndReplyTemplate As IUpdateDeviceCmdAndReplyTemplate
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UpdateDeviceCmdsObserver
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UpdateDeviceCmdsObserver = New UpdateDeviceCmdsObserver
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As UpdateDeviceCmdsObserver
        Return New UpdateDeviceCmdsObserver
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Build(ByVal IDevice As IDevice, ByVal updateDeviceCmdAndReplyTemplate As IUpdateDeviceCmdAndReplyTemplate)
        pIDevice = IDevice
        pUpdateDeviceCmdAndReplyTemplate = updateDeviceCmdAndReplyTemplate
    End Sub

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements BartelsLibrary.IObserver.ProcessMsg
        pIDevice.DeviceCmdsFacade.DeviceCmdAndReplyTemplateList.ForEach(AddressOf New SubDelegate(Of DeviceCmdAndReplyTemplate, IDevice) _
            (AddressOf pUpdateDeviceCmdAndReplyTemplate.Action, pIDevice).CallDelegate)
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
