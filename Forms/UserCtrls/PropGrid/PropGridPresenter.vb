#Region "Imports"
#End Region

Public Class PropGridPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IPropGridPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event SettingsOKd() Implements IPropGridPresenter.SettingsOKd
    Public Event SettingsAccepted() Implements IPropGridPresenter.SettingsAccepted
    Public Event CancelEvent() Implements IPropGridPresenter.CancelEvent
#End Region

#Region "Private and Protected Members"
    Protected pSettingsFacadeClone As SettingsFacadeTemplate
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PropGridPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PropGridPresenter = New PropGridPresenter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As PropGridPresenter
        Return New PropGridPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property SettingsFacadeTemplate() As SettingsFacadeTemplate Implements IPropGridPresenter.SettingsFacadeTemplate
        Get
            Return CType(DataModel, SettingsFacadeTemplate)
        End Get
        Set(ByVal Value As SettingsFacadeTemplate)
            DataModel = Value
        End Set
    End Property

    ' set clone to work with independent set of settings, else settings changes made here on the fly change settings in use elsewhere;
    ' settings parms are cloned in ISettings;
    Public Sub CloneSettingsFacadeTemplate() Implements IPropGridPresenter.CloneSettingsFacadeTemplate
        pSettingsFacadeClone = CType(SettingsFacadeTemplate.Clone, SettingsFacadeTemplate)
        'if settings have not been fully created, then the clone will need to create settings using the defaults
        'pSettingsFacadeClone.CloneISettings()
    End Sub

    Public Sub SetPropGridSelectedObject() Implements IPropGridPresenter.SetPropGridSelectedObject
        userCtrlPropGrid.SelectedObject = CType(pSettingsFacadeClone.ISettings, ISettingsToPropGridAdapter).PropGridSelectedObject
    End Sub

    Public Sub UpdateDisplayPropertiesFromSettings() Implements IPropGridPresenter.UpdateDisplayPropertiesFromSettings
        SettingsFacadeTemplate.ISettings.CopyPropertiesTo(pSettingsFacadeClone.ISettings)
        userCtrlPropGrid.SelectedObject = Nothing
        SetPropGridSelectedObject()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        userCtrlPropGrid.SetToolTip()

        AddHandler userCtrlPropGrid.DefaultClick, AddressOf defaultSettings
        AddHandler userCtrlPropGrid.LoadClick, AddressOf loadSettings
        AddHandler userCtrlPropGrid.SaveClick, AddressOf saveSettings
        AddHandler userCtrlPropGrid.OKClick, AddressOf ok
        AddHandler userCtrlPropGrid.CancelClick, AddressOf cancel
        AddHandler userCtrlPropGrid.AcceptClick, AddressOf acceptSettings
        AddHandler userCtrlPropGrid.PropertyValueChanged, AddressOf SetPropGridSelectedObject
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function userCtrlPropGrid() As UserCtrlPropGrid
        Return CType(IMVPUserCtrl, UserCtrlPropGrid)
    End Function

    Protected Sub defaultSettings()
        pSettingsFacadeClone.SetToDefaultSettings()
        SetPropGridSelectedObject()
    End Sub

    Protected Sub loadSettings()
        pSettingsFacadeClone.LoadSettingsFromConfig()
        SetPropGridSelectedObject()
    End Sub

    Protected Sub saveSettings()
        pSettingsFacadeClone.SaveSettingsToConfig()
    End Sub

    Protected Sub ok()
        updateSettings()
        RaiseEvent SettingsOKd()
    End Sub

    Private Sub acceptSettings()
        updateSettings()
        RaiseEvent SettingsAccepted()
    End Sub

    Protected Sub cancel()
        cancelSettings()
        RaiseEvent CancelEvent()
    End Sub

    Private Sub updateSettings()
        ' update underlying properties from display (cloned) properties
        pSettingsFacadeClone.ISettings.CopyPropertiesTo(SettingsFacadeTemplate.ISettings)
    End Sub

    Private Sub cancelSettings()
        ' return display properties to starting properties
        UpdateDisplayPropertiesFromSettings()
    End Sub
#End Region

End Class
