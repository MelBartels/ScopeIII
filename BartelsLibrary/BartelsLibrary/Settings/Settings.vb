Public Class Settings

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ExceptionFilename As String = "trace.log"
    Public SettingsFilename As String = "settings.xml"

    Public DefaultIPAddress As String = BartelsLibrary.Constants.DefaultIPAddress
    Public DefaultIPPort As Integer = BartelsLibrary.Constants.DefaultIPPort

    Public RendererFontFamilyName As String = "Arial"
    Public GaugeRimColor As Drawing.Color = Drawing.Color.Black
    Public GaugeCenterColor As Drawing.Color = Drawing.Color.Black
    Public GaugeScaleColor As Drawing.Color = Drawing.Color.Black
    Public GaugeCenterFillColor As Drawing.Color = Drawing.Color.Red
    Public GaugePointerColor As Drawing.Color = Drawing.Color.Red
    Public GaugeUomColor As Drawing.Color = Drawing.Color.DarkRed
    Public GaugeMarkColor As Drawing.Color = Drawing.Color.DarkRed
    Public GaugeBackgroundColor As Drawing.Color = Drawing.Color.Gray
    Public GaugeBackgroundColorChange As System.Byte = 64
    Public SliderKnobColor As Drawing.Color = Drawing.Color.Red
    Public SliderKnobShadowColor As Drawing.Color = Drawing.Color.DarkRed
    Public SliderKnobHighlightColor As Drawing.Color = Drawing.Color.Pink
    Public SliderSlotBrightColor As Drawing.Color = Drawing.Color.White
    Public SliderSlotDarkColor As Drawing.Color = Drawing.Color.Black
    Public SliderBackgroundColor As Drawing.Color = Drawing.Color.Silver
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