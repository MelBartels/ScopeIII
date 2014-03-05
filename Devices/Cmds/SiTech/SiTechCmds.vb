#Region "Imports"
#End Region

Public Class SiTechCmds
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(SiTechCmds))

    Public Shared ReadController As New SFTPrototype(pSFTSharedSupport, "Read controller (efficient 20 byte response).", DeviceCmdAndReplyTemplateDefaults.SiTechReadController)
    Public Shared SetPriAccel As New SFTPrototype(pSFTSharedSupport, "Set the acceleration for the primary motor.", DeviceCmdAndReplyTemplateDefaults.SiTechSetPriAccel)
    Public Shared SetSecAccel As New SFTPrototype(pSFTSharedSupport, "Set the acceleration speed for the secondary motor.", DeviceCmdAndReplyTemplateDefaults.SiTechSetSecAccel)
    Public Shared SetPriVel As New SFTPrototype(pSFTSharedSupport, "Command the primary motor to move at a velocity.", DeviceCmdAndReplyTemplateDefaults.SiTechSetPriVel)
    Public Shared SetSecVel As New SFTPrototype(pSFTSharedSupport, "Command the secondary motor to move at a velocity.", DeviceCmdAndReplyTemplateDefaults.SiTechSetSecVel)
    Public Shared SetPriPos As New SFTPrototype(pSFTSharedSupport, "Command the primary motor to move to a position.", DeviceCmdAndReplyTemplateDefaults.SiTechSetPriPos)
    Public Shared SetSecPos As New SFTPrototype(pSFTSharedSupport, "Command the secondary motor to move to a position.", DeviceCmdAndReplyTemplateDefaults.SiTechSetSecPos)
    Public Shared GetPriAccel As New SFTPrototype(pSFTSharedSupport, "Get the acceleration for the primary motor.", DeviceCmdAndReplyTemplateDefaults.SiTechGetPriAccel)
    Public Shared GetSecAccel As New SFTPrototype(pSFTSharedSupport, "Get the acceleration for the secondary motor.", DeviceCmdAndReplyTemplateDefaults.SiTechGetSecAccel)
    Public Shared GetPriVel As New SFTPrototype(pSFTSharedSupport, "Get the velocity for the primary motor.", DeviceCmdAndReplyTemplateDefaults.SiTechGetPriVel)
    Public Shared GetSecVel As New SFTPrototype(pSFTSharedSupport, "Get the velocity for the secondary motor.", DeviceCmdAndReplyTemplateDefaults.SiTechGetSecVel)
    Public Shared GetPriPos As New SFTPrototype(pSFTSharedSupport, "Get the position for the primary motor.", DeviceCmdAndReplyTemplateDefaults.SiTechGetPriPos)
    Public Shared GetSecPos As New SFTPrototype(pSFTSharedSupport, "Get the position for the secondary motor.", DeviceCmdAndReplyTemplateDefaults.SiTechGetSecPos)
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SiTechCmds
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SiTechCmds = New SiTechCmds
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SiTechCmds
        Return New SiTechCmds
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
