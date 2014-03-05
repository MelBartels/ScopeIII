#Region "imports"
#End Region

Public Class PositionPresenter
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
    Private WithEvents pFrmEnterPosition As FrmEnterPosition

    Private pUserCtrlPositionPresenter As UserCtrlPositionPresenter

    Private pPosition As Position

#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PositionPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PositionPresenter = New PositionPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As PositionPresenter
        Return New PositionPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Position() As Position
        Get
            Return pPosition
        End Get
        Set(ByVal Value As Position)
            pPosition = Value
        End Set
    End Property

#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEnterPosition = CType(IMVPView, FrmEnterPosition)
        AddHandler pFrmEnterPosition.OK, AddressOf ok

        pUserCtrlPositionPresenter = UserCtrlPositionPresenter.GetInstance
        pUserCtrlPositionPresenter.IMVPUserCtrl = pFrmEnterPosition.UserCtrlPosition
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub ok()
        Position = pUserCtrlPositionPresenter.Position
        pFrmEnterPosition.Close()
    End Sub
#End Region

End Class
