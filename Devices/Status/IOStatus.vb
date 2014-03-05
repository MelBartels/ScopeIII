#Region "Imports"
#End Region

Public Class IOStatus
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(IOStatus))

    Public Shared Build As New SFTPrototype(pSFTSharedSupport, "Build")
    Public Shared BuildFailed As New SFTPrototype(pSFTSharedSupport, "Build Failed")
    Public Shared Open As New SFTPrototype(pSFTSharedSupport, "Open")
    Public Shared OpenFailed As New SFTPrototype(pSFTSharedSupport, "Open Failed")
    Public Shared Closed As New SFTPrototype(pSFTSharedSupport, "Closed")
    Public Shared CmdSent As New SFTPrototype(pSFTSharedSupport, "CmdSent")
    Public Shared CmdReceived As New SFTPrototype(pSFTSharedSupport, "Command Received")
    Public Shared CmdIncomplete As New SFTPrototype(pSFTSharedSupport, "Command Incomplete")
    Public Shared CmdFailed As New SFTPrototype(pSFTSharedSupport, "Command Failed")
    Public Shared ValidResponse As New SFTPrototype(pSFTSharedSupport, "Valid Response")
    Public Shared ResponseFailed As New SFTPrototype(pSFTSharedSupport, "Response Failed")
    Public Shared ChecksumFailed As New SFTPrototype(pSFTSharedSupport, "ChecksumFailed")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IOStatus
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IOStatus = New IOStatus
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As IOStatus
        Return New IOStatus
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
