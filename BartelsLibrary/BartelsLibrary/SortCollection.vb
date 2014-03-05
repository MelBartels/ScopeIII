#Region "Imports"
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.IO
#End Region

Public Class SortCollection

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

    'Public Shared Function GetInstance() As SortCollection
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SortCollection = New SortCollection
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As SortCollection
        Return New SortCollection
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Shared Sub Sort(ByRef collectionToSort As Collection, ByRef comparer As IComparer)
        Dim a As New ArrayList(collectionToSort)
        a.Sort(comparer)

        ' VS05 use .Clear
        For ix As Int32 = collectionToSort.Count To 1 Step -1
            collectionToSort.Remove(ix)
        Next

        For Each [object] As Object In a
            collectionToSort.Add([object])
        Next
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
