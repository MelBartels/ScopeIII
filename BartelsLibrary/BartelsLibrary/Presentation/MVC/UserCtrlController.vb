Public MustInherit Class UserCtrlController

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
    Protected pUserControl As Windows.Forms.UserControl
    Protected pModels() As Object
    Protected pIViewArgs() As Object
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlController
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlController = New UserCtrlController
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As UserCtrlController
    '    Return New UserCtrlController
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Init(ByRef userControl As Windows.Forms.UserControl, ByRef ViewArgs() As Object, ByRef Models() As Object)
        ' set reference to UserControl
        pUserControl = userControl
        ' set reference Model(s)
        pModels = Models
        ' set reference to View arguments
        pIViewArgs = ViewArgs
        ' allow subclasses to set strongly typed reference to UserControl
        SetTypedUserControlReference()
        ' allow subclasses to set UserControl event handling references
        SetUserControlWithEventsReferences()
        ' allow subclasses to set strongly typed Model references
        SetTypedModelReferences()
        ' allow subclasses to set UserControl data binding
        SetUserControlDataBindings()
        ' finish up with any initializing needed by UserControl
        InitializeUserControl()
    End Sub

    Public Overridable Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

#End Region

#Region "Private and Protected Methods"
    Protected MustOverride Sub SetTypedUserControlReference()
    Protected MustOverride Sub SetUserControlWithEventsReferences()
    Protected MustOverride Sub SetTypedModelReferences()
    Protected MustOverride Sub SetUserControlDataBindings()
    Protected MustOverride Sub InitializeUserControl()
#End Region

End Class
