#Region "Imports"
#End Region

Public Class TestAlbum

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pArtist As String
    Private pTitle As String
    Private pComposer As String
    Private pIsClassical As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TestAlbum
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestAlbum = New TestAlbum
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As TestAlbum
        Return New TestAlbum
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Artist() As String
        Get
            Return pArtist
        End Get
        Set(ByVal Value As String)
            pArtist = Value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return pTitle
        End Get
        Set(ByVal Value As String)
            pTitle = Value
        End Set
    End Property

    Public Property Composer() As String
        Get
            Return pComposer
        End Get
        Set(ByVal Value As String)
            pComposer = Value
        End Set
    End Property

    Public Property IsClassical() As Boolean
        Get
            Return pIsClassical
        End Get
        Set(ByVal Value As Boolean)
            pIsClassical = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
