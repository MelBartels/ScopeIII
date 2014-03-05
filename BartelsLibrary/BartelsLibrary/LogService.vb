#Region "Imports"
Imports System.IO
#End Region

Public Class LogService

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
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As LogService
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As LogService = New LogService
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As LogService
    '    Return New LogService
    'End Function
#End Region

#Region "Shared Methods"
    Public Shared Function LogMsg(ByVal msg As String) As Boolean
        Dim fs As FileStream
        Dim sw As StreamWriter

        CommonShared.CreateDirectory(FullPathFilename)

        If Not File.Exists(FullPathFilename) Then
            fs = New FileStream(FullPathFilename, FileMode.Create)
            sw = New StreamWriter(fs)
            sw.Close()
            fs.Close()
        End If

        fs = New FileStream(FullPathFilename, FileMode.Append)
        sw = New StreamWriter(fs)
        sw.Write("Logging message at: ")
        sw.WriteLine(DateTime.Now.ToString())
        sw.WriteLine(msg)
        sw.WriteLine("=============================================================")
        sw.Close()
        fs.Close()
    End Function

    Public Shared Function FullPathFilename() As String
        'Return System.Windows.Forms.Application.StartupPath & "\" & Settings.GetInstance.ExceptionFilename
        Return BartelsLibrary.Constants.LogSubdir & Settings.GetInstance.ExceptionFilename
    End Function
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
