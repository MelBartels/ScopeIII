Public Class UserCtrlPosition
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
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents UserCtrl2AxisCoordEquat As ScopeIII.Forms.UserCtrl2AxisCoord
    Friend WithEvents UserCtrl2AxisCoordAltaz As ScopeIII.Forms.UserCtrl2AxisCoord
    Friend WithEvents UserCtrlCoordSidT As ScopeIII.Forms.UserCtrlCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.UserCtrl2AxisCoordEquat = New ScopeIII.Forms.UserCtrl2AxisCoord
        Me.UserCtrl2AxisCoordAltaz = New ScopeIII.Forms.UserCtrl2AxisCoord
        Me.UserCtrlCoordSidT = New ScopeIII.Forms.UserCtrlCoord
        Me.SuspendLayout()
        '
        'UserCtrl2AxisCoordEquat
        '
        Me.UserCtrl2AxisCoordEquat.Location = New System.Drawing.Point(8, 8)
        Me.UserCtrl2AxisCoordEquat.Name = "UserCtrl2AxisCoordEquat"
        Me.UserCtrl2AxisCoordEquat.Size = New System.Drawing.Size(184, 48)
        Me.UserCtrl2AxisCoordEquat.TabIndex = 2
        '
        'UserCtrl2AxisCoordAltaz
        '
        Me.UserCtrl2AxisCoordAltaz.Location = New System.Drawing.Point(8, 56)
        Me.UserCtrl2AxisCoordAltaz.Name = "UserCtrl2AxisCoordAltaz"
        Me.UserCtrl2AxisCoordAltaz.Size = New System.Drawing.Size(184, 48)
        Me.UserCtrl2AxisCoordAltaz.TabIndex = 3
        '
        'UserCtrlCoordSidT
        '
        Me.UserCtrlCoordSidT.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordSidT.Location = New System.Drawing.Point(8, 104)
        Me.UserCtrlCoordSidT.Name = "UserCtrlCoordSidT"
        Me.UserCtrlCoordSidT.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordSidT.TabIndex = 4
        '
        'UserCtrlPosition
        '
        Me.Controls.Add(Me.UserCtrlCoordSidT)
        Me.Controls.Add(Me.UserCtrl2AxisCoordAltaz)
        Me.Controls.Add(Me.UserCtrl2AxisCoordEquat)
        Me.Name = "UserCtrlPosition"
        Me.Size = New System.Drawing.Size(192, 128)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
