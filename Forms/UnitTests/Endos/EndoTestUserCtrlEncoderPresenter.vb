#Region "Imports"
#End Region

Public Class EndoTestUserCtrlEncoderPresenter
    Inherits UserCtrlEncoderPresenter

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
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As EndoTestUserCtrlEncoderPresenter
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As EndoTestUserCtrlEncoderPresenter = New EndoTestUserCtrlEncoderPresenter
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Shadows Function GetInstance() As EndoTestUserCtrlEncoderPresenter
        Return New EndoTestUserCtrlEncoderPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function ProcessMsg(ByRef [object] As Object) As Boolean
        Debug.WriteLine("EndoTestUserCtrlEncoderPresenter.ProcessMsg " & CStr([object]))
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
