#Region "Imports"
#End Region

Public Class CelestialErrorsPresenterMediator

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event ErrorsToIncludeChanged()
    Public FormLoadCallbackDelegate As [Delegate]
#End Region

#Region "Private and Protected Members"
    Private WithEvents pShowCelestialErrorsPresenter As ShowCelestialErrorsPresenter
    Private pDisplayingCelestialErrors As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As CelestialErrorsPresenterMediator
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As CelestialErrorsPresenterMediator = New CelestialErrorsPresenterMediator
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
        build()
    End Sub

    Public Shared Function GetInstance() As CelestialErrorsPresenterMediator
        Return New CelestialErrorsPresenterMediator
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property DisplayingCelestialErrors() As Boolean
        Get
            Return pDisplayingCelestialErrors
        End Get
        Set(ByVal value As Boolean)
            pDisplayingCelestialErrors = value
            CelestialErrorsCalculatorFacade.UseCalculator = pDisplayingCelestialErrors
        End Set
    End Property

    Public Function Presenter() As IMVPPresenter
        Return pShowCelestialErrorsPresenter
    End Function

    Public Function CelestialErrorsCalculatorFacade() As CelestialErrorsCalculatorFacade
        Return CType(Presenter(), ShowCelestialErrorsPresenter).CelestialErrorsCalculatorFacade
    End Function

    Public Sub BringFormToFront()
        Dim frm As Windows.Forms.Form = CType(pShowCelestialErrorsPresenter.IMVPView, Form)
        If frm.InvokeRequired Then
            frm.Invoke(New MethodInvoker(AddressOf BringFormToFront))
        Else
            frm.TopMost = True
            frm.Refresh()
            frm.TopMost = False
        End If
    End Sub

    Public Sub Startup()
        If DisplayingCelestialErrors Then
            BringFormToFront()
        Else
            pShowCelestialErrorsPresenter.SetCelestialErrorsCalculatorFacadeFromView()
            DisplayingCelestialErrors = True
            Dim CelestialErrorsThread As New Threading.Thread(AddressOf celestialErrorsWork)
            CelestialErrorsThread.Name = "Convert Presenter Celestial Errors"
            CelestialErrorsThread.Start()
        End If
    End Sub

    Public Sub Shutdown()
        If DisplayingCelestialErrors Then
            pShowCelestialErrorsPresenter.Close()
        End If
    End Sub

    Public Sub UpdateDataModel(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double, ByVal latitudeRad As Double)
        If DisplayingCelestialErrors Then
            pShowCelestialErrorsPresenter.UpdateDataModel(RaRad, DecRad, SidTRad, latitudeRad)
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Sub build()
        pShowCelestialErrorsPresenter = Forms.ShowCelestialErrorsPresenter.GetInstance
        AddHandler pShowCelestialErrorsPresenter.FormInvisible, AddressOf formInvisible
        AddHandler pShowCelestialErrorsPresenter.ErrorsToIncludeChanged, AddressOf errorsToIncludeChangedHandler
        pShowCelestialErrorsPresenter.StartingIncludeRefraction = True
        Dim frmShowCelestialErrors As New FrmShowCelestialErrors
        frmShowCelestialErrors.StartPosition = FormStartPosition.WindowsDefaultLocation
        frmShowCelestialErrors.FormLoadCallbackDelegate = FormLoadCallbackDelegate
        pShowCelestialErrorsPresenter.IMVPView = frmShowCelestialErrors
    End Sub

    Private Sub celestialErrorsWork()
        pShowCelestialErrorsPresenter.ShowDialog()
        DisplayingCelestialErrors = False
    End Sub

    Private Sub formInvisible()
        DisplayingCelestialErrors = False
    End Sub

    Private Sub errorsToIncludeChangedHandler()
        RaiseEvent ErrorsToIncludeChanged()
    End Sub
#End Region

End Class
