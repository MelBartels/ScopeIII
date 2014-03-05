#Region "imports"
#End Region

Public Class ShowCelestialErrorsInputPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmShowCelestialErrorsInput As FrmShowCelestialErrorsInput

    Private pIGauge2AxisCoordPresenterEquat As IGauge2AxisCoordPresenter
    Private pUserCtrlGaugeCoordPresenterSidT As UserCtrlGaugeCoordPresenter
    Private pUserCtrlGaugeCoordPresenterLat As UserCtrlGaugeCoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ShowCelestialErrorsInputPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ShowCelestialErrorsInputPresenter = New ShowCelestialErrorsInputPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ShowCelestialErrorsInputPresenter
        Return New ShowCelestialErrorsInputPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub CoordinateObserver(ByRef IObserver As IObserver)
        pIGauge2AxisCoordPresenterEquat.CoordinatePriObservableImp.Attach(IObserver)
        pIGauge2AxisCoordPresenterEquat.CoordinateSecObservableImp.Attach(IObserver)
        pUserCtrlGaugeCoordPresenterSidT.CoordinateObservableImp.Attach(IObserver)
        pUserCtrlGaugeCoordPresenterLat.CoordinateObservableImp.Attach(IObserver)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmShowCelestialErrorsInput = CType(IMVPView, FrmShowCelestialErrorsInput)

        pIGauge2AxisCoordPresenterEquat = UserCtrlGauge2AxisCoordPresenter.GetInstance
        CType(pIGauge2AxisCoordPresenterEquat, UserCtrlGauge2AxisCoordPresenter).IMVPUserCtrl = pFrmShowCelestialErrorsInput.UserCtrlGauge2AxisCoordEquat
        pIGauge2AxisCoordPresenterEquat.SetAxisNames(CoordName.RA.Description, CoordName.Dec.Description)
        pIGauge2AxisCoordPresenterEquat.SetExpCoordTypes(CoordExpType.FormattedHMSM, CoordExpType.FormattedDMS)
        pIGauge2AxisCoordPresenterEquat.SetRenderers(HourCircGaugeRenderer.GetInstance, DeclinationCircGaugeRenderer.GetInstance)

        pUserCtrlGaugeCoordPresenterSidT = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenterSidT.IMVPUserCtrl = pFrmShowCelestialErrorsInput.UserCtrlGaugeCoordSidT
        pUserCtrlGaugeCoordPresenterSidT.SetCoordinateName(CoordName.SidT.Description)
        pUserCtrlGaugeCoordPresenterSidT.CoordExpType = CoordExpType.FormattedHMSM
        pUserCtrlGaugeCoordPresenterSidT.IRenderer = HourCircGaugeRenderer.GetInstance

        pUserCtrlGaugeCoordPresenterLat = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenterLat.IMVPUserCtrl = pFrmShowCelestialErrorsInput.UserCtrlGaugeCoordLat
        pUserCtrlGaugeCoordPresenterLat.SetCoordinateName(CoordName.Latitude.Description)
        pUserCtrlGaugeCoordPresenterLat.IRenderer = DeclinationCircGaugeRenderer.GetInstance
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
