#Region "Imports"
#End Region

Public Class GraphOKPresenter
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
    Private pFrmGraphOK As frmGraphOK
    Private pMvpUserCtrlGraphics As MVPUserCtrlGraphics
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As GraphOKPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As GraphOKPresenter = New GraphOKPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As GraphOKPresenter
        Return New GraphOKPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property FormTitle() As String
        Get
            Return pFrmGraphOK.FormTitle
        End Get
        Set(ByVal Value As String)
            pFrmGraphOK.FormTitle = Value
        End Set
    End Property

#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmGraphOK = CType(IMVPView, FrmGraphOK)
        pMvpUserCtrlGraphics = pFrmGraphOK.MVPUserCtrlGraphics
        pMvpUserCtrlGraphics.BackColor = Drawing.Color.LightYellow
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pMvpUserCtrlGraphics.IRenderer = CType(DataModel, IRenderer)
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
