#Region "Imports"
#End Region

Public Class LoggingObserver
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pFilename As String
    Private pIsOpen As Boolean
    Private pAppendCRLF As Boolean
    Private pStreamWriter As IO.StreamWriter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As LoggingObserver
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As LoggingObserver = New LoggingObserver
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As LoggingObserver
        Return New LoggingObserver
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Filename() As String
        Get
            Return pFilename
        End Get
        Set(ByVal value As String)
            pFilename = value
        End Set
    End Property

    Public ReadOnly Property IsOpen() As Boolean
        Get
            Return pIsOpen
        End Get
    End Property

    Public Property AppendCRLF() As Boolean
        Get
            Return pAppendCRLF
        End Get
        Set(ByVal value As Boolean)
            pAppendCRLF = value
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        If Not IsOpen Then
            Return False
        End If
        Try
            If AppendCRLF Then
                pStreamWriter.WriteLine(CStr([object]))
            Else
                pStreamWriter.Write(CStr([object]))
            End If
            Return True
        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try
        Return False
    End Function

    Public Sub CreateDefaultFilename()
        Filename = BartelsLibrary.Constants.LogSubdir & BartelsLibrary.Constants.Logging & BartelsLibrary.Constants.LogExtension
    End Sub

    Public Function Open() As Boolean
        If String.IsNullOrEmpty(Filename) Then
            CreateDefaultFilename()
        End If

        CommonShared.CreateDirectory(Filename)

        Dim incrFilename As String = Filename
        pStreamWriter = CommonShared.GetIncrementedFilename(incrFilename)
        If pStreamWriter IsNot Nothing Then
            Filename = incrFilename
            pIsOpen = True
            Return True
        End If
        Return False
    End Function

    Public Sub Close()
        If pStreamWriter IsNot Nothing Then
            pStreamWriter.Close()
            pIsOpen = False
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
