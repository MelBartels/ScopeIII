Public Class MainRenderScalarGaugesOK
    Inherits MainPrototype

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pRenderScalarGaugesOKPresenter As RenderScalarGaugesOKPresenter
    Protected pValueObserverPresenterGauge1 As ValueObserverPresenter
    Protected pValueObserverPresenterGauge2 As ValueObserverPresenter
    Protected pValueObserverPresenterGauge3 As ValueObserverPresenter
    Protected pConnected As Int32
    Protected pMutex As New Threading.Mutex
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainRenderScalarGaugesOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainRenderScalarGaugesOK = New MainRenderScalarGaugesOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainRenderScalarGaugesOK
        Return New MainRenderScalarGaugesOK
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim frmGauges As New Threading.Thread(AddressOf gaugesForm)
        frmGauges.Start()
        Dim frmThreadGauge1Obs As New Threading.Thread(AddressOf observerFormGauge1)
        frmThreadGauge1Obs.Start()
        Dim frmThreadGauge2Obs As New Threading.Thread(AddressOf observerFormGauge2)
        frmThreadGauge2Obs.Start()
        Dim frmThreadGauge3Obs As New Threading.Thread(AddressOf observerFormGauge3)
        frmThreadGauge3Obs.Start()
    End Sub

    Protected Sub gaugesForm()
        pRenderScalarGaugesOKPresenter = Forms.RenderScalarGaugesOKPresenter.GetInstance
        pRenderScalarGaugesOKPresenter.IMVPView = New FrmRenderScalarGaugesOK
        pRenderScalarGaugesOKPresenter.RenderGaugeUtil1.IRendererGauge = SliderRenderer.GetInstance
        pRenderScalarGaugesOKPresenter.RenderGaugeUtil2.IRendererGauge = ScalarArcGaugeRenderer.GetInstance
        pRenderScalarGaugesOKPresenter.RenderGaugeUtil3.IRendererGauge = LogArcGaugeRenderer.GetInstance
        addToConnect()
        pRenderScalarGaugesOKPresenter.ShowDialog()
    End Sub

    Protected Sub observerFormGauge1()
        buildObserverForm(pValueObserverPresenterGauge1)
    End Sub

    Protected Sub observerFormGauge2()
        buildObserverForm(pValueObserverPresenterGauge2)
    End Sub

    Protected Sub observerFormGauge3()
        buildObserverForm(pValueObserverPresenterGauge3)
    End Sub

    Protected Sub buildObserverForm(ByRef valueObserverPresenter As ValueObserverPresenter)
        valueObserverPresenter = valueObserverPresenter.GetInstance
        valueObserverPresenter.IMVPView = New FrmValueObserver
        addToConnect()
        valueObserverPresenter.ShowDialog()
    End Sub

    Protected Sub addToConnect()
        pMutex.WaitOne()
        pConnected += 1
        If pConnected.Equals(4) Then
            connectObservers()
        End If
        pMutex.ReleaseMutex()
    End Sub

    Protected Sub connectObservers()
        connectObserver(pValueObserverPresenterGauge1, pRenderScalarGaugesOKPresenter.RenderGaugeUtil1)
        connectObserver(pValueObserverPresenterGauge2, pRenderScalarGaugesOKPresenter.RenderGaugeUtil2)
        connectObserver(pValueObserverPresenterGauge3, pRenderScalarGaugesOKPresenter.RenderGaugeUtil3)
    End Sub

    Protected Sub connectObserver(ByVal valueObserverPresenter As ValueObserverPresenter, ByVal renderGaugeUtil As RenderScalarGaugesOKPresenter.RenderGaugeUtil)
        ' create an observer and set its presenter to the presenter instantiated above
        Dim valueObserver As ValueObserver = Forms.ValueObserver.GetInstance
        valueObserver.ValueObserverPresenter = valueObserverPresenter
        ' now attach the observer just created to the presenter's value observer
        renderGaugeUtil.ObservableImpGauge.Attach(CType(valueObserver, IObserver))
        ' set starting value
        renderGaugeUtil.ValueGauge = 0
    End Sub
#End Region

End Class
