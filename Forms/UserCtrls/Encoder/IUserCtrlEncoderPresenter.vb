Public Interface IUserCtrlEncoderPresenter
    Inherits IMVPUserCtrlPresenter

    Property EncoderValue() As EncoderValue
    Sub BuildIRenderer(ByRef encoderValue As EncoderValue)
End Interface
