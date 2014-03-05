Public Class FrmCompareZ12
    Inherits MVPViewContainsGaugeCoordBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        UserCtrlGauge2AxisCoordZ12.GaugeLayout = BartelsLibrary.Layout.Vertical
        UserCtrlGauge2AxisCoordLatAz.GaugeLayout = BartelsLibrary.Layout.Vertical

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
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents PropertyGrid As System.Windows.Forms.PropertyGrid
    Friend WithEvents MvpUserCtrlGraphics As BartelsLibrary.MVPUserCtrlGraphics
    Friend WithEvents UserCtrlGauge2AxisCoordLatAz As ScopeIII.Forms.UserCtrlGauge2AxisCoord
    Friend WithEvents UserCtrlGauge2AxisCoordZ12 As ScopeIII.Forms.UserCtrlGauge2AxisCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCompareZ12))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnQuit = New System.Windows.Forms.Button
        Me.PropertyGrid = New System.Windows.Forms.PropertyGrid
        Me.MvpUserCtrlGraphics = New BartelsLibrary.MVPUserCtrlGraphics
        Me.UserCtrlGauge2AxisCoordZ12 = New ScopeIII.Forms.UserCtrlGauge2AxisCoord
        Me.UserCtrlGauge2AxisCoordLatAz = New ScopeIII.Forms.UserCtrlGauge2AxisCoord
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
        'btnQuit
        '
        Me.btnQuit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnQuit.Location = New System.Drawing.Point(440, 624)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 3
        Me.btnQuit.Text = "Quit"
        '
        'PropertyGrid
        '
        Me.PropertyGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.PropertyGrid.Location = New System.Drawing.Point(576, 0)
        Me.PropertyGrid.Name = "PropertyGrid"
        Me.PropertyGrid.Size = New System.Drawing.Size(248, 214)
        Me.PropertyGrid.TabIndex = 5
        '
        'MvpUserCtrlGraphics
        '
        Me.MvpUserCtrlGraphics.IRenderer = Nothing
        Me.MvpUserCtrlGraphics.Location = New System.Drawing.Point(0, 0)
        Me.MvpUserCtrlGraphics.Name = "MvpUserCtrlGraphics"
        Me.MvpUserCtrlGraphics.Size = New System.Drawing.Size(150, 150)
        Me.MvpUserCtrlGraphics.TabIndex = 11
        '
        'UserCtrlGauge2AxisCoordZ12
        '
        Me.UserCtrlGauge2AxisCoordZ12.GaugeLayout = Nothing
        Me.UserCtrlGauge2AxisCoordZ12.Location = New System.Drawing.Point(576, 220)
        Me.UserCtrlGauge2AxisCoordZ12.Name = "UserCtrlGauge2AxisCoordZ12"
        Me.UserCtrlGauge2AxisCoordZ12.Size = New System.Drawing.Size(248, 130)
        Me.UserCtrlGauge2AxisCoordZ12.TabIndex = 12
        '
        'UserCtrlGauge2AxisCoordLatAz
        '
        Me.UserCtrlGauge2AxisCoordLatAz.GaugeLayout = Nothing
        Me.UserCtrlGauge2AxisCoordLatAz.Location = New System.Drawing.Point(576, 356)
        Me.UserCtrlGauge2AxisCoordLatAz.Name = "UserCtrlGauge2AxisCoordLatAz"
        Me.UserCtrlGauge2AxisCoordLatAz.Size = New System.Drawing.Size(248, 304)
        Me.UserCtrlGauge2AxisCoordLatAz.TabIndex = 14
        '
        'FrmCompareZ12
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnQuit
        Me.ClientSize = New System.Drawing.Size(832, 662)
        Me.Controls.Add(Me.UserCtrlGauge2AxisCoordLatAz)
        Me.Controls.Add(Me.UserCtrlGauge2AxisCoordZ12)
        Me.Controls.Add(Me.MvpUserCtrlGraphics)
        Me.Controls.Add(Me.PropertyGrid)
        Me.Controls.Add(Me.btnQuit)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmCompareZ12"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Show Z12s"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property FormTitle() As String
        Get
            Return Text
        End Get
        Set(ByVal Value As String)
            Text = Value
        End Set
    End Property

    Public Property PropGridSelectedObject() As Object
        Get
            Return PropertyGrid.SelectedObject
        End Get
        Set(ByVal Value As Object)
            PropertyGrid.SelectedObject = Value
        End Set
    End Property

    Public Sub SetToolTip()
        ToolTip.SetToolTip(MvpUserCtrlGraphics, ScopeLibrary.Constants.GraphicsToolTipZ12)
        ToolTip.IsBalloon = True
    End Sub

    Private Sub FrmGraphOKPropGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        resizer(sender, e)
    End Sub

    Protected Sub propertyGridControllerpPropertyValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid.PropertyValueChanged
        MvpUserCtrlGraphics.Refresh()
    End Sub

    Protected Overridable Sub resizer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Dim UserCtrlGauge2AxisCoordWidth As Int32 = PropertyGrid.Width
        Dim UserCtrlGauge2AxisCoordHeight As Int32 = emath.rint((MyBase.Height - PropertyGrid.Height) * 0.3)
        UserCtrlGauge2AxisCoordZ12.Size = New Drawing.Size(UserCtrlGauge2AxisCoordWidth, UserCtrlGauge2AxisCoordHeight)
        UserCtrlGauge2AxisCoordZ12.Location = New Drawing.Point(PropertyGrid.Location.X, PropertyGrid.Height + 16)

        Dim UserCtrlGauge2AxisCoordLatAzWidth As Int32 = UserCtrlGauge2AxisCoordWidth
        Dim UserCtrlGauge2AxisCoordLatAzHeight As Int32 = MyBase.Height - UserCtrlGauge2AxisCoordZ12.Location.Y - UserCtrlGauge2AxisCoordZ12.Height - 40
        UserCtrlGauge2AxisCoordLatAz.Size = New Drawing.Size(UserCtrlGauge2AxisCoordLatAzWidth, UserCtrlGauge2AxisCoordLatAzHeight)
        UserCtrlGauge2AxisCoordLatAz.Location = New Drawing.Point(UserCtrlGauge2AxisCoordZ12.Location.X, UserCtrlGauge2AxisCoordZ12.Location.Y + UserCtrlGauge2AxisCoordZ12.Height)

        If MvpUserCtrlGraphics IsNot Nothing Then
            Dim w As Int32 = PropertyGrid.Location.X
            If w < 1 Then
                w = 1
            End If
            Dim h As Int32 = btnQuit.Location.Y - 16
            If h < 1 Then
                h = 1
            End If
            MvpUserCtrlGraphics.Size = New Drawing.Size(w, h)
        End If
    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Close()
    End Sub

End Class
