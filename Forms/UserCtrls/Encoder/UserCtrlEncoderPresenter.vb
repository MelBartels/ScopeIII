#Region "imports"
#End Region

Public Class UserCtrlEncoderPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IObserver, IUserCtrlEncoderPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Property IRenderer() As IRenderer
        Get
            Return pMvpUserCtrlGaugeBase.IRenderer
        End Get
        Set(ByVal Value As IRenderer)
            pMvpUserCtrlGaugeBase.IRenderer = Value
        End Set
    End Property

    Public Property EncoderValue() As EncoderValue Implements IUserCtrlEncoderPresenter.EncoderValue
        Get
            Return pEncoderValue
        End Get
        Set(ByVal value As EncoderValue)
            pEncoderValue = value
        End Set
    End Property
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlEncoder As UserCtrlEncoder
    Private WithEvents pMvpUserCtrlGaugeBase As MVPUserCtrlGaugeBase
    Private pEncoderValue As EncoderValue
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlEncoderPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlEncoderPresenter = New UserCtrlEncoderPresenter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlEncoderPresenter
        Return New UserCtrlEncoderPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overridable Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        IRenderer.ObjectToRender = [object]
        Render()
    End Function

    Public Sub Render()
        pMvpUserCtrlGaugeBase.Render()
    End Sub

    Public Sub BuildIRenderer(ByRef encoderValue As EncoderValue) Implements IUserCtrlEncoderPresenter.BuildIRenderer
        Me.EncoderValue = EncoderValue
        IRenderer = ValueObjectRenderer.GetInstance
        CType(IRenderer, ValueObjectRenderer).ValueObject = Me.EncoderValue
        EncoderValue.ObservableImp.Attach(Me)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlEncoder = CType(IMVPUserCtrl, UserCtrlEncoder)
        pMvpUserCtrlGaugeBase = pUserCtrlEncoder.MvpUserCtrlGaugeBase

        AddHandler pUserCtrlEncoder.MeasurementToPoint, AddressOf MeasurementToPoint
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub measurementToPoint(ByVal value As Double)
        EncoderValue.Value = EncoderValue.ConvertRadToTicks(value)
    End Sub
#End Region

End Class
