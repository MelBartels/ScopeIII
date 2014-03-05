Imports NUnit.Framework

<TestFixture()> Public Class TestDeviceAdapters

    Private pShowFormTimeMilliseconds As Int32 = 30000

    Private pEncoderValue As EncoderValue
    Private pEncoderValueToPropGridAdapter As EncoderValueToPropGridAdapter
    Private pFrmPropGridEncoderValue As FrmPropertyGrid

    Private pDevPropEncoderValueToPropGridAdapter2 As EncoderValueToPropGridAdapter
    Private pFrmPropGridEncoderValue2 As FrmPropertyGrid

    Private pDevPropContainer As DevPropContainer
    Private pFrmPropGridEncoder As FrmPropertyGrid

    Private pDevPropContainerOriginal As DevPropContainer
    Private pFrmPropGridDeviceOriginal As FrmPropertyGrid
    Private pDevPropContainerCloned As DevPropContainer
    Private pFrmPropGridDeviceCloned As FrmPropertyGrid

    Private pDevicesPropContainer As DevicesPropContainer
    Private pFrmPropGridDevices As FrmPropertyGrid
    Private pDevicesPropContainerCloned As DevicesPropContainer
    Private pFrmPropGridDevicesCloned As FrmPropertyGrid

    Private pIDeviceTV As IDevice
    Private pFrmTreeView As FrmTreeView

    Private pIDeviceTB As IDevice
    Private pFrmTextBox As FrmTextBox

    Private pIDevicePG As IDevice
    Private pFrmPropertyGrid As FrmPropertyGrid

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <Test()> Sub TestDeviceToHierarchicalAdapter()
        Dim DeviceToHierarchicalAdapter As DeviceToHierarchicalAdapter = ScopeIII.Devices.DeviceToHierarchicalAdapter.GetInstance
        DeviceToHierarchicalAdapter.RegisterComponent(New Windows.Forms.TreeView)
        Assert.IsTrue(True)

        ' "xxx" is not a valid component
        Assert.IsFalse(DeviceToHierarchicalAdapter.RegisterComponent("xxx"))

        Dim sb As New Text.StringBuilder
        Dim stringHierarchicalAdapter As StringHierarchicalAdapter = BartelsLibrary.StringHierarchicalAdapter.GetInstance
        stringHierarchicalAdapter.RegisterComponent(CObj(New Text.StringBuilder))
        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestPropGridEncoderValue()
        pEncoderValue = EncoderValue.GetInstance
        pEncoderValueToPropGridAdapter = EncoderValueToPropGridAdapter.GetInstance
        pEncoderValueToPropGridAdapter.Adapt(pEncoderValue)

        Dim frmThread As New Threading.Thread(AddressOf showPropGridEncoderValue)
        frmThread.Start()
        Dim frmTimer As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimer.Elapsed, AddressOf killPropGridEncoderValue
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private Sub showPropGridEncoderValue()
        pFrmPropGridEncoderValue = New FrmPropertyGrid
        pFrmPropGridEncoderValue.Text = "showPropGridEncoderValue"
        pFrmPropGridEncoderValue.PropertyGrid.SelectedObject = pEncoderValueToPropGridAdapter.PropContainer
        pFrmPropGridEncoderValue.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropGridEncoderValue(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropGridEncoderValue.InvokeRequired Then
            pFrmPropGridEncoderValue.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropGridEncoderValue.Close))
        Else
            pFrmPropGridEncoderValue.Close()
        End If
    End Sub

    <Test()> Sub TestPropGridEncoderValue2()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        pEncoderValue = CType(CType(CType(IDevice, EncodersBox).GetDevicesByName(CType(IDevice, EncodersBox).PriAxisName).Item(0), Encoder).GetProperty(GetType(EncoderValue).Name), EncoderValue)
        pDevPropEncoderValueToPropGridAdapter2 = EncoderValueToPropGridAdapter.GetInstance
        pDevPropEncoderValueToPropGridAdapter2.Adapt(pEncoderValue)

        Dim frmThread As New Threading.Thread(AddressOf showPropGridEncoderValue2)
        frmThread.Start()
        Dim frmTimer As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimer.Elapsed, AddressOf killPropGridEncoderValue2
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private Sub showPropGridEncoderValue2()
        pFrmPropGridEncoderValue2 = New FrmPropertyGrid
        pFrmPropGridEncoderValue2.Text = "showPropGridEncoderValue2"
        pFrmPropGridEncoderValue2.PropertyGrid.SelectedObject = pDevPropEncoderValueToPropGridAdapter2.PropContainer
        pFrmPropGridEncoderValue2.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropGridEncoderValue2(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropGridEncoderValue2.InvokeRequired Then
            pFrmPropGridEncoderValue2.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropGridEncoderValue2.Close))
        Else
            pFrmPropGridEncoderValue2.Close()
        End If
    End Sub

    <Test()> Sub TestPropGridEncoder()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.Encoder, ISFT), ScopeLibrary.Constants.TestEncoder)
        pDevPropContainer = DevPropContainer.GetInstance
        pDevPropContainer.Adapt(IDevice)

        Dim frmThread As New Threading.Thread(AddressOf showPropGridEncoder)
        frmThread.Start()
        Dim frmTimer As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimer.Elapsed, AddressOf killPropGridEncoder
        frmTimer.AutoReset = False
        frmTimer.Start()
    End Sub

    Private Sub showPropGridEncoder()
        pFrmPropGridEncoder = New FrmPropertyGrid
        pFrmPropGridEncoder.Text = "showPropGridEncoder"
        pFrmPropGridEncoder.PropertyGrid.SelectedObject = pDevPropContainer.PropContainer
        pFrmPropGridEncoder.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropGridEncoder(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropGridEncoder.InvokeRequired Then
            pFrmPropGridEncoder.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropGridEncoder.Close))
        Else
            pFrmPropGridEncoder.Close()
        End If
    End Sub

    <Test()> Sub TestPropGridDeviceWithManualCloningTest()
        ' build a device
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        DeviceObservers.GetInstance.AddTestObserversToEncodersBox(CType(IDevice, EncodersBox))

        ' original container and form
        pDevPropContainerOriginal = DevPropContainer.GetInstance
        pDevPropContainerOriginal.Adapt(IDevice)

        Dim frmThreadPG As New Threading.Thread(AddressOf showPropertyGridDeviceOriginal)
        frmThreadPG.Start()

        Dim frmTimerPG As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimerPG.Elapsed, AddressOf killPropertyGridDeviceOriginal
        frmTimerPG.AutoReset = False
        frmTimerPG.Start()

        ' clone it and vary properties manually in the forms
        pDevPropContainerCloned = CType(pDevPropContainerOriginal.Clone, DevPropContainer)
        Assert.AreEqual(pDevPropContainerOriginal.PropContainer.Properties.Count, pDevPropContainerCloned.PropContainer.Properties.Count)

        Dim frmThreadCloned As New Threading.Thread(AddressOf showPropertyGridDeviceCloned)
        frmThreadCloned.Start()

        Dim frmTimerCloned As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimerCloned.Elapsed, AddressOf killPropertyGridDeviceCloned
        frmTimerCloned.AutoReset = False
        frmTimerCloned.Start()

        Assert.IsTrue(True)
    End Sub

    Private Sub showPropertyGridDeviceOriginal()
        pFrmPropGridDeviceOriginal = New FrmPropertyGrid
        pFrmPropGridDeviceOriginal.Text = "showPropertyGridDeviceOriginal"
        pFrmPropGridDeviceOriginal.PropertyGrid.SelectedObject = pDevPropContainerOriginal.PropContainer
        pFrmPropGridDeviceOriginal.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropertyGridDeviceOriginal(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropGridDeviceOriginal.InvokeRequired Then
            pFrmPropGridDeviceOriginal.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropGridDeviceOriginal.Close))
        Else
            pFrmPropGridDeviceOriginal.Close()
        End If
    End Sub

    Private Sub showPropertyGridDeviceCloned()
        pFrmPropGridDeviceCloned = New FrmPropertyGrid
        pFrmPropGridDeviceCloned.Text = "showPropertyGridDeviceCloned"
        pFrmPropGridDeviceCloned.PropertyGrid.SelectedObject = pDevPropContainerCloned.PropContainer
        pFrmPropGridDeviceCloned.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropertyGridDeviceCloned(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropGridDeviceCloned.InvokeRequired Then
            pFrmPropGridDeviceCloned.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropGridDeviceCloned.Close))
        Else
            pFrmPropGridDeviceCloned.Close()
        End If
    End Sub

    <Test()> Sub TestPropGridDevicesWithManualCloningTest()
        pDevicesPropContainer = CType(DevicePropContainers.GetInstance, DevicePropContainers).BuildDevicesSettingsPropContainer

        Dim frmThread As New Threading.Thread(AddressOf showPropGridDevices)
        frmThread.Start()
        Dim frmTimer As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimer.Elapsed, AddressOf killPropGridDevices
        frmTimer.AutoReset = False
        frmTimer.Start()

        pDevicesPropContainerCloned = CType(pDevicesPropContainer.Clone, DevicesPropContainer)
        Assert.AreEqual(pDevicesPropContainer.IDevices.Count, pDevicesPropContainerCloned.IDevices.Count)
        Assert.AreEqual(pDevicesPropContainer.PropContainer.Properties.Count, pDevicesPropContainerCloned.PropContainer.Properties.Count)

        Dim frmThreadCloned As New Threading.Thread(AddressOf showPropGridDevicesCloned)
        frmThreadCloned.Start()
        Dim frmTimerCloned As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimerCloned.Elapsed, AddressOf killPropGridDevicesCloned
        frmTimerCloned.AutoReset = False
        frmTimerCloned.Start()
    End Sub

    Private Sub showPropGridDevices()
        pFrmPropGridDevices = New FrmPropertyGrid
        pFrmPropGridDevices.Text = "showPropGridDevices"
        pFrmPropGridDevices.PropertyGrid.SelectedObject = pDevicesPropContainer.PropContainer
        pFrmPropGridDevices.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropGridDevices(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropGridDevices.InvokeRequired Then
            pFrmPropGridDevices.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropGridDevices.Close))
        Else
            pFrmPropGridDevices.Close()
        End If
    End Sub

    Private Sub showPropGridDevicesCloned()
        pFrmPropGridDevicesCloned = New FrmPropertyGrid
        pFrmPropGridDevicesCloned.Text = "showPropGridDevicesCloned"
        pFrmPropGridDevicesCloned.PropertyGrid.SelectedObject = pDevicesPropContainerCloned.PropContainer
        pFrmPropGridDevicesCloned.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropGridDevicesCloned(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropGridDevicesCloned.InvokeRequired Then
            pFrmPropGridDevicesCloned.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropGridDevicesCloned.Close))
        Else
            pFrmPropGridDevicesCloned.Close()
        End If
    End Sub

    <Test()> Sub TestIDeviceAdapter()
        Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(DeviceName.EncodersBox, ISFT))
        DeviceObservers.GetInstance.AddTestObserversToEncodersBox(CType(IDevice, EncodersBox))

        ' StringBuilder
        Dim sb As New Text.StringBuilder
        Dim StringHierarchicalAdapter As DeviceToHierarchicalAdapter = DeviceToHierarchicalAdapter.GetInstance
        StringHierarchicalAdapter.RegisterComponent(CObj(sb))
        StringHierarchicalAdapter.Adapt(IDevice)
        Debug.WriteLine(sb.ToString)

        ' TreeView

        pIDeviceTV = IDevice
        Dim frmThreadTV As New Threading.Thread(AddressOf showTreeViewForm)
        frmThreadTV.Start()

        Dim frmTimerTV As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimerTV.Elapsed, AddressOf killTreeViewForm
        frmTimerTV.AutoReset = False
        frmTimerTV.Start()

        ' TextBox

        pIDeviceTB = IDevice
        Dim frmThreadTB As New Threading.Thread(AddressOf showTextBoxForm)
        frmThreadTB.Start()

        Dim frmTimerTB As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimerTB.Elapsed, AddressOf killTextBoxForm
        frmTimerTB.AutoReset = False
        frmTimerTB.Start()

        ' PropertyGrid

        pIDevicePG = IDevice
        Dim frmThreadPG As New Threading.Thread(AddressOf showPropertyGridForm)
        frmThreadPG.Start()

        Dim frmTimerPG As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimerPG.Elapsed, AddressOf killPropertyGridForm
        frmTimerPG.AutoReset = False
        frmTimerPG.Start()

        Assert.IsTrue(True)
    End Sub

    Private Sub showTreeViewForm()
        pFrmTreeView = New FrmTreeView
        Dim DeviceToHierarchicalAdapter As DeviceToHierarchicalAdapter = ScopeIII.Devices.DeviceToHierarchicalAdapter.GetInstance
        DeviceToHierarchicalAdapter.RegisterComponent(CObj(pFrmTreeView.TreeView))
        DeviceToHierarchicalAdapter.Adapt(pIDeviceTV)
        pFrmTreeView.TreeView.ExpandAll()
        pFrmTreeView.ShowDialog()

        Assert.IsTrue(True)
    End Sub

    Private Sub showTextBoxForm()
        pFrmTextBox = New FrmTextBox
        Dim DeviceToHierarchicalAdapter As DeviceToHierarchicalAdapter = ScopeIII.Devices.DeviceToHierarchicalAdapter.GetInstance
        DeviceToHierarchicalAdapter.RegisterComponent(CObj(pFrmTextBox.txBxDisplay))
        DeviceToHierarchicalAdapter.Adapt(pIDeviceTB)
        pFrmTextBox.ShowDialog()

        Assert.IsTrue(True)
    End Sub

    Private Sub showPropertyGridForm()
        pFrmPropertyGrid = New FrmPropertyGrid
        pFrmPropertyGrid.Text = "showPropertyGridForm"
        Dim devPropContainer As DevPropContainer = ScopeIII.Devices.DevPropContainer.GetInstance
        devPropContainer.Adapt(pIDevicePG)
        pFrmPropertyGrid.PropertyGrid.SelectedObject = devPropContainer.PropContainer
        pFrmPropertyGrid.ShowDialog()

        Assert.IsTrue(True)
    End Sub

    Private Sub killTreeViewForm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmTreeView.InvokeRequired Then
            pFrmTreeView.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmTreeView.Close))
        Else
            pFrmTreeView.Close()
        End If
    End Sub

    Private Sub killTextBoxForm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmTextBox.InvokeRequired Then
            pFrmTextBox.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmTextBox.Close))
        Else
            pFrmTextBox.Close()
        End If
    End Sub

    Private Sub killPropertyGridForm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropertyGrid.InvokeRequired Then
            pFrmPropertyGrid.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropertyGrid.Close))
        Else
            pFrmPropertyGrid.Close()
        End If
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

