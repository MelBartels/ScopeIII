Public Class FormattedHMSM
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

    'Public Shared Function GetInstance() As FormattedHMSM
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As FormattedHMSM = New FormattedHMSM
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As FormattedHMSM
        Return New FormattedHMSM
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function ToString(ByVal rad As Double) As String
        pRad = rad

        getHMSM(rad)

        Dim sb As New Text.StringBuilder
        If pSign.Equals(BartelsLibrary.Constants.Minus) Then
            sb.Append(pSign)
        End If
        sb.Append(pHr.ToString("00"))
        sb.Append("h ")
        sb.Append(pMin.ToString("00"))
        sb.Append("m ")
        sb.Append(pSecDouble.ToString("00.000"))
        sb.Append("s")
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class