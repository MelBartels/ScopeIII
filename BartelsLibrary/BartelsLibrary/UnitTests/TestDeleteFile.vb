Imports System.IO
Imports NUnit.Framework

<TestFixture()> Public Class TestDeleteFile

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestDelete()
        Dim filename As String = "TestDeleteFile.txt"
        Dim writeContents As String = "hello from TestDelete"
        Delete(filename)

        Debug.WriteLine("filename is " & Path.GetFullPath(filename))
        My.Computer.FileSystem.WriteAllText(filename, writeContents, True)
        Dim readContents As String = My.Computer.FileSystem.ReadAllText(filename)

        Assert.AreEqual(writeContents, readContents)

        Assert.IsTrue(True)
    End Sub

    Public Sub Delete(ByVal filename As String)
        Debug.WriteLine("attempting to delete " & Path.GetFullPath(filename))
        If File.Exists(filename) Then
            Debug.Write("deleting existing file...")
            File.Delete(filename)
            Threading.Thread.Sleep(500)
            If File.Exists(filename) Then
                Debug.WriteLine("***warning! file NOT deleted")
                Assert.Fail("Could not delete prior existing file in Common.TestDeleteFile.Delete(String).")
            Else
                Debug.WriteLine("file deleted")
            End If
        Else
            Debug.WriteLine("preexisting file not found")
        End If

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

