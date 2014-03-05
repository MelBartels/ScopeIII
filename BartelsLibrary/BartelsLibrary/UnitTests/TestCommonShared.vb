Imports NUnit.Framework

<TestFixture()> Public Class TestCommonShared

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestGetIncrementedFilename()
        Dim testFilename As String = "C:\TestCommonShared\testFilename.txt"
        Assert.AreEqual(testFilename, CommonShared.IncrementFilename(testFilename, 0))
        Assert.AreEqual("C:\TestCommonShared\testFilename_01.txt", CommonShared.IncrementFilename(testFilename, 1))
        Assert.AreEqual("C:\TestCommonShared\testFilename_02.txt", CommonShared.IncrementFilename(testFilename, 2))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetFullyQualifiedDirectory()
        Assert.AreEqual("C:\TestGetFullyQualifiedDirectory", CommonShared.GetFullyQualifiedDirectory("C:\TestGetFullyQualifiedDirectory\filename"))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestCreateDeleteDirectoryFileExists()
        Dim testFilename As String = "C:\TestCommonShared\testFilename.txt"
        deleteFileAndDir(testFilename)
        Assert.IsFalse(CommonShared.FileExists(testFilename))

        CommonShared.CreateDirectory(testFilename)
        Dim fs As IO.FileStream = System.IO.File.Create(testFilename)
        Assert.IsTrue(CommonShared.FileExists(testFilename))

        ' must dispose of file handle otherwise delete will fail
        fs.Dispose()
        deleteFileAndDir(testFilename)
        Assert.IsFalse(CommonShared.FileExists(testFilename))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeleteDirectoryWithFiles()
        Dim testDirectory As String = "C:\TestCommonShared\"
        Dim testFilename As String = "testFilename.txt"
        Dim fullPathFilename As String = testDirectory & testFilename

        CommonShared.DeleteDirectoryWithFiles(testDirectory)
        Assert.IsFalse(System.IO.Directory.Exists(testDirectory))

        CommonShared.CreateDirectory(testDirectory)
        Dim fs As System.IO.FileStream = System.IO.File.Create(fullPathFilename)
        fs.Dispose()
        Assert.IsTrue(CommonShared.FileExists(fullPathFilename))

        CommonShared.DeleteDirectoryWithFiles(testDirectory)
        Assert.IsFalse(System.IO.Directory.Exists(testDirectory))

        Assert.IsTrue(True)
    End Sub

    Private Sub deleteFileAndDir(ByVal filename As String)
        Try
            System.IO.File.Delete(filename)
            CommonShared.DeleteDirectory(filename)
        Catch ex As Exception
        End Try
    End Sub

    <Test()> Public Sub TestShellExecute()
        Dim filename As String = "shellexecute.txt"
        Debug.WriteLine("shell executing on file " & filename & " in " & CommonShared.GetFullyQualifiedDirectory(filename))
        CommonShared.ShellExecute(filename)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestType()
        ' won't work unless namespace is included
        Assert.IsNull(Type.GetType("FactoryTestClassA"))
        Assert.IsNotNull(Type.GetType("BartelsLibrary.FactoryTestClassA"))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
