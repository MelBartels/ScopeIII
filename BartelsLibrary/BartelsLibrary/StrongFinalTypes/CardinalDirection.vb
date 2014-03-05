#Region "Imports"
#End Region

Public Class CardinalDirection
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(CardinalDirection))

    Public Shared North As New SFTPrototype(pSFTSharedSupport, "North")
    Public Shared East As New SFTPrototype(pSFTSharedSupport, "East")
    Public Shared South As New SFTPrototype(pSFTSharedSupport, "South")
    Public Shared West As New SFTPrototype(pSFTSharedSupport, "West")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CardinalDirections
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CardinalDirections = New CardinalDirections
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As CardinalDirection
        Return New CardinalDirection
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
