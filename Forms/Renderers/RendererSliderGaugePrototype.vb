#Region "Imports"
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public MustInherit Class RendererSliderGaugePrototype
    Inherits RendererBase
    Implements IRendererGauge

#Region "Inner Classes"
#End Region

#Region "Constant Members"

#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public BaseScale As Int32
    Public FontScale As Int32
    Public MarkLenRatio As Double
    Public SliderSlotWidthRatio As Double
    Public SliderSlotHeightRatio As Double
    Public SliderSlotHeight As Int32
    Public SliderSlotWidth As Int32
    Public SliderKnobWidth As Int32
    Public SliderKnobHeight As Int32
    Public SliderKnobShadowThick As Int32
#End Region

#Region "Private and Protected Members"
    Protected pBackgroundColor As Color
    Protected pSliderKnobColor As Color
    Protected pSliderKnobShadowColor As Color
    Protected pSliderKnobHighlightColor As Color
    Protected pSliderSlotBrightColor As Color
    Protected pSliderSlotDarkColor As Color
    Protected pUomColor As Color
    Protected pMarkColor As Color

    Protected pScaleFactor As Double
    Protected pQuietZoneX As Int32
    Protected pQuietZoneY As Int32
    Protected pSliderSlotWidth As Int32
    Protected pSliderSlotHeight As Int32
    Protected pSliderKnobWidth As Int32
    Protected pSliderKnobHeight As Int32

    Protected pSliderSlotMidX As Int32
    Protected pSliderSlotMidY As Int32
    Protected pSliderSlotRect As Rectangle
    Protected pSliderKnobBrush As SolidBrush
    Protected pSliderKnobRect As Rectangle
    Protected pSliderKnobHorizShadowRect As Rectangle
    Protected pSliderKnobVertShadowRect As Rectangle
    Protected pSliderKnobShadowBrush As SolidBrush
    Protected pSliderKnobHorizHighlightRect As Rectangle
    Protected pSliderKnobVertHighlightRect As Rectangle
    Protected pSliderKnobHighlightBrush As SolidBrush

    Protected pMarkLen As Int32
    Protected pNumMarks As Int32
    Protected pStepMeasurement As Double
    Protected pMarkPen As Pen

    Protected pMarkFontSize As Int32
    Protected pMarkFont As Font
    Protected pMarkBrush As SolidBrush

    Protected pIGaugeValue As IGaugeValue
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RendererSliderGaugePrototype
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RendererSliderGaugePrototype = New RendererSliderGaugePrototype
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        BaseScale = 250
        FontScale = 30
        MarkLenRatio = 0.4
        SliderSlotWidthRatio = 0.9
        SliderSlotHeightRatio = 0.3
        SliderSlotHeight = 5
        SliderSlotWidth = SliderSlotHeight
        SliderKnobWidth = 8
        SliderKnobHeight = 20
        SliderKnobShadowThick = 2
        WidthToHeightRatio = 5

        pIGaugeValue = GaugeLinearValue.GetInstance
        ToolTip = BartelsLibrary.Constants.GaugeToolTip

        With BartelsLibrary.Settings.GetInstance
            pBackgroundColor = .SliderBackgroundColor
            pUomColor = .GaugeUomColor
            pMarkColor = .GaugeMarkColor
            pSliderKnobColor = .SliderKnobColor
            pSliderKnobShadowColor = .SliderKnobShadowColor
            pSliderKnobHighlightColor = .SliderKnobHighlightColor
            pSliderSlotBrightColor = .SliderSlotBrightColor
            pSliderSlotDarkColor = .SliderSlotDarkColor
        End With

        pMarkPen = New Pen(pMarkColor)

        pSliderKnobBrush = New SolidBrush(pSliderKnobColor)
        pSliderKnobShadowBrush = New SolidBrush(pSliderKnobShadowColor)
        pSliderKnobHighlightBrush = New SolidBrush(pSliderKnobHighlightColor)

        pSliderSlotRect = New Rectangle
        pSliderKnobRect = New Rectangle
        pSliderKnobHorizShadowRect = New Rectangle
        pSliderKnobVertShadowRect = New Rectangle
        pSliderKnobHorizHighlightRect = New Rectangle
        pSliderKnobVertHighlightRect = New Rectangle
    End Sub

    'Public Shared Function GetInstance() As RendererSliderGaugePrototype
    '    Return New RendererSliderGaugePrototype
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Render(ByRef g As System.Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As System.Drawing.Graphics
        MyBase.Render(g, width, height)

        pG.Clear(pBackgroundColor)
        setDimensions(width, height)

        renderTicksAndLabels()
        renderUOM()

        renderSliderSlot()
        renderKnob()

        Return g
    End Function

    Public MustOverride Function InsideGauge(ByVal point As Point) As Boolean Implements IRendererGauge.InsideGauge

    Public MustOverride Function MeasurementFromObjectToRender() As Double Implements IRendererGauge.MeasurementFromObjectToRender

    Public MustOverride Function MeasurementToPoint(ByVal point As Point) As Double Implements IRendererGauge.MeasurementToPoint

#End Region

#Region "Private and Protected Methods"
    Protected MustOverride Sub setDimensions(ByVal width As Int32, ByVal height As Int32)

    Protected Overridable Sub renderSliderSlot()
        pGLinGradBrush = New Drawing2D.LinearGradientBrush(pSliderSlotRect, pSliderSlotBrightColor, pSliderSlotDarkColor, LinearGradientMode.ForwardDiagonal)
        pG.FillRectangle(pGLinGradBrush, pSliderSlotRect)
        ' or could use a SolidBrush, ala pG.FillRectangle(aSolidBrush, pSliderSlotRect)
    End Sub

    Protected MustOverride Sub renderUOM()

    Protected MustOverride Sub renderKnob()

    Protected MustOverride Sub renderTicksAndLabels()

    Protected MustOverride Function formatMeasurement(ByVal measurement As Double) As String
#End Region

End Class
