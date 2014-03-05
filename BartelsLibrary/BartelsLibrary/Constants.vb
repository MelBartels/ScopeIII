Public Class Constants

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const BadExit As Int32 = -1
    Public Const NormalExit As Int32 = 0

    Public Const TCPBufferSize As Integer = 1024

    Public Const DefaultIPAddress As String = "127.0.0.1"
    Public Const DefaultIPPort As Integer = 2000

    Public Const DefaultBaudRate As Integer = 9600
    Public Const DefaultParity As System.IO.Ports.Parity = System.IO.Ports.Parity.None
    Public Const DefaultDataBits As Integer = 8
    Public Const DefaultStopBits As Integer = 0
    Public Const DefaultHandshake As System.IO.Ports.Handshake = System.IO.Ports.Handshake.None
    Public Const DefaultUseDTR As Boolean = False
    Public Const DefaultUseRTS As Boolean = False
    Public Const DefaultReadTimeout As Integer = -1
    Public Const DefaultWriteTimeout As Integer = -1

    Public Const DefaultLoggingActive As Boolean = False

    Public Const Delimiter As String = " "
    Public Const DatafilesExtension As String = "dat"

    Public Const LogSubdir As String = ".\log\"
    Public Const LogExtension As String = ".log"

    Public Const Plus As String = "+"
    Public Const Minus As String = "-"

    Public Const GaugeToolTip As String = "Click to point or drag pointer with mouse."

    Public Const ErrorMessage As String = "An error has been caught.  Details will be appended to the trace.log file."
    Public Const TopLevelExceptionMsg As String = "An error has occured.  The application is quitting."

    Public Const HierarchicalIncrement As String = "   "

    Public Const CfgSection As String = "Settings"
    Public Const SettingsUpdated As String = "SettingsUpdated"
    Public Const LoadSettingsFromConfigFailed As String = "Could not load settings from the configuration.  Adopting default values."

    Public Const NewPortBuilt As String = "New port built."
    Public Const OpenPort As String = "Open Port"
    Public Const ClosePort As String = "Close Port"
    Public Const OpeningPort As String = "Opening port"
    Public Const OpenPortFailure As String = "Port could not be opened."
    Public Const PortClosed As String = "Port closed."

    Public Const BadIPAddressFormat As String = "Bad IP address format.  IP address must take the form of xxx.xxx.xxx.xxx where xxx is a number."
    Public Const TransmitFailure As String = "Failure transmitting data."

    Public Const SerialPort As String = " Serial Port"
    Public Const IP As String = " IP"
    Public Const Logging As String = " Logging"

    Public Const UnknownSelectedAlignment As String = "Unknown cmbBoxAlignment.SelectedItem."

    Public Const FormLoaded As String = "FormLoaded"
    Public Const NothingEntered As String = "Nothing entered"

    Public Shared DeltaChar As String = Char.ConvertFromUtf32(&H394)

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

    'Public Shared Function GetInstance() As eString
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As eString = New eString
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Constants
        Return New Constants
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class