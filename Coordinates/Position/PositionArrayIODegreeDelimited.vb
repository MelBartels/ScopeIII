#Region "Imports"
Imports System.IO
Imports System.Runtime.Serialization
#End Region

Public Class PositionArrayIODegreeDelimited
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
    Dim pCet As ISFT
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PositionArrayIODegreeDelimited
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PositionArrayIODegreeDelimited = New PositionArrayIODegreeDelimited
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pCet = CoordExpType.Degree
    End Sub

    Public Shared Function GetInstance() As PositionArrayIODegreeDelimited
        Return New PositionArrayIODegreeDelimited
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

    Public Overrides Sub Export(ByVal filename As String, ByRef positionArray As ArrayList)
        Dim writer As StreamWriter
        Try
            writer = New StreamWriter(filename)
            For Each position As Position In positionArray
                writer.Write(position.RA.ToString(pCet) & BartelsLibrary.Constants.Delimiter)
                writer.Write(position.Dec.ToString(pCet) & BartelsLibrary.Constants.Delimiter)
                writer.Write(position.Az.ToString(pCet) & BartelsLibrary.Constants.Delimiter)
                writer.Write(position.Alt.ToString(pCet) & BartelsLibrary.Constants.Delimiter)
                writer.Write(position.SidT.ToString(pCet) & BartelsLibrary.Constants.Delimiter)
                writer.Write(position.ObjName)
                writer.WriteLine(String.Empty)
            Next
            writer.Close()
        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try
    End Sub

    Public Overrides Function Import(ByVal filename As String) As ArrayList
        Dim positionArray As New ArrayList

        Dim reader As StreamReader
        Dim line As String
        reader = New StreamReader(filename)
        line = reader.ReadLine
        While Not line Is Nothing
            Dim st As StringTokenizer = StringTokenizer.GetInstance
            st.Tokenize(line)
            If st.Count >= 5 Then
                Dim position As Position = Coordinates.Position.GetInstance
                position.RA.Rad = st.GetNextDouble * Units.DegToRad
                position.Dec.Rad = st.GetNextDouble * Units.DegToRad
                position.Az.Rad = st.GetNextDouble * Units.DegToRad
                position.Alt.Rad = st.GetNextDouble * Units.DegToRad
                position.SidT.Rad = st.GetNextDouble * Units.DegToRad
                position.ObjName = st.StringToEOL
                positionArray.Add(position)
            End If
            line = reader.ReadLine
        End While
        reader.Close()

        Return positionArray
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class