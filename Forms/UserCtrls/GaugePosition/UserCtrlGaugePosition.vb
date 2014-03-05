Public Class UserCtrlGaugePosition
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
    Friend WithEvents UserCtrlGauge2AxisCoordEquat As ScopeIII.Forms.UserCtrlGauge2AxisCoord
    Friend WithEvents UserCtrlGauge2AxisCoordAltaz As ScopeIII.Forms.UserCtrlGauge2AxisCoord
    Friend WithEvents UserCtrlGaugeCoordSidT As ScopeIII.Forms.UserCtrlGaugeCoord
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.UserCtrlGauge2AxisCoordEquat = New ScopeIII.Forms.UserCtrlGauge2AxisCoord
        Me.UserCtrlGauge2AxisCoordAltaz = New ScopeIII.Forms.UserCtrlGauge2AxisCoord
        Me.UserCtrlGaugeCoordSidT = New ScopeIII.Forms.UserCtrlGaugeCoord
        Me.SuspendLayout()
        '
        'UserCtrlGauge2AxisCoordEquat
        '
        Me.UserCtrlGauge2AxisCoordEquat.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlGauge2AxisCoordEquat.Name = "UserCtrlGauge2AxisCoordEquat"
        Me.UserCtrlGauge2AxisCoordEquat.Size = New System.Drawing.Size(368, 184)
        Me.UserCtrlGauge2AxisCoordEquat.TabIndex = 1
        '
        'UserCtrlGauge2AxisCoordAltaz
        '
        Me.UserCtrlGauge2AxisCoordAltaz.Location = New System.Drawing.Point(368, 0)
        Me.UserCtrlGauge2AxisCoordAltaz.Name = "UserCtrlGauge2AxisCoordAltaz"
        Me.UserCtrlGauge2AxisCoordAltaz.Size = New System.Drawing.Size(368, 184)
        Me.UserCtrlGauge2AxisCoordAltaz.TabIndex = 2
        '
        'UserCtrlGaugeCoordSidT
        '
        Me.UserCtrlGaugeCoordSidT.Location = New System.Drawing.Point(736, 0)
        Me.UserCtrlGaugeCoordSidT.Name = "UserCtrlGaugeCoordSidT"
        Me.UserCtrlGaugeCoordSidT.Size = New System.Drawing.Size(184, 184)
        Me.UserCtrlGaugeCoordSidT.TabIndex = 3
        '
        'UserCtrlGaugePosition
        '
        Me.Controls.Add(Me.UserCtrlGaugeCoordSidT)
        Me.Controls.Add(Me.UserCtrlGauge2AxisCoordAltaz)
        Me.Controls.Add(Me.UserCtrlGauge2AxisCoordEquat)
        Me.Name = "UserCtrlGaugePosition"
        Me.Size = New System.Drawing.Size(920, 184)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property GaugeLayout() As ISFT
        Get
            Return pLayout
        End Get
        Set(ByVal value As ISFT)
            pLayout = value
            UserCtrlGauge2AxisCoordEquat.GaugeLayout = pLayout
            UserCtrlGauge2AxisCoordAltaz.GaugeLayout = pLayout
        End Set
    End Property

    Private pLayout As ISFT = BartelsLibrary.Layout.Horizontal

    Protected Overridable Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If pLayout Is BartelsLibrary.Layout.Horizontal Then
            resizeHoriz()
        Else
            resizeVert()
        End If
    End Sub

    Private Sub resizeHoriz()
        Dim ucHeight As Int32 = Height
        If ucHeight > Width / 5 Then
            ucHeight = eMath.RInt(Width / 5)
        End If
        UserCtrlGauge2AxisCoordEquat.Size = New Drawing.Size(eMath.RInt(Width * 2 / 5), ucHeight)
        UserCtrlGauge2AxisCoordAltaz.Size = New Drawing.Size(eMath.RInt(Width * 2 / 5), ucHeight)
        UserCtrlGaugeCoordSidT.Size = New Drawing.Size(eMath.RInt(Width / 5), ucHeight)
        UserCtrlGauge2AxisCoordEquat.Location = New Drawing.Point(0, 0)
        UserCtrlGauge2AxisCoordAltaz.Location = New Drawing.Point(eMath.RInt(Width * 2 / 5), 0)
        UserCtrlGaugeCoordSidT.Location = New Drawing.Point(eMath.RInt(Width * 4 / 5), 0)
    End Sub

    Private Sub resizeVert()
        Dim ucWidth As Int32 = Width
        ' let width range as wide as possible in order to get the horizontally shaped UserCtrlCoord to fit 
        UserCtrlGauge2AxisCoordEquat.Size = New Drawing.Size(ucWidth, eMath.RInt(Height * 2 / 5))
        UserCtrlGauge2AxisCoordAltaz.Size = New Drawing.Size(ucWidth, eMath.RInt(Height * 2 / 5))
        UserCtrlGaugeCoordSidT.Size = New Drawing.Size(ucWidth, eMath.RInt(Height / 5))
        UserCtrlGauge2AxisCoordEquat.Location = New Drawing.Point(0, 0)
        UserCtrlGauge2AxisCoordAltaz.Location = New Drawing.Point(0, eMath.RInt(Height * 2 / 5))
        UserCtrlGaugeCoordSidT.Location = New Drawing.Point(0, eMath.RInt(Height * 4 / 5))
    End Sub
End Class
