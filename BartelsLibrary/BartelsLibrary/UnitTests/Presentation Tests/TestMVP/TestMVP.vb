Imports NUnit.Framework

<TestFixture()> Public Class TestMVP

    Private pTestAlbumPresenter As TestAlbumPresenter
    Private pTestAlbumPresenterNewThread As TestAlbumPresenter

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestStub()
        ' test creation of view then presenter 
        Dim frmTestMVPStub As FrmTestMVPStub = BartelsLibrary.FrmTestMVPStub.GetInstance
        Dim presenter As TestAlbumPresenter = TestAlbumPresenter.GetInstance
        presenter.IMVPView = frmTestMVPStub
        presenter.DataModel = createAlbums()

        ' test an eventhandler
        frmTestMVPStub.ComposerEnabled = True
        Assert.AreEqual(frmTestMVPStub.ComposerEnabled, True)
        frmTestMVPStub.IsClassical = False
        frmTestMVPStub.RaiseUpdated()
        Assert.AreEqual(frmTestMVPStub.ComposerEnabled, False)
    End Sub

    <Test()> Public Sub TestFrm()
        Dim frmThread As New Threading.Thread(AddressOf showFrm)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(60000)
        AddHandler frmTimer.Elapsed, AddressOf killFrm
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    <Test()> Public Sub TestShowDialogNewThread()
        Dim frmThread As New Threading.Thread(AddressOf showFrmNewThread)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(60000)
        AddHandler frmTimer.Elapsed, AddressOf killFrmNewThread
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private Function createAlbums() As TestAlbum()
        Dim testAlbums As TestAlbum() = New TestAlbum() {TestAlbum.GetInstance, _
                                                         TestAlbum.GetInstance, _
                                                         TestAlbum.GetInstance, _
                                                         TestAlbum.GetInstance}
        testAlbums(0).Title = "HQ"
        testAlbums(1).Title = "The Black Light"
        testAlbums(2).Title = "Feijao com Arroz"
        testAlbums(3).Title = "Symphony No.5"
        Return testAlbums
    End Function

    Private Sub showFrm()
        pTestAlbumPresenter = TestAlbumPresenter.GetInstance
        pTestAlbumPresenter.IMVPView = New FrmTestMVP
        pTestAlbumPresenter.DataModel = createAlbums()
        pTestAlbumPresenter.ShowDialog()
    End Sub

    Private Sub killFrm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pTestAlbumPresenter.Close()
    End Sub

    Private Sub showFrmNewThread()
        pTestAlbumPresenterNewThread = TestAlbumPresenter.GetInstance
        pTestAlbumPresenterNewThread.IMVPView = New FrmTestMVP
        pTestAlbumPresenterNewThread.DataModel = createAlbums()
        pTestAlbumPresenterNewThread.ShowDialog()
    End Sub

    Private Sub killFrmNewThread(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pTestAlbumPresenterNewThread.Close()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
