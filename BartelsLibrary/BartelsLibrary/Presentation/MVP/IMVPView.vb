Public Interface IMVPView
    Event LoadViewFromModel()
    Event ViewUpdated()
    Event SaveToModel()

    Sub ShowDialog()
    Sub Show()
    Sub Close()
End Interface
