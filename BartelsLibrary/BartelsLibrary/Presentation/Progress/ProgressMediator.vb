Imports System.Threading
Imports System.ComponentModel

Public Class ProgressMediator
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event CancelBackgroundWork()
#End Region

#Region "Private and Protected Members"
    Private WithEvents pProgressForm As Progress
    Private WithEvents pBackgroundWorker As BackgroundWorker
    Private pObservableImp As ObservableImp
    Private pShowDialogThread As Thread
    Private pProgressBarMaxValue As Int32
    Private pProgressBarCurrentValue As Int32
    Private pDelegateBackgroundWorker As [Delegate]
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ProgressMediator
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ProgressMediator = New ProgressMediator
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pObservableImp = ObservableImp.GetInstance
        createForm()
    End Sub

    Public Shared Function GetInstance() As ProgressMediator
        Return New ProgressMediator
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property BackgroundWorker() As BackgroundWorker
        Get
            Return pBackgroundWorker
        End Get
        Set(ByVal value As BackgroundWorker)
            pBackgroundWorker = value
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Dim msg As String = CStr([object])
        If msg.Equals(Constants.FormLoaded) Then
            pBackgroundWorker.RunWorkerAsync()
        Else
            ShowMsg(msg)
        End If
    End Function

    Public Property Title() As String
        Get
            Return pProgressForm.Text
        End Get
        Set(ByVal value As String)
            pProgressForm.Text = value
        End Set
    End Property

    Public Property ProgressBarMaxValue() As Int32
        Get
            Return pProgressBarMaxValue
        End Get
        Set(ByVal value As Int32)
            pProgressBarMaxValue = value
        End Set
    End Property

    Public Sub ShowMsg(ByVal msg As String)
        pProgressForm.AddText(msg)
        pProgressBarCurrentValue += 1
        updateProgressBarValue()
    End Sub

    Public Sub ProcessStartup(ByVal [delegate] As [Delegate])
        pBackgroundWorker = New BackgroundWorker
        pDelegateBackgroundWorker = [delegate]
        pProgressBarCurrentValue = 0
        pProgressForm.txBx.Text = String.Empty

        pShowDialogThread = New Thread(AddressOf showDialog)
        pShowDialogThread.Name = "Progress Form ShowDialog"
        pShowDialogThread.Start()
    End Sub

    Public Sub ProcessShutdown()
        If pProgressForm.InvokeRequired Then
            pProgressForm.Invoke(New Windows.Forms.MethodInvoker(AddressOf ProcessShutdown))
        Else
            pProgressForm.Close()
        End If
    End Sub

#End Region

#Region "Private and Protected Methods"
    Private Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles pBackgroundWorker.DoWork
        Try
            e.Result = pDelegateBackgroundWorker.DynamicInvoke(New Object() {sender, e})
        Catch ex As Exception
            ExceptionService.Notify(ex, "Exception caught for ProgressMediator BackgroundWorker thread.")
        End Try
    End Sub

    ' to cancel the background worker, set pBackgroundWorker.CancelAsync()
    ' then in the method that is executing in the background, pick up the .CancellationPending from the background worker

    'Private Sub backgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles pBackgroundWorker.ProgressChanged
    '    pForm.ProgressBar.Value = e.ProgressPercentage
    'End Sub

    'Private Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles pBackgroundWorker.RunWorkerCompleted
    '    If e.Error IsNot Nothing Then
    '        Debug.WriteLine("error")
    '    ElseIf e.Cancelled Then
    '        Debug.WriteLine("cancelled")
    '    Else
    '        Debug.WriteLine("succeeded")
    '    End If
    'End Sub

    Private Sub showDialog()
        pProgressForm.ShowDialog()
    End Sub

    Private Sub createForm()
        pProgressForm = New Progress
        AddHandler pProgressForm.Cancel, AddressOf cancel
        pProgressForm.ObservableImp.Attach(Me)
    End Sub

    Private Sub cancel()
        RaiseEvent CancelBackgroundWork()
    End Sub

    Private Sub updateProgressBarValue()
        If pProgressForm.ProgressBar.InvokeRequired Then
            pProgressForm.ProgressBar.Invoke(New Windows.Forms.MethodInvoker(AddressOf updateProgressBarValue))
        Else
            pProgressForm.ProgressBar.Value = eMath.RInt(100 * pProgressBarCurrentValue / pProgressBarMaxValue)
        End If
    End Sub
#End Region

End Class
