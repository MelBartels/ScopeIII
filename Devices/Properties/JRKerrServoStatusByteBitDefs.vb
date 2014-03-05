#Region "Imports"
#End Region

Public Class JRKerrServoStatusByteBitDefs
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(JRKerrServoStatusByteBitDefs))

    Public Shared SEND_POS As New SFTPrototype(pSFTSharedSupport, "Get current position.")
    Public Shared SEND_AD As New SFTPrototype(pSFTSharedSupport, "Get the AD value.")
    Public Shared SEND_VEL As New SFTPrototype(pSFTSharedSupport, "Get current velocity.")
    Public Shared SEND_AUX As New SFTPrototype(pSFTSharedSupport, "Get the auxiliary status.")
    Public Shared SEND_HOME As New SFTPrototype(pSFTSharedSupport, "Get the home position.")
    Public Shared SEND_ID As New SFTPrototype(pSFTSharedSupport, "Get the module ID.")
    Public Shared SEND_PERROR As New SFTPrototype(pSFTSharedSupport, "Get the position error.")
    Public Shared SEND_NPOINTS As New SFTPrototype(pSFTSharedSupport, "Get the number of path points.")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As JRKerrServoStatusByteBitDefs
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrServoStatusByteBitDefs = New JRKerrServoStatusByteBitDefs
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrServoStatusByteBitDefs
        Return New JRKerrServoStatusByteBitDefs
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
