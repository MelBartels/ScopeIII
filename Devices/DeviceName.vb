#Region "Imports"
#End Region

Public Class DeviceName
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(DeviceName))

    Public Shared Encoder As New SFTPrototype(pSFTSharedSupport, "Encoder")
    Public Shared EncodersBox As New SFTPrototype(pSFTSharedSupport, "Encoders Box (Tangent type): reads 2 encoders")
    Public Shared ServoMotor As New SFTPrototype(pSFTSharedSupport, "Servo motor with encoder")
    Public Shared SiTechController As New SFTPrototype(pSFTSharedSupport, "Sidereal Technology Controller")
    Public Shared JRKerrServoController As New SFTPrototype(pSFTSharedSupport, "JRKerr servo motor controller")
    Public Shared CustomDevice As New SFTPrototype(pSFTSharedSupport, "Custom defined device")
    Public Shared TestDevice As New SFTPrototype(pSFTSharedSupport, "Test device")

#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceName
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceName = New DeviceName
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceName
        Return New DeviceName
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
