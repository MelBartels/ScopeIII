#Region "Imports"
#End Region

Public Class SettingsFacadeTemplate
    Implements ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pSettingsFacade As SettingsFacade
    Protected pISettings As ISettings
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SettingsFacadeTemplate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SettingsFacadeTemplate = New SettingsFacadeTemplate
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SettingsFacadeTemplate
        Return New SettingsFacadeTemplate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property SettingsFacade() As SettingsFacade
        Get
            Return pSettingsFacade
        End Get
        Set(ByVal value As SettingsFacade)
            pSettingsFacade = Value
        End Set
    End Property

    Public Property ISettings() As BartelsLibrary.ISettings
        Get
            Return pISettings
        End Get
        Set(ByVal Value As BartelsLibrary.ISettings)
            pISettings = CType(Value, ISettings)
        End Set
    End Property

    Public Function LoadSettingsFromConfig() As Boolean
        SettingsFacade.LoadConfig()

        Dim cfgISettings As ISettings = CType(SettingsFacade.GetSetting(ISettings.Name).Tag, ISettings)
        If cfgISettings Is Nothing Then
            SetToDefaultSettings()
        Else
            If String.IsNullOrEmpty(cfgISettings.Name) Then
                ExceptionService.Notify("Loaded settings has no name.")
                cfgISettings.Name = ISettings.Name
            End If
            ISettings = cfgISettings
        End If
        ' default properties are not stored in the configuration settings, so update missing properties with default values
        ISettings.CopyPropertiesTo(ISettings)

        Return True
    End Function

    Public Sub SaveSettingsToConfig()
        Dim setting As Setting = SettingsFacade.GetSetting(ISettings.Name)
        setting.Type = ISettingsTypeName()
        setting.Tag = pISettings
        SettingsFacade.SaveConfig()
    End Sub

    Public Sub SetToDefaultSettings()
        ISettings.SetToDefaults()
    End Sub

    Public Sub CloneISettings()
        ISettings = CType(ISettings.Clone, BartelsLibrary.ISettings)
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance
        settingsFacadeTemplate.SettingsFacade = SettingsFacade.GetInstance
        settingsFacadeTemplate.ISettings = CType(pISettings.Clone, ISettings)
        Return settingsFacadeTemplate
    End Function

    Public Function Build(ByRef ISettings As ISettings) As SettingsFacadeTemplate
        Return Build(ISettings, CType(ISettings, Object).GetType.Name)
    End Function

    Public Function Build(ByRef ISettings As ISettings, ByVal name As String) As SettingsFacadeTemplate
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance
        settingsFacadeTemplate.SettingsFacade = SettingsFacade.GetInstance
        settingsFacadeTemplate.ISettings = ISettings
        settingsFacadeTemplate.ISettings.Name = name
        Return settingsFacadeTemplate
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Function ISettingsTypeName() As String
        Return CType(ISettings, Object).GetType.Name
    End Function
#End Region

End Class
