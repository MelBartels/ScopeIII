#Region "Imports"
#End Region

Public Class IOobservers
    Implements IIOobservers

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected Property IOState() As ISFT
        Get
            Return pIOState
        End Get
        Set(ByVal Value As ISFT)
            pIOState = Value
        End Set
    End Property

    Private pDisplayAsHex As Boolean
    Private pHexAdapter As HexAdapter
    Private pIOState As ISFT
    Private pReceiveObserver As IObserver
    Private pTransmitObserver As IObserver
    Private pStatusObserver As IObserver
    Private pProcessMsgObservers As ObservableImp
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IOobservers
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IOobservers = New IOobservers
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pHexAdapter = HexAdapter.GetInstance

        pIOState = BartelsLibrary.IOState.NotSet

        pReceiveObserver = IIOObserver.GetInstance.Build(BartelsLibrary.IOState.Rec)
        CType(pReceiveObserver, IIOObserver).IOobservers = Me

        pTransmitObserver = IIOObserver.GetInstance.Build(BartelsLibrary.IOState.Xmt)
        CType(pTransmitObserver, IIOObserver).IOobservers = Me

        pStatusObserver = IIOObserver.GetInstance.Build(BartelsLibrary.IOState.Status)
        CType(pStatusObserver, IIOObserver).IOobservers = Me

        pProcessMsgObservers = ObservableImp.GetInstance
    End Sub

    Public Shared Function GetInstance() As IOobservers
        Return New IOobservers
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ProcessMsgObservers() As ObservableImp
        Get
            Return pProcessMsgObservers
        End Get
        Set(ByVal Value As ObservableImp)
            pProcessMsgObservers = Value
        End Set
    End Property

    Public Property DisplayAsHex() As Boolean
        Get
            Return pDisplayAsHex
        End Get
        Set(ByVal Value As Boolean)
            pDisplayAsHex = Value
        End Set
    End Property

    Public Sub AttachObserversToIIO(ByRef IIO As IIO) Implements IIOobservers.AttachObserversToIIO
        IIO.ReceiveObservers.Attach(pReceiveObserver)
        IIO.TransmitObservers.Attach(pTransmitObserver)
        IIO.StatusObservers.Attach(pStatusObserver)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Sub processMsg(ByRef [object] As Object)
        pProcessMsgObservers.Notify([object])
    End Sub

    Protected Function processMsgState(ByVal msg As String, ByRef myDisplayState As ISFT) As Boolean
        Dim prepend As String = String.Empty
        ' allow subsequent receives, if immediate last msg was receive, to append to the same line
        If IOState IsNot BartelsLibrary.IOState.Rec OrElse myDisplayState IsNot BartelsLibrary.IOState.Rec Then
            prepend = vbCrLf + myDisplayState.Description
        End If
        If IOState IsNot myDisplayState Then
            IOState = myDisplayState
        End If
        ' only optionally convert to hex if observing transmit/receive messages
        If IOState Is BartelsLibrary.IOState.Rec OrElse IOState Is BartelsLibrary.IOState.Xmt Then
            processMsg(prepend & checkHexConvert(msg))
        Else
            processMsg(prepend & msg)
        End If
        Return True
    End Function

    Protected Function checkHexConvert(ByVal msg As String) As String
        If DisplayAsHex Then
            Return pHexAdapter.ConvertToHex(msg)
        Else
            Return msg
        End If
    End Function

    ' private classes in VB.NET cannot see into the containing class, so must include a reference to the containing class
    Private Class IIOObserver : Implements IObserver
        Public IOobservers As IOobservers
        Private pDisplayState As ISFT
        Private Sub New()
        End Sub
        Public Shared Function GetInstance() As IIOObserver
            Return New IIOObserver
        End Function
        Public Function Build(ByVal displayState As ISFT) As IObserver
            pDisplayState = displayState
            Return Me
        End Function
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Return IOobservers.processMsgState(CStr([object]), pDisplayState)
        End Function
    End Class
#End Region

End Class
