Public Class ViewWeb
    Inherits System.Web.UI.Page
    Implements IView

    Public Controller As Controller

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub PagepInit(ByVal sender As System.Object, _
                          ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    'Init will occur automatically when the .aspx page loads
    Private Sub GetControllerFactoryBuildAndInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'get concrete ControllerFactory class
            Dim controllerFactory As controllerFactory = GetControllerFactory()

            'createController is called and no ViewArgs are passed
            If controllerFactory IsNot Nothing Then
                Controller = controllerFactory.BuildAndInit(Me, Nothing)
            End If
        Catch ex As Exception
            Response.Write("*** EXCEPTION: {0}" & ex.ToString())
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
