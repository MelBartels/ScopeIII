#Region "Imports"
#End Region

Public Class PropParmEventArgs
    Inherits EventArgs

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pProperty As PropParm
    Private pVal As Object
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PropParmEventArgs
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PropParmEventArgs = New PropParmEventArgs
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As PropParmEventArgs
        Return New PropParmEventArgs
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public ReadOnly Property [Property]() As PropParm
        Get
            Return pProperty
        End Get
    End Property

    Public Property Value() As Object
        Get
            Return pVal
        End Get
        Set(ByVal Value As Object)
            pVal = Value
        End Set
    End Property

    Public Sub Init(ByVal [property] As PropParm, ByVal val As Object)
        pProperty = [property]
        pVal = val
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
