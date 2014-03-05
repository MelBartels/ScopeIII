Imports NUnit.Framework

<TestFixture()> Public Class TestAddDevProp

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestAddProperty()
        Assert.IsNotNull(ServoGain.GetInstance)
    End Sub

    <Test()> Public Sub TestAddPropGridAdapter()
        Assert.IsNotNull(ServoGainToPropGridAdapter.GetInstance)
    End Sub

    <Test()> Public Sub TestIDevPropAddedToIDevice()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.ServoMotor, ISFT))
        Assert.IsNotNull(IDevice.GetProperty(GetType(ServoGain).Name))
    End Sub

    <Test()> Public Sub TestConfig()
        Dim types() As Type = SettingsFacade.GetInstance.GetIncludedTypes
        Dim found As Boolean = False
        For Each type As Type In types
            If type.Equals(GetType(ServoGain)) Then
                found = True
                Exit For
            End If
        Next
        Assert.IsTrue(found)
    End Sub

    <Test()> Public Sub TestDevicePropContainer()
        ' prove that the IDevProp is in the property parameter collection
        Dim fake As DevicePropContainerFake = New DevicePropContainerFake(GetType(ServoGain).Name)
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.ServoMotor, ISFT))
        fake.Adapt(IDevice)
        Dim propContainer As PropContainer = fake.PropContainer
        Dim propParmCollection As PropContainer.PropParmCollection = propContainer.Properties
        Dim ix As Int32 = propParmCollection.IndexOf(GetType(ServoGain).Name)
        Assert.GreaterOrEqual(ix, 0)
        ' if no exception thrown, then IDevProp is handled in getValue()
        fake.GetValue()
        ' ditto for setValue()
        fake.SetValue()

        Assert.IsTrue(True)
    End Sub

    <Test(), ExpectedException(GetType(System.Exception))> Public Sub TestDevicePropContainerGetValue()
        ' prove that the IDevProp is in the property parameter collection
        Dim fake As DevicePropContainerFake = New DevicePropContainerFake("unknown type name")
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.ServoMotor, ISFT))
        fake.Adapt(IDevice)
        fake.GetValue()
    End Sub

    <Test(), ExpectedException(GetType(System.Exception))> Public Sub TestDevicePropContainerSetValue()
        ' prove that the IDevProp is in the property parameter collection
        Dim fake As DevicePropContainerFake = New DevicePropContainerFake("unknown type name")
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.ServoMotor, ISFT))
        fake.Adapt(IDevice)
        fake.SetValue()
    End Sub

    <Test()> Public Sub TestAddControllerSequence()
        Dim IDevPropName As String = GetType(JRKerrServoControl).Name
        ' property 
        Assert.IsNotNull(JRKerrServoControl.GetInstance)
        ' PropGridAdapter
        ' no explicit adapter: dealt w/ directly in DevPropContainer
        ' added to IDevice
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.JRKerrServoController, ISFT))
        Assert.IsNotNull(IDevice.GetProperty(IDevPropName))
        ' added to config
        Dim types() As Type = SettingsFacade.GetInstance.GetIncludedTypes
        Dim found As Boolean = False
        For Each type As Type In types
            If type.Equals(GetType(JRKerrServoControl)) Then
                found = True
                Exit For
            End If
        Next
        Assert.IsTrue(found)
        ' Test PropContainer: prove that the IDevProp is in the property parameter collection
        Dim fake As DevicePropContainerFake = New DevicePropContainerFake(IDevPropName)
        fake.Adapt(IDevice)
        Dim propParmCollection As PropContainer.PropParmCollection = fake.PropContainer.Properties
        Dim ix As Int32 = propParmCollection.IndexOf(IDevPropName)
        Assert.GreaterOrEqual(ix, 0)
        ' if no exception thrown, then IDevProp is handled in getValue()
        fake.GetValue()
        ' ditto for setValue()
        fake.SetValue()

        Assert.IsTrue(True)
    End Sub

    Class DevicePropContainerFake : Inherits DevPropContainer
        Dim e As PropParmEventArgs = PropParmEventArgs.GetInstance
        Public Sub New(ByVal typeName As String)
            e.Init(PropParm.GetInstance, typeName)
            e.Property.Name = typeName
        End Sub
        Public Overloads Sub GetValue()
            GetValue(Nothing, e)
        End Sub
        Public Overloads Sub SetValue()
            SetValue(Nothing, e)
        End Sub
    End Class

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

