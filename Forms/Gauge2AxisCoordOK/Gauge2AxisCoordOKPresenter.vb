#Region "imports"
#End Region

Public Class Gauge2AxisCoordOKPresenter
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
    Private WithEvents pFrmGauge2AxisCoordOK As FrmGauge2AxisCoordOK
    Private pIGauge2AxisCoordPresenter As IGauge2AxisCoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Gauge2AxisCoordOKPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Gauge2AxisCoordOKPresenter = New Gauge2AxisCoordOKPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Gauge2AxisCoordOKPresenter
        Return New Gauge2AxisCoordOKPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IGauge2AxisCoordPresenter() As IGauge2AxisCoordPresenter
        Get
            Return pIGauge2AxisCoordPresenter
        End Get
        Set(ByVal Value As IGauge2AxisCoordPresenter)
            pIGauge2AxisCoordPresenter = Value
        End Set
    End Property

    Public Sub SetTitle(ByVal title As String)
        pFrmGauge2AxisCoordOK.Text = title
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmGauge2AxisCoordOK = CType(IMVPView, FrmGauge2AxisCoordOK)
        pIGauge2AxisCoordPresenter = UserCtrlGauge2AxisCoordPresenter.GetInstance
        CType(pIGauge2AxisCoordPresenter, UserCtrlGauge2AxisCoordPresenter).IMVPUserCtrl = pFrmGauge2AxisCoordOK.UserCtrlGauge2AxisCoord
        pIGauge2AxisCoordPresenter.SetAxisNames(CoordName.PriAxis.Description, CoordName.SecAxis.Description)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
