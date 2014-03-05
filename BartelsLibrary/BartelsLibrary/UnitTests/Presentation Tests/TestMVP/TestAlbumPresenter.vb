#Region "Imports"
#End Region

Public Class TestAlbumPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pAlbums As TestAlbum()
    Private pIsListening As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TestAlbumPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestAlbumPresenter = New TestAlbumPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As TestAlbumPresenter
        Return New TestAlbumPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Property DataModel() As Object
        Get
            Return pAlbums
        End Get
        Set(ByVal Value As Object)
            pAlbums = CType(Value, TestAlbum())
            pIsListening = True
            loadViewFromModel()
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        AddHandler ITestAlbumView.LoadViewFromModel, AddressOf loadViewFromModel
        AddHandler ITestAlbumView.ViewUpdated, AddressOf viewUpdated
        AddHandler ITestAlbumView.SaveToModel, AddressOf saveToModel
    End Sub

    Protected Overrides Sub loadViewFromModel()
        If pIsListening Then
            pIsListening = False

            refreshAlbumList()
            ITestAlbumView.Title = selectedAlbum.Title
            updateWindowTitle()
            ITestAlbumView.Artist = selectedAlbum.Artist
            ITestAlbumView.IsClassical = selectedAlbum.IsClassical
            If selectedAlbum.IsClassical Then
                ITestAlbumView.Composer = selectedAlbum.Composer
            Else
                ITestAlbumView.Composer = String.Empty
            End If
            ITestAlbumView.ComposerEnabled = selectedAlbum.IsClassical
            enableApplyAndCancel(False)

            pIsListening = True
        End If
    End Sub

    Protected Overrides Sub saveToModel()
        selectedAlbum().Title = ITestAlbumView.Title
        selectedAlbum().Artist = ITestAlbumView.Artist
        selectedAlbum().IsClassical = ITestAlbumView.IsClassical
        If ITestAlbumView.IsClassical Then
            selectedAlbum().Composer = ITestAlbumView.Composer
        Else
            selectedAlbum().Composer = String.Empty
        End If
        enableApplyAndCancel(False)
        loadViewFromModel()
    End Sub

    Protected Overrides Sub viewUpdated()
        enableApplyAndCancel(True)
        ITestAlbumView.ComposerEnabled = ITestAlbumView.IsClassical
        updateWindowTitle()
    End Sub

    Private Function ITestAlbumView() As ITestAlbumView
        Return CType(pIMVPView, ITestAlbumView)
    End Function

    Private Sub refreshAlbumList()
        ' preserve the index and reinstitute it after creating the albums array
        Dim ix As Int32 = ITestAlbumView.AlbumIndex
        ITestAlbumView.Albums = createAlbumStringArray()
        ITestAlbumView.AlbumIndex = ix
    End Sub

    Private Sub updateWindowTitle()
        ITestAlbumView.WindowTitle = "Album: " & ITestAlbumView.Title
    End Sub

    Private Sub enableApplyAndCancel(ByVal state As Boolean)
        ITestAlbumView.ApplyEnabled = state
        ITestAlbumView.CancelEnabled = state
    End Sub

    Private Function selectedAlbum() As TestAlbum
        If ITestAlbumView.AlbumIndex > -1 AndAlso ITestAlbumView.AlbumIndex < pAlbums.Length Then
            Return pAlbums(ITestAlbumView.AlbumIndex)
        End If
        Return pAlbums(0)
    End Function

    Private Function createAlbumStringArray() As String()
        Dim albums(pAlbums.Length - 1) As String
        For ix As Int32 = 0 To pAlbums.Length - 1
            albums(ix) = pAlbums(ix).Title
        Next
        Return albums
    End Function
#End Region

End Class
