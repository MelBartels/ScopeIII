Public Class CorrectedRatesParmsFacade

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pCelestialErrorsCalculator As CelestialErrorsCalculator
    Private pStartingCorrectedEquatPosition As Position
    Private pToPosition As Position
    Private pFromEpoch As Date
    Private pToEpoch As Date
    Private pIncludePrecession As Boolean
    Private pIncludeNutationAnnualAberration As Boolean
    Private pIncludeRefraction As Boolean
    Private pLatitudeRad As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CorrectedRatesParmsFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CorrectedRatesParmsFacade = New CorrectedRatesParmsFacade
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CorrectedRatesParmsFacade
        Return New CorrectedRatesParmsFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub SetParms( _
            ByRef celestialErrorsCalculator As CelestialErrorsCalculator, _
            ByRef startingCorrectedEquatPosition As Position, _
            ByRef toPosition As Position, _
            ByRef fromEpoch As Date, _
            ByRef toEpoch As Date, _
            ByVal includePrecession As Boolean, _
            ByVal includeNutationAnnualAberration As Boolean, _
            ByVal includeRefraction As Boolean, _
            ByVal latitudeRad As Double)

        pCelestialErrorsCalculator = celestialErrorsCalculator
        pStartingCorrectedEquatPosition = startingCorrectedEquatPosition
        pToPosition = toPosition
        pFromEpoch = fromEpoch
        pToEpoch = toEpoch
        pIncludePrecession = includePrecession
        pIncludeNutationAnnualAberration = includeNutationAnnualAberration
        pIncludeRefraction = includeRefraction
        pLatitudeRad = latitudeRad
    End Sub

    Public Function GetUncorrectedEquat(ByRef fromPosition As Position) As Double()
        Dim RaDec(1) As Double

        RaDec(0) = fromPosition.RA.Rad
        RaDec(1) = fromPosition.Dec.Rad

        If pCelestialErrorsCalculator IsNot Nothing Then
            pCelestialErrorsCalculator.CalculateErrors( _
                    fromPosition, _
                    pToPosition, _
                    pFromEpoch, _
                    pToEpoch, _
                    pIncludePrecession, _
                    pIncludeNutationAnnualAberration, _
                    pIncludeRefraction, _
                    False, _
                    pLatitudeRad)
            ' toPosition contains updated values
            RaDec(0) = pToPosition.RA.Rad
            RaDec(1) = pToPosition.Dec.Rad
        End If

        Return RaDec
    End Function

    Public Function GetCorrectedEquat(ByRef fromPosition As Position) As Double()
        Dim RaDec(1) As Double

        RaDec(0) = fromPosition.RA.Rad
        RaDec(1) = fromPosition.Dec.Rad

        If pCelestialErrorsCalculator IsNot Nothing Then
            pCelestialErrorsCalculator.CalculateErrors( _
                    fromPosition, _
                    pToPosition, _
                    pFromEpoch, _
                    pToEpoch, _
                    pIncludePrecession, _
                    pIncludeNutationAnnualAberration, _
                    pIncludeRefraction, _
                    True, _
                    pLatitudeRad)
            ' toPosition contains updated values
            RaDec(0) = pToPosition.RA.Rad
            RaDec(1) = pToPosition.Dec.Rad
        End If

        Return RaDec
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region
End Class
