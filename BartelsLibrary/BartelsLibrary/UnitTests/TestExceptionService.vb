Imports NUnit.Framework

<TestFixture()> Public Class TestExceptionService

    Private pShowFormTimeMilliseconds As Int32 = 30000

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestException()
        Dim dir As String = CommonShared.GetFullyQualifiedDirectory(LogService.FullPathFilename)
        Debug.WriteLine("TestException() directory is " & dir)
        CommonShared.DeleteDirectoryWithFiles(dir)
        ' verify that directory has been deleted
        Assert.IsFalse(IO.Directory.Exists(dir))

        ' won't display in Resharper unit test sessions
        Dim thread1 As New Threading.Thread(AddressOf testMessageLogging)
        thread1.Start()

        Dim thread2 As New Threading.Thread(AddressOf testThrowingException)
        thread2.Start()

        Dim thread3 As New Threading.Thread(AddressOf testThrowingExceptionWithCustomMessage)
        thread3.Start()

        Assert.IsTrue(True)
    End Sub

    Private Sub testMessageLogging()
        ExceptionService.Notify("Test exception")
        Assert.IsTrue(IO.File.Exists(LogService.FullPathFilename))
    End Sub

    Private Sub testThrowingException()
        Try
            Throw New Exception("Test throwing exception")
        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try
    End Sub

    Private Sub testThrowingExceptionWithCustomMessage()
        Try
            Throw New Exception("Test throwing exception")
        Catch ex As Exception
            ExceptionService.Notify(ex, "custom message")
        End Try
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
