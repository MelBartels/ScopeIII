#Region "Imports"
#End Region

Public Class CoordErrorType
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(CoordErrorType))

    Public Shared Precession As New SFTPrototype(pSFTSharedSupport, "The slow drift of the Earth's rotation axis due to the Moon and Sun's pull on Earth's equatorial bulge.")
    Public Shared Nutation As New SFTPrototype(pSFTSharedSupport, "The periodic oscillation of the Earth's rotational axis due principally to the Moon.")
    Public Shared AnnualAberration As New SFTPrototype(pSFTSharedSupport, "Annual change in a star's position due to the combined effects of the motion of light and the Earth's movement around the Sun.")
    Public Shared CoordXform As New SFTPrototype(pSFTSharedSupport, "Difference of observed versus predicted coordinate transform positions. Positive values indicate that the telescope is aimed too high or too clockwise.")
    Public Shared Refraction As New SFTPrototype(pSFTSharedSupport, "The refraction (bending downward) of light as it travels through the atmosphere causes objects near the horizon to appear higher in elevation.")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CoordErrorType
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordErrorType = New CoordErrorType
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As CoordErrorType
        Return New CoordErrorType
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
