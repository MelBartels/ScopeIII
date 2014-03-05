#Region "Imports"
#End Region

Public Class UserCtrlGaugePositionPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IGaugePositionPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlGaugePosition As UserCtrlGaugePosition

    Private WithEvents pUserCtrlGauge2AxisCoordEquat As UserCtrlGauge2AxisCoord
    Private WithEvents pUserCtrlGauge2AxisCoordAltaz As UserCtrlGauge2AxisCoord
    Private WithEvents pUserCtrlGaugeCoordSidT As UserCtrlGaugeCoord

    Private pUserCtrlGauge2AxisCoordPresenterEquat As UserCtrlGauge2AxisCoordPresenter
    Private pUserCtrlGauge2AxisCoordPresenterAltaz As UserCtrlGauge2AxisCoordPresenter
    Private pUserCtrlGaugeCoordPresenterSidT As UserCtrlGaugeCoordPresenter

    Private pPosition As Position
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlGaugePositionPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlGaugePositionPresenter = New UserCtrlGaugePositionPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlGaugePositionPresenter
        Return New UserCtrlGaugePositionPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub DisplayPosition(ByVal position As Coordinates.Position) Implements IGaugePositionPresenter.DisplayPosition
        pUserCtrlGauge2AxisCoordPresenterEquat.DisplayCoordinates(position.RA.Rad, position.Dec.Rad)
        pUserCtrlGauge2AxisCoordPresenterAltaz.DisplayCoordinates(position.Az.Rad, position.Alt.Rad)
        pUserCtrlGaugeCoordPresenterSidT.DisplayCoordinate(position.SidT.Rad)
    End Sub

    Public Property IGauge2AxisCoordPresenterAltaz() As IGauge2AxisCoordPresenter Implements IGaugePositionPresenter.IGauge2AxisCoordPresenterAltaz
        Get
            Return pUserCtrlGauge2AxisCoordPresenterAltaz
        End Get
        Set(ByVal Value As IGauge2AxisCoordPresenter)
            pUserCtrlGauge2AxisCoordPresenterAltaz = CType(Value, UserCtrlGauge2AxisCoordPresenter)
        End Set
    End Property

    Public Property IGauge2AxisCoordPresenterEquat() As IGauge2AxisCoordPresenter Implements IGaugePositionPresenter.IGauge2AxisCoordPresenterEquat
        Get
            Return pUserCtrlGauge2AxisCoordPresenterEquat
        End Get
        Set(ByVal Value As IGauge2AxisCoordPresenter)
            pUserCtrlGauge2AxisCoordPresenterEquat = CType(Value, UserCtrlGauge2AxisCoordPresenter)
        End Set
    End Property

    Public ReadOnly Property Position() As Coordinates.Position Implements IGaugePositionPresenter.Position
        Get
            pPosition.RA.Rad = pUserCtrlGauge2AxisCoordPresenterEquat.CoordinatePri.Rad
            pPosition.Dec.Rad = pUserCtrlGauge2AxisCoordPresenterEquat.CoordinateSec.Rad
            pPosition.Az.Rad = pUserCtrlGauge2AxisCoordPresenterAltaz.CoordinatePri.Rad
            pPosition.Alt.Rad = pUserCtrlGauge2AxisCoordPresenterAltaz.CoordinateSec.Rad
            pPosition.SidT.Rad = pUserCtrlGaugeCoordPresenterSidT.Coordinate.Rad
            Return pPosition
        End Get
    End Property

    Public Property UserCtrlGaugeCoordPresenterSidT() As IRendererCoordPresenter Implements IGaugePositionPresenter.UserCtrlGaugeCoordPresenterSidT
        Get
            Return pUserCtrlGaugeCoordPresenterSidT
        End Get
        Set(ByVal Value As IRendererCoordPresenter)
            pUserCtrlGaugeCoordPresenterSidT = CType(Value, UserCtrlGaugeCoordPresenter)
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pPosition = PositionArray.GetInstance.GetPosition

        pUserCtrlGaugePosition = CType(IMVPUserCtrl, UserCtrlGaugePosition)

        pUserCtrlGauge2AxisCoordPresenterEquat = UserCtrlGauge2AxisCoordPresenter.GetInstance
        pUserCtrlGauge2AxisCoordPresenterEquat.IMVPUserCtrl = pUserCtrlGaugePosition.UserCtrlGauge2AxisCoordEquat
        AxisCoordGaugePresenter.GetInstance.Build(CType(pUserCtrlGauge2AxisCoordPresenterEquat, IGauge2AxisCoordPresenter), CoordName.RA, CoordName.Dec)

        pUserCtrlGauge2AxisCoordPresenterAltaz = UserCtrlGauge2AxisCoordPresenter.GetInstance
        pUserCtrlGauge2AxisCoordPresenterAltaz.IMVPUserCtrl = pUserCtrlGaugePosition.UserCtrlGauge2AxisCoordAltaz
        AxisCoordGaugePresenter.GetInstance.Build(CType(pUserCtrlGauge2AxisCoordPresenterAltaz, IGauge2AxisCoordPresenter), CoordName.Az, CoordName.Alt)

        pUserCtrlGaugeCoordPresenterSidT = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenterSidT.IMVPUserCtrl = pUserCtrlGaugePosition.UserCtrlGaugeCoordSidT
        AxisCoordGaugePresenter.GetInstance.Build(CType(pUserCtrlGaugeCoordPresenterSidT, IRendererCoordPresenter), CoordName.SidT)
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pUserCtrlGauge2AxisCoordPresenterEquat.DataModel = New Object() {CType(CType(DataModel, Object())(0), Coordinate), CType(CType(DataModel, Object())(1), Coordinate)}
        pUserCtrlGauge2AxisCoordPresenterAltaz.DataModel = New Object() {CType(CType(DataModel, Object())(2), Coordinate), CType(CType(DataModel, Object())(3), Coordinate)}
        pUserCtrlGaugeCoordPresenterSidT.DataModel = CType(CType(DataModel, Object())(4), Coordinate)
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
