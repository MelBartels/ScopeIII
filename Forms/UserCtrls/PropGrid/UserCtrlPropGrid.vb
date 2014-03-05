Public Class UserCtrlPropGrid
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
    Public WithEvents btnDefault As System.Windows.Forms.Button
    Public WithEvents btnCancel As System.Windows.Forms.Button
    Public WithEvents btnOK As System.Windows.Forms.Button
    Public WithEvents btnAccept As System.Windows.Forms.Button
    Public WithEvents propGrid As System.Windows.Forms.PropertyGrid
    Public WithEvents ToolTip As System.Windows.Forms.ToolTip
    Public WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Public WithEvents btnSave As System.Windows.Forms.Button
    Public WithEvents btnLoad As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.propGrid = New System.Windows.Forms.PropertyGrid
        Me.btnDefault = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnLoad = New System.Windows.Forms.Button
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
        'propGrid
        '
        Me.propGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.propGrid.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.propGrid.Location = New System.Drawing.Point(8, 8)
        Me.propGrid.Name = "propGrid"
        Me.propGrid.Size = New System.Drawing.Size(391, 424)
        Me.propGrid.TabIndex = 0
        '
        'btnDefault
        '
        Me.btnDefault.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnDefault.Location = New System.Drawing.Point(8, 448)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(56, 23)
        Me.btnDefault.TabIndex = 1
        Me.btnDefault.Text = "Default"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.Location = New System.Drawing.Point(276, 448)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(56, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOK.Location = New System.Drawing.Point(209, 448)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(56, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        '
        'btnAccept
        '
        Me.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnAccept.Location = New System.Drawing.Point(343, 448)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(56, 23)
        Me.btnAccept.TabIndex = 4
        Me.btnAccept.Text = "Accept"
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnSave.Location = New System.Drawing.Point(142, 448)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(56, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnLoad.Location = New System.Drawing.Point(75, 448)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(56, 23)
        Me.btnLoad.TabIndex = 6
        Me.btnLoad.Text = "Load"
        '
        'UserCtrlPropGrid
        '
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnDefault)
        Me.Controls.Add(Me.propGrid)
        Me.Name = "UserCtrlPropGrid"
        Me.Size = New System.Drawing.Size(407, 480)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event DefaultClick()
    Public Event LoadClick()
    Public Event SaveClick()
    Public Event OKClick()
    Public Event CancelClick()
    Public Event AcceptClick()
    Public Event PropertyValueChanged()

    Public Property SelectedObject() As Object
        Get
            Return propGrid.SelectedObject
        End Get
        Set(ByVal Value As Object)
            propGrid.SelectedObject = Value
        End Set
    End Property

    Public Sub SetToolTip()
        ToolTip.SetToolTip(btnDefault, "reload defaults to displayed values")
        ToolTip.SetToolTip(btnLoad, "reload from storage to displayed values")
        ToolTip.SetToolTip(btnSave, "save displayed values to storage")
        ToolTip.SetToolTip(btnOK, "accept changes and close form")
        ToolTip.SetToolTip(btnCancel, "close form without accepting changes")
        ToolTip.SetToolTip(btnAccept, "accept changes")

        ToolTip.IsBalloon = True
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        RaiseEvent DefaultClick()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        RaiseEvent LoadClick()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        RaiseEvent SaveClick()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RaiseEvent OKClick()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent CancelClick()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        RaiseEvent AcceptClick()
    End Sub

    Private Sub propGridpPropertyValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles propGrid.PropertyValueChanged
        RaiseEvent PropertyValueChanged()
    End Sub
End Class
