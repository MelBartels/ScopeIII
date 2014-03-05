Imports NUnit.Framework
Imports System.IO.Ports

<TestFixture()> Public Class SerialPortTests

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestStopBits()
        Dim serialPort As SerialPort = New SerialPort

        ' setting StopBits to StopBits.None or StopBits.OnePointFive throws an exception

        Try
            Dim newStopBits As String = [Enum].GetNames(GetType(StopBits))(0)
            serialPort.StopBits = CType([Enum].Parse(GetType(StopBits), newStopBits), StopBits)
            Assert.Fail("this message should not appear")
        Catch ex As Exception
            Assert.IsTrue(True, "assigning stopbits to 'None' by enum parse of string 'None' failed")
        End Try

        Try
            serialPort.StopBits = StopBits.None
            Assert.AreEqual(StopBits.None, serialPort.StopBits)
            Assert.Fail("this message should not appear")
        Catch ex As Exception
            Assert.IsTrue(True, "assigning stopbits to 'None' by the '=' operator failed")
        End Try

        Try
            Dim newStopBits As String = [Enum].GetNames(GetType(StopBits))(1)
            serialPort.StopBits = CType([Enum].Parse(GetType(StopBits), newStopBits), StopBits)
        Catch ex As Exception
            Assert.Fail("assigning stopbits to 'One' by enum parse of string 'One' failed")
        End Try

        Try
            serialPort.StopBits = StopBits.One
            Assert.AreEqual(StopBits.One, serialPort.StopBits)
        Catch ex As Exception
            Assert.Fail("assigning stopbits to 'One' by the '=' operator failed")
        End Try



        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
