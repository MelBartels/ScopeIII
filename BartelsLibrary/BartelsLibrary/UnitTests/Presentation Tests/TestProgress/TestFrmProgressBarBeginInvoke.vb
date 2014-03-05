Imports System.Windows.Forms
Imports NUnit.Framework

''' -----------------------------------------------------------------------------
''' Project	 : Forms
''' Class	 : Forms.TestFrmProgressBarBeginInvoke
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' 
''' http://msdn.microsoft.com/msdnmag/issues/03/02/Multithreading/
''' 
''' 
''' ex of callback using EventHandler:
''' 
''' // Created on UI thread
''' private Label lblStatus;
''' •••
''' // Doesn't run on UI thread
''' private void RunsOnWorkerThread() {
'''     DoSomethingSlow();
'''     // Do UI update on UI thread
'''     object[] pList = { this, System.EventArgs.Empty };
'''     lblStatus.BeginInvoke(new System.EventHandler(UpdateUI), pList);
''' }
''' •••
''' // Code to be run back on the UI thread (using System.EventHandler signature so we don't need to define a new delegate type here)
''' private void UpdateUI(object o, System.EventArgs e) {
'''     // Now OK - this method will be called via Control.Invoke, so we are allowed to do things to the UI.
'''     lblStatus.Text = "Finished!";
''' }
''' 
''' 
''' ex of wrapping calls:
''' 
''' public class MyForm : System.Windows.Forms.Form {
'''     ...
'''     public void ShowProgress(string msg, int percentDone) {
'''         // Wrap the parameters in some EventArgs-derived custom class:
'''         System.EventArgs e = new MyProgressEvents(msg, percentDone);
'''         object[] pList = { this, e };
''' 
'''         // Invoke the method. This class is derived from Form, so we can just call BeginInvoke to get to the UI thread.
'''         BeginInvoke(new MyProgressEventsHandler(UpdateUI), pList);
'''     }
''' 
'''     private delegate void MyProgressEventsHandler(object sender, MyProgressEvents e);
''' 
'''     private void UpdateUI(object sender, MyProgressEvents e) {
'''         lblStatus.Text = e.Msg;
'''         myProgressControl.Value = e.PercentDone;
'''     }
''' }
''' 
''' above ex including the InvokeRequired check to avoid unnecessarily creating another thread:
''' 
''' public void ShowProgress(string msg, int percentDone) {
'''     if (InvokeRequired) {
'''         // As before above
'''         •••
'''     } else {
'''         // We're already on the UI thread just call straight through.
'''         UpdateUI(this, new MyProgressEvents(msg,
'''             PercentDone));
'''     }
''' }
''' 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	4/27/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<TestFixture()> Public Class TestFrmProgressBarBeginInvoke
    Implements IObserver

    Private pProgressBarPresenter As ProgressBarPresenter

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFrm()
        Dim frmThread As New Threading.Thread(AddressOf showFrm)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(60000)
        AddHandler frmTimer.Elapsed, AddressOf killFrm
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private pBackgroundWorkThread As Threading.Thread

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Dim mi As New MethodInvoker(AddressOf backgroundWork)
        mi.BeginInvoke(Nothing, Nothing)
    End Function

    Private Sub backgroundWork()
        Dim max As Int32 = 20
        For ix As Int32 = 0 To max
            pProgressBarPresenter.DataModel = New Object() {ix / max, ix.ToString}
            ' do some stuff to make system busy
            Threading.Thread.Sleep(100)
            For wait As Double = 0 To 1000000
                workOnThisThread()
            Next
        Next
        pProgressBarPresenter.Close()
    End Sub

    Private Sub workOnThisThread()
        ' do some throw away work to make the system busy
        ProgressBarPresenter.GetInstance()
    End Sub

    Private Sub showFrm()
        pProgressBarPresenter = ProgressBarPresenter.GetInstance
        pProgressBarPresenter.IMVPView = New FrmProgressBar
        pProgressBarPresenter.IObservable.Attach(Me)
        pProgressBarPresenter.ShowDialog()
    End Sub

    Private Sub killFrm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pProgressBarPresenter.Close()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class
