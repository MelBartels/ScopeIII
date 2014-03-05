Imports NUnit.Framework

<TestFixture()> Public Class TestJRKerrServoStatus

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestStatusProperties()
        Dim m As JRKerrServoStatus = JRKerrServoStatus.GetInstance
        m.MoveDone = True
        m.PowerOn = True

        Assert.AreEqual(CByte(MOVE_DONE + POWER_ON), m.Status)

        Assert.IsTrue(m.MoveDone)
        Assert.IsTrue(m.PowerOn)
        Assert.IsFalse(m.ChecksumError)
        Assert.IsFalse(m.OverCurrent)
        Assert.IsFalse(m.PositionError)
        Assert.IsFalse(m.Limit1)
        Assert.IsFalse(m.Limit2)
        Assert.IsFalse(m.HomeInProgress)
    End Sub

    <Test()> Public Sub TestStatusItems()
        Dim m As JRKerrServoStatus = JRKerrServoStatus.GetInstance
        Assert.AreEqual(0, m.StatusItems)
        m.SendPosition = True
        m.SendVelocity = True
        Assert.AreEqual(SEND_POS + SEND_VEL, m.StatusItems)
        Assert.IsTrue(m.SendPosition)
        Assert.IsTrue(m.SendVelocity)
        Assert.IsFalse(m.SendAD)
        Assert.IsFalse(m.SendPositionError)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

