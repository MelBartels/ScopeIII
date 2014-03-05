#Region "Imports"
#End Region

Public Class JRKerrCalc

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Shared ReadOnly TickFreqHz As Double = 1953.0
    Public Shared ReadOnly rateDivisorSR As Int32 = 1
    Protected Shared ReadOnly rateDivisorByTickFreq As Double = rateDivisorSR / TickFreqHz
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As JRKerrCalc
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrCalc = New JRKerrCalc
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrCalc
        Return New JRKerrCalc
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' use countsToRad: if 100 counts in 1 rad, then countsToRad = 0.01

    ' velocity in counts/tick = velocity in rad/sec * counts/rad * rateDivisor/(ticks/sec);
    ' counts/tick = velocity rad/sec * counts/rad * rateDivisor*sec/tick;
    ' counts    rad   counts   sec
    ' ------ =  --- * ------ * ----
    ' tick      sec   rad      tick
    ' ie, if higher resolution encoder with more counts per revolution, then step size will be smaller,
    ' causing motor to report a higher counts/tick though maintaining the original rate of rotation;
    ' countsToRad = rads per count = rad/count
    ' 
    ' speed in rad/sec = targetVel / VelRadSecToTargetVel();

    Public Function VelRadSecFromVelocity(ByVal velocity As Int32, ByVal countsToRad As Double) As Double
        Return velocity / VelRadSecToTargetVel(countsToRad)
    End Function

    Public Function VelRevSecFromVelocity(ByVal velocity As Int32, ByVal countsToRad As Double) As Double
        Return VelRadSecFromVelocity(velocity, countsToRad) * Units.RadToRev
    End Function

    Public Function VelDegSecFromVelocity(ByVal velocity As Int32, ByVal countsToRad As Double) As Double
        Return VelRadSecFromVelocity(velocity, countsToRad) * Units.RadToDeg
    End Function

    Public Function VelRadSecToTargetVel(ByVal countsToRad As Double) As Double
        Return 65536 * rateDivisorByTickFreq / countsToRad
    End Function

    ' accel(counts/tick/tick) = accel(rad/sec/sec) = count/rad * sec/tick * sec/tick;
    ' accel(rev/sec/sec) = accel(rev/sec/sec) * rev/rad;
    ' accel(deg/sec/sec) = accel(rad/sec/sec) * deg/rad;
    ' invert all since returning value to convert other way (rad/sec/sec to counts/tick/tick);
    ' 
    ' accel(rev/sec/sec) = acceleration / accelRadSecSecToCountsTickTick() / rad/rev; 
    ' accel(deg/sec/sec) = acceleration / accelRadSecSecToCountsTickTick() / rad/deg; 

    Public Function AccelRadSecSecFromAcceleration(ByVal acceleration As Int32, ByVal countsToRad As Double) As Double
        Return acceleration / AccelRadSecSecToCountsTickTick(countsToRad)
    End Function

    Public Function AccelRevSecSecFromAcceleration(ByVal acceleration As Int32, ByVal countsToRad As Double) As Double
        Return AccelRadSecSecFromAcceleration(acceleration, countsToRad) / Units.RevToRad
    End Function

    Public Function AccelDegSecSecFromAcceleration(ByVal acceleration As Int32, ByVal countsToRad As Double) As Double
        Return AccelRadSecSecFromAcceleration(acceleration, countsToRad) / Units.DegToRad
    End Function

    Public Function AccelRadSecSecToCountsTickTick(ByVal countsToRad As Double) As Double
        Return 65536 / countsToRad * rateDivisorByTickFreq * rateDivisorByTickFreq
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
