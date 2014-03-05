#Region "Imports"
#End Region

Public Class UserCtrlTerminalPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IObserver, IUserCtrlTerminalPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ObserveNewPortBuilt As ObservableImp
#End Region

#Region "protected and Protected Members"
    Protected pIIOPresenter As IIOPresenter
    Protected WithEvents pUserCtrlTerminal As UserCtrlTerminal
    Protected WithEvents pSettingsPresenter As SettingsPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'protected Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlTerminalPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'protected Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlTerminalPresenter = New UserCtrlTerminalPresenter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        ObserveNewPortBuilt = ObservableImp.GetInstance
    End Sub

    Public Shared Function GetInstance() As UserCtrlTerminalPresenter
        Return New UserCtrlTerminalPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IIO() As IIO
        Get
            Return IIOPresenter.IIO
        End Get
        Set(ByVal Value As IIO)
            IIOPresenter.IIO = Value
        End Set
    End Property

    Public Property IIOPresenter() As IIOPresenter Implements IUserCtrlTerminalPresenter.IIOPresenter
        Get
            Return pIIOPresenter
        End Get
        Set(ByVal value As IIOPresenter)
            pIIOPresenter = value
        End Set
    End Property

    Public Function AppendText(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        pUserCtrlTerminal.AppendText([object])
    End Function

    Public Sub SetOpenCloseState(ByVal state As Boolean)
        pUserCtrlTerminal.SetOpenCloseState(state)
    End Sub

    Public Sub SetPortType(ByVal portType As ISFT)
        pUserCtrlTerminal.SetPortType(portType)
    End Sub

    Public Sub CloseForm()
        IIOPresenter.ShutdownIO()
        shutdownLogging()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlTerminal = CType(IMVPUserCtrl, UserCtrlTerminal)
        AddHandler pUserCtrlTerminal.OpenClose, AddressOf toggleOpenClose
        AddHandler pUserCtrlTerminal.PortType, AddressOf portType
        AddHandler pUserCtrlTerminal.SendText, AddressOf sendText
        AddHandler pUserCtrlTerminal.SendByteCodes, AddressOf sendByteCodes
        AddHandler pUserCtrlTerminal.DisplayAsHex, AddressOf displayAsHex
        AddHandler pUserCtrlTerminal.Settings, AddressOf settings
        AddHandler pUserCtrlTerminal.Logging, AddressOf logging

        pUserCtrlTerminal.FireEvents = False
        pUserCtrlTerminal.SetToolTip()
        pUserCtrlTerminal.PortTypeDataSource = IOType.ISFT.DataSource
        pUserCtrlTerminal.DisplayTypeDataSource = DisplayType.ISFT.DataSource
        SetOpenCloseState(False)
        pUserCtrlTerminal.FireEvents = True
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

#Region "IO"
    Protected Sub toggleOpenClose()
        If Not checkIIO() Then
            Exit Sub
        End If
        If IIO.isOpened Then
            IIO.Close()
        Else
            IIO.Open()
        End If

        SetOpenCloseState(IIO.isOpened)
    End Sub

    Protected Sub portType(ByVal portDesc As String)
        If IIO IsNot Nothing Then
            If IIO.isOpened Then
                IIO.Close()
            End If
            shutdownLogging()
        End If

        buildIO(BartelsLibrary.IOType.ISFT.MatchString(portDesc))

        ObserveNewPortBuilt.Notify(BartelsLibrary.Constants.NewPortBuilt)
    End Sub

    ' the key method
    Protected Sub buildIO(ByRef ioType As ISFT)
        'detach
        If IIOPresenter.IIO IsNot Nothing Then
            IIOPresenter.IIO.IOobservers.ProcessMsgObservers.Detach(Me)
        End If
        'build
        IIOPresenter.BuildIO(ioType)
        'attach
        If IIOPresenter.IIO IsNot Nothing Then
            IIOPresenter.IIO.IOobservers.ProcessMsgObservers.Attach(Me)
        End If
        'displays
        buildSettingsPresenter(ioType)
        pUserCtrlTerminal.SetPortType(ioType)
        If IIOPresenter.IIO IsNot Nothing Then
            setDisplayLoggingFilename()
        End If
    End Sub

    Protected Sub buildSettingsPresenter(ByRef ioType As ISFT)
        ' xref IOType to SettingsType
        Dim settingsType As ISFT

        If ioType Is BartelsLibrary.IOType.SerialPort Then
            settingsType = BartelsLibrary.SettingsType.SerialPort
        ElseIf ioType Is BartelsLibrary.IOType.TCPclient OrElse ioType Is BartelsLibrary.IOType.TCPserver Then
            settingsType = BartelsLibrary.SettingsType.IP
        ElseIf ioType Is BartelsLibrary.IOType.NotSet Then
            Exit Sub
        Else
            Throw New Exception("Unhandled IOType of " & ioType.Name & " in IOTerminalPresenter.buildSettingsPresenter().")
        End If

        pSettingsPresenter = SettingsPresenterBuilder.GetInstance.Build(settingsType)
        AddHandler pSettingsPresenter.SettingsUpdated, AddressOf settingsUpdated
    End Sub

    Protected Sub sendText(ByVal text As String)
        IIO.Send(text)
    End Sub

    Protected Sub sendByteCodes(ByVal bytes As Byte())
        IIO.Send(bytes)
    End Sub

    Protected Sub displayAsHex(ByVal typeToDisplay As Object)
        IIO.IOobservers.DisplayAsHex = DisplayType.Hex.Description.Equals(typeToDisplay)
    End Sub

    Protected Sub settings()
        If Not checkIIO() Then
            Exit Sub
        End If
        pSettingsPresenter.SettingsFacadeTemplate.ISettings = IIO.IOSettingsFacadeTemplate.ISettings
        pSettingsPresenter.ShowDialog()
    End Sub

    Protected Sub settingsUpdated()
        IIO.IOSettingsFacadeTemplate.ISettings = pSettingsPresenter.SettingsFacadeTemplate.ISettings
        updateLoggingSettings()
    End Sub

    Protected Function checkIIO() As Boolean
        If IIO Is Nothing Then
            AppMsgBox.Show("IO not set.")
            Return False
        End If
        Return True
    End Function
#End Region

#Region "logging"
    Protected Sub logging(ByVal switch As Boolean)
        If switch Then
            If IIO Is Nothing Then
                AppMsgBox.Show("IO Port not yet set.")
                pUserCtrlTerminal.UserCtrlLogging.ChangeChBxLogging(False)
                Exit Sub
            End If
            setLoggingObserverFilename()
            IIO.IOLoggingFacade.Open()
            setDisplayLoggingFilename()
        Else
            IIO.IOLoggingFacade.Close()
        End If
    End Sub

    Protected Sub shutdownLogging()
        If IIO IsNot Nothing AndAlso IIO.IOLoggingFacade.LoggingObserver.IsOpen Then
            AppMsgBox.Show("Deactivating IO logging.")
            IIO.IOLoggingFacade.Close()
            pUserCtrlTerminal.UserCtrlLogging.LoggingFilename = String.Empty
            pUserCtrlTerminal.UserCtrlLogging.ChangeChBxLogging(False)
        End If
    End Sub

    Protected Sub setLoggingObserverFilename()
        IIO.IOLoggingFacade.LoggingObserver.Filename = pUserCtrlTerminal.UserCtrlLogging.LoggingFilename
    End Sub

    Protected Sub setDisplayLoggingFilename()
        pUserCtrlTerminal.UserCtrlLogging.LoggingFilename = IIO.IOLoggingFacade.LoggingObserver.Filename
    End Sub

    Protected Sub updateLoggingSettings()
        IIO.IOLoggingFacade.SetIOLoggingFilenameToPortname()
        setDisplayLoggingFilename()
    End Sub
#End Region

#End Region

End Class
