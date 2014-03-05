Public Class MainGauge3AxisCoordOK
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
    Private pCoordinateObserverPresenterPri As CoordinateObserverPresenter
    Private pCoordinateObserverPresenterSec As CoordinateObserverPresenter
    Private pCoordinateObserverPresenterTier As CoordinateObserverPresenter
    Private Shared pGauge3AxisCoordOKPresenter As Gauge3AxisCoordOKPresenter
    Protected pConnected As Int32
    Protected pMutex As New Threading.Mutex
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainGauge3AxisCoordOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainGauge3AxisCoordOK = New MainGauge3AxisCoordOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainGauge3AxisCoordOK
        Return New MainGauge3AxisCoordOK
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
        Dim frmThreadObsPri As New Threading.Thread(AddressOf observerFormPri)
        frmThreadObsPri.Start()
        Dim frmThreadObsSec As New Threading.Thread(AddressOf observerFormSec)
        frmThreadObsSec.Start()
        Dim frmThreadObsTier As New Threading.Thread(AddressOf observerFormTier)
        frmThreadObsTier.Start()
    End Sub

    Protected Sub gaugeForm()
        pGauge3AxisCoordOKPresenter = Gauge3AxisCoordOKPresenter.GetInstance
        pGauge3AxisCoordOKPresenter.IMVPView = New FrmGauge3AxisCoordOK
        pGauge3AxisCoordOKPresenter.IGauge3AxisCoordPresenter.SetAxisNames(CoordName.PriAxis.Description, CoordName.SecAxis.Description, CoordName.TierAxis.Description)
        addToConnect()
        pGauge3AxisCoordOKPresenter.ShowDialog()
    End Sub

    Protected Sub observerFormPri()
        pCoordinateObserverPresenterPri = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenterPri.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenterPri.UserCtrlCoordPresenter.SetCoordinateName(CoordName.PriAxis.Description)
        addToConnect()
        pCoordinateObserverPresenterPri.ShowDialog()
    End Sub

    Protected Sub observerFormSec()
        pCoordinateObserverPresenterSec = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenterSec.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenterSec.UserCtrlCoordPresenter.SetCoordinateName(CoordName.SecAxis.Description)
        addToConnect()
        pCoordinateObserverPresenterSec.ShowDialog()
    End Sub

    Protected Sub observerFormTier()
        pCoordinateObserverPresenterTier = CoordinateObserverPresenter.GetInstance
        pCoordinateObserverPresenterTier.IMVPView = New FrmCoordinateObserver
        pCoordinateObserverPresenterTier.UserCtrlCoordPresenter.SetCoordinateName(CoordName.TierAxis.Description)
        addToConnect()
        pCoordinateObserverPresenterTier.ShowDialog()
    End Sub

    Protected Sub connectObservers()
        ' create an observer (class below), and set its presenter to the presenter instantiated above
        Dim CoordinateObserverPri As CoordinateObserver = New CoordinateObserver
        CoordinateObserverPri.CoordinateObserverPresenter = pCoordinateObserverPresenterPri
        ' now attach the observer just created to the presenter's coordinate observer
        pGauge3AxisCoordOKPresenter.IGauge3AxisCoordPresenter.CoordinatePriObservableImp.Attach(CType(CoordinateObserverPri, IObserver))

        ' create an observer (class below), and set its presenter to the presenter instantiated above
        Dim CoordinateObserverSec As CoordinateObserver = New CoordinateObserver
        CoordinateObserverSec.CoordinateObserverPresenter = pCoordinateObserverPresenterSec
        ' now attach the observer just created to the presenter's coordinate observer
        pGauge3AxisCoordOKPresenter.IGauge3AxisCoordPresenter.CoordinateSecObservableImp.Attach(CType(CoordinateObserverSec, IObserver))

        ' create an observer (class below), and set its presenter to the presenter instantiated above
        Dim CoordinateObserverTier As CoordinateObserver = New CoordinateObserver
        CoordinateObserverTier.CoordinateObserverPresenter = pCoordinateObserverPresenterTier
        ' now attach the observer just created to the presenter's coordinate observer
        pGauge3AxisCoordOKPresenter.IGauge3AxisCoordPresenter.CoordinateTierObservableImp.Attach(CType(CoordinateObserverTier, IObserver))
    End Sub

    Protected Sub addToConnect()
        pMutex.WaitOne()
        pConnected += 1
        If pConnected.Equals(4) Then
            connectObservers()
        End If
        pMutex.ReleaseMutex()
    End Sub
#End Region

End Class
