Public Class UserCtrl2AxisCoord
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
    Friend WithEvents UserCtrlCoordPri As ScopeIII.Forms.UserCtrlCoord
    Friend WithEvents UserCtrlCoordSec As ScopeIII.Forms.UserCtrlCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.UserCtrlCoordPri = New ScopeIII.Forms.UserCtrlCoord
        Me.UserCtrlCoordSec = New ScopeIII.Forms.UserCtrlCoord
        Me.SuspendLayout()
        '
        'UserCtrlCoordPri
        '
        Me.UserCtrlCoordPri.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordPri.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlCoordPri.Name = "UserCtrlCoordPri"
        Me.UserCtrlCoordPri.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordPri.TabIndex = 0
        '
        'UserCtrlCoordSec
        '
        Me.UserCtrlCoordSec.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordSec.Location = New System.Drawing.Point(0, 24)
        Me.UserCtrlCoordSec.Name = "UserCtrlCoordSec"
        Me.UserCtrlCoordSec.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordSec.TabIndex = 1
        '
        'UserCtrl2AxisCoord
        '
        Me.Controls.Add(Me.UserCtrlCoordSec)
        Me.Controls.Add(Me.UserCtrlCoordPri)
        Me.Name = "UserCtrl2AxisCoord"
        Me.Size = New System.Drawing.Size(184, 48)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
