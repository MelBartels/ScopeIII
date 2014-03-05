#Region "Imports"
#End Region

Public Class ReceiveInspectorState
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(ReceiveInspectorState))

    Public Shared NotSet As New SFTPrototype(pSFTSharedSupport, "Not set")
    Public Shared InProcess As New SFTPrototype(pSFTSharedSupport, "In process")
    Public Shared TerminatedCharFound As New SFTPrototype(pSFTSharedSupport, "Terminating character encountered")
    Public Shared ReadTooManyBytes As New SFTPrototype(pSFTSharedSupport, "Read too many bytes")
    Public Shared ReadCorrectNumberOfBytes As New SFTPrototype(pSFTSharedSupport, "Read the correct number of bytes")
    Public Shared TimedOut As New SFTPrototype(pSFTSharedSupport, "Timed out")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ReceiveInspectorState
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ReceiveInspectorState = New ReceiveInspectorState
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As ReceiveInspectorState
        Return New ReceiveInspectorState
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
