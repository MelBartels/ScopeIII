#Region "Imports"
#End Region

Public Class UserCtrlPositionPresenter
    Inherits MVPUserCtrlPresenterBase
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
    Private WithEvents pUserCtrlPosition As UserCtrlPosition

    Private pUserCtrlCoordPresenterSidT As UserCtrlCoordPresenter
    Private p2AxisCoordPresenterEquat As UserCtrl2AxisCoordPresenter
    Private p2AxisCoordPresenterAltaz As UserCtrl2AxisCoordPresenter

    Private pCoordinateSidT As Coordinate

    Private pPosition As Position
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlPositionPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlPositionPresenter = New UserCtrlPositionPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlPositionPresenter
        Return New UserCtrlPositionPresenter
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

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Select Case CType([object], Coordinate).Name
            Case CoordName.RA.Description, _
                 CoordName.Dec.Description, _
                 CoordName.Az.Description, _
                 CoordName.Alt.Description, _
                 CoordName.SidT.Description

                Position.RA = p2AxisCoordPresenterEquat.CoordinatePri
                Position.Dec = p2AxisCoordPresenterEquat.CoordinateSec
                Position.Az = p2AxisCoordPresenterAltaz.CoordinatePri
                Position.Alt = p2AxisCoordPresenterAltaz.CoordinateSec
                Position.SidT = pUserCtrlCoordPresenterSidT.Coordinate
                Return True
        End Select

        Return False
    End Function

    Public Sub DisplayPosition(ByVal position As Position)
        Me.Position.CopyFrom(position)
        p2AxisCoordPresenterEquat.DisplayCoordinates(position.RA.Rad, position.Dec.Rad)
        p2AxisCoordPresenterAltaz.DisplayCoordinates(position.Az.Rad, position.Alt.Rad)
        pUserCtrlCoordPresenterSidT.DisplayCoordinate(position.SidT.Rad)
    End Sub

#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub Init()
        MyBase.init()

        pUserCtrlPosition = CType(IMVPUserCtrl, UserCtrlPosition)

        pUserCtrlCoordPresenterSidT = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterSidT.IMVPUserCtrl = pUserCtrlPosition.UserCtrlCoordSidT()
        pUserCtrlCoordPresenterSidT.SetCoordinateName(CoordName.SidT.Description)
        pUserCtrlCoordPresenterSidT.CoordExpType = CType(CoordExpType.FormattedHMS, ISFT)
        pUserCtrlCoordPresenterSidT.CoordinateObservableImp.Attach(Me)

        p2AxisCoordPresenterEquat = UserCtrl2AxisCoordPresenter.GetInstance
        p2AxisCoordPresenterEquat.IMVPUserCtrl = pUserCtrlPosition.UserCtrl2AxisCoordEquat
        p2AxisCoordPresenterEquat.SetAxisNames(CoordName.RA.Description, CoordName.Dec.Description)
        p2AxisCoordPresenterEquat.SetExpCoordTypes(CType(CoordExpType.FormattedHMSM, ISFT), CType(CoordExpType.FormattedDMS, ISFT))
        p2AxisCoordPresenterEquat.CoordinatePriObservableImp.Attach(Me)
        p2AxisCoordPresenterEquat.CoordinateSecObservableImp.Attach(Me)

        p2AxisCoordPresenterAltaz = UserCtrl2AxisCoordPresenter.GetInstance
        p2AxisCoordPresenterAltaz.IMVPUserCtrl = pUserCtrlPosition.UserCtrl2AxisCoordAltaz
        p2AxisCoordPresenterAltaz.SetAxisNames(CoordName.Az.Description, CoordName.Alt.Description)
        p2AxisCoordPresenterAltaz.CoordinatePriObservableImp.Attach(Me)
        p2AxisCoordPresenterAltaz.CoordinateSecObservableImp.Attach(Me)

        Position = PositionArray.GetInstance.GetPosition
    End Sub

    Protected Overrides Sub loadViewFromModel()
        p2AxisCoordPresenterEquat.DataModel = New Object() {CType(DataModel, Position).RA, _
                                                            CType(DataModel, Position).Dec}
        p2AxisCoordPresenterAltaz.DataModel = New Object() {CType(DataModel, Position).Az, _
                                                            CType(DataModel, Position).Alt}
        pUserCtrlCoordPresenterSidT.DataModel = CType(DataModel, Position).SidT
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
