Public Class FrmTestMVP
    Inherits MVPViewBase
    Implements ITestAlbumView

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txBxArtist As System.Windows.Forms.TextBox
    Friend WithEvents txBxTitle As System.Windows.Forms.TextBox
    Friend WithEvents txBxComposer As System.Windows.Forms.TextBox
    Friend WithEvents chBxClassical As System.Windows.Forms.CheckBox
    Friend WithEvents lsBxAlbums As System.Windows.Forms.ListBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTestMVP))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txBxArtist = New System.Windows.Forms.TextBox
        Me.txBxTitle = New System.Windows.Forms.TextBox
        Me.txBxComposer = New System.Windows.Forms.TextBox
        Me.chBxClassical = New System.Windows.Forms.CheckBox
        Me.lsBxAlbums = New System.Windows.Forms.ListBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(152, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Artist"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(152, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Title"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(152, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Composer"
        '
        'txBxArtist
        '
        Me.txBxArtist.Location = New System.Drawing.Point(216, 8)
        Me.txBxArtist.Name = "txBxArtist"
        Me.txBxArtist.Size = New System.Drawing.Size(100, 20)
        Me.txBxArtist.TabIndex = 3
        Me.txBxArtist.Text = "TextBox1"
        '
        'txBxTitle
        '
        Me.txBxTitle.Location = New System.Drawing.Point(216, 40)
        Me.txBxTitle.Name = "txBxTitle"
        Me.txBxTitle.Size = New System.Drawing.Size(100, 20)
        Me.txBxTitle.TabIndex = 4
        Me.txBxTitle.Text = "TextBox1"
        '
        'txBxComposer
        '
        Me.txBxComposer.Location = New System.Drawing.Point(216, 104)
        Me.txBxComposer.Name = "txBxComposer"
        Me.txBxComposer.Size = New System.Drawing.Size(100, 20)
        Me.txBxComposer.TabIndex = 5
        Me.txBxComposer.Text = "TextBox1"
        '
        'chBxClassical
        '
        Me.chBxClassical.Location = New System.Drawing.Point(216, 72)
        Me.chBxClassical.Name = "chBxClassical"
        Me.chBxClassical.Size = New System.Drawing.Size(72, 24)
        Me.chBxClassical.TabIndex = 6
        Me.chBxClassical.Text = "classical"
        '
        'lsBxAlbums
        '
        Me.lsBxAlbums.Location = New System.Drawing.Point(8, 8)
        Me.lsBxAlbums.Name = "lsBxAlbums"
        Me.lsBxAlbums.Size = New System.Drawing.Size(120, 160)
        Me.lsBxAlbums.TabIndex = 7
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(240, 144)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(152, 144)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 9
        Me.btnApply.Text = "Apply"
        '
        'FrmTestMVP
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(328, 174)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lsBxAlbums)
        Me.Controls.Add(Me.chBxClassical)
        Me.Controls.Add(Me.txBxComposer)
        Me.Controls.Add(Me.txBxTitle)
        Me.Controls.Add(Me.txBxArtist)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmTestMVP"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmTestMVP"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Property WindowTitle() As String Implements ITestAlbumView.WindowTitle
        Get
            Return Me.Text
        End Get
        Set(ByVal Value As String)
            Me.Text = Value
        End Set
    End Property

    Public Property Title() As String Implements ITestAlbumView.Title
        Get
            Return txBxTitle.Text
        End Get
        Set(ByVal Value As String)
            txBxTitle.Text = Value
        End Set
    End Property

    Public Property Artist() As String Implements ITestAlbumView.Artist
        Get
            Return txBxArtist.Text
        End Get
        Set(ByVal Value As String)
            txBxArtist.Text = Value
        End Set
    End Property

    Public Property Composer() As String Implements ITestAlbumView.Composer
        Get
            Return txBxComposer.Text
        End Get
        Set(ByVal Value As String)
            txBxComposer.Text = Value
        End Set
    End Property

    Public Property IsClassical() As Boolean Implements ITestAlbumView.IsClassical
        Get
            Return chBxClassical.Checked
        End Get
        Set(ByVal Value As Boolean)
            chBxClassical.Checked = Value
        End Set
    End Property

    Public Property ComposerEnabled() As Boolean Implements ITestAlbumView.ComposerEnabled
        Get
            Return txBxComposer.Enabled
        End Get
        Set(ByVal Value As Boolean)
            txBxComposer.Enabled = Value
        End Set
    End Property

    Public Property Albums() As String() Implements ITestAlbumView.Albums
        Get
            Return CType(lsBxAlbums.DataSource, String())
            ' or
            'Dim stringArray(lsBxAlbums.Items.Count - 1) As String
            'For ix As Int32 = 0 To stringArray.Length - 1
            '    stringArray(ix) = CStr(lsBxAlbums.Items(ix))
            'Next
            'Return stringArray
        End Get
        Set(ByVal Value As String())
            lsBxAlbums.DataSource = Value
        End Set
    End Property

    Public Property AlbumIndex() As Integer Implements ITestAlbumView.AlbumIndex
        Get
            Return lsBxAlbums.SelectedIndex()
        End Get
        Set(ByVal Value As Integer)
            lsBxAlbums.SelectedIndex = Value
        End Set
    End Property

    Public Property ApplyEnabled() As Boolean Implements ITestAlbumView.ApplyEnabled
        Get
            Return btnApply.Enabled
        End Get
        Set(ByVal Value As Boolean)
            btnApply.Enabled = Value
        End Set
    End Property

    Public Property CancelEnabled() As Boolean Implements ITestAlbumView.CancelEnabled
        Get
            Return btnCancel.Enabled
        End Get
        Set(ByVal Value As Boolean)
            btnCancel.Enabled = Value
        End Set
    End Property

    ' if handles ...TextChanged or ...CheckedChanged, then this event is fired for every keystroke, otherwise use ...Leave
    Private Sub formUpdated(ByVal sender As Object, ByVal e As EventArgs) Handles txBxArtist.TextChanged, _
                                                                                  txBxComposer.TextChanged, _
                                                                                  txBxTitle.TextChanged, _
                                                                                  chBxClassical.CheckedChanged
        onViewUpdated()
    End Sub

    Private Sub formReload(ByVal sender As Object, ByVal e As EventArgs) Handles lsBxAlbums.SelectedValueChanged, _
                                                                                 btnCancel.Click
        onLoadViewFromModel()
    End Sub

    Private Sub applyChanges(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
        onSaveToModel()
    End Sub

End Class
