Public Class UserCtrlOneTwo
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
    Friend WithEvents UserCtrlPositionOne As ScopeIII.Forms.UserCtrlPosition
    Friend WithEvents UserCtrlPositionTwo As ScopeIII.Forms.UserCtrlPosition
    Friend WithEvents PanelOne As System.Windows.Forms.Panel
    Friend WithEvents PanelTwo As System.Windows.Forms.Panel
    Friend WithEvents lblOne As System.Windows.Forms.Label
    Friend WithEvents lblTwo As System.Windows.Forms.Label
    Friend WithEvents UserCtrlZ123 As ScopeIII.Forms.UserCtrlZ123
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.UserCtrlPositionOne = New ScopeIII.Forms.UserCtrlPosition
        Me.UserCtrlPositionTwo = New ScopeIII.Forms.UserCtrlPosition
        Me.PanelOne = New System.Windows.Forms.Panel
        Me.PanelTwo = New System.Windows.Forms.Panel
        Me.lblOne = New System.Windows.Forms.Label
        Me.lblTwo = New System.Windows.Forms.Label
        Me.UserCtrlZ123 = New ScopeIII.Forms.UserCtrlZ123
        Me.PanelOne.SuspendLayout()
        Me.PanelTwo.SuspendLayout()
        Me.SuspendLayout()
        '
        'UserCtrlPositionOne
        '
        Me.UserCtrlPositionOne.Location = New System.Drawing.Point(8, 16)
        Me.UserCtrlPositionOne.Name = "UserCtrlPositionOne"
        Me.UserCtrlPositionOne.Size = New System.Drawing.Size(192, 128)
        Me.UserCtrlPositionOne.TabIndex = 0
        '
        'UserCtrlPositionTwo
        '
        Me.UserCtrlPositionTwo.Location = New System.Drawing.Point(8, 16)
        Me.UserCtrlPositionTwo.Name = "UserCtrlPositionTwo"
        Me.UserCtrlPositionTwo.Size = New System.Drawing.Size(192, 128)
        Me.UserCtrlPositionTwo.TabIndex = 1
        '
        'PanelOne
        '
        Me.PanelOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelOne.Controls.Add(Me.UserCtrlPositionOne)
        Me.PanelOne.Location = New System.Drawing.Point(8, 16)
        Me.PanelOne.Name = "PanelOne"
        Me.PanelOne.Size = New System.Drawing.Size(208, 152)
        Me.PanelOne.TabIndex = 2
        '
        'PanelTwo
        '
        Me.PanelTwo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTwo.Controls.Add(Me.UserCtrlPositionTwo)
        Me.PanelTwo.Location = New System.Drawing.Point(240, 16)
        Me.PanelTwo.Name = "PanelTwo"
        Me.PanelTwo.Size = New System.Drawing.Size(208, 152)
        Me.PanelTwo.TabIndex = 3
        '
        'lblOne
        '
        Me.lblOne.Location = New System.Drawing.Point(32, 8)
        Me.lblOne.Name = "lblOne"
        Me.lblOne.Size = New System.Drawing.Size(32, 23)
        Me.lblOne.TabIndex = 4
        Me.lblOne.Text = "One"
        '
        'lblTwo
        '
        Me.lblTwo.Location = New System.Drawing.Point(264, 8)
        Me.lblTwo.Name = "lblTwo"
        Me.lblTwo.Size = New System.Drawing.Size(32, 23)
        Me.lblTwo.TabIndex = 5
        Me.lblTwo.Text = "Two"
        '
        'UserCtrlZ123
        '
        Me.UserCtrlZ123.Location = New System.Drawing.Point(0, 176)
        Me.UserCtrlZ123.Name = "UserCtrlZ123"
        Me.UserCtrlZ123.Size = New System.Drawing.Size(336, 120)
        Me.UserCtrlZ123.TabIndex = 6
        '
        'UserCtrlOneTwo
        '
        Me.Controls.Add(Me.UserCtrlZ123)
        Me.Controls.Add(Me.lblTwo)
        Me.Controls.Add(Me.lblOne)
        Me.Controls.Add(Me.PanelTwo)
        Me.Controls.Add(Me.PanelOne)
        Me.Name = "UserCtrlOneTwo"
        Me.Size = New System.Drawing.Size(456, 304)
        Me.PanelOne.ResumeLayout(False)
        Me.PanelTwo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
