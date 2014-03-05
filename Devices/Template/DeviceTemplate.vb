#Region "Imports"
#End Region

Public Class DeviceTemplate

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pDeviceObservableImp As ObservableImp
    Private pStatusObservableImp As ObservableImp
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceTemplate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceTemplate = New DeviceTemplate
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceTemplate
        Return New DeviceTemplate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property DeviceObserver() As ObservableImp
        Get
            Return pDeviceObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pDeviceObservableImp = Value
        End Set
    End Property

    Public Property StatusObserver() As ObservableImp
        Get
            Return pStatusObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pStatusObservableImp = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region
End Class
