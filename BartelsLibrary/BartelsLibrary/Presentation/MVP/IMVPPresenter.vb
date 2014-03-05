Public Interface IMVPPresenter
    Property IMVPView() As IMVPView
    Property DataModel() As Object
    Sub ShowDialog()
    Sub Show()
    Sub Close()
End Interface
