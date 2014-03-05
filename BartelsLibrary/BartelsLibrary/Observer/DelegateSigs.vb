Imports System.Drawing
Imports System.ComponentModel

Public Class DelegateSigs
    Delegate Sub DelegateNone()
    Delegate Sub DelegateDbl(ByVal [double] As Double)
    Delegate Sub DelegateObj(ByVal [object] As Object)
    Delegate Sub DelegateStr(ByVal [string] As String)
    Delegate Sub DelegateGraphics(ByRef [Graphics] As Graphics)
    Delegate Sub DelegateISFT(ByVal [ISFT] As ISFT)
    Delegate Sub DelegateObjDoWorkEventArgs(ByVal sender As Object, ByVal e As DoWorkEventArgs)

    Delegate Function DelegateDblDblAsDbl(ByVal d1 As Double, ByVal d2 As Double) As Double
    Delegate Function DelegateDblDblAsAZDouble(ByVal d1 As Double, ByVal d2 As Double) As AZdouble
    Delegate Function DelegateDblDblDblAsDbl(ByVal d1 As Double, ByVal d2 As Double, ByVal d3 As Double) As Double
    Delegate Function DelegateObjAsBool(ByRef [object] As Object) As Boolean
    Delegate Function DelegateDblAsStr(ByVal [string] As Double) As String
End Class
