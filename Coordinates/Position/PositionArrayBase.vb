Public Class PositionArrayBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Filename As String
    Public IPositionArrayIO As IPositionArrayIO
#End Region

#Region "Private and Protected Members"
    Protected pPositionArray As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PositionArrayBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PositionArrayBase = New PositionArrayBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pPositionArray = New ArrayList
    End Sub

    Public Shared Function GetInstance() As PositionArrayBase
        Return New PositionArrayBase
    End Function
#End Region

#Region "Public and Friend Methods"
    Public Overloads Function PositionArray() As ArrayList
        Return pPositionArray
    End Function

    Public Overloads Sub PositionArray(ByRef positionArray As ArrayList)
        pPositionArray = positionArray
    End Sub

    Public Function GetPosition() As Position
        Return GetPosition(String.Empty)
    End Function

    Public Function GetPosition(ByVal posName As String) As Position
        Dim position As position
        For Each position In pPositionArray
            If position.Available Then
                position.Available = False
                position.PosName = posName
                Return position
            End If
        Next
        position = Coordinates.Position.GetInstance
        position.Available = False
        position.PosName = posName
        pPositionArray.Add(position)
        Return position
    End Function

    Sub Import(ByVal filename As String)
        pPositionArray = IPositionArrayIO.Import(filename)
    End Sub

    Sub Export(ByVal filename As String)
        IPositionArrayIO.Export(filename, pPositionArray)
    End Sub

#End Region

#Region "Private and Protected Methods"
    Private Function BuildFilename(ByVal prepend As String) As String
        Filename = prepend '& ScopeLibrary.Constants.PositionArrayFilenamePostpend
        Return Filename
    End Function
#End Region

End Class
