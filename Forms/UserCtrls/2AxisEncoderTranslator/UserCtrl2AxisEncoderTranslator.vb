Public Class UserCtrl2AxisEncoderTranslator
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
    Friend WithEvents UserCtrlAxisEncoderTranslatorPri As ScopeIII.Forms.UserCtrlAxisEncoderTranslator
    Friend WithEvents UserCtrlAxisEncoderTranslatorSec As ScopeIII.Forms.UserCtrlAxisEncoderTranslator
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.UserCtrlAxisEncoderTranslatorPri = New ScopeIII.Forms.UserCtrlAxisEncoderTranslator
        Me.UserCtrlAxisEncoderTranslatorSec = New ScopeIII.Forms.UserCtrlAxisEncoderTranslator
        Me.SuspendLayout()
        '
        'UserCtrlAxisEncoderTranslatorPri
        '
        Me.UserCtrlAxisEncoderTranslatorPri.AxisSelectDataSource = Nothing
        Me.UserCtrlAxisEncoderTranslatorPri.GaugeLayout = Nothing
        Me.UserCtrlAxisEncoderTranslatorPri.GearRatioValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UserCtrlAxisEncoderTranslatorPri.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlAxisEncoderTranslatorPri.Name = "UserCtrlAxisEncoderTranslatorPri"
        Me.UserCtrlAxisEncoderTranslatorPri.Size = New System.Drawing.Size(185, 275)
        Me.UserCtrlAxisEncoderTranslatorPri.TabIndex = 0
        Me.UserCtrlAxisEncoderTranslatorPri.TotalTicksValue = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'UserCtrlAxisEncoderTranslatorSec
        '
        Me.UserCtrlAxisEncoderTranslatorSec.AxisSelectDataSource = Nothing
        Me.UserCtrlAxisEncoderTranslatorSec.GaugeLayout = Nothing
        Me.UserCtrlAxisEncoderTranslatorSec.GearRatioValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UserCtrlAxisEncoderTranslatorSec.Location = New System.Drawing.Point(185, 0)
        Me.UserCtrlAxisEncoderTranslatorSec.Name = "UserCtrlAxisEncoderTranslatorSec"
        Me.UserCtrlAxisEncoderTranslatorSec.Size = New System.Drawing.Size(185, 275)
        Me.UserCtrlAxisEncoderTranslatorSec.TabIndex = 1
        Me.UserCtrlAxisEncoderTranslatorSec.TotalTicksValue = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'UserCtrl2AxisEncoderTranslator
        '
        Me.Controls.Add(Me.UserCtrlAxisEncoderTranslatorSec)
        Me.Controls.Add(Me.UserCtrlAxisEncoderTranslatorPri)
        Me.Name = "UserCtrl2AxisEncoderTranslator"
        Me.Size = New System.Drawing.Size(370, 275)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
