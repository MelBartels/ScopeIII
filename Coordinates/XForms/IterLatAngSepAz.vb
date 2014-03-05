Public Class IterLatAngSepAz
    Implements IIterLatAngSep

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IterLatAngSepAz
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IterLatAngSepAz = New IterLatAngSepAz
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As IterLatAngSepAz
        Return New IterLatAngSepAz
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Calc(ByRef a As Coordinates.Position, ByRef z As Coordinates.Position) As Double Implements IIterLatAngSep.Calc
        Return Math.Abs(a.Az.Rad - z.Az.Rad)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
