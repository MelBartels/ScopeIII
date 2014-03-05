#Region "Imports"
#End Region

Public Class EndoTestEncodersBoxCtrlPresenter
    Inherits EncodersBoxCtrlPresenter

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

    '    Public Shared Function GetInstance() As EndoTestEncodersBoxCtrlPresenter
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As EndoTestEncodersBoxCtrlPresenter = New EndoTestEncodersBoxCtrlPresenter
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Shadows Function GetInstance() As EndoTestEncodersBoxCtrlPresenter
        Return New EndoTestEncodersBoxCtrlPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function EndoTestUserCtrlTerminalPresenter() As EndoTestUserCtrlTerminalPresenter
        Return CType(pUserCtrlTerminalPresenter, EndoTestUserCtrlTerminalPresenter)
    End Function

    Public Overrides Function ProcessMsg(ByRef [object] As Object) As Boolean
        Debug.WriteLine("EndoTestEncodersBoxCtrlPresenter.ProcessMsg " & CStr([object]))
    End Function

    Public Sub EndoTestSendQueryCmd()
        sendCmd(TangentEncodersQueryCmds.Query.Key)
    End Sub

    Public Function AxisEncoderTranslatorPriValue() As String
        Return userCtrl2AxisEncoderTranslatorPresenter.UserCtrlAxisEncoderTranslatorPresenterPri.AxisEncoderAdapter.EncoderValue.Value
    End Function

    Public Function AxisEncoderTranslatorSecValue() As String
        Return userCtrl2AxisEncoderTranslatorPresenter.UserCtrlAxisEncoderTranslatorPresenterSec.AxisEncoderAdapter.EncoderValue.Value
    End Function

    Public Sub StartLogging()
        logging(True)
    End Sub

    Public Sub StopLogging()
        pLoggingObserver.Close()
    End Sub

    Public Function LogFilename() As String
        Return pLoggingObserver.Filename
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function userCtrl2AxisEncoderTranslatorPresenter() As UserCtrl2AxisEncoderTranslatorPresenter
        Return CType(pITwoAxisEncoderTranslatorPresenter.IGauge2AxisCoordPresenter, UserCtrl2AxisEncoderTranslatorPresenter)
    End Function
#End Region

End Class
