Imports NUnit.Framework

<TestFixture()> Public Class ConvertMatrixPresetPositionTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestAltaz()
        Dim icmpp As IConvertMatrixPresetPosition = ConvertMatrixPresetPositionAltaz.GetInstance
        Dim one As Position = Position.GetInstance
        Dim two As Position = Position.GetInstance

        Dim latitudeRad As Double = 40 * Units.DegToRad
        icmpp.Preset(one, two, latitudeRad)
        Assert.AreEqual(90, one.Dec.Rad * Units.RadToDeg)
        Assert.AreEqual(90 - Math.Abs(latitudeRad * Units.RadToDeg), two.Alt.Rad * Units.RadToDeg)

        latitudeRad = -latitudeRad
        icmpp.Preset(one, two, latitudeRad)
        Assert.AreEqual(-90, one.Dec.Rad * Units.RadToDeg)
        Assert.AreEqual(90 - Math.Abs(latitudeRad * Units.RadToDeg), two.Alt.Rad * Units.RadToDeg)
    End Sub

    <Test()> Public Sub TestEquat()
        Dim icmpp As IConvertMatrixPresetPosition = ConvertMatrixPresetPositionEquat.GetInstance
        Dim one As Position = Position.GetInstance
        Dim two As Position = Position.GetInstance

        Dim latitudeRad As Double = 40 * Units.DegToRad
        icmpp.Preset(one, two, latitudeRad)
        Assert.AreEqual(90, one.Dec.Rad * Units.RadToDeg)
        Assert.AreEqual(0, two.Alt.Rad * Units.RadToDeg)

        latitudeRad = -latitudeRad
        icmpp.Preset(one, two, latitudeRad)
        Assert.AreEqual(-90, one.Dec.Rad * Units.RadToDeg)
        Assert.AreEqual(0, two.Alt.Rad * Units.RadToDeg)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
