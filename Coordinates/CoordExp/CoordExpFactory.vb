Imports System.Reflection

Public Class CoordExpFactory

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

    'Public Shared Function GetInstance() As CoordExpFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordExpFactory = New CoordExpFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CoordExpFactory
        Return New CoordExpFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' returns pointer to ICoordExp object if successful, otherwise returns null
    Public Function Build(ByVal coordExpType As ISFT) As ICoordExp
        Dim mType As Type = Type.GetType(CommonShared.IncludeNamespaceWithTypename(coordExpType.Name, Me))
        Dim [object] As Object = mType.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        Dim ICoordExp As ICoordExp = CType([object], ICoordExp)
        If ICoordExp Is Nothing Then
            ExceptionService.Notify("Could not build ICoordExp " & coordExpType.Name)
        Else
            ICoordExp.CoordExpType = coordExpType
        End If
        Return ICoordExp
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class