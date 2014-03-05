#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class MultiXYDataRenderer
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

    'Public Shared Function GetInstance() As MultiXYDataRenderer
    '    Return g NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MultiXYDataRenderer = New MultiXYDataRenderer
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MultiXYDataRenderer
        Return New MultiXYDataRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        Dim multiXYData As multiXYData = CType(ObjectToRender, multiXYData)

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

        If multiXYData.XRangeEnd <= multiXYData.XRangeStart Then
            Return g
        End If

        If multiXYData.YRangeEnd <= multiXYData.YRangeStart Then
            Return g
        End If

        If multiXYData.XLogBase < 2 Then
            If multiXYData.XGridSpacing <= 0 Then
                Return g
            End If
        Else
            If multiXYData.XRangeStart <= 0 Then
                Return g
            End If
        End If

        If multiXYData.XLogBase < 2 Then
            If multiXYData.XGridSpacing <= 0 Then
                Return g
            End If
        Else
            If multiXYData.XRangeStart <= 0 Then
                Return g
            End If
        End If

        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim rectangle As New rectangle(0, 0, width, height)
        g.FillRectangle(New SolidBrush(multiXYData.BackgroundColor), rectangle)
        Dim penGrid As New Pen(multiXYData.GridColor, 1)
        Dim penAxis As New Pen(multiXYData.AxisColor, 1)
        Dim brushAxis As New SolidBrush(multiXYData.AxisColor)

        x0 = rectangle.Left + multiXYData.BorderLeft
        y0 = rectangle.Top + multiXYData.BorderTop
        w0 = rectangle.Width - multiXYData.BorderLeft - multiXYData.BorderRight
        h0 = rectangle.Height - multiXYData.BorderTop - multiXYData.BorderBottom
        x1 = rectangle.Right - multiXYData.BorderRight
        y1 = rectangle.Bottom - multiXYData.BorderBottom

        If multiXYData.XLogBase < 2 Then
            ' linear 
            n = CType(((multiXYData.XRangeEnd - multiXYData.XRangeStart) / multiXYData.XGridSpacing), Int32)
            If n.Equals(0) Then
                n = 1
            End If
            d = CType(w0 / n, Int32)
            For ix = 0 To n
                x = x0 + ix * d
                g.DrawLine(penGrid, x, y0, x, y1)
                s = CStr((multiXYData.XRangeStart + (multiXYData.XRangeEnd - multiXYData.XRangeStart) * ix / n))
                Dim sf As SizeF = g.MeasureString(s, multiXYData.AxisFont)
                g.DrawString(s, multiXYData.AxisFont, brushAxis, x - sf.Width / 2, y1 + sf.Height / 2)
            Next
        Else
            ' log 
            n = CType((Math.Log(multiXYData.XRangeEnd, multiXYData.XLogBase) - Math.Log(multiXYData.XRangeStart, multiXYData.XLogBase)), Int32)
            If n.Equals(0) Then
                n = 1
            End If
            d = CType(w0 / n, Int32)
            For ix = 0 To n
                x = x0 + ix * d
                If ix < n Then
                    For jx = 1 To CType(multiXYData.XLogBase - 1, Int32)
                        d1 = CType((Math.Log(jx, multiXYData.XLogBase) * d), Int32)
                        g.DrawLine(penGrid, x + d1, y0, x + d1, y1)
                    Next
                End If
                s = CStr((Math.Pow(multiXYData.XLogBase, Math.Log(multiXYData.XRangeStart, multiXYData.XLogBase) + ix)))
                Dim sf As SizeF = g.MeasureString(s, multiXYData.AxisFont)
                g.DrawString(s, multiXYData.AxisFont, brushAxis, x - sf.Width / 2, y1 + sf.Height / 2)
            Next
        End If

        w1 = d * n

        If (multiXYData.YLogBase < 2) Then
            ' linear
            n = CType(((multiXYData.YRangeEnd - multiXYData.YRangeStart) / multiXYData.YGridSpacing), Int32)
            If n.Equals(0) Then
                n = 1
            End If
            d = CType(h0 / n, Int32)
            For ix = 0 To n
                y = y1 - ix * d
                g.DrawLine(penGrid, x0, y, x1, y)
                s = CStr((multiXYData.YRangeStart + (multiXYData.YRangeEnd - multiXYData.YRangeStart) * ix / n))
                Dim sf As SizeF = g.MeasureString(s, multiXYData.AxisFont)
                g.DrawString(s, multiXYData.AxisFont, brushAxis, x0 - sf.Width - sf.Height / 4, y - sf.Height / 2)
            Next
        Else
            'log
            n = CType((Math.Log(multiXYData.YRangeEnd, multiXYData.YLogBase) - Math.Log(multiXYData.YRangeStart, multiXYData.YLogBase)), Int32)
            If n.Equals(0) Then
                n = 1
            End If
            d = CType(h0 / n, Int32)
            For ix = 0 To n
                y = y1 - ix * d
                If ix < n Then
                    For jx = 1 To CType(multiXYData.YLogBase - 1, Int32)
                        d1 = CType((Math.Log(jx, multiXYData.YLogBase) * d), Int32)
                        g.DrawLine(penGrid, x0, y - d1, x1, y - d1)
                    Next
                End If
                s = Convert.ToString(Math.Pow(multiXYData.YLogBase, Math.Log(multiXYData.YRangeStart, multiXYData.YLogBase) + ix))
                Dim sf As SizeF = g.MeasureString(s, multiXYData.AxisFont)
                g.DrawString(s, multiXYData.AxisFont, brushAxis, x0 - sf.Width - sf.Height / 4, y - sf.Height / 2)
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

        If multiXYData.XData Is Nothing Then
            Return g
        End If

        If multiXYData.YData Is Nothing Then
            Return g
        End If

        If multiXYData.XData.Length <> multiXYData.YData.Length Then
            Return g
        End If

        If multiXYData.XData.Length <> multiXYData.DataColor.Length Then
            Return g
        End If

        Dim gx As Int32
        For gx = 0 To multiXYData.XData.Length - 1

            Dim penDraw As New Pen(multiXYData.DataColor(gx), multiXYData.PenWidth)
            Dim gXData As Double() = multiXYData.XData(gx)
            Dim gYdata As Double() = multiXYData.YData(gx)

            ' first convert the data arrays into points inside the axis rectangle
            Dim pt(gXData.Length - 1) As Point
            Dim lastValidPt As New Point(x0, y1)
            For ix = 0 To pt.Length - 1
                Try
                    If multiXYData.XLogBase < 2 Then
                        pt(ix).X = CType((x0 + (gXData(ix) - multiXYData.XRangeStart) / (multiXYData.XRangeEnd - multiXYData.XRangeStart) * w0), Int32)
                    Else
                        pt(ix).X = CType((x0 + (Math.Log(gXData(ix), multiXYData.XLogBase) - Math.Log(multiXYData.XRangeStart, multiXYData.XLogBase)) _
                        / (Math.Log(multiXYData.XRangeEnd, multiXYData.XLogBase) - Math.Log(multiXYData.XRangeStart, multiXYData.XLogBase)) * w0), Int32)
                    End If
                    If (multiXYData.YLogBase < 2) Then
                        pt(ix).Y = CType((y1 - (gYdata(ix) - multiXYData.YRangeStart) / (multiXYData.YRangeEnd - multiXYData.YRangeStart) * h0), Int32)
                    Else
                        pt(ix).Y = CType((y1 - (Math.Log(gYdata(ix), multiXYData.YLogBase) - Math.Log(multiXYData.YRangeStart, multiXYData.YLogBase)) _
                        / (Math.Log(multiXYData.YRangeEnd, multiXYData.YLogBase) - Math.Log(multiXYData.YRangeStart, multiXYData.YLogBase)) * h0), Int32)
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
                Select Case multiXYData.DrawMode

                    Case multiXYData.DrawModeType.Dot
                        g.DrawEllipse(penDraw, _
                                      CType(pt(ix).X - multiXYData.PenWidth / 2, Single), _
                                      CType(pt(ix).Y - multiXYData.PenWidth / 2, Single), _
                                      multiXYData.PenWidth, _
                                      multiXYData.PenWidth)

                    Case multiXYData.DrawModeType.Bar
                        g.DrawLine(penDraw, New Point(pt(ix).X, y1), pt(ix))

                    Case Else
                        If ix > 0 Then
                            g.DrawLine(penDraw, pt(ix - 1), pt(ix))
                        End If

                End Select
            Next
        Next

        Return g
    End Function

#End Region

End Class
