#Region "Imports"
#End Region

Public Class ConvertSubrType
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(ConvertSubrType))

    Public Shared TakiSimple As New SFTPrototype(pSFTSharedSupport, "per Taki eq 5.3-4")
    Public Shared TakiSmallAngle As New SFTPrototype(pSFTSharedSupport, "per Taki eq 5.3-2")
    Public Shared TakiIterative As New SFTPrototype(pSFTSharedSupport, "per Taki eq 5.3-5/6")
    Public Shared BellIterative As New SFTPrototype(pSFTSharedSupport, "Bell apparent alt, iterative az")
    Public Shared BellTaki As New SFTPrototype(pSFTSharedSupport, "Bell apparent alt, Taki iterative az")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ConvertSubrType
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ConvertSubrType = New ConvertSubrType
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As ConvertSubrType
        Return New ConvertSubrType
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
