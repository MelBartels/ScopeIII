Public Class UserCtrlLogging
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
    Friend WithEvents chbxLog As System.Windows.Forms.CheckBox
    Friend WithEvents txbxLoggingFilename As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.chbxLog = New System.Windows.Forms.CheckBox
        Me.txbxLoggingFilename = New System.Windows.Forms.TextBox
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
        'chbxLog
        '
        Me.chbxLog.AutoSize = True
        Me.chbxLog.Location = New System.Drawing.Point(187, 5)
        Me.chbxLog.Name = "chbxLog"
        Me.chbxLog.Size = New System.Drawing.Size(79, 17)
        Me.chbxLog.TabIndex = 22
        Me.chbxLog.Text = "Log To File"
        Me.chbxLog.UseVisualStyleBackColor = True
        '
        'txbxLoggingFilename
        '
        Me.txbxLoggingFilename.Location = New System.Drawing.Point(3, 3)
        Me.txbxLoggingFilename.Name = "txbxLoggingFilename"
        Me.txbxLoggingFilename.Size = New System.Drawing.Size(176, 20)
        Me.txbxLoggingFilename.TabIndex = 23
        Me.txbxLoggingFilename.Text = "Logging Filename"
        '
        'UserCtrlTerminal
        '
        Me.Controls.Add(Me.txbxLoggingFilename)
        Me.Controls.Add(Me.chbxLog)
        Me.Name = "UserCtrlTerminal"
        Me.Size = New System.Drawing.Size(267, 26)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event Logging(ByVal switch As Boolean)

    Private pChangeChBxLoggingUnderway As Boolean

    Public Property LoggingFilename() As String
        Get
            Return txbxLoggingFilename.Text
        End Get
        Set(ByVal value As String)
            txbxLoggingFilename.Text = value
            setTxBxLoggingToolTip()
        End Set
    End Property

    Public Sub SetToolTip()
        ToolTip.SetToolTip(chbxLog, "Log activity to a file.")
        ToolTip.IsBalloon = True
    End Sub

    Public Sub SetTxBxLoggingToolTip()
        ToolTip.SetToolTip(txbxLoggingFilename, "Filename of log: " & LoggingFilename)
    End Sub

    Public Sub ChangeChBxLogging(ByVal switch As Boolean)
        pChangeChBxLoggingUnderway = True
        chbxLog.Checked = switch
        pChangeChBxLoggingUnderway = False
    End Sub

    Private Sub chbxLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbxLog.CheckedChanged
        txbxLoggingFilename.ReadOnly = chbxLog.Checked
        If Not pChangeChBxLoggingUnderway Then
            RaiseEvent Logging(chbxLog.Checked)
        End If
    End Sub

End Class
