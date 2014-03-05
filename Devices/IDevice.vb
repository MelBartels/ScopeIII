Public Interface IDevice
    Property IDevices() As ArrayList
    Property Properties() As ArrayList
    Property DeviceTemplateArrayIx() As Int32

    Function Build() As IDevice
    Function Build(ByVal name As String) As IDevice

    Function GetDeviceTemplate() As DeviceTemplate

    Function GetDevicesByName(ByVal name As String) As ArrayList
    Function GetDevicesByType(ByVal type As String) As ArrayList
    Function GetProperty(ByVal type As String) As IDevProp
    Function GetProperties(ByVal type As String) As System.Collections.Generic.List(Of IDevProp)
    Function GetValueObject(ByVal name As String) As ValueObject

    Function GetIOStatus() As String
    Function SetIOStatus(ByVal status As String) As String

    Function GetCmdSet() As ISFTFacade
    ReadOnly Property DeviceCmdsFacade() As DeviceCmdsFacade

    Function Clone() As Object
End Interface
