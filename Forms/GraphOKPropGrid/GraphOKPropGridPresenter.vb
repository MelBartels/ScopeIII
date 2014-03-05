#Region "imports"
#End Region

Public Class GraphOKPropGridPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmGraphOKPropGrid As FrmGraphOKPropGrid
    Private pMvpUserCtrlGraphics As MVPUserCtrlGraphics
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As GraphOKPropGridPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As GraphOKPropGridPresenter = New GraphOKPropGridPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As GraphOKPropGridPresenter
        Return New GraphOKPropGridPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property FormTitle() As String
        Get
            Return pFrmGraphOKPropGrid.FormTitle
        End Get
        Set(ByVal Value As String)
            pFrmGraphOKPropGrid.FormTitle = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmGraphOKPropGrid = CType(IMVPView, FrmGraphOKPropGrid)
        pMvpUserCtrlGraphics = pFrmGraphOKPropGrid.MVPUserCtrlGraphics
        pMvpUserCtrlGraphics.BackColor = Drawing.Color.LightYellow
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pMvpUserCtrlGraphics.IRenderer = CType(DataModel, IRenderer)
        pFrmGraphOKPropGrid.PropGridSelectedObject = CType(DataModel, IRenderer).ObjectToRender
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
