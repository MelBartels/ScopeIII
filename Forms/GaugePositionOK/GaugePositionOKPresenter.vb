#Region "imports"
#End Region

Public Class GaugePositionOKPresenter
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
    Private WithEvents pFrmGaugePositionOK As FrmGaugePositionOK
    Private pUserCtrlGaugePositionPresenter As UserCtrlGaugePositionPresenter
    Private pPosition As Position
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As GaugePositionOKPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As GaugePositionOKPresenter = New GaugePositionOKPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As GaugePositionOKPresenter
        Return New GaugePositionOKPresenter
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

    Public Property IGauge2AxisCoordPresenterEquat() As IGauge2AxisCoordPresenter
        Get
            Return pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat
        End Get
        Set(ByVal Value As IGauge2AxisCoordPresenter)
            pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterEquat = Value
        End Set
    End Property

    Public Property IGauge2AxisCoordPresenterAltaz() As IGauge2AxisCoordPresenter
        Get
            Return pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz
        End Get
        Set(ByVal Value As IGauge2AxisCoordPresenter)
            pUserCtrlGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz = Value
        End Set
    End Property

    Public Property IRendererCoordPresenterSidT() As IRendererCoordPresenter
        Get
            Return pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT
        End Get
        Set(ByVal Value As IRendererCoordPresenter)
            pUserCtrlGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmGaugePositionOK = CType(IMVPView, FrmGaugePositionOK)
        AddHandler pFrmGaugePositionOK.OK, AddressOf ok

        pUserCtrlGaugePositionPresenter = UserCtrlGaugePositionPresenter.GetInstance
        pUserCtrlGaugePositionPresenter.IMVPUserCtrl = pFrmGaugePositionOK.UserCtrlGaugePosition
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub ok()
        Position = pUserCtrlGaugePositionPresenter.Position
        pFrmGaugePositionOK.Close()
    End Sub
#End Region

End Class
