#Region "Imports"
Imports System.ComponentModel
Imports System.Collections.Generic
#End Region

Public Class DevPropContainer
    Inherits DevPropToPropGridAdapterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ClassDescAttr As String = "Device Settings"
    Public Const NothingValue As String = "(none)"

    Public Const DeviceName As String = "Device Name"
    Public Const DeviceNameDesc As String = "Device name.  Enter a new name or select from a list."
    Public Const DeviceType As String = "Device Type"
    Public Const DeviceTypeDesc As String = "Device type.  Must select from a list."
    Public Const DeviceStatus As String = "Device Status"
    Public Const DeviceStatusDesc As String = "Device status.  Must select from a list."
    Public Const DeviceObservers As String = "Device Observers"
    Public Const DeviceObserversDesc As String = "Device Observers."
    Public Const DeviceCmdProtocol As String = "Command Protocol"
    Public Const DeviceCmdProtocolDesc As String = "The command protocol that the device will respond to.  Must select from a list."
    Public Const DeviceCmds As String = "Device Commands"
    Public Const DeviceCmdsDesc As String = "The commands that the device will respond to.  Must select from a list."
    Public Const DeviceIO As String = "Communications Channel"
    Public Const DeviceIODesc As String = "Communications channel for the device.  Must select from a list."
    Public Const DeviceRotation As String = "Device Rotation"
    Public Const DeviceRotationDesc As String = "Direction of rotation.  Must select from a list."
    Public Const SubDevice As String = "Sub Device: "
    Public Const SubDeviceDesc As String = "Subordinate or child device."
    Public Const ValueObjectLit As String = "Value "
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIDevice As IDevice

    Private pEncoderValue As EncoderValue
    Private pEncoderValueToPropGridAdapter As EncoderValueToPropGridAdapter

    Private pServoGain As ServoGain
    Private pServoGainToPropGridAdapter As ServoGainToPropGridAdapter

    Private pJRKerrServoControl As JRKerrServoControl
    Private pJRKerrServoControlToPropGridAdapter As JRKerrServoControlToPropGridAdapter

    Private pJRKerrServoStatus As JRKerrServoStatus
    Private pJRKerrServoStatusToPropGridAdapter As JRKerrServoStatusToPropGridAdapter

    Private pValueObjectToPropGridAdapters As List(Of ValueObjectToPropGridAdapter)
    Private pChildPropGridAdapters As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevPropContainer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevPropContainer = New DevPropContainer
    'End Class
#End Region

#Region "Constructors"
    ' must be public for .NET to instantiate
    Protected Sub New()
        pChildPropGridAdapters = New ArrayList
    End Sub

    Public Shared Function GetInstance() As DevPropContainer
        Return New DevPropContainer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function GetIDevice() As IDevice
        Return pIDevice
    End Function

    Public Overloads Sub Adapt(ByRef IDevice As IDevice)
        pIDevice = IDevice

        pPropContainer = Config.PropContainer.GetInstance

        AddHandler pPropContainer.GetValue, AddressOf getValue
        AddHandler pPropContainer.SetValue, AddressOf setValue

        If pIDevice.GetProperty(GetType(DevPropName).Name) IsNot Nothing Then
            initAndAddPropParm(DeviceName, GetType(System.String), ClassDescAttr, DeviceNameDesc, Nothing, GetType(ListViewTypeEditorDeviceName), GetType(StringConverter))
        End If

        If pIDevice.GetProperty(GetType(DevPropType).Name) IsNot Nothing Then
            initAndAddPropParm(DeviceType, GetType(System.String), ClassDescAttr, DeviceTypeDesc, Nothing, GetType(ListViewTypeEditorDevices), GetType(StringConverter))
        End If

        If pIDevice.GetProperty(GetType(DevPropStatus).Name) IsNot Nothing Then
            initAndAddPropParm(DeviceStatus, GetType(System.String), ClassDescAttr, DeviceStatusDesc, Nothing, GetType(ListViewTypeEditorDeviceStatus), GetType(StringConverter))
        End If

        ' readonly so always include it
        pPropParm = initAndAddPropParm(DeviceObservers, GetType(System.String), ClassDescAttr, DeviceObserversDesc, Nothing, Nothing, Nothing)
        pPropParm.Attributes = New Attribute() {ReadOnlyAttribute.Yes}

        If pIDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name) IsNot Nothing Then
            initAndAddPropParm(DeviceCmdProtocol, GetType(System.String), ClassDescAttr, DeviceCmdProtocolDesc, Nothing, GetType(ListViewTypeEditorDeviceCmdSet), GetType(StringConverter))

            pPropParm = initAndAddPropParm(DeviceCmds, GetType(System.String), ClassDescAttr, DeviceCmdsDesc, Nothing, Nothing, Nothing)
            pPropParm.Attributes = New Attribute() {ReadOnlyAttribute.Yes}
        End If

        If pIDevice.GetProperty(GetType(DevPropIOType).Name) IsNot Nothing Then
            initAndAddPropParm(DeviceIO, GetType(System.String), ClassDescAttr, DeviceIODesc, Nothing, GetType(ListViewTypeEditorIOType), GetType(StringConverter))
        End If

        If pIDevice.GetProperty(GetType(DevPropRotation).Name) IsNot Nothing Then
            initAndAddPropParm(DeviceRotation, GetType(System.String), ClassDescAttr, DeviceRotationDesc, Nothing, GetType(ListViewTypeEditorRotation), GetType(StringConverter))
        End If

        ' set ValueObjects and their prop grid adapters
        Dim valueObjects As List(Of IDevProp) = pIDevice.GetProperties(GetType(ValueObject).Name)
        If valueObjects IsNot Nothing Then
            pValueObjectToPropGridAdapters = New List(Of ValueObjectToPropGridAdapter)
            For Each IDevProp As IDevProp In valueObjects
                Dim valueObject As ValueObject = CType(IDevProp, ValueObject)
                Dim valueObjectToPropGridAdapter As ValueObjectToPropGridAdapter = valueObjectToPropGridAdapter.GetInstance
                valueObjectToPropGridAdapter.Adapt(valueObject)
                pValueObjectToPropGridAdapters.Add(valueObjectToPropGridAdapter)

                initAndAddPropParm(ValueObjectLit & valueObject.Properties, GetType(PropContainer), Nothing, Nothing, Nothing, Nothing, GetType(PropContainerConverter))
            Next
        End If

        ' set EncoderValue and its prop grid adapter
        If pIDevice.GetProperty(GetType(EncoderValue).Name) IsNot Nothing Then
            pEncoderValue = CType(pIDevice.GetProperty(GetType(EncoderValue).Name), EncoderValue)
            pEncoderValueToPropGridAdapter = EncoderValueToPropGridAdapter.GetInstance
            pEncoderValueToPropGridAdapter.Adapt(pEncoderValue)

            initAndAddPropParm(GetType(EncoderValue).Name, GetType(PropContainer), Nothing, Nothing, Nothing, Nothing, GetType(PropContainerConverter))
        End If

        'set ServoGain and its prop grid adapter
        If pIDevice.GetProperty(GetType(ServoGain).Name) IsNot Nothing Then
            pServoGain = CType(pIDevice.GetProperty(GetType(ServoGain).Name), ServoGain)
            pServoGainToPropGridAdapter = ServoGainToPropGridAdapter.GetInstance
            pServoGainToPropGridAdapter.Adapt(pServoGain)

            initAndAddPropParm(GetType(ServoGain).Name, GetType(PropContainer), Nothing, Nothing, Nothing, Nothing, GetType(PropContainerConverter))
        End If

        'set JRKerrServoControl and its prop grid adapter
        If pIDevice.GetProperty(GetType(JRKerrServoControl).Name) IsNot Nothing Then
            pJRKerrServoControl = CType(pIDevice.GetProperty(GetType(JRKerrServoControl).Name), JRKerrServoControl)
            pJRKerrServoControlToPropGridAdapter = JRKerrServoControlToPropGridAdapter.GetInstance
            pJRKerrServoControlToPropGridAdapter.Adapt(pJRKerrServoControl)

            initAndAddPropParm(GetType(JRKerrServoControl).Name, GetType(PropContainer), Nothing, Nothing, Nothing, Nothing, GetType(PropContainerConverter))
        End If

        'set JRKerrServoStatus and its prop grid adapter
        If pIDevice.GetProperty(GetType(JRKerrServoStatus).Name) IsNot Nothing Then
            pJRKerrServoStatus = CType(pIDevice.GetProperty(GetType(JRKerrServoStatus).Name), JRKerrServoStatus)
            pJRKerrServoStatusToPropGridAdapter = JRKerrServoStatusToPropGridAdapter.GetInstance
            pJRKerrServoStatusToPropGridAdapter.Adapt(pJRKerrServoStatus)

            initAndAddPropParm(GetType(JRKerrServoStatus).Name, GetType(PropContainer), Nothing, Nothing, Nothing, Nothing, GetType(PropContainerConverter))
        End If

        If pIDevice.IDevices IsNot Nothing Then
            For Each childDevice As IDevice In pIDevice.IDevices
                Dim devPropContainer As DevPropContainer = ScopeIII.Devices.DevPropContainer.GetInstance
                devPropContainer.Adapt(childDevice)
                pChildPropGridAdapters.Add(devPropContainer)
                Dim desc As String = SubDevice & childDevice.GetProperty(GetType(DevPropName).Name).Value
                initAndAddPropParm(desc, GetType(PropContainer), SubDeviceDesc, Nothing, Nothing, Nothing, GetType(PropContainerConverter))
            Next
        End If
    End Sub

    Public Overrides Function Clone() As Object
        Dim devPropContainer As DevPropContainer = ScopeIII.Devices.DevPropContainer.GetInstance
        If pIDevice IsNot Nothing Then
            devPropContainer.Adapt(CType(pIDevice.Clone, IDevice))
        End If
        Return devPropContainer
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Function getValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case DeviceName
                e.Value = pIDevice.GetProperty(GetType(DevPropName).Name).Value
            Case DeviceType
                e.Value = pIDevice.GetProperty(GetType(DevPropType).Name).Value
            Case DeviceStatus
                e.Value = pIDevice.GetProperty(GetType(DevPropStatus).Name).Value
            Case DeviceObservers
                Dim sb As New Text.StringBuilder
                Dim deviceTemplate As DeviceTemplate = pIDevice.GetDeviceTemplate
                If deviceTemplate IsNot Nothing Then
                    Dim observableImp As ObservableImp = deviceTemplate.DeviceObserver
                    If observableImp IsNot Nothing AndAlso observableImp.Observers IsNot Nothing Then
                        For Each observer As IObserver In observableImp.Observers
                            sb.Append(" Device observer: ")
                            sb.Append(CObj(observer).GetType.Name)
                        Next
                    End If
                    observableImp = deviceTemplate.StatusObserver
                    If observableImp IsNot Nothing AndAlso observableImp.Observers IsNot Nothing Then
                        For Each observer As IObserver In observableImp.Observers
                            sb.Append(" Status observer: ")
                            sb.Append(CObj(observer).GetType.Name)
                        Next
                    End If
                End If
                e.Value = sb.ToString
            Case DeviceCmdProtocol
                Dim IDevProp As IDevProp = pIDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name)
                If IDevProp Is Nothing Then
                    e.Value = DeviceCmdSet.None.Description
                Else
                    e.Value = IDevProp.Value
                End If
            Case DeviceCmds
                Dim ISFTFacade As ISFTFacade = pIDevice.GetCmdSet
                If ISFTFacade Is Nothing Then
                    e.Value = NothingValue
                Else
                    e.Value = pIDevice.GetCmdSet.FirstItem.DisplayAll
                End If
            Case DeviceIO
                Dim IDevProp As IDevProp = pIDevice.GetProperty(GetType(DevPropIOType).Name)
                If IDevProp Is Nothing Then
                    e.Value = IOType.NotSet.Description
                Else
                    e.Value = IDevProp.Value
                End If
            Case DeviceRotation
                Dim IDevProp As IDevProp = pIDevice.GetProperty(GetType(DevPropRotation).Name)
                If IDevProp Is Nothing Then
                    e.Value = Rotation.No.Description
                Else
                    e.Value = IDevProp.Value
                End If
            Case GetType(EncoderValue).Name
                e.Value = pEncoderValueToPropGridAdapter.PropContainer
            Case GetType(ServoGain).Name
                e.Value = pServoGainToPropGridAdapter.PropContainer
            Case GetType(JRKerrServoControl).Name
                e.Value = pJRKerrServoControlToPropGridAdapter.PropContainer
            Case GetType(JRKerrServoStatus).Name
                e.Value = pJRKerrServoStatusToPropGridAdapter.PropContainer
            Case Else
                If e.Property.Name.IndexOf(SubDevice) > -1 Then
                    Dim selectedName As String = e.Property.Name.Replace(SubDevice, String.Empty)
                    For Each devPropContainer As DevPropContainer In pChildPropGridAdapters
                        Dim childName As String = devPropContainer.GetIDevice.GetProperty(GetType(DevPropName).Name).Value()
                        If selectedName.Equals(childName) Then
                            e.Value = devPropContainer.PropContainer()
                            Exit Function
                        End If
                    Next
                End If

                For Each valueObjectToPropGridAdapter As ValueObjectToPropGridAdapter In pValueObjectToPropGridAdapters
                    If e.Property.Name.IndexOf(valueObjectToPropGridAdapter.ValueObject.ValueName) > -1 Then
                        e.Value = valueObjectToPropGridAdapter.PropContainer
                        Exit Function
                    End If
                Next

                Throw New Exception("unhandled Property.Name of " & e.Property.Name & " in DevPropContainer.getValue")
        End Select
        Return True
    End Function

    Protected Overrides Function setValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case DeviceName
                CType(pIDevice.GetProperty(GetType(DevPropName).Name), DevPropName).Name = CStr(e.Value)
            Case DeviceType
                ' 2 statements to preserve pIDevice if an exception is thrown and factory returns nothing (factory handles exception);
                Dim newDevice As IDevice = DeviceFactory.GetInstance.Build(ScopeIII.Devices.DeviceName.ISFT.MatchString(CStr(e.Value)))
                If newDevice IsNot Nothing Then
                    pIDevice = newDevice
                    DeviceTemplateBuilder.GetInstance.Build(pIDevice)
                End If
            Case DeviceStatus
                If e.Value IsNot Nothing AndAlso ScopeIII.Devices.DeviceStatus.ISFT.MatchString(CStr(e.Value)) IsNot Nothing Then
                    CType(pIDevice.GetProperty(GetType(DevPropStatus).Name), DevPropStatus).SetStatus(GetType(DeviceStatus).Name, CStr(e.Value))
                End If
            Case DeviceCmdProtocol
                If e.Value IsNot Nothing AndAlso ScopeIII.Devices.DeviceCmdSet.ISFT.MatchString(CStr(e.Value)) IsNot Nothing Then
                    Dim IDevProp As IDevProp = pIDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name)
                    If IDevProp Is Nothing Then
                        Dim DevPropDeviceCmdSet As DevPropDeviceCmdSet = ScopeIII.Devices.DevPropDeviceCmdSet.GetInstance
                        pIDevice.Properties.Add(DevPropDeviceCmdSet)
                    End If
                    CType(pIDevice.GetProperty(GetType(DevPropDeviceCmdSet).Name), DevPropDeviceCmdSet).Name = CStr(e.Value)
                End If
            Case DeviceIO
                If e.Value IsNot Nothing AndAlso IOType.ISFT.MatchString(CStr(e.Value)) IsNot Nothing Then
                    CType(pIDevice.GetProperty(GetType(DevPropIOType).Name), DevPropIOType).IOType = CStr(e.Value)
                End If
            Case DeviceRotation
                If e.Value IsNot Nothing AndAlso Rotation.ISFT.MatchString(CStr(e.Value)) IsNot Nothing Then
                    CType(pIDevice.GetProperty(GetType(DevPropRotation).Name), DevPropRotation).Rotation = CStr(e.Value)
                End If
            Case DeviceObservers
                ' readonly
            Case DeviceCmds
                ' readonly
            Case GetType(EncoderValue).Name
                ' readonly
            Case GetType(ServoGain).Name
                ' readonly
            Case GetType(JRKerrServoControl).Name
                ' readonly
            Case GetType(JRKerrServoStatus).Name
                ' readonly
            Case Else
                For Each valueObjectToPropGridAdapter As ValueObjectToPropGridAdapter In pValueObjectToPropGridAdapters
                    If e.Property.Name.IndexOf(valueObjectToPropGridAdapter.ValueObject.ValueName) > -1 Then
                        ' readonly
                        Exit Function
                    End If
                Next

                Throw New Exception("unhandled Property.Name of " & e.Property.Name & " in DevPropContainer.setValue")
        End Select
        Return True
    End Function
#End Region

End Class
