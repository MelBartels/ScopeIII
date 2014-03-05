#Region "Imports"
#End Region

Public Class UserCtrl2AxisEncoderTranslatorPresenter
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
    Private WithEvents pUserCtrl2AxisEncoderTranslator As UserCtrl2AxisEncoderTranslator
    Private WithEvents pUserCtrlAxisEncoderTranslatorPri As UserCtrlAxisEncoderTranslator
    Private WithEvents pUserCtrlAxisEncoderTranslatorSec As UserCtrlAxisEncoderTranslator
    Private pUserCtrlAxisEncoderTranslatorPresenterPri As UserCtrlAxisEncoderTranslatorPresenter
    Private pUserCtrlAxisEncoderTranslatorPresenterSec As UserCtrlAxisEncoderTranslatorPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrl2AxisEncoderTranslatorPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrl2AxisEncoderTranslatorPresenter = New UserCtrl2AxisEncoderTranslatorPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrl2AxisEncoderTranslatorPresenter
        Return New UserCtrl2AxisEncoderTranslatorPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordinatePri() As Coordinate Implements IGauge2AxisCoordPresenter.CoordinatePri
        Get
            Return userCtrlGaugeCoordPresenterPri.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            userCtrlGaugeCoordPresenterPri.Coordinate = Value
        End Set
    End Property

    Public Property CoordinateSec() As Coordinate Implements IGauge2AxisCoordPresenter.CoordinateSec
        Get
            Return userCtrlGaugeCoordPresenterSec.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            userCtrlGaugeCoordPresenterSec.Coordinate = Value
        End Set
    End Property

    Public Property CoordinatePriObservableImp() As ObservableImp Implements IGauge2AxisCoordPresenter.CoordinatePriObservableImp
        Get
            Return userCtrlGaugeCoordPresenterPri.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            userCtrlGaugeCoordPresenterPri.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property CoordinateSecObservableImp() As ObservableImp Implements IGauge2AxisCoordPresenter.CoordinateSecObservableImp
        Get
            Return userCtrlGaugeCoordPresenterSec.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            userCtrlGaugeCoordPresenterSec.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property PriCoordUpdatedByMe() As Boolean Implements IGauge2AxisCoordPresenter.PriCoordUpdatedByMe
        Get
            Return userCtrlGaugeCoordPresenterPri.CoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            userCtrlGaugeCoordPresenterPri.CoordUpdatedByMe = value
        End Set
    End Property

    Public Property SecCoordUpdatedByMe() As Boolean Implements IGauge2AxisCoordPresenter.SecCoordUpdatedByMe
        Get
            Return userCtrlGaugeCoordPresenterSec.CoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            userCtrlGaugeCoordPresenterSec.CoordUpdatedByMe = value
        End Set
    End Property

    Public Sub SetAxisNames(ByVal priName As String, ByVal secName As String) Implements IGauge2AxisCoordPresenter.SetAxisNames
        userCtrlGaugeCoordPresenterPri.SetCoordinateName(priName)
        userCtrlGaugeCoordPresenterSec.SetCoordinateName(secName)
    End Sub

    Public Sub SetExpCoordTypes(ByVal priExpCoordType As ISFT, ByVal secExpCoordType As ISFT) Implements IGauge2AxisCoordPresenter.SetExpCoordTypes
        userCtrlGaugeCoordPresenterPri.CoordExpType = priExpCoordType
        userCtrlGaugeCoordPresenterSec.CoordExpType = secExpCoordType
    End Sub

    Public Sub SetCoordinateLabelColors(ByVal priColor As System.Drawing.Color, ByVal secColor As System.Drawing.Color) Implements IGauge2AxisCoordPresenter.SetCoordinateLabelColors
        userCtrlGaugeCoordPresenterPri.SetCoordinateLabelColor(priColor)
        userCtrlGaugeCoordPresenterSec.SetCoordinateLabelColor(secColor)
    End Sub

    Public Sub SetRenderers(ByVal priRenderer As BartelsLibrary.IRenderer, ByVal secRenderer As BartelsLibrary.IRenderer) Implements IGauge2AxisCoordPresenter.SetRenderers
        userCtrlGaugeCoordPresenterPri.IRenderer = priRenderer
        userCtrlGaugeCoordPresenterSec.IRenderer = secRenderer
    End Sub

    Public Sub DisplayCoordinates(ByVal priRad As Double, ByVal secRad As Double) Implements IGauge2AxisCoordPresenter.DisplayCoordinates
        userCtrlGaugeCoordPresenterPri.DisplayCoordinate(priRad)
        userCtrlGaugeCoordPresenterSec.DisplayCoordinate(secRad)
    End Sub

    Public Sub Render() Implements IGauge2AxisCoordPresenter.Render
        pUserCtrl2AxisEncoderTranslator.Refresh()
    End Sub

    Public ReadOnly Property UserCtrlAxisEncoderTranslatorPresenterPri() As UserCtrlAxisEncoderTranslatorPresenter
        Get
            Return pUserCtrlAxisEncoderTranslatorPresenterPri
        End Get
    End Property

    Public ReadOnly Property UserCtrlAxisEncoderTranslatorPresenterSec() As UserCtrlAxisEncoderTranslatorPresenter
        Get
            Return pUserCtrlAxisEncoderTranslatorPresenterSec
        End Get
    End Property

    Public Sub BuildAxisEncoderAdapters(ByRef encoderValuePri As EncoderValue, ByRef encoderValueSec As EncoderValue)
        pUserCtrlAxisEncoderTranslatorPresenterPri.BuildAxisEncoderAdapter(encoderValuePri)
        pUserCtrlAxisEncoderTranslatorPresenterSec.BuildAxisEncoderAdapter(encoderValueSec)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrl2AxisEncoderTranslator = CType(IMVPUserCtrl, UserCtrl2AxisEncoderTranslator)

        pUserCtrlAxisEncoderTranslatorPresenterPri = UserCtrlAxisEncoderTranslatorPresenter.GetInstance
        pUserCtrlAxisEncoderTranslatorPresenterPri.IMVPUserCtrl = pUserCtrl2AxisEncoderTranslator.UserCtrlAxisEncoderTranslatorPri
        userCtrlGaugeCoordPresenterPri.SetCoordinateName(CoordName.PriAxis.Description)

        pUserCtrlAxisEncoderTranslatorPresenterSec = UserCtrlAxisEncoderTranslatorPresenter.GetInstance
        pUserCtrlAxisEncoderTranslatorPresenterSec.IMVPUserCtrl = pUserCtrl2AxisEncoderTranslator.UserCtrlAxisEncoderTranslatorSec
        userCtrlGaugeCoordPresenterSec.SetCoordinateName(CoordName.SecAxis.Description)
    End Sub

    Protected Overrides Sub loadViewFromModel()
        userCtrlGaugeCoordPresenterPri.DataModel = CType(CType(DataModel, Object())(0), Coordinate)
        userCtrlGaugeCoordPresenterSec.DataModel = CType(CType(DataModel, Object())(1), Coordinate)
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function userCtrlGaugeCoordPresenterPri() As UserCtrlGaugeCoordPresenter
        Return pUserCtrlAxisEncoderTranslatorPresenterPri.UserCtrlGaugeCoordPresenter
    End Function

    Private Function userCtrlGaugeCoordPresenterSec() As UserCtrlGaugeCoordPresenter
        Return pUserCtrlAxisEncoderTranslatorPresenterSec.UserCtrlGaugeCoordPresenter
    End Function
#End Region

End Class
