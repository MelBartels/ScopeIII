#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
#End Region

Public Class FunctionRenderer
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
    Private pWidthPixels As Single
    Private pHeightPixels As Single

    Private pGraphics As Graphics

    ' transforms app coordinates to bmp coordinates
    Dim pTransform As Matrix

    ' function to FunctionRenderer
    Private pIFunction As IFunction
    Private pMinX As Double
    Private pMaxX As Double
    Private pMinY As Double
    Private pMaxY As Double

    ' for axis labels
    Private pFontFamily As New FontFamily(pFontFamilyName)
    Private pFontStyle As Int32 = emath.rint(FontStyle.Regular)
    Private pStringFormat As StringFormat = StringFormat.GenericDefault

    ' sizes for grid, axes and function in app units
    Private pGridPensize As Double
    Private pXAxisPensize As Double
    Private pYAxisPensize As Double
    Private pFunctionPenSize As Double

    ' various values
    Private pXSteps As Int32

#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As FunctionRenderer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As FunctionRenderer = New FunctionRenderer
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As FunctionRenderer
        Return New FunctionRenderer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Integer, ByVal height As Integer) As System.Drawing.Graphics
        pGraphics = g
        pWidthPixels = width
        pHeightPixels = height

        Dim graphFunctionParams As GraphFunctionParams = CType(ObjectToRender, GraphFunctionParams)
        pXSteps = graphFunctionParams.XSteps
        pIFunction = graphFunctionParams.IFunction

        pMinX = pIFunction.MinX
        pMaxX = pIFunction.MaxX
        pMinY = pIFunction.MinY
        pMaxY = pIFunction.MaxY

        pGraphics.FillRectangle(New SolidBrush(Color.White), 0, 0, width, height)

        ' axis sizes will be 1/100th the height or width
        pXAxisPensize = (pMaxY - pMinY) / 100.0
        pYAxisPensize = (pMaxX - pMinX) / 100.0
        ' use the smallest axis size for the function line width
        pFunctionPenSize = Math.Min(pXAxisPensize, pYAxisPensize)

        Dim rectPlotF As RectangleF = New RectangleF(CType(pMinX, Single), CType(pMaxY, Single), CType((pMaxX - pMinX), Single), CType(-(pMaxY - pMinY), Single))

        Dim bmpCorners(2) As PointF
        bmpCorners(0) = New PointF(0, 0)
        bmpCorners(1) = New PointF(width, 0)
        bmpCorners(2) = New PointF(0, height)

        ' the graphic object will now implicitly convert (x,y) to pixel coordinates
        pTransform = New Matrix(rectPlotF, bmpCorners)
        pGraphics.Transform = pTransform

        XGrid(Color.Gray, Color.Black, Color.Black, pYAxisPensize)
        YGrid(Color.Gray, Color.Black, Color.Black, pXAxisPensize)

        DrawFunction(Color.Red)

        Return g
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub DrawFunction(ByVal Color As Color)
        Dim pen As New Pen(Color, CType(pFunctionPenSize, Single))
        Dim lineSegment As New GraphicsPath
        Dim lastPoint As New PointF(CType(pMinX, Single), CType(pIFunction.Y(pMinX), Single))
        Dim thisPoint As PointF

        For thisXValue As Double = pIFunction.MinX To pIFunction.MaxX _
        Step (pIFunction.MaxX - pIFunction.MinX) / CType(Math.Floor(pXSteps), Int32)

            thisPoint = New PointF(CType(thisXValue, Single), CType(pIFunction.Y(thisXValue), Single))
            lineSegment.AddLine(lastPoint, thisPoint)
            lastPoint = thisPoint
        Next thisXValue

        pGraphics.DrawPath(pen, lineSegment)
    End Sub

    ' used to obtain integer points for X and Y axis and grid lines
    Private Function AxisPoints(ByVal minValue As Double, ByVal maxValue As Double) As Collection
        Dim collection As New Collection
        Dim midpoint As Int32 = CType((maxValue + minValue) / 2, Int32)
        Dim shift As Int32 = 0 - midpoint
        Dim stepSize As Int32 = CType(Math.Max((maxValue - minValue) / 10, 1), Int32)

        For ix As Int32 = midpoint To CType(maxValue, Int32) + shift Step stepSize
            collection.Add(ix + shift)
            If ix <> midpoint Then
                collection.Add(midpoint - (ix - shift - midpoint))
            End If
        Next

        Return collection
    End Function

    ' vertical grid lines and Y axis
    Private Sub XGrid(ByVal GridColor As Color, _
                      ByVal AxisColor As Color, _
                      ByVal LabelColor As Color, _
                      ByVal LineWidth As Double)

        Dim line As GraphicsPath
        Dim gridPen As New Pen(GridColor, CType(LineWidth / 4.0, Single))
        Dim axisPen As New Pen(AxisColor, CType(LineWidth, Single))

        For Each ix As Int32 In AxisPoints(pMinX, pMaxX)
            line = New GraphicsPath
            line.AddLine(New Point(ix, CType(pMinY, Int32)), New Point(ix, CType(pMaxY, Int32)))

            If ix = 0 Then
                pGraphics.DrawPath(axisPen, line)
            Else
                pGraphics.DrawPath(gridPen, line)
            End If

            DrawString(ix.ToString, New PointF(ix, 0), LabelColor)
        Next
    End Sub

    ' horizontal grid lines and X axis
    Private Sub YGrid(ByVal GridColor As Color, _
                      ByVal AxisColor As Color, _
                      ByVal LabelColor As Color, _
                      ByVal LineWidth As Double)

        Dim line As GraphicsPath
        Dim gridPen As New Pen(GridColor, CType(LineWidth / 4.0, Single))
        Dim axisPen As New Pen(AxisColor, CType(LineWidth, Single))

        For Each ix As Int32 In AxisPoints(pMinY, pMaxY)
            line = New GraphicsPath
            line.AddLine(New PointF(CType(pMinX, Int32), ix), New PointF(CType(pMaxX, Int32), ix))

            If ix = 0 Then
                pGraphics.DrawPath(axisPen, line)
            Else
                pGraphics.DrawPath(gridPen, line)
            End If
            DrawString(ix.ToString, New PointF(0, ix), LabelColor)
        Next ix
    End Sub

    Private Sub DrawString(ByVal Str As String, ByVal FunctionPoint As PointF, ByVal Color As Color)
        Dim emSize As Int32 = CType(pHeightPixels / 20, Int32)
        Dim textPath As GraphicsPath = New GraphicsPath

        textPath.AddString(Str, pFontFamily, pFontStyle, emSize, New Point(5, 5), pStringFormat)

        Dim rectF As New RectangleF(New PointF(0.0, 0.0), New SizeF(pWidthPixels, pHeightPixels))

        Dim bmpCorners(2) As PointF
        bmpCorners(0) = New PointF(FunctionPoint.X, FunctionPoint.Y)
        bmpCorners(1) = New PointF(CType(FunctionPoint.X + pMaxX, Single), FunctionPoint.Y)
        bmpCorners(2) = New PointF(FunctionPoint.X, CType(FunctionPoint.Y - (pMaxY - pMinY), Single))

        Dim textTransform As New Matrix(rectF, bmpCorners)

        textPath.Transform(textTransform)
        Dim pen As New Pen(Color, -1)
        pGraphics.DrawPath(pen, textPath)
    End Sub
#End Region

End Class

