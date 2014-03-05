Public Interface ISettingsPresenterBuilder
    Function Build(ByRef settingsType As ISFT, Optional ByVal title As String = Nothing) As SettingsPresenter
    Function Build(ByRef settingsFacadeTemplate As SettingsFacadeTemplate, Optional ByVal title As String = Nothing) As SettingsPresenter
End Interface
