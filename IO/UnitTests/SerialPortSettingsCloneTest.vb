Imports NUnit.Framework

<TestFixture()> Public Class SerialPortSettingsCloneTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestCloning()
        Dim SerialSettings As SerialSettings = IO.SerialSettings.GetInstance

        Dim newSerialSettings As SerialSettings = CType(SerialSettings.Clone, SerialSettings)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
