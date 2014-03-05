Imports BartelsLibrary.DelegateSigs

Public Class UserCtrlCelestialErrors
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
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents DGVBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.DGV = New System.Windows.Forms.DataGridView
        Me.DGVBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'DGV
        '
        Me.DGV.AllowUserToAddRows = False
        Me.DGV.AllowUserToDeleteRows = False
        Me.DGV.AllowUserToResizeRows = False
        Me.DGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Location = New System.Drawing.Point(3, 3)
        Me.DGV.Name = "DGV"
        Me.DGV.ReadOnly = True
        Me.DGV.Size = New System.Drawing.Size(412, 144)
        Me.DGV.TabIndex = 123
        '
        'UserCtrlCelestialErrors
        '
        Me.Controls.Add(Me.DGV)
        Me.Name = "UserCtrlCelestialErrors"
        Me.Size = New System.Drawing.Size(418, 150)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private pDGVCelestialErrorsProperties As DGVCelestialErrorsProperties

    Public Property CelestialErrorsDataSource() As Object
        Get
            Return DGV.DataSource
        End Get
        Set(ByVal Value As Object)
            setErrorsDataSource(Value)
        End Set
    End Property

    Public Sub RefreshDataGridView()
        DGV.Refresh()
    End Sub

    Private Sub setErrorsDataSource(ByVal value As Object)
        If DGV.InvokeRequired Then
            DGV.Invoke(New DelegateObj(AddressOf setErrorsDataSource), New Object() {value})
        Else
            DGVBindingSource.DataSource = value
            DGV.DataSource = DGVBindingSource
            If value IsNot Nothing Then
                If DGV.Columns IsNot Nothing AndAlso DGV.Columns.Count > 0 Then
                    If pDGVCelestialErrorsProperties Is Nothing Then
                        pDGVCelestialErrorsProperties = Forms.DGVCelestialErrorsProperties.GetInstance
                    End If
                    pDGVCelestialErrorsProperties.SetProperties(DGV)
                End If
                RefreshDataGridView()
            End If
        End If
    End Sub

End Class
