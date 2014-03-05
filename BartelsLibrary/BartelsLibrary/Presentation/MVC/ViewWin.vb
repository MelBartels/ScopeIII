Public Class ViewWin
    Inherits System.Windows.Forms.Form
    Implements IView

    Public Controller As Controller

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

    ' modified as this View will never display;

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewWin))
        Me.SuspendLayout()
        '
        'ViewWin
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ViewWin"
        Me.Text = "ViewWin"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub GetControllerFactoryBuildAndInit(ByVal ParamArray ViewArgs() As Object)
        Try
            'get concrete ControllerFactory class
            Dim controllerFactory As controllerFactory = GetControllerFactory()

            'createController is called and ViewArgs are passed
            If controllerFactory IsNot Nothing Then
                Controller = controllerFactory.BuildAndInit(Me, ViewArgs)
            End If
        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try
    End Sub

    'subclass (form) must implement and return a concrete ControllerFactory class
    Protected Overridable Function GetControllerFactory() As ControllerFactory
        Return Nothing
    End Function

    Public Function GetControllerReference() As Controller
        Return Controller
    End Function
End Class
