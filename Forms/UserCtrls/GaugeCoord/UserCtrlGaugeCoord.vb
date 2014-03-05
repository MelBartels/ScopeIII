Public Class UserCtrlGaugeCoord
    Inherits MVPUserCtrlBase

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
    Friend WithEvents UserCtrlCoord As ScopeIII.Forms.UserCtrlCoord
    Friend WithEvents MvpUserCtrlGaugeBase As BartelsLibrary.MVPUserCtrlGaugeBase
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.UserCtrlCoord = New ScopeIII.Forms.UserCtrlCoord
        Me.MvpUserCtrlGaugeBase = New BartelsLibrary.MVPUserCtrlGaugeBase
        Me.SuspendLayout()
        '
        'UserCtrlCoord
        '
        Me.UserCtrlCoord.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.UserCtrlCoord.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoord.Location = New System.Drawing.Point(0, 176)
        Me.UserCtrlCoord.Name = "UserCtrlCoord"
        Me.UserCtrlCoord.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoord.TabIndex = 7
        '
        'MvpUserCtrlGaugeBase
        '
        Me.MvpUserCtrlGaugeBase.IRenderer = Nothing
        Me.MvpUserCtrlGaugeBase.Location = New System.Drawing.Point(0, 0)
        Me.MvpUserCtrlGaugeBase.Name = "MvpUserCtrlGaugeBase"
        Me.MvpUserCtrlGaugeBase.Size = New System.Drawing.Size(176, 176)
        Me.MvpUserCtrlGaugeBase.TabIndex = 8
        '
        'UserCtrlGaugeCoord
        '
        Me.Controls.Add(Me.MvpUserCtrlGaugeBase)
        Me.Controls.Add(Me.UserCtrlCoord)
        Me.Name = "UserCtrlGaugeCoord"
        Me.Size = New System.Drawing.Size(184, 200)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event MeasurementToPoint(ByVal value As Double)

    Protected Sub resizer(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        MvpUserCtrlGaugeBase.NewSize(MyBase.Width, MyBase.Height - UserCtrlCoord.Height)
        Dim x As Int32 = eMath.RInt((MyBase.Width - UserCtrlCoord.Width) / 2)
        Dim y As Int32 = MvpUserCtrlGaugeBase.Height
        UserCtrlCoord.Location = New Drawing.Point(x, y)
    End Sub

    Private Sub gaugeMeasurementToPoint(ByVal value As Double) Handles MvpUserCtrlGaugeBase.MeasurementToPoint
        UserCtrlCoord.CoordUpdatedByMe = True
        RaiseEvent MeasurementToPoint(value)
    End Sub
End Class
