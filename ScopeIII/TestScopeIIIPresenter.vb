#Region "Imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class TestScopeIIIPresenter
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
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TestScopeIIIPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestScopeIIIPresenter = New TestScopeIIIPresenter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As TestScopeIIIPresenter
        Return New TestScopeIIIPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Property DataModel() As Object
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Object)
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        AddHandler FrmTestScopeIII.Convert, AddressOf MainConvert.GetInstance.Main
        AddHandler FrmTestScopeIII.CompareZ12s, AddressOf MainCompareZ12.GetInstance.Main
        AddHandler FrmTestScopeIII.QueryDatafiles, AddressOf MainQueryDatafiles.GetInstance.Main
        AddHandler FrmTestScopeIII.Saguaro, AddressOf MainSaguaro.GetInstance.Main
        AddHandler FrmTestScopeIII.PrecessDatafiles, AddressOf MainPrecessDatafiles.GetInstance.Main
        AddHandler FrmTestScopeIII.CoordinateErrors, AddressOf MainCoordinateErrors.GetInstance.Main
        AddHandler FrmTestScopeIII.RefractionAirMass, AddressOf MainRefractionAirMass.GetInstance.Main
        AddHandler FrmTestScopeIII.IOTerminal, AddressOf MainIOTerminal.GetInstance.Main
        AddHandler FrmTestScopeIII.TestCfg, AddressOf MainTestCfg.GetInstance.Main
        AddHandler FrmTestScopeIII.MsgBox, AddressOf MainTestMsgBox.GetInstance.Main
        AddHandler FrmTestScopeIII.TestExceptions, AddressOf MainTestException.GetInstance.Main
        AddHandler FrmTestScopeIII.TestThreading, AddressOf MainTestThreading.GetInstance.Main
        AddHandler FrmTestScopeIII.TestSerialPortSettings, AddressOf MainTestSerialPortSettings.GetInstance.Main
        AddHandler FrmTestScopeIII.TestIPsettings, AddressOf MainTestIPsettings.GetInstance.Main
        AddHandler FrmTestScopeIII.TestLoggingSettings, AddressOf MainTestLoggingSettings.GetInstance.Main
        AddHandler FrmTestScopeIII.TestDeviceSettings, AddressOf MainTestDeviceSettings.GetInstance.Main
        AddHandler FrmTestScopeIII.TestEnterCoord, AddressOf MainTestEnterCoord.GetInstance.Main
        AddHandler FrmTestScopeIII.TestEnter2AxisCoord, AddressOf MainTest2AxisCoord.GetInstance.Main
        AddHandler FrmTestScopeIII.TestEnter3AxisCoord, AddressOf MainTest3AxisCoord.GetInstance.Main
        AddHandler FrmTestScopeIII.TestEnterSite, AddressOf MainTestEnterSite.GetInstance.Main
        AddHandler FrmTestScopeIII.TestEnterPosition, AddressOf MainTestEnterPosition.GetInstance.Main
        AddHandler FrmTestScopeIII.TestEnterOneTwo, AddressOf MainTestEnterOneTwo.GetInstance.Main
        AddHandler FrmTestScopeIII.TestEnterZ123, AddressOf MainTestEnterZ123.GetInstance.Main
        AddHandler FrmTestScopeIII.TestGraphFunction, AddressOf MainTestGraphOK.GetInstance.Main
        AddHandler FrmTestScopeIII.TestGraphOKPropGrid, AddressOf MainTestGraphOKPropGrid.GetInstance.Main
        AddHandler FrmTestScopeIII.Slider, AddressOf MainSliderOK.GetInstance.Main
        AddHandler FrmTestScopeIII.Gauges, AddressOf MainGaugeCoordsOK.GetInstance.Main
        AddHandler FrmTestScopeIII.GaugeCoord, AddressOf MainGaugeCoordOK.GetInstance.Main
        AddHandler FrmTestScopeIII.Gauge2AxisCoord, AddressOf MainGauge2AxisCoordOK.GetInstance.Main
        AddHandler FrmTestScopeIII.Gauge3AxisCoord, AddressOf MainGauge3AxisCoordOK.GetInstance.Main
        AddHandler FrmTestScopeIII.GaugePosition, AddressOf MainGaugePositionOK.GetInstance.Main
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function FrmTestScopeIII() As FrmTestScopeIII
        Return CType(pIMVPView, FrmTestScopeIII)
    End Function
#End Region

End Class
