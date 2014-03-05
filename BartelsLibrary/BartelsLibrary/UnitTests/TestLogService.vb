Imports NUnit.Framework
Imports System.Collections.ObjectModel

<TestFixture()> Public Class TestLogService

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestLogMsg()
        Dim dir As String = CommonShared.GetFullyQualifiedDirectory(LogService.FullPathFilename)
        Debug.WriteLine("TestLogMsg() directory is " & dir)
        CommonShared.DeleteDirectoryWithFiles(dir)
        ' verify that directory has been deleted
        Assert.IsFalse(IO.Directory.Exists(dir))

        ' now finally test the message logging
        LogService.LogMsg("TestLogMsg")
        Assert.IsTrue(IO.File.Exists(LogService.FullPathFilename))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
