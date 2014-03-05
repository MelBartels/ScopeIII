Public Class FrmEnterOneTwo
    Inherits MVPViewBase

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
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblPreset As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chBxEquat As System.Windows.Forms.CheckBox
    Friend WithEvents chBxAltaz As System.Windows.Forms.CheckBox
    Friend WithEvents UserCtrlCoordLat As ScopeIII.Forms.UserCtrlCoord
    Friend WithEvents UserCtrlOneTwo As ScopeIII.Forms.UserCtrlOneTwo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEnterOneTwo))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.UserCtrlOneTwo = New ScopeIII.Forms.UserCtrlOneTwo
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.UserCtrlCoordLat = New ScopeIII.Forms.UserCtrlCoord
        Me.chBxEquat = New System.Windows.Forms.CheckBox
        Me.chBxAltaz = New System.Windows.Forms.CheckBox
        Me.lblPreset = New System.Windows.Forms.Label
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
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
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(293, 390)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(77, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(381, 390)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'UserCtrlOneTwo
        '
        Me.UserCtrlOneTwo.Location = New System.Drawing.Point(8, 80)
        Me.UserCtrlOneTwo.Name = "UserCtrlOneTwo"
        Me.UserCtrlOneTwo.Size = New System.Drawing.Size(456, 304)
        Me.UserCtrlOneTwo.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.UserCtrlCoordLat)
        Me.Panel1.Controls.Add(Me.chBxEquat)
        Me.Panel1.Controls.Add(Me.chBxAltaz)
        Me.Panel1.Location = New System.Drawing.Point(16, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 62)
        Me.Panel1.TabIndex = 5
        '
        'UserCtrlCoordLat
        '
        Me.UserCtrlCoordLat.CoordinateText = "+00d 00m 00s"
        Me.UserCtrlCoordLat.Location = New System.Drawing.Point(128, 19)
        Me.UserCtrlCoordLat.Name = "UserCtrlCoordLat"
        Me.UserCtrlCoordLat.Size = New System.Drawing.Size(184, 24)
        Me.UserCtrlCoordLat.TabIndex = 11
        '
        'chBxEquat
        '
        Me.chBxEquat.AutoSize = True
        Me.chBxEquat.Location = New System.Drawing.Point(18, 33)
        Me.chBxEquat.Name = "chBxEquat"
        Me.chBxEquat.Size = New System.Drawing.Size(73, 17)
        Me.chBxEquat.TabIndex = 9
        Me.chBxEquat.Text = "Equatorial"
        Me.chBxEquat.UseVisualStyleBackColor = True
        '
        'chBxAltaz
        '
        Me.chBxAltaz.AutoSize = True
        Me.chBxAltaz.Location = New System.Drawing.Point(18, 14)
        Me.chBxAltaz.Name = "chBxAltaz"
        Me.chBxAltaz.Size = New System.Drawing.Size(74, 17)
        Me.chBxAltaz.TabIndex = 8
        Me.chBxAltaz.Text = "Altazimuth"
        Me.chBxAltaz.UseVisualStyleBackColor = True
        '
        'lblPreset
        '
        Me.lblPreset.AutoSize = True
        Me.lblPreset.Location = New System.Drawing.Point(36, 5)
        Me.lblPreset.Name = "lblPreset"
        Me.lblPreset.Size = New System.Drawing.Size(42, 13)
        Me.lblPreset.TabIndex = 6
        Me.lblPreset.Text = "Presets"
        '
        'FrmEnterOneTwo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(470, 425)
        Me.Controls.Add(Me.lblPreset)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.UserCtrlOneTwo)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmEnterOneTwo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Enter OneTwo"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event OK()
    Public Event Preset(ByRef alignment As ISFT)

    Public Sub CheckAlign(ByRef alignment As ISFT)
        If alignment Is AlignmentStyle.AltazSiteAligned Then
            chBxAltaz.Checked = True
        Else
            chBxEquat.Checked = True
        End If
    End Sub

    Public Function PresetSelected() As Boolean
        Return chBxAltaz.Checked OrElse chBxEquat.Checked
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RaiseEvent OK()
    End Sub

    Private Sub chBxAltaz_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxAltaz.CheckedChanged
        If chBxAltaz.Checked Then
            chBxEquat.Checked = Not chBxAltaz.Checked
            RaiseEvent Preset(CType(AlignmentStyle.AltazSiteAligned, ISFT))
        End If
    End Sub

    Private Sub chBxEquat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxEquat.CheckedChanged
        If chBxEquat.Checked Then
            chBxAltaz.Checked = Not chBxEquat.Checked
            RaiseEvent Preset(CType(AlignmentStyle.PolarAligned, ISFT))
        End If
    End Sub
End Class
