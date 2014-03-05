Public Class SeparationIx
    Public Separation As Double
    Public Ix As Int32
End Class

Public Class SeparationIxComparer
    Implements IComparer
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Return CType(x, SeparationIx).Separation.CompareTo(CType(y, SeparationIx).Separation)
    End Function
End Class

