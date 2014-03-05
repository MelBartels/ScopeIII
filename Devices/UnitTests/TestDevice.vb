#Region "Imports"
#End Region

Public Class TestDevice
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

    'Public Shared Function GetInstance() As TestDevice
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestDevice = New TestDevice
    'End Class
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared Function GetInstance() As TestDevice
        Return New TestDevice
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function Build() As IDevice
        Return Build(DeviceName.TestDevice.Name)
    End Function

    Public Overloads Overrides Function Build(ByVal name As String) As IDevice
        Build(name, Me.GetType.Name, createActiveStatus)

        buildDevPropDeviceCmdSet(DeviceCmdSet.Test.Name)

        Return Me
    End Function

    Public Overrides Function Clone() As Object
        Return cloneSubr(TestDevice.GetInstance)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
