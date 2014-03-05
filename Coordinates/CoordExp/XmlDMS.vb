Public Class XmlDMS
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

    'Public Shared Function GetInstance() As XmlDMS
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As XmlDMS = New XmlDMS
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As XmlDMS
        Return New XmlDMS
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function ToString(ByVal rad As Double) As String
        pRad = rad
        getDMS(rad)

        Dim sb As New Text.StringBuilder
        sb.Append("<")
        sb.Append(CoordXmlTags.Declination.Name)
        sb.Append(">")
        sb.Append("<")
        sb.Append(CoordXmlTags.Sign.Name)
        sb.Append(">")
        sb.Append(pSign)
        sb.Append("</")
        sb.Append(CoordXmlTags.Sign.Name)
        sb.Append(">")
        sb.Append("<")
        sb.Append(CoordXmlTags.Degrees.Name)
        sb.Append(">")
        sb.Append(pDeg.ToString)
        sb.Append("</")
        sb.Append(CoordXmlTags.Degrees.Name)
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
        sb.Append(pSec.ToString())
        sb.Append("</")
        sb.Append(CoordXmlTags.Seconds.Name)
        sb.Append(">")
        sb.Append("</")
        sb.Append(CoordXmlTags.Declination.Name)
        sb.Append(">")
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class