#Region "Imports"
#End Region

Public MustInherit Class DevPropBase
    Implements IDevProp, ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pObservableImp As ObservableImp
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevPropBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevPropBase = New DevPropBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pObservableImp = ObservableImp.GetInstance
    End Sub

    'Public Shared Function GetInstance() As DevPropBase
    '    Return New DevPropBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ObservableImp() As ObservableImp Implements IDevProp.ObservableImp
        Get
            Return pObservableImp
        End Get
        Set(ByVal value As ObservableImp)
            pObservableImp = value
        End Set
    End Property

    Public MustOverride Property Value() As String Implements IDevProp.Value

    Public MustOverride Function Clone() As Object Implements IDevProp.Clone, ICloneable.Clone

#End Region

#Region "Private and Protected Methods"
#End Region

End Class
