#Region "imports"
Imports System.Threading
#End Region

Public Class EncoderPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmEncoder As FrmEncoder
    Private pIUserCtrlEncoderPresenter As IUserCtrlEncoderPresenter
    Private pSettingsPresenter As SettingsPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EncoderPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EncoderPresenter = New EncoderPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As EncoderPresenter
        Return New EncoderPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Title() As String
        Get
            Return pFrmEncoder.Title
        End Get
        Set(ByVal Value As String)
            pFrmEncoder.Title = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEncoder = CType(IMVPView, FrmEncoder)
        AddHandler pFrmEncoder.Properties, AddressOf propertiesHandler

        pIUserCtrlEncoderPresenter = FormsDependencyInjector.GetInstance.IUserCtrlEncoderPresenterFactory()
        pIUserCtrlEncoderPresenter.IMVPUserCtrl = pFrmEncoder.UserCtrlEncoder

        pSettingsPresenter = SettingsPresenterBuilder.GetInstance.Build(CType(SettingsType.NamedDevice, ISFT), ScopeLibrary.Constants.TestEncoder)
        AddHandler pSettingsPresenter.SettingsUpdated, AddressOf settingsUpdated

        ' clone so as to not operate on the settings value until user oks
        pIUserCtrlEncoderPresenter.BuildIRenderer(CType(getSettingsEncoderValue.Clone, EncoderValue))
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub propertiesHandler()
        Dim showPropertiesThread As New Thread(AddressOf showProperties)
        showPropertiesThread.Name = Me.GetType.Name & ".showProperties"
        showPropertiesThread.Start()
    End Sub

    Private Sub showProperties()
        updateSettingsEncoderValue()
        pSettingsPresenter.IPropGridPresenter.CloneSettingsFacadeTemplate()
        pSettingsPresenter.IPropGridPresenter.SetPropGridSelectedObject()
        pSettingsPresenter.ShowDialog()
    End Sub

    Private Sub settingsUpdated()
        getSettingsEncoderValue.CopyPropertiesTo(pIUserCtrlEncoderPresenter.EncoderValue)
    End Sub

    Private Function getSettingsEncoderValue() As EncoderValue
        Dim devicesSettings As DevicesSettings = CType(pSettingsPresenter.SettingsFacadeTemplate.ISettings, DevicesSettings)
        Dim encoder As Devices.Encoder = CType(devicesSettings.DevicesPropContainer.IDevices(0), Devices.Encoder)
        Return CType(encoder.GetProperty(GetType(EncoderValue).Name), Devices.EncoderValue)
    End Function

    Private Sub updateSettingsEncoderValue()
        pIUserCtrlEncoderPresenter.EncoderValue.CopyPropertiesTo(getSettingsEncoderValue)
    End Sub
#End Region

End Class
