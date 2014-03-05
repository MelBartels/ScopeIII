Imports NUnit.Framework

<TestFixture()> Public Class CelestialErrorsCalculatorTest

    Dim pVariance As Double = Units.ArcsecToRad / 20

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub ErrorsCalculator()
        Dim position As Position = Coordinates.Position.GetInstance
        ' az=180, alt=0
        position.SetCoordDeg(180, -60, 180, 0, 180)
        Dim latitudeRad As Double = 30 * Units.DegToRad
        Dim fromDate As New DateTime(2005, 1, 1)
        Dim toDate As New DateTime(2006, 6, 6)

        Dim cec As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim cecPosition As Position = Coordinates.Position.GetInstance
        cecPosition.CopyFrom(position)
        cec.CalculateErrors(cecPosition, Nothing, fromDate, toDate, True, True, True, True, latitudeRad)
        ' verify that total errors = sum of coord errors from CoordErrorArray
        Dim totalRaRadCoordError As Double = cec.CoordErrorArray.SumRad(CType(CoordName.RA, ISFT))
        Dim totalDecRadCoordError As Double = cec.CoordErrorArray.SumRad(CType(CoordName.Dec, ISFT))
        Assert.AreEqual(cec.DeltaRa, totalRaRadCoordError, pVariance)
        Assert.AreEqual(cec.DeltaDec, totalDecRadCoordError, pVariance)
        Dim totalCecRaRad As Double = cecPosition.RA.Rad + cec.DeltaRa
        Dim totalCecDecRad As Double = cecPosition.Dec.Rad + cec.DeltaDec

        ' create comparison test values
        Dim precession As Precession = Coordinates.Precession.GetInstance
        Dim precessionPosition As Position = Coordinates.Position.GetInstance
        precessionPosition.CopyFrom(position)
        Dim deltaYr As Double = toDate.Subtract(fromDate).Days / Units.DayToYear
        precession.Calc(precessionPosition, deltaYr)

        Dim nutationAnnualAberration As NutationAnnualAberration = Coordinates.NutationAnnualAberration.GetInstance
        Dim nutationAnnualAberrationPosition As Position = Coordinates.Position.GetInstance
        nutationAnnualAberrationPosition.CopyFrom(precessionPosition)
        nutationAnnualAberrationPosition.RA.Rad += precession.DeltaRa
        nutationAnnualAberrationPosition.Dec.Rad += precession.DeltaDec
        nutationAnnualAberration.Calc(nutationAnnualAberrationPosition, Time.GetInstance.CalcJD(toDate))

        Dim refractionFromSiteSidT As RefractionFromSiteSidT = Coordinates.RefractionFromSiteSidT.GetInstance
        Dim refractionFromSiteSidTPosition As Position = Coordinates.Position.GetInstance
        refractionFromSiteSidTPosition.CopyFrom(nutationAnnualAberrationPosition)
        refractionFromSiteSidTPosition.RA.Rad += nutationAnnualAberration.NutationDeltaRa + nutationAnnualAberration.AnnualAberrationDeltaRa
        refractionFromSiteSidTPosition.Dec.Rad += nutationAnnualAberration.NutationDeltaDec + nutationAnnualAberration.AnnualAberrationDeltaDec
        refractionFromSiteSidT.Calc(True, refractionFromSiteSidTPosition, latitudeRad)

        Dim individualPosition As Position = Coordinates.Position.GetInstance
        individualPosition.CopyFrom(refractionFromSiteSidTPosition)
        individualPosition.RA.Rad += refractionFromSiteSidT.DeltaRa
        individualPosition.Dec.Rad += refractionFromSiteSidT.DeltaDec

        ' compare calculator to individual calculations
        Assert.AreEqual(9, cec.CoordErrorArray.ErrorArray.Count)
        Assert.AreEqual(precession.DeltaRa, cec.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad, pVariance)
        Assert.AreEqual(precession.DeltaDec, cec.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad, pVariance)
        Assert.AreEqual(nutationAnnualAberration.NutationDeltaRa, cec.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Nutation, ISFT)).Rad, pVariance)
        Assert.AreEqual(nutationAnnualAberration.NutationDeltaDec, cec.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Nutation, ISFT)).Rad, pVariance)
        Assert.AreEqual(nutationAnnualAberration.AnnualAberrationDeltaRa, cec.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.AnnualAberration, ISFT)).Rad, pVariance)
        Assert.AreEqual(nutationAnnualAberration.AnnualAberrationDeltaDec, cec.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.AnnualAberration, ISFT)).Rad, pVariance)
        Assert.AreEqual(refractionFromSiteSidT.DeltaRa, cec.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Refraction, ISFT)).Rad, pVariance)
        Assert.AreEqual(refractionFromSiteSidT.DeltaDec, cec.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Refraction, ISFT)).Rad, pVariance)
        Assert.AreEqual(refractionFromSiteSidT.Refract.Coordinate.Rad, cec.CoordErrorArray.CoordError(CType(CoordName.Alt, ISFT), CType(CoordErrorType.Refraction, ISFT)).Rad, pVariance)

        Dim totalIndividualDeltaRa As Double = precession.DeltaRa + nutationAnnualAberration.NutationDeltaRa + nutationAnnualAberration.AnnualAberrationDeltaRa + refractionFromSiteSidT.DeltaRa
        Dim totalIndividualDeltaDec As Double = precession.DeltaDec + nutationAnnualAberration.NutationDeltaDec + nutationAnnualAberration.AnnualAberrationDeltaDec + refractionFromSiteSidT.DeltaDec

        Assert.AreEqual(totalIndividualDeltaRa, cec.DeltaRa, pVariance)
        Assert.AreEqual(totalIndividualDeltaDec, cec.DeltaDec, pVariance)

        Assert.AreEqual(individualPosition.RA.Rad, totalCecRaRad, pVariance)
        Assert.AreEqual(individualPosition.Dec.Rad, totalCecDecRad, pVariance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub UncorrectedToCorrectedToUncorrected()
        Dim position As Position = Coordinates.Position.GetInstance
        ' az=180, alt=1 (if alt=0, then precession/nutationAnnualAberration changes may cause alt to drop below zero,
        ' affecting the ability of the refraction routine to return the same value to remove as was originally added)
        position.SetCoordDeg(180, -59, 180, 0, 180)
        Dim latitudeRad As Double = 30 * Units.DegToRad
        Dim fromDate As New DateTime(2005, 1, 1)
        Dim toDate As New DateTime(2006, 6, 6)
        ' precession to and from ~ 1/50"
        Dim includePrecession As Boolean = True
        Dim includeNutationAnnualAberration As Boolean = True
        ' keep input position (after precession/nutationAnnualAberration) >=0, otherwise to and from difference will be
        ' in the arcseconds
        Dim includeRefraction As Boolean = True

        Dim cec As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim cecPosition As Position = Coordinates.Position.GetInstance
        cecPosition.CopyFrom(position)
        cec.CalculateErrors(cecPosition, Nothing, fromDate, toDate, includePrecession, includeNutationAnnualAberration, includeRefraction, True, latitudeRad)
        Dim totalRaRadUncorrectedToCorrected As Double = cec.DeltaRa
        Dim totalDecRadUncorrectedToCorrected As Double = cec.DeltaDec
        Debug.WriteLine(cec.CoordErrorArray.ToString)

        ' add corrections to equat coordinates and calculate from these corrected coordinates, setting
        ' uncorrectedToCorrected to false: this gives the errors to remove from the corrected coordinates
        ' in order to return to the original uncorrected equat coordinates
        cecPosition.RA.Rad += totalRaRadUncorrectedToCorrected
        cecPosition.Dec.Rad += totalDecRadUncorrectedToCorrected
        Debug.WriteLine(cecPosition.ShowCoordDeg)
        cec.CalculateErrors(cecPosition, Nothing, fromDate, toDate, includePrecession, includeNutationAnnualAberration, includeRefraction, False, latitudeRad)
        Debug.WriteLine(cec.CoordErrorArray.ToString)

        ' compare starting values to the values obtained from the uncorrected->corrected then corrected->uncorrected 
        ' transformations
        cecPosition.RA.Rad -= cec.DeltaRa
        cecPosition.Dec.Rad -= cec.DeltaDec
        Debug.WriteLine("position " & position.ShowCoordDeg)
        Debug.WriteLine("cec      " & cecPosition.ShowCoordDeg)
        Debug.WriteLine((position.RA.Rad - cecPosition.RA.Rad) * Units.RadToArcsec & BartelsLibrary.Constants.Quote)
        Debug.WriteLine((position.Dec.Rad - cecPosition.Dec.Rad) * Units.RadToArcsec & BartelsLibrary.Constants.Quote)

        Assert.AreEqual(position.RA.Rad, cecPosition.RA.Rad, pVariance)
        Assert.AreEqual(position.Dec.Rad, cecPosition.Dec.Rad, pVariance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub CelestialErrorsToPosition()
        Dim position As Position = Coordinates.Position.GetInstance
        ' az=180, alt=1
        position.SetCoordDeg(180, -59, 180, 0, 180)
        Dim latitudeRad As Double = 30 * Units.DegToRad
        Dim fromDate As New DateTime(2005, 1, 1)
        Dim toDate As New DateTime(2006, 6, 6)

        Dim correctedPosition As Position = Coordinates.Position.GetInstance

        Dim cec As CelestialErrorsCalculator = Coordinates.CelestialErrorsCalculator.GetInstance
        Dim uncorrectedToCorrected As Boolean = True
        cec.CalculateErrors(position, correctedPosition, fromDate, toDate, True, True, True, uncorrectedToCorrected, latitudeRad)

        ' verify that error corrections occurred
        Assert.AreNotEqual(position.RA.Rad, correctedPosition.RA.Rad)
        Assert.AreNotEqual(position.Dec.Rad, correctedPosition.Dec.Rad)

        Dim uncorrectedPosition As Position = Coordinates.Position.GetInstance
        uncorrectedToCorrected = False
        cec.CalculateErrors(correctedPosition, uncorrectedPosition, fromDate, toDate, True, True, True, uncorrectedToCorrected, latitudeRad)

        ' retrieved the original equat coordinates?
        Assert.AreEqual(position.RA.Rad, uncorrectedPosition.RA.Rad, pVariance)
        Assert.AreEqual(position.Dec.Rad, uncorrectedPosition.Dec.Rad, pVariance)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
