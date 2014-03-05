Imports NUnit.Framework

<TestFixture()> Public Class CoordErrorArrayTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestCoordErrorArray()
        Dim coordErrorArray As CoordErrorArray = Coordinates.CoordErrorArray.GetInstance

        coordErrorArray.CoordError(CType(CoordName.Alt, ISFT), CType(CoordErrorType.CoordXform, ISFT), 2)
        coordErrorArray.CoordError(CType(CoordName.Az, ISFT), CType(CoordErrorType.Precession, ISFT), 1)
        coordErrorArray.CoordError(CType(CoordName.Alt, ISFT), CType(CoordErrorType.Nutation, ISFT), 3)
        Assert.AreEqual(3, coordErrorArray.ErrorArray.Count)
        Assert.AreEqual(2, coordErrorArray.CoordError(CType(CoordName.Alt, ISFT), CType(CoordErrorType.CoordXform, ISFT)).Rad)

        coordErrorArray.CoordError(CType(CoordName.Alt, ISFT), CType(CoordErrorType.CoordXform, ISFT), 1.5)
        Assert.AreEqual(1.5, coordErrorArray.CoordError(CType(CoordName.Alt, ISFT), CType(CoordErrorType.CoordXform, ISFT)).Rad)

        Assert.AreEqual(3, coordErrorArray.ErrorArray.Count)
    End Sub

    ' demostrate that precession errors are saved for both RA and Dec
    <Test()> Public Sub TestPrecessionError()
        Dim coordErrorArray As CoordErrorArray = Coordinates.CoordErrorArray.GetInstance

        coordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Precession, ISFT), 1)
        coordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Precession, ISFT), 2)
        Assert.AreEqual(2, coordErrorArray.ErrorArray.Count)
        Assert.AreEqual(1, coordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad)
        Assert.AreEqual(2, coordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
