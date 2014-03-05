Public MustInherit Class ControllerFactory

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pView As IView
    Protected pViewArgs() As Object
    Protected pModels() As Object
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ControllerFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ControllerFactory = New ControllerFactory
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As ControllerFactory
    '    Return New ControllerFactory
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function BuildAndInit(ByRef View As IView, ByRef ViewArgs() As Object) As Controller
        Dim Controller As Controller

        pView = View
        pViewArgs = ViewArgs
        pModels = GetModels()
        ' get controller from subclass
        Controller = GetController()
        ' run subclass controller's Init()
        Controller.Init(View, ViewArgs, pModels)

        Return Controller
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected MustOverride Function GetModels() As Object()
    Protected MustOverride Function GetController() As Controller
#End Region

End Class
