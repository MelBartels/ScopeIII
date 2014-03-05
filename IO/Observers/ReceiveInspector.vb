#Region "Imports"
#End Region

Public Class ReceiveInspector
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Private Const ReadBufferSize As Int32 = 4096
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pInspectorObservers As ObservableImp
    Private pReceiveInspectParms As ReceiveInspectParms
    Private pWaitTimer As Timers.Timer
    Private pInProcess As Boolean
    Private pState As ISFT

    Protected pReadBuffer(ReadBufferSize - 1) As Byte
    Protected pEndReadBufferPtr As Int32
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ReceiveInspector
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ReceiveInspector = New ReceiveInspector
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pInspectorObservers = ObservableImp.GetInstance
        pState = ReceiveInspectorState.NotSet
        pWaitTimer = New Timers.Timer
        pWaitTimer.AutoReset = False
        AddHandler pWaitTimer.Elapsed, AddressOf timerExpired
    End Sub

    Public Shared Function GetInstance() As ReceiveInspector
        Return New ReceiveInspector
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property InspectorObservers() As ObservableImp
        Get
            Return pInspectorObservers
        End Get
        Set(ByVal Value As ObservableImp)
            pInspectorObservers = Value
        End Set
    End Property

    Public ReadOnly Property State() As ISFT
        Get
            Return pState
        End Get
    End Property

    Public ReadOnly Property BytesRead() As Int32
        Get
            Return pEndReadBufferPtr + 1
        End Get
    End Property

    ' Read is 'finished' when:
    '     1) eod char encountered (if eod char passed in as nothing, byte value will be 0)
    '     2) desired # of bytes read (if unknown # of bytes, set parameter to Int32.MaxValue)
    '     3) timed out 
    ' 
    ' Observers of ReceiveInspector given whatever bytes were received.
    ' Results should be inspected by State and BytesRead properties.

    Public Sub Inspect(ByVal receiveInspectParms As ReceiveInspectParms)
        ' get ready to process received data
        pReceiveInspectParms = receiveInspectParms

        pEndReadBufferPtr = -1
        pState = ReceiveInspectorState.InProcess
        pInProcess = True

        pWaitTimer.Interval = pReceiveInspectParms.TimeoutMilliseconds
        pWaitTimer.Start()
    End Sub

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        If pInProcess Then
            ' first add to queue
            For Each b As Byte In Encoder.StringtoBytes(CStr([object]))
                pEndReadBufferPtr += 1
                pReadBuffer(pEndReadBufferPtr) = b
            Next
            ' now inspect queue: if condition met then set state and notify my observers of what's been received
            If pReceiveInspectParms.UseEndByte AndAlso foundTerminatingByte(pReceiveInspectParms.EndByte) Then
                pState = ReceiveInspectorState.TerminatedCharFound
                Return notifyInspectorObservers()
            ElseIf BytesRead > pReceiveInspectParms.ExpectedByteCount Then
                pState = ReceiveInspectorState.ReadTooManyBytes
                Return notifyInspectorObservers()
            ElseIf BytesRead.Equals(pReceiveInspectParms.ExpectedByteCount) Then
                pState = ReceiveInspectorState.ReadCorrectNumberOfBytes
                Return notifyInspectorObservers()
            End If
            ' no condition met that concludes receiving, so continue processing by waiting for another msg to appear: 
            ' timerExpired() may eventually fire, setting state and notifying my observers of what's been received
        Else
            Return False
        End If
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Function createStringFromQueue() As String
        Dim bytes(pEndReadBufferPtr) As Byte
        Array.Copy(pReadBuffer, bytes, pEndReadBufferPtr + 1)
        Return Encoder.BytesToString(bytes)
    End Function

    Private Sub timerExpired(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pInProcess Then
            pState = ReceiveInspectorState.TimedOut
            pInspectorObservers.Notify(createStringFromQueue)
        End If
    End Sub

    Private Function foundTerminatingByte(ByVal endOfReceiveByte As Byte) As Boolean
        For ix As Int32 = 0 To pEndReadBufferPtr
            If pReadBuffer(ix).Equals(endOfReceiveByte) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function notifyInspectorObservers() As Boolean
        pInProcess = False
        pWaitTimer.Stop()
        pInspectorObservers.Notify(createStringFromQueue)
        Return True
    End Function
#End Region

End Class
