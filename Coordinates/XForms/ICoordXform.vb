Public Interface ICoordXform
    Property CoordXformType() As ISFT
    Property Site() As Coordinates.Site
    Property Position() As Position
    Property MeridianFlip() As MeridianFlip
    Function GetAltaz() As Boolean
    Function GetEquat() As Boolean
    Sub TranslateAltazAcrossPole()
End Interface
