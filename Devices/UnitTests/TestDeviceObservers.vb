Imports NUnit.Framework
Imports System.Collections.Generic


<TestFixture()> Public Class TestDeviceObservers

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestDeviceObservers()
        Dim te As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        DeviceObservers.GetInstance.AddTestObserversToEncodersBox(CType(te, EncodersBox))

        Dim obs As List(Of IObserver) = DeviceObservers.GetInstance.GetObservers(te)
        For Each observer As IObserver In obs
            Debug.WriteLine(CType(observer, Object).GetType.Name)
        Next
        Assert.AreEqual(3, obs.Count)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestAllObservers()
        Dim te As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        Dim testObs As IObserver = TestIObserver.GetInstance
        DeviceObservers.GetInstance.AddObserverToDevice(te, testObs)

        CType(te, EncodersBox).SetValues(CStr(1000), CStr(2000))
        Assert.AreEqual(SensorStatus.ValidRead.Description, CType(testObs, TestIObserver).Msg)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestAddObserversWithIDToDevice()
        Dim te As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        Dim testObs As IObserver = TestIObserver.GetInstance
        DeviceObservers.GetInstance.AddObserversWithIDToDevice(te, "Test", testObs)

        CType(te, EncodersBox).SetValues(CStr(1000), CStr(2000))
        Assert.IsTrue(CType(testObs, TestIObserver).Msg.EndsWith(SensorStatus.ValidRead.Description))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeviceStatuses()
        Dim te As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))

        Debug.WriteLine("IOStatus=" & te.GetIOStatus)
        displayAllDevicesStatuses(te)
        Assert.AreEqual(DeviceStatus.Active.Name, CType(CType(te.GetProperty(GetType(DevPropStatus).Name), DevPropStatus).Statuses(0), StatusTypeValuePair).Value)

        Assert.IsTrue(True)
    End Sub

    Private Sub displayAllDevicesStatuses(ByRef IDevice As IDevice)
        displayDeviceStatuses(IDevice)
        For Each subDevice As IDevice In IDevice.IDevices
            displayDeviceStatuses(subDevice)
        Next
    End Sub

    Private Sub displayDeviceStatuses(ByRef IDevice As IDevice)
        Dim sb As New Text.StringBuilder
        sb.Append("Device ")
        sb.Append(IDevice.GetProperty(GetType(DevPropName).Name).Value)
        sb.Append(" statuses: ")
        For Each stvp As StatusTypeValuePair In CType(IDevice.GetProperty(GetType(DevPropStatus).Name), DevPropStatus).Statuses
            sb.Append(stvp.Type)
            sb.Append("=")
            sb.Append(stvp.Value)
            sb.Append(", ")
        Next
        Debug.WriteLine(sb.ToString)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

