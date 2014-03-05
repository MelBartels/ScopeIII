Imports NUnit.Framework

<TestFixture()> Public Class TestDevices

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Public Sub TestDeviceClone()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.TestDevice, ISFT))
        Dim clonedDevice As IDevice = CType(IDevice.Clone, IDevice)
        Assert.IsNotNull(clonedDevice)

        ' compare clone against original
        Dim original As New Text.StringBuilder
        Dim StringHierarchicalAdapter As DeviceToHierarchicalAdapter = DeviceToHierarchicalAdapter.GetInstance
        StringHierarchicalAdapter.RegisterComponent(CObj(original))
        StringHierarchicalAdapter.Adapt(IDevice)

        Dim cloned As New Text.StringBuilder
        StringHierarchicalAdapter = DeviceToHierarchicalAdapter.GetInstance
        StringHierarchicalAdapter.RegisterComponent(CObj(cloned))
        StringHierarchicalAdapter.Adapt(clonedDevice)

        Assert.AreEqual(original.ToString.Length > 0, cloned.ToString.Length > 0)

        ' change clone and compare
        CType(clonedDevice.GetProperty(GetType(DevPropName).Name), DevPropName).Name = "xxxxxx"
        StringHierarchicalAdapter.Adapt(clonedDevice)
        Assert.AreEqual(original.ToString.Length > 0, cloned.ToString.Length > 0)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeviceCloneCmdsFacade()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.TestDevice, ISFT))
        Dim clonedDevice As IDevice = CType(IDevice.Clone, IDevice)

        Dim DeviceCmdAndReplyTemplateList As Generic.List(Of DeviceCmdAndReplyTemplate) = IDevice.DeviceCmdsFacade.DeviceCmdAndReplyTemplateList
        Dim clonedDeviceCmdAndReplyTemplateList As Generic.List(Of DeviceCmdAndReplyTemplate) = clonedDevice.DeviceCmdsFacade.DeviceCmdAndReplyTemplateList

        For ix As Int32 = 0 To DeviceCmdAndReplyTemplateList.Count - 1
            Dim cmd As String = DeviceCmdAndReplyTemplateList(ix).IDeviceCmd.CmdMsgParms.Cmd
            Dim clonedCmd As String = DeviceCmdAndReplyTemplateList(ix).IDeviceCmd.CmdMsgParms.Cmd
            Assert.AreEqual(cmd, clonedCmd)
        Next

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeviceClonePropertyObservers()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.TestDevice, ISFT))
        Dim clonedDevice As IDevice = CType(IDevice.Clone, IDevice)

        Dim IDevProp As IDevProp = IDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name)
        Dim clonedIDevProp As IDevProp = IDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name)

        Assert.AreEqual(1, CType(IDevProp, DevPropDeviceCmdSet).ObservableImp.Observers.Count)
        Assert.AreEqual(1, CType(clonedIDevProp, DevPropDeviceCmdSet).ObservableImp.Observers.Count)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestDeviceBuild()
        ' test default Build
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT))

        ' test if name was set
        Dim testReached As Boolean = False
        For Each prop As Object In IDevice.Properties
            If prop.GetType.Name.Equals(GetType(DevPropName).Name) Then
                Dim name As String = DeviceName.Encoder.Name
                Assert.IsTrue(CType(prop, DevPropName).Name.Equals(name))
                testReached = True
                Exit For
            End If
        Next
        Assert.IsTrue(testReached)

        ' test Build w/ Device name
        Dim testName As String = ScopeLibrary.Constants.TestEncoder
        IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT), testName)

        ' test if name was set
        testReached = False
        For Each prop As Object In IDevice.Properties
            If prop.GetType.Name.Equals(GetType(DevPropName).Name) Then
                Assert.IsTrue(CType(prop, DevPropName).Name.Equals(testName))
                testReached = True
                Exit For
            End If
        Next

        Assert.IsTrue(testReached)
    End Sub

    <Test()> Public Sub TestDeviceCmdSet()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        Assert.IsNotNull(IDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name))
        Assert.IsNotNull(IDevice.GetProperty(GetType(DevPropIOType).Name))

        ' extract DeviceCmdSet
        Dim IDevProp As IDevProp = IDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name)
        Dim DevPropDeviceCmdSet As DevPropDeviceCmdSet = CType(IDevProp, DevPropDeviceCmdSet)
        Dim myDeviceCmdSet As ISFT = DeviceCmdSet.ISFT.MatchString(DevPropDeviceCmdSet.Name)
        Assert.AreSame(DeviceCmdSet.TangentEncodersWithResetR, myDeviceCmdSet)
        Dim cmdProtocolName As String = CType(IDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name), DevPropDeviceCmdSet).Name
        Assert.AreEqual("TangentEncodersWithResetR", cmdProtocolName)
        Assert.AreSame(DeviceCmdSet.TangentEncodersWithResetR, myDeviceCmdSet.FirstItem.MatchString(cmdProtocolName))
        ' or...
        Assert.AreSame(DeviceCmdSet.TangentEncodersWithResetR, DeviceCmdSet.GetInstance.SetSelectedItem(cmdProtocolName).SelectedItem)

        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestDeviceFactory()
        Dim eDevices As IEnumerator = DeviceName.ISFT.Enumerator
        While eDevices.MoveNext
            Assert.IsNotNull(DeviceFactory.GetInstance.Build(CType(eDevices.Current, ISFT)))
        End While
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

