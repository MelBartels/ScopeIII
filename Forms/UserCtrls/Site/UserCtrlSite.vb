Public Class UserCtrlSite
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
    Public WithEvents txBxName As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents UserCtrlCoordLatitude As ScopeIII.Forms.UserCtrlCoord
    Friend WithEvents UserCtrlCoordLongitude As ScopeIII.Forms.UserCtrlCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.txBxName = New System.Windows.Forms.TextBox
        Me.lblTitle = New System.Windows.Forms.Label
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.UserCtrlCoordLatitude = New ScopeIII.Forms.UserCtrlCoord
        Me.UserCtrlCoordLongitude = New ScopeIII.Forms.UserCtrlCoord
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txBxName
        '
        Me.txBxName.Location = New System.Drawing.Point(100, 0)
        Me.txBxName.Name = "txBxName"
        Me.txBxName.Size = New System.Drawing.Size(88, 20)
        Me.txBxName.TabIndex = 114
        '
        'lblTitle
        '
        Me.lblTitle.Location = New System.Drawing.Point(8, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(88, 23)
        Me.lblTitle.TabIndex = 115
        Me.lblTitle.Text = "Site Name"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'UserCtrlCoordLatitude
        '
        Me.UserCtrlCoordLatitude.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordLatitude.Location = New System.Drawing.Point(8, 24)
        Me.UserCtrlCoordLatitude.Name = "UserCtrlCoordLatitude"
        Me.UserCtrlCoordLatitude.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordLatitude.TabIndex = 118
        '
        'UserCtrlCoordLongitude
        '
        Me.UserCtrlCoordLongitude.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordLongitude.Location = New System.Drawing.Point(8, 48)
        Me.UserCtrlCoordLongitude.Name = "UserCtrlCoordLongitude"
        Me.UserCtrlCoordLongitude.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordLongitude.TabIndex = 119
        '
        'UserCtrlSite
        '
        Me.Controls.Add(Me.UserCtrlCoordLongitude)
        Me.Controls.Add(Me.UserCtrlCoordLatitude)
        Me.Controls.Add(Me.txBxName)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "UserCtrlSite"
        Me.Size = New System.Drawing.Size(192, 72)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event NewSiteName(ByVal name As String)

    Private Sub txBx_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txBxName.Leave
        RaiseEvent NewSiteName(txBxName.Text)
    End Sub
End Class
