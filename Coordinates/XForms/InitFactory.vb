Imports System.Reflection

Public Class InitFactory

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Friend IInit As IInit
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As InitFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As InitFactory = New InitFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As InitFactory
        Return New InitFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Build(ByRef InitType As ISFT, ByRef ICoordXform As ICoordXform) As IInit
        Dim mType As Type = Type.GetType(CommonShared.IncludeNamespaceWithTypename(InitType.Name, Me))
        Dim [object] As Object = mType.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        IInit = CType([object], IInit)

        If IInit Is Nothing Then
            ExceptionService.Notify("Could not Build InitType: " & InitType.Name)
        Else
            IInit.ICoordXform = ICoordXform
            IInit.InitType = InitType
        End If

        Return IInit
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class