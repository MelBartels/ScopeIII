#Region "Imports"
#End Region

Public Class SettingsPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event SettingsUpdated()
#End Region

#Region "Private and Protected Members"
    Private pIPropGridPresenter As IPropGridPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SettingsPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SettingsPresenter = New SettingsPresenter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIPropGridPresenter = FormsDependencyInjector.GetInstance.IPropGridPresenterFactory
    End Sub

    Public Shared Function GetInstance() As SettingsPresenter
        Return New SettingsPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property SettingsFacadeTemplate() As SettingsFacadeTemplate
        Get
            Return IPropGridPresenter.SettingsFacadeTemplate
        End Get
        Set(ByVal Value As SettingsFacadeTemplate)
            IPropGridPresenter.SettingsFacadeTemplate = Value
        End Set
    End Property

    Public Property IPropGridPresenter() As IPropGridPresenter
        Get
            Return pIPropGridPresenter
        End Get
        Set(ByVal value As IPropGridPresenter)
            pIPropGridPresenter = value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return IFrmShowSettings.Title
        End Get
        Set(ByVal Value As String)
            IFrmShowSettings.Title = Value
        End Set
    End Property

    Public Sub CloneSettingsFacadeTemplate()
        IPropGridPresenter.CloneSettingsFacadeTemplate()
    End Sub

    Public Sub SetPropGridSelectedObject()
        IPropGridPresenter.SetPropGridSelectedObject()
    End Sub

    Public Sub UpdateDisplayPropertiesFromSettings()
        IPropGridPresenter.UpdateDisplayPropertiesFromSettings()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        IPropGridPresenter.IMVPUserCtrl = IFrmShowSettings.UserCtrlPropGrid

        AddHandler IPropGridPresenter.SettingsOKd, AddressOf settingsOKdHandler
        AddHandler IPropGridPresenter.SettingsAccepted, AddressOf settingsAcceptedHandler
        AddHandler IPropGridPresenter.CancelEvent, AddressOf cancel
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function IFrmShowSettings() As IFrmShowSettings
        Return CType(pIMVPView, IFrmShowSettings)
    End Function

    Private Sub settingsOKdHandler()
        RaiseEvent SettingsUpdated()
        Close()
    End Sub

    Private Sub settingsAcceptedHandler()
        RaiseEvent SettingsUpdated()
    End Sub

    Private Sub cancel()
        Close()
    End Sub
#End Region

End Class
