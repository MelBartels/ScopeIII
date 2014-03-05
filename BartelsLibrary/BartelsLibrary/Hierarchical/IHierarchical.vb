Public Interface IHierarchical
    Function RegisterComponent(ByRef component As Object) As Boolean
    Function AddChild(ByRef parent As Object, ByVal name As String) As Object
End Interface

