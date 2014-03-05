Public Interface IObservable
    Sub Attach(ByRef observer As IObserver)
    Sub Detach(ByRef observer As IObserver)
    Sub Notify(ByRef [object] As Object)
    Sub Observers(ByRef observers As ArrayList)
    Function Observers() As ArrayList
End Interface
