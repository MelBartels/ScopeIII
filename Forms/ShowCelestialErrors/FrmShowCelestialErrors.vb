Public Class FrmShowCelestialErrors
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
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents UserCtrlCelestialErrors As ScopeIII.Forms.UserCtrlCelestialErrors
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerToEpoch As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCoordYear As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFromEpoch As System.Windows.Forms.DateTimePicker
    Friend WithEvents chBxIncludeRefraction As System.Windows.Forms.CheckBox
    Friend WithEvents chBxIncludeNutationAnnualAberration As System.Windows.Forms.CheckBox
    Friend WithEvents chBxIncludePrecession As System.Windows.Forms.CheckBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmShowCelestialErrors))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.UserCtrlCelestialErrors = New ScopeIII.Forms.UserCtrlCelestialErrors
        Me.lblCoordYear = New System.Windows.Forms.Label
        Me.DateTimePickerFromEpoch = New System.Windows.Forms.DateTimePicker
        Me.lblTo = New System.Windows.Forms.Label
        Me.DateTimePickerToEpoch = New System.Windows.Forms.DateTimePicker
        Me.chBxIncludeNutationAnnualAberration = New System.Windows.Forms.CheckBox
        Me.chBxIncludePrecession = New System.Windows.Forms.CheckBox
        Me.chBxIncludeRefraction = New System.Windows.Forms.CheckBox
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
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(310, 331)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(220, 331)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'UserCtrlCelestialErrors
        '
        Me.UserCtrlCelestialErrors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserCtrlCelestialErrors.CelestialErrorsDataSource = Nothing
        Me.UserCtrlCelestialErrors.Location = New System.Drawing.Point(12, 67)
        Me.UserCtrlCelestialErrors.Name = "UserCtrlCelestialErrors"
        Me.UserCtrlCelestialErrors.Size = New System.Drawing.Size(373, 250)
        Me.UserCtrlCelestialErrors.TabIndex = 6
        '
        'lblCoordYear
        '
        Me.lblCoordYear.Location = New System.Drawing.Point(12, 12)
        Me.lblCoordYear.Name = "lblCoordYear"
        Me.lblCoordYear.Size = New System.Drawing.Size(88, 23)
        Me.lblCoordYear.TabIndex = 122
        Me.lblCoordYear.Text = "Coordinate Year"
        Me.lblCoordYear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DateTimePickerFromEpoch
        '
        Me.DateTimePickerFromEpoch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFromEpoch.Location = New System.Drawing.Point(106, 12)
        Me.DateTimePickerFromEpoch.Name = "DateTimePickerFromEpoch"
        Me.DateTimePickerFromEpoch.Size = New System.Drawing.Size(85, 20)
        Me.DateTimePickerFromEpoch.TabIndex = 121
        Me.DateTimePickerFromEpoch.Value = New Date(2005, 1, 1, 0, 0, 0, 0)
        '
        'lblTo
        '
        Me.lblTo.Location = New System.Drawing.Point(12, 35)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(88, 23)
        Me.lblTo.TabIndex = 124
        Me.lblTo.Text = "Calculate For"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DateTimePickerToEpoch
        '
        Me.DateTimePickerToEpoch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerToEpoch.Location = New System.Drawing.Point(106, 35)
        Me.DateTimePickerToEpoch.Name = "DateTimePickerToEpoch"
        Me.DateTimePickerToEpoch.Size = New System.Drawing.Size(85, 20)
        Me.DateTimePickerToEpoch.TabIndex = 123
        Me.DateTimePickerToEpoch.Value = New Date(2006, 11, 30, 0, 0, 0, 0)
        '
        'chBxIncludeNutationAnnualAberration
        '
        Me.chBxIncludeNutationAnnualAberration.AutoSize = True
        Me.chBxIncludeNutationAnnualAberration.Checked = True
        Me.chBxIncludeNutationAnnualAberration.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chBxIncludeNutationAnnualAberration.Location = New System.Drawing.Point(238, 28)
        Me.chBxIncludeNutationAnnualAberration.Name = "chBxIncludeNutationAnnualAberration"
        Me.chBxIncludeNutationAnnualAberration.Size = New System.Drawing.Size(152, 17)
        Me.chBxIncludeNutationAnnualAberration.TabIndex = 126
        Me.chBxIncludeNutationAnnualAberration.Text = "Nutation/AnnualAberration"
        Me.chBxIncludeNutationAnnualAberration.UseVisualStyleBackColor = True
        '
        'chBxIncludePrecession
        '
        Me.chBxIncludePrecession.AutoSize = True
        Me.chBxIncludePrecession.Checked = True
        Me.chBxIncludePrecession.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chBxIncludePrecession.Location = New System.Drawing.Point(238, 9)
        Me.chBxIncludePrecession.Name = "chBxIncludePrecession"
        Me.chBxIncludePrecession.Size = New System.Drawing.Size(78, 17)
        Me.chBxIncludePrecession.TabIndex = 125
        Me.chBxIncludePrecession.Text = "Precession"
        Me.chBxIncludePrecession.UseVisualStyleBackColor = True
        '
        'chBxIncludeRefraction
        '
        Me.chBxIncludeRefraction.AutoSize = True
        Me.chBxIncludeRefraction.Checked = True
        Me.chBxIncludeRefraction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chBxIncludeRefraction.Location = New System.Drawing.Point(238, 47)
        Me.chBxIncludeRefraction.Name = "chBxIncludeRefraction"
        Me.chBxIncludeRefraction.Size = New System.Drawing.Size(75, 17)
        Me.chBxIncludeRefraction.TabIndex = 127
        Me.chBxIncludeRefraction.Text = "Refraction"
        Me.chBxIncludeRefraction.UseVisualStyleBackColor = True
        '
        'FrmShowCelestialErrors
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(397, 366)
        Me.Controls.Add(Me.chBxIncludeRefraction)
        Me.Controls.Add(Me.chBxIncludeNutationAnnualAberration)
        Me.Controls.Add(Me.chBxIncludePrecession)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.DateTimePickerToEpoch)
        Me.Controls.Add(Me.lblCoordYear)
        Me.Controls.Add(Me.DateTimePickerFromEpoch)
        Me.Controls.Add(Me.UserCtrlCelestialErrors)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmShowCelestialErrors"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Show Celestial Position Errors"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event PrecessionCheckChanged(ByVal include As Boolean)
    Public Event NutationAnnualAberrationCheckChanged(ByVal include As Boolean)
    Public Event RefractionCheckChanged(ByVal include As Boolean)
    Public Event FromEpochChanged()
    Public Event ToEpochChanged()
    Public Event Invisible()

    Public FormLoadCallbackDelegate As [Delegate]

    Public Property FromEpoch() As DateTime
        Get
            Return CType(DateTimePickerFromEpoch, DateTimePicker).Value
        End Get
        Set(ByVal value As DateTime)
            CType(DateTimePickerFromEpoch, DateTimePicker).Value = value
        End Set
    End Property

    Public Property ToEpoch() As DateTime
        Get
            Return CType(DateTimePickerToEpoch, DateTimePicker).Value
        End Get
        Set(ByVal value As DateTime)
            CType(DateTimePickerToEpoch, DateTimePicker).Value = value
        End Set
    End Property

    Public Property RefractionChecked() As Boolean
        Get
            Return chBxIncludeRefraction.Checked
        End Get
        Set(ByVal value As Boolean)
            chBxIncludeRefraction.Checked = value
        End Set
    End Property

    Public Property IncludeRefractionVisible() As Boolean
        Get
            Return chBxIncludeRefraction.Visible
        End Get
        Set(ByVal value As Boolean)
            chBxIncludeRefraction.Visible = value
        End Set
    End Property

    Private Sub FrmShowCelestialErrors_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If FormLoadCallbackDelegate IsNot Nothing Then
            FormLoadCallbackDelegate.DynamicInvoke()
            'Try
            '    FormLoadCallbackDelegate.DynamicInvoke()
            'Catch tie As System.Reflection.TargetInvocationException
            '    ' avoid nunit assertion failure causing an exception here
            'End Try
        End If
    End Sub

    Private Sub FrmShowCelestialErrors_Closing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible = False Then
            RaiseEvent Invisible()
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        MyBase.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        MyBase.Close()
    End Sub

    Private Sub chBxIncludePrecession_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxIncludePrecession.CheckedChanged
        RaiseEvent PrecessionCheckChanged(chBxIncludePrecession.Checked)
    End Sub

    Private Sub chBxIncludeNutationAnnualAberration_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxIncludeNutationAnnualAberration.CheckedChanged
        RaiseEvent NutationAnnualAberrationCheckChanged(chBxIncludeNutationAnnualAberration.Checked)
    End Sub

    Private Sub chBxRefraction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBxIncludeRefraction.CheckedChanged
        RaiseEvent RefractionCheckChanged(chBxIncludeRefraction.Checked)
    End Sub

    Private Sub DateTimePickerFromEpoch_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerFromEpoch.ValueChanged
        RaiseEvent FromEpochChanged()
    End Sub

    Private Sub DateTimePickerTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerToEpoch.ValueChanged
        RaiseEvent ToEpochChanged()
    End Sub
End Class
