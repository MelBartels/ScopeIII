<Serializable()> Public Class Position

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Available As Boolean
    Public Init As Boolean

    Public PosName As String
    Public ObjName As String
    Public Epoch As Double

    Public RA As Coordinate
    Public HA As Coordinate
    Public Dec As Coordinate
    Public Az As Coordinate
    Public Alt As Coordinate
    Public Axis3 As Coordinate
    Public SidT As Coordinate

    Public CoordErrorArray As CoordErrorArray
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Position
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Position = New Position
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        Available = True
        Init = False

        RA = Coordinate.GetInstance
        HA = Coordinate.GetInstance
        Dec = Coordinate.GetInstance
        Az = Coordinate.GetInstance
        Alt = Coordinate.GetInstance
        Axis3 = Coordinate.GetInstance
        SidT = Coordinate.GetInstance

        coordErrorArray = Coordinates.CoordErrorArray.GetInstance
    End Sub

    Public Shared Function GetInstance() As Position
        Return New Position
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub CopyFrom(ByRef position As Position)
        Available = position.Available

        PosName = position.PosName
        ObjName = position.ObjName
        Epoch = position.Epoch

        CopyCoordsFrom(position)
    End Sub

    Public Sub CopyCoordsFrom(ByRef position As Position)
        RA.CopyFrom(position.RA)
        HA.CopyFrom(position.HA)
        Dec.CopyFrom(position.Dec)
        Az.CopyFrom(position.Az)
        Alt.CopyFrom(position.Alt)
        Axis3.CopyFrom(position.Axis3)
        SidT.CopyFrom(position.SidT)
    End Sub

    Public Sub SetCoordDeg(ByVal raDeg As Double, ByVal decDeg As Double, ByVal AzDeg As Double, ByVal AltDeg As Double, ByVal SidTDeg As Double)
        Available = False
        RA.Rad = raDeg * Units.DegToRad
        Dec.Rad = decDeg * Units.DegToRad
        Az.Rad = AzDeg * Units.DegToRad
        Alt.Rad = AltDeg * Units.DegToRad
        SidT.Rad = SidTDeg * Units.DegToRad
    End Sub

    Public Function ShowCoordDeg() As String
        Dim coordExpType As ISFT = CType(Coordinates.CoordExpType.Degree, ISFT)
        Dim sb As New Text.StringBuilder
        sb.Append(PosName)
        sb.Append(" (deg) Ra: ")
        sb.Append(RA.ToString(coordExpType))
        sb.Append(", Dec: ")
        sb.Append(Dec.ToString(coordExpType))
        sb.Append(", Az: ")
        sb.Append(Az.ToString(coordExpType))
        sb.Append(", Alt: ")
        sb.Append(Alt.ToString(coordExpType))
        sb.Append(", SidT: ")
        sb.Append(SidT.ToString(coordExpType))
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class