Imports NUnit.Framework

<TestFixture()> Public Class LoggingObserverTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestLogging()
        ' test instantiate
        Dim lo As LoggingObserver = LoggingObserver.GetInstance
        Assert.IsNotNull(lo)

        ' write random test msg
        Randomize()
        Dim testMsg As String = Rnd.ToString
        lo.Open()
        Assert.IsTrue(lo.IsOpen)
        lo.ProcessMsg(CObj(testMsg))
        lo.Close()
        Assert.IsFalse(lo.IsOpen)
        ' reopen file and see if test msg written
        Try
            Debug.WriteLine("opening for reading " & System.IO.Path.GetFullPath(lo.Filename))
            Dim reader As IO.StreamReader = New IO.StreamReader(lo.Filename)
            Dim results As String = reader.ReadLine
            reader.Close()
            Assert.IsTrue(results.Equals(testMsg))
        Catch ex As Exception
            Assert.Fail("couldn't open reader")
        End Try

        ' try using a filename that uses a randomly created subdir
        lo.Filename = IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) _
            & "\" _
            & testMsg _
            & "\" _
            & testMsg
        lo.Open()
        lo.ProcessMsg(CObj(testMsg))
        lo.Close()
        ' reopen file and see if test msg written
        Try
            Debug.WriteLine("opening for reading " & System.IO.Path.GetFullPath(lo.Filename))
            Dim reader As IO.StreamReader = New IO.StreamReader(lo.Filename)
            Dim results As String = reader.ReadLine
            reader.Close()
            Assert.IsTrue(results.Equals(testMsg))
        Catch ex As Exception
            Assert.Fail("couldn't open reader")
        End Try

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
