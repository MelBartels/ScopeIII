Imports NUnit.Framework

<TestFixture()> Public Class TestArcGauge

    Private pArcGaugeOKPresenter As ArcGaugeOKPresenter
    Private pValueObserverPresenter As ValueObserverPresenter
    Private pConnected As Int32
    Private Shared pMutex As New Threading.Mutex

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestArcGauge()
        Dim threadStartup As New Threading.Thread(AddressOf startup)
        threadStartup.Start()

        Dim frmTimer As New Timers.Timer(10000)
        AddHandler frmTimer.Elapsed, AddressOf killFrms
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private Sub startup()
        Dim frmThreadArcGauge As New Threading.Thread(AddressOf ArcGaugeForm)
        frmThreadArcGauge.Start()
        Dim frmThreadObs As New Threading.Thread(AddressOf observerForm)
        frmThreadObs.Start()
    End Sub

    Private Sub ArcGaugeForm()
        pArcGaugeOKPresenter = ArcGaugeOKPresenter.GetInstance
        pArcGaugeOKPresenter.IMVPView = New FrmArcGaugeOK
        pArcGaugeOKPresenter.IRenderer = ScalarArcGaugeRenderer.GetInstance
        addToConnect()
        pArcGaugeOKPresenter.ShowDialog()
    End Sub

    Private Sub observerForm()
        pValueObserverPresenter = ValueObserverPresenter.GetInstance
        pValueObserverPresenter.IMVPView = New FrmValueObserver
        addToConnect()
        pValueObserverPresenter.ShowDialog()
    End Sub

    Public Sub addToConnect()
        pMutex.WaitOne()
        pConnected += 1
        If pConnected.Equals(2) Then
            connectObserver()
        End If
        pMutex.ReleaseMutex()
    End Sub

    Private Sub connectObserver()
        ' create an observer and set its presenter to the presenter instantiated above
        Dim valueObserver As ValueObserver = Forms.ValueObserver.GetInstance
        valueObserver.ValueObserverPresenter = pValueObserverPresenter
        ' now attach the observer just created to the presenter's value observer
        pArcGaugeOKPresenter.ObservableImp.Attach(CType(valueObserver, IObserver))
        ' set starting value
        pArcGaugeOKPresenter.Value = 0
    End Sub

    Private Sub killFrms(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pValueObserverPresenter.Close()
        pArcGaugeOKPresenter.Close()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class
