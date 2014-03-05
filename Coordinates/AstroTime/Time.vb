Public Class Time

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public JD As Double
    Public SidT As Coordinate
    Public Longitude As Coordinate
#End Region

#Region "Private and Protected Members"
    Private pDT2000 As DateTime
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    ' End Sub

    'Public Shared Function GetInstance() As Time
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Time = New Time
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        SidT = Coordinate.GetInstance
        Longitude = Coordinate.GetInstance
        pDT2000 = New DateTime(2000, 1, 1, 12, 0, 0)
    End Sub

    Public Shared Function GetInstance() As Time
        Return New Time
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function CalcJD(ByVal dateTime As DateTime) As Double
        Return CalcJD(dateTime, DefaultUtcOffsetHrs)
    End Function

    Public Function CalcJD(ByVal dateTime As DateTime, ByVal UtcOffsetHrs As Double) As Double
        Dim days As Double = dateTime.Subtract(pDT2000).TotalDays
        JD = Units.JD2000 + days + UtcOffsetHrs * Units.HrToDay
        Return JD
    End Function

    Public Function CalcJDNow() As Double
        Return CalcJD(DateTime.Now, DefaultUtcOffsetHrs)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' from Jean Meeus' Astronomical Algorithms, 2nd ed, chapter 12, pg 88, formula 12.4
    ''' </summary>
    ''' <param name="dateTime"></param>
    ''' <param name="UtcOffsetHrs"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/24/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function CalcSidTGreenwichMean(ByVal dateTime As DateTime, ByVal UtcOffsetHrs As Double) As Double
        CalcJD(dateTime, UtcOffsetHrs)
        Dim T As Double = (JD - Units.JD2000) / (Units.DayToYear * 100)
        Dim SidTGreenwichMeanDeg As Double = 280.46061837 + 360.98564736629 * (JD - Units.JD2000) + 0.000387933 * T * T - T * T * T / 38710000
        SidT.Rad = eMath.ValidRad(SidTGreenwichMeanDeg * Units.degToRad)
        Return SidT.Rad
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Uses Longitude.Rad timer; resolution of tick interval is 1/100 sec
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	2/24/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function CalcSidTNow() As Double
        Dim utcOffsetHrs As Double = DateTime.UtcNow.Subtract(DateTime.Now).TotalHours
        SidT.Rad = eMath.ValidRad(CalcSidTGreenwichMean(DateTime.Now, utcOffsetHrs) - Longitude.Rad)
        Return SidT.Rad
    End Function

    Public Function CreateDateTime(ByVal year As Double) As DateTime
        Dim dt As New DateTime(eMath.RInt(Math.Floor(year)), 1, 1)
        Return dt.AddDays((year - dt.Year) * Units.DayToYear)
    End Function

    Public Function NowDate() As DateTime
        Return New DateTime(Now.Year, Now.Month, Now.Day)
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function DefaultUtcOffsetHrs() As Double
        Return DateTime.UtcNow.Subtract(DateTime.Now).TotalHours
    End Function
#End Region

End Class