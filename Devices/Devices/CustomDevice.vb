#Region "Imports"
#End Region

Public Class CustomDevice
    Inherits DeviceBase

#Region "Inner Classes"
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

    'Public Shared Function GetInstance() As CustomDevice
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CustomDevice = New CustomDevice
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CustomDevice
        Return New CustomDevice
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function Build() As IDevice
        Return Build(DeviceName.CustomDevice.Name)
    End Function

    Public Overloads Overrides Function Build(ByVal name As String) As IDevice
        Build(name, GetType(CustomDevice).Name, createActiveStatus)

        Dim IDevProp As IDevProp = DevPropDeviceCmdSet.GetInstance
        CType(IDevProp, DevPropDeviceCmdSet).Name = DeviceCmdSet.None.Name
        Properties.Add(IDevProp)

        Return Me
    End Function

    Public Overrides Function Clone() As Object
        Return cloneSubr(CustomDevice.GetInstance)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
