Public Class Units

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    'JD on Greenwich Jan 1, 2000, noon
    Public Const JD2000 As Double = 2451545
    Public Const JDYear As Double = 2000

    ' 1 revolution = 2 Pi Radians = 360 Degrees = 24 Hours
    Public Const OneRev As Double = 2 * Math.PI
    Public Const ThreeFourthsRev As Double = 1.5 * Math.PI
    Public Const HalfRev As Double = Math.PI
    Public Const QtrRev As Double = 0.5 * Math.PI
    Public Const RadToRev As Double = 1 / OneRev
    Public Const RevToRad As Double = OneRev
    Public Const RadToDeg As Double = 360 / OneRev
    Public Const DegToRad As Double = OneRev / 360
    Public Const RadToArcmin As Double = 60 * RadToDeg
    Public Const ArcminToRad As Double = DegToRad / 60
    Public Const RadToArcsec As Double = 60 * RadToArcmin
    Public Const ArcsecToRad As Double = ArcminToRad / 60
    Public Const RadToTenthsArcsec As Double = 100 * RadToArcsec
    Public Const TenthsArcsecToRad As Double = ArcsecToRad / 10
    Public Const RadToMilliDeg As Double = 1000 * RadToDeg
    Public Const MilliDegToRad As Double = DegToRad / 1000
    Public Const RadToHr As Double = 24 / OneRev
    Public Const HrToRad As Double = OneRev / 24
    Public Const RadToMin As Double = 60 * RadToHr
    Public Const MinToRad As Double = HrToRad / 60
    Public Const RadToSec As Double = 60 * RadToMin
    Public Const SecToRad As Double = MinToRad / 60
    Public Const RadToHundSec As Double = 100 * RadToSec
    Public Const HundSecToRad As Double = SecToRad / 100
    Public Const RadToMilliSec As Double = 1000 * RadToSec
    Public Const MilliSecToRad As Double = SecToRad / 1000
    Public Const DayToYear As Double = 365.25
    Public Const YearToDay As Double = 1 / DayToYear
    Public Const DayToHr As Double = 24
    Public Const HrToDay As Double = 1 / DayToHr
    Public Const DayToMin As Double = 60 * DayToHr
    Public Const MinToDay As Double = 1 / DayToMin
    Public Const DayToSec As Double = 60 * DayToMin
    Public Const SecToDay As Double = 1 / DayToSec
    Public Const RevToArcsec As Double = 1296000
    Public Const ArcsecToRev As Double = 1 / RevToArcsec
    Public Const DegToArcsec As Double = 3600
    Public Const ArcsecToDeg As Double = 1 / DegToArcsec
    Public Const DegToArcmin As Double = 60
    Public Const ArcminToDeg As Double = 1 / DegToArcmin
    Public Const SidRate As Double = 1.002737909
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

    'Public Shared Function GetInstance() As Units
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Units = New Units
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Units
        Return New Units
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class