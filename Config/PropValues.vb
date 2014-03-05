#Region "Imports"
#End Region

Public Class PropValues
    Inherits PropContainer

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pPropValues As Hashtable
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PropValues
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PropValues = New PropValues
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pPropValues = New Hashtable
    End Sub

    Public Shared Shadows Function GetInstance() As PropValues
        Return New PropValues
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Default Public Property Item(ByVal key As String) As Object
        Get
            Return pPropValues(key)
        End Get
        Set(ByVal Value As Object)
            pPropValues(key) = Value
        End Set
    End Property

    Public Property PropValues() As Hashtable
        Get
            Return pPropValues
        End Get
        Set(ByVal Value As Hashtable)
            pPropValues = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    ' called from PropContainer.PropParmDescriptor.GetValue
    Protected Overrides Sub OnGetValue(ByVal e As PropParmEventArgs)
        e.Value = pPropValues(e.Property.Name)
        MyBase.OnGetValue(e)
    End Sub

    Protected Overrides Sub OnSetValue(ByVal e As PropParmEventArgs)
        pPropValues(e.Property.Name) = e.Value
        MyBase.OnSetValue(e)
    End Sub
#End Region

End Class
