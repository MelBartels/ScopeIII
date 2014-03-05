Imports NUnit.Framework

<TestFixture()> Public Class TestIOPresenterDevice

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestBuildShutdown()
        Dim IIOPresenter As IIOPresenter = IOPresenterDevice.GetInstance
        ' create convenient ref
        Dim iopd As IOPresenterDevice = CType(IIOPresenter, IOPresenterDevice)

        iopd.DeviceToIOBridge = DeviceToIOBridge.GetInstance
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        iopd.DeviceToIOBridge.IDevice = IDevice

        Dim ioType As ISFT = BartelsLibrary.IOType.TCPserver
        ' demonstrate that default IOType is something other than what we've set ioType to
        Assert.IsFalse(ioType.Name.Equals(IDevice.GetProperty(GetType(DevPropIOType).Name).Value))

        ' 1st way using DeviceToIOBridge:
        'iopd.DeviceToIOBridge.IOType = ioType
        'iopd.DeviceToIOBridge.BuildIIO()
        ' 2nd way using IIOPresenter:
        IIOPresenter.BuildIO(ioType)

        Assert.IsNotNull(IIOPresenter.IIO)
        ' verify that IDevice's IOType was updated
        Assert.IsTrue(ioType.Name.Equals(IDevice.GetProperty(GetType(DevPropIOType).Name).Value))

        IIOPresenter.IIO.Open()
        Assert.IsTrue(IIOPresenter.IIO.isOpened)

        IIOPresenter.ShutdownIO()
        Assert.IsFalse(IIOPresenter.IIO.isOpened)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
