#Region "Imports"
Imports System.Collections.Generic
Imports System.Reflection

#End Region

Public Class TypeResolverHelper

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
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As TypeResolverHelper
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As TypeResolverHelper = New TypeResolverHelper
    '    End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As TypeResolverHelper
        Return New TypeResolverHelper
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function InstantiateType(Of T)(ByVal type As Type) As T
        Dim [object] As Object = Nothing
        Dim constructorInfo As ConstructorInfo = type.GetConstructor(BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public, Nothing, type.EmptyTypes, Nothing)
        If constructorInfo IsNot Nothing AndAlso constructorInfo.IsPublic Then
            [object] = constructorInfo.Invoke(Nothing)
        Else
            [object] = type.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        End If
        Return CType([object], T)
    End Function

    Public Sub RegisterType(Of T)(ByVal type As Type, ByRef pTypes As Dictionary(Of Type, Object))
        ' subclasses come w/ interfaces of their superclass, so only need to check interfaces once
        For Each IType As Type In type.GetInterfaces
            If IType Is GetType(T) Then
                pTypes.Add(GetType(T), type)
                Return
            End If
        Next

        ' if not found in interfaces, then check type then base types recursively
        If typeFound(Of T)(type, pTypes) Then
            Return
        End If

        Dim errMsg As String = "Type " & type.FullName & " does not implement " & GetType(T).FullName & " ."
        Throw New InvalidOperationException(errMsg)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Function typeFound(Of T)(ByVal type As Type, ByRef pTypes As Dictionary(Of Type, Object)) As Boolean
        If addType(Of T)(type, pTypes) Then
            Return True
        End If
        If type.BaseType IsNot Nothing Then
            Return typeFound(Of T)(type.BaseType, pTypes)
        End If
        Return False
    End Function

    Protected Function addType(Of T)(ByVal type As Type, ByRef pTypes As Dictionary(Of Type, Object)) As Boolean
        If type Is GetType(T) Then
            pTypes.Add(GetType(T), type)
            Return True
        End If
        Return False
    End Function
#End Region

End Class
