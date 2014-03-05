#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Public Class PositionArrayIODatafile
    Inherits PositionArrayIOBase

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

    'Public Shared Function GetInstance() As PositionArrayIODatafile
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PositionArrayIODatafile = New PositionArrayIODatafile
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As PositionArrayIODatafile
        Return New PositionArrayIODatafile
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

    Public Overrides Sub Export(ByVal filename As String, ByRef positionArray As ArrayList)
        DatafileWriter.GetInstance.Write(filename, positionArray)
    End Sub

    Public Overrides Function Import(ByVal filename As String) As ArrayList
        Dim positionArray As New ArrayList

        Dim datafileReader As DatafileReader = Coordinates.DatafileReader.GetInstance
        If datafileReader.Open(filename) Then

            Dim position As Position = Coordinates.Position.GetInstance
            While datafileReader.ReadValues(position.RA.Rad, position.Dec.Rad, position.ObjName)
                positionArray.Add(position)
                position = Coordinates.Position.GetInstance
            End While

            datafileReader.Close()
            Return positionArray
        End If

        Return Nothing
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class