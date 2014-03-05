Imports System.Reflection

Public Class RatesFactory

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

    'Public Shared Function GetInstance() As RatesFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RatesFactory = New RatesFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As RatesFactory
        Return New RatesFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Build(ByRef rates As ISFT) As IRates
        Dim mType As Type = Type.GetType(CommonShared.IncludeNamespaceWithTypename(rates.Name, Me))
        Dim [object] As Object = mType.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        Dim IRates As IRates = CType([object], IRates)
        If IRates Is Nothing Then
            ExceptionService.Notify("Could not build IRates " & rates.Name)
        End If
        Return IRates
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
