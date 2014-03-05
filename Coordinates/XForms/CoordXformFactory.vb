Imports System.Reflection

Public Class CoordXformFactory

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

    'Public Shared Function GetInstance() As CoordXformFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordXformFactory = New CoordXformFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CoordXformFactory
        Return New CoordXformFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' returns pointer to ICoordXform object if successful, otherwise returns null
    Public Function Build(ByVal CoordXformType As ISFT) As ICoordXform
        Dim mType As Type = Type.GetType(CommonShared.IncludeNamespaceWithTypename(CoordXformType.Name, Me))
        Dim [object] As Object = mType.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        Dim ICoordXform As ICoordXform = CType([object], ICoordXform)
        If ICoordXform Is Nothing Then
            ExceptionService.Notify("Could not build ICoordXform " & CoordXformType.Name)
        Else
            ICoordXform.CoordXformType = CoordXformType
        End If
        Return ICoordXform
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class