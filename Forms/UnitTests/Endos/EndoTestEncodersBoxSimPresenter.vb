#Region "Imports"
#End Region

Public Class EndoTestEncodersBoxSimPresenter
    Inherits EncodersBoxSimPresenter

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

    '    Public Shared Function GetInstance() As EndoTestEncodersBoxSimPresenter
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As EndoTestEncodersBoxSimPresenter = New EndoTestEncodersBoxSimPresenter
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Shadows Function GetInstance() As EndoTestEncodersBoxSimPresenter
        Return New EndoTestEncodersBoxSimPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public ReadOnly Property EndoTestUserCtrlTerminalPresenter() As EndoTestUserCtrlTerminalPresenter
        Get
            Return CType(pUserCtrlTerminalPresenter, Forms.EndoTestUserCtrlTerminalPresenter)
        End Get
    End Property

    Public Overrides Function ProcessMsg(ByRef [object] As Object) As Boolean
        Debug.WriteLine("EndoTestEncodersBoxSimPresenter.ProcessMsg " & CStr([object]))
    End Function

    Public Sub SetEncoderValues(ByVal priValue As Int32, ByVal secValue As Int32)
        getSettingsEncoderValuePri.Value = CStr(priValue)
        getSettingsEncoderValueSec.Value = CStr(secValue)
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
