Imports NUnit.Framework
Imports System.Collections.Generic

<TestFixture()> Public Class TestTreeViewDeviceAdapterForAllDevices

    Private pShowFormTimeMilliseconds As Int32 = 30000
    Private frmTreeViews As List(Of FrmTreeView)

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
        frmTreeViews = New List(Of FrmTreeView)
    End Sub

    <Test()> Sub TestShowDevices()
        Dim eDevices As IEnumerator = DeviceName.ISFT.Enumerator
        While eDevices.MoveNext
            Dim IDevice As IDevice = DeviceFactory.GetInstance.Build(CType(eDevices.Current, ISFT))
            Dim frmThreadTV As New Threading.Thread(AddressOf showTreeViewForm)
            frmThreadTV.Start(IDevice)
        End While

        Dim frmTimerTV As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimerTV.Elapsed, AddressOf killTreeViewForms
        frmTimerTV.AutoReset = False
        frmTimerTV.Start()

        Assert.IsTrue(True)
    End Sub

    Private Sub showTreeViewForm(ByVal [object] As Object)
        Dim frmTreeView As New FrmTreeView
        frmTreeViews.Add(frmTreeView)
        Dim DeviceToHierarchicalAdapter As DeviceToHierarchicalAdapter = ScopeIII.Devices.DeviceToHierarchicalAdapter.GetInstance
        DeviceToHierarchicalAdapter.RegisterComponent(CObj(frmTreeView.TreeView))
        DeviceToHierarchicalAdapter.Adapt(CType([object], IDevice))
        frmTreeView.TreeView.ExpandAll()
        frmTreeView.ShowDialog()

        Assert.IsTrue(True)
    End Sub

    Private Sub killTreeViewForms(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        frmTreeViews.ForEach(New Action(Of FrmTreeView)(AddressOf killTreeViewForm))
    End Sub

    Private Sub killTreeViewForm(ByVal frmTreeView As FrmTreeView)
        If frmTreeView.InvokeRequired Then
            frmTreeView.Invoke(New Windows.Forms.MethodInvoker(AddressOf frmTreeView.Close))
        Else
            frmTreeView.Close()
        End If
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

