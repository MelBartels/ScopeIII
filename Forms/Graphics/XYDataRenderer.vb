#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class XYDataRenderer
    Inherits RendererBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As XYDataRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As XYDataRenderer = New XYDataRenderer
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As XYDataRenderer
        Return New XYDataRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        Dim XYData As XYData = CType(ObjectToRender, XYData)

        Dim x0 As Int32
        Dim y0 As Int32
        Dim x1 As Int32
        Dim y1 As Int32
        Dim w0 As Int32
        Dim h0 As Int32
        Dim w1 As Int32
        Dim h1 As Int32

        Dim ix As Int32
        Dim jx As Int32
        Dim x As Int32
        Dim y As Int32
        Dim n As Int32
        Dim d As Int32
        Dim d1 As Int32

        Dim s As String

        If XYData.XRangeEnd <= XYData.XRangeStart Then
            Return g
        End If

        If XYData.YRangeEnd <= XYData.YRangeStart Then
            Return g
        End If

        If XYData.XLogBase < 2 Then
            If XYData.XGridSpacing <= 0 Then
                Return g
            End If
        Else
            If XYData.XRangeStart <= 0 Then
                Return g
            End If
        End If

        If XYData.XLogBase < 2 Then
            If XYData.XGridSpacing <= 0 Then
                Return g
            End If
        Else
            If XYData.XRangeStart <= 0 Then
                Return g
            End If
        End If

        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim rectangle As New rectangle(0, 0, width, height)
        g.FillRectangle(New SolidBrush(XYData.BackgroundColor), rectangle)
        Dim penGrid As New Pen(XYData.GridColor, 1)
        Dim penAxis As New Pen(XYData.AxisColor, 1)
        Dim brushAxis As New SolidBrush(XYData.AxisColor)

        x0 = rectangle.Left + XYData.BorderLeft
        y0 = rectangle.Top + XYData.BorderTop
        w0 = rectangle.Width - XYData.BorderLeft - XYData.BorderRight
        h0 = rectangle.Height - XYData.BorderTop - XYData.BorderBottom
        x1 = rectangle.Right - XYData.BorderRight
        y1 = rectangle.Bottom - XYData.BorderBottom

        If XYData.XLogBase < 2 Then
            ' linear 
            n = CType(((XYData.XRangeEnd - XYData.XRangeStart) / XYData.XGridSpacing), Int32)
            If n.Equals(0) Then
                n = 1
            End If
            d = CType(w0 / n, Int32)
            For ix = 0 To n
                x = x0 + ix * d
                g.DrawLine(penGrid, x, y0, x, y1)
                s = CStr((XYData.XRangeStart + (XYData.XRangeEnd - XYData.XRangeStart) * ix / n))
                Dim sf As SizeF = g.MeasureString(s, XYData.AxisFont)
                g.DrawString(s, XYData.AxisFont, brushAxis, x - sf.Width / 2, y1 + sf.Height / 2)
            Next
        Else
            ' log 
            n = CType((Math.Log(XYData.XRangeEnd, XYData.XLogBase) - Math.Log(XYData.XRangeStart, XYData.XLogBase)), Int32)
            If n.Equals(0) Then
                n = 1
            End If
            d = CType(w0 / n, Int32)
            For ix = 0 To n
                x = x0 + ix * d
                If ix < n Then
                    For jx = 1 To CType(XYData.XLogBase - 1, Int32)
                        d1 = CType((Math.Log(jx, XYData.XLogBase) * d), Int32)
                        g.DrawLine(penGrid, x + d1, y0, x + d1, y1)
                    Next
                End If
                s = CStr((Math.Pow(XYData.XLogBase, Math.Log(XYData.XRangeStart, XYData.XLogBase) + ix)))
                Dim sf As SizeF = g.MeasureString(s, XYData.AxisFont)
                g.DrawString(s, XYData.AxisFont, brushAxis, x - sf.Width / 2, y1 + sf.Height / 2)
            Next
        End If

        w1 = d * n

        If (XYData.YLogBase < 2) Then
            ' linear
            n = CType(((XYData.YRangeEnd - XYData.YRangeStart) / XYData.YGridSpacing), Int32)
            If n.Equals(0) Then
                n = 1
            End If
            d = CType(h0 / n, Int32)
            For ix = 0 To n
                y = y1 - ix * d
                g.DrawLine(penGrid, x0, y, x1, y)
                s = CStr((XYData.YRangeStart + (XYData.YRangeEnd - XYData.YRangeStart) * ix / n))
                Dim sf As SizeF = g.MeasureString(s, XYData.AxisFont)
                g.DrawString(s, XYData.AxisFont, brushAxis, x0 - sf.Width - sf.Height / 4, y - sf.Height / 2)
            Next
        Else
            'log
            n = CType((Math.Log(XYData.YRangeEnd, XYData.YLogBase) - Math.Log(XYData.YRangeStart, XYData.YLogBase)), Int32)
            If n.Equals(0) Then
                n = 1
            End If
            d = CType(h0 / n, Int32)
            For ix = 0 To n
                y = y1 - ix * d
                If ix < n Then
                    For jx = 1 To CType(XYData.YLogBase - 1, Int32)
                        d1 = CType((Math.Log(jx, XYData.YLogBase) * d), Int32)
                        g.DrawLine(penGrid, x0, y - d1, x1, y - d1)
                    Next
                End If
                s = Convert.ToString(Math.Pow(XYData.YLogBase, Math.Log(XYData.YRangeStart, XYData.YLogBase) + ix))
                Dim sf As SizeF = g.MeasureString(s, XYData.AxisFont)
                g.DrawString(s, XYData.AxisFont, brushAxis, x0 - sf.Width - sf.Height / 4, y - sf.Height / 2)
            Next
        End If

        h1 = d * n

        ' axis
        g.DrawRectangle(penAxis, x0, y0, w0, h0)

        ' Correct internal width and height.
        ' This is necessary because equidistant grid lines may not fit into
        ' the axis rectangle without rounding errors
        h0 = h1
        w0 = w1

        ' draw data if available

        If XYData.XData Is Nothing OrElse XYData.YData Is Nothing Then
            Return g
        End If

        If XYData.XData.Length <> XYData.YData.Length Then
            Return g
        End If

        Dim penDraw As New Pen(XYData.DataColor, XYData.PenWidth)

        ' first convert the data arrays into points inside the axis rectangle
        Dim pt(XYData.XData.Length - 1) As Point
        Dim lastValidPt As New Point(x0, y1)
        For ix = 0 To pt.Length - 1
            Try
                If XYData.XLogBase < 2 Then
                    pt(ix).X = CType((x0 + (XYData.XData(ix) - XYData.XRangeStart) / (XYData.XRangeEnd - XYData.XRangeStart) * w0), Int32)
                Else
                    pt(ix).X = CType((x0 + (Math.Log(XYData.XData(ix), XYData.XLogBase) - Math.Log(XYData.XRangeStart, XYData.XLogBase)) _
                    / (Math.Log(XYData.XRangeEnd, XYData.XLogBase) - Math.Log(XYData.XRangeStart, XYData.XLogBase)) * w0), Int32)
                End If
                If (XYData.YLogBase < 2) Then
                    pt(ix).Y = CType((y1 - (XYData.YData(ix) - XYData.YRangeStart) / (XYData.YRangeEnd - XYData.YRangeStart) * h0), Int32)
                Else
                    pt(ix).Y = CType((y1 - (Math.Log(XYData.YData(ix), XYData.YLogBase) - Math.Log(XYData.YRangeStart, XYData.YLogBase)) _
                    / (Math.Log(XYData.YRangeEnd, XYData.YLogBase) - Math.Log(XYData.YRangeStart, XYData.YLogBase)) * h0), Int32)
                End If
                lastValidPt = pt(ix)

            Catch ex As Exception
                ' catch invalid data points 
                ' redraw last valid point on error
                pt(ix) = lastValidPt
            End Try
        Next

        ' now draw the points
        For ix = 0 To pt.Length - 1
            Select Case XYData.DrawMode
                Case XYData.DrawModeType.Dot
                    g.DrawEllipse(penDraw, _
                                  CType(pt(ix).X - XYData.PenWidth / 2, Single), _
                                  CType(pt(ix).Y - XYData.PenWidth / 2, Single), _
                                  XYData.PenWidth, _
                                  XYData.PenWidth)

                Case XYData.DrawModeType.Bar
                    g.DrawLine(penDraw, New Point(pt(ix).X, y1), pt(ix))

                Case Else
                    If ix > 0 Then
                        g.DrawLine(penDraw, pt(ix - 1), pt(ix))
                    End If

            End Select
        Next

        Return g
    End Function
#End Region

End Class
