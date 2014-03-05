#Region "Imports"
Imports System.Drawing
Imports BartelsLibrary.DelegateSigs
#End Region

Public Class MVPUserCtrlGraphics
    Inherits MVPUserCtrlBase
    Implements IMVPUserCtrlGraphics

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'MVPUserCtrlGraphics
        '
        Me.Name = "MVPUserCtrlGraphics"

    End Sub

#End Region

    Private pIRenderer As IRenderer
    Private pBackgroundImage As Image
    Private pBackgroundImageGraphics As Graphics
    Private pToolTipSet As Boolean

    Public Property IRenderer() As IRenderer Implements IMVPUserCtrlGraphics.IRenderer
        Get
            Return pIRenderer
        End Get
        Set(ByVal Value As IRenderer)
            pIRenderer = Value
        End Set
    End Property

    Public Sub Render() Implements IMVPUserCtrlGraphics.Render
        If Visible Then
            paintSubr(Me.CreateGraphics)
        End If
    End Sub

    Private Sub newSize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        initBackground()
        Render()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        paintSubr(Me.CreateGraphics)
    End Sub

    Protected Sub initBackground()
        If Width < 1 Then
            Width = 1
        End If
        If Height < 1 Then
            Height = 1

        End If
        ' create offscreen image         
        pBackgroundImage = New Bitmap(Width, Height)
        ' create offscreen graphics                              
        pBackgroundImageGraphics = Graphics.FromImage(pBackgroundImage)
        pBackgroundImageGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
    End Sub

    Private Sub MVPUserCtrlGraphics_MouseMove(ByVal sender As Object, ByVal e As Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove

    End Sub

    Private Sub MVPUserCtrlGraphics_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseEnter
        ' set the tool tip so that it shows the first time entering the control
        setToolTip()
    End Sub

    Private Sub MVPUserCtrlGraphics_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseLeave

    End Sub

    Private Sub MVPUserCtrlGraphics_MouseDown(ByVal sender As Object, ByVal e As Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown

    End Sub

    Private Sub MVPUserCtrlGraphics_MouseUp(ByVal sender As Object, ByVal e As Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp

    End Sub

    Protected Sub setToolTip()
        If Not pToolTipSet AndAlso pIRenderer IsNot Nothing AndAlso pIRenderer.ToolTip IsNot Nothing Then
            ToolTip.SetToolTip(Me, pIRenderer.ToolTip)
            ToolTip.IsBalloon = True
            pToolTipSet = True
        End If
    End Sub

    Private Sub paintSubr(ByRef g As Graphics)
        If MyBase.InvokeRequired Then
            MyBase.Invoke(New DelegateGraphics(AddressOf paintSubr), New Object() {g})
        Else
            ' create graphics in background image then draw the background image on the component
            If pIRenderer IsNot Nothing Then
                If pBackgroundImageGraphics Is Nothing Then
                    initBackground()
                End If
                pIRenderer.Render(pBackgroundImageGraphics, Width, Height)
                Try
                    g.DrawImage(pBackgroundImage, 0, 0)
                Catch ioe As InvalidOperationException
                    ' object currently in use elsewhere is an ok error as we are simply overrunning the renderer
                    DebugTrace.WriteLine(ioe.Message)
                End Try
            End If
        End If
    End Sub

End Class
