Public Class MessageBoxCoord

#Region "Inner Classes"
#End Region

#Region "Constant Members"
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

    'Public Shared Function GetInstance() As MessageBoxCoord
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MessageBoxCoord = New MessageBoxCoord
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MessageBoxCoord
        Return New MessageBoxCoord
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Show(ByVal msg As String, ByVal title As String, ByVal messageBoxIcon As MessageBoxIcon) As DialogResult
        Return MessageBox.Show(msg, title, MessageBoxButtons.OK, messageBoxIcon)
    End Function

    Public Function Show(ByVal msg As String, ByVal title As String) As DialogResult
        Return Show(msg, title, MessageBoxIcon.Information)
    End Function

    Public Function Show(ByRef coordinate As Coordinate) As DialogResult
        If coordinate Is Nothing Then
            Return Show("No Coordinate to Display", "Coordinate Value", MessageBoxIcon.Warning)
        Else
            Return Show(coordinate.ToString(CType(CoordExpType.DMS, ISFT)), "Coordinate Value")
        End If
    End Function

    Public Function Show(ByRef coordinate1 As Coordinate, ByRef coordinate2 As Coordinate) As DialogResult
        If coordinate1 Is Nothing OrElse coordinate2 Is Nothing Then
            Return Show("No Coordinates to Display", "Coordinate Value", MessageBoxIcon.Warning)
        Else
            Dim sb As New Text.StringBuilder
            sb.Append(coordinate1.ToString(CType(CoordExpType.DMS, ISFT)))
            sb.Append(", ")
            sb.Append(coordinate2.ToString(CType(CoordExpType.DMS, ISFT)))
            Return Show(sb.ToString, "Coordinate Values")
        End If
    End Function

    Public Function Show(ByRef coordinate1 As Coordinate, ByRef coordinate2 As Coordinate, ByRef coordinate3 As Coordinate) As DialogResult
        If coordinate1 Is Nothing OrElse coordinate2 Is Nothing Then
            Return Show("No Coordinates to Display", "Coordinate Value", MessageBoxIcon.Warning)
        Else
            Dim sb As New Text.StringBuilder
            sb.Append(coordinate1.ToString(CType(CoordExpType.DMS, ISFT)))
            sb.Append(", ")
            sb.Append(coordinate2.ToString(CType(CoordExpType.DMS, ISFT)))
            sb.Append(", ")
            sb.Append(coordinate3.ToString(CType(CoordExpType.DMS, ISFT)))
            Return Show(sb.ToString, "Coordinate Values")
        End If
    End Function

    Public Function Show(ByVal name As String, ByRef coordinate1 As Coordinate, ByRef coordinate2 As Coordinate) As DialogResult
        If coordinate1 Is Nothing OrElse coordinate2 Is Nothing Then
            Return Show("No Coordinates to Display", "Coordinate Value", MessageBoxIcon.Warning)
        Else
            Dim sb As New Text.StringBuilder
            sb.Append(coordinate1.ToString(CType(CoordExpType.DMS, ISFT)))
            sb.Append(", ")
            sb.Append(coordinate2.ToString(CType(CoordExpType.DMS, ISFT)))
            Return Show(sb.ToString, name)
        End If
    End Function

    Public Function Show(ByVal name As String, ByRef coordinate1 As Coordinate, ByRef coordinate2 As Coordinate, ByRef coordinate3 As Coordinate) As DialogResult
        If coordinate1 Is Nothing OrElse coordinate2 Is Nothing Then
            Return Show("No Coordinates to Display", "Coordinate Value", MessageBoxIcon.Warning)
        Else
            Dim sb As New Text.StringBuilder
            sb.Append(coordinate1.ToString(CType(CoordExpType.DMS, ISFT)))
            sb.Append(", ")
            sb.Append(coordinate2.ToString(CType(CoordExpType.DMS, ISFT)))
            sb.Append(", ")
            sb.Append(coordinate3.ToString(CType(CoordExpType.DMS, ISFT)))
            Return Show(sb.ToString, name)
        End If
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
