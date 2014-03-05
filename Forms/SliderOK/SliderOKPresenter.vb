#Region "imports"
#End Region

Public Class SliderOKPresenter
    Inherits MVPPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmSliderOK As FrmSliderOK
    Private pMVPUserCtrlGaugeBase As MVPUserCtrlGaugeBase
    Private pObservableImp As ObservableImp
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SliderOKPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SliderOKPresenter = New SliderOKPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pObservableImp = ObservableImp.GetInstance
    End Sub

    Public Shared Function GetInstance() As SliderOKPresenter
        Return New SliderOKPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IRenderer() As IRenderer
        Get
            Return pMVPUserCtrlGaugeBase.IRenderer
        End Get
        Set(ByVal Value As IRenderer)
            pMVPUserCtrlGaugeBase.IRenderer = Value
        End Set
    End Property

    Public Property Value() As Double
        Get
            Return CDbl(pMVPUserCtrlGaugeBase.IRenderer.ObjectToRender)
        End Get
        Set(ByVal Value As Double)
            MeasurementToPoint(Value)
        End Set
    End Property

    Public Property ObservableImp() As ObservableImp
        Get
            Return pObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pObservableImp = Value
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg

    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmSliderOK = CType(IMVPView, FrmSliderOK)
        pMVPUserCtrlGaugeBase = pFrmSliderOK.MvpUserCtrlGaugeBase
        AddHandler pFrmSliderOK.MeasurementToPoint, AddressOf MeasurementToPoint
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub measurementToPoint(ByVal value As Double)
        pObservableImp.Notify(CObj(value))
        pMVPUserCtrlGaugeBase.IRenderer.ObjectToRender = CObj(value)
        pMVPUserCtrlGaugeBase.Render()
    End Sub
#End Region

End Class
