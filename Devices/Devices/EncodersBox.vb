#Region "Imports"
#End Region

Public Class EncodersBox
    Inherits DeviceBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public PriAxisName As String
    Public SecAxisName As String
#End Region

#Region "Private and Protected Members"
    Protected maxCountsPerRevolution As Int32
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EncodersBox
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EncodersBox = New EncodersBox
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        PriAxisName = Axis.PriAxis.Name & DeviceName.Encoder.Name
        SecAxisName = Axis.SecAxis.Name & DeviceName.Encoder.Name
        maxCountsPerRevolution = 65535
    End Sub

    Public Shared Function GetInstance() As EncodersBox
        Return New EncodersBox
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function Build() As IDevice
        Return Build(DeviceName.EncodersBox.Name)
    End Function

    Public Overloads Overrides Function Build(ByVal name As String) As IDevice
        ' DeviceBase.Build
        Build(name, GetType(EncodersBox).Name, createActiveStatus)

        Dim IDevice As IDevice = Encoder.GetInstance
        IDevice.Build(PriAxisName)
        IDevices.Add(IDevice)

        IDevice = Encoder.GetInstance
        IDevice.Build(SecAxisName)
        IDevices.Add(IDevice)

        buildDevPropDeviceCmdSet(DeviceCmdSet.TangentEncodersWithResetR.Name)

        Return Me
    End Function

    Public Overridable Overloads Function SetCountsPerRevolution(ByVal priAxisCountsPerRevolution As Int32, ByVal secAxisCountsPerRevolution As Int32) As Boolean
        Dim encoder As Encoder = CType(GetDevicesByName(PriAxisName).Item(0), Encoder)
        Dim value As EncoderValue = CType(encoder.GetProperty(GetType(EncoderValue).Name), EncoderValue)
        If priAxisCountsPerRevolution > maxCountsPerRevolution Then
            Return False
        End If
        SetCountsPerRevolution(value, priAxisCountsPerRevolution)

        encoder = CType(GetDevicesByName(SecAxisName).Item(0), Encoder)
        value = CType(encoder.GetProperty(GetType(EncoderValue).Name), EncoderValue)
        If secAxisCountsPerRevolution > maxCountsPerRevolution Then
            Return False
        End If
        SetCountsPerRevolution(value, secAxisCountsPerRevolution)

        Return True
    End Function

    Public Overridable Function GetValues() As String()
        Dim values(1) As String

        Dim encoder As Encoder = CType(GetDevicesByName(PriAxisName).Item(0), Encoder)
        values(0) = CStr(encoder.Value)
        encoder = CType(GetDevicesByName(SecAxisName).Item(0), Encoder)
        values(1) = CStr(encoder.Value)

        Return values
    End Function

    Public Overridable Function SetValues(ByVal priValue As String, ByVal secValue As String) As Boolean
        Dim encoder As Encoder = CType(GetDevicesByName(PriAxisName).Item(0), Encoder)
        ' encoder.Value = ... also updates EncoderValue.Value
        encoder.Value = priValue

        encoder = CType(GetDevicesByName(SecAxisName).Item(0), Encoder)
        encoder.Value = secValue

        Return True
    End Function

    Public Overrides Function Clone() As Object
        Return cloneSubr(EncodersBox.GetInstance)
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overloads Sub setCountsPerRevolution(ByRef encoderValue As EncoderValue, ByVal countsPerRevolution As Int32)
        If countsPerRevolution > maxCountsPerRevolution / 2 Then
            EncoderValue.MinValue = 0.ToString
            EncoderValue.MaxValue = (countsPerRevolution - 1).ToString
        Else
            EncoderValue.MinValue = (-CType(countsPerRevolution / 2, Int32)).ToString
            EncoderValue.MaxValue = (countsPerRevolution + CType(EncoderValue.MinValue, Int32) - 1).ToString
        End If
    End Sub
#End Region

End Class
