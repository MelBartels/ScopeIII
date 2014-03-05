' wraps a Delegate Function
Public Class FuncDelegate(Of TValue, TContext)
    ' the predicate with context
    Public Delegate Function ContextPredicate(ByVal value As TValue, ByVal context As TContext) As Boolean
    ' the callback
    Private pContextPredicate As ContextPredicate
    ' the context
    Private pTContext As TContext
    ' save the callback and context
    Public Sub New(ByVal match As ContextPredicate, ByVal context As TContext)
        pContextPredicate = match
        pTContext = context
    End Sub
    ' predicate wrapper (eg, Find, FindAll) for the delegate with context
    Public Function CallDelegate(ByVal value As TValue) As Boolean
        Return pContextPredicate(value, pTContext)
    End Function
End Class

' wraps a Delegate Sub
Public Class SubDelegate(Of TValue, TContext)
    ' the predicate with context
    Public Delegate Sub ContextPredicate(ByVal value As TValue, ByVal context As TContext)
    ' the callback
    Private pContextPredicate As ContextPredicate
    ' the context
    Private pTContext As TContext
    ' save the callback and context
    Public Sub New(ByVal action As ContextPredicate, ByVal context As TContext)
        pContextPredicate = action
        pTContext = context
    End Sub
    ' predicate wrapper (eg, ForEach) for the delegate with context
    Public Sub CallDelegate(ByVal value As TValue)
        pContextPredicate(value, pTContext)
    End Sub
End Class

Public Class FuncDelegate2(Of TValue, TContext, TContext2)
    Public Delegate Function ContextPredicate(ByVal value As TValue, ByVal context As TContext, ByVal context2 As TContext2) As Boolean
    Private pContextPredicate As ContextPredicate
    Private pTContext As TContext
    Private pTContext2 As TContext2
    Public Sub New(ByVal match As ContextPredicate, ByVal context As TContext, ByVal context2 As TContext2)
        pContextPredicate = match
        pTContext = context
        pTContext2 = context2
    End Sub
    Public Function CallDelegate(ByVal value As TValue) As Boolean
        Return pContextPredicate(value, pTContext, pTContext2)
    End Function
End Class

Public Class SubDelegate2(Of TValue, TContext, TContext2)
    Public Delegate Sub ContextPredicate(ByVal value As TValue, ByVal context As TContext, ByVal context2 As TContext2)
    Private pContextPredicate As ContextPredicate
    Private pTContext As TContext
    Private pTContext2 As TContext2
    Public Sub New(ByVal Action As ContextPredicate, ByVal context As TContext, ByVal context2 As TContext2)
        pContextPredicate = Action
        pTContext = context
        pTContext2 = context2
    End Sub
    Public Sub CallDelegate(ByVal value As TValue)
        pContextPredicate(value, pTContext, pTContext2)
    End Sub
End Class

Public Class FuncDelegate3(Of TValue, TContext, TContext2, TContext3)
    Public Delegate Function ContextPredicate(ByVal value As TValue, ByVal context As TContext, ByVal context2 As TContext2, ByVal context3 As TContext3) As Boolean
    Private pContextPredicate As ContextPredicate
    Private pTContext As TContext
    Private pTContext2 As TContext2
    Private pTContext3 As TContext3
    Public Sub New(ByVal match As ContextPredicate, ByVal context As TContext, ByVal context2 As TContext2, ByVal context3 As TContext3)
        pContextPredicate = match
        pTContext = context
        pTContext2 = context2
        pTContext3 = context3
    End Sub
    Public Function CallDelegate(ByVal value As TValue) As Boolean
        Return pContextPredicate(value, pTContext, pTContext2, pTContext3)
    End Function
End Class

Public Class SubDelegate3(Of TValue, TContext, TContext2, TContext3)
    Public Delegate Sub ContextPredicate(ByVal value As TValue, ByVal context As TContext, ByVal context2 As TContext2, ByVal context3 As TContext3)
    Private pContextPredicate As ContextPredicate
    Private pTContext As TContext
    Private pTContext2 As TContext2
    Private pTContext3 As TContext3
    Public Sub New(ByVal Action As ContextPredicate, ByVal context As TContext, ByVal context2 As TContext2, ByVal context3 As TContext3)
        pContextPredicate = Action
        pTContext = context
        pTContext2 = context2
        pTContext3 = context3
    End Sub
    Public Sub CallDelegate(ByVal value As TValue)
        pContextPredicate(value, pTContext, pTContext2, pTContext3)
    End Sub
End Class

