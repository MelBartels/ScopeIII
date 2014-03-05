#Region "Imports"
Imports System.Reflection
#End Region

Public Class SFTFacadeFactory

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

    'Public Shared Function GetInstance() As SFTFacadeFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SFTFacadeFactory = New SFTFacadeFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As SFTFacadeFactory
        Return New SFTFacadeFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Build(ByVal typeName As String) As ISFTFacade
        Dim mType As Type = Type.GetType(CommonShared.IncludeNamespaceWithTypename(typeName, Me))
        Dim [object] As Object = mType.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        Dim ISFTFacade As ISFTFacade = CType([object], ISFTFacade)
        If ISFTFacade Is Nothing Then
            ExceptionService.Notify("Could not build ISFTFacade " & typeName)
        End If
        Return ISFTFacade
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
