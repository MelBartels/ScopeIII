Public Interface IRenderer2MeasurementsGauge
    Inherits IRenderer

    Function InsideGauge(ByVal point As Drawing.Point) As Boolean
    Function MeasurementsFromObjectToRender() As Object
    Function MeasurementsToPoint(ByVal point As Drawing.Point) As Object
End Interface