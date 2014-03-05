Public Interface IGauge2AxisCoordPresenter
    Property CoordinatePri() As Coordinate
    Property CoordinateSec() As Coordinate
    Property CoordinatePriObservableImp() As ObservableImp
    Property CoordinateSecObservableImp() As ObservableImp
    Property PriCoordUpdatedByMe() As Boolean
    Property SecCoordUpdatedByMe() As Boolean
    Sub SetAxisNames(ByVal priName As String, ByVal secName As String)
    Sub SetExpCoordTypes(ByVal priExpCoordType As ISFT, ByVal secExpCoordType As ISFT)
    Sub SetCoordinateLabelColors(ByVal priColor As Drawing.Color, ByVal secColor As Drawing.Color)
    Sub SetRenderers(ByVal priRenderer As IRenderer, ByVal secRenderer As IRenderer)
    Sub DisplayCoordinates(ByVal priRad As Double, ByVal secRad As Double)
    Sub Render()
End Interface

