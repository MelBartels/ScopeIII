#Region "Imports"
#End Region

Public Class DevicesPropContainer
    Inherits DevPropToPropGridAdapterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ClassDescAttr As String = "Devices Settings"
    Public Const DevicesDesc As String = "Devices."
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIDevices As ArrayList
    Private pIDevicePropGridAdapters As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevicesPropContainer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevicesPropContainer = New DevicesPropContainer
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        buildObjects()
    End Sub

    Public Shared Function GetInstance() As DevicesPropContainer
        Return New DevicesPropContainer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IDevices() As ArrayList
        Get
            Return pIDevices
        End Get
        Set(ByVal Value As ArrayList)
            pIDevices = Value
        End Set
    End Property

    Public Sub Add(ByRef IDevice As IDevice)
        IDevices.Add(IDevice)

        Dim devPropContainer As DevPropContainer = ScopeIII.Devices.DevPropContainer.GetInstance
        devPropContainer.Adapt(IDevice)
        pIDevicePropGridAdapters.Add(devPropContainer)
        Dim desc As String = IDevice.GetProperty(GetType(DevPropName).Name).Value

        initAndAddPropParm(desc, GetType(PropContainer), DevicesDesc, Nothing, Nothing, Nothing, GetType(PropContainerConverter))
    End Sub

    Public Sub Clear()
        IDevices.Clear()
        pIDevicePropGridAdapters.Clear()
        PropContainer.Properties.Clear()
    End Sub

    Public Overrides Function Clone() As Object
        Dim devicesPropContainer As DevicesPropContainer = ScopeIII.Devices.DevicesPropContainer.GetInstance
        For Each IDevice As IDevice In IDevices
            devicesPropContainer.Add(IDevice)
        Next
        Return devicesPropContainer
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub buildPropContainer()
        pPropContainer = Config.PropContainer.GetInstance
        AddHandler pPropContainer.GetValue, AddressOf getValue
        AddHandler pPropContainer.SetValue, AddressOf setValue
    End Sub

    Private Sub buildObjects()
        IDevices = New ArrayList
        buildPropContainer()
        pIDevicePropGridAdapters = New ArrayList
    End Sub

    Protected Overrides Function getValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        For Each devPropContainer As DevPropContainer In pIDevicePropGridAdapters
            Dim name As String = devPropContainer.GetIDevice.GetProperty(GetType(DevPropName).Name).Value
            If name.Equals(e.Property.Name) Then
                e.Value = devPropContainer.PropContainer
                Exit Function
            End If
        Next
    End Function

    Protected Overrides Function setValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        For Each devPropContainer As DevPropContainer In pIDevicePropGridAdapters
            Dim name As String = devPropContainer.GetIDevice.GetProperty(GetType(DevPropName).Name).Value
            If name.Equals(e.Property.Name) Then
                devPropContainer.PropContainer = CType(e.Value, PropContainer)
                Exit Function
            End If
        Next
    End Function
#End Region

End Class
