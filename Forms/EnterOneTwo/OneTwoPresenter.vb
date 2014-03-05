#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class OneTwoPresenter
    Inherits MVPPresenterBase
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
    Private WithEvents pFrmEnterOneTwo As FrmEnterOneTwo
    Private pUserCtrlOneTwoPresenter As UserCtrlOneTwoPresenter
    Private pUserCtrlCoordPresenterLat As UserCtrlCoordPresenter
    Private pAlignment As ISFT
    Private pRestoreOneTwoPresenterDataModel As OneTwoPresenterDataModel
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As OneTwoPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As OneTwoPresenter = New OneTwoPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As OneTwoPresenter
        Return New OneTwoPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property LatitudeRad() As Double
        Get
            Return pUserCtrlCoordPresenterLat.Coordinate.Rad
        End Get
        Set(ByVal value As Double)
            oneTwoPresenterDataModel.LatitudeRad = value
            loadViewFromModel()
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        If pFrmEnterOneTwo.PresetSelected Then
            oneTwoPresenterDataModel.LatitudeRad = pUserCtrlCoordPresenterLat.Coordinate.Rad
            Preset(pAlignment)
        End If
    End Function

    Public Sub Preset(ByRef alignment As ISFT)
        pAlignment = alignment
        With oneTwoPresenterDataModel()
            If alignment Is AlignmentStyle.AltazSiteAligned Then
                ConvertMatrixPresetPositionAltaz.GetInstance.Preset(.One, .Two, .LatitudeRad)
            Else
                ConvertMatrixPresetPositionEquat.GetInstance.Preset(.One, .Two, .LatitudeRad)
            End If
        End With
        pFrmEnterOneTwo.CheckAlign(alignment)
        pUserCtrlOneTwoPresenter.DataModel = DataModel
    End Sub

    Public Overrides Sub ShowDialog()
        pRestoreOneTwoPresenterDataModel.CopyFrom(oneTwoPresenterDataModel)

        MyBase.ShowDialog()

        If pFrmEnterOneTwo.DialogResult.Equals(DialogResult.OK) Then
            UpdateDataModelFromView()
        Else
            oneTwoPresenterDataModel.CopyFrom(pRestoreOneTwoPresenterDataModel)
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEnterOneTwo = CType(IMVPView, FrmEnterOneTwo)
        AddHandler pFrmEnterOneTwo.OK, AddressOf ok
        AddHandler pFrmEnterOneTwo.Preset, AddressOf Preset

        pUserCtrlOneTwoPresenter = UserCtrlOneTwoPresenter.GetInstance
        pUserCtrlOneTwoPresenter.IMVPUserCtrl = pFrmEnterOneTwo.UserCtrlOneTwo

        pUserCtrlCoordPresenterLat = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterLat.IMVPUserCtrl = pFrmEnterOneTwo.UserCtrlCoordLat
        pUserCtrlCoordPresenterLat.SetCoordinateName(CoordName.Latitude.Description)
        pUserCtrlCoordPresenterLat.CoordExpType = CoordExpType.FormattedDMS
        pUserCtrlCoordPresenterLat.CoordinateObservableImp.Attach(Me)

        DataModel = Forms.OneTwoPresenterDataModel.GetInstance
        pRestoreOneTwoPresenterDataModel = Forms.OneTwoPresenterDataModel.GetInstance
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pUserCtrlCoordPresenterLat.DisplayCoordinate(OneTwoPresenterDataModel.LatitudeRad)
        pUserCtrlOneTwoPresenter.DataModel = DataModel
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function oneTwoPresenterDataModel() As OneTwoPresenterDataModel
        Return CType(DataModel, OneTwoPresenterDataModel)
    End Function

    Private Sub updateDataModelFromView()
        pUserCtrlOneTwoPresenter.UpdateDataModelFromView()
        oneTwoPresenterDataModel.LatitudeRad = pUserCtrlCoordPresenterLat.Coordinate.Rad
    End Sub

    Private Sub ok()
        pFrmEnterOneTwo.DialogResult = DialogResult.OK
        pFrmEnterOneTwo.Close()
    End Sub
#End Region
End Class
