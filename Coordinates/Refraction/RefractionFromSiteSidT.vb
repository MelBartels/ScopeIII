Public Class RefractionFromSiteSidT

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Refract As Refract
    Public RatesFacade As RatesFacade
    Public DeltaRa As Double
    Public DeltaDec As Double
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RefractionFromSiteSidT
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RefractionFromSiteSidT = New RefractionFromSiteSidT
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        Refract = Coordinates.Refract.GetInstance

        RatesFacade = Coordinates.RatesFacade.GetInstance
        RatesFacade.Build(CType(Rates.TrigRates, ISFT))
        RatesFacade.SetInit(CType(InitStateType.Altazimuth, ISFT))
        RatesFacade.Init()
    End Sub

    Public Shared Function GetInstance() As RefractionFromSiteSidT
        Return New RefractionFromSiteSidT
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' in summary, equat -> site altaz -> corrected equat
    ' in detail, equat -> site altaz -> refraction -> corrected site altaz -> corrected equat
    '
    ' both uncorrected to corrected and currected to uncorrected result in (northern hemisphere lat=30 facing south horizon alt=1): 
    ' Alt Refraction 26.25', RA Refraction 8.872676146911E-05', Dec Refraction -26.2499999999991'

    ' results in uncorrected to corrected alt refraction (pos value) that should be subtracted, ie, start alt=1 - refract=26.25' = end alt=.5625
    ' and corrected to uncorrected alt refraction (pos value) that should be added, ie, start alt=.5625 + refract=26.25' = end alt=1
    '
    ' and results in uncorrected to corrected equat values that should be added, ie, start dec=-59 + dec refract=-26.25' = end dec=-59.4375,
    ' and corrected to uncorrected equat values that should be subtracted, ie start dec=-59.4375 - dec refract=-26.25' = end dec=-59
    '
    Public Function Calc(ByVal uncorrectedToCorrected As Boolean, ByRef position As Position, ByVal latitudeRad As Double) As Double
        RatesFacade.Site.Latitude.Rad = latitudeRad
        RatesFacade.GetAltaz(position.RA.Rad, position.Dec.Rad, position.SidT.Rad)

        If uncorrectedToCorrected Then
            Refract.Calc(RatesFacade.Position.Alt.Rad)
            RatesFacade.Position.Alt.Rad -= Refract.Coordinate.Rad

            RatesFacade.GetEquat()

            DeltaRa = eMath.ValidRadHalfPi(RatesFacade.Position.RA.Rad - position.RA.Rad)
            DeltaDec = RatesFacade.Position.Dec.Rad - position.Dec.Rad
        Else
            Refract.CalcRefractionToBackOut(RatesFacade.Position.Alt.Rad)
            RatesFacade.Position.Alt.Rad += Refract.Coordinate.Rad

            RatesFacade.GetEquat()

            DeltaRa = eMath.ValidRadHalfPi(position.RA.Rad - RatesFacade.Position.RA.Rad)
            DeltaDec = position.Dec.Rad - RatesFacade.Position.Dec.Rad
        End If

        Return Refract.Coordinate.Rad
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class