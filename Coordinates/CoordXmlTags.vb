#Region "Imports"
#End Region

Public Class CoordXmlTags
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(CoordXmlTags))

    Public Shared RightAscension As New SFTPrototype(pSFTSharedSupport, "RightAscension")
    Public Shared Declination As New SFTPrototype(pSFTSharedSupport, "Declination")
    Public Shared Sign As New SFTPrototype(pSFTSharedSupport, "Sign")
    Public Shared Hours As New SFTPrototype(pSFTSharedSupport, "Hours")
    Public Shared Degrees As New SFTPrototype(pSFTSharedSupport, "Degrees")
    Public Shared Minutes As New SFTPrototype(pSFTSharedSupport, "Minutes")
    Public Shared Seconds As New SFTPrototype(pSFTSharedSupport, "Seconds")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CoordXmlTags
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordXmlTags = New CoordXmlTags
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As CoordXmlTags
        Return New CoordXmlTags
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
