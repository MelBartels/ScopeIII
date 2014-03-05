' Interface for all coordinate expressions.
' See 'formatting for different cultures' where:
' Dim ci As CultureInfo = New CultureInfo("de-DE")
' formatting on the spot can be changed: num.ToString("d", ci)
' threads can be changed: Thread.CurrentThread.CurrentCulture = New CultureInfo("fr-BE") : num.ToString("d")

Public Interface ICoordExp
    Property CoordExpType() As ISFT
    Function ToString(ByVal rad As Double) As String
End Interface
