#Region "Imports"
#End Region

Public Class TrajMoveState
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(TrajMoveState))

    Public Shared WaitToBuildTraj As New SFTPrototype(pSFTSharedSupport, "WaitToBuildTraj")
    Public Shared TrajStarted As New SFTPrototype(pSFTSharedSupport, "TrajStarted")
    Public Shared WaitForRampDown As New SFTPrototype(pSFTSharedSupport, "WaitForRampDown")
    Public Shared RampDownStarted As New SFTPrototype(pSFTSharedSupport, "RampDownStarted")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TrajMoveState
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TrajMoveState = New TrajMoveState
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As TrajMoveState
        Return New TrajMoveState
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
