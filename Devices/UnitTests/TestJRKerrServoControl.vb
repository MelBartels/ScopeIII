Imports NUnit.Framework

<TestFixture()> Public Class TestJRKerrServoControl

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestClone()
        Dim JRKerrServoControl As JRKerrServoControl = JRKerrServoControl.GetInstance
        JRKerrServoControl.CurrentLimitCL = 1

        Dim clonedJRKerrGain As JRKerrServoControl = CType(JRKerrServoControl.Clone, Devices.JRKerrServoControl)
        Assert.AreEqual(1, clonedJRKerrGain.CurrentLimitCL)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

