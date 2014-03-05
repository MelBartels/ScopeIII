#Region "Imports"
#End Region

Public Class RatesFacade

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pRates As IRates
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RatesFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RatesFacade = New RatesFacade
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As RatesFacade
        Return New RatesFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Rates() As IRates
        Get
            Return pRates
        End Get
        Set(ByVal Value As IRates)
            pRates = Value
        End Set
    End Property

    Public Property Site() As Site
        Get
            Return Rates.InitStateTemplate.ICoordXform.Site
        End Get
        Set(ByVal Value As Site)
            Rates.InitStateTemplate.ICoordXform.Site = Value
        End Set
    End Property

    Public Property ICoordXForm() As ICoordXform
        Get
            Return Rates.InitStateTemplate.ICoordXform
        End Get
        Set(ByVal value As ICoordXform)
            Rates.InitStateTemplate.ICoordXform = value
        End Set
    End Property

    Public Property FabErrors() As FabErrors
        Get
            Dim cm As ConvertMatrix = ConvertMatrix()
            If cm Is Nothing Then
                Return Nothing
            Else
                Return cm.FabErrors
            End If
        End Get
        Set(ByVal Value As FabErrors)
            Dim cm As ConvertMatrix = ConvertMatrix()
            If cm IsNot Nothing Then
                cm.FabErrors = Value
            End If
        End Set
    End Property

    ' RatesFactory creates desired IRate, whose constructor creates InitStateTemplate, building ICoordXform
    Public Function Build(ByRef rates As ISFT) As IRates
        Me.Rates = RatesFactory.GetInstance.Build(rates)
        Return Me.Rates
    End Function

    Public Sub SetInit(ByRef InitStateType As ISFT)
        InitStateFactory.GetInstance.SetInit(Rates.InitStateTemplate, InitStateType)
    End Sub

    Public Sub Init()
        Rates.Init()
    End Sub

    Public Sub CalcRates()
        Rates.CalcRates()
    End Sub

    Public Function CalcCorrectedRates( _
            ByRef celestialErrorsCalculator As CelestialErrorsCalculator, _
            ByRef startingCorrectedEquatPosition As Position, _
            ByRef toPosition As Position, _
            ByRef fromEpoch As Date, _
            ByRef toEpoch As Date, _
            ByVal includePrecession As Boolean, _
            ByVal includeNutationAnnualAberration As Boolean, _
            ByVal includeRefraction As Boolean, _
            ByVal latitudeRad As Double) As Boolean

        Rates.CalcCorrectedRates( _
                celestialErrorsCalculator, _
                startingCorrectedEquatPosition, _
                toPosition, _
                fromEpoch, _
                toEpoch, _
                includePrecession, _
                includeNutationAnnualAberration, _
                includeRefraction, _
                latitudeRad)
    End Function

    Public Function PriAxisTrackRate() As TrackRatesDataModel.TrackRate
        Return Rates.PriAxisTrackRate
    End Function

    Public Function SecAxisTrackRate() As TrackRatesDataModel.TrackRate
        Return Rates.SecAxisTrackRate
    End Function

    Public Function TierAxisTrackRate() As TrackRatesDataModel.TrackRate
        Return Rates.TierAxisTrackRate
    End Function

    Public Sub GetEquat()
        Rates.InitStateTemplate.ICoordXform.GetEquat()
    End Sub

    Public Sub GetEquat(ByVal azRad As Double, ByVal altRad As Double, ByVal sidTRad As Double)
        Rates.InitStateTemplate.ICoordXform.Position.Az.Rad = azRad
        Rates.InitStateTemplate.ICoordXform.Position.Alt.Rad = altRad
        Rates.InitStateTemplate.ICoordXform.Position.SidT.Rad = sidTRad
        GetEquat()
    End Sub

    Public Sub GetAltaz()
        Rates.InitStateTemplate.ICoordXform.GetAltaz()
    End Sub

    Public Sub GetAltaz(ByVal RaRad As Double, ByVal DecRad As Double, ByVal sidTRad As Double)
        Rates.InitStateTemplate.ICoordXform.Position.RA.Rad = RARad
        Rates.InitStateTemplate.ICoordXform.Position.Dec.Rad = DecRad
        Rates.InitStateTemplate.ICoordXform.Position.SidT.Rad = sidTRad
        GetAltaz()
    End Sub

    Public Function Position() As Position
        Return Rates.InitStateTemplate.ICoordXform.Position
    End Function

    Public Sub CopyFromOneTwoThreeFabErrors(ByRef one As Position, ByRef two As Position, ByRef three As Position, ByRef fabErrors As FabErrors)
        Dim cm As ConvertMatrix = ConvertMatrix()
        If cm IsNot Nothing Then
            If one IsNot Nothing Then
                cm.One.CopyFrom(one)
            End If
            If two IsNot Nothing Then
                cm.Two.CopyFrom(two)
            End If
            If three IsNot Nothing Then
                cm.Three.CopyFrom(three)
            End If
            If fabErrors IsNot Nothing Then
                cm.FabErrors.CopyFrom(fabErrors)
            Else
                cm.FabErrors.SetFabErrorsDeg(0, 0, 0)
            End If
        End If
    End Sub

    Public Sub CopyToOneTwoThreeFabErrors(ByRef one As Position, ByRef two As Position, ByRef three As Position, ByRef fabErrors As FabErrors)
        Dim cm As ConvertMatrix = ConvertMatrix()
        If cm IsNot Nothing Then
            If one IsNot Nothing Then
                one.CopyFrom(cm.One)
            End If
            If two IsNot Nothing Then
                two.CopyFrom(cm.Two)
            End If
            If three IsNot Nothing Then
                three.CopyFrom(cm.Three)
            End If
            If fabErrors IsNot Nothing AndAlso cm.FabErrors IsNot Nothing Then
                fabErrors.CopyFrom(cm.FabErrors)
            Else
                fabErrors.SetFabErrorsDeg(0, 0, 0)
            End If
        End If
    End Sub

    Public Function ConvertMatrix() As ConvertMatrix
        If CObj(Rates.InitStateTemplate.ICoordXform).GetType Is GetType(ConvertMatrix) Then
            Return CType(Rates.InitStateTemplate.ICoordXform, ConvertMatrix)
        End If
        Return Nothing
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
