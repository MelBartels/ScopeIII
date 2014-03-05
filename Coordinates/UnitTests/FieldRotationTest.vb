Imports NUnit.Framework

<TestFixture()> Public Class FieldRotationTest

    Private Const AllowedErrorRad As Double = Units.ArcsecToRad / 1000

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFieldRotation()
        Dim convert As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Altazimuth, ISFT))
        convert.ICoordXform.Site.Latitude.Rad = 45 * Units.DegToRad
        convert.IInit.Init()
        convert.ICoordXform.Position.Az.Rad = 160 * Units.DegToRad
        convert.ICoordXform.Position.Alt.Rad = 45 * Units.DegToRad
        convert.ICoordXform.GetEquat()
        Dim p1 As Position = Position.GetInstance
        p1.CopyFrom(convert.ICoordXform.Position)

        Dim FR As FieldRotation = FieldRotation.GetInstance
        Dim varianceArcsec As Double = 0.001
        Dim testFRAngle As Double = 345.99805783448306
        ' from formula
        Dim testRateSidTrackArcsecSec As Double = 14.095389311788617

        Assert.AreEqual(testFRAngle, FR.CalcAngleViaTrig(convert.ICoordXform.Position, 0, convert.ICoordXform.Site.Latitude) * Units.RadToDeg)
        Assert.AreEqual(testRateSidTrackArcsecSec, FR.CalcRateSidTrackViaFormula(convert.ICoordXform.Position, 0, convert.ICoordXform.Site.Latitude) * Units.RadToArcsec, varianceArcsec)
        Assert.AreEqual(testRateSidTrackArcsecSec, FR.CalcRateSidTrackViaDeltaFR(convert.ICoordXform.Position, 0, convert.ICoordXform.Site.Latitude) * Units.RadToArcsec, varianceArcsec)

        p1.SidT.Rad -= 0.5 * Units.SecToRad
        Dim p2 As Position = Position.GetInstance
        p2.CopyFrom(p1)
        p2.SidT.Rad += Units.SecToRad
        Dim p3 As Position = Position.GetInstance
        p3.CopyFrom(convert.ICoordXform.Position)
        Dim p4 As Position = Position.GetInstance
        p4.CopyFrom(p2)
        Assert.AreEqual(testRateSidTrackArcsecSec, FR.CalcRateSidTrackViaDeltaFR(p1, p2, 0, convert.ICoordXform.Site.Latitude) * Units.RadToArcsec, varianceArcsec)
        Assert.AreEqual(testRateSidTrackArcsecSec, FR.CalcRateSidTrackViaFormula(p3, p4, 0, convert.ICoordXform.Site.Latitude) * Units.RadToArcsec, varianceArcsec)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
