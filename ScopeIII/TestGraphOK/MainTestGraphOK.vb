Public Class MainTestGraphOK
    Inherits MainPrototype

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

    'Public Shared Function GetInstance() As MainTestGraphOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainTestGraphOK = New MainTestGraphOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainTestGraphOK
        Return New MainTestGraphOK
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim graphFunctionParams As GraphFunctionParams = Forms.GraphFunctionParams.GetInstance
        graphFunctionParams.XSteps = 100
        graphFunctionParams.IFunction = TestFunction.GetInstance

        Dim graphOKPresenter As GraphOKPresenter = Forms.GraphOKPresenter.GetInstance
        graphOKPresenter.IMVPView = New FrmGraphOK
        graphOKPresenter.DataModel = FunctionRenderer.GetInstance
        CType(graphOKPresenter.DataModel, IRenderer).ObjectToRender = graphFunctionParams
        graphOKPresenter.FormTitle = Application.ProductName & " Test Graph of " & graphFunctionParams.IFunction.ToString
        graphOKPresenter.ShowDialog()
    End Sub
#End Region

End Class
