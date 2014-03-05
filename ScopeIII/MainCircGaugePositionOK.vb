Public Class MainGaugePositionOK
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
    Private pGaugePositionOKPresenter As GaugePositionOKPresenter

    Private pCoordinateObserverPresenterRa As CoordinateObserverPresenter
    Private pCoordinateObserverPresenterDec As CoordinateObserverPresenter
    Private pCoordinateObserverPresenterAz As CoordinateObserverPresenter
    Private pCoordinateObserverPresenterAlt As CoordinateObserverPresenter
    Private pCoordinateObserverPresenterSidT As CoordinateObserverPresenter

    Protected pConnected As Int32
    Protected pMutex As New Threading.Mutex
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainGaugePositionOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainGaugePositionOK = New MainGaugePositionOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainGaugePositionOK
        Return New MainGaugePositionOK
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim frmThreadSlider As Threading.Thread = New Threading.Thread(AddressOf gaugeForm)
        frmThreadSlider.Start()
        Dim frmThreadObsRA As Threading.Thread = New Threading.Thread(AddressOf observerFormRA)
        frmThreadObsRA.Start()
        Dim frmThreadObsDec As Threading.Thread = New Threading.Thread(AddressOf observerFormDec)
        frmThreadObsDec.Start()
        Dim frmThreadObsAz As Threading.Thread = New Threading.Thread(AddressOf observerFormAz)
        frmThreadObsAz.Start()
        Dim frmThreadObsAlt As Threading.Thread = New Threading.Thread(AddressOf observerFormAlt)
        frmThreadObsAlt.Start()
        Dim frmThreadObsSidT As Threading.Thread = New Threading.Thread(AddressOf observerFormSidT)
        frmThreadObsSidT.Start()
    End Sub

    Protected Sub gaugeForm()
        pGaugePositionOKPresenter = GaugePositionOKPresenter.GetInstance
        pGaugePositionOKPresenter.IMVPView = New FrmGaugePositionOK
        addToConnect(6)
        pGaugePositionOKPresenter.ShowDialog()
    End Sub

    Protected Sub observerFormRA()
        pCoordinateObserverPresenterRa = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenterRa.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenterRa.UserCtrlCoordPresenter.SetCoordinateName(CoordName.RA.Description)
        addToConnect(6)
        pCoordinateObserverPresenterRa.ShowDialog()
    End Sub

    Protected Sub observerFormDec()
        pCoordinateObserverPresenterDec = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenterDec.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenterDec.UserCtrlCoordPresenter.SetCoordinateName(CoordName.Dec.Description)
        addToConnect(6)
        pCoordinateObserverPresenterDec.ShowDialog()
    End Sub

    Protected Sub observerFormAz()
        pCoordinateObserverPresenterAz = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenterAz.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenterAz.UserCtrlCoordPresenter.SetCoordinateName(CoordName.Az.Description)
        addToConnect(6)
        pCoordinateObserverPresenterAz.ShowDialog()
    End Sub

    Protected Sub observerFormAlt()
        pCoordinateObserverPresenterAlt = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenterAlt.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenterAlt.UserCtrlCoordPresenter.SetCoordinateName(CoordName.Alt.Description)
        addToConnect(6)
        pCoordinateObserverPresenterAlt.ShowDialog()
    End Sub

    Protected Sub observerFormSidT()
        pCoordinateObserverPresenterSidT = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenterSidT.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenterSidT.UserCtrlCoordPresenter.SetCoordinateName(CoordName.SidT.Description)
        addToConnect(6)
        pCoordinateObserverPresenterSidT.ShowDialog()
    End Sub

    Protected Sub connectObservers()
        ' RA observer
        ' create an observer (class below), and set its presenter to the presenter instantiated above
        Dim CoordinateObserverRa As CoordinateObserver = New CoordinateObserver
        CoordinateObserverRa.CoordinateObserverPresenter = pCoordinateObserverPresenterRa
        CoordinateObserverRa.CoordinateObserverPresenter.UserCtrlCoordPresenter.CoordExpType = CType(CoordExpType.FormattedHMSM, ISFT)
        ' now attach the observer just created to the presenter's coordinate observer
        pGaugePositionOKPresenter.IGauge2AxisCoordPresenterEquat.CoordinatePriObservableImp.Attach(CType(CoordinateObserverRa, IObserver))

        ' Dec observer
        Dim CoordinateObserverDec As CoordinateObserver = New CoordinateObserver
        CoordinateObserverDec.CoordinateObserverPresenter = pCoordinateObserverPresenterDec
        CoordinateObserverDec.CoordinateObserverPresenter.UserCtrlCoordPresenter.CoordExpType = CType(CoordExpType.FormattedDMS, ISFT)
        pGaugePositionOKPresenter.IGauge2AxisCoordPresenterEquat.CoordinateSecObservableImp.Attach(CType(CoordinateObserverDec, IObserver))

        ' Az observer
        Dim CoordinateObserverAz As CoordinateObserver = New CoordinateObserver
        CoordinateObserverAz.CoordinateObserverPresenter = pCoordinateObserverPresenterAz
        pGaugePositionOKPresenter.IGauge2AxisCoordPresenterAltaz.CoordinatePriObservableImp.Attach(CType(CoordinateObserverAz, IObserver))

        ' Alt observer
        Dim CoordinateObserverAlt As CoordinateObserver = New CoordinateObserver
        CoordinateObserverAlt.CoordinateObserverPresenter = pCoordinateObserverPresenterAlt
        pGaugePositionOKPresenter.IGauge2AxisCoordPresenterAltaz.CoordinateSecObservableImp.Attach(CType(CoordinateObserverAlt, IObserver))

        ' SidT observer
        Dim CoordinateObserverSidT As CoordinateObserver = New CoordinateObserver
        CoordinateObserverSidT.CoordinateObserverPresenter = pCoordinateObserverPresenterSidT
        CoordinateObserverSidT.CoordinateObserverPresenter.UserCtrlCoordPresenter.CoordExpType = CType(CoordExpType.FormattedHMSM, ISFT)
        pGaugePositionOKPresenter.IRendererCoordPresenterSidT.CoordinateObservableImp.Attach(CType(CoordinateObserverSidT, IObserver))
    End Sub

    Protected Sub addToConnect(ByVal connectCount As Int32)
        pMutex.WaitOne()
        pConnected += 1
        If pConnected.Equals(connectCount) Then
            connectObservers()
        End If
        pMutex.ReleaseMutex()
    End Sub
#End Region

End Class
