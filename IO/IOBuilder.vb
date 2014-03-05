#Region "Imports"
#End Region

Public Class IOBuilder

#Region "Inner Classes"
#End Region

#Region "Constant Members"
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

    'Public Shared Function GetInstance() As IOBuilder
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IOBuilder = New IOBuilder
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As IOBuilder
        Return New IOBuilder
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Build(ByRef ioType As ISFT) As IIO
        Debug.WriteLine("Building IIO " & ioType.Name)

        Dim IIO As IIO
        If ioType Is BartelsLibrary.IOType.SerialPort Then
            IIO = SerialPortFacade.GetInstance
        ElseIf ioType Is BartelsLibrary.IOType.TCPclient Then
            IIO = TCPclientFacade.GetInstance
        ElseIf ioType Is BartelsLibrary.IOType.TCPserver Then
            IIO = TCPserverFacade.GetInstance
        ElseIf ioType Is BartelsLibrary.IOType.NotSet Then
            Return Nothing
        Else
            Throw New Exception("Unhandled IOType of " & ioType.Name & " in IOBuilder.Build().")
        End If

        If IIO IsNot Nothing Then
            IIO.IOobservers.AttachObserversToIIO(IIO)
            IIO.ReceiveObservers.Attach(CType(IIO.ReceiveInspector, IObserver))

            IIO.IOLoggingFacade.IIO = IIO
            IIO.LoadSettings()

            IIO.IOLoggingFacade = IOLoggingFacade.GetInstance.Build(IIO)
        End If

        Return IIO
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
