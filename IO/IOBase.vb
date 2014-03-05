#Region "Imports"
Imports system.io
#End Region

Public MustInherit Class IOBase
    Implements IIO

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pIOType As ISFT
    Protected pSettingsName As String
    Protected pIOSettingsFacadeTemplate As SettingsFacadeTemplate
    Protected pIOLoggingSettingsFacade As IOLoggingFacade
    Protected pReceiveObservers As IObservable
    Protected pTransmitObservers As IObservable
    Protected pStatusObservers As IObservable
    Protected pIOObservers As IOobservers
    Protected pReceiveInspector As ReceiveInspector
    Protected pIsOpened As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IOBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IOBase = New IOBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pReceiveObservers = ObservableImp.GetInstance
        pTransmitObservers = ObservableImp.GetInstance
        pStatusObservers = ObservableImp.GetInstance

        pIOObservers = IO.IOobservers.GetInstance
        pReceiveInspector = IO.ReceiveInspector.GetInstance

        pIOLoggingSettingsFacade = IO.IOLoggingFacade.GetInstance
    End Sub

    'Public Shared Function GetInstance() As IOBase
    '    Return New IOBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public ReadOnly Property IOType() As ISFT Implements IIO.IOType
        Get
            Return pIOType
        End Get
    End Property

    Public Overridable ReadOnly Property PortName() As String Implements IIO.PortName
        Get
            Return "IO_" _
                & pIOType.Name _
                & "_"
        End Get
    End Property

    Public ReadOnly Property ISettingsFacade() As SettingsFacadeTemplate Implements IIO.IOSettingsFacadeTemplate
        Get
            Return pIOSettingsFacadeTemplate
        End Get
    End Property

    Public Property SettingsName() As String Implements IIO.SettingsName
        Get
            Return pSettingsName
        End Get
        Set(ByVal Value As String)
            pSettingsName = Value
        End Set
    End Property

    Public Property ReceiveObservers() As IObservable Implements IIO.ReceiveObservers
        Get
            Return pReceiveObservers
        End Get
        Set(ByVal Value As IObservable)
            pReceiveObservers = Value
        End Set
    End Property

    Public Property TransmitObservers() As IObservable Implements IIO.TransmitObservers
        Get
            Return pTransmitObservers
        End Get
        Set(ByVal Value As IObservable)
            pTransmitObservers = Value
        End Set
    End Property

    Public Property StatusObservers() As IObservable Implements IIO.StatusObservers
        Get
            Return pStatusObservers
        End Get
        Set(ByVal Value As IObservable)
            pStatusObservers = Value
        End Set
    End Property

    Public Property IOobservers() As IOobservers Implements IIO.IOobservers
        Get
            Return pIOObservers
        End Get
        Set(ByVal Value As IOobservers)
            pIOObservers = Value
        End Set
    End Property

    Public Property ReceiveInspector() As ReceiveInspector Implements IIO.ReceiveInspector
        Get
            Return pReceiveInspector
        End Get
        Set(ByVal Value As ReceiveInspector)
            pReceiveInspector = Value
        End Set
    End Property

    Public ReadOnly Property isOpened() As Boolean Implements IIO.isOpened
        Get
            Return pIsOpened
        End Get
    End Property

    Public Overridable Sub LoadSettings() Implements IIO.LoadSettings
        Try
            pIOSettingsFacadeTemplate.LoadSettingsFromConfig()
            If pIOSettingsFacadeTemplate.ISettings Is Nothing Then
                pIOSettingsFacadeTemplate.SetToDefaultSettings()
            End If
        Catch ex As Exception
            pIOSettingsFacadeTemplate.SetToDefaultSettings()
        End Try
    End Sub

    Public Overridable Sub SaveSettings() Implements IIO.SaveSettings
        pIOSettingsFacadeTemplate.SaveSettingsToConfig()
    End Sub

    Public Overridable Function Open() As Boolean Implements IIO.Open
        If pIOSettingsFacadeTemplate.ISettings Is Nothing Then
            LoadSettings()
        End If

        pIsOpened = True
        NotifyStatusObservers(BartelsLibrary.Constants.OpeningPort & " " & PortName)

        Return True
    End Function

    Public Overridable Sub Close() Implements IIO.Close
        pIsOpened = False
        NotifyStatusObservers(BartelsLibrary.Constants.ClosePort)
    End Sub

    Public Sub Shutdown() Implements IIO.Shutdown
        If isOpened Then
            Close()
        End If
    End Sub

    Public MustOverride Overloads Function Send(ByVal bytes() As Byte) As Boolean Implements IIO.Send

    Public Overloads Function Send(ByVal [byte] As Byte) As Boolean Implements IIO.Send
        Return Send(New Byte() {[byte]})
    End Function

    Public Overloads Function Send(ByVal [string] As String) As Boolean Implements IIO.Send
        Return Send(Encoder.StringtoBytes([string]))
    End Function

    Public Overloads Function Send(ByRef fs As FileStream) As Boolean Implements IIO.Send
        Dim fileStreamLength As Integer = CType(fs.Length, Integer)
        Dim buffer(fileStreamLength - 1) As Byte
        fs.Read(buffer, 0, fileStreamLength)
        Return Send(buffer)
    End Function

    Public Sub QueueReceiveBytes(ByVal bytes() As Byte) Implements IIO.QueueReceiveBytes
        NotifyReceiveObservers(Encoder.BytesToString(bytes))
    End Sub

    Public Property IOLoggingSettingsFacade() As IOLoggingFacade Implements IIO.IOLoggingFacade
        Get
            Return pIOLoggingSettingsFacade
        End Get
        Set(ByVal Value As IOLoggingFacade)
            pIOLoggingSettingsFacade = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Sub NotifyReceiveObservers(ByVal data As String)
        pReceiveObservers.Notify(CObj(data))
    End Sub

    Protected Sub NotifyTransmitObservers(ByVal data As String)
        pTransmitObservers.Notify(CObj(data))
    End Sub

    Protected Sub NotifyStatusObservers(ByVal status As String)
        pStatusObservers.Notify(CObj(status))
    End Sub

    Protected Function openPortFailure(ByVal ex As Exception) As Boolean
        pIsOpened = False
        Dim failureMsg As String = BartelsLibrary.Constants.OpenPortFailure & " " & PortName
        AppMsgBox.Show(failureMsg & vbCrLf & ex.Message)
        NotifyStatusObservers(failureMsg)
        Return False
    End Function
#End Region

End Class
