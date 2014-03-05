#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
#End Region

Public Class TestUserCtrlController
    Inherits UserCtrlController

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pTestUserCtrl As TestUserCtrl

    Private WithEvents pToolTip As System.Windows.Forms.ToolTip
    Private WithEvents pErrorProvider As ErrorProvider

    Private pIRenderer As IRenderer
    Private pBackgroundImage As Image
    Private pBackgroundImageGraphics As Graphics

#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TestUserCtrlController
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestUserCtrlController = New TestUserCtrlController
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As TestUserCtrlController
        Return New TestUserCtrlController
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IRenderer() As IRenderer
        Get
            Return pIRenderer
        End Get
        Set(ByVal Value As IRenderer)
            pIRenderer = Value
        End Set
    End Property

    Public Property GraphicsSize() As Drawing.Size
        Get
            Return pTestUserCtrl.Size
        End Get
        Set(ByVal Value As Drawing.Size)
            pTestUserCtrl.Size = Value
        End Set
    End Property

    Public Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Try
            ' can create graphics from any component, ie:
            'Dim cg As Graphics = pTestUserCtrl.CreateGraphics
            ' can create graphics directly
            'Dim dg As Graphics = e.Graphics
            ' can create graphics from eventargs
            'Dim eg As Graphics = CType(e.Graphics, Graphics)

            paintSubr(CType(e.Graphics, Graphics))

        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try
    End Sub
#End Region

#Region "Private and Protected Methods"

    Protected Overrides Sub SetTypedModelReferences()

    End Sub

    Protected Overrides Sub SetTypedUserControlReference()
        pTestUserCtrl = CType(pUserControl, TestUserCtrl)
    End Sub

    Protected Overrides Sub SetUserControlDataBindings()
    End Sub

    Protected Overrides Sub SetUserControlWithEventsReferences()
        pToolTip = pTestUserCtrl.ToolTip
        pErrorProvider = pTestUserCtrl.ErrorProvider
    End Sub

    Protected Overrides Sub InitializeUserControl()
        initBackground()
        SetToolTip()
    End Sub

    Private Sub TestUserCtrlpPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pTestUserCtrl.Paint
        OnPaint(e)
    End Sub

    Private Sub TestUserCtrlpResize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pTestUserCtrl.Resize
        initBackground()
    End Sub

    Private Sub initBackground()
        ' create offscreen image                                 
        pBackgroundImage = New Bitmap(pTestUserCtrl.Width, pTestUserCtrl.Height)
        ' create offscreen graphics                              
        pBackgroundImageGraphics = Graphics.FromImage(pBackgroundImage)
        pBackgroundImageGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
    End Sub

    ' create graphics in background image then draw the background image on the component
    Private Sub paintSubr(ByRef g As Graphics)
        If pIRenderer IsNot Nothing Then
            pIRenderer.Render(pBackgroundImageGraphics, pTestUserCtrl.Width, pTestUserCtrl.Height)
            g.DrawImage(pBackgroundImage, 0, 0)
        End If
    End Sub

    Private Sub SetToolTip()
        pToolTip.SetToolTip(pTestUserCtrl, "Test ToolTip")
    End Sub
#End Region

End Class
