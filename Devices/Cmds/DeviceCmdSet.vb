#Region "Imports"
#End Region

Public Class DeviceCmdSet
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(DeviceCmdSet))

    Public Shared None As New SFTPrototype(pSFTSharedSupport, "No command set")
    Public Shared TangentEncodersQuery As New SFTPrototype(pSFTSharedSupport, "Tangent encoder interface box command set; no support for reset.", TangentEncodersQueryCmds.GetInstance)
    Public Shared TangentEncodersWithResetR As New SFTPrototype(pSFTSharedSupport, "Tangent encoder interface box command set with support for reset via 'R'.", TangentEncodersWithResetRCmds.GetInstance)
    Public Shared TangentEncodersWithResetZ As New SFTPrototype(pSFTSharedSupport, "Tangent encoder interface box command set with support for reset via 'Z'.", TangentEncodersWithResetZCmds.GetInstance)
    Public Shared SiTechController As New SFTPrototype(pSFTSharedSupport, "Basic command set for Sidereal Technology controller.", SiTechCmds.GetInstance)
    Public Shared JRKerrServoController As New SFTPrototype(pSFTSharedSupport, "Basic command set for JRKerr servo controller.", JRKerrCmds.GetInstance)
    Public Shared Test As New SFTPrototype(pSFTSharedSupport, "Command set for testing purposes only.", TestCmds.GetInstance)
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceCmdSet
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceCmdSet = New DeviceCmdSet
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceCmdSet
        Return New DeviceCmdSet
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
