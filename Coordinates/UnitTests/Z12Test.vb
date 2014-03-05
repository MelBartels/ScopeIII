#Region "Imports"
#End Region

Public Class Z12Test

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Position As Position
    Public AzError As Coordinate
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Z12Test
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Z12Test = New Z12Test
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        Position = Coordinates.Position.GetInstance
        AzError = Coordinate.GetInstance
    End Sub

    Public Shared Function GetInstance() As Z12Test
        Return New Z12Test
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
