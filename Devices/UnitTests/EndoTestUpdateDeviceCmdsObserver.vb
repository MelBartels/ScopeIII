#Region "Imports"
#End Region

Public Class EndoTestUpdateDeviceCmdsObserver
    Inherits UpdateDeviceCmdsObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EndoTestUpdateDeviceCmdsObserver
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EndoTestUpdateDeviceCmdsObserver = New EndoTestUpdateDeviceCmdsObserver
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Overloads Shared Function GetInstance() As EndoTestUpdateDeviceCmdsObserver
        Return New EndoTestUpdateDeviceCmdsObserver
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IUpdateDeviceCmdAndReplyTemplate() As IUpdateDeviceCmdAndReplyTemplate
        Get
            Return pUpdateDeviceCmdAndReplyTemplate
        End Get
        Set(ByVal value As IUpdateDeviceCmdAndReplyTemplate)
            pUpdateDeviceCmdAndReplyTemplate = value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
