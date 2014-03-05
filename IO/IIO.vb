
''' -----------------------------------------------------------------------------
''' <summary>
''' Interface for all IO, where implementations include SerialPortFacade, TCPserverFacade, and TCPclientFacade.
''' Settings are stored in and retrieved from a ISettingsFacade.  This allows settings to be manipulated
''' in an independent manner.
''' PortName is used as the unique IO channel identifier.  Trace files use PortName.
''' SettingsName is the key name to retrieve and save settings from the configuration.
''' LoadSettings attempts to load from configuration.  If not found, programmed default values are used.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	9/8/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Interface IIO
    ReadOnly Property IOType() As ISFT
    ReadOnly Property PortName() As String

    ReadOnly Property IOSettingsFacadeTemplate() As SettingsFacadeTemplate
    Property SettingsName() As String
    Sub LoadSettings()
    Sub SaveSettings()

    Property ReceiveObservers() As IObservable
    Property TransmitObservers() As IObservable
    Property StatusObservers() As IObservable
    Property IOobservers() As IOobservers
    Property ReceiveInspector() As ReceiveInspector

    ReadOnly Property isOpened() As Boolean
    Function Open() As Boolean
    Sub Close()
    Sub Shutdown()

    Function Send(ByVal [byte] As Byte) As Boolean
    Function Send(ByVal bytes() As Byte) As Boolean
    Function Send(ByVal [string] As String) As Boolean
    Function Send(ByRef fs As System.IO.FileStream) As Boolean

    Sub QueueReceiveBytes(ByVal bytes() As Byte)

    Property IOLoggingFacade() As IOLoggingFacade
End Interface
