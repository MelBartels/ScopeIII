#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class OrthographicProjectionCalculator

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

    'Public Shared Function GetInstance() As OrthographicProjectionCalculator
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As OrthographicProjectionCalculator = New OrthographicProjectionCalculator
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As OrthographicProjectionCalculator
        Return New OrthographicProjectionCalculator
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' equations from wikipedia on orthographic projection (which contained a typo that I edited for pPd.SecRad = ...)
    Public Function MeasurementsToPoint(ByVal point As Point, ByRef pPd As PointData, ByVal pGMidX As Int32, ByVal pGMidY As Int32, ByVal pRimDimen As Int32, ByRef dir As ISFT) As Object
        Dim pd As PointData = CType(pPd.Clone, PointData)

        ' derotate x, y before inverting
        Dim xc As Double = point.X - pGMidX
        Dim yc As Double = pGMidY - point.Y
        Dim p As Double = Math.Sqrt(xc * xc + yc * yc)

        ' Math.Acos(xc / p) and Math.Asin(yc / p) respectively gives following angles:
        ' pointed up: 90,90
        ' pointed right: 0,0
        ' pointed down: 90,-90
        ' pointed left: 180,0
        ' get angle w/ respect to pointing right
        Dim angleRadFromAcos As Double = Math.Acos(xc / p)
        Dim angleRadFromAsin As Double = Math.Asin(yc / p)
        Dim angleRadPointRight As Double
        If angleRadFromAsin <= 0 Then
            angleRadPointRight = angleRadFromAcos
        Else
            angleRadPointRight = eMath.ReverseRad(angleRadFromAcos)
        End If
        ' get angle w/ respect to vertical
        Dim angleRadVert As Double = eMath.ValidRad(angleRadPointRight + Units.QtrRev)
        ' remove any tilt in the 3rd axis
        Dim angleRadTiltTier As Double = eMath.ValidRad(angleRadVert - pd.TiltTierRad)
        'Debug.WriteLine("angleRadPointRight(deg) " & angleRadPointRight * Units.RadToDeg _
        '              & ", angleRadVert(deg) " & angleRadVert * Units.RadToDeg _
        '              & ", angleRadTiltTier(deg) " & angleRadTiltTier * Units.RadToDeg)
        ' get derotated x, y and invert
        ' sin w/ x and cos w/ y 'cause angle measured from vertical
        If dir Is Rotation.CW Then
            angleRadTiltTier = -angleRadTiltTier
        End If
        Dim x As Double = Math.Sin(angleRadTiltTier) * p
        Dim y As Double = Math.Cos(angleRadTiltTier) * p

        Dim c As Double = Math.Asin(p / (pRimDimen / 2))
        Dim sinC As Double = Math.Sin(c)
        Dim cosC As Double = Math.Cos(c)
        pd.SecRad = Math.Asin(cosC * pd.SinTiltSecRad + y * sinC * pd.CosTiltSecRad / p)
        ' .atan2(x,y) works like .atan(x/y) but gets correct quadrant
        pd.PriRad = pd.TiltPriRad + Math.Atan2(x * sinC, p * pd.CosTiltSecRad * cosC - y * pd.SinTiltSecRad * sinC)

        ' if dragging outside of sphere, then default to 0
        If [Double].IsNaN(pd.PriRad) Then
            pd.PriRad = 0
        End If
        If [Double].IsNaN(pd.SecRad) Then
            pd.SecRad = 0
        Else
            pd.SecRad = eMath.ValidRadPi(pd.SecRad)
        End If

        Return New Double() {pd.PriRad, pd.SecRad}
    End Function

    Public Sub CalcPoint(ByRef pd As PointData, ByRef dir As ISFT, ByRef ConvertCoords As [Delegate])
        Dim az As AZdouble = CType(ConvertCoords.DynamicInvoke(New Object() {pd.PriRad, pd.SecRad}), AZdouble)
        pd.PriRad = az.Z
        pd.SecRad = eMath.ValidRadPi(az.A)

        CalcPoint(pd, dir)
    End Sub

    Public Sub CalcPoint(ByRef pd As PointData, ByRef dir As ISFT)
        Dim Xoffset As Int32 = emath.rint(pd.GlobeRadius * Math.Cos(pd.SecRad) * Math.Sin(pd.PriRad - pd.TiltPriRad))
        If dir Is Rotation.CW Then
            pd.Point.X = pd.Xcenter - Xoffset
        Else
            pd.Point.X = pd.Xcenter + Xoffset
        End If
        ' center - calc value because in .NET, Microsoft decrements Y as one moves upward
        Dim sinSecRad As Double = Math.Sin(pd.SecRad)
        Dim cosSecRad As Double = Math.Cos(pd.SecRad)
        Dim cosDeltaPriRad As Double = Math.Cos(pd.PriRad - pd.TiltPriRad)
        pd.Point.Y = pd.Ycenter - emath.rint(pd.GlobeRadius * (pd.CosTiltSecRad * sinSecRad - pd.SinTiltSecRad * cosSecRad * cosDeltaPriRad))
        pd.BeyondMapRange = ((pd.SinTiltSecRad * sinSecRad + pd.CosTiltSecRad * cosSecRad * cosDeltaPriRad) < 0)
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class

