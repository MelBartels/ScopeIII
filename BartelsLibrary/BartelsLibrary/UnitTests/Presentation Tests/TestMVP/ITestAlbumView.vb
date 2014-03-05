Public Interface ITestAlbumView
    Inherits IMVPView

    Property WindowTitle() As String
    Property Title() As String
    Property Artist() As String
    Property IsClassical() As Boolean
    Property Composer() As String
    Property ComposerEnabled() As Boolean
    Property Albums() As String()
    Property AlbumIndex() As Int32
    Property ApplyEnabled() As Boolean
    Property CancelEnabled() As Boolean
End Interface
