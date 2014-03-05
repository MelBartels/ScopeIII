Public Class AirMass
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

    'Public Shared Function GetInstance() As AirMass
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AirMass = New AirMass
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AirMass
        Return New AirMass
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function ToString(ByVal rad As Double) As String
        Dim str As String = rad.ToString
        Dim len As Int32 = str.Length
        If len > 4 Then
            len = 4
        End If
        Return str.Substring(0, len)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class