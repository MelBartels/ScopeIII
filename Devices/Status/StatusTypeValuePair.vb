#Region "Imports"
#End Region

Public Class StatusTypeValuePair
    Implements ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pType As String
    Private pValue As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As StatusTypeValuePair
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As StatusTypeValuePair = New StatusTypeValuePair
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As StatusTypeValuePair
        Return New StatusTypeValuePair
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Type() As String
        Get
            Return pType
        End Get
        Set(ByVal Value As String)
            pType = Value
        End Set
    End Property

    Public Property Value() As String
        Get
            Return pValue
        End Get
        Set(ByVal Value As String)
            pValue = Value
        End Set
    End Property

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim StatusTypeValuePair As StatusTypeValuePair = ScopeIII.Devices.StatusTypeValuePair.GetInstance
        StatusTypeValuePair.Type = Type
        StatusTypeValuePair.Value = Value
        Return StatusTypeValuePair
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
