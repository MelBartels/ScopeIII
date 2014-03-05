#Region "Imports"
Imports system.io
#End Region

Public Class SerialPortFacade
    Inherits IOBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const BufferSize As Int32 = 1024
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pSerialPort As System.IO.Ports.SerialPort
    Private pReadHandler As Ports.SerialDataReceivedEventHandler
    Protected pBuffer(BufferSize - 1) As Byte
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SerialPortFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SerialPortFacade = New SerialPortFacade
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIOType = BartelsLibrary.IOType.SerialPort
        pIOSettingsFacadeTemplate = SettingsFacadeTemplate.GetInstance.Build(IO.SerialSettings.GetInstance, GetType(SerialSettings).Name)

        pSerialPort = New System.IO.Ports.SerialPort
        ' create delegate to handle DataReceived event
        pReadHandler = New System.IO.Ports.SerialDataReceivedEventHandler(AddressOf Read)
    End Sub

    Public Shared Function GetInstance() As SerialPortFacade
        Return New SerialPortFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides ReadOnly Property PortName() As String
        Get
            Dim name As String = MyBase.PortName _
                & SerialSettings.Portname _
                & "_" _
                & SerialSettings.BaudRate
            ' serial port names come in as 'COM1:' where the colon needs to be stripped for valid logging filename
            name = name.Replace(":", String.Empty)
            Return name
        End Get
    End Property

    Public Overrides Sub LoadSettings()
        MyBase.LoadSettings()
    End Sub

    Public Overrides Function Open() As Boolean
        ' loads settings, sets flag, notifies observers
        MyBase.Open()
        ' copy settings over
        setProperties()
        Try
            pSerialPort.Open()
            pSerialPort.DiscardInBuffer()
            pSerialPort.DiscardOutBuffer()
            Return True
        Catch ex As Exception
            Return openPortFailure(ex)
        End Try
    End Function

    Public Overrides Sub Close()
        MyBase.Close()
        pSerialPort.Close()
    End Sub

    Public Overloads Overrides Function Send(ByVal bytes As Byte()) As Boolean
        Try
            pSerialPort.Write(bytes, 0, bytes.Length)
            NotifyTransmitObservers(Encoder.BytesToString(bytes))
            Return True
        Catch ex As Exception
            NotifyTransmitObservers(BartelsLibrary.Constants.TransmitFailure _
                & ex.Message _
                & ex.InnerException.Message)
            Return False
        End Try
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Sub setProperties()
        ' default encoding is ASCIIEncoding
        'pSerialPort.Encoding = New Text.ASCIIEncoding
        pSerialPort.PortName = SerialSettings.Portname
        pSerialPort.BaudRate = SerialSettings.BaudRate
        pSerialPort.Parity = SerialSettings.Parity
        pSerialPort.DataBits = SerialSettings.DataBits
        ' .None and .OnePointFive not supported and will throw exception
        If SerialSettings.StopBits = Ports.StopBits.None OrElse SerialSettings.StopBits = Ports.StopBits.OnePointFive Then
            SerialSettings.StopBits = Ports.StopBits.One
        End If
        pSerialPort.StopBits = SerialSettings.StopBits
        pSerialPort.Handshake = SerialSettings.Handshake
        pSerialPort.DtrEnable = SerialSettings.DTREnable
        pSerialPort.RtsEnable = SerialSettings.RTSEnable
        pSerialPort.ReadTimeout = SerialSettings.ReadTimeout
        pSerialPort.WriteTimeout = SerialSettings.WriteTimeout
    End Sub

    Private Function SerialSettings() As SerialSettings
        Return CType(pIOSettingsFacadeTemplate.ISettings, SerialSettings)
    End Function

    Protected Sub Read(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles pSerialPort.DataReceived
        ' .ReadExisting() requires that the encoding be set properly;
        ' none of the available encodings appear to work for both bytes and strings
        'NotifyReceiveObservers(CType(sender, System.IO.Ports.SerialPort).ReadExisting)

        ' tried a number of formulations: this is the only one that works consistently for bytes and strings
        Dim port As Ports.SerialPort = CType(sender, Ports.SerialPort)
        Dim ix As Int32 = 0
        ' use this test to ensure that all bytes in the SerialPort buffer and underlying stream are captured
        While port.BytesToRead > 0
            pBuffer(ix) = CByte(port.ReadByte)
            ix += 1
        End While
        NotifyReceiveObservers(BartelsLibrary.Encoder.BytesToString(pBuffer, 0, ix))
    End Sub
#End Region
End Class
