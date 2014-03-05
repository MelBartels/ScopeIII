Imports NUnit.Framework

<TestFixture()> Public Class TestUpdateDeviceCmdSetObserver

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestBuildDevPropDeviceCmdSet()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.TestDevice, ISFT))
        Assert.IsNotNull(IDevice.GetCmdSet)

        ' now test for device that has no command set
        IDevice = Encoder.GetInstance
        Assert.IsNull(IDevice.GetCmdSet)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

