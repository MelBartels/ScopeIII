#Region "Imports"
#End Region

Public Class KingRate

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private cosHA As Double
    Private sinHA As Double
    Private cosDec As Double
    Private sinDec As Double
    Private tanDec As Double
    Private cosLat As Double
    Private sinLat As Double
    Private cotLat As Double

    Private pRefract As Refract
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As KingRate
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As KingRate = New KingRate
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pRefract = Refract.GetInstance
    End Sub

    Public Shared Function GetInstance() As KingRate
        Return New KingRate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function AtRefractedPole(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double, ByVal refractedLatitudeRad As Double) As Double
        Dim dir As Double = 1
        If refractedLatitudeRad < 0 Then
            refractedLatitudeRad = -refractedLatitudeRad
            DecRad = -DecRad
            dir = -1
        End If

        setCosSin(RaRad, DecRad, SidTRad, refractedLatitudeRad)

        Return dir * (1436.07 + 0.4 * (cosLat / cosDec * (cosLat * cosDec + sinLat * sinDec * cosHA) / Math.Pow(sinLat * sinDec + cosLat * cosDec * cosHA, 2) - cotLat * tanDec * cosHA))
    End Function

    Public Function AtTruePole(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double, ByVal latitudeRad As Double) As Double
        Dim absLatitudeRad As Double = Math.Abs(latitudeRad)
        Dim refractedLatitudeRad As Double = absLatitudeRad + pRefract.Calc(absLatitudeRad).Rad
        If latitudeRad < 0 Then
            refractedLatitudeRad = -refractedLatitudeRad
        End If

        Return AtRefractedPole(RaRad, DecRad, SidTRad, refractedLatitudeRad)
    End Function

    Public Function SidTrackRatio(ByVal kingRate As Double) As Double
        ' 21600 = # of 15" in 24 hrs; equation is equivalent of 1440/sidRate/KingRate*15
        Return 21600 / Units.SidRate / kingRate
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub setCosSin(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double, ByVal latitudeRad As Double)
        Dim HA As Double = eMath.ValidRadPi(RaRad - SidTRad)
        cosHA = Math.Cos(HA)
        sinHA = Math.Sin(HA)
        cosDec = Math.Cos(DecRad)
        sinDec = Math.Sin(DecRad)
        tanDec = Math.Tan(DecRad)
        cosLat = Math.Cos(latitudeRad)
        sinLat = Math.Sin(latitudeRad)
        cotLat = eMath.Cot(latitudeRad)
    End Sub
#End Region

End Class
