#Region "Imports"
#End Region

Public Class IOLoggingFacade

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIIO As IIO
    Private pLoggingObserver As LoggingObserver
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IOLoggingFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IOLoggingFacade = New IOLoggingFacade
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        LoggingObserver = LoggingObserver.GetInstance
    End Sub

    Public Shared Function GetInstance() As IOLoggingFacade
        Return New IOLoggingFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IIO() As IIO
        Get
            Return pIIO
        End Get
        Set(ByVal Value As IIO)
            pIIO = Value
        End Set
    End Property

    Public Property LoggingObserver() As LoggingObserver
        Get
            Return pLoggingObserver
        End Get
        Set(ByVal value As LoggingObserver)
            pLoggingObserver = value
        End Set
    End Property

    Public Function Build(ByRef IIO As IIO) As IOLoggingFacade
        Dim IOLoggingFacade As IOLoggingFacade = IOLoggingFacade.GetInstance
        IOLoggingFacade.IIO = IIO
        IOLoggingFacade.SetIOLoggingFilenameToPortname()
        Return IOLoggingFacade
    End Function

    Public Sub SetIOLoggingFilenameToPortname()
        LoggingObserver.Filename = BartelsLibrary.Constants.LogSubdir & IIO.PortName & BartelsLibrary.Constants.LogExtension
    End Sub

    Public Sub Open()
        LoggingObserver.Open()
        attach()
    End Sub

    Public Sub Close()
        LoggingObserver.Close()
        detach()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Sub attach()
        IIO.IOobservers.ProcessMsgObservers.Attach(CType(LoggingObserver, IObserver))
    End Sub

    Private Sub detach()
        IIO.IOobservers.ProcessMsgObservers.Detach(CType(LoggingObserver, IObserver))
    End Sub
#End Region

End Class
