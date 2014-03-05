#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class SitePresenter
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
    Private WithEvents pFrmEnterSite As FrmEnterSite

    Private WithEvents pUserCtrlSite As UserCtrlSite
    Private pUserCtrlSitePresenter As UserCtrlSitePresenter

    Private pSite As Coordinates.Site
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SitePresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SitePresenter = New SitePresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As SitePresenter
        Return New SitePresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public ReadOnly Property SitePresenter() As UserCtrlSitePresenter
        Get
            buildUserCtrlSitePresenter()
            Return pUserCtrlSitePresenter
        End Get
    End Property

    Public Property Site() As Coordinates.Site
        Get
            Return pSite
        End Get
        Set(ByVal Value As Coordinates.Site)
            pSite = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEnterSite = CType(IMVPView, FrmEnterSite)
        AddHandler pFrmEnterSite.Ok, AddressOf ok

        buildUserCtrlSitePresenter()
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub ok()
        Site = pUserCtrlSitePresenter.Site
    End Sub

    Private Sub buildUserCtrlSitePresenter()
        If pUserCtrlSitePresenter Is Nothing Then
            pUserCtrlSitePresenter = UserCtrlSitePresenter.GetInstance
            pUserCtrlSitePresenter.IMVPUserCtrl = pFrmEnterSite.UserCtrlSite
        End If
    End Sub
#End Region

End Class
