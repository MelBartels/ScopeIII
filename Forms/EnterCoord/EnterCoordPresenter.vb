#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class EnterCoordPresenter
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
    Private WithEvents pFrmEnterCoord As FrmEnterCoord
    Private pICoordPresenter As ICoordPresenter
    Private pCoordinate As Coordinate
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EnterCoordPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EnterCoordPresenter = New EnterCoordPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As EnterCoordPresenter
        Return New EnterCoordPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ICoordPresenter() As ICoordPresenter
        Get
            Return pICoordPresenter
        End Get
        Set(ByVal Value As ICoordPresenter)
            pICoordPresenter = Value
        End Set
    End Property

    Public Property Coordinate() As Coordinate
        Get
            Return (pCoordinate)
        End Get
        Set(ByVal Value As Coordinate)
            pCoordinate = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEnterCoord = CType(IMVPView, FrmEnterCoord)
        AddHandler pFrmEnterCoord.OK, AddressOf OK

        pICoordPresenter = UserCtrlCoordPresenter.GetInstance
        CType(pICoordPresenter, MVPUserCtrlPresenterBase).IMVPUserCtrl = pFrmEnterCoord.UserCtrlCoord
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub OK()
        Coordinate = pICoordPresenter.Coordinate
        pFrmEnterCoord.DialogResult.equals(DialogResult.OK)
        pFrmEnterCoord.Close()
    End Sub
#End Region

End Class
