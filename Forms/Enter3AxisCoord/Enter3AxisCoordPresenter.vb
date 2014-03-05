#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class Enter3AxisCoordPresenter
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
    Private WithEvents pFrmEnter3AxisCoord As FrmEnter3AxisCoord

    Private pUserCtrl3AxisCoordPresenter As UserCtrl3AxisCoordPresenter

    Private pCoordinatePri As Coordinate
    Private pCoordinateSec As Coordinate
    Private pCoordinateTier As Coordinate
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Enter3AxisCoordPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Enter3AxisCoordPresenter = New Enter3AxisCoordPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Enter3AxisCoordPresenter
        Return New Enter3AxisCoordPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordinatePri() As Coordinate
        Get
            Return pCoordinatePri
        End Get
        Set(ByVal Value As Coordinate)
            pCoordinatePri = Value
        End Set
    End Property

    Public Property CoordinateSec() As Coordinate
        Get
            Return pCoordinateSec
        End Get
        Set(ByVal Value As Coordinate)
            pCoordinateSec = Value
        End Set
    End Property

    Public Property CoordinateTier() As Coordinate
        Get
            Return pCoordinateTier
        End Get
        Set(ByVal Value As Coordinate)
            pCoordinateTier = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEnter3AxisCoord = CType(IMVPView, FrmEnter3AxisCoord)
        AddHandler pFrmEnter3AxisCoord.OK, AddressOf ok

        pUserCtrl3AxisCoordPresenter = UserCtrl3AxisCoordPresenter.GetInstance
        pUserCtrl3AxisCoordPresenter.IMVPUserCtrl = pFrmEnter3AxisCoord.UserCtrl3AxisCoord
        pUserCtrl3AxisCoordPresenter.SetAxisNames(CoordName.PriAxis.Description, CoordName.SecAxis.Description, CoordName.TierAxis.Description)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub ok()
        CoordinatePri = pUserCtrl3AxisCoordPresenter.CoordinatePri
        CoordinateSec = pUserCtrl3AxisCoordPresenter.CoordinateSec
        CoordinateTier = pUserCtrl3AxisCoordPresenter.CoordinateTier

        pFrmEnter3AxisCoord.DialogResult.equals(DialogResult.OK)
        pFrmEnter3AxisCoord.Close()
    End Sub
#End Region

End Class
