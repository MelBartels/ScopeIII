Public Interface IGauge3AxisCoordPresenter
    Property CoordinatePri() As Coordinate
    Property CoordinateSec() As Coordinate
    Property CoordinateTier() As Coordinate
    Property CoordinatePriObservableImp() As ObservableImp
    Property CoordinateSecObservableImp() As ObservableImp
    Property CoordinateTierObservableImp() As ObservableImp
    Property PriCoordUpdatedByMe() As Boolean
    Property SecCoordUpdatedByMe() As Boolean
    Property TierCoordUpdatedByMe() As Boolean
    Sub SetAxisNames(ByVal priName As String, ByVal secName As String, ByVal tierName As String)
    Sub SetExpCoordTypes(ByVal priExpCoordType As ISFT, ByVal secExpCoordType As ISFT, ByVal tierExpCoordType As ISFT)
    Sub SetCoordinateLabelColors(ByVal priColor As Drawing.Color, ByVal secColor As Drawing.Color, ByVal tierColor As Drawing.Color)
    Sub SetRenderers(ByVal priRenderer As IRenderer, ByVal secRenderer As IRenderer, ByVal tierRenderer As IRenderer)
    Sub DisplayCoordinates(ByVal priRad As Double, ByVal secRad As Double, ByVal tierRad As Double)
    Sub Render()
End Interface

