<Serializable()> Public Class Coordinate

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Rad As Double
    Public CoordExpArray As ArrayList
    Public Name As String

    Public SinRad As Double
    Public CosRad As Double
#End Region

#Region "Private and Protected Members"
    Dim pHoldRad As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Coordinate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Coordinate = New Coordinate
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        CoordExpArray = New ArrayList
        pHoldRad = Double.MaxValue
    End Sub

    Public Shared Function GetInstance() As Coordinate
        Return New Coordinate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Function ToString(ByRef coordExpType As ISFT) As String
        Dim ICoordExp As ICoordExp
        For Each ICoordExp In CoordExpArray
            If ICoordExp.CoordExpType Is coordExpType Then
                Return ICoordExp.ToString(Rad)
            End If
        Next
        ICoordExp = CoordExpFactory.GetInstance.Build(coordExpType)
        If ICoordExp Is Nothing Then
            Return String.Empty
        Else
            CoordExpArray.Add(ICoordExp)
            Return ICoordExp.ToString(Rad)
        End If
    End Function

    Public Function CopyFrom(ByRef coordinate As Coordinate) As Boolean
        Rad = coordinate.Rad
        CoordExpArray = coordinate.CoordExpArray
    End Function

    Public Sub CheckHoldSinCos()
        If Not pHoldRad.Equals(Rad) Then
            CosRad = Math.Cos(Rad)
            SinRad = Math.Sin(Rad)
            pHoldRad = Rad
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class