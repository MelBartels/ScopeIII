#Region "Imports"
#End Region

Public Class EndoTestTwoAxisEncoderTranslatorPresenter
    Inherits TwoAxisEncoderTranslatorPresenter

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

    '    Public Shared Function GetInstance() As EndoTestTwoAxisEncoderTranslatorPresenter
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As EndoTestTwoAxisEncoderTranslatorPresenter = New EndoTestTwoAxisEncoderTranslatorPresenter
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Shadows Function GetInstance() As EndoTestTwoAxisEncoderTranslatorPresenter
        Return New EndoTestTwoAxisEncoderTranslatorPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Sub ShowDialog()
        ' don't show form: override default behavior
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
