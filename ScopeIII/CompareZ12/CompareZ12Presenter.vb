#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class CompareZ12Presenter
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
    Private WithEvents pFrmCompareZ12 As FrmCompareZ12
    Private pMvpUserCtrlGraphics As MVPUserCtrlGraphics
    Private WithEvents pUserCtrlGauge2AxisCoordZ12 As UserCtrlGauge2AxisCoord
    Private WithEvents pUserCtrlGauge2AxisCoordLatAz As UserCtrlGauge2AxisCoord
    Private pUserCtrlGauge2AxisCoordZ12Presenter As UserCtrlGauge2AxisCoordPresenter
    Private pUserCtrlGauge2AxisCoordLatAzPresenter As UserCtrlGauge2AxisCoordPresenter
    Private pZ12CompareMultiXYDataAdapter As Z12CompareMultiXYDataAdapter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CompareZ12Presenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CompareZ12Presenter = New CompareZ12Presenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CompareZ12Presenter
        Return New CompareZ12Presenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Title() As String
        Get
            Return pFrmCompareZ12.FormTitle
        End Get
        Set(ByVal Value As String)
            pFrmCompareZ12.FormTitle = Value
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements Common.IObserver.ProcessMsg
        Select Case CType([object], Coordinate).Name
            Case CoordName.Z1.Description, _
                 CoordName.Z2.Description, _
                 CoordName.Latitude.Description, _
                 CoordName.Az.Description

                getZ12()
                Return True
        End Select

        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pZ12CompareMultiXYDataAdapter = Z12CompareMultiXYDataAdapter.GetInstance

        pFrmCompareZ12 = CType(IMVPView, FrmCompareZ12)
        pFrmCompareZ12.SetToolTip()
        pMvpUserCtrlGraphics = pFrmCompareZ12.MvpUserCtrlGraphics
        pMvpUserCtrlGraphics.BackColor = Drawing.Color.LightYellow

        pUserCtrlGauge2AxisCoordZ12Presenter = UserCtrlGauge2AxisCoordPresenter.GetInstance
        pUserCtrlGauge2AxisCoordZ12Presenter.IMVPUserCtrl = pFrmCompareZ12.UserCtrlGauge2AxisCoordZ12
        pUserCtrlGauge2AxisCoordZ12Presenter.SetAxisNames(CoordName.Z1.Description, CoordName.Z2.Description)
        pUserCtrlGauge2AxisCoordZ12Presenter.SetExpCoordTypes(CType(CoordExpType.FormattedDegree, ISFT), CType(CoordExpType.FormattedDegree, ISFT))
        pUserCtrlGauge2AxisCoordZ12Presenter.SetRenderers(ArcminSliderRenderer.GetInstance, ArcminSliderRenderer.GetInstance)

        pUserCtrlGauge2AxisCoordZ12Presenter.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGauge2AxisCoordZ12Presenter.CoordinateSecObservableImp.Attach(Me)

        pUserCtrlGauge2AxisCoordLatAzPresenter = UserCtrlGauge2AxisCoordPresenter.GetInstance
        pUserCtrlGauge2AxisCoordLatAzPresenter.IMVPUserCtrl = pFrmCompareZ12.UserCtrlGauge2AxisCoordLatAz
        pUserCtrlGauge2AxisCoordLatAzPresenter.SetAxisNames(CoordName.Latitude.Description, CoordName.Az.Description)
        pUserCtrlGauge2AxisCoordLatAzPresenter.SetExpCoordTypes(CType(CoordExpType.FormattedDegree, ISFT), CType(CoordExpType.FormattedDegree, ISFT))
        pUserCtrlGauge2AxisCoordLatAzPresenter.SetRenderers(DegreeNegCircGaugeRenderer.GetInstance, DegreeCircGaugeRenderer.GetInstance)

        pUserCtrlGauge2AxisCoordLatAzPresenter.CoordinatePriObservableImp.Attach(Me)
        pUserCtrlGauge2AxisCoordLatAzPresenter.CoordinateSecObservableImp.Attach(Me)

        ' set some default values
        pUserCtrlGauge2AxisCoordZ12Presenter.DisplayCoordinates(0.2 * Units.DegToRad, -0.28 * Units.DegToRad)
        pUserCtrlGauge2AxisCoordLatAzPresenter.DisplayCoordinates(30 * Units.DegToRad, 180 * Units.DegToRad)
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pMvpUserCtrlGraphics.IRenderer = CType(DataModel, IRenderer)
        pFrmCompareZ12.PropGridSelectedObject = CType(DataModel, IRenderer).ObjectToRender

        defaults()
        getZ12()
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub defaults()
        Dim multiXYData As MultiXYData = CType(CType(DataModel, MultiXYDataRenderer).ObjectToRender, MultiXYData)

        Dim dc(1) As Drawing.Color
        dc(0) = CType(AxisColors.Primary.Tag, Drawing.Color)
        dc(1) = CType(AxisColors.Secondary.Tag, Drawing.Color)
        multiXYData.DataColor = dc
        pUserCtrlGauge2AxisCoordZ12Presenter.SetCoordinateLabelColors(multiXYData.DataColor(0), multiXYData.DataColor(1))

        ' let Z12 errors range from -60' to 60', spacing of 10'
        multiXYData.XRangeStart = -60
        multiXYData.XRangeEnd = 60
        multiXYData.XGridSpacing = 10
        ' let altitude range from -90 to 90, spacing of 5 deg
        multiXYData.YRangeStart = -90
        multiXYData.YRangeEnd = 90
        multiXYData.YGridSpacing = 5
    End Sub

    Private Sub getZ12()
        If DataModel Is Nothing Then
            Exit Sub
        End If

        pZ12CompareMultiXYDataAdapter.GetZ12( _
                pUserCtrlGauge2AxisCoordZ12Presenter.CoordinatePri.Rad, _
                pUserCtrlGauge2AxisCoordZ12Presenter.CoordinateSec.Rad, _
                pUserCtrlGauge2AxisCoordLatAzPresenter.CoordinatePri.Rad, _
                pUserCtrlGauge2AxisCoordLatAzPresenter.CoordinateSec.Rad, _
                CType(CType(DataModel, MultiXYDataRenderer).ObjectToRender, MultiXYData))

        pMvpUserCtrlGraphics.Render()
    End Sub
#End Region

End Class
