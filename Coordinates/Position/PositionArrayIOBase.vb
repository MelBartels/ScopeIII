Public MustInherit Class PositionArrayIOBase
    Implements IPositionArrayIO

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

    'Public Shared Function GetInstance() As PositionArrayIOBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PositionArrayIOBase = New PositionArrayIOBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As PositionArrayIOBase
    '    Return New PositionArrayIOBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

    Public MustOverride Sub Export(ByVal filename As String, ByRef positionArray As ArrayList) Implements IPositionArrayIO.Export
    Public MustOverride Function Import(ByVal filename As String) As ArrayList Implements IPositionArrayIO.Import

#End Region

#Region "Private and Protected Methods"
#End Region

End Class