Imports NUnit.Framework

<TestFixture()> Public Class CelestialErrorComparerTests
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub Test()
        ' control
        Dim i1 As Int32 = 1
        Dim i2 As Int32 = 2
        Assert.AreEqual(-1, i1.CompareTo(i2))
        Assert.AreEqual(0, i1.CompareTo(i1))
        Assert.AreEqual(1, i2.CompareTo(i1))

        ' test the comparer
        Dim posErrorComparer As CelestialErrorComparer = Forms.CelestialErrorComparer.GetInstance

        Dim dp1 As DisplayPosition = DisplayPosition.GetInstance
        Dim dp2 As DisplayPosition = DisplayPosition.GetInstance

        dp1.Name = CoordErrorType.Precession.Name
        dp2.Name = CoordErrorType.Nutation.Name
        Assert.AreEqual(-1, posErrorComparer.Compare(dp1, dp2))

        dp1.Name = CoordErrorType.AnnualAberration.Name
        dp2.Name = CoordErrorType.AnnualAberration.Name
        Assert.AreEqual(0, posErrorComparer.Compare(dp1, dp2))

        dp1.Name = CoordErrorType.Refraction.Name
        dp2.Name = CoordErrorType.Nutation.Name
        Assert.AreEqual(1, posErrorComparer.Compare(dp1, dp2))
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
