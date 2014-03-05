Imports BartelsLibrary.DelegateSigs

Public Class UserCtrlCoord
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
    Friend WithEvents lblName As System.Windows.Forms.Label
    Public WithEvents txBxCoordinate As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UserCtrlCoord))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider
        Me.lblName = New System.Windows.Forms.Label
        Me.txBxCoordinate = New System.Windows.Forms.TextBox
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.SystemColors.Control
        Me.lblName.Image = CType(resources.GetObject("lblName.Image"), System.Drawing.Image)
        Me.lblName.Location = New System.Drawing.Point(0, 0)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(90, 24)
        Me.lblName.TabIndex = 118
        Me.lblName.Text = "Coord Name"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txBxCoordinate
        '
        Me.txBxCoordinate.Location = New System.Drawing.Point(92, 1)
        Me.txBxCoordinate.Name = "txBxCoordinate"
        Me.txBxCoordinate.Size = New System.Drawing.Size(88, 20)
        Me.txBxCoordinate.TabIndex = 117
        Me.txBxCoordinate.Text = "+00d 00m 00s"
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'UserCtrlCoord
        '
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.txBxCoordinate)
        Me.Name = "UserCtrlCoord"
        Me.Size = New System.Drawing.Size(184, 24)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event ValidCoord(ByVal rad As Double)

    Public CoordUpdatedByMe As Boolean

    Public Sub SetToolTip()
        ToolTip.SetToolTip(txBxCoordinate, ScopeLibrary.Constants.CoordFormatMsg)
    End Sub

    Public Property CoordinateText() As String
        Get
            Return txBxCoordinate.Text
        End Get
        Set(ByVal Value As String)
            If MyBase.InvokeRequired Then
                MyBase.Invoke(New DelegateStr(AddressOf setCoordinate), New Object() {Value})
            Else
                setCoordinate(Value)
            End If
        End Set
    End Property

    Public Sub SetCoordinateName(ByVal name As String)
        lblName.Text = name
        txBxCoordinate.Name = name
        ToolTip.SetToolTip(lblName, name)
    End Sub

    Public Sub SetCoordinateLabelColor(ByVal color As Drawing.Color)
        lblName.ForeColor = color
    End Sub

    Protected Sub coord_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txBxCoordinate.Leave
        ValidateTextBoxAsRad()
    End Sub

    Private Sub ValidateTextBoxAsRad()
        Dim rad As Double
        If ScopeIII.Forms.Validate.GetInstance.ValidateTextBoxAsRad(ErrorProvider, txBxCoordinate, rad) Then
            RaiseEvent ValidCoord(rad)
        End If
    End Sub

    Private Sub setCoordinate(ByVal value As String)
        txBxCoordinate.Text = value
    End Sub
End Class
