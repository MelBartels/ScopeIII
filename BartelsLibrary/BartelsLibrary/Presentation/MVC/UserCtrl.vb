Public Class UserCtrl
    Inherits System.Windows.Forms.UserControl

    Public UserCtrlController As UserCtrlController

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
        Me.Text = "ViewWin"

    End Sub

#End Region

    Public Sub GetUserCtrlControllerFactoryBuildAndInit(ByVal ParamArray ViewArgs() As Object)
        'get concrete UserCtrlControllerFactory class
        Dim UserCtrlControllerFactory As UserCtrlControllerFactory = GetUserCtrlControllerFactory()

        'createUserCtrlController is called and ViewArgs are passed
        If UserCtrlControllerFactory IsNot Nothing Then
            UserCtrlController = UserCtrlControllerFactory.BuildAndInit(Me, ViewArgs)
        End If
    End Sub

    'subclass (form) must implement and return a concrete UserCtrlControllerFactory class
    Protected Overridable Function GetUserCtrlControllerFactory() As UserCtrlControllerFactory
        Return Nothing
    End Function

    Public Function GetUserCtrlControllerReference() As UserCtrlController
        Return UserCtrlController
    End Function

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        ' 'try' necessary to catch OnPaint event exception fired from .NET visual designer, where the controller is not built
        Try
            UserCtrlController.OnPaint(e)
        Catch ex As Exception
        End Try
    End Sub

End Class
