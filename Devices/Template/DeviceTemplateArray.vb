#Region "Imports"
#End Region

Public Class DeviceTemplateArray

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pDeviceTemplates As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    Private Sub New()
        pDeviceTemplates = New ArrayList
    End Sub

    Public Shared Function GetInstance() As DeviceTemplateArray
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As DeviceTemplateArray = New DeviceTemplateArray
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceTemplateArray
    '    Return New DeviceTemplateArray
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property DeviceTemplates() As ArrayList
        Get
            Return pDeviceTemplates
        End Get
        Set(ByVal Value As ArrayList)
            pDeviceTemplates = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region
End Class
