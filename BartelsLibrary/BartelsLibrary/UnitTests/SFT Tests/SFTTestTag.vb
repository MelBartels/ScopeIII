#Region "Imports"
#End Region

Public Class SFTTestTag
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(SFTTestTag))

    Public Shared lll As New SFTPrototype(pSFTSharedSupport, "description of lll", 5)
    Public Shared mmm As New SFTPrototype(pSFTSharedSupport, "description of mmm", "good")
    Public Shared nnn As New SFTPrototype(pSFTSharedSupport, "description of nnn", CObj(SFTTest.bbb))
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SFTTestTag
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SFTTestTag = New SFTTestTag
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SFTTestTag
        Return New SFTTestTag
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Enum Names
        lll
        mmm
        nnn
    End Enum

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

    ' ex of looping through the enumerated values:
    'For Each name As String In [Enum].GetNames(GetType(Names))   (..)   Next
    Public Function EnumName(ByVal [string] As System.String) As Names
        Return CType([Enum].Parse(GetType(Names), [string]), Names)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
