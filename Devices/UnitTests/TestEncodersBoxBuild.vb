Imports NUnit.Framework

<TestFixture()> Public Class TestEncodersBoxBuild

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestEncodersBox()
        Dim eb As EncodersBox = CType(ScopeIII.Devices.DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT)), EncodersBox)
        DeviceTemplateBuilder.GetInstance.Build(CType(eb, IDevice))

        Assert.AreEqual(2, eb.IDevices.Count)
        Assert.IsNotNull(eb.GetProperty(GetType(DevPropDeviceCmdSet).Name))
        Assert.IsNotNull(eb.GetProperty(GetType(DevPropIOType).Name))
    End Sub

    <Test()> Public Sub TestBuildingOfEncodersBox()
        Dim eb As EncodersBox = ScopeIII.Devices.EncodersBox.GetInstance
        eb.Build(DeviceName.EncodersBox.Name)
        DeviceTemplateBuilder.GetInstance.Build(CType(eb, IDevice))

        eb.SetCountsPerRevolution(4000, 4001)
        Dim encoder As Encoder = CType(eb.GetDevicesByName(eb.PriAxisName).Item(0), Encoder)
        Dim value As EncoderValue = CType(encoder.GetProperty(GetType(EncoderValue).Name), EncoderValue)
        Assert.AreEqual((-2000).ToString, value.MinValue)
        Assert.AreEqual((1999).ToString, value.MaxValue)

        ' or alternative to above

        eb = ScopeIII.Devices.EncodersBox.GetInstance
        DeviceName.GetInstance()
        eb.Build(DeviceName.EncodersBox.Name)
        DeviceTemplateBuilder.GetInstance.Build(CType(eb, IDevice))

        eb.SetCountsPerRevolution(40000, 40001)
        encoder = CType(eb.GetDevicesByName(eb.PriAxisName).Item(0), Encoder)
        value = CType(encoder.GetProperty(GetType(EncoderValue).Name), EncoderValue)
        Assert.AreEqual((0).ToString, value.MinValue)
        Assert.AreEqual((39999).ToString, value.MaxValue)

        ' preferred alternative 

        eb = CType(DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT)), ScopeIII.Devices.EncodersBox)
        eb.SetCountsPerRevolution(20000, 20001)
        encoder = CType(eb.GetDevicesByName(eb.PriAxisName).Item(0), Encoder)
        value = CType(encoder.GetProperty(GetType(EncoderValue).Name), EncoderValue)
        Assert.AreEqual((-10000).ToString, value.MinValue)
        Assert.AreEqual((9999).ToString, value.MaxValue)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestSetCountsPerRevolution()
        Dim eb As EncodersBox = CType(DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT)), ScopeIII.Devices.EncodersBox)
        Dim encoderPri As Encoder = CType(eb.GetDevicesByName(eb.PriAxisName).Item(0), Encoder)
        Dim encoderSec As Encoder = CType(eb.GetDevicesByName(eb.SecAxisName).Item(0), Encoder)
        Dim valuePri As EncoderValue = CType(encoderPri.GetProperty(GetType(EncoderValue).Name), EncoderValue)
        Dim valueSec As EncoderValue = CType(encoderSec.GetProperty(GetType(EncoderValue).Name), EncoderValue)

        eb.SetCountsPerRevolution(32767, 32768)

        Assert.IsTrue(eMath.RInt(valuePri.MinValue) < 0)
        Assert.IsTrue(eMath.RInt(valueSec.MinValue) = 0)

        eb.SetCountsPerRevolution(20000, 20001)
        Assert.AreEqual((-10000).ToString, valuePri.MinValue)
        Assert.AreEqual((9999).ToString, valuePri.MaxValue)
        Assert.AreEqual((-10000).ToString, valueSec.MinValue)
        Assert.AreEqual((10000).ToString, valueSec.MaxValue)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

