Public Interface ICoordPresenter
    Property CoordinateObservableImp() As ObservableImp
    Property CoordUpdatedByMe() As Boolean
    Property CoordExpType() As ISFT
    Property Coordinate() As Coordinate
    Sub SetCoordinateName(ByVal name As String)
    Sub SetCoordinateLabelColor(ByVal color As Drawing.Color)
    Sub DisplayCoordinate(ByVal angleRad As Double)
End Interface
