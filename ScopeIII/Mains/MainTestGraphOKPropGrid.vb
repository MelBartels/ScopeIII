Public Class MainTestGraphOKPropGrid
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

    'Public Shared Function GetInstance() As MainTestGraphOKPropGrid
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainTestGraphOKPropGrid = New MainTestGraphOKPropGrid
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainTestGraphOKPropGrid
        Return New MainTestGraphOKPropGrid
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim graphOKPropGridPresenter As GraphOKPropGridPresenter = Forms.GraphOKPropGridPresenter.GetInstance
        graphOKPropGridPresenter.IMVPView = New FrmGraphOKPropGrid
        Dim XYDataRenderer As XYDataRenderer = Forms.XYDataRenderer.GetInstance
        Dim XYData As XYData = Forms.XYData.GetInstance
        XYDataRenderer.ObjectToRender = XYData
        graphOKPropGridPresenter.DataModel = XYDataRenderer

        graphOKPropGridPresenter.FormTitle = Application.ProductName & " Test Graph (linear) with PropertyGrid"
        XYData.BackgroundColor = Color.LightYellow
        XYData.GridColor = Color.LightGray
        XYData.XLogBase = 0
        XYData.XRangeStart = -100
        XYData.XRangeEnd = 1000
        XYData.XGridSpacing = 100
        XYData.YLogBase = 0
        XYData.YRangeStart = -10
        XYData.YRangeEnd = 10
        XYData.YGridSpacing = 2
        XYData.XData = New Double() {-10, 10, 200, 250, 700}
        XYData.YData = New Double() {5, -2, -3, 1, 7}

        graphOKPropGridPresenter.ShowDialog()

        graphOKPropGridPresenter.FormTitle = Application.ProductName & " Test Graph (log) with PropertyGrid"
        XYData.BackgroundColor = Color.White
        XYData.GridColor = Color.FromArgb(255, 200, 200)
        XYData.XLogBase = 10
        XYData.XRangeStart = 1
        XYData.XRangeEnd = 100000
        XYData.YLogBase = 10
        XYData.YRangeStart = 1
        XYData.YRangeEnd = 100000
        XYData.XData = New Double() {1.1, 10, 100, 1000, 10000}
        XYData.YData = New Double() {1.1, 10, 100, 1000, 10000}

        graphOKPropGridPresenter.ShowDialog()
    End Sub
#End Region

End Class
