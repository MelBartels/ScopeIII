Imports NUnit.Framework

<TestFixture()> Public Class TestEncoder

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestBytesToString()
        Dim numBytes(7) As Byte
        Dim test1 As Int32 = 1234    ' 0-0-4-210
        Dim test2 As Int32 = 9876    ' 0-0-38-148
        Array.Copy(BitConverter.GetBytes(test1), 0, numBytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(test2), 0, numBytes, 4, 4)

        Dim numString As String = Encoder.BytesToString(numBytes)
        Assert.AreEqual(8, numString.Length)

        Dim textBytes() As Byte = New Byte() {Asc("h"), Asc("e"), Asc("l"), Asc("l"), Asc("o")}
        Dim textString As String = Encoder.BytesToString(textBytes)
        Assert.AreEqual("hello", textString)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestStringToBytes()
        Dim testString As String = "hello"
        Dim expectedTextBytes() As Byte = New Byte() {Asc("h"), Asc("e"), Asc("l"), Asc("l"), Asc("o")}

        Dim textBytes() As Byte = Encoder.StringtoBytes(testString)
        Assert.AreEqual(expectedTextBytes.Length, textBytes.Length)
        For ix As Int32 = 0 To 4
            Assert.AreEqual(expectedTextBytes(ix), textBytes(ix))
        Next

        ' represents two int32s, values 1234 and 9876 (see above test)
        Dim expectedNumBytes() As Byte = New Byte() {210, 4, 0, 0, 148, 38, 0, 0}
        Dim numString As String = Encoder.BytesToString(expectedNumBytes)
        Assert.AreEqual(8, numString.Length)
        Dim numBytes() As Byte = Encoder.StringtoBytes(numString)
        Assert.AreEqual(8, numBytes.Length)
        For ix As Int32 = 0 To numBytes.Length - 1
            Assert.AreEqual(expectedNumBytes, numBytes)
        Next

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub BufferToString()
        Dim testBytes() As Byte = New Byte() {128, 128, Asc("h"), Asc("e"), Asc("l"), Asc("l"), Asc("o"), 128, 128}
        Assert.AreEqual("hello", Encoder.BytesToString(testBytes, 2, 5))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
