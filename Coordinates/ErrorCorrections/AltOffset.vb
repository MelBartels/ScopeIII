''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.AltOffset
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Offset measured as the amount to add to altitude to arrive at the true value.
''' If offset is -1, then altitude is 1 too high.
''' 
''' Input parameters required: 2 positions each with altaz and equat coordinates.  No CoordXform is needed.
''' 
''' Independent of coordinate transform method.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	3/2/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class AltOffset

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public AltOffset As Coordinate
#End Region

#Region "Private and Protected Members"
    Dim pCelestialCoordinateCalcs As CelestialCoordinateCalcs
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As AltOffset
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AltOffset = New AltOffset
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        AltOffset = Coordinate.GetInstance
        pCelestialCoordinateCalcs = CelestialCoordinateCalcs.GetInstance
    End Sub

    Public Shared Function GetInstance() As AltOffset
        Return New AltOffset
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' angular separation of two equatorial coordinates should = the angular separation of the corresponding altazimuth coordinates;
    ' for target altitudes that cross the equator of their coordinate system, there are two solutions
    ' formula from Dave Ek <ekdave@earthlink.net>
    Public Function CalcAltOffsetDirectly(ByRef a As Position, ByRef z As Position) As Coordinate
        Dim a1 As Double
        Dim a2 As Double
        Dim n As Double = Math.Cos(a.Az.Rad - z.Az.Rad)
        Dim m As Double = Math.Cos(pCelestialCoordinateCalcs.CalcEquatAngularSepViaHrAngle(a, z))
        Dim x As Double = (2 * m - (n + 1) * Math.Cos(a.Alt.Rad - z.Alt.Rad)) / (n - 1)

        ' likely causes: azimuths not separate enough resulting in n-1 term being too small, or, variation from ideal numbers in other variables
        If x > 1 Then
            x = 1
        ElseIf x < -1 Then
            x = -1
        End If

        a1 = 0.5 * (+Math.Acos(x) - a.Alt.Rad - z.Alt.Rad)
        a2 = 0.5 * (-Math.Acos(x) - a.Alt.Rad - z.Alt.Rad)

        If (Math.Abs(a1) < Math.Abs(a2)) Then
            AltOffset.Rad = a1
        Else
            AltOffset.Rad = a2
        End If

        DebugTrace.WriteLine("altOffset (deg) = " & AltOffset.Rad * Units.RadToDeg)

        Return AltOffset
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' when angular separation of altaz values closest to that of equat values, best altitude offset found
    ''' work with copy of Positions as .alt values changed
    ''' +- 45 deg range
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="z"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	2/28/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function CalcAltOffsetIteratively(ByRef a As Position, ByRef z As Position) As Coordinate
        Dim aa As Position = PositionArraySingleton.GetInstance.GetPosition
        Dim zz As Position = PositionArraySingleton.GetInstance.GetPosition
        aa.CopyFrom(a)
        zz.CopyFrom(z)
        Dim bestAltOff As Double = Double.MaxValue
        Dim diff As Double
        Dim lastDiff As Double
        Dim bestDiff As Double
        Dim incr As Double = Units.ArcsecToRad
        Dim iter As Int32
        ' +- 45 deg search range
        Dim maxIter As Int32 = CType(45 * Units.DegToRad / incr, Int32)
        Dim begA As Double = aa.Alt.Rad
        Dim begZ As Double = zz.Alt.Rad

        bestDiff = Double.MaxValue

        ' start from zero offset and increment offset until difference starts to get worse
        lastDiff = Double.MaxValue
        iter = 0
        While iter < maxIter
            diff = pCelestialCoordinateCalcs.AngSepDiffViaHrAngle(aa, zz)
            If diff < bestDiff Then
                bestDiff = diff
                bestAltOff = aa.Alt.Rad - begA
            End If
            If (diff > lastDiff) Then
                Exit While
            Else
                lastDiff = diff
            End If

            iter += 1
            aa.Alt.Rad += incr
            zz.Alt.Rad += incr
        End While

        ' again, start from zero offset, but this time decrement offset
        aa.Alt.Rad = begA
        zz.Alt.Rad = begZ
        diff = Double.MaxValue
        lastDiff = Double.MaxValue
        iter = 0
        While iter < maxIter
            diff = pCelestialCoordinateCalcs.AngSepDiffViaHrAngle(aa, zz)
            If diff < bestDiff Then
                bestDiff = diff
                bestAltOff = aa.Alt.Rad - begA
            End If
            If diff > lastDiff Then
                Exit While
            Else
                lastDiff = diff
            End If

            iter += 1
            aa.Alt.Rad -= incr
            zz.Alt.Rad -= incr
        End While

        If bestAltOff.Equals(Double.MaxValue) Then
            AltOffset.Rad = 0
        Else
            AltOffset.Rad = bestAltOff
        End If

        'DebugTrace.WriteLine("calcAltOffsetIteratively() calculated altOffset from: " _
        '    & a.ShowCoordDeg() _
        '    & " and " _
        '    & z.ShowCoordDeg())
        DebugTrace.WriteLine("altOffset (deg) = " & AltOffset.Rad * Units.RadToDeg)

        aa.Available = True
        zz.Available = True

        Return AltOffset
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class