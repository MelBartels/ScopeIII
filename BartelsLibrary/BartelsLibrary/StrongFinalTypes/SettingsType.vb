#Region "Imports"
#End Region

Public Class SettingsType
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(SettingsType))

    Public Shared SerialPort As New SFTPrototype(pSFTSharedSupport, "Serial Port")
    Public Shared IP As New SFTPrototype(pSFTSharedSupport, "IP")
    Public Shared Devices As New SFTPrototype(pSFTSharedSupport, "Devices")
    Public Shared NamedDevice As New SFTPrototype(pSFTSharedSupport, "NamedDevice")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SettingsType
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SettingsType = New SettingsType
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SettingsType
        Return New SettingsType
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides ReadOnly Property FirstItem() As ISFT
        Get
            Return pSFTSharedSupport.FirstItem
        End Get
    End Property

    Public Shared ReadOnly Property ISFT() As ISFT
        Get
            Return pSFTSharedSupport.FirstItem
        End Get
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
