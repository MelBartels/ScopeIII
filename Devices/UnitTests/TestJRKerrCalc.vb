Imports NUnit.Framework

<TestFixture()> Public Class TestJRKerrCalc

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Sub TestVelocity()
        Dim jrk As JRKerrCalc = JRKerrCalc.GetInstance
        Dim encoderCountsPerRev As Double = 2000
        Dim gearheadRatio As Double = 10
        Dim countsPerRad As Double = encoderCountsPerRev * gearheadRatio / Units.OneRev
        Dim countsToRad As Double = 1 / countsPerRad

        ' from observing SiTech controller
        ' a velocity of 700 000 ~ 1rps motor shaft (60rpm) using Pittman motor 10:1 gearing w/ 2000 quad decoded encoder
        Dim vel As int32 = 700000
        Dim velRadSec As Double = jrk.VelRadSecFromVelocity(vel, countsToRad)
        Assert.AreEqual(6.553453547245188, velRadSec)
        Dim velRevSec As Double = jrk.VelRevSecFromVelocity(vel, countsToRad)
        Assert.AreEqual(1.0430145263671875, velRevSec)
        Dim velDegSec As Double = jrk.VelDegSecFromVelocity(vel, countsToRad)
        Assert.AreEqual(375.4852294921875, velDegSec)

        Dim rpm As Double = velRevSec * 60
        Assert.AreEqual(62.58087158203125, rpm)

        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestAcceleration()
        Dim jrk As JRKerrCalc = JRKerrCalc.GetInstance
        Dim encoderCountsPerRev As Double = 2000
        Dim gearheadRatio As Double = 10
        Dim countsPerRad As Double = encoderCountsPerRev * gearheadRatio / Units.OneRev
        Dim countsToRad As Double = 1 / countsPerRad

        ' from SiTech ServoDoc documentation where default acceleration is 1000
        Dim accel As int32 = 1000
        Dim accelRadSecSec As Double = jrk.AccelRadSecSecFromAcceleration(accel, countsToRad)
        Assert.AreEqual(18.284135396814072, accelRadSecSec)
        Dim accelRevSecSec As Double = jrk.AccelRevSecSecFromAcceleration(accel, countsToRad)
        Assert.AreEqual(2.9100105285644529, accelRevSecSec)
        Dim accelDegSecSec As Double = jrk.AccelDegSecSecFromAcceleration(accel, countsToRad)
        Assert.AreEqual(1047.6037902832031, accelDegSecSec)

        ' 360 deg per rev
        Assert.AreEqual(accelRevSecSec, accelDegSecSec / 360, 0.000000000000001)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

