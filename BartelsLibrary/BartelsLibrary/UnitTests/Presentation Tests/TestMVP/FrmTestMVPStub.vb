#Region "Imports"
#End Region

Public Class FrmTestMVPStub
    Inherits MVPViewBase
    Implements ITestAlbumView

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pComposerEnabled As Boolean
    Private pIsClassical As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As FrmTestMVPStub
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As FrmTestMVPStub = New FrmTestMVPStub
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pComposerEnabled = True
    End Sub

    Public Shared Function GetInstance() As FrmTestMVPStub
        Return New FrmTestMVPStub
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property AlbumIndex() As Integer Implements ITestAlbumView.AlbumIndex
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Integer)
        End Set
    End Property

    Public Property Albums() As String() Implements ITestAlbumView.Albums
        Get
            Return Nothing
        End Get
        Set(ByVal Value() As String)
        End Set
    End Property

    Public Property ApplyEnabled() As Boolean Implements ITestAlbumView.ApplyEnabled
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Boolean)
        End Set
    End Property

    Public Property Artist() As String Implements ITestAlbumView.Artist
        Get
            Return Nothing
        End Get
        Set(ByVal Value As String)
        End Set
    End Property

    Public Property CancelEnabled() As Boolean Implements ITestAlbumView.CancelEnabled
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Boolean)
        End Set
    End Property

    Public Property Composer() As String Implements ITestAlbumView.Composer
        Get
            Return Nothing
        End Get
        Set(ByVal Value As String)
        End Set
    End Property

    Public Property ComposerEnabled() As Boolean Implements ITestAlbumView.ComposerEnabled
        Get
            Return pComposerEnabled
        End Get
        Set(ByVal Value As Boolean)
            pComposerEnabled = Value
        End Set
    End Property

    Public Property IsClassical() As Boolean Implements ITestAlbumView.IsClassical
        Get
            Return pIsClassical
        End Get
        Set(ByVal Value As Boolean)
            pIsClassical = Value
        End Set
    End Property

    Public Property Title() As String Implements ITestAlbumView.Title
        Get
            Return Nothing
        End Get
        Set(ByVal Value As String)
        End Set
    End Property

    Public Property WindowTitle() As String Implements ITestAlbumView.WindowTitle
        Get
            Return Nothing
        End Get
        Set(ByVal Value As String)
        End Set
    End Property

    Public Sub RaiseUpdated()
        onViewUpdated()
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTestMVPStub))
        Me.SuspendLayout()
        '
        'frmtestmvpstub
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmtestmvpstub"
        Me.ResumeLayout(False)

    End Sub
End Class
