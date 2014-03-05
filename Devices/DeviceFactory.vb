#Region "Imports"
Imports System.Reflection
#End Region

Public Class DeviceFactory

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

    'Public Shared Function GetInstance() As DeviceFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceFactory = New DeviceFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceFactory
        Return New DeviceFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Build(ByRef device As ISFT, Optional ByVal buildArg As String = Nothing) As IDevice
        Dim IDevice As IDevice

        Dim mType As Type = Type.GetType(CommonShared.IncludeNamespaceWithTypename(device.Name, Me))
        Dim [object] As Object = mType.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        IDevice = CType([object], ScopeIII.Devices.IDevice)

        If IDevice Is Nothing Then
            ExceptionService.Notify("Could not build device " & device.Name)
            Return Nothing
        End If

        If String.IsNullOrEmpty(buildArg) Then
            IDevice.Build()
        Else
            IDevice.Build(buildArg)
        End If

        DeviceTemplateBuilder.GetInstance.Build(IDevice)

        Return IDevice
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
