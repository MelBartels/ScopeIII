Public Class Settings

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public DefaultDatafilesLocation As String = "..\..\data files epoch 2000"
    Public DatafilesEpoch As String = "2000"

    Public SaguaroRAColumn As Integer = 5
    Public SaguaroDecColumn As Integer = 6
    Public SaguaroObjectNameColumns As String = "1,3,7,11,18,19"
    Public SaguaroEpoch As Integer = 2000

    Public ScopePilotGroundPlane As Drawing.Color = Drawing.Color.Gray
    Public ScopePilotBackgroundPlotPen As Drawing.Color = Drawing.Color.LightGray
    Public ScopePilotForegroundPlotPen As Drawing.Color = Drawing.Color.Black
    Public ScopePilotClickToColor As Drawing.Color = Drawing.Color.Black
    Public ScopePilotRulerColor As Drawing.Color = Drawing.Color.Black
    Public ScopePilotBackgroundColor As Drawing.Color = Drawing.Color.RoyalBlue
    Public ScopePilotGlobeBackgroundColor As Drawing.Color = Drawing.Color.LightGray
    Public ScopePilotSiteRendererBackgroundColor As Drawing.Color = Drawing.Color.LightBlue
    Public ScopePilotSiteRendererForegroundColor As Drawing.Color = Drawing.Color.Black
    Public ScopePilotCelestialRendererBackgroundColor As Drawing.Color = Drawing.Color.LightSkyBlue
    Public ScopePilotCelestialRendererForegroundColor As Drawing.Color = Drawing.Color.RoyalBlue
    Public ScopePilotAltazRendererBackgroundColor As Drawing.Color = Drawing.Color.Pink
    Public ScopePilotAltazRendererForegroundColor As Drawing.Color = Drawing.Color.Red
    Public ScopePilotGreatCircleResolutionDeg As Double = 3
    Public ScopePilotGridLinesPerQuadrant As Double = 3
    Public ScopePilotAnalysisStepResolutionDeg As Double = 3
    Public ScopePilotAnalysisSpotRadius As Int32 = 3
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Settings
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As Settings = New Settings
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Settings
    '    Return New Settings
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class