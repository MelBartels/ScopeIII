#Region "Imports"
Imports System.IO
Imports System.ComponentModel

#End Region

<DefaultPropertyAttribute(SerialSettings.ClassDefaultProp), _
DescriptionAttribute(SerialSettings.ClassDescAttr)> _
Public Class SerialSettings
    Inherits SettingsBase
    Implements ISettingsToPropGridAdapter, ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ClassDefaultProp As String = "Port"
    Public Const ClassDescAttr As String = "Serial Port Settings"
    ' space to start so that Basic Settings floats to the top of the categories
    Public Const BasicSerialPortSettingsDesc As String = " Basic Settings"
    Public Const AdvancedSerialSettingsDesc As String = "Advanced Settings"
    Public Const PortnameDesc As String = "The communications port. The default is COM1."
    Public Const BaudRateDesc As String = "Baud rate."
    Public Const DataBitsDesc As String = "Number of data bits to describe the character.  The range of values for this property is from 5 through 8. The default value is 8."
    Public Const ParityDesc As String = _
        "One of the Parity values that represents the parity-checking protocol. The default is None." _
        & vbCrLf _
        & "Even: Sets the parity bit so that the count of bits set is an even number." _
        & vbCrLf _
        & "Mark: Leaves the parity bit set to 1." _
        & vbCrLf _
        & "None: No parity check occurs." _
        & vbCrLf _
        & "Odd: Sets the parity bit so that the count of bits set is an odd number." _
        & vbCrLf _
        & "Space: Leaves the parity bit set to 0."
    Public Const StopBitsDesc As String = "Line is asserted for this number of bits at the end of each character."
    Public Const HandshakeDesc As String = _
        "One of the Handshake values. The default is None." _
        & vbCrLf _
        & "None: No control is used for the handshake." _
        & vbCrLf _
        & "RequestToSend: Request-to-Send (RTS) hardware flow control is used. RTS signals that data is available for transmission. If the input buffer becomes full, the RTS line will be set to false. The RTS line will be set to true when more room becomes available in the input buffer." _
        & vbCrLf _
        & "RequestToSendXOnXOff: RequestToSendXOnXOff Both the Request-to-Send (RTS) hardware control and the XON/XOFF software controls are used." _
        & vbCrLf _
        & "XOnXOff: The XON/XOFF software control protocol is used. The XOFF control is sent to stop the transmission of data. The XON control is sent to resume the transmission. These software controls are used instead of Request to Send (RTS) and Clear to Send (CTS) hardware controls."
    Public Const DtrDesc As String = "True to enable Data Terminal Ready (DTR); otherwise, false. The default is false.  Data Terminal Ready (DTR) is typically enabled during XON/XOFF software handshaking and Request to Send/Clear to Send (RTS/CTS) hardware handshaking, and modem communications."
    Public Const RtsDesc As String = "True to enable Request to Transmit (RTS); otherwise, false. The default is false.  The Request to Transmit (RTS) signal is typically used in Request to Send/Clear to Send (RTS/CTS) hardware handshaking."
    Public Const ReadTimeoutDesc As String = "The number of milliseconds before a time-out occurs when a read operation does not finish. The read time-out value was originally set at 500 milliseconds in the Win32 Communications API. This property allows you to set this value. The time-out can be set to any value greater than zero, or set to InfiniteTimeout (-1), in which case no time-out occurs. InfiniteTimeout (-1) is the default.   Any value of 0 or less will be edited to -1."
    Public Const WriteTimeoutDesc As String = "The number of milliseconds before a time-out occurs when a write operation does not finish. The write time-out value was originally set at 500 milliseconds in the Win32 Communications API. This property allows you to set this value. The time-out can be set to any value greater than zero, or set to InfiniteTimeout (-1), in which case no time-out occurs. InfiniteTimeout (-1) is the default.   Any value of 0 or less will be edited to -1."
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pPortName As String
    Private pBaudRate As Int32
    Private pParity As Ports.Parity
    Private pDataBits As Int32
    Private pStopBits As Ports.StopBits
    Private pHandShake As Ports.Handshake
    Private pDTREnable As Boolean
    Private pRTSEnable As Boolean
    Private pReadTimeout As Int32
    Private pWriteTimeout As Int32
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SerialSettings
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SerialSettings = New SerialSettings
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As SerialSettings
        Return New SerialSettings
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    <CategoryAttribute(BasicSerialPortSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DesignOnly(False), _
    DescriptionAttribute(PortnameDesc), _
    TypeConverter(GetType(IO.EditableSerialPortList))> _
    Public Property Portname() As String
        Get
            Return pPortName
        End Get
        Set(ByVal Value As String)
            pPortName = Value
        End Set
    End Property

    <CategoryAttribute(BasicSerialPortSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultBaudRate), _
    DesignOnly(False), _
    DescriptionAttribute(BaudRateDesc), _
    TypeConverter(GetType(IO.EditableBaudRateList))> _
    Public Property BaudRate() As Int32
        Get
            Return pBaudRate
        End Get
        Set(ByVal Value As Int32)
            If Value.Equals(0) Then
                Value = BartelsLibrary.Constants.DefaultBaudRate
            End If
            pBaudRate = Value
        End Set
    End Property

    <CategoryAttribute(BasicSerialPortSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultParity), _
    DesignOnly(False), _
    DescriptionAttribute(ParityDesc)> _
    Public Property Parity() As System.IO.Ports.Parity
        Get
            Return pParity
        End Get
        Set(ByVal Value As System.IO.Ports.Parity)
            pParity = Value
        End Set
    End Property

    <CategoryAttribute(BasicSerialPortSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultDataBits), _
    DesignOnly(False), _
    DescriptionAttribute(DataBitsDesc), _
    TypeConverter(GetType(IO.EditableDataBitsList))> _
    Public Property DataBits() As Int32
        Get
            Return pDataBits
        End Get
        Set(ByVal Value As Int32)
            If Value.Equals(0) Then
                Value = BartelsLibrary.Constants.DefaultDataBits
            End If
            pDataBits = Value
        End Set
    End Property

    <CategoryAttribute(BasicSerialPortSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultStopBits), _
    DesignOnly(False), _
    DescriptionAttribute(StopBitsDesc)> _
    Public Property StopBits() As System.IO.Ports.StopBits
        Get
            Return pStopBits
        End Get
        Set(ByVal Value As System.IO.Ports.StopBits)
            pStopBits = Value
        End Set
    End Property

    <CategoryAttribute(BasicSerialPortSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultHandshake), _
    DesignOnly(False), _
    DescriptionAttribute(HandshakeDesc)> _
    Public Property Handshake() As System.IO.Ports.Handshake
        Get
            Return pHandShake
        End Get
        Set(ByVal Value As System.IO.Ports.Handshake)
            pHandShake = Value
        End Set
    End Property

    <CategoryAttribute(AdvancedSerialSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultUseDTR), _
    DesignOnly(False), _
    DescriptionAttribute(DtrDesc)> _
    Public Property DTREnable() As Boolean
        Get
            Return pDTREnable
        End Get
        Set(ByVal Value As Boolean)
            pDTREnable = Value
        End Set
    End Property

    <CategoryAttribute(AdvancedSerialSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultUseRTS), _
    DesignOnly(False), _
    DescriptionAttribute(RtsDesc)> _
    Public Property RTSEnable() As Boolean
        Get
            Return pRTSEnable
        End Get
        Set(ByVal Value As Boolean)
            pRTSEnable = Value
        End Set
    End Property

    <CategoryAttribute(AdvancedSerialSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultReadTimeout), _
    DesignOnly(False), _
    DescriptionAttribute(ReadTimeoutDesc)> _
    Public Property ReadTimeout() As Int32
        Get
            Return pReadTimeout
        End Get
        Set(ByVal Value As Int32)
            pReadTimeout = Value
            If pReadTimeout < 1 Then
                pReadTimeout = System.IO.Ports.SerialPort.InfiniteTimeout
            End If
        End Set
    End Property

    <CategoryAttribute(AdvancedSerialSettingsDesc), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultWriteTimeout), _
    DesignOnly(False), _
    Description(WriteTimeoutDesc)> _
    Public Property WriteTimeout() As Int32
        Get
            Return pWriteTimeout
        End Get
        Set(ByVal Value As Int32)
            pWriteTimeout = Value
            If pWriteTimeout < 1 Then
                pWriteTimeout = System.IO.Ports.SerialPort.InfiniteTimeout
            End If
        End Set
    End Property

    Public Overrides Sub SetToDefaults()
        Dim serialPort As New Ports.SerialPort

        Portname = serialPort.PortName
        BaudRate = serialPort.BaudRate
        Parity = serialPort.Parity
        DataBits = serialPort.DataBits
        StopBits = serialPort.StopBits
        Handshake = serialPort.Handshake
        DTREnable = serialPort.DtrEnable
        RTSEnable = serialPort.RtsEnable
        ReadTimeout = serialPort.ReadTimeout
        WriteTimeout = serialPort.WriteTimeout
    End Sub

    Public Overrides Sub CopyPropertiesTo(ByRef ISettings As ISettings)
        Dim SerialSettings As SerialSettings = CType(ISettings, SerialSettings)

        SerialSettings.Name = Name

        SerialSettings.Portname = Portname
        SerialSettings.BaudRate = BaudRate
        SerialSettings.Parity = Parity
        SerialSettings.DataBits = DataBits
        SerialSettings.StopBits = StopBits
        SerialSettings.Handshake = Handshake
        SerialSettings.DTREnable = DTREnable
        SerialSettings.RTSEnable = RTSEnable
        SerialSettings.ReadTimeout = ReadTimeout
        SerialSettings.WriteTimeout = WriteTimeout
    End Sub

    Public Overrides Function Clone() As Object Implements System.ICloneable.Clone
        Dim SerialSettings As ISettings = IO.SerialSettings.GetInstance
        CopyPropertiesTo(SerialSettings)
        Return SerialSettings
    End Function

    Public Overrides Function PropertiesSet() As Boolean
        Return Not String.IsNullOrEmpty(pPortName)
    End Function

    Public Function PropGridSelectedObject() As Object Implements ISettingsToPropGridAdapter.PropGridSelectedObject
        Return Me
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
