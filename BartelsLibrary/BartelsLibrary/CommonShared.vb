#Region "Imports"
#End Region
Imports System.IO
Imports System.Collections.ObjectModel

Public Class CommonShared

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

    'Public Shared Function GetInstance() As CommonShared
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CommonShared = New CommonShared
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CommonShared
        Return New CommonShared
    End Function
#End Region

#Region "Shared Methods"
    Public Shared Function GetFullyQualifiedDirectory(ByVal filename As String) As String
        Return IO.Path.GetDirectoryName(IO.Path.GetFullPath(filename))
    End Function

    Public Shared Function CreateDirectory(ByVal filename As String) As Boolean
        Dim dirName As String = GetFullyQualifiedDirectory(filename)
        If Not IO.Directory.Exists(dirName) Then
            IO.Directory.CreateDirectory(dirName)
        End If
        Return True
    End Function

    Public Shared Function DeleteDirectory(ByVal filename As String) As Boolean
        Dim dirName As String = GetFullyQualifiedDirectory(filename)
        If IO.Directory.Exists(dirName) Then
            IO.Directory.Delete(dirName)
            Return True
        End If
        Return False
    End Function

    Public Shared Function DeleteDirectoryWithFiles(ByVal directory As String) As Boolean
        If IO.Directory.Exists(directory) Then
            ' kill all the files in the directory preparatory to deleting the directory
            Dim files As ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetFiles(directory, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
            For Each file As String In files
                IO.File.Delete(file)
            Next
            IO.Directory.Delete(directory)
        End If
        Return True
    End Function

    Public Shared Function FileExists(ByVal filename As String) As Boolean
        Return IO.File.Exists(filename)
    End Function

    Public Shared Function GetIncrementedFilename(ByRef filename As String) As System.IO.StreamWriter
        Dim sw As System.IO.StreamWriter = Nothing
        Dim incr As Int32 = -1
        Const maxIncr As Int32 = 99
        While sw Is Nothing AndAlso incr < maxIncr
            incr += 1
            filename = IncrementFilename(filename, incr)
            Try
                sw = New StreamWriter(filename)
            Catch ioe As IOException
            End Try
        End While
        Return sw
    End Function

    Public Shared Function IncrementFilename(ByVal filename As String, ByVal increment As Int32) As String
        If increment > 0 Then
            Dim sb As New Text.StringBuilder
            sb.Append(System.IO.Path.GetDirectoryName(filename))
            sb.Append("\")
            sb.Append(System.IO.Path.GetFileNameWithoutExtension(filename))
            sb.Append("_")
            sb.Append(Format(increment, "0#"))
            sb.Append(System.IO.Path.GetExtension(filename))
            Return sb.ToString
        End If
        Return filename
    End Function

    Public Shared Function IncludeNamespaceWithTypename(ByVal name As String, ByVal [object] As Object) As String
        Dim fullName As String = name
        If name.IndexOf(".").Equals(-1) Then
            Dim sb As New Text.StringBuilder
            sb.Append([object].GetType.Namespace.ToString)
            sb.Append(".")
            sb.Append(name)
            fullName = sb.ToString
        End If
        Return fullName
    End Function

    Public Shared Sub ShellExecute(ByVal filename As String)
        System.Diagnostics.Process.Start(filename)
    End Sub
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
