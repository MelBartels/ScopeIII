Imports NUnit.Framework

<TestFixture()> Public Class TestIntToByteConversions

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestConversions()
        Dim b(3) As Byte

        ' test array
        Assert.AreEqual(4, b.Length)

        ' test conversion from bytes to int
        ' b arranged from least significant to most significant
        b(0) = 1
        b(1) = 4
        b(2) = 8
        b(3) = 12
        Dim testNum As Int32 = 1 + 4 * 256 + 8 * 256 * 256 + 12 * 256 * 256 * 256
        Assert.AreEqual(testNum, BitConverter.ToInt32(b, 0))

        ' test using a too large byte array
        Dim l(7) As Byte
        l(0) = 1
        l(1) = 4
        l(2) = 8
        l(3) = 12
        l(4) = 2
        l(5) = 3
        l(6) = 5
        l(7) = 7
        Assert.AreEqual(testNum, BitConverter.ToInt32(l, 0))

        ' test conversion from int to bytes
        Dim r() As Byte = BitConverter.GetBytes(testNum)
        Assert.AreEqual(1, r(0))
        Assert.AreEqual(4, r(1))
        Assert.AreEqual(8, r(2))
        Assert.AreEqual(12, r(3))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub CopyBytesIntoBytes()
        Dim bytes(7) As Byte
        Dim test1 As Int32 = 1234
        Dim test2 As Int32 = 9876

        Array.Copy(BitConverter.GetBytes(test1), 0, bytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(test2), 0, bytes, 4, 4)

        Assert.AreEqual(test1, BitConverter.ToInt32(bytes, 0))
        Assert.AreEqual(test2, BitConverter.ToInt32(bytes, 4))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

