Public Class MainArcGaugeOK
    Inherits MainPrototype

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pArcGaugeOKPresenter As ArcGaugeOKPresenter
    Protected pValueObserverPresenter As ValueObserverPresenter
    Protected pConnected As Int32
    Protected pMutex As New Threading.Mutex
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainArcGaugeOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainArcGaugeOK = New MainArcGaugeOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainArcGaugeOK
        Return New MainArcGaugeOK
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim frmThreadArcGauge As New Threading.Thread(AddressOf gaugeForm)
        frmThreadArcGauge.Start()
        Dim frmThreadObs As New Threading.Thread(AddressOf observerForm)
        frmThreadObs.Start()
    End Sub

    Protected Sub gaugeForm()
        pArcGaugeOKPresenter = ArcGaugeOKPresenter.GetInstance
        pArcGaugeOKPresenter.IMVPView = New FrmArcGaugeOK
        pArcGaugeOKPresenter.IRenderer = ScalarArcGaugeRenderer.GetInstance
        addToConnect()
        pArcGaugeOKPresenter.ShowDialog()
    End Sub

    Protected Sub observerForm()
        pValueObserverPresenter = ValueObserverPresenter.GetInstance
        pValueObserverPresenter.IMVPView = New FrmValueObserver
        addToConnect()
        pValueObserverPresenter.ShowDialog()
    End Sub

    Protected Sub connectObserver()
        ' create an observer and set its presenter to the presenter instantiated above
        Dim valueObserver As ValueObserver = Forms.ValueObserver.GetInstance
        valueObserver.ValueObserverPresenter = pValueObserverPresenter
        ' now attach the observer just created to the presenter's value observer
        pArcGaugeOKPresenter.ObservableImp.Attach(CType(valueObserver, IObserver))
        ' set starting value
        pArcGaugeOKPresenter.Value = 0
    End Sub

    Protected Sub addToConnect()
        pMutex.WaitOne()
        pConnected += 1
        If pConnected.Equals(2) Then
            connectObserver()
        End If
        pMutex.ReleaseMutex()
    End Sub
#End Region

End Class
