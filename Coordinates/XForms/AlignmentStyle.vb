#Region "Imports"
#End Region

Public Class AlignmentStyle
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(AlignmentStyle))

    Public Shared PolarAligned As New SFTPrototype(pSFTSharedSupport, "Equatorial mount that is polar aligned.")
    Public Shared AltazSiteAligned As New SFTPrototype(pSFTSharedSupport, "Altazimuth mount set by site latitude, longitude.")
    Public Shared CelestialAligned As New SFTPrototype(pSFTSharedSupport, "Altazimuth mount initialized by pointing at two stars.")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As AlignmentStyle
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AlignmentStyle = New AlignmentStyle
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As AlignmentStyle
        Return New AlignmentStyle
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
