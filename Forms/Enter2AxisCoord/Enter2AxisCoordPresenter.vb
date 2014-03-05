#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class Enter2AxisCoordPresenter
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
    Private WithEvents pFrmEnter2AxisCoord As FrmEnter2AxisCoord

    Private pUserCtrl2AxisCoordPresenter As UserCtrl2AxisCoordPresenter

    Private pCoordinatePri As Coordinate
    Private pCoordinateSec As Coordinate
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Enter2AxisCoordPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Enter2AxisCoordPresenter = New Enter2AxisCoordPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Enter2AxisCoordPresenter
        Return New Enter2AxisCoordPresenter
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

#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEnter2AxisCoord = CType(IMVPView, FrmEnter2AxisCoord)
        AddHandler pFrmEnter2AxisCoord.OK, AddressOf ok

        pUserCtrl2AxisCoordPresenter = UserCtrl2AxisCoordPresenter.GetInstance
        pUserCtrl2AxisCoordPresenter.IMVPUserCtrl = pFrmEnter2AxisCoord.UserCtrl2AxisCoord
        pUserCtrl2AxisCoordPresenter.SetAxisNames(CoordName.PriAxis.Description, CoordName.SecAxis.Description)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub ok()
        CoordinatePri = pUserCtrl2AxisCoordPresenter.CoordinatePri
        CoordinateSec = pUserCtrl2AxisCoordPresenter.CoordinateSec

        pFrmEnter2AxisCoord.DialogResult.equals(DialogResult.OK)
        pFrmEnter2AxisCoord.Close()
    End Sub
#End Region

End Class
