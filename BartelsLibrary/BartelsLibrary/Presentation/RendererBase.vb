#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public MustInherit Class RendererBase
    Implements IRenderer

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pToolTip As String
    Protected pWidthToHeightRatio As Double
    Protected pObjectToRender As Object
    Protected pRendering As Boolean

    Protected pG As Drawing.Graphics

    Protected pGUlPoint As Point
    Protected pGMidPoint As Point
    Protected pGLrPoint As Point

    Protected pGSize As Size
    Protected pGRect As Rectangle

    Protected pGMidX As Int32
    Protected pGMidY As Int32

    Protected pGColor As Color
    Protected pGBrush As SolidBrush

    ' change color by ratio of 55/255
    Protected pGColorChange As Byte
    Protected pGLinGradBrush As Drawing2D.LinearGradientBrush

    Protected pFontFamilyName As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RendererBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RendererBase = New RendererBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        With Settings.GetInstance
            pFontFamilyName = .RendererFontFamilyName
            pGColor = .GaugeBackgroundColor
            ' change color by ratio of this number divided by 256
            pGColorChange = .GaugeBackgroundColorChange
        End With
    End Sub

    'Public Shared Function GetInstance() As RendererBase
    '    Return New RendererBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ToolTip() As String Implements IRenderer.ToolTip
        Get
            Return pToolTip
        End Get
        Set(ByVal Value As String)
            pToolTip = Value
        End Set
    End Property

    Public Property WidthToHeightRatio() As Double Implements IRenderer.WidthToHeightRatio
        Get
            Return pWidthToHeightRatio
        End Get
        Set(ByVal Value As Double)
            pWidthToHeightRatio = Value
        End Set
    End Property

    Public Property ObjectToRender() As Object Implements IRenderer.ObjectToRender
        Get
            Return pObjectToRender
        End Get
        Set(ByVal Value As Object)
            pObjectToRender = Value
        End Set
    End Property

    Public Overridable Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics Implements IRenderer.Render
        pG = g

        ' full area to work with
        pGUlPoint = New Point(0, 0)
        pGMidPoint = New Point(eMath.RInt(width / 2), eMath.RInt(height / 2))
        pGLrPoint = New Point(width, height)

        pGSize = New Size(width, height)
        pGRect = New Rectangle(pGUlPoint, pGSize)

        pGMidX = eMath.RInt(width / 2)
        pGMidY = eMath.RInt(height / 2)

        pGBrush = New SolidBrush(pGColor)
        setLinGradBrush(pGColor)

        Return g
    End Function

#End Region

#Region "Private and Protected Methods"
    Protected Sub setLinGradBrush(ByVal color As Drawing.Color)
        pGLinGradBrush = New Drawing2D.LinearGradientBrush(pGRect, getLightColor(color, pGColorChange), getDarkColor(color, pGColorChange), LinearGradientMode.ForwardDiagonal)
    End Sub

    Protected Function getDarkColor(ByVal color As Color, ByVal dark As Byte) As Color
        Dim r As Byte = Byte.MinValue
        Dim g As Byte = Byte.MinValue
        Dim b As Byte = Byte.MinValue

        If color.R > dark Then
            r = color.R - dark
        End If
        If color.G > dark Then
            g = color.G - dark
        End If
        If color.B > dark Then
            b = color.B - dark
        End If

        Return Drawing.Color.FromArgb(r, g, b)
    End Function

    Protected Function getLightColor(ByVal color As Color, ByVal dark As Byte) As Color
        Dim r As Byte = Byte.MaxValue
        Dim g As Byte = Byte.MaxValue
        Dim b As Byte = Byte.MaxValue

        If eMath.RInt(color.R) + eMath.RInt(dark) <= Byte.MaxValue Then
            r = color.R + dark
        End If
        If eMath.RInt(color.G) + eMath.RInt(dark) <= Byte.MaxValue Then
            g = color.G + dark
        End If
        If eMath.RInt(color.B) + eMath.RInt(dark) <= Byte.MaxValue Then
            b = color.B + dark
        End If

        Return Drawing.Color.FromArgb(r, g, b)
    End Function
#End Region

End Class
