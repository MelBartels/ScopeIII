#Region "Imports"
#End Region

Public Class JRKerrServoController
    Inherits DeviceBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const MotorEncoderName As String = "Motor Encoder"
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

    'Public Shared Function GetInstance() As JRKerrServoController
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrServoController = New JRKerrServoController
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrServoController
        Return New JRKerrServoController
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function Build() As IDevice
        Return Build(DeviceName.JRKerrServoController.Name)
    End Function

    Public Overloads Overrides Function Build(ByVal name As String) As IDevice
        ' DeviceBase.Build
        Build(name, GetType(JRKerrServoController).Name, createActiveStatus)

        Dim servoMotor As IDevice = Devices.ServoMotor.GetInstance
        servoMotor.Build(DeviceName.ServoMotor.Name)
        IDevices.Add(servoMotor)
        Dim servoGain As IDevProp = servoMotor.GetProperty(GetType(ServoGain).Name)
        JRKerrServoGainDefaults.GetInstance.SetToDefaults(CType(servoGain, ServoGain))

        ' add unique properties
        Dim moduleAddress As ValueObject = ValueObject.GetInstance
        moduleAddress.Build(ValueObjectNames.ModuleAddress.Name, CStr(0), CStr(0), CStr(255), UOM.Scalar.Description)
        Properties.Add(moduleAddress)

        Properties.Add(JRKerrServoControl.GetInstance)
        Properties.Add(JRKerrServoStatus.GetInstance)

        buildDevPropDeviceCmdSet(DeviceCmdSet.JRKerrServoController.Name)

        ' update observers
        DeviceCmdsFacade.UpdateDeviceCmdsObserver.Build(Me, JRKerrUpdateDeviceCmdAndReplyTemplate.GetInstance)
        moduleAddress.ObservableImp.Attach(CType(DeviceCmdsFacade.UpdateDeviceCmdsObserver, IObserver))
        JRKerrUtil.GetJRKerrServoStatus(Me).ObservableImp.Attach(CType(DeviceCmdsFacade.UpdateDeviceCmdsObserver, IObserver))

        Return Me
    End Function

    Public Overrides Function Clone() As Object
        Return cloneSubr(JRKerrServoController.GetInstance)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
