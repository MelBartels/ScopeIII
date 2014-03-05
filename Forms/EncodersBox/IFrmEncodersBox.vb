Public Interface IFrmEncodersBox
    Event Properties()
    Event DisplayDevice()
    Event CloseForm()
    Event Logging(ByVal switch As Boolean)

    Property Title() As String
    Sub DisplayStatus(ByVal msg As String)
    Sub SetToolTip()
    Function GetUserCtrlEncoderPri() As UserCtrlEncoder
    Function GetUserCtrlEncoderSec() As UserCtrlEncoder
    Function GetUserCtrlTerminal() As UserCtrlTerminal
    Function GetUserCtrlLogging() As UserCtrlLogging
End Interface
