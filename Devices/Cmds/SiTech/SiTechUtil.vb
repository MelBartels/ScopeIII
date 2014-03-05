#Region "Imports"
#End Region

Public Class SiTechUtil

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const PrimaryControllerPrimaryMotorPrefix As String = "X"
    Public Const PrimaryControllerSecondaryMotorPrefix As String = "Y"
    Public Const SecondaryControllerPrimaryMotorPrefix As String = "T"
    Public Const SecondaryControllerSecondaryMotorPrefix As String = "U"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Enum DeviceOrder
        PrimaryServoMotor = 0
        SecondaryServoMotor = 1
        PrimaryEncoder = 2
        SecondaryEncoder = 3
    End Enum
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SiTechUtil
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SiTechUtil = New SiTechUtil
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SiTechUtil
        Return New SiTechUtil
    End Function
#End Region

#Region "Shared Methods"
    Public Shared Function GetDevice(ByVal IDevice As IDevice, ByVal index As DeviceOrder) As IDevice
        Return CType(IDevice.IDevices(index), IDevice)
    End Function

    Public Shared Function GetPrimaryServoMotor(ByVal IDevice As IDevice) As IDevice
        Return GetDevice(IDevice, DeviceOrder.PrimaryServoMotor)
    End Function

    Public Shared Function GetSecondaryServoMotor(ByVal IDevice As IDevice) As IDevice
        Return GetDevice(IDevice, DeviceOrder.SecondaryServoMotor)
    End Function

    Public Shared Function GetPrimaryEncoder(ByVal IDevice As IDevice) As IDevice
        Return GetDevice(IDevice, DeviceOrder.PrimaryEncoder)
    End Function

    Public Shared Function GetSecondaryEncoder(ByVal IDevice As IDevice) As IDevice
        Return GetDevice(IDevice, DeviceOrder.SecondaryEncoder)
    End Function

    Public Shared Function GetPrimaryServoMotorEncoder(ByVal IDevice As IDevice) As IDevice
        Return CType(GetDevice(IDevice, DeviceOrder.PrimaryServoMotor).GetDevicesByName(SiTechController.MotorEncoderName).Item(0), IDevice)
    End Function

    Public Shared Function GetSecondaryServoMotorEncoder(ByVal IDevice As IDevice) As IDevice
        Return CType(GetDevice(IDevice, DeviceOrder.SecondaryServoMotor).GetDevicesByName(SiTechController.MotorEncoderName).Item(0), IDevice)
    End Function

    Public Shared Function GetDevPropName(ByVal IDevice As IDevice) As DevPropName
        Return CType(IDevice.GetProperty(GetType(DevPropName).Name), DevPropName)
    End Function

    Public Shared Function GetControllerSequence(ByVal IDevice As IDevice) As ControllerSequence
        Return CType(IDevice.GetProperty(GetType(ControllerSequence).Name), ControllerSequence)
    End Function

    Public Shared Sub BuildDeviceNames(ByVal IDevice As IDevice)
        Dim controllerSequence As ISFT = ControllerSequenceValues.ISFT.MatchString(GetControllerSequence(IDevice).Value)
        If controllerSequence Is ControllerSequenceValues.Primary Then
            BuildDeviceNamesPrimaryController(IDevice)
        Else
            BuildDeviceNamesSecondaryController(IDevice)
        End If
    End Sub

    Public Shared Sub BuildDeviceNamesPrimaryController(ByVal IDevice As IDevice)
        GetDevPropName(GetPrimaryServoMotor(IDevice)).Name = SiTechAxes.PriAxis.Name & DeviceName.ServoMotor.Name
        GetDevPropName(GetSecondaryServoMotor(IDevice)).Name = SiTechAxes.SecAxis.Name & DeviceName.ServoMotor.Name
        GetDevPropName(GetPrimaryEncoder(IDevice)).Name = SiTechAxes.PriAxis.Name & DeviceName.Encoder.Name
        GetDevPropName(GetSecondaryEncoder(IDevice)).Name = SiTechAxes.SecAxis.Name & DeviceName.Encoder.Name
    End Sub

    Public Shared Sub BuildDeviceNamesSecondaryController(ByVal IDevice As IDevice)
        GetDevPropName(GetPrimaryServoMotor(IDevice)).Name = SiTechAxes.TierAxis.Name & DeviceName.ServoMotor.Name
        GetDevPropName(GetSecondaryServoMotor(IDevice)).Name = SiTechAxes.AuxAxis.Name & DeviceName.ServoMotor.Name
        GetDevPropName(GetPrimaryEncoder(IDevice)).Name = SiTechAxes.TierAxis.Name & DeviceName.Encoder.Name
        GetDevPropName(GetSecondaryEncoder(IDevice)).Name = SiTechAxes.AuxAxis.Name & DeviceName.Encoder.Name
    End Sub
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
