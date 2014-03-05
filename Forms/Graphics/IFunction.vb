Public Interface IFunction
    Property MinX() As Double
    Property MaxX() As Double
    Property MinY() As Double
    Property MaxY() As Double
    Function Y(ByVal X As Double) As Double
    Function ToString() As String
End Interface
