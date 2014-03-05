Public Class MainGauge2AxisCoordOK
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
    Private pGauge2AxisCoordOKPresenter As Gauge2AxisCoordOKPresenter
    Protected pConnected As Int32
    Protected pMutex As New Threading.Mutex
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainGauge2AxisCoordOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainGauge2AxisCoordOK = New MainGauge2AxisCoordOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainGauge2AxisCoordOK
        Return New MainGauge2AxisCoordOK
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
    End Sub

    Protected Sub gaugeForm()
        pGauge2AxisCoordOKPresenter = Gauge2AxisCoordOKPresenter.GetInstance
        pGauge2AxisCoordOKPresenter.IMVPView = New FrmGauge2AxisCoordOK
        pGauge2AxisCoordOKPresenter.IGauge2AxisCoordPresenter.SetAxisNames(CoordName.PriAxis.Description, CoordName.SecAxis.Description)
        addToConnect()
        pGauge2AxisCoordOKPresenter.ShowDialog()
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

    Protected Sub addToConnect()
        pMutex.WaitOne()
        pConnected += 1
        If pConnected.Equals(3) Then
            connectObservers()
        End If
        pMutex.ReleaseMutex()
    End Sub

    Protected Sub connectObservers()
        ' create an observer (class below), and set its presenter to the presenter instantiated above
        Dim CoordinateObserverPri As CoordinateObserver = New CoordinateObserver
        CoordinateObserverPri.CoordinateObserverPresenter = pCoordinateObserverPresenterPri
        ' now attach the observer just created to the presenter's coordinate observer
        pGauge2AxisCoordOKPresenter.IGauge2AxisCoordPresenter.CoordinatePriObservableImp.Attach(CType(CoordinateObserverPri, IObserver))

        ' create an observer (class below), and set its presenter to the presenter instantiated above
        Dim CoordinateObserverSec As CoordinateObserver = New CoordinateObserver
        CoordinateObserverSec.CoordinateObserverPresenter = pCoordinateObserverPresenterSec
        ' now attach the observer just created to the presenter's coordinate observer
        pGauge2AxisCoordOKPresenter.IGauge2AxisCoordPresenter.CoordinateSecObservableImp.Attach(CType(CoordinateObserverSec, IObserver))
    End Sub
#End Region

End Class
