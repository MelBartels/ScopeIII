Public Class Site

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Name As String
    Public Latitude As Coordinate
    Public Longitude As Coordinate
    Public TimeZone As Double
    Public ElevationMeters As Double
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Site
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Site = New Site
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        Latitude = Coordinate.GetInstance
        Longitude = Coordinate.GetInstance
    End Sub

    Public Shared Function GetInstance() As Site
        Return New Site
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class