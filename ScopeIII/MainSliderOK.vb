Public Class MainSliderOK
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
    Protected pSliderOKPresenter As SliderOKPresenter
    Protected pValueObserverPresenter As ValueObserverPresenter
    Protected pConnected As Int32
    Protected pMutex As New Threading.Mutex
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainSliderOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainSliderOK = New MainSliderOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainSliderOK
        Return New MainSliderOK
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim frmThreadSlider As Threading.Thread = New Threading.Thread(AddressOf gaugeForm)
        frmThreadSlider.Start()
        Dim frmThreadObs As Threading.Thread = New Threading.Thread(AddressOf observerForm)
        frmThreadObs.Start()
    End Sub

    Protected Sub gaugeForm()
        pSliderOKPresenter = SliderOKPresenter.GetInstance
        pSliderOKPresenter.IMVPView = New FrmSliderOK
        pSliderOKPresenter.IRenderer = SliderRenderer.GetInstance
        addToConnect(2)
        pSliderOKPresenter.ShowDialog()
    End Sub

    Protected Sub observerForm()
        pValueObserverPresenter = ValueObserverPresenter.GetInstance
        pValueObserverPresenter.IMVPView = New FrmValueObserver
        addToConnect(2)
        pValueObserverPresenter.ShowDialog()
    End Sub

    Protected Sub connectObserver()
        ' create an observer and set its presenter to the presenter instantiated above
        Dim valueObserver As ValueObserver = Forms.ValueObserver.GetInstance
        valueObserver.ValueObserverPresenter = pValueObserverPresenter
        ' now attach the observer just created to the presenter's value observer
        pSliderOKPresenter.ObservableImp.Attach(CType(valueObserver, IObserver))
        ' set starting value
        pSliderOKPresenter.Value = 0
    End Sub

    Protected Sub addToConnect(ByVal connectCount As Int32)
        pMutex.WaitOne()
        pConnected += 1
        If pConnected.Equals(connectCount) Then
            connectObserver()
        End If
        pMutex.ReleaseMutex()
    End Sub
#End Region

End Class
