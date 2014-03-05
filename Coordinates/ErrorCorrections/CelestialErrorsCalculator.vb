#Region "Imports"
#End Region

Public Class CelestialErrorsCalculator

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    ' inputs
    Public FromPosition As Position
    Public FromDate As Date
    Public ToDate As Date
    Public IncludePrecession As Boolean
    Public IncludeNutationAnnualAberration As Boolean
    Public IncludeRefraction As Boolean
    Public UncorrectedToCorrected As Boolean
    Public LatitudeRad As Double
    ' input/output
    Public ToPosition As Position
    ' outputs
    Public CoordErrorArray As CoordErrorArray = Coordinates.CoordErrorArray.GetInstance
    Public CelestialErrorsPosition As Position
    Public DeltaRa As Double
    Public DeltaDec As Double
#End Region

#Region "Private and Protected Members"
    Private pToJD As Double
    Private pDeltaYr As Double
    Private pLastFromYear As Double
    Private pStartPosition As Position
    Private pTime As Time
    Private pPrecession As Precession
    Private pNutationAnnualAberration As NutationAnnualAberration
    Private pRefractionFromSiteSidT As RefractionFromSiteSidT
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CelestialErrorsCalculator
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CelestialErrorsCalculator = New CelestialErrorsCalculator
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pTime = Coordinates.Time.GetInstance
        pPrecession = Coordinates.Precession.GetInstance
        pPrecession.UseRigorousCalc = True
        pNutationAnnualAberration = Coordinates.NutationAnnualAberration.GetInstance
        pRefractionFromSiteSidT = RefractionFromSiteSidT.GetInstance
        pStartPosition = PositionArray.GetInstance.GetPosition
        Me.FromPosition = PositionArray.GetInstance.GetPosition
        Me.ToPosition = PositionArray.GetInstance.GetPosition
        Me.CelestialErrorsPosition = PositionArray.GetInstance.GetPosition
    End Sub

    Public Shared Function GetInstance() As CelestialErrorsCalculator
        Return New CelestialErrorsCalculator
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub CalculateErrors( _
            ByRef fromPosition As Position, _
            ByRef toPosition As Position, _
            ByRef fromDate As Date, _
            ByRef toDate As Date, _
            ByVal includePrecession As Boolean, _
            ByVal includeNutationAnnualAberration As Boolean, _
            ByVal includeRefraction As Boolean, _
            ByVal uncorrectedToCorrected As Boolean, _
            ByVal LatitudeRad As Double)

        Me.FromPosition = fromPosition
        Me.FromDate = fromDate
        Me.ToDate = toDate
        Me.IncludePrecession = includePrecession
        Me.IncludeNutationAnnualAberration = includeNutationAnnualAberration
        Me.IncludeRefraction = includeRefraction
        Me.UncorrectedToCorrected = uncorrectedToCorrected
        Me.LatitudeRad = LatitudeRad
        Me.ToPosition = toPosition

        ' clear CoordErrors
        Me.CoordErrorArray.ErrorArray.Clear()
        ' preserve starting position
        pStartPosition.CopyFrom(Me.FromPosition)
        'Debug.WriteLine("starting " & Me.FromPosition.ShowCoordDeg)
        ' calc errors
        calculateErrorsSubr()
        ' reset position to starting values
        Me.FromPosition.CopyFrom(pStartPosition)
        ' set Me.ToPosition (contains updated values) 
        setToPosition()
        ' set CelestialErrorsPosition (original values + CoordErrorArray)
        CelestialErrorsPosition.CopyFrom(Me.FromPosition)
        CelestialErrorsPosition.CoordErrorArray.CopyFrom(Me.CoordErrorArray)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Sub calculateErrorsSubr()
        zeroDeltas()

        If Me.IncludePrecession OrElse Me.IncludeNutationAnnualAberration Then
            pToJD = pTime.CalcJD(Me.ToDate)
            pDeltaYr = (pToJD - pTime.CalcJD(Me.FromDate)) / Units.DayToYear
        End If

        ' each calculation takes the results of the previous calculation as its starting point
        If Me.UncorrectedToCorrected Then
            ' precess, then nutate and annual aberration, finally refraction
            If Me.IncludePrecession Then
                calcPrecession()
            End If
            If Me.IncludeNutationAnnualAberration Then
                calcNutationAnnualAberration()
            End If
            If Me.IncludeRefraction Then
                calcRefraction()
            End If
        Else
            ' back out corrections in reverse order
            If Me.IncludeRefraction Then
                calcRefraction()
            End If
            If Me.IncludeNutationAnnualAberration Then
                calcNutationAnnualAberration()
            End If
            If Me.IncludePrecession Then
                calcPrecession()
            End If
        End If

        sumErrors()
        addCoordErrors()
    End Sub

    Private Sub calcPrecession()
        Dim fromYear As Double = Me.FromDate.Year + Me.FromDate.DayOfYear * Units.YearToDay
        pPrecession.Calc(Me.FromPosition, pDeltaYr, fromYear)

        If Me.UncorrectedToCorrected Then
            Me.FromPosition.RA.Rad = eMath.ValidRad(Me.FromPosition.RA.Rad + pPrecession.DeltaRa)
            Me.FromPosition.Dec.Rad += pPrecession.DeltaDec
        Else
            Me.FromPosition.RA.Rad = eMath.ValidRad(Me.FromPosition.RA.Rad - pPrecession.DeltaRa)
            Me.FromPosition.Dec.Rad -= pPrecession.DeltaDec
        End If
        'Debug.WriteLine("after precession " & Me.FromPosition.ShowCoordDeg)
    End Sub

    Private Sub calcNutationAnnualAberration()
        pNutationAnnualAberration.Calc(Me.FromPosition, pToJD)

        If Me.UncorrectedToCorrected Then
            Me.FromPosition.RA.Rad = eMath.ValidRad(Me.FromPosition.RA.Rad + pNutationAnnualAberration.NutationDeltaRa + pNutationAnnualAberration.AnnualAberrationDeltaRa)
            Me.FromPosition.Dec.Rad += pNutationAnnualAberration.NutationDeltaDec + pNutationAnnualAberration.AnnualAberrationDeltaDec
        Else
            Me.FromPosition.RA.Rad = eMath.ValidRad(Me.FromPosition.RA.Rad - pNutationAnnualAberration.NutationDeltaRa - pNutationAnnualAberration.AnnualAberrationDeltaRa)
            Me.FromPosition.Dec.Rad -= pNutationAnnualAberration.NutationDeltaDec - pNutationAnnualAberration.AnnualAberrationDeltaDec
        End If
        'Debug.WriteLine("after nutationAnnualaberration " & Me.FromPosition.ShowCoordDeg)
    End Sub

    Private Sub calcRefraction()
        pRefractionFromSiteSidT.Calc(Me.UncorrectedToCorrected, Me.FromPosition, Me.LatitudeRad)

        If Me.UncorrectedToCorrected Then
            Me.FromPosition.RA.Rad = eMath.ValidRad(Me.FromPosition.RA.Rad + pRefractionFromSiteSidT.DeltaRa)
            Me.FromPosition.Dec.Rad += pRefractionFromSiteSidT.DeltaDec
        Else
            Me.FromPosition.RA.Rad = eMath.ValidRad(Me.FromPosition.RA.Rad - pRefractionFromSiteSidT.DeltaRa)
            Me.FromPosition.Dec.Rad -= pRefractionFromSiteSidT.DeltaDec
        End If
        'Debug.WriteLine("after refraction " & Me.FromPosition.ShowCoordDeg)
    End Sub

    Private Sub zeroDeltas()
        pPrecession.DeltaRa = 0
        pPrecession.DeltaDec = 0

        pNutationAnnualAberration.NutationDeltaRa = 0
        pNutationAnnualAberration.AnnualAberrationDeltaRa = 0
        pNutationAnnualAberration.NutationDeltaDec = 0
        pNutationAnnualAberration.AnnualAberrationDeltaDec = 0

        pRefractionFromSiteSidT.DeltaRa = 0
        pRefractionFromSiteSidT.DeltaDec = 0
    End Sub

    Private Sub sumErrors()
        Me.DeltaRa = 0
        Me.DeltaDec = 0

        Me.DeltaRa += pPrecession.DeltaRa
        Me.DeltaDec += pPrecession.DeltaDec

        Me.DeltaRa += pNutationAnnualAberration.NutationDeltaRa
        Me.DeltaDec += pNutationAnnualAberration.NutationDeltaDec
        Me.DeltaRa += pNutationAnnualAberration.AnnualAberrationDeltaRa
        Me.DeltaDec += pNutationAnnualAberration.AnnualAberrationDeltaDec

        Me.DeltaRa += pRefractionFromSiteSidT.DeltaRa
        Me.DeltaDec += pRefractionFromSiteSidT.DeltaDec

        'Debug.WriteLine("sum " & Me.DeltaRa * Units.RadToArcmin & "' " & Me.DeltaDec * Units.RadToArcmin & "'")
    End Sub

    Private Sub setToPosition()
        If Me.ToPosition IsNot Nothing Then
            If Me.UncorrectedToCorrected Then
                Me.ToPosition.RA.Rad = eMath.ValidRad(Me.FromPosition.RA.Rad + Me.DeltaRa)
                Me.ToPosition.Dec.Rad = eMath.ValidRadPi(Me.FromPosition.Dec.Rad + Me.DeltaDec)
            Else
                Me.ToPosition.RA.Rad = eMath.ValidRad(Me.FromPosition.RA.Rad - Me.DeltaRa)
                Me.ToPosition.Dec.Rad = eMath.ValidRadPi(Me.FromPosition.Dec.Rad - Me.DeltaDec)
            End If
            Me.ToPosition.SidT.Rad = Me.FromPosition.SidT.Rad
        End If
    End Sub

    Private Sub addCoordErrors()
        Me.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad = pPrecession.DeltaRa
        Me.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad = pPrecession.DeltaDec

        Me.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Nutation, ISFT)).Rad = pNutationAnnualAberration.NutationDeltaRa
        Me.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Nutation, ISFT)).Rad = pNutationAnnualAberration.NutationDeltaDec
        Me.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.AnnualAberration, ISFT)).Rad = pNutationAnnualAberration.AnnualAberrationDeltaRa
        Me.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.AnnualAberration, ISFT)).Rad = pNutationAnnualAberration.AnnualAberrationDeltaDec

        Me.CoordErrorArray.CoordError(CType(CoordName.Alt, ISFT), CType(CoordErrorType.Refraction, ISFT), pRefractionFromSiteSidT.Refract.Coordinate.Rad)
        Me.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Refraction, ISFT), pRefractionFromSiteSidT.DeltaRa)
        Me.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Refraction, ISFT), pRefractionFromSiteSidT.DeltaDec)
    End Sub
#End Region

End Class
