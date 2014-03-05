Public Class FrmRenderCoordGaugesOK
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
    Friend WithEvents UserCtrlGaugeCoord10 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord6 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord7 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord8 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord9 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord5 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord4 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord3 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord2 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoord1 As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents btnOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRenderCoordGaugesOK))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnOK = New System.Windows.Forms.Button
        Me.UserCtrlGaugeCoord6 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord7 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord2 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord3 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord4 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord9 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord8 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord5 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord10 = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoord1 = New ScopeIII.Forms.UserCtrlGaugeCoord
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
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(421, 343)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(80, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        '
        'UserCtrlGaugeCoord6
        '
        Me.UserCtrlGaugeCoord6.Location = New System.Drawing.Point(0, 165)
        Me.UserCtrlGaugeCoord6.Name = "UserCtrlGaugeCoord6"
        Me.UserCtrlGaugeCoord6.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord6.TabIndex = 12
        '
        'UserCtrlGaugeCoord7
        '
        Me.UserCtrlGaugeCoord7.Location = New System.Drawing.Point(184, 165)
        Me.UserCtrlGaugeCoord7.Name = "UserCtrlGaugeCoord7"
        Me.UserCtrlGaugeCoord7.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord7.TabIndex = 11
        '
        'UserCtrlGaugeCoord2
        '
        Me.UserCtrlGaugeCoord2.Location = New System.Drawing.Point(184, 0)
        Me.UserCtrlGaugeCoord2.Name = "UserCtrlGaugeCoord2"
        Me.UserCtrlGaugeCoord2.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord2.TabIndex = 5
        '
        'UserCtrlGaugeCoord3
        '
        Me.UserCtrlGaugeCoord3.Location = New System.Drawing.Point(368, 0)
        Me.UserCtrlGaugeCoord3.Name = "UserCtrlGaugeCoord3"
        Me.UserCtrlGaugeCoord3.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord3.TabIndex = 6
        '
        'UserCtrlGaugeCoord4
        '
        Me.UserCtrlGaugeCoord4.Location = New System.Drawing.Point(552, 0)
        Me.UserCtrlGaugeCoord4.Name = "UserCtrlGaugeCoord4"
        Me.UserCtrlGaugeCoord4.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord4.TabIndex = 7
        '
        'UserCtrlGaugeCoord9
        '
        Me.UserCtrlGaugeCoord9.Location = New System.Drawing.Point(552, 165)
        Me.UserCtrlGaugeCoord9.Name = "UserCtrlGaugeCoord9"
        Me.UserCtrlGaugeCoord9.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord9.TabIndex = 9
        '
        'UserCtrlGaugeCoord8
        '
        Me.UserCtrlGaugeCoord8.Location = New System.Drawing.Point(368, 165)
        Me.UserCtrlGaugeCoord8.Name = "UserCtrlGaugeCoord8"
        Me.UserCtrlGaugeCoord8.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord8.TabIndex = 10
        '
        'UserCtrlGaugeCoord5
        '
        Me.UserCtrlGaugeCoord5.Location = New System.Drawing.Point(736, 0)
        Me.UserCtrlGaugeCoord5.Name = "UserCtrlGaugeCoord5"
        Me.UserCtrlGaugeCoord5.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord5.TabIndex = 8
        '
        'UserCtrlGaugeCoord10
        '
        Me.UserCtrlGaugeCoord10.Location = New System.Drawing.Point(736, 165)
        Me.UserCtrlGaugeCoord10.Name = "UserCtrlGaugeCoord10"
        Me.UserCtrlGaugeCoord10.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord10.TabIndex = 13
        '
        'UserCtrlGaugeCoord1
        '
        Me.UserCtrlGaugeCoord1.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGaugeCoord1.Name = "UserCtrlGaugeCoord1"
        Me.UserCtrlGaugeCoord1.Size = New System.Drawing.Size(184, 159)
        Me.UserCtrlGaugeCoord1.TabIndex = 4
        '
        'FrmRenderCoordGaugesOK
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(920, 378)
        Me.Controls.Add(Me.UserCtrlGaugeCoord10)
        Me.Controls.Add(Me.UserCtrlGaugeCoord6)
        Me.Controls.Add(Me.UserCtrlGaugeCoord7)
        Me.Controls.Add(Me.UserCtrlGaugeCoord8)
        Me.Controls.Add(Me.UserCtrlGaugeCoord9)
        Me.Controls.Add(Me.UserCtrlGaugeCoord5)
        Me.Controls.Add(Me.UserCtrlGaugeCoord4)
        Me.Controls.Add(Me.UserCtrlGaugeCoord3)
        Me.Controls.Add(Me.UserCtrlGaugeCoord2)
        Me.Controls.Add(Me.UserCtrlGaugeCoord1)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmRenderCoordGaugesOK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Gauge Coordinates"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected Overridable Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim numberOfCols As Int32 = 5
        Dim ctrlHeight As Int32 = eMath.RInt(MyBase.Height / 2 - 40)

        Dim startY As Int32 = 0
        UserCtrlGaugeCoord1.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord1.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 0 / numberOfCols), startY)

        UserCtrlGaugeCoord2.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord2.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 1 / numberOfCols), startY)

        UserCtrlGaugeCoord3.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord3.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 2 / numberOfCols), startY)

        UserCtrlGaugeCoord4.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord4.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 3 / numberOfCols), startY)

        UserCtrlGaugeCoord5.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord5.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 4 / numberOfCols), startY)

        startY = ctrlHeight + 10
        UserCtrlGaugeCoord6.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord6.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 0 / numberOfCols), startY)

        UserCtrlGaugeCoord7.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord7.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 1 / numberOfCols), startY)

        UserCtrlGaugeCoord8.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord8.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 2 / numberOfCols), startY)

        UserCtrlGaugeCoord9.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols), ctrlHeight)
        UserCtrlGaugeCoord9.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 3 / numberOfCols), startY)

        ' arc gauge not designed w/ borders, so make room for a width border now
        UserCtrlGaugeCoord10.Size = New Drawing.Size(eMath.RInt(MyBase.Width / numberOfCols - 16), ctrlHeight)
        UserCtrlGaugeCoord10.Location = New Drawing.Point(eMath.RInt(MyBase.Width * 4 / numberOfCols + 8), startY)
    End Sub

    ' force resizing in order to properly render
    Private Sub FrmGaugeCoordsOK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyBase.Width -= 1
    End Sub
End Class
