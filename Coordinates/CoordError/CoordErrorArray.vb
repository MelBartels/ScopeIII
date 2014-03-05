<Serializable()> Public Class CoordErrorArray

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pErrArray As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CoordErrorArray
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordErrorArray = New CoordErrorArray
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pErrArray = New ArrayList
    End Sub

    Public Shared Function GetInstance() As CoordErrorArray
        Return New CoordErrorArray
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Function ErrorArray() As ArrayList
        Return pErrArray
    End Function

    Public Overloads Sub ErrorArray(ByRef ErrorArray As ArrayList)
        pErrArray = ErrorArray
    End Sub

    Public Function CoordError(ByRef coordName As ISFT, ByRef coordErrorType As ISFT) As Coordinate
        Return GetCoordError(coordName, coordErrorType).Coordinate
    End Function

    Public Sub CoordError(ByRef coordName As ISFT, ByRef coordErrorType As ISFT, ByVal rad As Double)
        GetCoordError(coordName, coordErrorType).Coordinate.Rad = rad
    End Sub

    Public Sub CopyFrom(ByRef coordErrorArray As CoordErrorArray)
        ErrorArray.Clear()
        For Each coordError As CoordError In coordErrorArray.ErrorArray
            ErrorArray.Add(coordError)
        Next
    End Sub

    Public Function SumRad(ByRef coordName As ISFT) As Double
        If pErrArray.Count.Equals(0) Then
            Return 0
        End If

        Dim runningTotal As Double = 0
        For Each coordError As CoordError In pErrArray
            If coordError.CoordName Is coordName Then
                runningTotal += coordError.Coordinate.Rad
            End If
        Next

        Return runningTotal
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        For Each coordError As CoordError In pErrArray
            sb.Append(coordError.CoordName.Name)
            sb.Append(" ")
            sb.Append(coordError.CoordErrorType.Name)
            sb.Append(" ")
            sb.Append(coordError.Coordinate.Rad * Units.RadToArcmin)
            sb.Append("', ")
        Next
        If pErrArray.Count > 0 Then
            sb.Remove(sb.Length - 2, 2)
        End If
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function GetCoordError(ByRef coordName As ISFT, ByRef coordErrorType As ISFT) As CoordError
        Dim coordError As coordError
        For Each coordError In pErrArray
            If coordName Is coordError.CoordName AndAlso coordErrorType Is coordError.CoordErrorType Then
                Return coordError
            End If
        Next
        coordError = Coordinates.CoordError.GetInstance
        coordError.CoordName = coordName
        coordError.CoordErrorType = coordErrorType
        pErrArray.Add(coordError)
        Return coordError
    End Function
#End Region

End Class
