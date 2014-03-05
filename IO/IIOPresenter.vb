Public Interface IIOPresenter
    Property IIO() As IIO
    Function BuildIO(ByRef ioType As ISFT) As Boolean
    Sub ShutdownIO()
End Interface
