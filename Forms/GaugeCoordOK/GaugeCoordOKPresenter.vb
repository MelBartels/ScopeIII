#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class GaugeCoordOKPresenter
    Inherits MVPPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmGaugeCoordOK As FrmGaugeCoordOK
    Private pIRendererCoordPresenter As IRendererCoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As GaugeCoordOKPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As GaugeCoordOKPresenter = New GaugeCoordOKPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As GaugeCoordOKPresenter
        Return New GaugeCoordOKPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Title() As String
        Get
            Return pFrmGaugeCoordOK.Title
        End Get
        Set(ByVal Value As String)
            pFrmGaugeCoordOK.Title = Value
        End Set
    End Property

    Public Property Size() As Drawing.Size
        Get
            Return pFrmGaugeCoordOK.Size
        End Get
        Set(ByVal Value As Drawing.Size)
            pFrmGaugeCoordOK.Size = Value
        End Set
    End Property

    Public Property IRendererCoordPresenter() As IRendererCoordPresenter
        Get
            Return pIRendererCoordPresenter
        End Get
        Set(ByVal Value As IRendererCoordPresenter)
            pIRendererCoordPresenter = Value
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        ' renderer's ObjectToRender is the coordinate, which having been updated, sends a message to this observer
        pIRendererCoordPresenter.Render()
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmGaugeCoordOK = CType(IMVPView, FrmGaugeCoordOK)
        pIRendererCoordPresenter = UserCtrlGaugeCoordPresenter.GetInstance
        CType(pIRendererCoordPresenter, MVPUserCtrlPresenterBase).IMVPUserCtrl = pFrmGaugeCoordOK.UserCtrlGaugeCoord
        IRendererCoordPresenter.CoordinateObservableImp.Attach(Me)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
