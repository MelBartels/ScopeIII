Public Class MVPUserCtrl2MeasurementsGaugeBase
    Inherits BartelsLibrary.MVPUserCtrlGraphics

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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Public Event MeasurementsToPoint(ByRef [object] As Object)

    Private pDragPointer As Boolean

    Public Sub NewSize(ByVal width As Int32, ByVal height As Int32)
        Dim size As Int32 = width
        If height < size Then
            size = height
        End If

        Me.Size = New Drawing.Size(size, size)
        Me.Location = New Drawing.Point(eMath.RInt((width - size) / 2), 0)
    End Sub

    Private Function IRenderer2MeasurementsGauge() As IRenderer2MeasurementsGauge
        Return CType(IRenderer, IRenderer2MeasurementsGauge)
    End Function

    Private Sub gauge_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        If IRenderer2MeasurementsGauge.InsideGauge(New Drawing.Point(e.X, e.Y)) Then
            RaiseEvent MeasurementsToPoint(IRenderer2MeasurementsGauge.MeasurementsToPoint(New Drawing.Point(e.X, e.Y)))
            pDragPointer = True
        End If
    End Sub

    Private Sub gauge_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        pDragPointer = False
    End Sub

    Private Sub gauge_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If pDragPointer Then
            RaiseEvent MeasurementsToPoint(IRenderer2MeasurementsGauge.MeasurementsToPoint(New Drawing.Point(e.X, e.Y)))
        End If
    End Sub

End Class
