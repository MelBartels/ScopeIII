Public Class UserCtrlZ123
    Inherits MVPUserCtrlBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents chBxUseZ123 As System.Windows.Forms.CheckBox
    Friend WithEvents PanelMain As System.Windows.Forms.Panel
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lblFabErrors As System.Windows.Forms.Label
    Friend WithEvents UserCtrlCoordZ1 As ScopeIII.Forms.UserCtrlCoord
    Friend WithEvents UserCtrlCoordZ2 As ScopeIII.Forms.UserCtrlCoord
    Friend WithEvents UserCtrlCoordZ3 As ScopeIII.Forms.UserCtrlCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.PanelMain = New System.Windows.Forms.Panel
        Me.chBxUseZ123 = New System.Windows.Forms.CheckBox
        Me.lblFabErrors = New System.Windows.Forms.Label
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.UserCtrlCoordZ1 = New ScopeIII.Forms.UserCtrlCoord
        Me.UserCtrlCoordZ2 = New ScopeIII.Forms.UserCtrlCoord
        Me.UserCtrlCoordZ3 = New ScopeIII.Forms.UserCtrlCoord
        Me.PanelMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelMain
        '
        Me.PanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelMain.Controls.Add(Me.UserCtrlCoordZ3)
        Me.PanelMain.Controls.Add(Me.UserCtrlCoordZ2)
        Me.PanelMain.Controls.Add(Me.UserCtrlCoordZ1)
        Me.PanelMain.Controls.Add(Me.chBxUseZ123)
        Me.PanelMain.Location = New System.Drawing.Point(8, 16)
        Me.PanelMain.Name = "PanelMain"
        Me.PanelMain.Size = New System.Drawing.Size(320, 100)
        Me.PanelMain.TabIndex = 9
        '
        'chBxUseZ123
        '
        Me.chBxUseZ123.Checked = True
        Me.chBxUseZ123.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chBxUseZ123.Location = New System.Drawing.Point(8, 24)
        Me.chBxUseZ123.Name = "chBxUseZ123"
        Me.chBxUseZ123.TabIndex = 1
        Me.chBxUseZ123.Text = "Use Corrections"
        '
        'lblFabErrors
        '
        Me.lblFabErrors.Location = New System.Drawing.Point(32, 8)
        Me.lblFabErrors.Name = "lblFabErrors"
        Me.lblFabErrors.Size = New System.Drawing.Size(96, 23)
        Me.lblFabErrors.TabIndex = 10
        Me.lblFabErrors.Text = "FabricationErrors"
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
        'UserCtrlCoordZ1
        '
        Me.UserCtrlCoordZ1.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordZ1.Location = New System.Drawing.Point(128, 16)
        Me.UserCtrlCoordZ1.Name = "UserCtrlCoordZ1"
        Me.UserCtrlCoordZ1.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordZ1.TabIndex = 5
        '
        'UserCtrlCoordZ2
        '
        Me.UserCtrlCoordZ2.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordZ2.Location = New System.Drawing.Point(128, 40)
        Me.UserCtrlCoordZ2.Name = "UserCtrlCoordZ2"
        Me.UserCtrlCoordZ2.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordZ2.TabIndex = 6
        '
        'UserCtrlCoordZ3
        '
        Me.UserCtrlCoordZ3.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordZ3.Location = New System.Drawing.Point(128, 64)
        Me.UserCtrlCoordZ3.Name = "UserCtrlCoordZ3"
        Me.UserCtrlCoordZ3.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordZ3.TabIndex = 7
        '
        'UserCtrlZ123
        '
        Me.Controls.Add(Me.lblFabErrors)
        Me.Controls.Add(Me.PanelMain)
        Me.Name = "UserCtrlZ123"
        Me.Size = New System.Drawing.Size(336, 120)
        Me.PanelMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property UseZ123() As Boolean
        Get
            Return chBxUseZ123.Checked
        End Get
        Set(ByVal Value As Boolean)
            chBxUseZ123.Checked = Value
        End Set
    End Property

    Public Sub SetToolTip()
        ToolTip.SetToolTip(PanelMain, ScopeLibrary.Constants.Z123ToolTip)
        ToolTip.IsBalloon = True
    End Sub
End Class
