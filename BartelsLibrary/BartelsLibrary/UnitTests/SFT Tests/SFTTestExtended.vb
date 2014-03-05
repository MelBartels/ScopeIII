#Region "Imports"
#End Region

Public Class SFTTestExtended
    Inherits SFTTest

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Public Shared fff As New SFTPrototype(GetType(SFTTestExtended), pSFTSharedSupport, "description of fff")
    Public Shared ggg As New SFTPrototype(pSFTSharedSupport, "description of ggg")
    Public Shared hhh As New SFTPrototype(pSFTSharedSupport, "description of hhh")
#End Region

#Region "Public and Friend Members"
    Shadows Enum Names
        fff
        ggg
        hhh
    End Enum
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SFTTestExtended
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SFTTestExtended = New SFTTestExtended
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        MyBase.new()
    End Sub

    Public Shared Shadows Function GetInstance() As SFTTestExtended
        Return New SFTTestExtended
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Shadows Function EnumName(ByVal [string] As System.String) As Names
        Return CType([Enum].Parse(GetType(Names), [string]), Names)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
