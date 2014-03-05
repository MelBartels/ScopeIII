''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.BestZ123
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' compute mount misalignment errors z1, z2, z3 using iterative search;
''' best values picked by mean square of resulting altitude and azimuth errors: that is, find best values of z123 that,
''' after plugging in z123 values, minimize altitude and azimuth errors (azimuth error corrected for cos of altitude);
''' search for z3 outside of search for z12 because including z3 creates multiple local minima that can only be found with brute force
''' or optimization such as annealing;
''' z12 range is +- 7 deg;
''' critically important to separate z1 z2 cleanly by determining accurate azimuths for series of altitudes between 10 and 80 deg;
''' routine run time is 3 sec on 2ghz machine in debug, 6 sec running Java from DOS prompt (similar code compiled in C and
''' run in DOS completes 10x faster);
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	3/8/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class BestZ123

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public BestZ1 As Double
    Public BestZ2 As Double
    Public BestZ3 As Double
    Public Z12Range As Double = 25200 * Units.ArcsecToRad
    Public Z12Interval As Double = 1800 * Units.ArcsecToRad
    Public MinInterval As Double = 3.6 * Units.ArcsecToRad
#End Region

#Region "Private and Protected Members"
    Private pConvertMatrix As ConvertMatrix
    Private pPositionArray As PositionArray
    Private pAnalysisErrors As AnalysisErrors
    Private pAltoffset As AltOffset
    Private pTotal As Double
    Private pAltOffsetCount As Int32
    Private pZ12count As Int32
    Private pBestPointErrRMS As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As BestZ123
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As BestZ123 = New BestZ123
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pAltoffset = AltOffset.GetInstance
        pAnalysisErrors = AnalysisErrors.GetInstance
    End Sub

    Public Shared Function GetInstance() As BestZ123
        Return New BestZ123
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function BestZ123FromPositionArray(ByRef convertMatrix As ConvertMatrix, ByRef positionArray As PositionArray) As Boolean

        DebugTrace.WriteLine("starting BestZ3FromPositionArray()...")

        pConvertMatrix = convertMatrix
        pPositionArray = positionArray
        pAnalysisErrors.PositionArray = positionArray
        pAnalysisErrors.ICoordXform = convertMatrix

        If pConvertMatrix.One.Init AndAlso pConvertMatrix.Two.Init Then
            If BestZ3FromPositionArray() Then
                Return computeBestZ12FromPositionArray()
            End If
        End If

        Return False
    End Function
#End Region

#Region "Private and Protected Methods"

    Private Function BestZ3FromPositionArray() As Boolean
        AddAltOffset(pConvertMatrix.One, pConvertMatrix.Two)
        If pConvertMatrix.Three.Init Then
            AddAltOffset(pConvertMatrix.One, pConvertMatrix.Three)
            AddAltOffset(pConvertMatrix.Two, pConvertMatrix.Three)
        End If

        Dim ix As Int32
        For Each position As Position In pPositionArray.PositionArray
            AddAltOffset(position, pConvertMatrix.One)
            AddAltOffset(position, pConvertMatrix.Two)
            If pConvertMatrix.Three.Init Then
                AddAltOffset(position, pConvertMatrix.Three)
            End If

            Dim position2 As Position
            Dim ixB As Int32 = 0
            For Each position2 In pPositionArray.PositionArray
                If ixB > ix Then
                    AddAltOffset(position, position2)
                End If
                ixB += 1
            Next

            ix += 1
        Next

        BestZ3 = pTotal / pAltOffsetCount
        DebugTrace.WriteLine("BestZ3FromPositionArray(): BestZ3 " & BestZ3 * Units.RadToDeg)

        Return True
    End Function

    Private Function AddAltOffset(ByRef a As Position, ByRef z As Position) As Boolean
        Dim altOffset As Coordinate = pAltoffset.CalcAltOffsetIteratively(a, z)
        pTotal += altOffset.Rad
        pAltOffsetCount += 1
        Return True
    End Function

    Private Function computeBestZ12FromPositionArray() As Boolean
        Dim startZ1 As Double = 0
        Dim startZ2 As Double = 0

        Return computeBestZ12FromPositionArraySubr(startZ1, startZ2, Z12Range, Z12Interval, MinInterval, BestZ3)
    End Function

    Private Function computeBestZ12FromPositionArraySubr(ByVal startZ1 As Double, ByVal startZ2 As Double, ByVal Z12Range As Double, ByVal Z12Interval As Double, ByVal MinInterval As Double, ByVal workZ3 As Double) As Boolean
        Dim fabErrors As FabErrors = Coordinates.FabErrors.GetInstance
        fabErrors.Z1 = pConvertMatrix.FabErrors.Z1
        fabErrors.Z2 = pConvertMatrix.FabErrors.Z2
        Dim tempPosition As Position = PositionArraySingleton.GetInstance.GetPosition
        tempPosition.CopyFrom(pConvertMatrix.Position)
        pBestPointErrRMS = Double.MaxValue
        BestZ1 = Double.MaxValue
        BestZ2 = Double.MaxValue
        Dim z1 As Double
        Dim z2 As Double
        pZ12count = 0

        Do
            For z1 = startZ1 - Z12Range To startZ1 + Z12Range Step Z12Interval
                For z2 = startZ2 - Z12Range To startZ2 + Z12Range Step Z12Interval
                    pConvertMatrix.FabErrors.SetFabErrorsDeg(z1 * Units.RadToDeg, z2 * Units.RadToDeg, workZ3 * Units.RadToDeg)
                    'DebugTrace.Writeline("z1 " & z1 * Units.RadToDeg & " z2 " & z2 * Units.RadToDeg & " z3 " & workZ3 * Units.RadToDeg)
                    pConvertMatrix.Position.CopyFrom(pConvertMatrix.One)
                    pConvertMatrix.InitMatrix(1)
                    pAnalysisErrors.Calc()
                    If pAnalysisErrors.PointingErrorRMS < pBestPointErrRMS Then
                        pBestPointErrRMS = pAnalysisErrors.PointingErrorRMS
                        BestZ1 = z1
                        BestZ2 = z2
                        'DebugTrace.Writeline(BestZ12RMSToString)
                    End If
                    pZ12count += 1
                Next
            Next
            DebugTrace.WriteLine(BestZ12RMSToString)
            startZ1 = BestZ1
            startZ2 = BestZ2
            Z12Range /= 10
            Z12Interval /= 10
        Loop While Z12Interval >= MinInterval

        DebugTrace.WriteLine(BestZ12RMSToString)

        pConvertMatrix.FabErrors.Z1 = fabErrors.Z1
        pConvertMatrix.FabErrors.Z2 = fabErrors.Z2
        pConvertMatrix.Position.CopyFrom(pConvertMatrix.One)
        pConvertMatrix.InitMatrix(1)
        pConvertMatrix.Position.CopyFrom(tempPosition)
        tempPosition.Available = True
        Return True
    End Function

    Private Function BestZ12RMSToString() As String
        Dim sb As New Text.StringBuilder
        sb.Append("BestZ3FromPositionArray(): BestZ1Deg ")
        sb.Append(BestZ1 * Units.RadToDeg)
        sb.Append(" BestZ2Deg ")
        sb.Append(BestZ2 * Units.RadToDeg)
        sb.Append(", RMS ")
        sb.Append(pBestPointErrRMS * Units.RadToArcmin)
        sb.Append(" arcmin, iterations = ")
        sb.Append(pZ12count)
        Return (sb.ToString)
    End Function
#End Region

End Class
