Imports BartelsLibrary.DelegateSigs

Public Class FrmProgressBar
    Inherits MVPViewBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        pIObservable = ObservableImp.GetInstance

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
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblProgress = New System.Windows.Forms.Label
        Me.ProgressBar = New System.Windows.Forms.ProgressBar
        Me.SuspendLayout()
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.Location = New System.Drawing.Point(8, 24)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(464, 23)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "progress text"
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(0, 3)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(472, 16)
        Me.ProgressBar.TabIndex = 2
        '
        'FrmProgressBar
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(472, 46)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.lblProgress)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProgressBar"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Progress"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private pIObservable As IObservable

    Public Property FormTitle() As String
        Get
            Return Text
        End Get
        Set(ByVal Value As String)
            Text = Value
        End Set
    End Property

    Public Property IObservable() As IObservable
        Get
            Return pIObservable
        End Get
        Set(ByVal Value As IObservable)
            pIObservable = Value
        End Set
    End Property

    Public Sub ProgressText(ByVal msg As String)
        If lblProgress.InvokeRequired Then
            lblProgress.Invoke(New DelegateStr(AddressOf ProgressText), New Object() {msg})
        Else
            lblProgress.Text = msg
        End If
    End Sub

    ' wrapper example where thread safety is observed (a new thread is obtained from the OS if required)
    Public Sub ProgressPercent(ByVal percent As Double)
        If ProgressBar.InvokeRequired Then
            ProgressBar.BeginInvoke(New DelegateDbl(AddressOf ProgressPercent), New Object() {percent})
        Else
            ProgressBar.Value = eMath.RInt(percent * 100)
        End If
    End Sub

    Private Sub FrmProgressBar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If MyBase.Visible Then
            pIObservable.Notify(Constants.FormLoaded)
        End If
    End Sub

End Class
