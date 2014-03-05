#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
#End Region

Public Class UserCtrlGauge3AxisCoordPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IGauge3AxisCoordPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlGauge3AxisCoord As UserCtrlGauge3AxisCoord

    Private WithEvents pUserCtrlGaugeCoordPri As UserCtrlGaugeCoord
    Private WithEvents pUserCtrlGaugeCoordSec As UserCtrlGaugeCoord
    Private WithEvents pUserCtrlGaugeCoordTier As UserCtrlGaugeCoord

    Private pUserCtrlGaugeCoordPresenterPri As UserCtrlGaugeCoordPresenter
    Private pUserCtrlGaugeCoordPresenterSec As UserCtrlGaugeCoordPresenter
    Private pUserCtrlGaugeCoordPresenterTier As UserCtrlGaugeCoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlGauge3AxisCoordPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlGauge3AxisCoordPresenter = New UserCtrlGauge3AxisCoordPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlGauge3AxisCoordPresenter
        Return New UserCtrlGauge3AxisCoordPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordinatePri() As Coordinate Implements IGauge3AxisCoordPresenter.CoordinatePri
        Get
            Return pUserCtrlGaugeCoordPresenterPri.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pUserCtrlGaugeCoordPresenterPri.Coordinate = Value
        End Set
    End Property

    Public Property CoordinateSec() As Coordinate Implements IGauge3AxisCoordPresenter.CoordinateSec
        Get
            Return pUserCtrlGaugeCoordPresenterSec.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pUserCtrlGaugeCoordPresenterSec.Coordinate = Value
        End Set
    End Property

    Public Property CoordinateTier() As Coordinates.Coordinate Implements IGauge3AxisCoordPresenter.CoordinateTier
        Get
            Return pUserCtrlGaugeCoordPresenterTier.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pUserCtrlGaugeCoordPresenterTier.Coordinate = Value
        End Set
    End Property

    Public Property CoordinatePriObservableImp() As ObservableImp Implements IGauge3AxisCoordPresenter.CoordinatePriObservableImp
        Get
            Return pUserCtrlGaugeCoordPresenterPri.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pUserCtrlGaugeCoordPresenterPri.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property CoordinateSecObservableImp() As ObservableImp Implements IGauge3AxisCoordPresenter.CoordinateSecObservableImp
        Get
            Return pUserCtrlGaugeCoordPresenterSec.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pUserCtrlGaugeCoordPresenterSec.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property CoordinateTierObservableImp() As ObservableImp Implements IGauge3AxisCoordPresenter.CoordinateTierObservableImp
        Get
            Return pUserCtrlGaugeCoordPresenterTier.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pUserCtrlGaugeCoordPresenterTier.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property PriCoordUpdatedByMe() As Boolean Implements IGauge3AxisCoordPresenter.PriCoordUpdatedByMe
        Get
            Return pUserCtrlGaugeCoordPresenterPri.CoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            pUserCtrlGaugeCoordPresenterPri.CoordUpdatedByMe = value
        End Set
    End Property

    Public Property SecCoordUpdatedByMe() As Boolean Implements IGauge3AxisCoordPresenter.SecCoordUpdatedByMe
        Get
            Return pUserCtrlGaugeCoordPresenterSec.CoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            pUserCtrlGaugeCoordPresenterSec.CoordUpdatedByMe = value
        End Set
    End Property

    Public Property TierCoordUpdatedByMe() As Boolean Implements IGauge3AxisCoordPresenter.TierCoordUpdatedByMe
        Get
            Return pUserCtrlGaugeCoordPresenterTier.CoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            pUserCtrlGaugeCoordPresenterTier.CoordUpdatedByMe = value
        End Set
    End Property

    Public Sub SetAxisNames(ByVal priName As String, ByVal secName As String, ByVal tierName As String) Implements IGauge3AxisCoordPresenter.SetAxisNames
        pUserCtrlGaugeCoordPresenterPri.SetCoordinateName(priName)
        pUserCtrlGaugeCoordPresenterSec.SetCoordinateName(secName)
        pUserCtrlGaugeCoordPresenterTier.SetCoordinateName(tierName)
    End Sub

    Public Sub SetExpCoordTypes(ByVal priExpCoordType As ISFT, ByVal secExpCoordType As ISFT, ByVal tierExpCoordType As ISFT) Implements IGauge3AxisCoordPresenter.SetExpCoordTypes
        pUserCtrlGaugeCoordPresenterPri.CoordExpType = priExpCoordType
        pUserCtrlGaugeCoordPresenterSec.CoordExpType = secExpCoordType
        pUserCtrlGaugeCoordPresenterTier.CoordExpType = tierExpCoordType
    End Sub

    Public Sub SetCoordinateLabelColors(ByVal priColor As System.Drawing.Color, ByVal secColor As System.Drawing.Color, ByVal tierColor As System.Drawing.Color) Implements IGauge3AxisCoordPresenter.SetCoordinateLabelColors
        pUserCtrlGaugeCoordPresenterPri.SetCoordinateLabelColor(priColor)
        pUserCtrlGaugeCoordPresenterSec.SetCoordinateLabelColor(secColor)
        pUserCtrlGaugeCoordPresenterTier.SetCoordinateLabelColor(tierColor)
    End Sub

    Public Sub SetRenderers(ByVal priRenderer As BartelsLibrary.IRenderer, ByVal secRenderer As BartelsLibrary.IRenderer, ByVal tierRenderer As BartelsLibrary.IRenderer) Implements IGauge3AxisCoordPresenter.SetRenderers
        pUserCtrlGaugeCoordPresenterPri.IRenderer = priRenderer
        pUserCtrlGaugeCoordPresenterSec.IRenderer = secRenderer
        pUserCtrlGaugeCoordPresenterTier.IRenderer = tierRenderer
    End Sub

    Public Sub DisplayCoordinates(ByVal priRad As Double, ByVal secRad As Double, ByVal tierRad As Double) Implements IGauge3AxisCoordPresenter.DisplayCoordinates
        pUserCtrlGaugeCoordPresenterPri.DisplayCoordinate(priRad)
        pUserCtrlGaugeCoordPresenterSec.DisplayCoordinate(secRad)
        pUserCtrlGaugeCoordPresenterTier.DisplayCoordinate(tierRad)
    End Sub

    Public Sub Render() Implements IGauge3AxisCoordPresenter.Render
        pUserCtrlGauge3AxisCoord.Refresh()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlGauge3AxisCoord = CType(IMVPUserCtrl, UserCtrlGauge3AxisCoord)

        pUserCtrlGaugeCoordPresenterPri = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenterPri.IMVPUserCtrl = pUserCtrlGauge3AxisCoord.UserCtrlGaugeCoordPri
        pUserCtrlGaugeCoordPresenterPri.SetCoordinateName("A Axis")

        pUserCtrlGaugeCoordPresenterSec = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenterSec.IMVPUserCtrl = pUserCtrlGauge3AxisCoord.UserCtrlGaugeCoordSec
        pUserCtrlGaugeCoordPresenterSec.SetCoordinateName("Z Axis")

        pUserCtrlGaugeCoordPresenterTier = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenterTier.IMVPUserCtrl = pUserCtrlGauge3AxisCoord.UserCtrlGaugeCoordTier
        pUserCtrlGaugeCoordPresenterTier.SetCoordinateName("X Axis")
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pUserCtrlGaugeCoordPresenterPri.DataModel = CType(CType(DataModel, Object())(0), Coordinate)
        pUserCtrlGaugeCoordPresenterSec.DataModel = CType(CType(DataModel, Object())(1), Coordinate)
        pUserCtrlGaugeCoordPresenterTier.DataModel = CType(CType(DataModel, Object())(2), Coordinate)
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
