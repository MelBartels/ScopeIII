Public Class CelestialErrorComparer
    Implements IComparer

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pCoordErrorTypeNameSequence As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CelestialErrorComparer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforepColNamesinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CelestialErrorComparer = New CelestialErrorComparer
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        buildCoordErrorTypeSequence()
    End Sub

    Public Shared Function GetInstance() As CelestialErrorComparer
        Return New CelestialErrorComparer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim xName As String = CType(x, DisplayPosition).Name
        Dim yName As String = CType(y, DisplayPosition).Name

        If xName.Equals(yName) Then
            Return 0
        End If

        ' see if x or y comes first...
        For Each sequenceName As String In pCoordErrorTypeNameSequence
            If sequenceName.Equals(xName) Then
                Return -1
            End If
            If sequenceName.Equals(yName) Then
                Return 1
            End If
        Next

        Return 0
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub buildCoordErrorTypeSequence()
        pCoordErrorTypeNameSequence = New ArrayList

        pCoordErrorTypeNameSequence.Add(CoordErrorType.Precession.Name)
        pCoordErrorTypeNameSequence.Add(CoordErrorType.Nutation.Name)
        pCoordErrorTypeNameSequence.Add(CoordErrorType.AnnualAberration.Name)
        pCoordErrorTypeNameSequence.Add(CoordErrorType.Refraction.Name)
    End Sub
#End Region
End Class
