#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class InstanceResolverHelper

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

    '    Public Shared Function GetInstance() As InstanceResolverHelper
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As InstanceResolverHelper = New InstanceResolverHelper
    '    End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As InstanceResolverHelper
        Return New InstanceResolverHelper
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub RegisterObject(Of T)(ByVal [object] As Object, ByRef pTypes As Dictionary(Of Type, Object))
        ' not: [object].GetType Is GetType(T)
        ' in C#, equivalent is: if (@object is T) {..}
        If TypeOf [object] Is T Then
            pTypes.Add(GetType(T), [object])
        Else
            Dim errMsg As String = "Object " & [object].GetType.FullName & " does not implement " & GetType(T).FullName & " ."
            Throw New InvalidOperationException(errMsg)
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
