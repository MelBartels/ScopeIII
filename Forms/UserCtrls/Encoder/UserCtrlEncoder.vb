Public Class UserCtrlEncoder
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
    Friend WithEvents MvpUserCtrlGaugeBase As BartelsLibrary.MVPUserCtrlGaugeBase

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.MvpUserCtrlGaugeBase = New BartelsLibrary.MVPUserCtrlGaugeBase
        Me.SuspendLayout()
        '
        'MvpUserCtrlGaugeBase
        '
        Me.MvpUserCtrlGaugeBase.IRenderer = Nothing
        Me.MvpUserCtrlGaugeBase.Location = New System.Drawing.Point(0, 0)
        Me.MvpUserCtrlGaugeBase.Name = "MvpUserCtrlGaugeBase"
        Me.MvpUserCtrlGaugeBase.Size = New System.Drawing.Size(200, 200)
        Me.MvpUserCtrlGaugeBase.TabIndex = 9
        '
        'UserCtrlEncoder
        '
        Me.Controls.Add(Me.MvpUserCtrlGaugeBase)
        Me.Name = "UserCtrlEncoder"
        Me.Size = New System.Drawing.Size(200, 200)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event MeasurementToPoint(ByVal value As Double)

    Protected Overridable Sub resizer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Dim size As Int32 = Me.Width
        Dim height As Int32 = Me.Height
        If size > height Then
            size = height
        End If
        MvpUserCtrlGaugeBase.Size = New Drawing.Size(size, size)
    End Sub

    Private Sub gaugeMeasurementToPoint(ByVal value As Double) Handles MvpUserCtrlGaugeBase.MeasurementToPoint
        RaiseEvent MeasurementToPoint(value)
    End Sub
End Class
