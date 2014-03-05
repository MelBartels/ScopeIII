#Region "Imports"
#End Region

Public Class SettingsPresenterBuilder
    Implements ISettingsPresenterBuilder

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SettingsPresenterBuilder
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SettingsPresenterBuilder = New SettingsPresenterBuilder
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SettingsPresenterBuilder
        Return New SettingsPresenterBuilder
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Build(ByRef settingsType As ISFT, Optional ByVal title As String = Nothing) As SettingsPresenter Implements ISettingsPresenterBuilder.Build
        If settingsType Is BartelsLibrary.SettingsType.SerialPort Then
            Return Build(SettingsFacadeTemplate.GetInstance.Build(SerialSettings.GetInstance), title)

        ElseIf settingsType Is BartelsLibrary.SettingsType.IP Then
            Return Build(SettingsFacadeTemplate.GetInstance.Build(IPsettings.GetInstance), title)

        ElseIf settingsType Is BartelsLibrary.SettingsType.Devices Then
            Return Build(SettingsFacadeTemplate.GetInstance.Build(DevicesSettings.GetInstance), title)

        ElseIf settingsType Is BartelsLibrary.SettingsType.NamedDevice Then
            Return Build(SettingsFacadeTemplate.GetInstance.Build(DevicesSettings.GetInstance, title), title)

        Else
            Throw New Exception("Unhandled SettingsType of " & settingsType.Name & " in SettingsPresenterBuilder.Build().")
        End If
    End Function

    Public Function Build(ByRef settingsFacadeTemplate As SettingsFacadeTemplate, Optional ByVal title As String = Nothing) As SettingsPresenter Implements ISettingsPresenterBuilder.Build
        Dim settingsPresenter As SettingsPresenter = Forms.SettingsPresenter.GetInstance
        With settingsPresenter
            .IMVPView = CType(FormsDependencyInjector.GetInstance.IFrmShowSettingsFactory, IMVPView)
            If String.IsNullOrEmpty(title) Then
                .Title += " " & settingsFacadeTemplate.ISettings.Name
            Else
                .Title += title
            End If
            .SettingsFacadeTemplate = settingsFacadeTemplate
            setToDefaultsIfNecessary(.SettingsFacadeTemplate.ISettings)
            .CloneSettingsFacadeTemplate()
            .SetPropGridSelectedObject()

            Return settingsPresenter
        End With
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub setToDefaultsIfNecessary(ByVal ISettings As ISettings)
        If Not ISettings.PropertiesSet Then
            ISettings.SetToDefaults()
        End If
    End Sub
#End Region

End Class
