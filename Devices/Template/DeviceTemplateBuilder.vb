#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class DeviceTemplateBuilder

#Region "Inner Classes"
    Private Class TestIOStatusObserver : Implements IObserver
        Public Success As Boolean
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Dim msg As String = CStr([object])
            If msg.Equals(IOStatus.ValidResponse.Name) Then
                Success = True
            Else
                Success = False
            End If
        End Function
    End Class

    Private Class TestSensorStatusObserver : Implements IObserver
        Public Success As Boolean
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            Dim msg As String = CStr([object])
            If msg.Equals(SensorStatus.ValidRead.Name) Then
                Success = True
            Else
                Success = False
            End If
        End Function
    End Class
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

    'Public Shared Function GetInstance() As DeviceTemplateBuilder
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceTemplateBuilder = New DeviceTemplateBuilder
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceTemplateBuilder
        Return New DeviceTemplateBuilder
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Build(ByRef Device As IDevice) As Boolean
        ' create template object for tag: setting the template's device and devices array references
        Dim DeviceTemplate As DeviceTemplate = ScopeIII.Devices.DeviceTemplate.GetInstance
        DeviceTemplate.DeviceObserver = ObservableImp.GetInstance
        DeviceTemplate.StatusObserver = ObservableImp.GetInstance
        Device.DeviceTemplateArrayIx = DeviceTemplateArray.GetInstance.DeviceTemplates.Add(DeviceTemplate)

        ' recursively create tags for children
        If Device.IDevices IsNot Nothing Then
            For Each childDevice As IDevice In Device.IDevices
                Build(childDevice)
            Next
        End If
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
