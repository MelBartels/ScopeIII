#Region "Imports"
#End Region

Public Class MountType
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(MountType))

    Public Shared MountTypeNone As New SFTPrototype(pSFTSharedSupport, "No mount type")
    Public Shared MountTypeCustom As New SFTPrototype(pSFTSharedSupport, "Custom characteristics")
    Public Shared MountTypeEquatorial As New SFTPrototype(pSFTSharedSupport, "Plain old equatorial mount where primary axis that rotates 360 deg aimed at pole")
    Public Shared MountTypeAltazimuth As New SFTPrototype(pSFTSharedSupport, "Plain old altazimuth mount where primary axis that rotates 360 deg aimed at local zenith")
    Public Shared MountTypeAltAlt As New SFTPrototype(pSFTSharedSupport, "Altazimuth mount where where primary axis that rotates 360 deg aimed at horizon")
    Public Shared MountTypeHorseshoe As New SFTPrototype(pSFTSharedSupport, "Can swing to pole but cannot swing past pole")
    Public Shared MountTypeEquatorialFork As New SFTPrototype(pSFTSharedSupport, "Can swing to and past pole into sub-polar region")
    Public Shared MountTypeEquatorialYoke As New SFTPrototype(pSFTSharedSupport, "Cannot swing near pole")
    Public Shared MountTypeCrossAxisEnglish As New SFTPrototype(pSFTSharedSupport, "Can track well past meridian when aimed toward celestial equator, but cannot cross meridian while aimed at sub-polar region due to poleward support post")
    Public Shared MountTypeSplitRing As New SFTPrototype(pSFTSharedSupport, "Can swing through pole, cannot rotate primary axis full circle in RA")
    Public Shared MountTypeGermanEquatorialMount As New SFTPrototype(pSFTSharedSupport, "Requires meridian flip")
    Public Shared MountTypeExtendedGerman As New SFTPrototype(pSFTSharedSupport, "No meridian flip, no pole support to impede crossing meridian while pointing underneath pole")
    Public Shared MountTypeOffAxisTorqueTube As New SFTPrototype(pSFTSharedSupport, "Configured same as extended german")
    Public Shared MountTypeWeightStressCompensated As New SFTPrototype(pSFTSharedSupport, "Per famous Zeiss example: dec and ota pivot on top of RA axis, counterweights held by bars that are placed outside ota and hang down past pivot")
    Public Shared MountTypeInvertedFork As New SFTPrototype(pSFTSharedSupport, "Top of RA axis is split into fork that moves in dec, ota held by outside inverted fork that fits over the RA fork, this outside fork also holds the counterweights for the ota")
    Public Shared MountTypeSiderostat As New SFTPrototype(pSFTSharedSupport, "Means stationary star, tube horizontal pointed at pole, flat is mounted equatorially")
    Public Shared MountTypePolarSiderostat As New SFTPrototype(pSFTSharedSupport, "Means stationary star, tube parallel to polar axis looking down into flat, flat is mounted equatorially")
    Public Shared MountTypeUranostat As New SFTPrototype(pSFTSharedSupport, "Tube horizontal pointed at pole, flat is mounted altazimuthly")
    Public Shared MountTypeHeliostat As New SFTPrototype(pSFTSharedSupport, "Same as siderostat but looks at the Sun, only one axis of movement")
    Public Shared MountTypePolarHeliostat As New SFTPrototype(pSFTSharedSupport, "Same as heliostat but tube aimed up at polar axis")
    Public Shared MountTypeCoelostat As New SFTPrototype(pSFTSharedSupport, "Developed from siderostat. Means stationary sky, siderostat mirror fixed parallel to polar axis, tube moves in dec plane mirror mounted facing the celestial equator on axis pointing to celestial pole, when driven around axis the reflected beam remains stationary and does not alter its orientation sometimes a 2nd mirror used to reflect the light into the telescope")
    Public Shared MountTypeCoude As New SFTPrototype(pSFTSharedSupport, "2 mirrors, the upper rotates in Dec, the lower rotates in RA")
    Public Shared MountTypeSpringfield As New SFTPrototype(pSFTSharedSupport, "2 diagonals produce stationary eyepiece, needs meridian flip")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MountType
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MountType = New MountType
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As MountType
        Return New MountType
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
