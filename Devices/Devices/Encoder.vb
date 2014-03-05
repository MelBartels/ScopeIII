#Region "Imports"
#End Region

Public Class Encoder
    Inherits DeviceBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Public Const DefaultMinEncoderValue As Int32 = 0
    Public Const DefaultMaxEncoderValue As Int32 = 9999
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Encoder
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Encoder = New Encoder
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Encoder
        Return New Encoder
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Value() As String
        Get
            Dim encoderValue As EncoderValue = CType(GetProperty(GetType(EncoderValue).Name), EncoderValue)
            Return encoderValue.Value
        End Get
        Set(ByVal value As String)
            Dim encoderValue As EncoderValue = CType(GetProperty(GetType(EncoderValue).Name), EncoderValue)
            encoderValue.Value = value
            Dim deviceTemplate As DeviceTemplate = GetDeviceTemplate()
            If deviceTemplate IsNot Nothing Then
                deviceTemplate.StatusObserver.Notify(CObj(encoderValue.ValueStatus))
            End If
        End Set
    End Property

    Public Overloads Overrides Function Build() As IDevice
        Return Build(DeviceName.Encoder.Name)
    End Function

    Public Overloads Overrides Function Build(ByVal name As String) As IDevice
        Build(name, GetType(Encoder).Name, createActiveStatus)

        Dim encoderValue As EncoderValue = ScopeIII.Devices.EncoderValue.GetInstance
        encoderValue.Build(DefaultMinEncoderValue, DefaultMaxEncoderValue, Rotation.CW.Name)
        Properties.Add(encoderValue)

        Return Me
    End Function

    Public Overrides Function Clone() As Object
        Return cloneSubr(Encoder.GetInstance)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
