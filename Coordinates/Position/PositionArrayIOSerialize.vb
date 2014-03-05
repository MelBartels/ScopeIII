#Region "Imports"
Imports System.IO
Imports System.Runtime.Serialization
' add project reference to System.Runtime.Serialization.Formatters.Soap.dll
Imports System.Runtime.Serialization.Formatters.Soap
#End Region

Public Class PositionArrayIoSerialize
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

    'Public Shared Function GetInstance() As PositionArrayIoSerialize
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PositionArrayIoSerialize = New PositionArrayIoSerialize
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As PositionArrayIoSerialize
        Return New PositionArrayIoSerialize
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

    Public Overrides Sub Export(ByVal filename As String, ByRef positionArray As ArrayList)
        Serialize(filename, positionArray)
    End Sub

    Public Overrides Function Import(ByVal filename As String) As ArrayList
        Return Deserialize(filename)
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function Serialize(ByVal filename As String, ByRef positionArray As ArrayList) As String
        Dim fs As New FileStream(filename, FileMode.Create)
        Dim formatter As New SoapFormatter
        Try
            formatter.Serialize(fs, positionArray)
        Catch e As SerializationException
            DebugTrace.WriteLine("Serialization failed. " & e.Message)
            Throw
        Finally
            fs.Close()
        End Try
        Return filename
    End Function

    Private Function Deserialize(ByVal filename As String) As ArrayList
        Dim fs As New FileStream(filename, FileMode.Open)
        Dim positionArray As ArrayList = Nothing
        Try
            Dim formatter As New SoapFormatter
            Dim al As ArrayList
            al = DirectCast(formatter.Deserialize(fs), ArrayList)
            positionArray = New ArrayList
            For Each position As position In al
                positionArray.Add(position)
            Next
        Catch ex As Exception
            ExceptionService.Notify(ex)
        Finally
            fs.Close()
        End Try
        Return positionArray
    End Function
#End Region

End Class