#Region "Imports"
#End Region

Public Class UserCtrlGauge2AxisCoordPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IGauge2AxisCoordPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlGauge2AxisCoord As UserCtrlGauge2AxisCoord

    Private WithEvents pUserCtrlGaugeCoordPri As UserCtrlGaugeCoord
    Private WithEvents pUserCtrlGaugeCoordSec As UserCtrlGaugeCoord

    Private pUserCtrlGaugeCoordPresenterPri As UserCtrlGaugeCoordPresenter
    Private pUserCtrlGaugeCoordPresenterSec As UserCtrlGaugeCoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlGauge2AxisCoordPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlGauge2AxisCoordPresenter = New UserCtrlGauge2AxisCoordPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlGauge2AxisCoordPresenter
        Return New UserCtrlGauge2AxisCoordPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordinatePri() As Coordinate Implements IGauge2AxisCoordPresenter.CoordinatePri
        Get
            Return pUserCtrlGaugeCoordPresenterPri.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pUserCtrlGaugeCoordPresenterPri.Coordinate = Value
        End Set
    End Property

    Public Property CoordinateSec() As Coordinate Implements IGauge2AxisCoordPresenter.CoordinateSec
        Get
            Return pUserCtrlGaugeCoordPresenterSec.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pUserCtrlGaugeCoordPresenterSec.Coordinate = Value
        End Set
    End Property

    Public Property CoordinatePriObservableImp() As ObservableImp Implements IGauge2AxisCoordPresenter.CoordinatePriObservableImp
        Get
            Return pUserCtrlGaugeCoordPresenterPri.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pUserCtrlGaugeCoordPresenterPri.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property CoordinateSecObservableImp() As ObservableImp Implements IGauge2AxisCoordPresenter.CoordinateSecObservableImp
        Get
            Return pUserCtrlGaugeCoordPresenterSec.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pUserCtrlGaugeCoordPresenterSec.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property PriCoordUpdatedByMe() As Boolean Implements IGauge2AxisCoordPresenter.PriCoordUpdatedByMe
        Get
            Return pUserCtrlGaugeCoordPresenterPri.CoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            pUserCtrlGaugeCoordPresenterPri.CoordUpdatedByMe = value
        End Set
    End Property

    Public Property SecCoordUpdatedByMe() As Boolean Implements IGauge2AxisCoordPresenter.SecCoordUpdatedByMe
        Get
            Return pUserCtrlGaugeCoordPresenterSec.CoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            pUserCtrlGaugeCoordPresenterSec.CoordUpdatedByMe = value
        End Set
    End Property

    Public Sub SetAxisNames(ByVal priName As String, ByVal secName As String) Implements IGauge2AxisCoordPresenter.SetAxisNames
        pUserCtrlGaugeCoordPresenterPri.SetCoordinateName(priName)
        pUserCtrlGaugeCoordPresenterSec.SetCoordinateName(secName)
    End Sub

    Public Sub SetExpCoordTypes(ByVal priExpCoordType As ISFT, ByVal secExpCoordType As ISFT) Implements IGauge2AxisCoordPresenter.SetExpCoordTypes
        pUserCtrlGaugeCoordPresenterPri.CoordExpType = priExpCoordType
        pUserCtrlGaugeCoordPresenterSec.CoordExpType = secExpCoordType
    End Sub

    Public Sub SetCoordinateLabelColors(ByVal priColor As System.Drawing.Color, ByVal secColor As System.Drawing.Color) Implements IGauge2AxisCoordPresenter.SetCoordinateLabelColors
        pUserCtrlGaugeCoordPresenterPri.SetCoordinateLabelColor(priColor)
        pUserCtrlGaugeCoordPresenterSec.SetCoordinateLabelColor(secColor)
    End Sub

    Public Sub SetRenderers(ByVal priRenderer As BartelsLibrary.IRenderer, ByVal secRenderer As BartelsLibrary.IRenderer) Implements IGauge2AxisCoordPresenter.SetRenderers
        pUserCtrlGaugeCoordPresenterPri.IRenderer = priRenderer
        pUserCtrlGaugeCoordPresenterSec.IRenderer = secRenderer
    End Sub

    Public Sub DisplayCoordinates(ByVal priRad As Double, ByVal secRad As Double) Implements IGauge2AxisCoordPresenter.DisplayCoordinates
        pUserCtrlGaugeCoordPresenterPri.DisplayCoordinate(priRad)
        pUserCtrlGaugeCoordPresenterSec.DisplayCoordinate(secRad)
    End Sub

    Public Sub Render() Implements IGauge2AxisCoordPresenter.Render
        pUserCtrlGauge2AxisCoord.Refresh()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlGauge2AxisCoord = CType(IMVPUserCtrl, UserCtrlGauge2AxisCoord)

        pUserCtrlGaugeCoordPresenterPri = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenterPri.IMVPUserCtrl = pUserCtrlGauge2AxisCoord.UserCtrlGaugeCoordPri
        pUserCtrlGaugeCoordPresenterPri.SetCoordinateName("A Axis")

        pUserCtrlGaugeCoordPresenterSec = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenterSec.IMVPUserCtrl = pUserCtrlGauge2AxisCoord.UserCtrlGaugeCoordSec
        pUserCtrlGaugeCoordPresenterSec.SetCoordinateName("Z Axis")
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pUserCtrlGaugeCoordPresenterPri.DataModel = CType(CType(DataModel, Object())(0), Coordinate)
        pUserCtrlGaugeCoordPresenterSec.DataModel = CType(CType(DataModel, Object())(1), Coordinate)
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
