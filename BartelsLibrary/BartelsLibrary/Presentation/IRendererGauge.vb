Public Interface IRendererGauge
    Inherits IRenderer

    Function InsideGauge(ByVal point As Drawing.Point) As Boolean
    Function MeasurementFromObjectToRender() As Double
    Function MeasurementToPoint(ByVal point As Drawing.Point) As Double
End Interface