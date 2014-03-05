Public Class UserCtrlGauge2AxisCoord
    Inherits MVPUserCtrlGaugesBase

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
    Friend WithEvents UserCtrlGaugeCoordPri As ScopeIII.Forms.UserCtrlGaugeCoord
    Friend WithEvents UserCtrlGaugeCoordSec As ScopeIII.Forms.UserCtrlGaugeCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.UserCtrlGaugeCoordPri = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.UserCtrlGaugeCoordSec = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.SuspendLayout()
        '
        'UserCtrlGaugeCoordPri
        '
        Me.UserCtrlGaugeCoordPri.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGaugeCoordPri.Name = "UserCtrlGaugeCoordPri"
        Me.UserCtrlGaugeCoordPri.Size = New System.Drawing.Size(184, 200)
        Me.UserCtrlGaugeCoordPri.TabIndex = 0
        '
        'UserCtrlGaugeCoordSec
        '
        Me.UserCtrlGaugeCoordSec.Location = New System.Drawing.Point(184, 0)
        Me.UserCtrlGaugeCoordSec.Name = "UserCtrlGaugeCoordSec"
        Me.UserCtrlGaugeCoordSec.Size = New System.Drawing.Size(184, 200)
        Me.UserCtrlGaugeCoordSec.TabIndex = 1
        '
        'UserCtrlGauge2AxisCoord
        '
        Me.Controls.Add(Me.UserCtrlGaugeCoordSec)
        Me.Controls.Add(Me.UserCtrlGaugeCoordPri)
        Me.Name = "UserCtrlGauge2AxisCoord"
        Me.Size = New System.Drawing.Size(368, 200)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Overrides Sub NewSize(ByVal width As Int32, ByVal height As Int32)
        Dim userCtrlWidth As Int32
        Dim userCtrlHeight As Int32
        Dim secLocationX As Int32
        Dim secLocationY As Int32

        If GaugeLayout Is BartelsLibrary.Layout.Vertical Then
            userCtrlWidth = width
            userCtrlHeight = eMath.RInt(height / 2)
            secLocationX = 0
            secLocationY = userCtrlHeight
        Else
            userCtrlWidth = eMath.RInt(width / 2)
            userCtrlHeight = height
            secLocationX = userCtrlWidth
            secLocationY = 0
        End If

        UserCtrlGaugeCoordPri.Size = New Drawing.Size(userCtrlWidth, userCtrlHeight)
        UserCtrlGaugeCoordSec.Size = New Drawing.Size(userCtrlWidth, userCtrlHeight)
        UserCtrlGaugeCoordSec.Location = New Drawing.Point(secLocationX, secLocationY)
    End Sub

    Protected Overridable Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        NewSize(CType(sender, System.Windows.Forms.UserControl).Width, CType(sender, System.Windows.Forms.UserControl).Height)
    End Sub

    Private Sub MyBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NewSize(MyBase.Width, MyBase.Height)
    End Sub
End Class
