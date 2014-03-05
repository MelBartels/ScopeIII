#Region "Imports"
#End Region

Public Class ServoMotor
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

    'Public Shared Function GetInstance() As ServoMotor
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ServoMotor = New ServoMotor
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ServoMotor
        Return New ServoMotor
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function Build() As IDevice
        Return Build(DeviceName.ServoMotor.Name)
    End Function

    Public Overloads Overrides Function Build(ByVal name As String) As IDevice
        ' DeviceBase.Build
        Build(name, GetType(ServoMotor).Name, createActiveStatus)

        Dim accelValue As ValueObject = ValueObject.GetInstance
        accelValue.Build(ValueObjectNames.Acceleration.Name, CStr(1000), CStr(0), CStr(3900), UOM.TicksPerSecPerSec.Description)
        Properties.Add(accelValue)

        Dim velocityValue As ValueObject = ValueObject.GetInstance
        velocityValue.Build(ValueObjectNames.Velocity.Name, CStr(0), CStr(0), CStr(Int32.MaxValue), UOM.TicksPerSec.Description)
        Properties.Add(velocityValue)

        Dim IDevProp As IDevProp = DevPropRotation.GetInstance
        CType(IDevProp, DevPropRotation).Rotation = Rotation.No.Name
        Properties.Add(IDevProp)

        Properties.Add(ServoGain.GetInstance)

        Dim IDevice As IDevice = Encoder.GetInstance
        IDevice.Build(MotorEncoderName)
        IDevices.Add(IDevice)

        Return Me
    End Function

    Public Overrides Function Clone() As Object
        Return cloneSubr(ServoMotor.GetInstance)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
