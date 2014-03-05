Public Interface IPropGridPresenter
    Inherits IMVPUserCtrlPresenter

    Event SettingsOKd()
    Event SettingsAccepted()
    Event CancelEvent()

    Property SettingsFacadeTemplate() As SettingsFacadeTemplate
    Sub CloneSettingsFacadeTemplate()
    Sub SetPropGridSelectedObject()
    Sub UpdateDisplayPropertiesFromSettings()
End Interface
