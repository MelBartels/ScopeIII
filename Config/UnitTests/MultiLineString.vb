' custom type to edit in PG so that we can set a custom editor for it
Public Class MultiLineString
    Private pStr As String
    Public Sub New()
    End Sub
    Public Sub New(ByVal str As String)
        pStr = str
    End Sub
    Public Property Value() As String
        Get
            Return pStr
        End Get
        Set(ByVal Value As String)
            pStr = Value
        End Set
    End Property
    Public Overrides Function ToString() As String
        If pStr.Equals("") Then
            Return ""
        Else
            Return "..."
        End If
    End Function
End Class
