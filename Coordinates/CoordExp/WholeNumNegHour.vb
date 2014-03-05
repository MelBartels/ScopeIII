Public Class WholeNumNegHour
    Inherits CoordExpBase

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

    'Public Shared Function GetInstance() As WholeNumNegHour
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As WholeNumNegHour = New WholeNumNegHour
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As WholeNumNegHour
        Return New WholeNumNegHour
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function ToString(ByVal rad As Double) As String
        Dim sb As New Text.StringBuilder
        sb.Append((eMath.ValidRadPi(rad) * Units.RadToHr).ToString("#0"))
        sb.Append("h")
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class