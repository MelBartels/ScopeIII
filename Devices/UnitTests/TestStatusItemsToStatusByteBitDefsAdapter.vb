Imports NUnit.Framework

<TestFixture()> Public Class TestStatusItemsToStatusByteBitDefsAdapter

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestGetStatusItems()
        Dim sb As New Text.StringBuilder
        sb.Append(JRKerrServoStatusByteBitDefs.SEND_POS.Name)
        sb.Append(",")
        sb.Append(JRKerrServoStatusByteBitDefs.SEND_HOME.Name)
        sb.Append(",")
        sb.Append(JRKerrServoStatusByteBitDefs.SEND_NPOINTS.Name)
        Dim bits As String = sb.ToString

        Dim expectedByte As Byte = SEND_POS + SEND_HOME + SEND_NPOINTS
        Assert.AreEqual(expectedByte, StatusItemsToStatusByteBitDefsAdapter.ConvertToStatusItems(bits))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestConvertToString()
        Dim sb As New Text.StringBuilder
        sb.Append(JRKerrServoStatusByteBitDefs.SEND_POS.Name)
        sb.Append(",")
        sb.Append(JRKerrServoStatusByteBitDefs.SEND_HOME.Name)
        sb.Append(",")
        sb.Append(JRKerrServoStatusByteBitDefs.SEND_NPOINTS.Name)
        Dim bits As String = sb.ToString

        Dim expectedByte As Byte = SEND_POS + SEND_HOME + SEND_NPOINTS
        Assert.AreEqual(bits, StatusItemsToStatusByteBitDefsAdapter.ConvertToString(expectedByte))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

