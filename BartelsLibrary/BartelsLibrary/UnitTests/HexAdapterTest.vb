Imports NUnit.Framework

<TestFixture()> Public Class HexAdapterTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestHexConversion()
        Dim hexAdapter As HexAdapter = hexAdapter.GetInstance

        Dim msg As String = "R+20000" & vbTab & "+20000" & vbCr
        Dim result As String = hexAdapter.ConvertToHex(msg)
        Assert.AreEqual("x52(R) x2B(+) x32(2) x30(0) x30(0) x30(0) x30(0) x9 x2B(+) x32(2) x30(0) x30(0) x30(0) x30(0) xD ", result)

        result = hexAdapter.ConvertToHex(msg, False)
        Assert.AreEqual("x52 x2B x32 x30 x30 x30 x30 x9 x2B x32 x30 x30 x30 x30 xD ", result)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
