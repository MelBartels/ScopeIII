Public Interface IPositionArrayIO
    Function Import(ByVal filename As String) As ArrayList
    Sub Export(ByVal filename As String, ByRef positionArray As ArrayList)
End Interface
