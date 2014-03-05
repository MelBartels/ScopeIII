Public Class UserCtrlCoordErrors
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
    Friend WithEvents lblUncorrected As System.Windows.Forms.Label
    Friend WithEvents lblCorrected As System.Windows.Forms.Label
    Friend WithEvents lblPrecession As System.Windows.Forms.Label
    Friend WithEvents lblNutation As System.Windows.Forms.Label
    Friend WithEvents lblAnnualAbberation As System.Windows.Forms.Label
    Friend WithEvents UserCtrl2AxisCoordUncorrected As ScopeIII.Forms.UserCtrl2AxisCoord
    Friend WithEvents UserCtrl2AxisCoordCorrected As ScopeIII.Forms.UserCtrl2AxisCoord
    Friend WithEvents UserCtrl2AxisCoordNutation As ScopeIII.Forms.UserCtrl2AxisCoord
    Friend WithEvents UserCtrl2AxisCoordAnnualAberration As ScopeIII.Forms.UserCtrl2AxisCoord
    Friend WithEvents UserCtrl2AxisCoordPrecession As ScopeIII.Forms.UserCtrl2AxisCoord
    Friend WithEvents DateTimePickerCorrected As System.Windows.Forms.DateTimePicker
    Public WithEvents txBxEpoch As System.Windows.Forms.TextBox
    Friend WithEvents lblUncorrectedEpoch As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents chBoxIncludeNutationAnnualAberration As System.Windows.Forms.CheckBox
    Friend WithEvents chBoxIncludePrecession As System.Windows.Forms.CheckBox
    Friend WithEvents lblCorrectedEpoch As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.UserCtrl2AxisCoordUncorrected = New ScopeIII.Forms.UserCtrl2AxisCoord
        Me.UserCtrl2AxisCoordCorrected = New ScopeIII.Forms.UserCtrl2AxisCoord
        Me.UserCtrl2AxisCoordNutation = New ScopeIII.Forms.UserCtrl2AxisCoord
        Me.UserCtrl2AxisCoordAnnualAberration = New ScopeIII.Forms.UserCtrl2AxisCoord
        Me.UserCtrl2AxisCoordPrecession = New ScopeIII.Forms.UserCtrl2AxisCoord
        Me.lblUncorrected = New System.Windows.Forms.Label
        Me.lblCorrected = New System.Windows.Forms.Label
        Me.lblPrecession = New System.Windows.Forms.Label
        Me.lblNutation = New System.Windows.Forms.Label
        Me.lblAnnualAbberation = New System.Windows.Forms.Label
        Me.DateTimePickerCorrected = New System.Windows.Forms.DateTimePicker
        Me.txBxEpoch = New System.Windows.Forms.TextBox
        Me.lblUncorrectedEpoch = New System.Windows.Forms.Label
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.chBoxIncludeNutationAnnualAberration = New System.Windows.Forms.CheckBox
        Me.chBoxIncludePrecession = New System.Windows.Forms.CheckBox
        Me.lblCorrectedEpoch = New System.Windows.Forms.Label
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UserCtrl2AxisCoordUncorrected
        '
        Me.UserCtrl2AxisCoordUncorrected.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrl2AxisCoordUncorrected.Location = New System.Drawing.Point(24, 32)
        Me.UserCtrl2AxisCoordUncorrected.Name = "UserCtrl2AxisCoordUncorrected"
        Me.UserCtrl2AxisCoordUncorrected.Size = New System.Drawing.Size(184, 48)
        Me.UserCtrl2AxisCoordUncorrected.TabIndex = 1
        '
        'UserCtrl2AxisCoordCorrected
        '
        Me.UserCtrl2AxisCoordCorrected.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrl2AxisCoordCorrected.Location = New System.Drawing.Point(24, 168)
        Me.UserCtrl2AxisCoordCorrected.Name = "UserCtrl2AxisCoordCorrected"
        Me.UserCtrl2AxisCoordCorrected.Size = New System.Drawing.Size(184, 48)
        Me.UserCtrl2AxisCoordCorrected.TabIndex = 3
        '
        'UserCtrl2AxisCoordNutation
        '
        Me.UserCtrl2AxisCoordNutation.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrl2AxisCoordNutation.Location = New System.Drawing.Point(232, 112)
        Me.UserCtrl2AxisCoordNutation.Name = "UserCtrl2AxisCoordNutation"
        Me.UserCtrl2AxisCoordNutation.Size = New System.Drawing.Size(184, 48)
        Me.UserCtrl2AxisCoordNutation.TabIndex = 4
        '
        'UserCtrl2AxisCoordAnnualAberration
        '
        Me.UserCtrl2AxisCoordAnnualAberration.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrl2AxisCoordAnnualAberration.Location = New System.Drawing.Point(232, 192)
        Me.UserCtrl2AxisCoordAnnualAberration.Name = "UserCtrl2AxisCoordAnnualAberration"
        Me.UserCtrl2AxisCoordAnnualAberration.Size = New System.Drawing.Size(184, 48)
        Me.UserCtrl2AxisCoordAnnualAberration.TabIndex = 5
        '
        'UserCtrl2AxisCoordPrecession
        '
        Me.UserCtrl2AxisCoordPrecession.IMVPUserCtrlPresenter = Nothing
        Me.UserCtrl2AxisCoordPrecession.Location = New System.Drawing.Point(232, 32)
        Me.UserCtrl2AxisCoordPrecession.Name = "UserCtrl2AxisCoordPrecession"
        Me.UserCtrl2AxisCoordPrecession.Size = New System.Drawing.Size(184, 48)
        Me.UserCtrl2AxisCoordPrecession.TabIndex = 6
        '
        'lblUncorrected
        '
        Me.lblUncorrected.Location = New System.Drawing.Point(24, 8)
        Me.lblUncorrected.Name = "lblUncorrected"
        Me.lblUncorrected.Size = New System.Drawing.Size(136, 23)
        Me.lblUncorrected.TabIndex = 7
        Me.lblUncorrected.Text = "Uncorrected Coordinates"
        Me.lblUncorrected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCorrected
        '
        Me.lblCorrected.Location = New System.Drawing.Point(24, 144)
        Me.lblCorrected.Name = "lblCorrected"
        Me.lblCorrected.Size = New System.Drawing.Size(128, 23)
        Me.lblCorrected.TabIndex = 8
        Me.lblCorrected.Text = "Corrected Coordinates"
        Me.lblCorrected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPrecession
        '
        Me.lblPrecession.Location = New System.Drawing.Point(232, 8)
        Me.lblPrecession.Name = "lblPrecession"
        Me.lblPrecession.Size = New System.Drawing.Size(95, 23)
        Me.lblPrecession.TabIndex = 9
        Me.lblPrecession.Text = "Precession"
        Me.lblPrecession.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNutation
        '
        Me.lblNutation.Location = New System.Drawing.Point(232, 88)
        Me.lblNutation.Name = "lblNutation"
        Me.lblNutation.Size = New System.Drawing.Size(95, 23)
        Me.lblNutation.TabIndex = 10
        Me.lblNutation.Text = "Nutation"
        Me.lblNutation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAnnualAbberation
        '
        Me.lblAnnualAbberation.Location = New System.Drawing.Point(232, 168)
        Me.lblAnnualAbberation.Name = "lblAnnualAbberation"
        Me.lblAnnualAbberation.Size = New System.Drawing.Size(95, 23)
        Me.lblAnnualAbberation.TabIndex = 11
        Me.lblAnnualAbberation.Text = "Annual Aberration"
        Me.lblAnnualAbberation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DateTimePickerCorrected
        '
        Me.DateTimePickerCorrected.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerCorrected.Location = New System.Drawing.Point(82, 216)
        Me.DateTimePickerCorrected.Name = "DateTimePickerCorrected"
        Me.DateTimePickerCorrected.Size = New System.Drawing.Size(122, 20)
        Me.DateTimePickerCorrected.TabIndex = 3
        Me.DateTimePickerCorrected.Value = New Date(2005, 5, 27, 21, 54, 21, 641)
        '
        'txBxEpoch
        '
        Me.txBxEpoch.Location = New System.Drawing.Point(152, 80)
        Me.txBxEpoch.Name = "txBxEpoch"
        Me.txBxEpoch.Size = New System.Drawing.Size(50, 20)
        Me.txBxEpoch.TabIndex = 2
        '
        'lblUncorrectedEpoch
        '
        Me.lblUncorrectedEpoch.Location = New System.Drawing.Point(0, 80)
        Me.lblUncorrectedEpoch.Name = "lblUncorrectedEpoch"
        Me.lblUncorrectedEpoch.Size = New System.Drawing.Size(144, 23)
        Me.lblUncorrectedEpoch.TabIndex = 117
        Me.lblUncorrectedEpoch.Text = "Epoch (Coordinates' Year)"
        Me.lblUncorrectedEpoch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'chBoxIncludeNutationAnnualAberration
        '
        Me.chBoxIncludeNutationAnnualAberration.AutoSize = True
        Me.chBoxIncludeNutationAnnualAberration.Checked = True
        Me.chBoxIncludeNutationAnnualAberration.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chBoxIncludeNutationAnnualAberration.Location = New System.Drawing.Point(27, 124)
        Me.chBoxIncludeNutationAnnualAberration.Name = "chBoxIncludeNutationAnnualAberration"
        Me.chBoxIncludeNutationAnnualAberration.Size = New System.Drawing.Size(152, 17)
        Me.chBoxIncludeNutationAnnualAberration.TabIndex = 119
        Me.chBoxIncludeNutationAnnualAberration.Text = "Nutation/AnnualAberration"
        Me.chBoxIncludeNutationAnnualAberration.UseVisualStyleBackColor = True
        '
        'chBoxIncludePrecession
        '
        Me.chBoxIncludePrecession.AutoSize = True
        Me.chBoxIncludePrecession.Checked = True
        Me.chBoxIncludePrecession.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chBoxIncludePrecession.Location = New System.Drawing.Point(27, 112)
        Me.chBoxIncludePrecession.Name = "chBoxIncludePrecession"
        Me.chBoxIncludePrecession.Size = New System.Drawing.Size(78, 17)
        Me.chBoxIncludePrecession.TabIndex = 118
        Me.chBoxIncludePrecession.Text = "Precession"
        Me.chBoxIncludePrecession.UseVisualStyleBackColor = True
        '
        'lblCorrectedEpoch
        '
        Me.lblCorrectedEpoch.Location = New System.Drawing.Point(33, 216)
        Me.lblCorrectedEpoch.Name = "lblCorrectedEpoch"
        Me.lblCorrectedEpoch.Size = New System.Drawing.Size(41, 23)
        Me.lblCorrectedEpoch.TabIndex = 120
        Me.lblCorrectedEpoch.Text = "Epoch"
        Me.lblCorrectedEpoch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UserCtrlCoordErrors
        '
        Me.Controls.Add(Me.lblCorrectedEpoch)
        Me.Controls.Add(Me.chBoxIncludeNutationAnnualAberration)
        Me.Controls.Add(Me.chBoxIncludePrecession)
        Me.Controls.Add(Me.txBxEpoch)
        Me.Controls.Add(Me.lblUncorrectedEpoch)
        Me.Controls.Add(Me.DateTimePickerCorrected)
        Me.Controls.Add(Me.lblAnnualAbberation)
        Me.Controls.Add(Me.lblNutation)
        Me.Controls.Add(Me.lblPrecession)
        Me.Controls.Add(Me.lblCorrected)
        Me.Controls.Add(Me.lblUncorrected)
        Me.Controls.Add(Me.UserCtrl2AxisCoordPrecession)
        Me.Controls.Add(Me.UserCtrl2AxisCoordAnnualAberration)
        Me.Controls.Add(Me.UserCtrl2AxisCoordNutation)
        Me.Controls.Add(Me.UserCtrl2AxisCoordCorrected)
        Me.Controls.Add(Me.UserCtrl2AxisCoordUncorrected)
        Me.Name = "UserCtrlCoordErrors"
        Me.Size = New System.Drawing.Size(416, 248)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Overrides Function DefaultUserCtrlPresenter() As IMVPUserCtrlPresenter
        Return initDefaultUserCtrlPresenter(UserCtrlCoordErrorsPresenter.GetInstance)
    End Function

    Public Event CalcErrors()

    Public Property EpochText() As String
        Get
            Return txBxEpoch.Text
        End Get
        Set(ByVal Value As String)
            txBxEpoch.Text = Value
        End Set
    End Property

    Public ReadOnly Property DateTimePickerValue() As Date
        Get
            Return DateTimePickerCorrected.Value
        End Get
    End Property

    Public ReadOnly Property IncludePrecessionChecked() As Boolean
        Get
            Return chBoxIncludePrecession.Checked
        End Get
    End Property

    Public ReadOnly Property IncludeNutationAnnualAberrationChecked() As Boolean
        Get
            Return chBoxIncludeNutationAnnualAberration.Checked
        End Get
    End Property

    Public Function ValidateEpoch() As Boolean
        Return ScopeIII.Forms.Validate.GetInstance.ValidateEpoch(ErrorProvider, txBxEpoch)
    End Function

    Private Sub UserCtrlCoordErrors_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePickerCorrected.CustomFormat = "yyyy MMMM dd"
        DateTimePickerCorrected.Format = DateTimePickerFormat.Custom
        DateTimePickerCorrected.Text = Now.ToString
    End Sub

    Protected Sub uncorrectedEpoch_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txBxEpoch.Leave
        RaiseEvent CalcErrors()
    End Sub

    Protected Sub correctedDateTime_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerCorrected.Leave
        RaiseEvent CalcErrors()
    End Sub

    Private Sub chBoxIncludePrecession_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBoxIncludePrecession.CheckedChanged
        RaiseEvent CalcErrors()
    End Sub

    Private Sub chBoxIncludeNutationAnnualAberration_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chBoxIncludeNutationAnnualAberration.CheckedChanged
        RaiseEvent CalcErrors()
    End Sub

End Class
