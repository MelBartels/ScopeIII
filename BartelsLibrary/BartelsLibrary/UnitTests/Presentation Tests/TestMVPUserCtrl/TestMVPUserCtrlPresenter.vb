#Region "Imports"
#End Region

Public Class TestMVPUserCtrlPresenter
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
    Private pMvpUserCtrlGraphics As MVPUserCtrlGraphics
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TestMVPUserCtrlPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestMVPUserCtrlPresenter = New TestMVPUserCtrlPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As TestMVPUserCtrlPresenter
        Return New TestMVPUserCtrlPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pMvpUserCtrlGraphics = CType(IMVPView, FrmTestMVPUserCtrl).TestMVPUserCtrlGraphics1
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
