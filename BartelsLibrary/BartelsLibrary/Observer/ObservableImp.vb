Public Class ObservableImp
    Implements IObservable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pObservers As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ObservableImp
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ObservableImp = New ObservableImp
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pObservers = New ArrayList
    End Sub

    Public Shared Function GetInstance() As ObservableImp
        Return New ObservableImp
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Attach(ByRef observer As IObserver) Implements IObservable.Attach
        pObservers.Add(observer)
    End Sub

    Public Sub Detach(ByRef observer As IObserver) Implements IObservable.Detach
        pObservers.Remove(observer)
    End Sub

    Public Sub Notify(ByRef [object] As Object) Implements IObservable.Notify
        For Each observer As IObserver In pObservers
            observer.ProcessMsg([object])
        Next
    End Sub

    Public Overloads Function Observers() As ArrayList Implements BartelsLibrary.IObservable.Observers
        Return pObservers
    End Function

    Public Overloads Sub Observers(ByRef observers As ArrayList) Implements BartelsLibrary.IObservable.Observers
        pObservers = observers
    End Sub

#End Region

#Region "Private and Protected Methods"
#End Region

End Class
