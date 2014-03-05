#Region "Imports"
#End Region

Public Class InitType
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(InitType))

    Public Shared InitDoNothing As New SFTPrototype(pSFTSharedSupport, "InitDoNothing")
    Public Shared InitConvertMatrixAltazimuth As New SFTPrototype(pSFTSharedSupport, "InitConvertMatrixAltazimuth")
    Public Shared InitConvertMatrixCelestial As New SFTPrototype(pSFTSharedSupport, "InitConvertMatrixCelestial")
    Public Shared InitConvertMatrixEquatorial As New SFTPrototype(pSFTSharedSupport, "InitConvertMatrixEquatorial")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As InitType
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As InitType = New InitType
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As InitType
        Return New InitType
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
