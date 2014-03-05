#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class DeviceObservers

#Region "Inner Classes"
    Private Class TestIOStatusObserver : Implements IObserver
        Public Success As Boolean
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Dim msg As String = CStr([object])
            If msg.Equals(IOStatus.ValidResponse.Name) Then
                Success = True
            Else
                Success = False
            End If
        End Function
    End Class

    Private Class TestSensorStatusObserver : Implements IObserver
        Public Success As Boolean
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Dim msg As String = CStr([object])
            If msg.Equals(SensorStatus.ValidRead.Name) Then
                Success = True
            Else
                Success = False
            End If
        End Function
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceObservers
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceObservers = New DeviceObservers
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceObservers
        Return New DeviceObservers
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub AddTestObserversToEncodersBox(ByRef te As EncodersBox)
        Dim statusObserver As IObserver = New TestIOStatusObserver
        te.GetDeviceTemplate.StatusObserver.Attach(statusObserver)

        Dim priValueObserver As IObserver = New TestSensorStatusObserver
        Dim encoder As IDevice = CType(te.GetDevicesByName(te.PriAxisName).Item(0), IDevice)
        encoder.GetDeviceTemplate.StatusObserver.Attach(priValueObserver)

        Dim secValueObserver As IObserver = New TestSensorStatusObserver
        encoder = CType(te.GetDevicesByName(te.SecAxisName).Item(0), IDevice)
        encoder.GetDeviceTemplate.StatusObserver.Attach(secValueObserver)
    End Sub

    Public Function GetObservers(ByRef IDevice As IDevice) As List(Of IObserver)
        Dim observers As New List(Of IObserver)
        addObservers(IDevice, observers)
        Return observers
    End Function

    Public Sub AddObserverToDevice(ByRef IDevice As IDevice, ByRef IObserver As IObserver)
        IDevice.GetDeviceTemplate.DeviceObserver.Attach(IObserver)
        IDevice.GetDeviceTemplate.StatusObserver.Attach(IObserver)

        For Each subDevice As IDevice In IDevice.IDevices
            AddObserverToDevice(subDevice, IObserver)
        Next

        ' attach per specific devices
        If CType(IDevice, Object).GetType.Name.Equals(GetType(Encoder).Name) Then
            Dim encoderValue As EncoderValue = CType(IDevice.GetProperty(GetType(EncoderValue).Name), EncoderValue)
            encoderValue.ObservableImp.Attach(IObserver)
        End If
    End Sub

    Public Sub AddObserversWithIDToDevice(ByRef IDevice As IDevice, ByVal observingID As String, ByRef IObserver As IObserver)
        Dim deviceName As String = CType(IDevice.GetProperty(GetType(DevPropName).Name), DevPropName).Name

        Dim sb As New Text.StringBuilder
        sb.Append(deviceName)
        sb.Append(" device")
        addObserverWithID(IDevice.GetDeviceTemplate.DeviceObserver, observingID, sb.ToString, IObserver)

        sb = New Text.StringBuilder
        sb.Append(deviceName)
        sb.Append(" status")
        addObserverWithID(IDevice.GetDeviceTemplate.StatusObserver, observingID, sb.ToString, IObserver)

        ' attach per specific devices
        If CType(IDevice, Object).GetType.Name.Equals(GetType(Encoder).Name) Then
            sb = New Text.StringBuilder
            sb.Append(deviceName)
            sb.Append(" encoder value")
            Dim encoderValue As EncoderValue = CType(IDevice.GetProperty(GetType(EncoderValue).Name), EncoderValue)
            addObserverWithID(encoderValue.ObservableImp, observingID, sb.ToString, IObserver)

        End If

        For Each subDevice As IDevice In IDevice.IDevices
            AddObserversWithIDToDevice(subDevice, observingID, IObserver)
        Next
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Sub addObservers(ByRef IDevice As IDevice, ByRef observers As List(Of IObserver))
        For Each observer As IObserver In IDevice.GetDeviceTemplate.DeviceObserver.Observers
            observers.Add(observer)
        Next
        For Each observer As IObserver In IDevice.GetDeviceTemplate.StatusObserver.Observers
            observers.Add(observer)
        Next
        For Each subDevice As IDevice In IDevice.IDevices
            addObservers(subDevice, observers)
        Next
    End Sub

    ' attach to observableImp, build observerWithID with ID and IObserver
    Private Sub addObserverWithID(ByRef observableImp As ObservableImp, ByVal observingID As String, ByVal observedID As String, ByRef IObserver As IObserver)
        Dim observerWithID As ObserverWithID = observerWithID.GetInstance.Build(observingID, observedID, IObserver)
        observableImp.Attach(CType(observerWithID, IObserver))
    End Sub
#End Region

End Class
