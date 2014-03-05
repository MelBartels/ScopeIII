#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class Gauge3AxisCoordOKPresenter
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
    Private WithEvents pFrmGauge3AxisCoordOK As FrmGauge3AxisCoordOK
    Private pIGauge3AxisCoordPresenter As IGauge3AxisCoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Gauge3AxisCoordOKPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Gauge3AxisCoordOKPresenter = New Gauge3AxisCoordOKPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Gauge3AxisCoordOKPresenter
        Return New Gauge3AxisCoordOKPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IGauge3AxisCoordPresenter() As IGauge3AxisCoordPresenter
        Get
            Return pIGauge3AxisCoordPresenter
        End Get
        Set(ByVal Value As IGauge3AxisCoordPresenter)
            pIGauge3AxisCoordPresenter = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmGauge3AxisCoordOK = CType(IMVPView, FrmGauge3AxisCoordOK)
        pIGauge3AxisCoordPresenter = UserCtrlGauge3AxisCoordPresenter.GetInstance
        CType(pIGauge3AxisCoordPresenter, UserCtrlGauge3AxisCoordPresenter).IMVPUserCtrl = pFrmGauge3AxisCoordOK.UserCtrlGauge3AxisCoord
        pIGauge3AxisCoordPresenter.SetAxisNames(CoordName.PriAxis.Description, CoordName.SecAxis.Description, CoordName.TierAxis.Description)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
