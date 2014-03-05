''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.IterativeLatitude
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Iterate for Latitude using the equat coord for 2 positions.
''' 
''' When the alt diff and the az diff of the resulting GetAltaz agree closest to the 
''' alt diff and az diff of the given 2 positions, then Latitude is found.
''' 
''' Use az diff first to pin down hemisphere, then average w/ alt diff.
''' 
''' Required parameters are 2 positions with altaz/equat/sidT values present.
''' 
''' Independent of coordinate transform method.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	3/2/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class IterativeLatitude

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Latitude As Coordinate
#End Region

#Region "Private and Protected Members"
    Dim pCelestialCoordinateCalcs As CelestialCoordinateCalcs
    Dim IIterLatAngSep As IIterLatAngSep
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IterativeLatitude
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IterativeLatitude = New IterativeLatitude
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        Latitude = Coordinate.GetInstance
        pCelestialCoordinateCalcs = CelestialCoordinateCalcs.GetInstance
    End Sub

    Public Shared Function GetInstance() As IterativeLatitude
        Return New IterativeLatitude
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Calc(ByRef a As Position, ByRef z As Position) As Coordinate

        Dim tPa As Position = PositionArraySingleton.GetInstance.GetPosition
        Dim tPz As Position = PositionArraySingleton.GetInstance.GetPosition
        Dim tP1 As Position = PositionArraySingleton.GetInstance.GetPosition
        Dim tP2 As Position = PositionArraySingleton.GetInstance.GetPosition
        tPa.CopyFrom(a)
        tPz.CopyFrom(z)

        ' start w/ az angsep to get proper hemisphere
        IIterLatAngSep = IterLatAngSepAz.GetInstance
        Dim correctAngSep As Double = IIterLatAngSep.Calc(tPa, tPz)
        Dim bestLatAz As Double = SetBestLat(-Units.QtrRev, Units.QtrRev, tPa, tPz, tP1, tP2, correctAngSep)

        ' search for narrow range about best lat as determined by az angsep but using alt angsep
        IIterLatAngSep = IterLatAngSepAlt.GetInstance
        correctAngSep = IIterLatAngSep.Calc(tPa, tPz)
        Dim bestLatAlt As Double = SetBestLat(bestLatAz - 5 * Units.DegToRad, bestLatAz + 5 * Units.DegToRad, tPa, tPz, tP1, tP2, correctAngSep)

        ' average az and alt best latitudes
        Latitude.Rad = (bestLatAz + bestLatAlt) / 2

        tPa.Available = True
        tPz.Available = True
        tP1.Available = True
        tP2.Available = True

        Return Latitude
    End Function

#End Region

#Region "Private and Protected Methods"
    Private Function SetBestLat(ByVal lowV As Double, ByVal highV As Double, ByRef tPa As Position, ByRef tPz As Position, ByRef tP1 As Position, ByRef tP2 As Position, ByVal correctAngSep As Double) As Double
        Dim bestAngSepDiff As Double = Double.MaxValue
        Const StepCount As Double = 10
        Dim lat As Double
        Dim bestLat As Double
        Dim stepSize As Double

        Do
            stepSize = (highV - lowV) / StepCount
            For lat = lowV To highV Step stepSize

                Dim ct As ConvertTrig = ConvertTrig.GetInstance
                ct.Site.Latitude.Rad = lat

                ct.Position.CopyFrom(tPa)
                ct.GetAltaz()
                tP1.CopyFrom(ct.Position)

                ct.Position.CopyFrom(tPz)
                ct.GetAltaz()
                tP2.CopyFrom(ct.Position)

                Dim thisAngSep As Double = IIterLatAngSep.Calc(tP1, tP2)
                Dim angSepDiff As Double = Math.Abs(correctAngSep - thisAngSep)

                If angSepDiff < bestAngSepDiff Then
                    bestAngSepDiff = angSepDiff
                    bestLat = lat
                End If
            Next

            lowV = bestLat - stepSize
            highV = bestLat + stepSize

        Loop While stepSize > Units.ArcsecToRad

        Return bestLat
    End Function
#End Region

End Class