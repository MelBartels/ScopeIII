Imports NUnit.Framework

<TestFixture()> Public Class FabErrorsTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub CopyFrom()
        Dim fabErrors As FabErrors = Coordinates.FabErrors.GetInstance
        fabErrors.SetFabErrorsDeg(1, 0, 0)

        Dim newFabErrors As FabErrors = Coordinates.FabErrors.GetInstance
        Assert.AreEqual(0, newFabErrors.Z1.Rad)
        newFabErrors.CopyFrom(fabErrors)
        Assert.AreEqual(Units.DegToRad, newFabErrors.Z1.Rad)
    End Sub

    <Test()> Public Sub Z12NonZero()
        Dim fabErrors As FabErrors = Coordinates.FabErrors.GetInstance

        fabErrors.SetFabErrorsDeg(0, 0, 0)
        Assert.IsFalse(fabErrors.Z12NonZero)

        fabErrors.SetFabErrorsDeg(1, 0, 0)
        Assert.IsTrue(fabErrors.Z12NonZero)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
