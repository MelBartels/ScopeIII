#Region "Imports"
#End Region

Public Class DeviceCmdFacade
    Inherits DeviceCmdFacadeBase
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
    Private pIDeviceCmd As IDeviceCmd

#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceCmdFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceCmdFacade = New DeviceCmdFacade
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceCmdFacade
        Return New DeviceCmdFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IDeviceCmd() As IDeviceCmd
        Get
            Return pIDeviceCmd
        End Get
        Set(ByVal Value As IDeviceCmd)
            pIDeviceCmd = Value
        End Set
    End Property

    Public Overloads Sub StartIOListening()
        StartIOListening(Me)
    End Sub

    Public Function Execute(ByRef ISFT As ISFT) As Boolean
        Return Execute(IDevice.DeviceCmdsFacade.GetIDeviceCmd(ISFT))
    End Function

    Public Function Execute(ByRef IDeviceCmd As IDeviceCmd) As Boolean
        ' save IDeviceCmd for subsequent ProcessMsg()
        Me.IDeviceCmd = IDeviceCmd
        Return IDeviceCmd.Execute(IDevice, IIO)
    End Function

    Public Overrides Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        ' besides the msg, include results from ReceiveInspector
        Return IDeviceCmd.ProcessMsg(CStr([object]), IIO.ReceiveInspector.BytesRead, IIO.ReceiveInspector.State)
        ' to be notified when cmd finished, attach an observer (eg, myObserver):
        ' IDevice.GetDeviceTemplate.StatusObserver.Attach(myObserver)
        ' to tell if cmd succeeded, check processed msg:
        ' IOStatus.ValidResponse.Description.Equals(myObserver.msg)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region
End Class
