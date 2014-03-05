Public Interface IGaugePositionPresenter
    Property IGauge2AxisCoordPresenterAltaz() As IGauge2AxisCoordPresenter
    Property IGauge2AxisCoordPresenterEquat() As IGauge2AxisCoordPresenter
    Property UserCtrlGaugeCoordPresenterSidT() As IRendererCoordPresenter
    ReadOnly Property Position() As Position
    Sub DisplayPosition(ByVal position As Position)
End Interface

