#Region "Imports"
#End Region

Public Class UserCtrlAxisEncoderTranslatorPresenter
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
    Private WithEvents pUserCtrlAxisEncoderTranslator As UserCtrlAxisEncoderTranslator
    Private WithEvents pUserCtrlGaugeCoord As UserCtrlGaugeCoord
    Private pUserCtrlGaugeCoordPresenter As UserCtrlGaugeCoordPresenter
    Private pAxisEncoderAdapter As AxisEncoderAdapter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlAxisEncoderTranslatorPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlAxisEncoderTranslatorPresenter = New UserCtrlAxisEncoderTranslatorPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlAxisEncoderTranslatorPresenter
        Return New UserCtrlAxisEncoderTranslatorPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public ReadOnly Property UserCtrlGaugeCoordPresenter() As UserCtrlGaugeCoordPresenter
        Get
            Return pUserCtrlGaugeCoordPresenter
        End Get
    End Property

    Public Property AxisEncoderAdapter() As AxisEncoderAdapter
        Get
            Return pAxisEncoderAdapter
        End Get
        Set(ByVal value As AxisEncoderAdapter)
            pAxisEncoderAdapter = value
        End Set
    End Property

    Public Sub BuildAxisEncoderAdapter(ByRef encoderValue As EncoderValue)
        If AxisEncoderAdapter Is Nothing Then
            AxisEncoderAdapter = AxisEncoderAdapter.GetInstance
        End If
        AxisEncoderAdapter.EncoderValue = EncoderValue
        AxisEncoderAdapter.ICoordPresenter = pUserCtrlGaugeCoordPresenter
        AxisEncoderAdapter.RegisterCoordinateChange()
        EncoderValue.ObservableImp.Attach(CType(AxisEncoderAdapter, IObserver))
        pUserCtrlAxisEncoderTranslator.GearRatioValue = 1
        UpdateTotalTicks()
    End Sub

    Public Sub UpdateTotalTicks()
        calcTotalTicks()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlAxisEncoderTranslator = CType(IMVPUserCtrl, UserCtrlAxisEncoderTranslator)
        pUserCtrlAxisEncoderTranslator.AxisSelectDataSource = CoordName.ISFT.DataSource
        AddHandler pUserCtrlAxisEncoderTranslator.GearRatio, AddressOf gearRatioHandler
        AddHandler pUserCtrlAxisEncoderTranslator.TotalTicks, AddressOf totalTicksHandler
        AddHandler pUserCtrlAxisEncoderTranslator.AxisSelected, AddressOf axisSelected

        pUserCtrlGaugeCoordPresenter = UserCtrlGaugeCoordPresenter.GetInstance
        pUserCtrlGaugeCoordPresenter.IMVPUserCtrl = pUserCtrlAxisEncoderTranslator.UserCtrlGaugeCoord
        pUserCtrlGaugeCoordPresenter.SetCoordinateName("Axis Name")
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pUserCtrlGaugeCoordPresenter.DataModel = CType(CType(DataModel, Object())(0), Coordinate)
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub axisSelected(ByVal coordName As Object)
        AxisCoordGaugePresenter.GetInstance.Build(UserCtrlGaugeCoordPresenter, Coordinates.CoordName.ISFT.MatchString(CStr(coordName)))
        UserCtrlGaugeCoordPresenter.Render()
    End Sub

    Private Sub gearRatioHandler()
        calcTotalTicks()
    End Sub

    Private Sub totalTicksHandler()
        calcGearRatio()
    End Sub

    Private Sub calcGearRatio()
        pUserCtrlAxisEncoderTranslator.GearRatioValue = CDec(pUserCtrlAxisEncoderTranslator.TotalTicksValue / pAxisEncoderAdapter.EncoderValue.Range)
    End Sub

    Private Sub calcTotalTicks()
        pUserCtrlAxisEncoderTranslator.TotalTicksValue = CDec(pUserCtrlAxisEncoderTranslator.GearRatioValue * pAxisEncoderAdapter.EncoderValue.Range)
        AxisEncoderAdapter.TotalTicks = pUserCtrlAxisEncoderTranslator.TotalTicksValue
    End Sub
#End Region

End Class
