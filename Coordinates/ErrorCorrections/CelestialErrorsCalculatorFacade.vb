#Region "Imports"
#End Region

Public Class CelestialErrorsCalculatorFacade

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Property FromPosition() As Position
        Get
            Return pCelestialErrorsCalculator.FromPosition
        End Get
        Set(ByVal value As Position)
            pCelestialErrorsCalculator.FromPosition = value
        End Set
    End Property

    Public Property ToPosition() As Position
        Get
            Return pCelestialErrorsCalculator.ToPosition
        End Get
        Set(ByVal value As Position)
            pCelestialErrorsCalculator.ToPosition = value
        End Set
    End Property

    Public Property FromDate() As DateTime
        Get
            Return pCelestialErrorsCalculator.FromDate
        End Get
        Set(ByVal value As DateTime)
            pCelestialErrorsCalculator.FromDate = value
        End Set
    End Property

    Public Property ToDate() As DateTime
        Get
            Return pCelestialErrorsCalculator.ToDate
        End Get
        Set(ByVal value As DateTime)
            pCelestialErrorsCalculator.ToDate = value
        End Set
    End Property

    Public Property UncorrectedToCorrected() As Boolean
        Get
            Return pCelestialErrorsCalculator.UncorrectedToCorrected
        End Get
        Set(ByVal value As Boolean)
            pCelestialErrorsCalculator.UncorrectedToCorrected = value
        End Set
    End Property

    Public Property IncludePrecession() As Boolean
        Get
            Return pCelestialErrorsCalculator.IncludePrecession
        End Get
        Set(ByVal value As Boolean)
            pCelestialErrorsCalculator.IncludePrecession = value
        End Set
    End Property

    Public Property IncludeNutationAnnualAberration() As Boolean
        Get
            Return pCelestialErrorsCalculator.IncludeNutationAnnualAberration
        End Get
        Set(ByVal value As Boolean)
            pCelestialErrorsCalculator.IncludeNutationAnnualAberration = value
        End Set
    End Property

    Public Property IncludeRefraction() As Boolean
        Get
            Return pCelestialErrorsCalculator.IncludeRefraction
        End Get
        Set(ByVal value As Boolean)
            pCelestialErrorsCalculator.IncludeRefraction = value
        End Set
    End Property

    Public Property LatitudeRad() As Double
        Get
            Return pCelestialErrorsCalculator.LatitudeRad
        End Get
        Set(ByVal value As Double)
            pCelestialErrorsCalculator.LatitudeRad = value
        End Set
    End Property

    Public Property CoordErrorArray() As CoordErrorArray
        Get
            Return pCelestialErrorsCalculator.CoordErrorArray
        End Get
        Set(ByVal value As CoordErrorArray)
            pCelestialErrorsCalculator.CoordErrorArray = value
        End Set
    End Property

    Public Property CelestialErrorsPosition() As Position
        Get
            Return pCelestialErrorsCalculator.CelestialErrorsPosition
        End Get
        Set(ByVal value As Position)
            pCelestialErrorsCalculator.CelestialErrorsPosition = value
        End Set
    End Property

    Public Property DeltaRa() As Double
        Get
            Return pCelestialErrorsCalculator.DeltaRa
        End Get
        Set(ByVal value As Double)
            pCelestialErrorsCalculator.DeltaRa = value
        End Set
    End Property

    Public Property DeltaDec() As Double
        Get
            Return pCelestialErrorsCalculator.DeltaDec
        End Get
        Set(ByVal value As Double)
            pCelestialErrorsCalculator.DeltaDec = value
        End Set
    End Property

    Public Property CelestialErrorsCalculator() As CelestialErrorsCalculator
        Get
            Return pCelestialErrorsCalculator
        End Get
        Set(ByVal value As CelestialErrorsCalculator)
            pCelestialErrorsCalculator = value
        End Set
    End Property

    Public Property UseCalculator() As Boolean
        Get
            Return pUseCalculator
        End Get
        Set(ByVal value As Boolean)
            pUseCalculator = value
        End Set
    End Property
#End Region

#Region "Private and Protected Members"
    Private pUseCalculator As Boolean
    Private pCelestialErrorsCalculator As CelestialErrorsCalculator
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CelestialErrorsCalculatorFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CelestialErrorsCalculatorFacade = New CelestialErrorsCalculatorFacade
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pCelestialErrorsCalculator = CelestialErrorsCalculator.GetInstance
    End Sub

    Public Shared Function GetInstance() As CelestialErrorsCalculatorFacade
        Return New CelestialErrorsCalculatorFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function AdaptForCelestialErrors( _
            ByVal RaRad As Double, _
            ByVal DecRad As Double, _
            ByVal sidTRad As Double, _
            ByVal latitudeRad As Double, _
            ByVal uncorrectedToCorrected As Boolean) As Double()

        If UseCalculator Then
            FromPosition.RA.Rad = RaRad
            FromPosition.Dec.Rad = DecRad
            FromPosition.SidT.Rad = sidTRad

            AdaptForCelestialErrors( _
                    FromPosition, _
                    ToPosition, _
                    FromDate, _
                    ToDate, _
                    IncludePrecession, _
                    IncludeNutationAnnualAberration, _
                    IncludeRefraction, _
                    uncorrectedToCorrected, _
                    latitudeRad)

            Return New Double() {ToPosition.RA.Rad, ToPosition.Dec.Rad}
        End If
        Return New Double() {RaRad, DecRad}
    End Function

    Public Function AdaptForCelestialErrors( _
            ByRef fromPosition As Position, _
            ByRef toPosition As Position, _
            ByRef fromDate As Date, _
            ByRef toDate As Date, _
            ByVal includePrecession As Boolean, _
            ByVal includeNutationAnnualAberration As Boolean, _
            ByVal includeRefraction As Boolean, _
            ByVal uncorrectedToCorrected As Boolean, _
            ByVal LatitudeRad As Double) As Double()

        If UseCalculator Then
            pCelestialErrorsCalculator.CalculateErrors( _
                    fromPosition, _
                    toPosition, _
                    fromDate, _
                    toDate, _
                    includePrecession, _
                    includeNutationAnnualAberration, _
                    includeRefraction, _
                    uncorrectedToCorrected, _
                    LatitudeRad)

            Return New Double() {toPosition.RA.Rad, toPosition.Dec.Rad}
        End If

        Return New Double() {fromPosition.RA.Rad, fromPosition.Dec.Rad}
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
