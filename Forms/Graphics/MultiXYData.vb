#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class MultiXYData

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Data color.")> _
    Public Property DataColor() As Drawing.Color()
        Get
            Return pDataColor
        End Get
        Set(ByVal Value As Drawing.Color())
            pDataColor = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Grid color.")> _
    Public Property GridColor() As Drawing.Color
        Get
            Return pGridColor
        End Get
        Set(ByVal Value As Drawing.Color)
            pGridColor = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Background color.")> _
    Public Property BackgroundColor() As Drawing.Color
        Get
            Return pBackgroundColor
        End Get
        Set(ByVal Value As Drawing.Color)
            pBackgroundColor = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Axis color.")> _
    Public Property AxisColor() As Drawing.Color
        Get
            Return pAxisColor
        End Get
        Set(ByVal Value As Drawing.Color)
            pAxisColor = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Text color.")> _
    Public Property TextColor() As Drawing.Color
        Get
            Return pTextColor
        End Get
        Set(ByVal Value As Drawing.Color)
            pTextColor = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Text font.")> _
    Public Property AxisFont() As Drawing.Font
        Get
            Return pAxisFont
        End Get
        Set(ByVal Value As Drawing.Font)
            pAxisFont = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Data pen width.")> _
    Public Property PenWidth() As Int32
        Get
            Return pPenWidth
        End Get
        Set(ByVal Value As Int32)
            pPenWidth = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Data drawing mode.")> _
    Public Property DrawMode() As DrawModeType
        Get
            Return pDrawMode
        End Get
        Set(ByVal Value As DrawModeType)
            pDrawMode = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Border's top.")> _
    Public Property BorderTop() As Int32
        Get
            Return pBorderTop
        End Get
        Set(ByVal Value As Int32)
            pBorderTop = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Border's left.")> _
    Public Property BorderLeft() As Int32
        Get
            Return pBorderLeft
        End Get
        Set(ByVal Value As Int32)
            pBorderLeft = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Border's bottom.")> _
    Public Property BorderBottom() As Int32
        Get
            Return pBorderBottom
        End Get
        Set(ByVal Value As Int32)
            pBorderBottom = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Border's right.")> _
    Public Property BorderRight() As Int32
        Get
            Return pBorderRight
        End Get
        Set(ByVal Value As Int32)
            pBorderRight = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Starting 'X' data value.")> _
    Public Property XRangeStart() As Double
        Get
            Return pXRangeStart
        End Get
        Set(ByVal Value As Double)
            pXRangeStart = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Ending 'X' data value.")> _
    Public Property XRangeEnd() As Double
        Get
            Return pXRangeEnd
        End Get
        Set(ByVal Value As Double)
            pXRangeEnd = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Starting 'Y' data value.")> _
    Public Property YRangeStart() As Double
        Get
            Return pYRangeStart
        End Get
        Set(ByVal Value As Double)
            pYRangeStart = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Ending 'Y' data value.")> _
    Public Property YRangeEnd() As Double
        Get
            Return pYRangeEnd
        End Get
        Set(ByVal Value As Double)
            pYRangeEnd = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Grid spacing for 'X' axis.")> _
    Public Property XGridSpacing() As Double
        Get
            Return pXGridSpacing
        End Get
        Set(ByVal Value As Double)
            pXGridSpacing = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Grid spacing for 'Y' aYis.")> _
    Public Property YGridSpacing() As Double
        Get
            Return pYGridSpacing
        End Get
        Set(ByVal Value As Double)
            pYGridSpacing = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Log base for 'X' axis.")> _
    Public Property XLogBase() As Double
        Get
            Return pXLogBase
        End Get
        Set(ByVal Value As Double)
            pXLogBase = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("Log base for 'Y' axis.")> _
    Public Property YLogBase() As Double
        Get
            Return pYLogBase
        End Get
        Set(ByVal Value As Double)
            pYLogBase = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("'X' data.")> _
    Public Property XData() As Double()()
        Get
            Return pXData
        End Get
        Set(ByVal Value As Double()())
            pXData = Value
        End Set
    End Property

    <CategoryAttribute("Graph"), _
    DescriptionAttribute("'Y' data.")> _
    Public Property YData() As Double()()
        Get
            Return pYData
        End Get
        Set(ByVal Value As Double()())
            pYData = Value
        End Set
    End Property

    Public Enum DrawModeType
        Line = 1
        Dot
        Bar
    End Enum
#End Region

#Region "Private and Protected Members"
    Private pDataColor() As Drawing.Color
    Private pGridColor As Drawing.Color
    Private pBackgroundColor As Drawing.Color
    Private pAxisColor As Drawing.Color
    Private pTextColor As Drawing.Color
    Private pAxisFont As Drawing.Font
    Private pPenWidth As Int32
    Private pDrawMode As DrawModeType
    Private pBorderTop As Int32
    Private pBorderLeft As Int32
    Private pBorderBottom As Int32
    Private pBorderRight As Int32
    Private pXRangeStart As Double
    Private pXRangeEnd As Double
    Private pYRangeStart As Double
    Private pYRangeEnd As Double
    Private pXGridSpacing As Double
    Private pYGridSpacing As Double
    Private pXLogBase As Double
    Private pYLogBase As Double
    Private pXData()() As Double
    Private pYData()() As Double

#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MultiXYData
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MultiXYData = New MultiXYData
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        defaults()
    End Sub

    Public Shared Function GetInstance() As MultiXYData
        Return New MultiXYData
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Private Sub defaults()
        BackgroundColor = Drawing.Color.LightYellow
        GridColor = Drawing.Color.LightGray
        AxisColor = Drawing.Color.Black
        TextColor = Drawing.Color.Gray
        AxisFont = New Drawing.Font("Arial", 8)
        PenWidth = 2
        DrawMode = DrawModeType.Line
        BorderTop = 30
        BorderLeft = 50
        BorderBottom = 50
        BorderRight = 30
    End Sub
#End Region

End Class
