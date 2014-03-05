Public Class FrmRefractionAirMass
    Inherits MVPViewContainsGaugeCoordBase

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
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents UserCtrlCoordRefract As ScopeIII.Forms.UserCtrlCoord
    Friend WithEvents UserCtrlGaugeCoordAlt As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlCoordAirMass As ScopeIII.Forms.UserCtrlCoord
    Friend WithEvents MVPUserCtrlGraphics As BartelsLibrary.MVPUserCtrlGraphics
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRefractionAirMass))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button
        Me.MVPUserCtrlGraphics = New BartelsLibrary.MVPUserCtrlGraphics
        Me.UserCtrlCoordAirMass = New ScopeIII.Forms.UserCtrlCoord
        Me.UserCtrlGaugeCoordAlt = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlCoordRefract = New ScopeIII.Forms.UserCtrlCoord
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(401, 313)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'MVPUserCtrlGraphics
        '
        Me.MVPUserCtrlGraphics.IRenderer = Nothing
        Me.MVPUserCtrlGraphics.Location = New System.Drawing.Point(0, 0)
        Me.MVPUserCtrlGraphics.Name = "MVPUserCtrlGraphics"
        Me.MVPUserCtrlGraphics.Size = New System.Drawing.Size(150, 150)
        Me.MVPUserCtrlGraphics.TabIndex = 9
        '
        'UserCtrlCoordAirMass
        '
        Me.UserCtrlCoordAirMass.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlCoordAirMass.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordAirMass.Location = New System.Drawing.Point(304, 266)
        Me.UserCtrlCoordAirMass.Name = "UserCtrlCoordAirMass"
        Me.UserCtrlCoordAirMass.Size = New System.Drawing.Size(176, 24)
        Me.UserCtrlCoordAirMass.TabIndex = 10
        '
        'UserCtrlGaugeCoordAlt
        '
        Me.UserCtrlGaugeCoordAlt.Location = New System.Drawing.Point(296, 0)
        Me.UserCtrlGaugeCoordAlt.Name = "UserCtrlGaugeCoordAlt"
        Me.UserCtrlGaugeCoordAlt.Size = New System.Drawing.Size(184, 248)
        Me.UserCtrlGaugeCoordAlt.TabIndex = 8
        '
        'UserCtrlCoordRefract
        '
        Me.UserCtrlCoordRefract.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlCoordRefract.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordRefract.Location = New System.Drawing.Point(304, 245)
        Me.UserCtrlCoordRefract.Name = "UserCtrlCoordRefract"
        Me.UserCtrlCoordRefract.Size = New System.Drawing.Size(176, 24)
        Me.UserCtrlCoordRefract.TabIndex = 4
        '
        'FrmRefractionAirMass
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(488, 348)
        Me.Controls.Add(Me.UserCtrlCoordAirMass)
        Me.Controls.Add(Me.MVPUserCtrlGraphics)
        Me.Controls.Add(Me.UserCtrlGaugeCoordAlt)
        Me.Controls.Add(Me.UserCtrlCoordRefract)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmRefractionAirMass"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Refraction + AirMass"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property GraphRenderer() As IRenderer
        Get
            Return MVPUserCtrlGraphics.IRenderer
        End Get
        Set(ByVal Value As IRenderer)
            MVPUserCtrlGraphics.IRenderer = Value
        End Set
    End Property

    Public Sub RenderGraph()
        MVPUserCtrlGraphics.IRenderer.Render(MVPUserCtrlGraphics.CreateGraphics, MVPUserCtrlGraphics.Width, MVPUserCtrlGraphics.Height)
    End Sub

    Protected Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        UserCtrlGaugeCoordAlt.Height = MyBase.Height - 140
        UserCtrlGaugeCoordAlt.Location = New Drawing.Point(MyBase.Width - UserCtrlGaugeCoordAlt.Width - 8, 0)

        MVPUserCtrlGraphics.Size = New Drawing.Size(UserCtrlGaugeCoordAlt.Location.X, MyBase.Height - 8)
    End Sub

End Class
