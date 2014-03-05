#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class TestMVPRenderer
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

    'Public Shared Function GetInstance() As TestMVPRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestMVPRenderer = New TestMVPRenderer
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As TestMVPRenderer
        Return New TestMVPRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        If Not pRendering Then
            Try
                pRendering = True

                g.Clear(Color.LightGray)
                Dim linGradBrush As New Drawing2D.LinearGradientBrush(New PointF(0, 0), New PointF(width - 1, height - 1), Color.LightBlue, Color.DarkGray)
                g.FillRectangle(linGradBrush, New Rectangle(0, 0, width - 1, height - 1))

                Dim circThick As Int32 = 4
                Dim circColor As Color = Color.CadetBlue
                Dim circPen As New Pen(circColor, circThick)
                g.DrawLine(New Pen(Color.Blue), 100, 100, 200, 500)
                g.DrawLine(New Pen(Color.Turquoise, 5), 50, 50, width, height)
                g.DrawEllipse(circPen, eMath.RInt(circThick / 2), eMath.RInt(circThick / 2), eMath.RInt(width - circThick), eMath.RInt(height - circThick))
                g.DrawEllipse(New Pen(Color.LightPink, 1), 1, 1, eMath.RInt(width - circThick), eMath.RInt(height - circThick))
                g.DrawEllipse(New Pen(Color.Red, 1), 1, 1, eMath.RInt(width / 2), eMath.RInt(height / 2))

            Catch ex As Exception
                ExceptionService.Notify(ex)
            End Try
        End If

        pRendering = False
        Return g
    End Function

#End Region

#Region "Private and Protected Methods"
#End Region

End Class
