Public Class MainJRKerrExample
    Inherits MainPrototype

#Region "Inner Classes"
    Private Class MainJRKerrInput : Inherits MainPrototype
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

    'Public Shared Function GetInstance() As MainJRKerrExample
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainJRKerrExample = New MainJRKerrExample
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainJRKerrExample
        Return New MainJRKerrExample
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        CType(New Devices.Form1, Form).ShowDialog()
    End Sub
#End Region

End Class
