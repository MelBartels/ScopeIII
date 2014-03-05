Public Interface IDependencyResolver
    Function Resolve(Of T)() As T
End Interface
