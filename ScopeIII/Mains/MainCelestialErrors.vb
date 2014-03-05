Public Class MainCelestialErrors
    Inherits MainPrototype

#Region "Inner Classes"
    Private Class mainCelestialErrorsInput : Inherits MainPrototype
        Public IObserver As IObserver
        Protected Overrides Sub work()
            Dim showCelestialErrorsInputPresenter As ShowCelestialErrorsInputPresenter = Forms.ShowCelestialErrorsInputPresenter.GetInstance
            showCelestialErrorsInputPresenter.IMVPView = New FrmShowCelestialErrorsInput
            showCelestialErrorsInputPresenter.CoordinateObserver(IObserver)
            showCelestialErrorsInputPresenter.ShowDialog()
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
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainCelestialErrors
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainCelestialErrors = New MainCelestialErrors
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainCelestialErrors
        Return New MainCelestialErrors
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim showCelestialErrorsPresenter As ShowCelestialErrorsPresenter = Forms.ShowCelestialErrorsPresenter.GetInstance
        showCelestialErrorsPresenter.UseCalculator = True
        showCelestialErrorsPresenter.StartingIncludeRefraction = True
        showCelestialErrorsPresenter.IMVPView = New FrmShowCelestialErrors

        Dim mainCelestialErrorsInput As New mainCelestialErrorsInput
        mainCelestialErrorsInput.IObserver = showCelestialErrorsPresenter
        mainCelestialErrorsInput.Main()

        showCelestialErrorsPresenter.ShowDialog()
    End Sub
#End Region

End Class
