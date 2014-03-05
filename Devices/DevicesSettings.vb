#Region "Imports"
#End Region

#Region "Imports"
#End Region

Public Class DevicesSettings
    Inherits SettingsBase
    Implements ISettingsToPropGridAdapter, ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pDevicesPropContainer As DevicesPropContainer
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevicesSettings
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevicesSettings = New DevicesSettings
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DevicesSettings
        Return New DevicesSettings
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property DevicesPropContainer() As DevicesPropContainer
        Get
            Return pDevicesPropContainer
        End Get
        Set(ByVal Value As DevicesPropContainer)
            pDevicesPropContainer = Value
        End Set
    End Property

    Public Overrides Sub SetToDefaults()
        ' handle names that are not in DeviceName
        If Name.Equals(ScopeLibrary.Constants.DevicesSettings) Then
            pDevicesPropContainer = DevicePropContainers.GetInstance.BuildDevicesSettingsPropContainer()
            Exit Sub
        End If
        If Name.Equals(ScopeLibrary.Constants.TestEncoder) Then
            ' SettingsFacadeTemplate.Build() executes SettingsFacadeTemplate.ISettings.Name = Name; Name is retrieved and used here
            pDevicesPropContainer = DevicePropContainers.GetInstance.BuildTestEncoderPropContainer()
            Exit Sub
        End If

        ' name should be found in DeviceName
        pDevicesPropContainer = DevicePropContainers.GetInstance.PropContainerFactory(CType(DeviceName.ISFT.MatchString(Name), SFTPrototype))
    End Sub

    Public Overrides Sub CopyPropertiesTo(ByRef ISettings As ISettings)
        Dim DevicesSettings As DevicesSettings = CType(ISettings, DevicesSettings)
        DevicesSettings.Name = Name
        DevicesSettings.DevicesPropContainer = CType(DevicesPropContainer.Clone, DevicesPropContainer)
    End Sub

    Public Overrides Function Clone() As Object Implements System.ICloneable.Clone
        Dim DevicesSettings As ISettings = ScopeIII.Devices.DevicesSettings.GetInstance
        CopyPropertiesTo(DevicesSettings)
        Return DevicesSettings
    End Function

    Public Overrides Function PropertiesSet() As Boolean
        Return pDevicesPropContainer IsNot Nothing
    End Function

    Public Function PropGridSelectedObject() As Object Implements ISettingsToPropGridAdapter.PropGridSelectedObject
        Return DevicesPropContainer.PropContainer
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
