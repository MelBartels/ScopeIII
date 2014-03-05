#Region "Imports"
#End Region

Public Class UOM
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(UOM))

    Public Shared Radian As New SFTPrototype(pSFTSharedSupport, "Radian")
    Public Shared Degree As New SFTPrototype(pSFTSharedSupport, "Degree")
    Public Shared Arcmin As New SFTPrototype(pSFTSharedSupport, "Arcmin")
    Public Shared Hour As New SFTPrototype(pSFTSharedSupport, "Hour")
    Public Shared Scalar As New SFTPrototype(pSFTSharedSupport, "Scalar")
    Public Shared Counts As New SFTPrototype(pSFTSharedSupport, "Counts")
    Public Shared Ticks As New SFTPrototype(pSFTSharedSupport, "Ticks")
    Public Shared TicksPerSec As New SFTPrototype(pSFTSharedSupport, "TicksPerSec")
    Public Shared TicksPerSecPerSec As New SFTPrototype(pSFTSharedSupport, "TicksPerSecPerSec")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UOM
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UOM = New UOM
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As UOM
        Return New UOM
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
