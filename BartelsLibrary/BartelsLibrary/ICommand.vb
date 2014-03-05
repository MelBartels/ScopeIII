Public Interface ICommand
    Function Execute() As Boolean
    Function Undo() As Boolean
End Interface
