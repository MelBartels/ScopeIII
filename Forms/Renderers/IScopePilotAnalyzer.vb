Public Interface IScopePilotAnalyzer
    Sub Init(ByRef G As Drawing.Graphics, _
             ByRef Pd As PointData, _
             ByVal GSize As Drawing.Size, _
             ByVal GMidX As Int32, _
             ByVal GMidY As Int32, _
             ByVal BackPlotPen As Drawing.Pen, _
             ByVal ForePlotPen As Drawing.Pen, _
             ByVal RulerText As String, _
             ByVal RulerFont As Drawing.Font, _
             ByVal RulerBrush As Drawing.SolidBrush, _
             ByVal RimDimen As Int32, _
             ByVal GlobeRadius As Double, _
             ByVal RimUlPoint As Drawing.Point)
    Sub DrawAnalysis()
End Interface
