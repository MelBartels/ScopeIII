#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class DependencyResolver
    Implements IDependencyResolver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected ReadOnly pTypes As Dictionary(Of Type, Object)
    Protected ReadOnly pTypeResolverHelper As TypeResolverHelper
    Protected ReadOnly pInstanceResolverHelper As InstanceResolverHelper
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As DependencyResolver
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As DependencyResolver = New DependencyResolver
    '    End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pTypes = New Dictionary(Of Type, Object)
        pTypeResolverHelper = TypeResolverHelper.GetInstance
        pInstanceResolverHelper = InstanceResolverHelper.GetInstance
    End Sub

    Public Shared Function GetInstance() As DependencyResolver
        Return New DependencyResolver
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Resolve(Of T)() As T Implements IDependencyResolver.Resolve
        Dim [object] As Object = pTypes(GetType(T))
        If isType([object]) Then
            Return pTypeResolverHelper.InstantiateType(Of T)(CType([object], Type))
        Else
            Return CType([object], T)
        End If
    End Function

    Public Sub Register(Of T)(ByVal [object] As Object)
        If isType([object]) Then
            pTypeResolverHelper.RegisterType(Of T)(CType([object], Type), pTypes)
        Else
            pInstanceResolverHelper.RegisterObject(Of T)([object], pTypes)
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Function isType(ByVal [object] As Object) As Boolean
        Return TypeOf [object] Is System.Type
    End Function
#End Region

End Class
