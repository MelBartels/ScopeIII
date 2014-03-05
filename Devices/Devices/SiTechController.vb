#Region "Imports"
#End Region

Public Class SiTechController
    Inherits DeviceBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const MotorEncoderName As String = "Motor Encoder"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public SiTechReadControllerData As SiTechReadControllerData
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SiTechController
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SiTechController = New SiTechController
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        SiTechReadControllerData = SiTechReadControllerData.GetInstance
    End Sub

    Public Shared Function GetInstance() As SiTechController
        Return New SiTechController
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function Build() As IDevice
        Return Build(DeviceName.SiTechController.Name)
    End Function

    Public Overloads Overrides Function Build(ByVal name As String) As IDevice
        ' DeviceBase.Build
        Build(name, GetType(SiTechController).Name, createActiveStatus)

        Dim IDevice As IDevice = ServoMotor.GetInstance
        IDevice.Build()
        IDevices.Add(IDevice)
        Dim IDevProp As IDevProp = IDevice.GetProperty(GetType(ServoGain).Name)
        SiTechServoGainDefaults.GetInstance.SetToDefaults(CType(IDevProp, ServoGain))

        IDevice = ServoMotor.GetInstance
        IDevice.Build()
        IDevices.Add(IDevice)
        IDevProp = IDevice.GetProperty(GetType(ServoGain).Name)
        SiTechServoGainDefaults.GetInstance.SetToDefaults(CType(IDevProp, ServoGain))

        IDevice = Encoder.GetInstance
        IDevice.Build()
        IDevices.Add(IDevice)

        IDevice = Encoder.GetInstance
        IDevice.Build()
        IDevices.Add(IDevice)

        ' add unique properties
        Dim moduleAddress As ValueObject = ValueObject.GetInstance
        moduleAddress.Build(ValueObjectNames.ModuleAddress.Name, CStr(0), CStr(0), CStr(255), UOM.Scalar.Description)
        Properties.Add(moduleAddress)

        buildDevPropDeviceCmdSet(DeviceCmdSet.SiTechController.Name)

        SiTechUtil.BuildDeviceNamesPrimaryController(Me)

        ' update observers
        DeviceCmdsFacade.UpdateDeviceCmdsObserver.Build(Me, SiTechUpdateDeviceCmdAndReplyTemplate.GetInstance)
        moduleAddress.ObservableImp.Attach(CType(DeviceCmdsFacade.UpdateDeviceCmdsObserver, IObserver))

        Return Me
    End Function

    Public Overrides Function Clone() As Object
        Return cloneSubr(SiTechController.GetInstance)
    End Function

    Public Sub UpdateSiTechReadControllerData()
        With SiTechReadControllerData
            Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryServoMotorEncoder(Me), Encoder)
            .SecMotorPosition = eMath.RInt(encoder.Value)

            encoder = CType(SiTechUtil.GetPrimaryServoMotorEncoder(Me), Encoder)
            .PriMotorPosition = eMath.RInt(encoder.Value)

            .SecEncoderPosition = eMath.RInt(CType(SiTechUtil.GetSecondaryEncoder(Me), Encoder).Value)

            .PriEncoderPosition = eMath.RInt(CType(SiTechUtil.GetPrimaryEncoder(Me), Encoder).Value)

            .EncodeByteReturn()
        End With
    End Sub

    Public Sub UpdateFromSiTechReadControllerData()
        With SiTechReadControllerData
            .DecodeByteReturn()

            Dim encoder As Encoder = CType(SiTechUtil.GetSecondaryEncoder(Me), Encoder)
            ' encoder.Value = ... also updates EncoderValue.Value
            encoder.Value = CStr(.SecEncoderPosition)

            encoder = CType(SiTechUtil.GetPrimaryEncoder(Me), Encoder)
            encoder.Value = CStr(.PriEncoderPosition)

            encoder = CType(SiTechUtil.GetSecondaryServoMotorEncoder(Me), Encoder)
            encoder.Value = CStr(.SecMotorPosition)

            encoder = CType(SiTechUtil.GetPrimaryServoMotorEncoder(Me), Encoder)
            encoder.Value = CStr(.PriMotorPosition)
        End With
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
