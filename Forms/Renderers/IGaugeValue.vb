Public interface IGaugeValue
    Property UOM() As ISFT
    Property MaxValue() As Double
    Property MinValue() As Double
    Function Validate(ByVal value As Double) As Double
    Function ValueRange() As Double
    Function ScaleValue(ByVal percentOfScale As Double) As Double
    Function ScalePercent(ByVal value As Double) As Double
    Function ValueIncrement(ByVal incr As Double) As Double
end interface