#Region "Imports"
Imports System.Drawing
#End Region

Public MustInherit Class RendererCoordGaugeBase
    Inherits RendererGaugeBase

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

    'Public Shared Function GetInstance() As RendererCoordGaugeBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RendererCoordGaugeBase = New RendererCoordGaugeBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As RendererCoordGaugeBase
    '    Return New RendererCoordGaugeBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function MeasurementFromObjectToRender() As Double
        Return CType(pObjectToRender, Coordinate).Rad
    End Function

    ' 0 deg is directly upward;
    ' returns within 0-360deg range
    Public Overrides Function MeasurementToPoint(ByVal point As Point) As Double
        Return eMath.AngleRadFromPoints(pGMidPoint, point)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
