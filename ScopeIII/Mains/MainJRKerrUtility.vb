Public Class MainJRKerrUtility
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

    'Public Shared Function GetInstance() As MainJRKerrUtility
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainJRKerrUtility = New MainJRKerrUtility
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainJRKerrUtility
        Return New MainJRKerrUtility
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        CommonShared.ShellExecute("NMCTEST.exe")
    End Sub
#End Region

End Class
