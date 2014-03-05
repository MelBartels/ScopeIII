#Region "Imports"
#End Region

Public Class JRKerrCmds
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(JRKerrCmds))

    Public Shared NmcHardReset As New SFTPrototype(pSFTSharedSupport, "Hard reset the controller network.", DeviceCmdAndReplyTemplateDefaults.JRKerrNmcHardReset)
    Public Shared SetAddress As New SFTPrototype(pSFTSharedSupport, "Set the address of a controller.", DeviceCmdAndReplyTemplateDefaults.JRKerrSetAddress)
    Public Shared ReadStatus As New SFTPrototype(pSFTSharedSupport, "Read the status of a controller.", DeviceCmdAndReplyTemplateDefaults.JRKerrReadStatus)
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As JRKerrCmds
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrCmds = New JRKerrCmds
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrCmds
        Return New JRKerrCmds
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
