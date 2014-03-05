Imports BartelsLibrary.DelegateSigs

Public Class FrmEncodersBox
    Inherits MVPViewContainsGaugeCoordBase
    Implements IFrmEncodersBox

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
    Friend WithEvents UserCtrlEncoderPri As ScopeIII.Forms.UserCtrlEncoder
    Friend WithEvents btnProperties As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnDisplayDevice As System.Windows.Forms.Button
    Friend WithEvents txBxStatus As System.Windows.Forms.TextBox
    Friend WithEvents UserCtrlEncoderSec As ScopeIII.Forms.UserCtrlEncoder
    Friend WithEvents UserCtrlTerminal As ScopeIII.Forms.UserCtrlTerminal
    Friend WithEvents UserCtrlLogging As ScopeIII.Forms.UserCtrlLogging
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEncodersBoxSim))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnProperties = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txBxStatus = New System.Windows.Forms.TextBox
        Me.btnDisplayDevice = New System.Windows.Forms.Button
        Me.UserCtrlEncoderSec = New ScopeIII.Forms.UserCtrlEncoder
        Me.UserCtrlTerminal = New ScopeIII.Forms.UserCtrlTerminal
        Me.UserCtrlLogging = New ScopeIII.Forms.UserCtrlLogging
        Me.UserCtrlEncoderPri = New ScopeIII.Forms.UserCtrlEncoder
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
        'btnProperties
        '
        Me.btnProperties.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnProperties.Location = New System.Drawing.Point(340, 10)
        Me.btnProperties.Name = "btnProperties"
        Me.btnProperties.Size = New System.Drawing.Size(75, 23)
        Me.btnProperties.TabIndex = 1
        Me.btnProperties.Text = "Properties"
        Me.btnProperties.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(764, 10)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txBxStatus
        '
        Me.txBxStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txBxStatus.Location = New System.Drawing.Point(4, 182)
        Me.txBxStatus.Multiline = True
        Me.txBxStatus.Name = "txBxStatus"
        Me.txBxStatus.ReadOnly = True
        Me.txBxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txBxStatus.Size = New System.Drawing.Size(413, 174)
        Me.txBxStatus.TabIndex = 8
        '
        'btnDisplayDevice
        '
        Me.btnDisplayDevice.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnDisplayDevice.Location = New System.Drawing.Point(340, 39)
        Me.btnDisplayDevice.Name = "btnDisplayDevice"
        Me.btnDisplayDevice.Size = New System.Drawing.Size(75, 23)
        Me.btnDisplayDevice.TabIndex = 7
        Me.btnDisplayDevice.Text = "Display"
        Me.btnDisplayDevice.UseVisualStyleBackColor = True
        '
        'UserCtrlEncoderSec
        '
        Me.UserCtrlEncoderSec.Location = New System.Drawing.Point(156, 0)
        Me.UserCtrlEncoderSec.Name = "UserCtrlEncoderSec"
        Me.UserCtrlEncoderSec.Size = New System.Drawing.Size(150, 150)
        Me.UserCtrlEncoderSec.TabIndex = 11
        '
        'UserCtrlTerminal
        '
        Me.UserCtrlTerminal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlTerminal.DisplayTypeDataSource = Nothing
        Me.UserCtrlTerminal.Location = New System.Drawing.Point(429, 68)
        Me.UserCtrlTerminal.Name = "UserCtrlTerminal"
        Me.UserCtrlTerminal.PortTypeDataSource = Nothing
        Me.UserCtrlTerminal.Size = New System.Drawing.Size(420, 291)
        Me.UserCtrlTerminal.TabIndex = 10
        '
        'UserCtrlLogging
        '
        Me.UserCtrlLogging.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlLogging.Location = New System.Drawing.Point(153, 152)
        Me.UserCtrlLogging.LoggingFilename = "Logging Filename"
        Me.UserCtrlLogging.Name = "UserCtrlLogging"
        Me.UserCtrlLogging.Size = New System.Drawing.Size(267, 26)
        Me.UserCtrlLogging.TabIndex = 9
        '
        'UserCtrlEncoderPri
        '
        Me.UserCtrlEncoderPri.Location = New System.Drawing.Point(0, 0)
        Me.UserCtrlEncoderPri.Name = "UserCtrlEncoderPri"
        Me.UserCtrlEncoderPri.Size = New System.Drawing.Size(150, 150)
        Me.UserCtrlEncoderPri.TabIndex = 0
        '
        'FrmEncodersBoxSim
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(848, 359)
        Me.Controls.Add(Me.UserCtrlEncoderSec)
        Me.Controls.Add(Me.UserCtrlTerminal)
        Me.Controls.Add(Me.UserCtrlLogging)
        Me.Controls.Add(Me.txBxStatus)
        Me.Controls.Add(Me.btnDisplayDevice)
        Me.Controls.Add(Me.btnProperties)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.UserCtrlEncoderPri)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmEncodersBoxSim"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Encoders Box Simulator"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event Properties() Implements IFrmEncodersBox.Properties
    Public Event DisplayDevice() Implements IFrmEncodersBox.DisplayDevice
    Public Event CloseForm() Implements IFrmEncodersBox.CloseForm
    Public Event Logging(ByVal switch As Boolean) Implements IFrmEncodersBox.Logging

    Public Property Title() As String Implements IFrmEncodersBox.Title
        Get
            Return Me.Text
        End Get
        Set(ByVal Value As String)
            Me.Text = Value
        End Set
    End Property

    Public Sub DisplayStatus(ByVal msg As String) Implements IFrmEncodersBox.DisplayStatus
        If txBxStatus.InvokeRequired Then
            txBxStatus.Invoke(New DelegateStr(AddressOf DisplayStatus), New Object() {msg})
        Else
            txBxStatus.AppendText(msg & vbCrLf)
            txBxStatus.SelectionStart = txBxStatus.Text.Length
            txBxStatus.ScrollToCaret()
        End If
    End Sub

    Public Sub SetToolTip() Implements IFrmEncodersBox.SetToolTip
        UserCtrlLogging.SetToolTip()
        UserCtrlLogging.SetTxBxLoggingToolTip()

        ToolTip.IsBalloon = True
    End Sub

    Public Function GetUserCtrlEncoderPri() As UserCtrlEncoder Implements IFrmEncodersBox.GetUserCtrlEncoderPri
        Return UserCtrlEncoderPri
    End Function

    Public Function GetUserCtrlEncoderSec() As UserCtrlEncoder Implements IFrmEncodersBox.GetUserCtrlEncoderSec
        Return UserCtrlEncoderSec
    End Function

    Public Function GetUserCtrlTerminal() As UserCtrlTerminal Implements IFrmEncodersBox.GetUserCtrlTerminal
        Return UserCtrlTerminal
    End Function

    Public Function GetUserCtrlLogging() As UserCtrlLogging Implements IFrmEncodersBox.GetUserCtrlLogging
        Return UserCtrlLogging
    End Function

    Protected Sub resizer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Dim gaugeWidth As Int32 = emath.rint((emath.rint(Me.Width / 2) - 100) / 2)
        If gaugeWidth < 1 Then
            gaugeWidth = 1
        End If

        ' allow room for status panel and IOTerminal
        Dim gaugeHeight As Int32 = Me.Height - 240
        If gaugeHeight < 1 Then
            gaugeHeight = 1
        End If

        Dim size As Int32 = gaugeWidth
        If size > gaugeHeight Then
            size = gaugeHeight
        End If

        UserCtrlEncoderPri.Width = size
        UserCtrlEncoderPri.Height = size

        UserCtrlEncoderSec.Location = New Drawing.Point(size, 0)
        UserCtrlEncoderSec.Width = size
        UserCtrlEncoderSec.Height = size
    End Sub

    Private Sub btnProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties.Click
        RaiseEvent Properties()
    End Sub

    Private Sub form_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        RaiseEvent CloseForm()
    End Sub

    Private Sub btnDisplayDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisplayDevice.Click
        RaiseEvent DisplayDevice()
    End Sub

    Private Sub userCtrlLoggingHandler(ByVal switch As Boolean) Handles UserCtrlLogging.Logging
        RaiseEvent Logging(switch)
    End Sub

End Class
