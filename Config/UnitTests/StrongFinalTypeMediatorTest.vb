Imports NUnit.Framework

<TestFixture()> Public Class StrongFinalTypeMediatorTest

    Private pFrmThread As Threading.Thread
    Private pSelectStrongFinalTypeMediator As SelectStrongFinalTypeMediator

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    ', IgnoreAttribute("Form requires user interaction")
    <Test()> Public Sub TestSelectStrongFinalType()
        pFrmThread = New Threading.Thread(AddressOf getSelection)
        ' necessary for com registering of drag-n-drop
        pFrmThread.SetApartmentState(Threading.ApartmentState.STA)
        pFrmThread.Start()

        Dim frmTimerTV As New Timers.Timer(10000)
        AddHandler frmTimerTV.Elapsed, AddressOf killForm
        frmTimerTV.AutoReset = False
        frmTimerTV.Start()

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Sub getSelection()
        pSelectStrongFinalTypeMediator = Config.SelectStrongFinalTypeMediator.GetInstance

        ' test w/ some preselected values
        Dim ISFTFacade As ISFTFacade = SFTTest.GetInstance
        ISFTFacade.SelectedItems.Add(SFTTest.aaa)
        ISFTFacade.SelectedItems.Add(SFTTest.ccc)

        If pSelectStrongFinalTypeMediator.GetSelection(ISFTFacade, True) Then
            AppMsgBox.Show("You selected " & ISFTFacade.SelectedItemsAsCommaDelimitedString)
        Else
            'AppMsgBox.Show("You selected nothing.")
        End If

        Assert.IsTrue(True)
    End Sub

    Private Sub killForm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pSelectStrongFinalTypeMediator.CloseForm()
    End Sub
End Class
