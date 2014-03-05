#Region "imports"
#End Region

Public Class RenderCoordGaugesOKPresenter
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
    Private WithEvents pFrmGaugeCoordsOK As FrmRenderCoordGaugesOK
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RenderCoordGaugesOKPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RenderCoordGaugesOKPresenter = New RenderCoordGaugesOKPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As RenderCoordGaugesOKPresenter
        Return New RenderCoordGaugesOKPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmGaugeCoordsOK = CType(IMVPView, FrmRenderCoordGaugesOK)

        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord1, DegreeCircGaugeRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "0-360 degrees")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord2, HourCircGaugeRenderer.GetInstance, CType(CoordExpType.FormattedHMS, ISFT), "0-24 hours")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord3, DegreeNegCircGaugeRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "-180+180 deg")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord4, DeclinationCircGaugeRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "0-90-0/180-90-0")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord5, ArcminCircGaugeRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "-60-60 arcmins")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord6, DegreeSliderRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "0-360 degrees")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord7, DegreeNegSliderRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "-180-180 degrees")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord8, ArcminSliderRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "-120-120 arcmins")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord9, Degree0_90VertSliderRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "0-90 degrees")
        buildGaugeCoord(pFrmGaugeCoordsOK.UserCtrlGaugeCoord10, DegreeArcGaugeRenderer.GetInstance, CType(CoordExpType.FormattedDMS, ISFT), "0-360 degrees")
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub buildGaugeCoord(ByRef userCtrlCirGauge As UserCtrlGaugeCoord, ByRef IRenderer As IRenderer, ByRef CoordExpType As ISFT, ByRef title As String)
        Dim IRendererCoordPresenter As IRendererCoordPresenter = UserCtrlGaugeCoordPresenter.GetInstance
        CType(IRendererCoordPresenter, MVPUserCtrlPresenterBase).IMVPUserCtrl = userCtrlCirGauge
        IRendererCoordPresenter.IRenderer = IRenderer
        IRendererCoordPresenter.SetCoordinateName(title)
        IRendererCoordPresenter.CoordExpType = CoordExpType
        IRendererCoordPresenter.DisplayCoordinate(0)
    End Sub
#End Region

End Class
