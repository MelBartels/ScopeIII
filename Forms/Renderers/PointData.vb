#Region "Imports"
#End Region

Public Class PointData
    Implements ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public TiltPriRad As Double

    ' TiltSecRad encapsulated so as to precalculate and cache sin/cos
    Public SinTiltSecRad As Double
    Public CosTiltSecRad As Double

    Public TiltTierRad As Double
    Public PriRad As Double
    Public SecRad As Double
    Public GlobeRadius As Double
    Public Xcenter As Int32
    Public Ycenter As Int32
    Public Point As Drawing.Point
    Public BeyondMapRange As Boolean
#End Region

#Region "Private and Protected Members"
    Private _tiltSecRad As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PointData
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PointData = New PointData
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As PointData
        Return New PointData
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property TiltSecRad() As Double
        Get
            Return _tiltSecRad
        End Get
        Set(ByVal value As Double)
            _tiltSecRad = value
            SinTiltSecRad = Math.Sin(_tiltSecRad)
            CosTiltSecRad = Math.Cos(_tiltSecRad)
        End Set
    End Property

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim newPD As PointData = PointData.GetInstance
        CopyTo(newPD)
        Return newPD
    End Function

    Public Sub CopyTo(ByRef pd As PointData)
        pd.TiltPriRad = Me.TiltPriRad
        pd.TiltSecRad = Me.TiltSecRad
        pd.TiltTierRad = Me.TiltTierRad
        pd.PriRad = Me.PriRad
        pd.SecRad = Me.SecRad
        pd.GlobeRadius = Me.GlobeRadius
        pd.Xcenter = Me.Xcenter
        pd.Ycenter = Me.Ycenter
        pd.Point = Me.Point
        pd.BeyondMapRange = Me.BeyondMapRange
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
