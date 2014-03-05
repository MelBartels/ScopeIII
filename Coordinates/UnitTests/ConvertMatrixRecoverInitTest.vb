Imports NUnit.Framework

<TestFixture()> Public Class ConvertMatrixRecoverInitTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestBadInitNumber()
        Assert.IsNull(ConvertMatrix.GetInstance.RecoverInit(0))
        Assert.IsNull(ConvertMatrix.GetInstance.RecoverInit(4))
    End Sub

    <Test()> Public Sub TestRecoverInitsEquatCoords()
        Dim cm As ConvertMatrix = ConvertMatrix.GetInstance
        Dim position As Position = position.GetInstance

        Dim init As Int32 = 1
        For SidTDeg As Double = 0 To 315 Step 45
            For raDeg As Double = 0 To 315 Step 45
                For DecDeg As Double = -90 To 90 Step 45
                    position.SetCoordDeg(raDeg, DecDeg, 0, 0, SidTDeg)
                    cm.Position.CopyFrom(position)
                    cm.InitMatrix(init)
                    VerifyPosition(position, cm.RecoverInit(init))
                Next
            Next
        Next

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestRecoverInitsAltazCoords()
        Dim cm As ConvertMatrix = ConvertMatrix.GetInstance
        Dim position As Position = position.GetInstance

        Dim init As Int32 = 1
        For azDeg As Double = 0 To 315 Step 45
            For altDeg As Double = -90 To 90 Step 45
                position.SetCoordDeg(0, 0, azDeg, altDeg, 0)
                cm.Position.CopyFrom(position)
                cm.InitMatrix(init)
                VerifyPosition(position, cm.RecoverInit(init))
            Next
        Next

        Assert.IsTrue(True)
    End Sub

    Public Sub VerifyPosition(ByRef position As Position, ByRef testPosition As Position)
        Dim variance As Double = Units.ArcsecToRad
        Assert.AreEqual(position.RA.Rad, testPosition.RA.Rad, variance)
        Assert.AreEqual(position.Dec.Rad, testPosition.Dec.Rad, variance)
        Assert.AreEqual(position.Az.Rad, testPosition.Az.Rad, variance)
        Assert.AreEqual(position.Alt.Rad, testPosition.Alt.Rad, variance)
        Assert.AreEqual(position.SidT.Rad, testPosition.SidT.Rad, variance)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
