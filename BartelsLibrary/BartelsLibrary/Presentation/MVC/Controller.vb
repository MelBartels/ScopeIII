Public MustInherit Class Controller

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    ' declare here in case MustOverride subs need them
    Protected pIView As IView
    Protected pIViewArgs() As Object
    Protected pModels() As Object
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Controller
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Controller = New Controller
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As Controller
    '    Return New Controller
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Init(ByRef View As IView, ByRef ViewArgs() As Object, ByRef Models() As Object)
        ' set reference to View
        pIView = View
        ' set reference Model(s)
        pModels = Models
        ' set reference to View arguments
        pIViewArgs = ViewArgs
        ' allow subclasses to set strongly typed reference to View
        SetTypedViewReference()
        ' allow subclasses to set View event handling references
        SetViewWithEventsReferences()
        ' set references to forms or components in View
        SetFormReferences()
        ' allow subclasses to set strongly typed Model references
        SetTypedModelReferences()
        ' allow subclasses to set View data binding
        SetViewDataBindings()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected MustOverride Sub SetTypedViewReference()
    Protected MustOverride Sub SetViewWithEventsReferences()
    Protected MustOverride Sub SetFormReferences()
    Protected MustOverride Sub SetTypedModelReferences()
    Protected MustOverride Sub SetViewDataBindings()
#End Region

End Class
