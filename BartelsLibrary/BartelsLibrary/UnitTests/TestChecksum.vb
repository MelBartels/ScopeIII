Imports NUnit.Framework

<TestFixture()> Public Class TestChecksum

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub Test()
        Dim testBytes() As Byte = {1, 2, 3}
        Assert.AreEqual(6, Checksum.Calc(testBytes, 0, 3))
        Assert.AreEqual(3, Checksum.Calc(testBytes, 0, 2))
        Assert.AreEqual(5, Checksum.Calc(testBytes, 1, 2))

        ' test for 255 which causes overflow in .Net if added as a byte
        Dim testBytes2() As Byte = {1, 2, 3, 255, 0}
        Assert.AreEqual(5, Checksum.Calc(testBytes2, 0, 5))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
