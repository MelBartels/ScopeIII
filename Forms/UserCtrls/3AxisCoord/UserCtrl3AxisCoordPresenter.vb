#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
#End Region

Public Class UserCtrl3AxisCoordPresenter
    Inherits MVPUserCtrlPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrl3AxisCoord As UserCtrl3AxisCoord

    Private WithEvents pUserCtrlCoordPri As UserCtrlCoord
    Private WithEvents pUserCtrlCoordSec As UserCtrlCoord
    Private WithEvents pUserCtrlCoordTier As UserCtrlCoord

    Private pUserCtrlCoordPresenterPri As UserCtrlCoordPresenter
    Private pUserCtrlCoordPresenterSec As UserCtrlCoordPresenter
    Private pUserCtrlCoordPresenterTier As UserCtrlCoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrl3AxisCoordPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrl3AxisCoordPresenter = New UserCtrl3AxisCoordPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrl3AxisCoordPresenter
        Return New UserCtrl3AxisCoordPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordinatePri() As Coordinate
        Get
            Return pUserCtrlCoordPresenterPri.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pUserCtrlCoordPresenterPri.Coordinate = Value
        End Set
    End Property

    Public Property CoordinateSec() As Coordinate
        Get
            Return pUserCtrlCoordPresenterSec.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pUserCtrlCoordPresenterSec.Coordinate = Value
        End Set
    End Property

    Public Property CoordinateTier() As Coordinate
        Get
            Return pUserCtrlCoordPresenterTier.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pUserCtrlCoordPresenterTier.Coordinate = Value
        End Set
    End Property

    Public Property CoordinatePriObservableImp() As ObservableImp
        Get
            Return pUserCtrlCoordPresenterPri.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pUserCtrlCoordPresenterPri.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property CoordinateSecObservableImp() As ObservableImp
        Get
            Return pUserCtrlCoordPresenterSec.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pUserCtrlCoordPresenterSec.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property CoordinatetierObservableImp() As ObservableImp
        Get
            Return pUserCtrlCoordPresenterTier.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pUserCtrlCoordPresenterTier.CoordinateObservableImp = Value
        End Set
    End Property

    Public Sub SetAxisNames(ByVal priName As String, ByVal secName As String, ByVal tierName As String)
        pUserCtrlCoordPresenterPri.SetCoordinateName(priName)
        pUserCtrlCoordPresenterSec.SetCoordinateName(secName)
        pUserCtrlCoordPresenterTier.SetCoordinateName(tierName)
    End Sub

    Public Sub SetExpCoordTypes(ByVal priExpCoordType As ISFT, ByVal secExpCoordType As ISFT, ByVal tierExpCoordType As ISFT)
        pUserCtrlCoordPresenterPri.CoordExpType = priExpCoordType
        pUserCtrlCoordPresenterSec.CoordExpType = secExpCoordType
        pUserCtrlCoordPresenterTier.CoordExpType = tierExpCoordType
    End Sub

    Public Sub DisplayCoordinates(ByVal priRad As Double, ByVal secRad As Double, ByVal tierRad As Double)
        pUserCtrlCoordPresenterPri.DisplayCoordinate(priRad)
        pUserCtrlCoordPresenterSec.DisplayCoordinate(secRad)
        pUserCtrlCoordPresenterTier.DisplayCoordinate(tierRad)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrl3AxisCoord = CType(IMVPUserCtrl, UserCtrl3AxisCoord)

        pUserCtrlCoordPresenterPri = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterPri.IMVPUserCtrl = pUserCtrl3AxisCoord.UserCtrlCoordPri
        pUserCtrlCoordPresenterPri.SetCoordinateName(CoordName.PriAxis.Description)

        pUserCtrlCoordPresenterSec = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterSec.IMVPUserCtrl = pUserCtrl3AxisCoord.UserCtrlCoordSec
        pUserCtrlCoordPresenterSec.SetCoordinateName(CoordName.SecAxis.Description)

        pUserCtrlCoordPresenterTier = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterTier.IMVPUserCtrl = pUserCtrl3AxisCoord.UserCtrlCoordtier
        pUserCtrlCoordPresenterTier.SetCoordinateName(CoordName.TierAxis.Description)
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pUserCtrlCoordPresenterPri.DataModel = CType(CType(DataModel, Object())(0), Coordinate)
        pUserCtrlCoordPresenterSec.DataModel = CType(CType(DataModel, Object())(1), Coordinate)
        pUserCtrlCoordPresenterTier.DataModel = CType(CType(DataModel, Object())(2), Coordinate)
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
