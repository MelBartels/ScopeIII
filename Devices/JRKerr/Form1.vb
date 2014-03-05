Public Class Form1
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents InitButton As System.Windows.Forms.Button
    Friend WithEvents SetGainsButton As System.Windows.Forms.Button
    Friend WithEvents EnableServoButton As System.Windows.Forms.Button
    Friend WithEvents MoveButton As System.Windows.Forms.Button
    Friend WithEvents ReadButton As System.Windows.Forms.Button
    Friend WithEvents ExitButton As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.InitButton = New System.Windows.Forms.Button
        Me.SetGainsButton = New System.Windows.Forms.Button
        Me.EnableServoButton = New System.Windows.Forms.Button
        Me.MoveButton = New System.Windows.Forms.Button
        Me.ReadButton = New System.Windows.Forms.Button
        Me.ExitButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'InitButton
        '
        Me.InitButton.Location = New System.Drawing.Point(12, 8)
        Me.InitButton.Name = "InitButton"
        Me.InitButton.Size = New System.Drawing.Size(232, 24)
        Me.InitButton.TabIndex = 0
        Me.InitButton.Text = "Initialize NMC Network"
        '
        'SetGainsButton
        '
        Me.SetGainsButton.Enabled = False
        Me.SetGainsButton.Location = New System.Drawing.Point(12, 36)
        Me.SetGainsButton.Name = "SetGainsButton"
        Me.SetGainsButton.Size = New System.Drawing.Size(232, 24)
        Me.SetGainsButton.TabIndex = 1
        Me.SetGainsButton.Text = "Set Gains"
        '
        'EnableServoButton
        '
        Me.EnableServoButton.Enabled = False
        Me.EnableServoButton.Location = New System.Drawing.Point(12, 64)
        Me.EnableServoButton.Name = "EnableServoButton"
        Me.EnableServoButton.Size = New System.Drawing.Size(232, 24)
        Me.EnableServoButton.TabIndex = 2
        Me.EnableServoButton.Text = "Enable Servo and  Reset Position"
        '
        'MoveButton
        '
        Me.MoveButton.Enabled = False
        Me.MoveButton.Location = New System.Drawing.Point(12, 92)
        Me.MoveButton.Name = "MoveButton"
        Me.MoveButton.Size = New System.Drawing.Size(232, 24)
        Me.MoveButton.TabIndex = 3
        Me.MoveButton.Text = "Move to pos = 10000"
        '
        'ReadButton
        '
        Me.ReadButton.Enabled = False
        Me.ReadButton.Location = New System.Drawing.Point(12, 120)
        Me.ReadButton.Name = "ReadButton"
        Me.ReadButton.Size = New System.Drawing.Size(232, 24)
        Me.ReadButton.TabIndex = 4
        Me.ReadButton.Text = "Read Position"
        '
        'ExitButton
        '
        Me.ExitButton.Location = New System.Drawing.Point(12, 148)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(232, 24)
        Me.ExitButton.TabIndex = 5
        Me.ExitButton.Text = "Exit"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(260, 177)
        Me.Controls.Add(Me.ExitButton)
        Me.Controls.Add(Me.ReadButton)
        Me.Controls.Add(Me.MoveButton)
        Me.Controls.Add(Me.EnableServoButton)
        Me.Controls.Add(Me.SetGainsButton)
        Me.Controls.Add(Me.InitButton)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PIC-SERVO Visual Basic Example"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim Nummod As Integer

    Private Sub InitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InitButton.Click
        Dim modtype As Integer

        '
        'Initialize the network of controllers on COM1:
        '
        Nummod = NmcInit("COM1:", 19200)

        '
        'Check if at least 1 module is found
        '
        If (Nummod < 1) Then
            MsgBox("PIC-SERVO module not found")
            Exit Sub
        End If

        '
        'Check that module is a PIC-SERVO
        '
        modtype = NmcGetModType(1)
        If (modtype <> SERVOMODTYPE) Then
            MsgBox("PIC-SERVO board not found")
            Exit Sub
        End If

        MsgBox("PIC-SERVO Board Found")

        SetGainsButton.Enabled = True
        ReadButton.Enabled = True
        InitButton.Enabled = False

    End Sub

    Private Sub SetGainsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetGainsButton.Click

        '
        'Set the gains for the servo
        '
        ServoSetGain(1, 100, 1000, 0, 0, 255, 0, 2000, 1, 0)

        EnableServoButton.Enabled = True
        EnableServoButton.Focus()
    End Sub

    Private Sub EnableServoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableServoButton.Click
        '
        'Use the Stop Motor command to both enable the amplifier and
        'with the STOP_ABRUPT option, start servoing to the current position
        '
        ServoStopMotor(1, AMP_ENABLE Or STOP_ABRUPT)

        '
        'Reset the motor position to zero
        '
        ServoResetPos(1)

        MoveButton.Enabled = True
        MoveButton.Focus()
    End Sub

    Private Sub MoveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveButton.Click
        Dim statbyte As Byte

        '
        'Do a trapezoidal position move with pos = 10,000, vel = 100,000, acc = 100
        '
        ServoLoadTraj(1, LOAD_POS Or LOAD_VEL Or LOAD_ACC Or ENABLE_SERVO Or START_NOW, 10000, 100000, 100, 0)

        'Read the status byte in a loop until the MOVE_DONE bit is set
        Do
            NmcNoOp(1)               'NoOp command to read current module status
            statbyte = NmcGetStat(1) 'Fetch the module status byte
        Loop While (statbyte And MOVE_DONE) = 0

        ReadButton.Focus()
    End Sub

    Private Sub ReadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadButton.Click
        Dim pos As Int32
        Dim msg As String

        NmcReadStatus(1, SEND_POS)  'Read the current position data from the cotnroller
        pos = ServoGetPos(1)        'Fetch the position data 

        msg = "Motor position = " & pos
        MsgBox(msg)
    End Sub

    Private Sub ExitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        Close()
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If (Nummod > 0) Then NmcShutdown()
    End Sub
End Class
