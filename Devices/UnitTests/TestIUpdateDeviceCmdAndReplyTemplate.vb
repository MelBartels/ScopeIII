Imports NUnit.Framework

<TestFixture()> Public Class TestIUpdateDeviceCmdAndReplyTemplate

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        DevicesDependencyInjector.GetInstance.UseUnitTestContainer = True
    End Sub

    <Test()> Public Sub TestConstruction()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))
        Dim moduleAddress As ValueObject = IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name)
        Assert.GreaterOrEqual(moduleAddress.ObservableImp.Observers.Count, 1)
        Dim [object] As Object = moduleAddress.ObservableImp.Observers.Item(0)
        Assert.IsInstanceOfType(GetType(UpdateDeviceCmdsObserver), [object])
        Dim endoTestUpdateDeviceCmdsObserver As EndoTestUpdateDeviceCmdsObserver = CType([object], EndoTestUpdateDeviceCmdsObserver)
        Assert.IsInstanceOfType(GetType(JRKerrUpdateDeviceCmdAndReplyTemplate), endoTestUpdateDeviceCmdsObserver.IUpdateDeviceCmdAndReplyTemplate)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
        DevicesDependencyInjector.GetInstance.UseUnitTestContainer = False
    End Sub

End Class

