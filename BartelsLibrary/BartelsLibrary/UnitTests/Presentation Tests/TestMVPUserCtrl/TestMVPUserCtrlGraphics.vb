#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
#End Region

''' -----------------------------------------------------------------------------
''' Project	 : TJModelViewControllerMediator
''' Class	 : Common.TestMVPUserCtrlGraphics
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Create this class to illustrate how to draw graphics in the IDE Designer.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	3/7/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class TestMVPUserCtrlGraphics
    Inherits MVPUserCtrlGraphics

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    ' will show in designer if base class overridden and explicit graphics objects declared
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        ' this will appear in the IDE's designer
        Dim g As Graphics = e.Graphics
        g.DrawLine(New Pen(Color.Red), 1, 1, 150, 150)

        MyBase.OnPaint(e)
    End Sub

End Class
