Public Class TrigRates
    Inherits RatesBase


#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TrigRates
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TrigRates = New TrigRates
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pInitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertTrig, ISFT), Nothing)
    End Sub

    Public Shared Function GetInstance() As TrigRates
        Return New TrigRates
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Sub CalcRates()
        CalcRatesAltazViaElapsedTimeFRViaFormula()
    End Sub

    Public Overrides Sub CalcCorrectedRates( _
        ByRef celestialErrorsCalculator As CelestialErrorsCalculator, _
        ByRef startingCorrectedEquatPosition As Position, _
        ByRef toPosition As Position, _
        ByRef fromEpoch As Date, _
        ByRef toEpoch As Date, _
        ByVal includePrecession As Boolean, _
        ByVal includeNutationAnnualAberration As Boolean, _
        ByVal includeRefraction As Boolean, _
        ByVal latitudeRad As Double)

        buildCorrectedRatesParmsFacade( _
                celestialErrorsCalculator, _
                startingCorrectedEquatPosition, _
                toPosition, _
                fromEpoch, _
                toEpoch, _
                includePrecession, _
                includeNutationAnnualAberration, _
                includeRefraction, _
                latitudeRad)

        CalcCorrectedRatesViaElapsedTime(startingCorrectedEquatPosition)
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
