Public Class FrmShowSettings
    Inherits MVPViewBase
    Implements IFrmShowSettings

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
    Friend WithEvents UserCtrlPropGrid As ScopeIII.Forms.UserCtrlPropGrid

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmShowSettings))
        Me.UserCtrlPropGrid = New ScopeIII.Forms.UserCtrlPropGrid
        Me.SuspendLayout()
        '
        'UserCtrlPropGrid
        '
        Me.UserCtrlPropGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlPropGrid.Location = New System.Drawing.Point(2, 2)
        Me.UserCtrlPropGrid.Name = "UserCtrlPropGrid"
        Me.UserCtrlPropGrid.SelectedObject = Nothing
        Me.UserCtrlPropGrid.Size = New System.Drawing.Size(407, 480)
        Me.UserCtrlPropGrid.TabIndex = 0
        '
        'FrmShowSettings
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(411, 486)
        Me.Controls.Add(Me.UserCtrlPropGrid)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmShowSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Settings"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property Title() As String Implements IFrmShowSettings.Title
        Get
            Return Me.Text
        End Get
        Set(ByVal Value As String)
            Me.Text = Value
        End Set
    End Property

    Public Property UserCtrlPropGridPropertyWrapper() As UserCtrlPropGrid Implements IFrmShowSettings.UserCtrlPropGrid
        Get
            Return UserCtrlPropGrid
        End Get
        Set(ByVal value As UserCtrlPropGrid)
            UserCtrlPropGrid = value
        End Set
    End Property
End Class
