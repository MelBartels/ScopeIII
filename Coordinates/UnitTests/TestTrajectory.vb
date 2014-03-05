Imports NUnit.Framework

<TestFixture()> Public Class TestTrajectory
    Dim pTrajectory As Trajectory
    Dim pTd As MotionVector

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        pTrajectory = Trajectory.GetInstance
        pTd = MotionVector.GetInstance
    End Sub

    <Test()> Public Sub TestTimeDistanceFromAccelBegVelEndVel()
        pTrajectory.TimeDistanceFromAccelBegVelEndVel(pTd, 2, 3, 6)
        Assert.AreEqual(1.5, pTd.Time)
        Assert.AreEqual(6.75, pTd.Distance)

        pTrajectory.TimeDistanceFromAccelBegVelEndVel(pTd, 2, 3, -6)
        Assert.AreEqual(4.5, pTd.Time)
        Assert.AreEqual(-6.75, pTd.Distance)

        pTrajectory.TimeDistanceFromAccelBegVelEndVel(pTd, 2, -3, -6)
        Assert.AreEqual(1.5, pTd.Time)
        Assert.AreEqual(-6.75, pTd.Distance)

        pTrajectory.TimeDistanceFromAccelBegVelEndVel(pTd, 2, -3, 6)
        Assert.AreEqual(4.5, pTd.Time)
        Assert.AreEqual(6.75, pTd.Distance)
    End Sub

    <Test()> Public Sub TestTimeDistanceFromTimeAccelBegVelEndVel()
        pTrajectory.TimeDistanceFromTimeAccelBegVelEndVel(pTd, 1.5, 2, 3, 6)
        Assert.AreEqual(6.75, pTd.Distance)

        pTrajectory.TimeDistanceFromTimeAccelBegVelEndVel(pTd, 2.5, 2, 3, 6)
        Assert.AreEqual(6.75, pTd.Distance)

        pTrajectory.TimeDistanceFromTimeAccelBegVelEndVel(pTd, 0.5, 2, 3, 6)
        Assert.AreEqual(1.75, pTd.Distance)
    End Sub

    <Test()> Public Sub TestFinalVelFromAccelDistanceTimeDiff()
        ' avgvel = 50/10=5 5sec of accel increases vel by 10, so finalVel=5+10=15
        Assert.AreEqual(15, pTrajectory.FinalVelFromAccelDistanceTimeDiff(2, 50, 10))

        ' avgvel = -50/10=-5 5sec of accel decreases vel by 10, so finalVel=-5-10=-15
        Assert.AreEqual(-15, pTrajectory.FinalVelFromAccelDistanceTimeDiff(-2, -50, 10))
    End Sub

    <Test()> Public Sub TestTriggerTimeDistanceFromAccelBegVelEndVel()
        ' final position has head start of 2.25 and moves 4.5 while initial position moves 6.75
        pTrajectory.TriggerTimeDistanceFromAccelBegVelEndVel(pTd, 2, 6, 3)
        Assert.AreEqual(1.5, pTd.Time)
        Assert.AreEqual(2.25, pTd.Distance)

        ' final position starts at 20.25 and moves -27 while initial position starts at 0 and moves -6.75"
        ' initial position moves forward 2.25 before stopping to reverse direction
        pTrajectory.TriggerTimeDistanceFromAccelBegVelEndVel(pTd, 2, 3, -6)
        Assert.AreEqual(4.5, pTd.Time)
        Assert.AreEqual(20.25, pTd.Distance)
        ' at this point: pTrajectory.TriggerTimeDistanceFromAccelBegVelEndVel(pTd, 2, 0, -6)
        ' separation has closed to 9
        pTrajectory.TriggerTimeDistanceFromAccelBegVelEndVel(pTd, 2, 0, -6)
        Assert.AreEqual(9, pTd.Distance)

        pTrajectory.TriggerTimeDistanceFromAccelBegVelEndVel(pTd, 2, -6, -3)
        Assert.AreEqual(1.5, pTd.Time)
        Assert.AreEqual(-2.25, pTd.Distance)

        pTrajectory.TriggerTimeDistanceFromAccelBegVelEndVel(pTd, 2, -3, 6)
        Assert.AreEqual(4.5, pTd.Time)
        Assert.AreEqual(-20.25, pTd.Distance)
    End Sub

    <Test()> Public Sub TestMaxVelFromAccelpUnCompDistance_BegVelEndVel()
        ' first leg time=(5-1)/2=2, distance=2*(5+1)/2=6
        ' last leg time=(5--1)/2=3, distance=3*(5+-1)/2=6
        ' total distance=12
        Assert.AreEqual(5, pTrajectory.MaxVelFromAccelpUnCompDistance_BegVelEndVel(2, 12, 1, -1))

        Assert.AreEqual(-5, pTrajectory.MaxVelFromAccelpUnCompDistance_BegVelEndVel(2, -12, 1, -1))
    End Sub

    <Test()> Public Sub TestMaxVelFromAccelDistanceBegVelEndVel()
        ' distance is separation of Positions at start of move
        ' NOT distance moved by first position if finalVel != 0

        ' first leg time=(5-1)/2=2, distance=2*(5+1)/2=6
        ' last leg time=(5--1)/2=3, distance=3*(5+-1)/2=6 
        ' change in position over total time of 5 = -5 
        ' total distance=6(first leg)+6(second leg)--5(distance target moved during time)=17 
        Assert.AreEqual(5, pTrajectory.MaxVelFromAccelDistanceBegVelEndVel(2, 17, 1, -1))

        ' first leg time=(5-1)/2=2, distance=2*(5+1)/2=6
        ' last leg time=(5-1)/2=2, distance=2*(5+1)/2=6
        ' change in position over total time of 4 = 4
        ' total distance=6(first leg)+6(second leg)-4(distance target moved during time)=8
        Assert.AreEqual(5, pTrajectory.MaxVelFromAccelDistanceBegVelEndVel(2, 8, 1, 1))

        Assert.AreEqual(-5, pTrajectory.MaxVelFromAccelDistanceBegVelEndVel(2, -17, -1, 1))

        Assert.AreEqual(-5, pTrajectory.MaxVelFromAccelDistanceBegVelEndVel(2, -8, -1, -1))
    End Sub

    <Test()> Public Sub TestMaxVelFromAccelDistanceBegVelEndVelFirstTrajZeroEndVel()
        ' distance is separation of Positions at start of move
        ' NOT distance moved by first position if finalVel != 0
        ' 1st trajectory ends with zero velocity matching 2nd trajectory that continues with finalVel

        ' first leg time=(5-1)/2=2, distance=2*(5+1)/2=6
        ' last leg time=(5-0)/2=2.5, distance=2.5*(5+0)/2=6.26
        ' change in position over total time of 4.5 = -4.5
        ' total distance=6(first leg)+6.25(second leg)--4.5(distance target moved during time)=16.75
        Assert.AreEqual(5, pTrajectory.MaxVelFromAccelDistanceBegVelEndVelFirstTrajZeroEndVel(2, 16.75, 1, -1))

        Assert.AreEqual(-5, pTrajectory.MaxVelFromAccelDistanceBegVelEndVelFirstTrajZeroEndVel(2, -16.75, -1, 1))
    End Sub

    <Test()> Public Sub TestMaxVelFromTimepUnCompDistance_BegVelEndVel()
        ' first leg time=(5-1)/2=2, distance=2*(5+1)/2=6
        ' last leg time=(5--1)/2=3, distance=3*(5+-1)/2=6
        Assert.AreEqual(5, pTrajectory.MaxVelFromTimepUnCompDistance_BegVelEndVel(5, 12, 1, -1))

        Assert.AreEqual(-5, pTrajectory.MaxVelFromTimepUnCompDistance_BegVelEndVel(5, -12, -1, 1))
    End Sub

    <Test()> Public Sub TestMaxVelFromTimeDistanceBegVelEndVel()
        ' distance is separation of Positions at start of move
        ' NOT distance moved by first position if finalVel != 0

        ' first leg time=(5-1)/2=2, distance=2*(5+1)/2=6
        ' last leg time=(5--1)/2=3, distance=3*(5+-1)/2=6
        ' change in position over total time of 5 = -5
        ' total distance=6(first leg)+6(second leg)--5(distance target moved during time)=17
        Assert.AreEqual(5, pTrajectory.MaxVelFromTimeDistanceBegVelEndVel(5, 17, 1, -1))

        Assert.AreEqual(-5, pTrajectory.MaxVelFromTimeDistanceBegVelEndVel(5, -17, -1, 1))
    End Sub

    <Test()> Public Sub TestTrajFromTimeDistanceBegVelEndVel()
        ' distance is separation of Positions at start of move
        ' NOT distance moved by first position if finalVel != 0

        ' void trajFromTimeDistanceBegVelEndVel(pTrajectory, 5, 17, 1, -1)
        pTrajectory.TrajFromTimeDistanceBegVelEndVel(pTrajectory, 5, 17, 1, -1)
        Assert.AreEqual(5, pTrajectory.MaxVel.BegVel)
        Assert.AreEqual(2, pTrajectory.RampUp.Accel)
    End Sub

    <Test()> Public Sub TestTrajFromAccelDistanceMaxVel()
        pTrajectory.TrajFromAccelDistanceMaxVel(pTrajectory, 2, 24, 4)
        Assert.AreEqual(24, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVel(pTrajectory, 2, 8, 8)
        Assert.AreEqual(8, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVel(pTrajectory, 2, -8, 8)
        Assert.AreEqual(-8, pTrajectory.TotalDistance)
    End Sub

    <Test()> Public Sub TestDistanceFromTimeAccelDistanceMaxVel()
        Assert.AreEqual(0, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 0, 2, 24, 4))
        Assert.AreEqual(1, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 1, 2, 24, 4))
        Assert.AreEqual(4, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 2, 2, 24, 4))
        Assert.AreEqual(8, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 3, 2, 24, 4))
        Assert.AreEqual(12, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 4, 2, 24, 4))
        Assert.AreEqual(16, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 5, 2, 24, 4))
        Assert.AreEqual(20, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 6, 2, 24, 4))
        Assert.AreEqual(23, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 7, 2, 24, 4))
        Assert.AreEqual(24, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 8, 2, 24, 4))
        Assert.AreEqual(24, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 9, 2, 24, 4))

        Assert.AreEqual(0, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 0, 2, -24, 4))
        Assert.AreEqual(1, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 1, 2, -24, 4))
        Assert.AreEqual(-4, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 2, 2, -24, 4))
        Assert.AreEqual(-8, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 3, 2, -24, 4))
        Assert.AreEqual(-12, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 4, 2, -24, 4))
        Assert.AreEqual(-16, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 5, 2, -24, 4))
        Assert.AreEqual(-20, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 6, 2, -24, 4))
        Assert.AreEqual(-23, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 7, 2, -24, 4))
        Assert.AreEqual(-24, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 8, 2, -24, 4))
        Assert.AreEqual(-24, pTrajectory.DistanceFromTimeAccelDistanceMaxVel(pTrajectory, 9, 2, -24, 4))
    End Sub

    <Test()> Public Sub TestVelFromTimeAccelDistanceMaxVel()
        Assert.AreEqual(0, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 0, 2, 24, 4))
        Assert.AreEqual(1, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 0.5, 2, 24, 4))
        Assert.AreEqual(2, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 1, 2, 24, 4))
        Assert.AreEqual(3, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 1.5, 2, 24, 4))
        Assert.AreEqual(4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 2, 2, 24, 4))
        Assert.AreEqual(4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 3, 2, 24, 4))
        Assert.AreEqual(4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 4, 2, 24, 4))
        Assert.AreEqual(4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 5, 2, 24, 4))
        Assert.AreEqual(4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 6, 2, 24, 4))
        Assert.AreEqual(3, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 6.5, 2, 24, 4))
        Assert.AreEqual(2, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 7, 2, 24, 4))
        Assert.AreEqual(1, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 7.5, 2, 24, 4))
        Assert.AreEqual(0, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 8, 2, 24, 4))
        Assert.AreEqual(0, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 9, 2, 24, 4))

        Assert.AreEqual(0, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 0, 2, -24, 4))
        Assert.AreEqual(-1, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 0.5, 2, -24, 4))
        Assert.AreEqual(-2, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 1, 2, -24, 4))
        Assert.AreEqual(-3, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 1.5, 2, -24, 4))
        Assert.AreEqual(-4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 2, 2, -24, 4))
        Assert.AreEqual(-4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 3, 2, -24, 4))
        Assert.AreEqual(-4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 4, 2, -24, 4))
        Assert.AreEqual(-4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 5, 2, -24, 4))
        Assert.AreEqual(-4, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 6, 2, -24, 4))
        Assert.AreEqual(-3, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 6.5, 2, -24, 4))
        Assert.AreEqual(-2, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 7, 2, -24, 4))
        Assert.AreEqual(-1, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 7.5, 2, -24, 4))
        Assert.AreEqual(0, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 8, 2, -24, 4))
        Assert.AreEqual(0, pTrajectory.VelFromTimeAccelDistanceMaxVel(pTrajectory, 9, 2, -24, 4))
    End Sub

    <Test()> Public Sub TestTrajFromAccelDistanceMaxVelBegVelEndVel()
        ' distance is separation of Positions at start of move
        ' NOT distance moved by first position if finalVel != 0

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, 17, 5, 1, -1)
        Assert.AreEqual(5, pTrajectory.TotalTime)
        Assert.AreEqual(12, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, 17, 9999, 1, -1)
        Assert.AreEqual(5, pTrajectory.TotalTime)
        Assert.AreEqual(12, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, 29, 5, 1, -1)
        Assert.AreEqual(7, pTrajectory.TotalTime)
        Assert.AreEqual(22, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, 68, 10, 6, 2)
        Assert.AreEqual(11, pTrajectory.TotalTime)
        Assert.AreEqual(90, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, 14, 10, 6, 2)
        Assert.AreEqual(4, pTrajectory.TotalTime)
        Assert.AreEqual(22, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, -17, 5, -1, 1)
        Assert.AreEqual(5, pTrajectory.TotalTime)
        Assert.AreEqual(-12, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, -29, 5, -1, 1)
        Assert.AreEqual(7, pTrajectory.TotalTime)
        Assert.AreEqual(-22, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, -68, 10, -6, -2)
        Assert.AreEqual(11, pTrajectory.TotalTime)
        Assert.AreEqual(-90, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVel(pTrajectory, 2, -14, 10, -6, -2)
        Assert.AreEqual(4, pTrajectory.TotalTime)
        Assert.AreEqual(-22, pTrajectory.TotalDistance)
    End Sub

    <Test()> Public Sub TestTrajFromAccelDistanceMaxVelBegVelEndVelFirstTrajZeroEndVel()
        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVelFirstTrajZeroEndVel(pTrajectory, 2, 67, 10, 6, 2)
        Assert.AreEqual(12, pTrajectory.TotalTime)
        Assert.AreEqual(91, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelDistanceMaxVelBegVelEndVelFirstTrajZeroEndVel(pTrajectory, 2, -67, 10, -6, -2)
        Assert.AreEqual(12, pTrajectory.TotalTime)
        Assert.AreEqual(-91, pTrajectory.TotalDistance)
    End Sub

    <Test()> Public Sub TestTrajFromAccelBegVelTargetVel()
        pTrajectory.TrajFromAccelBegVelTargetVel(pTrajectory, 2, 3, 6)
        Assert.AreEqual(1.5, pTrajectory.TotalTime)
        Assert.AreEqual(6.75, pTrajectory.TotalDistance)

        pTrajectory.TrajFromAccelBegVelTargetVel(pTrajectory, 2, 3, -6)
        Assert.AreEqual(4.5, pTrajectory.TotalTime)
        Assert.AreEqual(-6.75, pTrajectory.TotalDistance)
    End Sub

    <Test()> Public Sub TestDistanceFromTimeAccelBegVelTargetVel()
        Assert.AreEqual(0, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 0, 2, 3, -6))
        Assert.AreEqual(1.25, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 0.5, 2, 3, -6))
        Assert.AreEqual(2, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 1, 2, 3, -6))
        Assert.AreEqual(2.25, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 1.5, 2, 3, -6))
        Assert.AreEqual(2, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 2, 2, 3, -6))
        Assert.AreEqual(1.25, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 2.5, 2, 3, -6))
        Assert.AreEqual(0, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 3, 2, 3, -6))
        Assert.AreEqual(-1.75, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 3.5, 2, 3, -6))
        Assert.AreEqual(-4, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 4, 2, 3, -6))
        Assert.AreEqual(-6.75, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 4.5, 2, 3, -6))
        Assert.AreEqual(-9.75, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 5, 2, 3, -6))
        Assert.AreEqual(-12.75, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 5.5, 2, 3, -6))
        Assert.AreEqual(-15.75, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 6, 2, 3, -6))
        Assert.AreEqual(-18.75, pTrajectory.DistanceFromTimeAccelBegVelTargetVel(pTrajectory, 6.5, 2, 3, -6))
    End Sub

    <Test()> Public Sub TestVelFromTimeAccelBegVelTargetVel()
        Assert.AreEqual(3, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 0, 2, 3, -6))
        Assert.AreEqual(2, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 0.5, 2, 3, -6))
        Assert.AreEqual(1, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 1, 2, 3, -6))
        Assert.AreEqual(0, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 1.5, 2, 3, -6))
        Assert.AreEqual(-1, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 2, 2, 3, -6))
        Assert.AreEqual(-2, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 2.5, 2, 3, -6))
        Assert.AreEqual(-3, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 3, 2, 3, -6))
        Assert.AreEqual(-4, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 3.5, 2, 3, -6))
        Assert.AreEqual(-5, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 4, 2, 3, -6))
        Assert.AreEqual(-6, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 4.5, 2, 3, -6))
        Assert.AreEqual(-6, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 5, 2, 3, -6))
        Assert.AreEqual(-6, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 5.5, 2, 3, -6))
        Assert.AreEqual(-6, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 6, 2, 3, -6))
        Assert.AreEqual(-6, pTrajectory.VelFromTimeAccelBegVelTargetVel(pTrajectory, 6.5, 2, 3, -6))
    End Sub

    <Test()> Public Sub Test()
    End Sub

    <TearDown()> Public Sub Dispose()
        pTrajectory = Nothing
    End Sub
End Class
