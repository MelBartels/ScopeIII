#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
#End Region

Public Class TestUserCtrl
    Inherits UserCtrl

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
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
        'TestUserCtrl
        '
        Me.Name = "TestUserCtrl"
        Me.Size = New System.Drawing.Size(300, 300)

    End Sub

#End Region

    Protected Overrides Function GetUserCtrlControllerFactory() As UserCtrlControllerFactory
        Return TestUserCtrlControllerFactory.GetInstance
    End Function

    ' will show in designer if base class overridden and explicit graphics objects declared
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        ' 'try' necessary to catch OnPaint event exception fired from .NET visual designer, where the controller is not built
        Try
            ' since we're calling OnPaint here, must explicitly call controller's OnPaint,
            ' which would be called if this method was commented out
            UserCtrlController.OnPaint(e)
        Catch ex As Exception
        End Try

        ' this will appear in the IDE's designer
        Dim g As Graphics = e.Graphics
        g.DrawLine(New Pen(Color.Red), 1, 75, 150, 225)
    End Sub

End Class
