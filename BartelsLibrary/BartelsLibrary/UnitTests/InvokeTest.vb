Imports NUnit.Framework

<TestFixture()> Public Class InvokeTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    ' illustrates several features and coding styles, including use of:
    ' handling an event raised from a class,
    ' background work thread callback from form load,
    ' delegate, 
    ' .InvokeRequired, 
    ' .Invoke a method, 
    ' .Invoke a delegate with parms and return value, 
    ' timers, 
    ' threads including thread join,
    ' mutexes

    ' 1. form is created on this thread (thread#1)
    ' 2. then shown on a new thread (thread#2) where components are created and subsequently invoked against
    ' 3. add text form created and shown on a new thread (thread#3)
    ' 4. form load event is handled by a sub on this thread that launches a background work thread (thread#4)
    ' 5. form load also calls back on a delegate that launches another background work thread (thread#5)
    ' 6. background work thread displays a message (thread#4 crosses to thread#2) and launches a timer on new thread (thread#6)
    ' 7. background work thread #2 displays a message (thread#5 crosses to thread#2)
    ' 8. timer waits for several seconds, displays a message (thread#6 crosses to thread#2), sleeps for 1 second, then closes the form
    ' 
    ' this thread#1 -> creates thread#2 (show text form) and thread#3 (add text form);
    ' thread #2 (show text form) -> .ShowDialog() -> FrmTextBox_Load event which:
    '    1. raises event -> caught here -> creates backgroundWork thread#4 -> writes to textbox
    '                                                                      -> creates timer (thread#6) -> writes to textbox
    '                                                                                                  -> closes forms
    '    2. callback via delegate here -> creates backgroundWork2 thread#5 -> writes to textbox

    ' relevant code from the form FormTextBox:
    'Public Event FormLoaded()
    'Public Event AddText(ByVal textToAdd As String)
    'Private formLoadDelegate As [Delegate]
    'Public Sub RegisterDelegate(ByRef delegateToRegister As [Delegate])
    '    formLoadDelegate = delegateToRegister
    'End Sub
    'Private Sub FrmTextBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    RaiseEvent FormLoaded()
    '    If formLoadDelegate IsNot Nothing Then
    '        formLoadDelegate.DynamicInvoke()
    '    End If
    'End Sub
    'Private Sub btnAddText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddText.Click
    '    'txBxDisplay.AppendText(Environment.NewLine & txBxAddText.Text)
    '    RaiseEvent AddText(txBxAddText.Text)
    'End Sub

    Dim WithEvents testForm As FrmTextBox
    Dim WithEvents addTextForm As FrmAddText

    ' for event handler
    Private Delegate Function delegateDisplayText(ByVal text As String) As Boolean

    ' for delegate callback: [Delegate] is MustInherit, so must declare a Delegate here
    Private Delegate Sub delegateFormCallback()

    ' must be Shared to work
    Private Shared pMutex As New Threading.Mutex

    <Test()> Public Sub TestInvoke()
        ' instantiate form on this thread, then .ShowDialog on another, requiring .Invoke to access form's components
        testForm = New FrmTextBox
        ' must downcast to type [Delegate]
        testForm.RegisterDelegate(CType(New delegateFormCallback(AddressOf backgroundWork2Thread), [Delegate]))

        ' form's components created on the thread that calls .ShowDialog(); consequently .Invoke runs on .ShowDialog()'s thread;
        ' if testForm.ShowDialog() here, then this thread used, if testForm.ShowDialog() on new thread, then new thread used;
        ' wherever .ShowDialog is called, it blocks the thread that it is called from: hence call .ShowDialog from a new thread
        'testForm.ShowDialog()
        Dim showFormThread As New Threading.Thread(AddressOf showTestForm)
        showFormThread.Name = "showTestForm"
        showFormThread.Start()

        ' ditto for add text from another form
        Dim addTextFormThread As New Threading.Thread(AddressOf showAddTextForm)
        addTextFormThread.Name = "showAddTextForm"
        addTextFormThread.Start()

        Debug.WriteLine("TestInvoke before thread join")
        ' wait until the .ShowDialog thread finishes
        showFormThread.Join()
        Debug.WriteLine("TestInvoke finished")
    End Sub

    Private Sub showTestForm()
        ' blocks until form is closed
        testForm.ShowDialog()
        Debug.WriteLine("showTestForm finished")
    End Sub

    Private Sub showAddTextForm()
        addTextForm = New FrmAddText
        ' blocks until form is closed
        addTextForm.ShowDialog()
        Debug.WriteLine("showAddTextForm finished")
    End Sub

    ' do some long running background work on a new thread immediately after the form loads
    Private Sub formLoaded() Handles testForm.FormLoaded
        Dim backgroundWorkThread As New Threading.Thread(AddressOf backgroundWork)
        backgroundWorkThread.Name = "backgroundWork"
        backgroundWorkThread.Start()
    End Sub

    Private Sub addTextFromForm(ByVal text As String) Handles testForm.AddText
        decorateTextStrategy(text)
    End Sub

    Private Sub addTextFromOtherForm(ByVal text As String) Handles addTextForm.AddText
        decorateTextInvokeWhenNecessary(text)
    End Sub

    Private Sub backgroundWork()
        ' attempt to display some text in the form's textBox
        decorateTextStrategy("hello from backgroundWork thread")

        ' close the forms on a timer
        Dim closeFormsTimer As New Timers.Timer(4000)
        AddHandler closeFormsTimer.Elapsed, AddressOf closeForms
        closeFormsTimer.AutoReset = False
        closeFormsTimer.Start()
    End Sub

    Private Sub backgroundWork2Thread()
        Dim backgroundWorkThread2 As New Threading.Thread(AddressOf backgroundWork2)
        backgroundWorkThread2.Name = "backgroundWork2"
        backgroundWorkThread2.Start()
    End Sub

    Private Sub backgroundWork2()
        Try
            'Threading.Thread.Sleep(1000)
            ' attempt to display some text in the form's textBox
            decorateTextInvokeWhenNecessary("hello from backgroundWork#2 thread")
        Catch ex As Exception
            Debug.WriteLine(ex)
        End Try
    End Sub

    Private Function decorateTextStrategy(ByVal text As String) As Boolean
        ' make subsequent calls to this function wait until function completes current call
        pMutex.WaitOne()

        If testForm.txBxDisplay.InvokeRequired Then
            testForm.txBxDisplay.Invoke(New delegateDisplayText(AddressOf decorateText), New Object() {"(InvokeRequired)"})
            testForm.txBxDisplay.Invoke(New delegateDisplayText(AddressOf decorateText), New Object() {text})
        Else
            decorateText("Invoke not required")
            decorateText(text)
        End If

        pMutex.ReleaseMutex()
        Return True
    End Function

    Private Function decorateText(ByVal text As String) As Boolean
        If testForm.txBxDisplay.Text.Length > 0 Then
            testForm.txBxDisplay.AppendText(Environment.NewLine & text)
        Else
            testForm.txBxDisplay.Text = text
        End If
        Return True
    End Function

    ' this works by invoking a delegate pointing to itself (the reentrant part) with the difference being
    ' that the 2nd time through, since the delegate was invoked on the component, it's on the component's thread
    ' so it jumps to the 'else' portion of the if-else conditional
    Private Function decorateTextInvokeWhenNecessary(ByVal text As String) As Boolean
        If testForm.txBxDisplay.InvokeRequired Then
            testForm.txBxDisplay.Invoke(New delegateDisplayText(AddressOf decorateTextInvokeWhenNecessary), New Object() {"(reentrant InvokeRequired)" & text})
        Else
            testForm.txBxDisplay.AppendText(Environment.NewLine & "(reentrant) " & text)
        End If
        Return True
    End Function

    Private Sub closeForms(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        ' display text from this newly created thread
        decorateTextInvokeWhenNecessary("closing forms from close thread")
        ' wait long enough to read text displayed on the form
        Threading.Thread.Sleep(2000)

        ' test here after displaying text is finished and immediately prior to closing form
        ' user might have closed the form already
        If testForm.Visible Then
            Assert.IsTrue(testForm.InvokeRequired)
            Assert.IsTrue(testForm.txBxDisplay.InvokeRequired)
        End If

        closeTestForm()
        closeAddTextForm()

        Assert.IsFalse(testForm.Visible)
    End Sub

    ' directly invoke class.method
    Private Sub closeTestForm()
        If testForm.txBxDisplay.InvokeRequired Then
            testForm.txBxDisplay.Invoke(New Windows.Forms.MethodInvoker(AddressOf testForm.Close))
        Else
            testForm.Close()
        End If
    End Sub

    ' directly invoke class.method
    Private Sub closeAddTextForm()
        If addTextForm.InvokeRequired Then
            addTextForm.Invoke(New Windows.Forms.MethodInvoker(AddressOf addTextForm.Close))
        Else
            addTextForm.Close()
        End If
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class
