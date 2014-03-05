#Region "Imports"
#End Region

Public Class Trajectory

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public RampUp As MotionVector
    Public MaxVel As MotionVector
    Public RampDown As MotionVector
    Public TotalTime As Double
    Public TotalDistance As Double
    Public StartSidT As Double
    Public StartRampDownSidT As Double
    Public EndSidT As Double
    Public MoveState As ISFT
#End Region

#Region "Private and Protected Members"
    Dim pTime As Time
    Dim pTd As MotionVector
    Dim pHoldPosDeg As Double
    Dim pDeltaPosDeg As Double
    Dim pErrorPosDeg As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Trajectory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Trajectory = New Trajectory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        RampUp = MotionVector.GetInstance
        MaxVel = MotionVector.GetInstance
        RampDown = MotionVector.GetInstance
        pTime = Time.GetInstance
        pTd = MotionVector.GetInstance

        EndSidT = pTime.CalcSidTNow
        MoveState = TrajMoveState.WaitToBuildTraj
    End Sub

    Public Shared Function GetInstance() As Trajectory
        Return New Trajectory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

    ' make accel fit direction implied with begVel and finalVel

    Public Function SignAccel(ByVal accel As Double, ByVal begVel As Double, ByVal finalVel As Double) As Double
        If finalVel > begVel Then
            Return Math.Abs(accel)
        Else
            Return -Math.Abs(accel)
        End If
    End Function

    Public Sub TimeDistanceFromAccelBegVelEndVel(ByRef td As MotionVector, ByVal accel As Double, ByVal begVel As Double, ByVal finalVel As Double)
        accel = SignAccel(accel, begVel, finalVel)
        td.Time = Math.Abs((begVel - finalVel) / accel)
        td.Distance = begVel * td.Time + accel * td.Time * td.Time / 2
    End Sub

    ' include check for time

    Public Sub TimeDistanceFromTimeAccelBegVelEndVel(ByRef td As MotionVector, ByVal time As Double, ByVal accel As Double, ByVal begVel As Double, ByVal finalVel As Double)
        accel = SignAccel(accel, begVel, finalVel)
        td.Time = Math.Abs((begVel - finalVel) / accel)
        If time < td.Time Then
            td.Time = time
        End If
        td.Distance = begVel * td.Time + accel * td.Time * td.Time / 2
    End Sub

    'time+distance for two trajectories to close within so that when change between begVel and finalVel
    'is finished the two trajectories coincide
    'distance is (final vel's start position) - start position
    'takes into account position change based on final vel and time that occurs during move

    Public Sub TriggerTimeDistanceFromAccelBegVelEndVel(ByRef td As MotionVector, ByVal accel As Double, ByVal begVel As Double, ByVal finalVel As Double)
        ' time, distance for initial position to complete change from begVel to finalVel
        TimeDistanceFromAccelBegVelEndVel(td, accel, begVel, finalVel)
        ' distance of final position travel while initial position is completing its move
        Dim finalPosDistance As Double = finalVel * td.Time
        td.Distance -= finalPosDistance
    End Sub

    ' does NOT take into account the distance traveled while move in progress

    Public Function MaxVelFromAccelpUnCompDistance_BegVelEndVel(ByVal accel As Double, ByVal distance As Double, ByVal begVel As Double, ByVal finalVel As Double) As Double
        Dim negativeMV As Boolean
        Dim mv As Double

        If accel < 0 Then
            accel = -accel
        End If
        If distance < 0 Then
            negativeMV = True
            distance = -distance
            begVel = -begVel
            finalVel = -finalVel

        Else
            negativeMV = False
        End If

        mv = Math.Sqrt(accel * distance + begVel * begVel / 2 + finalVel * finalVel / 2)
        If negativeMV Then
            Return -mv
        Else
            Return mv
        End If
    End Function

    ' takes into account the distance traveled while move in progress: distance parameter is starting separation
    ' between Positions or net distance to cover if finalVel != 0, distance moved by by first position in order to
    ' reach target != net distance since target has moved by finalVel*timepOfpFirstpPositionpMove
    ' since time is not known, net distance cannot be calculated from finalVel*time, so must use this equation

    Public Function MaxVelFromAccelDistanceBegVelEndVel(ByVal accel As Double, ByVal distance As Double, ByVal begVel As Double, ByVal finalVel As Double) As Double
        Dim negativeMV As Boolean
        Dim mv As Double

        If accel < 0 Then
            accel = -accel
        End If
        If distance < 0 Then
            negativeMV = True
            distance = -distance
            begVel = -begVel
            finalVel = -finalVel
        Else
            negativeMV = False
        End If

        mv = Math.Sqrt(accel * distance - begVel * finalVel + begVel * begVel / 2 + finalVel * finalVel / 2) + finalVel
        If negativeMV Then
            Return -mv
        Else
            Return mv
        End If
    End Function

    ' takes into account the distance traveled while move in progress: distance parameter is starting separation
    ' between Positions or net distance to cover if finalVel != 0, distance moved by by first position in order to
    ' reach target != net distance since target has moved by finalVel*timepOfpFirstpPositionpMove
    ' since time is not known, net distance cannot be calculated from finalVel*time, so must use this equation
    ' first trajectory ends with zero final velocity matching position of second trajectory which continues at final velocity

    Public Function MaxVelFromAccelDistanceBegVelEndVelFirstTrajZeroEndVel(ByVal accel As Double, ByVal distance As Double, ByVal begVel As Double, ByVal finalVel As Double) As Double
        Dim negativeMV As Boolean
        Dim mv As Double

        If accel < 0 Then
            accel = -accel
        End If
        If distance < 0 Then
            negativeMV = True
            distance = -distance
            begVel = -begVel
            finalVel = -finalVel
        Else
            negativeMV = False
        End If

        mv = Math.Sqrt(accel * distance - begVel * finalVel + begVel * begVel / 2 + finalVel * finalVel) + finalVel
        If negativeMV Then
            Return -mv
        Else
            Return mv
        End If
    End Function

    ' does NOT take into account the distance traveled while move in progress

    Public Function MaxVelFromTimepUnCompDistance_BegVelEndVel(ByVal time As Double, ByVal distance As Double, ByVal begVel As Double, ByVal finalVel As Double) As Double
        Dim negativeMV As Boolean
        Dim s As Double
        Dim mv As Double

        If time <= 0 Then
            time = 1 / Double.MaxValue
        End If

        If distance < 0 Then
            negativeMV = True
            distance = -distance
            begVel = -begVel
            finalVel = -finalVel

        Else
            negativeMV = False
        End If

        ' after 's' is checked, rest of equation is completed
        s = Math.Sqrt(begVel * begVel / 2 + finalVel * finalVel / 2 - (begVel + finalVel) * distance / time + Math.Pow(distance / time, 2))
        If distance < time * (begVel + finalVel) / 2 Then
            s = -s
        End If

        mv = s + distance / time
        If negativeMV Then
            Return -mv
        Else
            Return mv
        End If
    End Function

    ' takes into account the distance traveled while move in progress: distance parameter is starting separation
    ' between Positions or net distance to cover if finalVel != 0, distance moved by by first position in order to
    ' reach target != net distance since target has moved by finalVel*timepOfpFirstpPositionpMove

    Public Function MaxVelFromTimeDistanceBegVelEndVel(ByVal time As Double, ByVal distance As Double, ByVal begVel As Double, ByVal finalVel As Double) As Double
        Dim newDistance As Double = distance + finalVel * time
        Return MaxVelFromTimepUnCompDistance_BegVelEndVel(time, newDistance, begVel, finalVel)
    End Function

    Public Sub TrajFromTimeDistanceBegVelEndVel(ByRef traj As Trajectory, ByVal time As Double, ByVal distance As Double, ByVal begVel As Double, ByVal finalVel As Double)
        Dim maxVel As Double = MaxVelFromTimeDistanceBegVelEndVel(time, distance, begVel, finalVel)
        Dim accel As Double = Math.Abs(2 * maxVel - begVel - finalVel) / time
        TrajFromAccelDistanceMaxVelBegVelEndVel(traj, accel, distance, maxVel, begVel, finalVel)
    End Sub

    ' if Positions in radians and time in seconds, return final vel will be radians/sec
    ' if Positions in radians and time in radians, return final vel will be radians/radian

    Public Function FinalVelFromAccelDistanceTimeDiff(ByVal accel As Double, ByVal distance As Double, ByVal timediff As Double) As Double
        Dim avgvel As Double = distance / timediff
        Dim finalVel As Double = avgvel + accel * timediff / 2
        Return finalVel
    End Function

    ' builds a Positional trapezoidal trajectory
    ' assumes starting and ending velocities of zero

    Public Sub TrajFromAccelDistanceMaxVel(ByRef traj As Trajectory, ByVal accel As Double, ByVal distance As Double, ByVal maxvel As Double)
        Dim Rotation As ISFT
        Dim maxVelToUse As Double
        Dim remainDistance As Double
        Dim absMaxVel As Double

        If distance >= 0 Then
            Rotation = BartelsLibrary.Rotation.CW
        Else
            distance = -distance
            Rotation = BartelsLibrary.Rotation.CCW
        End If
        absMaxVel = Math.Abs(maxvel)
        ' compare theoretical max velocity of trajectory to passed in maxvel (which should always be + value)
        Dim theoMaxVel As Double = MaxVelFromAccelpUnCompDistance_BegVelEndVel(accel, distance, 0, 0)
        If theoMaxVel > absMaxVel Then
            maxVelToUse = absMaxVel
        Else
            maxVelToUse = theoMaxVel
        End If

        TimeDistanceFromAccelBegVelEndVel(pTd, accel, 0, maxVelToUse)
        traj.RampUp.BegVel = 0
        traj.RampUp.EndVel = maxVelToUse
        traj.RampUp.Accel = accel
        traj.RampUp.Time = pTd.Time
        traj.RampUp.Distance = pTd.Distance

        TimeDistanceFromAccelBegVelEndVel(pTd, accel, maxVelToUse, 0)
        traj.RampDown.BegVel = maxVelToUse
        traj.RampDown.EndVel = 0
        traj.RampDown.Accel = -accel
        traj.RampDown.Time = pTd.Time
        traj.RampDown.Distance = pTd.Distance

        traj.MaxVel.BegVel = maxVelToUse
        traj.MaxVel.EndVel = maxVelToUse
        traj.MaxVel.Accel = 0

        ' remaining distance
        remainDistance = distance - (traj.RampUp.Distance + traj.RampDown.Distance)

        ' if remainDistance>0, then equivalent to myMaxVel>maxvel where move is a trapezoid,
        ' else move is a sawtooth (possibly truncated by reduced max vel)
        traj.MaxVel.Time = Math.Abs(remainDistance / maxVelToUse)
        traj.MaxVel.Distance = traj.MaxVel.Time * maxVelToUse
        traj.TotalTime = traj.RampUp.Time + traj.RampDown.Time + traj.MaxVel.Time
        traj.TotalDistance = traj.RampUp.Distance + traj.RampDown.Distance + traj.MaxVel.Distance

        If Rotation Is BartelsLibrary.Rotation.CCW Then
            traj.RampUp.EndVel = -traj.RampUp.EndVel
            traj.RampUp.Accel = -traj.RampUp.Accel
            traj.RampUp.Distance = -traj.RampUp.Distance

            traj.RampDown.BegVel = -traj.RampDown.BegVel
            traj.RampDown.Accel = -traj.RampDown.Accel
            traj.RampDown.Distance = -traj.RampDown.Distance

            traj.MaxVel.BegVel = -traj.MaxVel.BegVel
            traj.MaxVel.EndVel = -traj.MaxVel.EndVel
            traj.MaxVel.Distance = -traj.MaxVel.Distance

            traj.TotalDistance = -traj.TotalDistance
        End If
    End Sub

    ' return distance traveled in a Positional trapezoidal trajectory based on time elapsed where begVel and endVel are zero

    Public Function DistanceFromTimeAccelDistanceMaxVel(ByRef traj As Trajectory, ByVal time As Double, ByVal accel As Double, ByVal distance As Double, ByVal maxvel As Double) As Double
        TrajFromAccelDistanceMaxVel(traj, accel, distance, maxvel)
        Return DistanceFromTrajFromAccelDistanceMaxVel(traj, time, accel, maxvel)
    End Function

    Public Function DistanceFromTrajFromAccelDistanceMaxVel(ByRef traj As Trajectory, ByVal time As Double, ByVal accel As Double, ByVal maxvel As Double) As Double
        Dim distance As Double

        If time >= traj.RampUp.Time Then
            distance = traj.RampUp.Distance
            If time >= traj.RampUp.Time + traj.MaxVel.Time Then
                distance += traj.MaxVel.Distance
                If time >= traj.TotalTime Then
                    distance = traj.TotalDistance
                Else
                    TimeDistanceFromTimeAccelBegVelEndVel(pTd, time - (traj.RampUp.Time + traj.MaxVel.Time), traj.RampDown.Accel, traj.RampDown.BegVel, 0)
                    distance += pTd.Distance
                End If
            Else
                distance += (time - traj.RampUp.Time) * traj.MaxVel.BegVel
            End If
        Else
            TimeDistanceFromTimeAccelBegVelEndVel(pTd, time, accel, 0, maxvel)
            distance = pTd.Distance
        End If

        Return distance
    End Function

    ' return velocity at time 'time' in a Positional trapezoidal trajectory distance where begVel and endVel are zero

    Public Function VelFromTimeAccelDistanceMaxVel(ByRef traj As Trajectory, ByVal time As Double, ByVal accel As Double, ByVal distance As Double, ByVal maxvel As Double) As Double
        TrajFromAccelDistanceMaxVel(traj, accel, distance, maxvel)
        Return VelFromTrajFromAccelDistanceMaxVel(traj, time)
    End Function

    Public Function VelFromTrajFromAccelDistanceMaxVel(ByRef traj As Trajectory, ByVal time As Double) As Double
        Dim timediff As Double

        If time >= traj.TotalTime Then
            Return 0
        End If

        timediff = time - (traj.RampUp.Time + traj.MaxVel.Time)
        If timediff >= 0 Then
            Return (traj.RampDown.Time - timediff) / traj.RampDown.Time * (traj.RampDown.BegVel - traj.RampDown.EndVel)
        End If

        timediff = time - traj.RampUp.Time
        If timediff >= 0 Then
            Return traj.MaxVel.BegVel
        End If

        Return time / traj.RampUp.Time * (traj.RampUp.EndVel - traj.RampUp.BegVel)
    End Function

    ' takes into account the distance traveled while move in progress: distance parameter is starting separation
    ' between Positions or net distance to cover if finalVel != 0, distance moved by by first position in order to
    ' reach target != net distance since target has moved by finalVel*timepOfpFirstpPositionpMove

    Public Sub TrajFromAccelDistanceMaxVelBegVelEndVel(ByRef traj As Trajectory, ByVal accel As Double, ByVal distance As Double, ByVal maxvel As Double, ByVal begVel As Double, ByVal finalVel As Double)
        Dim myMaxVel As Double
        Dim maxVelToUse As Double
        Dim timeRamping As Double
        Dim targetPos As Double
        Dim remainDistance As Double

        myMaxVel = MaxVelFromAccelDistanceBegVelEndVel(accel, distance, begVel, finalVel)
        ' point maxvel in same direction as myMaxVel
        If myMaxVel > 0 Then
            maxvel = Math.Abs(maxvel)
        Else
            maxvel = -Math.Abs(maxvel)
        End If
        ' adjust maxVelToUse to not exceed maxvel
        If myMaxVel > 0 AndAlso myMaxVel > maxvel OrElse myMaxVel < 0 AndAlso myMaxVel < maxvel Then
            maxVelToUse = maxvel
        Else
            maxVelToUse = myMaxVel
        End If

        TimeDistanceFromAccelBegVelEndVel(pTd, accel, begVel, maxVelToUse)
        traj.RampUp.BegVel = begVel
        traj.RampUp.EndVel = maxVelToUse
        traj.RampUp.Accel = accel
        traj.RampUp.Time = pTd.Time
        traj.RampUp.Distance = pTd.Distance

        TimeDistanceFromAccelBegVelEndVel(pTd, accel, maxVelToUse, finalVel)
        traj.RampDown.BegVel = maxVelToUse
        traj.RampDown.EndVel = finalVel
        traj.RampDown.Accel = -accel
        traj.RampDown.Time = pTd.Time
        traj.RampDown.Distance = pTd.Distance

        traj.MaxVel.BegVel = maxVelToUse
        traj.MaxVel.EndVel = maxVelToUse
        traj.MaxVel.Accel = 0

        timeRamping = traj.RampUp.Time + traj.RampDown.Time
        ' target position moves finalVel*timeRamping distance during timeRamping time
        targetPos = finalVel * timeRamping
        ' remaining distance
        remainDistance = distance - (traj.RampUp.Distance + traj.RampDown.Distance - targetPos)

        ' if remainDistance!=0, then equivalent to myMaxVel>maxvel where move is a trapezoid,
        ' else move is a sawtooth (possibly truncated by reduced max vel)
        traj.MaxVel.Time = Math.Abs(remainDistance / (maxVelToUse - finalVel))
        traj.MaxVel.Distance = traj.MaxVel.Time * maxVelToUse
        If remainDistance < 0 Then
            traj.MaxVel.Distance = -Math.Abs(traj.MaxVel.Distance)
        End If
        traj.TotalTime = traj.RampUp.Time + traj.RampDown.Time + traj.MaxVel.Time
        traj.TotalDistance = traj.RampUp.Distance + traj.RampDown.Distance + traj.MaxVel.Distance
    End Sub

    ' takes into account the distance traveled while move in progress: distance parameter is starting separation
    ' between Positions or net distance to cover if finalVel != 0, distance moved by by first position in order to
    ' reach target != net distance since target has moved by finalVel*timepOfpFirstpPositionpMove
    ' first trajectory ends with zero final velocity matching position of second trajectory which continues at final velocity

    Public Sub TrajFromAccelDistanceMaxVelBegVelEndVelFirstTrajZeroEndVel(ByRef traj As Trajectory, ByVal accel As Double, ByVal distance As Double, ByVal maxvel As Double, ByVal begVel As Double, ByVal finalVel As Double)
        Dim myMaxVel As Double
        Dim maxVelToUse As Double
        Dim timeRamping As Double
        Dim targetPos As Double
        Dim remainDistance As Double

        myMaxVel = MaxVelFromAccelDistanceBegVelEndVelFirstTrajZeroEndVel(accel, distance, begVel, finalVel)
        ' point maxvel in same direction as myMaxVel
        If myMaxVel > 0 Then
            maxvel = Math.Abs(maxvel)
        Else
            maxvel = -Math.Abs(maxvel)
        End If
        ' adjust maxVelToUse to not exceed maxvel
        If myMaxVel > 0 AndAlso myMaxVel > maxvel OrElse myMaxVel < 0 AndAlso myMaxVel < maxvel Then
            maxVelToUse = maxvel
        Else
            maxVelToUse = myMaxVel
        End If

        TimeDistanceFromAccelBegVelEndVel(pTd, accel, begVel, maxVelToUse)
        traj.RampUp.BegVel = begVel
        traj.RampUp.EndVel = maxVelToUse
        traj.RampUp.Accel = accel
        traj.RampUp.Time = pTd.Time
        traj.RampUp.Distance = pTd.Distance

        TimeDistanceFromAccelBegVelEndVel(pTd, accel, maxVelToUse, 0)
        traj.RampDown.BegVel = maxVelToUse
        traj.RampDown.EndVel = 0
        traj.RampDown.Accel = -accel
        traj.RampDown.Time = pTd.Time
        traj.RampDown.Distance = pTd.Distance

        traj.MaxVel.BegVel = maxVelToUse
        traj.MaxVel.EndVel = maxVelToUse
        traj.MaxVel.Accel = 0

        timeRamping = traj.RampUp.Time + traj.RampDown.Time
        ' target position moves finalVel*timeRamping distance during timeRamping time
        targetPos = finalVel * timeRamping
        ' remaining distance
        remainDistance = distance - (traj.RampUp.Distance + traj.RampDown.Distance - targetPos)

        ' if remainDistance!=0, then equivalent to myMaxVel>maxvel where move is a trapezoid,
        ' else move is a sawtooth (possibly truncated by reduced max vel)
        traj.MaxVel.Time = Math.Abs(remainDistance / (maxVelToUse - finalVel))
        traj.MaxVel.Distance = traj.MaxVel.Time * maxVelToUse
        If remainDistance < 0 Then
            traj.MaxVel.Distance = -Math.Abs(traj.MaxVel.Distance)
        End If
        traj.TotalTime = traj.RampUp.Time + traj.RampDown.Time + traj.MaxVel.Time
        traj.TotalDistance = traj.RampUp.Distance + traj.RampDown.Distance + traj.MaxVel.Distance
    End Sub

    ' does NOT take into account the distance traveled while move in progress
    ' velocity trajectory in form of line of constant slope from begVel to endVel

    Public Sub TrajFromAccelBegVelTargetVel(ByRef traj As Trajectory, ByVal accel As Double, ByVal begVel As Double, ByVal endVel As Double)
        TimeDistanceFromAccelBegVelEndVel(pTd, accel, begVel, endVel)
        traj.RampUp.BegVel = begVel
        traj.RampUp.Accel = SignAccel(accel, begVel, endVel)
        traj.RampUp.Time = pTd.Time
        traj.RampUp.EndVel = traj.RampUp.Time * traj.RampUp.Accel + begVel
        traj.RampUp.Distance = pTd.Distance

        traj.MaxVel.Time = 0
        traj.MaxVel.Distance = 0
        traj.MaxVel.BegVel = traj.RampUp.EndVel
        traj.MaxVel.EndVel = traj.RampUp.EndVel

        traj.RampDown.BegVel = traj.RampUp.EndVel
        traj.RampDown.EndVel = traj.RampUp.EndVel
        traj.RampDown.Accel = 0
        traj.RampDown.Time = 0
        traj.RampDown.Distance = 0

        traj.TotalTime = traj.RampUp.Time
        traj.TotalDistance = traj.RampUp.Distance
    End Sub

    ' does NOT take into account the distance traveled while move in progress
    ' velocity trajectory in form of line of constant slope followed by horizontal line, ie, a deccel situation: \__ 
    ' returns distance at time 'time' in the trajectory

    Public Function DistanceFromTimeAccelBegVelTargetVel(ByRef traj As Trajectory, ByVal time As Double, ByVal accel As Double, ByVal begVel As Double, ByVal endVel As Double) As Double
        TrajFromAccelBegVelTargetVel(traj, accel, begVel, endVel)
        Return DistanceFromTrajFromTimeAccelBegVelTargetVel(traj, time)
    End Function

    Public Function DistanceFromTrajFromTimeAccelBegVelTargetVel(ByRef traj As Trajectory, ByVal time As Double) As Double
        Dim timediff As Double = time - traj.RampUp.Time
        If timediff >= 0 Then
            Return traj.RampUp.Distance + timediff * traj.MaxVel.BegVel
        End If
        TimeDistanceFromTimeAccelBegVelEndVel(pTd, time, traj.RampUp.Accel, traj.RampUp.BegVel, traj.RampUp.EndVel)
        Return pTd.Distance
    End Function

    ' does NOT take into account the distance traveled while move in progress
    ' velocity trajectory in form of line of constant slope followed by horizontal line, ie, a deccel situation: \__ 
    ' returns velocity at time 'time' in the trajectory

    Public Function VelFromTimeAccelBegVelTargetVel(ByRef traj As Trajectory, ByVal time As Double, ByVal accel As Double, ByVal begVel As Double, ByVal endVel As Double) As Double
        TrajFromAccelBegVelTargetVel(traj, accel, begVel, endVel)
        Return VelFromTrajFromTimeAccelBegVelTargetVel(traj, time)
    End Function

    Public Function VelFromTrajFromTimeAccelBegVelTargetVel(ByRef traj As Trajectory, ByVal time As Double) As Double
        If time - traj.RampUp.Time >= 0 Then
            Return traj.MaxVel.BegVel
        End If
        Return time / traj.RampUp.Time * (traj.RampUp.EndVel - traj.RampUp.BegVel) + traj.RampUp.BegVel
    End Function

#End Region

#Region "Private and Protected Methods"
#End Region

End Class
