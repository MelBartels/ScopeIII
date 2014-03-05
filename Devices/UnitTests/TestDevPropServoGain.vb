Imports NUnit.Framework

<TestFixture()> Public Class TestDevPropServoGain

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestClone()
        Dim servoGain As ServoGain = servoGain.GetInstance
        servoGain.PositionGainKp = 1

        Dim clonedJRKerrGain As ServoGain = CType(servoGain.Clone, Devices.ServoGain)
        Assert.AreEqual(1, clonedJRKerrGain.PositionGainKp)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

