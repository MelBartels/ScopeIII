Public Class MVPUserCtrlGaugeBase
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
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Public Event MeasurementToPoint(ByVal value As Double)

    Private pDragPointer As Boolean

    Public Sub NewSize(ByVal width As Int32, ByVal height As Int32)
        Dim widthHeightRatio As Double
        If IRenderer Is Nothing Then
            widthHeightRatio = 1
        Else
            widthHeightRatio = IRendererGauge.WidthToHeightRatio
        End If

        Dim widthFromHeight As Int32 = eMath.RInt(height * widthHeightRatio)
        Dim heightFromWidth As Int32 = eMath.RInt(width / widthHeightRatio)

        Dim calculatedWidth As Int32 = width
        If calculatedWidth > widthFromHeight Then
            calculatedWidth = widthFromHeight
        End If

        Dim calculatedHeight As Int32 = height
        If calculatedHeight > heightFromWidth Then
            calculatedHeight = heightFromWidth
        End If

        Me.Size = New Drawing.Size(calculatedWidth, calculatedHeight)
        Me.Location = New Drawing.Point(eMath.RInt((width - calculatedWidth) / 2), 0)

        ' rendering handled through the NewSize method on the view side
        'If IRendererGauge() IsNot Nothing Then
        '    IRendererGauge.Render(Me.CreateGraphics, calculatedWidth, calculatedHeight)
        'End If
    End Sub

    Private Function IRendererGauge() As IRendererGauge
        Return CType(IRenderer, IRendererGauge)
    End Function

    Private Sub gauge_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        If IRendererGauge.InsideGauge(New Drawing.Point(e.X, e.Y)) Then
            RaiseEvent MeasurementToPoint(IRendererGauge.MeasurementToPoint(New Drawing.Point(e.X, e.Y)))
            pDragPointer = True
        End If
    End Sub

    Private Sub gauge_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        pDragPointer = False
    End Sub

    Private Sub gauge_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If pDragPointer Then
            RaiseEvent MeasurementToPoint(IRendererGauge.MeasurementToPoint(New Drawing.Point(e.X, e.Y)))
        End If
    End Sub

End Class
