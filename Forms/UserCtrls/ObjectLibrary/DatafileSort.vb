Public Class DatafileSort
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
    Private pUpDown As Boolean
    Private pDatafileColumnName As ISFT
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DatafileSort
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforepColNamesinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DatafileSort = New DatafileSort
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pDatafileColumnName = DGVColumnNames.GetInstance.FirstItem
    End Sub

    Public Shared Function GetInstance() As DatafileSort
        Return New DatafileSort
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property UpDown() As Boolean
        Get
            Return pUpdown
        End Get
        Set(ByVal Value As Boolean)
            pUpdown = Value
        End Set
    End Property

    Public Property DatafileColumnName() As ISFT
        Get
            Return pDatafileColumnName
        End Get
        Set(ByVal Value As ISFT)
            pDatafileColumnName = Value
        End Set
    End Property

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim a As LWPosition
        Dim z As LWPosition

        If pUpDown Then
            z = CType(x, LWPosition)
            a = CType(y, LWPosition)
        Else
            a = CType(x, LWPosition)
            z = CType(y, LWPosition)
        End If

        If pDatafileColumnName Is DGVColumnNames.RADisplay Then
            Return a.RA.CompareTo(z.RA)
        ElseIf pDatafileColumnName Is DGVColumnNames.DecDisplay Then
            Return a.Dec.CompareTo(z.Dec)
        ElseIf pDatafileColumnName Is DGVColumnNames.Name Then
            Return a.Name.CompareTo(z.Name)
        ElseIf pDatafileColumnName Is DGVColumnNames.Source Then
            Return a.Source.CompareTo(z.Source)
        Else
            ExceptionService.Notify("Could not compare " & pDatafileColumnName.Name & " in DatafileSort.Compare().")
        End If
    End Function

#End Region

#Region "Private and Protected Methods"
#End Region
End Class
