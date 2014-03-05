Public Class DMSSymbols
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

    'Public Shared Function GetInstance() As DMSSymbols
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DMSSymbols = New DMSSymbols
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DMSSymbols
        Return New DMSSymbols
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function ToString(ByVal rad As Double) As String
        pRad = rad

        getDMSDouble(rad)

        Dim sb As New Text.StringBuilder
        sb.Append(pSign)
        sb.Append(pDeg.ToString("00"))
        sb.Append("°")
        sb.Append(pMin.ToString("00"))
        sb.Append("'")
        sb.Append(pSecDouble.ToString("00.0"))
        sb.Append(BartelsLibrary.Constants.Quote)
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class