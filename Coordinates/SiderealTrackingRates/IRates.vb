Public Interface IRates
    Function InitStateTemplate() As InitStateTemplate
    Function Init() As Boolean
    Function PriAxisTrackRate() As TrackRatesDataModel.TrackRate
    Function SecAxisTrackRate() As TrackRatesDataModel.TrackRate
    Function TierAxisTrackRate() As TrackRatesDataModel.TrackRate
    Sub CalcRates()
    Sub CalcCorrectedRates( _
            ByRef celestialErrorsCalculator As CelestialErrorsCalculator, _
            ByRef startingCorrectedEquatPosition As Position, _
            ByRef toPosition As Position, _
            ByRef fromEpoch As DateTime, _
            ByRef toEpoch As DateTime, _
            ByVal includePrecession As Boolean, _
            ByVal includeNutationAnnualAberration As Boolean, _
            ByVal includeRefraction As Boolean, _
            ByVal latitudeRad As Double)
    Function GetFieldRotationAngle() As Double
End Interface
