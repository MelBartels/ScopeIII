Public Class XmlHMSM
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

    'Public Shared Function GetInstance() As XmlHMSM
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As XmlHMSM = New XmlHMSM
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As XmlHMSM
        Return New XmlHMSM
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function ToString(ByVal rad As Double) As String
        pRad = rad
        getHMSM(rad)

        Dim sb As New Text.StringBuilder
        sb.Append("<")
        sb.Append(CoordXmlTags.RightAscension.Name)
        sb.Append(">")
        sb.Append("<")
        sb.Append(CoordXmlTags.Sign.Name)
        sb.Append(">")
        sb.Append(pSign)
        sb.Append("</")
        sb.Append(CoordXmlTags.Sign.Name)
        sb.Append(">")
        sb.Append("<")
        sb.Append(CoordXmlTags.Hours.Name)
        sb.Append(">")
        sb.Append(pHr.ToString)
        sb.Append("</")
        sb.Append(CoordXmlTags.Hours.Name)
        sb.Append(">")
        sb.Append("<")
        sb.Append(CoordXmlTags.Minutes.Name)
        sb.Append(">")
        sb.Append(pMin.ToString())
        sb.Append("</")
        sb.Append(CoordXmlTags.Minutes.Name)
        sb.Append(">")
        sb.Append("<")
        sb.Append(CoordXmlTags.Seconds.Name)
        sb.Append(">")
        sb.Append(pSecDouble.ToString())
        sb.Append("</")
        sb.Append(CoordXmlTags.Seconds.Name)
        sb.Append(">")
        sb.Append("</")
        sb.Append(CoordXmlTags.RightAscension.Name)
        sb.Append(">")
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class