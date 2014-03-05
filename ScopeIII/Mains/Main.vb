#Region "Imports"
#End Region

Public Class Main
    Inherits MainPrototype

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event ExitApplication()
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Main
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Main = New Main
    'End Class
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Shared Function GetInstance() As Main
        Return New Main
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Function Main(ByRef args() As String) As Integer
        Dim rtnValue As Int32
        Try
            ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
            ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
            ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
            loadSettings()

            Dim TestScopeIIIPresenter As TestScopeIIIPresenter = ScopeIII.TestScopeIIIPresenter.GetInstance
            TestScopeIIIPresenter.IMVPView = New FrmTestScopeIII
            TestScopeIIIPresenter.ShowDialog()
            rtnValue = BartelsLibrary.Constants.NormalExit

        Catch ex As Exception
            ExceptionService.Notify(ex, BartelsLibrary.Constants.TopLevelExceptionMsg)
            rtnValue = BartelsLibrary.Constants.BadExit
        Finally
            Application.Exit()
        End Try
        ' don't return until truly ready to exit, eg, must have a ShowDialog on this thread 
        Return rtnValue
    End Function

    ' load settings here because My.Settings visible here and not in the lowest common denominator project Common
    Private Sub loadSettings()
        With BartelsLibrary.Settings.GetInstance
            .DefaultIPAddress = My.Settings.DefaultIPAddress
            .DefaultIPPort = My.Settings.DefaultIPPort
            .ExceptionFilename = My.Settings.ExceptionFilename
            .SettingsFilename = My.Settings.SettingsFilename
            .RendererFontFamilyName = My.Settings.RendererFontFamily
            .GaugeRimColor = My.Settings.GaugeRimColor
            .GaugeCenterColor = My.Settings.GaugeCenterColor
            .GaugeScaleColor = My.Settings.GaugeScaleColor
            .GaugeCenterFillColor = My.Settings.GaugeCenterFillColor
            .GaugePointerColor = My.Settings.GaugePointerColor
            .GaugeUomColor = My.Settings.GaugeUOMColor
            .GaugeMarkColor = My.Settings.GaugeMarkColor
            .GaugeBackgroundColor = My.Settings.GaugeBackgroundColor
            .GaugeBackgroundColorChange = My.Settings.GaugeBackgroundColorChange
            .SliderKnobColor = My.Settings.SliderKnobColor
            .SliderKnobShadowColor = My.Settings.SliderKnobShadowColor
            .SliderKnobHighlightColor = My.Settings.SliderKnobHighlightColor
            .SliderSlotBrightColor = My.Settings.SliderSlotBrightColor
            .SliderSlotDarkColor = My.Settings.SliderSlotDarkColor
            .SliderBackgroundColor = My.Settings.SliderBackgroundColor
            .ArcGaugeBrightColor = My.Settings.ArcGaugeBrightColor
            .ArcGaugeDarkColor = My.Settings.ArcGaugeDarkColor
            .ArcGaugeInnerArcRatio = My.Settings.ArcGaugeInnerArcRatio
            .ArcGaugeAngleFromVert = My.Settings.ArcGaugeAngleFromVert
            .ArcGaugeBackgroundColor = My.Settings.ArcGaugeBackgroundColor
        End With
        With ScopeLibrary.Settings.GetInstance
            .DefaultDatafilesLocation = My.Settings.DefaultDatafilesLocation
            .DatafilesEpoch = My.Settings.DatafilesEpoch
            .SaguaroRAColumn = My.Settings.SaguaroRAColumn
            .SaguaroDecColumn = My.Settings.SaguaroDecColumn
            .SaguaroObjectNameColumns = My.Settings.SaguaroObjectNameColumns
            .SaguaroEpoch = My.Settings.SaguaroEpoch
            .ScopePilotGroundPlane = My.Settings.ScopePilotGroundPlane
            .ScopePilotBackgroundPlotPen = My.Settings.ScopePilotBackgroundPlotPen
            .ScopePilotForegroundPlotPen = My.Settings.ScopePilotForegroundPlotPen
            .ScopePilotClickToColor = My.Settings.ScopePilotClickToColor
            .ScopePilotRulerColor = My.Settings.ScopePilotRulerColor
            .ScopePilotBackgroundColor = My.Settings.ScopePilotBackgroundColor
            .ScopePilotGlobeBackgroundColor = My.Settings.ScopePilotGlobeBackgroundColor
            .ScopePilotSiteRendererBackgroundColor = My.Settings.ScopePilotSiteRendererBackgroundColor
            .ScopePilotSiteRendererForegroundColor = My.Settings.ScopePilotSiteRendererForegroundColor
            .ScopePilotCelestialRendererBackgroundColor = My.Settings.ScopePilotCelestialRendererBackgroundColor
            .ScopePilotCelestialRendererForegroundColor = My.Settings.ScopePilotCelestialRendererForegroundColor
            .ScopePilotAltazRendererBackgroundColor = My.Settings.ScopePilotAltazRendererBackgroundColor
            .ScopePilotAltazRendererForegroundColor = My.Settings.ScopePilotAltazRendererForegroundColor
            .ScopePilotGreatCircleResolutionDeg = My.Settings.ScopePilotGreatCircleResolutionDeg
            .ScopePilotGridLinesPerQuadrant = My.Settings.ScopePilotGridLinesPerQuadrant
            .ScopePilotAnalysisStepResolutionDeg = My.Settings.ScopePilotAnalysisStepResolutionDeg
            .ScopePilotAnalysisSpotRadius = My.Settings.ScopePilotAnalysisSpotRadius
        End With
    End Sub

    Protected Overrides Sub work()

    End Sub
#End Region

End Class
