Imports NUnit.Framework

<TestFixture()> Public Class AirMassTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestAirMass()
        Dim tolerance As Double = 0.01
        Dim airMassCalculator As AirMassCalculator = Coordinates.AirMassCalculator.GetInstance
        airMassCalculator.Calc(60 * Units.DegToRad)
        Assert.AreEqual(1.15, airMassCalculator.AirMass, tolerance)
        airMassCalculator.Calc(30 * Units.DegToRad)
        Assert.AreEqual(1.99, airMassCalculator.AirMass, tolerance)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
