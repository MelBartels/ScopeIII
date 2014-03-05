#Region "Imports"
#End Region

Public Class CoordName
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(CoordName))

    Public Shared RA As New SFTPrototype(pSFTSharedSupport, "Right Ascension")
    Public Shared Dec As New SFTPrototype(pSFTSharedSupport, "Declination")
    Public Shared Alt As New SFTPrototype(pSFTSharedSupport, "Altitude")
    Public Shared Az As New SFTPrototype(pSFTSharedSupport, "Azimuth")
    Public Shared SidT As New SFTPrototype(pSFTSharedSupport, "Sidereal Time")
    Public Shared FieldRotation As New SFTPrototype(pSFTSharedSupport, "Field Rotation")
    Public Shared Latitude As New SFTPrototype(pSFTSharedSupport, "Latitude")
    Public Shared Longitude As New SFTPrototype(pSFTSharedSupport, "Longitude")
    Public Shared Z1 As New SFTPrototype(pSFTSharedSupport, "Z1 Axis Twist")
    Public Shared Z2 As New SFTPrototype(pSFTSharedSupport, "Z2 Pri Offset")
    Public Shared Z3 As New SFTPrototype(pSFTSharedSupport, "Z3 Sec Offset")
    Public Shared Refraction As New SFTPrototype(pSFTSharedSupport, "Refraction")
    Public Shared PriAxis As New SFTPrototype(pSFTSharedSupport, "Primary Axis")
    Public Shared SecAxis As New SFTPrototype(pSFTSharedSupport, "Secondary Axis")
    Public Shared TierAxis As New SFTPrototype(pSFTSharedSupport, "Tiertiary Axis")
    Public Shared TiltPri As New SFTPrototype(pSFTSharedSupport, "Tilt Y")
    Public Shared TiltSec As New SFTPrototype(pSFTSharedSupport, "Tilt X")
    Public Shared TiltTier As New SFTPrototype(pSFTSharedSupport, "Tilt Z")
    Public Shared AirMass As New SFTPrototype(pSFTSharedSupport, "Air Mass")
    Public Shared Test As New SFTPrototype(pSFTSharedSupport, "Test")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CoordName
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordName = New CoordName
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As CoordName
        Return New CoordName
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides ReadOnly Property FirstItem() As ISFT
        Get
            Return pSFTSharedSupport.FirstItem
        End Get
    End Property

    Public Shared ReadOnly Property ISFT() As ISFT
        Get
            Return pSFTSharedSupport.FirstItem
        End Get
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
