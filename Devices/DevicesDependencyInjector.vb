#Region "Imports"
#End Region

Public Class DevicesDependencyInjector
    Inherits DependencyInjectorBase

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
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DevicesDependencyInjector
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As DevicesDependencyInjector = New DevicesDependencyInjector
    End Class
#End Region

#Region "Constructors"
    'Protected Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevicesDependencyInjector
    '    Return New DevicesDependencyInjector
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function UpdateDeviceCmdsObserverFactory() As UpdateDeviceCmdsObserver
        Return pResolver.Resolve(Of UpdateDeviceCmdsObserver)()
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub buildProductionIoC()
        InversionOfControl.Initialize(Nothing)
        pResolver = DependencyResolver.GetInstance

        pResolver.Register(Of UpdateDeviceCmdsObserver)(UpdateDeviceCmdsObserver.GetInstance)

        InversionOfControl.Initialize(pResolver)
    End Sub

    Protected Overrides Sub buildTestIoC()
        InversionOfControl.Initialize(Nothing)
        pResolver = DependencyResolver.GetInstance

        pResolver.Register(Of UpdateDeviceCmdsObserver)(EndoTestUpdateDeviceCmdsObserver.GetInstance)

        InversionOfControl.Initialize(pResolver)
    End Sub
#End Region

End Class
