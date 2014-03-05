#Region "Imports"
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
#End Region

Public Class TCPclientFacade
    Inherits IOBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pBuffer(BartelsLibrary.Constants.TCPBufferSize - 1) As Byte
    Protected pTCPclient As TcpClient
    Protected pNetworkStream As NetworkStream
    Protected pClientThread As Thread
    Protected pIntentionalStop As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TCPclientFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '     Sub New()
    '    End Sub
    '    ' friend = internal,  = static, readonly = final
    '    Friend  ReadOnly INSTANCE As TCPclientFacade = New TCPclientFacade
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pIOType = BartelsLibrary.IOType.TCPclient
        pIOSettingsFacadeTemplate = SettingsFacadeTemplate.GetInstance.Build(IPsettings.GetInstance, GetType(IPsettings).Name)
    End Sub

    Public Shared Function GetInstance() As TCPclientFacade
        Return New TCPclientFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides ReadOnly Property PortName() As String
        Get
            Return MyBase.PortName _
                & CType(pIOSettingsFacadeTemplate.ISettings, IPsettings).Address _
                & "_" _
                & CType(pIOSettingsFacadeTemplate.ISettings, IPsettings).Port
        End Get
    End Property

    Public Overrides Function Open() As Boolean
        MyBase.Open()

        Try
            pIntentionalStop = False

            pClientThread = New Thread(AddressOf Connect)
            pClientThread.Name = "TCPclient"
            pClientThread.Start()

            Return True
        Catch ex As Exception
            Return openPortFailure(ex)
        End Try
    End Function

    Public Overrides Sub Close()
        MyBase.Close()

        Try
            pIntentionalStop = True
            If pNetworkStream IsNot Nothing Then
                pNetworkStream.Close()
                pNetworkStream = Nothing
            End If
            If pTCPclient IsNot Nothing Then
                pTCPclient.Close()
                pTCPclient = Nothing
            End If
            GC.Collect()
        Catch ex As Exception
        End Try
    End Sub

    Public Overloads Overrides Function Send(ByVal bytes As Byte()) As Boolean
        Try
            pNetworkStream.Write(bytes, 0, bytes.Length)
            NotifyTransmitObservers(Encoder.BytesToString(bytes))
        Catch ex As Exception
            ExceptionService.Notify(ex)
            Return False
        End Try
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub Connect()
        Try
            Dim ipHostInfo As IPHostEntry = Dns.GetHostEntry(CType(pIOSettingsFacadeTemplate.ISettings, IPsettings).Address)
            Dim ipAddress As IPAddress = ipHostInfo.AddressList(0)
            pTCPclient = New TcpClient
            pTCPclient.Connect(ipAddress, CType(pIOSettingsFacadeTemplate.ISettings, IPsettings).Port)
            pNetworkStream = pTCPclient.GetStream()
            NotifyStatusObservers("Connected.")

            ' Read blocks 
            Dim bytesRead As Int32 = pNetworkStream.Read(pBuffer, 0, pBuffer.Length)
            While bytesRead > 0
                ' copy received data into new buffer to send to observers, in case observers are long running
                Dim bs(bytesRead - 1) As Byte
                Array.Copy(pBuffer, bs, bytesRead)
                NotifyReceiveObservers(Encoder.BytesToString(bs))

                bytesRead = pNetworkStream.Read(pBuffer, 0, pBuffer.Length)
            End While

        Catch ex As Exception
            If pIntentionalStop Then
                Exit Sub
            End If
            ExceptionService.Notify(ex)
        End Try
    End Sub
#End Region
End Class
