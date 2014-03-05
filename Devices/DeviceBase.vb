#Region "Imports"
Imports System.Collections.Generic
#End Region

Public MustInherit Class DeviceBase
    Implements IDevice, ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pIDevices As ArrayList
    Protected pProperties As ArrayList
    Protected pDeviceTemplateArrayIx As Int32
    Protected pDeviceCmdsFacade As DeviceCmdsFacade
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceBase = New DeviceBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pProperties = New ArrayList
        pDeviceCmdsFacade = DeviceCmdsFacade.GetInstance
    End Sub

    'Public Shared Function GetInstance() As DeviceBase
    '    Return New DeviceBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IDevices() As ArrayList Implements IDevice.IDevices
        Get
            Return pIDevices
        End Get
        Set(ByVal Value As ArrayList)
            pIDevices = Value
        End Set
    End Property

    Public Property Properties() As ArrayList Implements IDevice.Properties
        Get
            Return pProperties
        End Get
        Set(ByVal Value As ArrayList)
            pProperties = Value
        End Set
    End Property

    Public Property DeviceTemplateArrayIx() As Integer Implements IDevice.DeviceTemplateArrayIx
        Get
            Return pDeviceTemplateArrayIx
        End Get
        Set(ByVal Value As Integer)
            pDeviceTemplateArrayIx = Value
        End Set
    End Property

    Public MustOverride Overloads Function Build() As IDevice Implements IDevice.Build
    Public MustOverride Overloads Function Build(ByVal name As String) As IDevice Implements IDevice.Build

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' If DeviceTemplate missing, then create one before returning it.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	11/17/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDeviceTemplate() As DeviceTemplate Implements IDevice.GetDeviceTemplate
        Dim deviceTemplate As DeviceTemplate = myDeviceTemplate()
        If deviceTemplate IsNot Nothing Then
            Return deviceTemplate
        End If

        DeviceTemplateBuilder.GetInstance.Build(Me)
        Return myDeviceTemplate()
    End Function

    Public Function GetDevicesByName(ByVal name As String) As ArrayList Implements IDevice.GetDevicesByName
        Dim matches As New ArrayList
        For Each Device As IDevice In pIDevices
            GetDevicesByNameSubr(name, Device, matches)
        Next
        Return matches
    End Function

    Public Function GetDevicesByType(ByVal type As String) As ArrayList Implements IDevice.GetDevicesByType
        Dim matches As New ArrayList
        For Each Device As IDevice In pIDevices
            GetDevicesByTypeSubr(type, Device, matches)
        Next
        Return matches
    End Function

    Public Function GetProperty(ByVal type As String) As IDevProp Implements IDevice.GetProperty
        For Each prop As Object In pProperties
            If prop.GetType.Name.Equals(type) Then
                Return CType(prop, IDevProp)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetProperties(ByVal type As String) As List(Of IDevProp) Implements IDevice.GetProperties
        Dim properties As New List(Of IDevProp)
        For Each prop As Object In pProperties
            If prop.GetType.Name.Equals(type) Then
                properties.Add(CType(prop, IDevProp))
            End If
        Next
        If properties.Count > 0 Then
            Return properties
        End If
        Return Nothing
    End Function

    Public Function GetValueObject(ByVal name As String) As ValueObject Implements IDevice.GetValueObject
        ' get properties by type
        Dim IDevProps As System.Collections.Generic.List(Of IDevProp) = GetProperties(GetType(ValueObject).Name)
        ' get by name
        Dim ValueObject As IDevProp = IDevProps.Find(AddressOf New FuncDelegate(Of IDevProp, String) _
                                                                    (AddressOf valueObjectByName, name).CallDelegate)
        Return CType(ValueObject, ValueObject)
    End Function

    Public Function GetIOStatus() As String Implements IDevice.GetIOStatus
        Return CType(GetProperty(GetType(DevPropStatus).Name), DevPropStatus).GetStatus(GetType(IOStatus).Name)
    End Function

    Public Function SetIOStatus(ByVal status As String) As String Implements IDevice.SetIOStatus
        CType(GetProperty(GetType(DevPropStatus).Name), DevPropStatus).SetStatus(GetType(IOStatus).Name, status)
        GetDeviceTemplate.StatusObserver.Notify(CObj(status))
        Return status
    End Function

    Public ReadOnly Property DeviceCmdsFacade() As DeviceCmdsFacade Implements IDevice.DeviceCmdsFacade
        Get
            Return pDeviceCmdsFacade
        End Get
    End Property

    Public Function GetCmdSet() As ISFTFacade Implements IDevice.GetCmdSet
        Return DeviceCmdsFacade.CmdSet
    End Function

    Public MustOverride Function Clone() As Object Implements IDevice.Clone, System.ICloneable.Clone
#End Region

#Region "Private and Protected Methods"
    Protected Sub buildDevPropDeviceCmdSet(ByVal name As String)
        Dim DevPropDeviceCmdSet As DevPropDeviceCmdSet = DevPropDeviceCmdSet.GetInstance
        Dim updateDeviceCmdSetObserver As UpdateDeviceCmdSetObserver = updateDeviceCmdSetObserver.GetInstance
        updateDeviceCmdSetObserver.DeviceCmdsFacade = DeviceCmdsFacade
        DevPropDeviceCmdSet.ObservableImp.Attach(CType(updateDeviceCmdSetObserver, IObserver))
        Properties.Add(DevPropDeviceCmdSet)

        DevPropDeviceCmdSet.Name = name
    End Sub

    Protected Overloads Function Build(ByVal name As String, ByVal type As String, ByVal statuses As ArrayList) As Boolean
        pIDevices = New ArrayList
        Return buildProperties(name, type, statuses)
    End Function

    Private Function buildProperties(ByVal name As String, ByVal type As String, ByVal statuses As ArrayList) As Boolean
        Dim IDevProp As IDevProp

        IDevProp = DevPropName.GetInstance
        CType(IDevProp, DevPropName).Name = name
        Properties.Add(IDevProp)

        IDevProp = DevPropType.GetInstance
        CType(IDevProp, DevPropType).Type = type
        Properties.Add(IDevProp)

        IDevProp = DevPropStatus.GetInstance
        For Each statusTypeValuePair As StatusTypeValuePair In statuses
            CType(IDevProp, DevPropStatus).SetStatus(statusTypeValuePair.Type, statusTypeValuePair.Value)
        Next
        Properties.Add(IDevProp)

        IDevProp = DevPropIOType.GetInstance
        CType(IDevProp, DevPropIOType).IOType = IOType.SerialPort.Name
        Properties.Add(IDevProp)

        Return True
    End Function

    Private Sub getDevicesByTypeSubr(ByVal type As String, ByRef Device As IDevice, ByRef matches As ArrayList)
        If CObj(Device).GetType.Name.Equals(type) Then
            matches.Add(CType(Device, IDevice))
        End If
        If Device.IDevices IsNot Nothing Then
            For Each subDevice As IDevice In Device.IDevices
                getDevicesByTypeSubr(type, subDevice, matches)
            Next
        End If
    End Sub

    Private Sub getDevicesByNameSubr(ByVal name As String, ByRef Device As IDevice, ByRef matches As ArrayList)
        Dim prop As IDevProp = Device.GetProperty(GetType(DevPropName).Name)
        If CType(prop, DevPropName).Name.Equals(name) Then
            matches.Add(CType(Device, IDevice))
        End If
        If Device.IDevices IsNot Nothing Then
            For Each subDevice As IDevice In Device.IDevices
                getDevicesByNameSubr(name, subDevice, matches)
            Next
        End If
    End Sub

    Private Function valueObjectByName(ByVal IDevProp As IDevProp, ByVal name As String) As Boolean
        If TypeOf IDevProp Is ValueObject Then
            If CType(IDevProp, ValueObject).ValueName.Equals(name) Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function myDeviceTemplate() As DeviceTemplate
        Dim deviceTemplates As ArrayList = DeviceTemplateArray.GetInstance.DeviceTemplates
        If pDeviceTemplateArrayIx >= 0 AndAlso pDeviceTemplateArrayIx < deviceTemplates.Count Then
            Return CType(deviceTemplates.Item(pDeviceTemplateArrayIx), DeviceTemplate)
        End If
        Return Nothing
    End Function

    Protected Function createActiveStatus() As ArrayList
        Dim statuses As New ArrayList
        Dim statusTypeValuePair As StatusTypeValuePair = ScopeIII.Devices.StatusTypeValuePair.GetInstance
        statusTypeValuePair.Type = GetType(DeviceStatus).Name
        statusTypeValuePair.Value = DeviceStatus.Active.Description
        statuses.Add(statusTypeValuePair)
        Return statuses
    End Function

    Protected Function cloneSubr(ByRef clonedDevice As IDevice) As IDevice
        cloneProperties(clonedDevice)
        completeDeviceCmdsFacade(clonedDevice)
        cloneDevices(clonedDevice)
        Return clonedDevice
    End Function

    ' each property should handle custom cloning needs
    Private Sub cloneProperties(ByRef clonedDevice As IDevice)
        For Each IDevProp As IDevProp In Properties()
            clonedDevice.Properties.Add(IDevProp.Clone)
        Next
    End Sub

    Private Sub cloneDevices(ByRef clonedDevice As IDevice)
        ' not calling Build(...) so explicitly instantiate here
        clonedDevice.IDevices = New ArrayList
        For Each IDevice As IDevice In pIDevices
            clonedDevice.IDevices.Add(IDevice.Clone)
        Next
    End Sub

    Protected Sub completeDeviceCmdsFacade(ByRef clonedDevice As IDevice)
        Dim IDevProp As IDevProp = GetProperty(GetType(DevPropDeviceCmdSet).Name)
        If IDevProp IsNot Nothing Then
            Dim cmdProtocolName As String = CType(IDevProp, DevPropDeviceCmdSet).Name
            If Not String.IsNullOrEmpty(cmdProtocolName) Then
                Dim clonedDevPropDeviceCmdSet As IDevProp = clonedDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name)
                CType(clonedDevPropDeviceCmdSet, DevPropDeviceCmdSet).Name = cmdProtocolName
            End If
        End If
    End Sub
#End Region

End Class
