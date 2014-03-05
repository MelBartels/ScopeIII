#Region "imports"
#End Region

Public Class RenderScalarGaugesOKPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
    Class RenderGaugeUtil
        Private pMVPUserCtrlGaugeBase As MVPUserCtrlGaugeBase
        Private pObservableImpGauge As ObservableImp = ObservableImp.GetInstance
        Public Property MVPUserCtrlGaugeBase() As MVPUserCtrlGaugeBase
            Get
                Return pMVPUserCtrlGaugeBase
            End Get
            Set(ByVal value As MVPUserCtrlGaugeBase)
                pMVPUserCtrlGaugeBase = value
            End Set
        End Property
        Public Property IRendererGauge() As IRenderer
            Get
                Return MVPUserCtrlGaugeBase.IRenderer
            End Get
            Set(ByVal Value As IRenderer)
                MVPUserCtrlGaugeBase.IRenderer = Value
            End Set
        End Property
        Public Property ValueGauge() As Double
            Get
                Return CDbl(MVPUserCtrlGaugeBase.IRenderer.ObjectToRender)
            End Get
            Set(ByVal Value As Double)
                MeasurementToPointGauge(Value)
            End Set
        End Property
        Public Property ObservableImpGauge() As ObservableImp
            Get
                Return pObservableImpGauge
            End Get
            Set(ByVal Value As ObservableImp)
                pObservableImpGauge = Value
            End Set
        End Property
        Public Sub MeasurementToPointGauge(ByVal value As Double)
            pObservableImpGauge.Notify(CObj(value))
            MVPUserCtrlGaugeBase.IRenderer.ObjectToRender = CObj(value)
            MVPUserCtrlGaugeBase.Render()
        End Sub
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmRenderScalarGaugesOK As FrmRenderScalarGaugesOK
    Private pRenderGaugeUtil1 As RenderGaugeUtil
    Private pRenderGaugeUtil2 As RenderGaugeUtil
    Private pRenderGaugeUtil3 As RenderGaugeUtil
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ScalarGaugesOKPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RenderScalarGaugesOKPresenter = New RenderScalarGaugesOKPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        RenderGaugeUtil1 = New RenderGaugeUtil
        RenderGaugeUtil2 = New RenderGaugeUtil
        RenderGaugeUtil3 = New RenderGaugeUtil
    End Sub

    Public Shared Function GetInstance() As RenderScalarGaugesOKPresenter
        Return New RenderScalarGaugesOKPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property RenderGaugeUtil1() As RenderGaugeUtil
        Get
            Return pRenderGaugeUtil1
        End Get
        Set(ByVal value As RenderGaugeUtil)
            pRenderGaugeUtil1 = value
        End Set
    End Property

    Public Property RenderGaugeUtil2() As RenderGaugeUtil
        Get
            Return pRenderGaugeUtil2
        End Get
        Set(ByVal value As RenderGaugeUtil)
            pRenderGaugeUtil2 = value
        End Set
    End Property

    Public Property RenderGaugeUtil3() As RenderGaugeUtil
        Get
            Return pRenderGaugeUtil3
        End Get
        Set(ByVal value As RenderGaugeUtil)
            pRenderGaugeUtil3 = value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmRenderScalarGaugesOK = CType(IMVPView, FrmRenderScalarGaugesOK)

        RenderGaugeUtil1.MVPUserCtrlGaugeBase = pFrmRenderScalarGaugesOK.MvpUserCtrlGaugeBase1
        AddHandler pFrmRenderScalarGaugesOK.MeasurementToPointGauge1, AddressOf RenderGaugeUtil1.MeasurementToPointGauge

        RenderGaugeUtil2.MVPUserCtrlGaugeBase = pFrmRenderScalarGaugesOK.MvpUserCtrlGaugeBase2
        AddHandler pFrmRenderScalarGaugesOK.MeasurementToPointGauge2, AddressOf RenderGaugeUtil2.MeasurementToPointGauge

        RenderGaugeUtil3.MVPUserCtrlGaugeBase = pFrmRenderScalarGaugesOK.MvpUserCtrlGaugeBase3
        AddHandler pFrmRenderScalarGaugesOK.MeasurementToPointGauge3, AddressOf RenderGaugeUtil3.MeasurementToPointGauge
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
