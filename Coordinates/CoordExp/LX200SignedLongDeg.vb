Public Class LX200SignedLongDeg
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

    'Public Shared Function GetInstance() As LX200SignedLongDeg
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As LX200SignedLongDeg = New LX200SignedLongDeg
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As LX200SignedLongDeg
        Return New LX200SignedLongDeg
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function ToString(ByVal rad As Double) As String
        pRad = rad

        getDMS(rad)

        Dim sb As New Text.StringBuilder
        sb.Append(pSign)
        sb.Append(pDeg.ToString("00"))
        sb.Append(LX200DegSym)
        sb.Append(pMin.ToString("00"))
        sb.Append(":")
        sb.Append(pSec.ToString("00"))
        sb.Append("#")
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class