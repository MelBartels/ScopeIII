#Region "imports"
Imports System.Threading
#End Region

Public MustInherit Class EncodersBoxPresenterBase
    Inherits MVPPresenterBase
    Implements IObserver

#Region "Inner Classes"
    Protected Class ObserveNewPortBuiltObserver : Implements IObserver
        Protected processMsgDelegate As [Delegate]
        Protected Sub New()
        End Sub
        Public Shared Function GetInstance() As ObserveNewPortBuiltObserver
            Return New ObserveNewPortBuiltObserver
        End Function
        Public Sub RegisterDelegate(ByVal processMsgDelegate As [Delegate])
            Me.processMsgDelegate = processMsgDelegate
        End Sub
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            processMsgDelegate.DynamicInvoke()
        End Function
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "protected and Protected Members"
    Protected pObserveNewPortBuiltObserver As ObserveNewPortBuiltObserver
    Protected WithEvents pUserCtrlTerminalPresenter As UserCtrlTerminalPresenter
    Protected WithEvents pIFrmEncodersBox As IFrmEncodersBox
    Protected pIUserCtrlEncoderPriPresenter As IUserCtrlEncoderPresenter
    Protected pIUserCtrlEncoderSecPresenter As IUserCtrlEncoderPresenter
    Protected pSettingsPresenter As SettingsPresenter
    Protected pDeviceObservers As DeviceObservers
    Protected pLoggingObserver As LoggingObserver
#End Region

#Region "Constructors (Singleton Pattern)"
    'protected Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EncodersBoxPresenterBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'protected Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EncodersBoxPresenterBase = New EncodersBoxPresenterBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As EncodersBoxPresenterBase
    '    Return New EncodersBoxPresenterBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Title() As String
        Get
            Return pIFrmEncodersBox.Title
        End Get
        Set(ByVal Value As String)
            pIFrmEncodersBox.Title = Value
        End Set
    End Property

    Public Overridable Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        pIFrmEncodersBox.DisplayStatus(CStr([object]))
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pIFrmEncodersBox = CType(IMVPView, IFrmEncodersBox)
        pIFrmEncodersBox.SetToolTip()
        AddHandler pIFrmEncodersBox.Properties, AddressOf propertiesHandler
        AddHandler pIFrmEncodersBox.DisplayDevice, AddressOf displayDeviceHandler
        AddHandler pIFrmEncodersBox.CloseForm, AddressOf closeForm
        AddHandler pIFrmEncodersBox.Logging, AddressOf logging

        pIUserCtrlEncoderPriPresenter = FormsDependencyInjector.GetInstance.IUserCtrlEncoderPresenterFactory
        pIUserCtrlEncoderPriPresenter.IMVPUserCtrl = pIFrmEncodersBox.GetUserCtrlEncoderPri
        pIUserCtrlEncoderSecPresenter = FormsDependencyInjector.GetInstance.IUserCtrlEncoderPresenterFactory
        pIUserCtrlEncoderSecPresenter.IMVPUserCtrl = pIFrmEncodersBox.GetUserCtrlEncoderSec

        ' create EncodersBox and SettingsPresenter
        pSettingsPresenter = SettingsPresenterBuilder.GetInstance.Build(CType(SettingsType.NamedDevice, ISFT), DeviceName.EncodersBox.Name)
        AddHandler pSettingsPresenter.SettingsUpdated, AddressOf settingsUpdated
        ' SettingsPresenter must already be instantiated; 
        ' avoid cloning so that when encoder gauge is updated device encoder value is also updated, ie, not getSettingsEncoderValuePri.Clone
        pIUserCtrlEncoderPriPresenter.BuildIRenderer(CType(getSettingsEncoderValuePri(), EncoderValue))
        pIUserCtrlEncoderSecPresenter.BuildIRenderer(CType(getSettingsEncoderValueSec(), EncoderValue))

        pUserCtrlTerminalPresenter = CType(UserCtrlTerminalPresenterFactory.GetInstance.Build _
            (IOPresenterDevice.GetInstance, pIFrmEncodersBox.GetUserCtrlTerminal), UserCtrlTerminalPresenter)

        pDeviceObservers = DeviceObservers.GetInstance
        pDeviceObservers.AddObserversWithIDToDevice(getEncodersBox, Me.GetType.Name, Me)
        initLogging()

        buildNewPortBuiltObserver()
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Protected Overridable Sub closeForm()
        shutdownLogging()
        pUserCtrlTerminalPresenter.CloseForm()
    End Sub

#Region "Device Properties"
    Protected Sub propertiesHandler()
        Dim showPropertiesThread As New Thread(AddressOf showProperties)
        showPropertiesThread.Name = Me.GetType.Name & ".showProperties"
        showPropertiesThread.Start()
    End Sub

    Protected Sub showProperties()
        updateSettingsEncoderValue()

        pSettingsPresenter.IPropGridPresenter.CloneSettingsFacadeTemplate()
        pSettingsPresenter.IPropGridPresenter.SetPropGridSelectedObject()
        pSettingsPresenter.ShowDialog()
    End Sub

    Protected Sub displayDeviceHandler()
        Dim displayDeviceThread As New Thread(AddressOf displayDevice)
        displayDeviceThread.Name = Me.GetType.Name & ".displayDevice"
        displayDeviceThread.Start()
    End Sub

    Protected Sub displayDevice()
        Dim frmTreeView As New FrmTreeView
        Dim DeviceToHierarchicalAdapter As DeviceToHierarchicalAdapter = ScopeIII.Devices.DeviceToHierarchicalAdapter.GetInstance
        DeviceToHierarchicalAdapter.RegisterComponent(CObj(frmTreeView.TreeView))
        DeviceToHierarchicalAdapter.Adapt(getEncodersBox)
        frmTreeView.TreeView.ExpandAll()
        frmTreeView.ShowDialog()
    End Sub

    Protected Overridable Sub settingsUpdated()
        getSettingsEncoderValuePri.CopyPropertiesTo(pIUserCtrlEncoderPriPresenter.EncoderValue)
        getSettingsEncoderValueSec.CopyPropertiesTo(pIUserCtrlEncoderSecPresenter.EncoderValue)
        updateTerminalIOFromDevice()
    End Sub

    Protected Function getDevicesSettings() As DevicesSettings
        Return CType(pSettingsPresenter.SettingsFacadeTemplate.ISettings, DevicesSettings)
    End Function

    Protected Function getEncodersBox() As EncodersBox
        Return CType(getDevicesSettings.DevicesPropContainer.IDevices(0), Devices.EncodersBox)
    End Function

    Protected Function getSettingsEncoderValuePri() As EncoderValue
        Dim tc As EncodersBox = getEncodersBox()
        Dim encoder As IDevice = CType(tc.GetDevicesByName(tc.PriAxisName).Item(0), IDevice)
        Return CType(encoder.GetProperty(GetType(EncoderValue).Name), Devices.EncoderValue)
    End Function

    Protected Function getSettingsEncoderValueSec() As EncoderValue
        Dim tc As EncodersBox = getEncodersBox()
        Dim encoder As IDevice = CType(tc.GetDevicesByName(tc.SecAxisName).Item(0), IDevice)
        Return CType(encoder.GetProperty(GetType(EncoderValue).Name), Devices.EncoderValue)
    End Function

    Protected Function getDeviceIOType() As String
        Return CType(getEncodersBox.GetProperty(GetType(DevPropIOType).Name), DevPropIOType).IOType
    End Function

    Protected Sub updateSettingsEncoderValue()
        getSettingsEncoderValuePri.Value = pIUserCtrlEncoderPriPresenter.EncoderValue.Value
        getSettingsEncoderValueSec.Value = pIUserCtrlEncoderSecPresenter.EncoderValue.Value
    End Sub
#End Region

#Region "IO"
    Protected Sub buildNewPortBuiltObserver()
        pObserveNewPortBuiltObserver = ObserveNewPortBuiltObserver.GetInstance
        pObserveNewPortBuiltObserver.RegisterDelegate(New BartelsLibrary.DelegateSigs.DelegateNone(AddressOf newPortBuilt))
        pUserCtrlTerminalPresenter.ObserveNewPortBuilt.Attach(CType(pObserveNewPortBuiltObserver, IObserver))
    End Sub

    Protected Sub updateTerminalIOFromDevice()
        pUserCtrlTerminalPresenter.SetPortType(IOType.ISFT.MatchString(getDeviceIOType))
    End Sub

    Protected Sub setTermPortTypeToDeviceType()
        pUserCtrlTerminalPresenter.SetPortType(IOType.ISFT.MatchString(getDeviceIOType))
    End Sub

    Protected MustOverride Sub newPortBuilt()

#End Region

#Region "logging"
    Protected Sub initLogging()
        pLoggingObserver = LoggingObserver.GetInstance
        pLoggingObserver.AppendCRLF = True
        setDefaultDisplayLoggingFilename()
        pDeviceObservers.AddObserversWithIDToDevice(getEncodersBox, pLoggingObserver.GetType.Name, CType(pLoggingObserver, IObserver))
    End Sub

    Protected Sub logging(ByVal switch As Boolean)
        If switch Then
            setLoggingObserverFilename()
            pLoggingObserver.Open()
            setDisplayLoggingFilename()
        Else
            pLoggingObserver.Close()
        End If
    End Sub

    Protected Sub shutdownLogging()
        If pLoggingObserver.IsOpen Then
            AppMsgBox.Show("Deactivating device logging.")
            pLoggingObserver.Close()
            pIFrmEncodersBox.GetUserCtrlLogging.LoggingFilename = String.Empty
            pIFrmEncodersBox.GetUserCtrlLogging.ChangeChBxLogging(False)
        End If
    End Sub

    Protected Sub setLoggingObserverFilename()
        pLoggingObserver.Filename = pIFrmEncodersBox.GetUserCtrlLogging.LoggingFilename
    End Sub

    Protected Sub setDisplayLoggingFilename()
        pIFrmEncodersBox.GetUserCtrlLogging.LoggingFilename = pLoggingObserver.Filename
    End Sub

    Protected Sub setDefaultDisplayLoggingFilename()
        Dim sb As New Text.StringBuilder
        sb.Append(BartelsLibrary.Constants.LogSubdir)
        sb.Append(getEncodersBox.GetProperty(GetType(DevPropName).Name).Value)
        sb.Append(BartelsLibrary.Constants.LogExtension)
        pIFrmEncodersBox.GetUserCtrlLogging.LoggingFilename = sb.ToString
    End Sub
#End Region

#End Region

End Class
