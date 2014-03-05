Public Class MainGaugeCoordOK
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
    Protected pCoordinateObserverPresenter As CoordinateObserverPresenter
    Protected pGaugeCoordOKPresenter As GaugeCoordOKPresenter
    Protected pConnected As Int32
    Protected pMutex As New Threading.Mutex
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainGaugeCoordOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainGaugeCoordOK = New MainGaugeCoordOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainGaugeCoordOK
        Return New MainGaugeCoordOK
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim frmThreadSlider As New Threading.Thread(AddressOf gaugeForm)
        frmThreadSlider.Start()
        Dim frmThreadObs As New Threading.Thread(AddressOf observerForm)
        frmThreadObs.Start()
    End Sub

    Protected Sub gaugeForm()
        pGaugeCoordOKPresenter = GaugeCoordOKPresenter.GetInstance
        pGaugeCoordOKPresenter.IMVPView = New FrmGaugeCoordOK
        pGaugeCoordOKPresenter.IRendererCoordPresenter.SetCoordinateName("Gauge")
        addToConnect()
        pGaugeCoordOKPresenter.ShowDialog()
    End Sub

    Protected Sub observerForm()
        pCoordinateObserverPresenter = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenter.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenter.UserCtrlCoordPresenter.SetCoordinateName("Observer")
        addToConnect()
        pCoordinateObserverPresenter.ShowDialog()
    End Sub

    Protected Sub connectObserver()
        ' create an observer (class below), and set its presenter to the presenter instantiated above
        Dim CoordinateObserver As CoordinateObserver = New CoordinateObserver
        CoordinateObserver.CoordinateObserverPresenter = pCoordinateObserverPresenter
        ' now attach the observer just created to the presenter's coordinate observer
        pGaugeCoordOKPresenter.IRendererCoordPresenter.CoordinateObservableImp.Attach(CType(CoordinateObserver, IObserver))
    End Sub

    Protected Sub addToConnect()
        pMutex.WaitOne()
        pConnected += 1
        If pConnected.Equals(2) Then
            connectObserver()
        End If
        pMutex.ReleaseMutex()
    End Sub
#End Region

End Class
