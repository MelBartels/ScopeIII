#Region "Imports"
#End Region

Public Class TestCmds
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(TestCmds))

    Public Shared One As New SFTPrototype(pSFTSharedSupport, "Test One 'XYZ'", DeviceCmdAndReplyTemplateDefaults.TestOne)
    Public Shared Two As New SFTPrototype(pSFTSharedSupport, "Test Two 'A'", DeviceCmdAndReplyTemplateDefaults.TestTwo)
    Public Shared Three As New SFTPrototype(pSFTSharedSupport, "Test Three 'AA'", DeviceCmdAndReplyTemplateDefaults.TestThree)
    Public Shared Four As New SFTPrototype(pSFTSharedSupport, "Test Four, two bytes {7, 80} = <bell> and P", DeviceCmdAndReplyTemplateDefaults.TestFour)
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TestCmds
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestCmds = New TestCmds
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As TestCmds
        Return New TestCmds
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
