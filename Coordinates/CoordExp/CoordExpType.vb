#Region "Imports"
#End Region

Public Class CoordExpType
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(CoordExpType))

    Public Shared Radian As New SFTPrototype(pSFTSharedSupport, "double")
    Public Shared Degree As New SFTPrototype(pSFTSharedSupport, "double")
    Public Shared WholeNumDegree As New SFTPrototype(pSFTSharedSupport, "DDd")
    Public Shared WholeNumNegDegree As New SFTPrototype(pSFTSharedSupport, "+DDd")
    Public Shared WholeNumHour As New SFTPrototype(pSFTSharedSupport, "HH")
    Public Shared WholeNumNegHour As New SFTPrototype(pSFTSharedSupport, "+HH")
    Public Shared DMS As New SFTPrototype(pSFTSharedSupport, "DD:MM:SS")
    Public Shared HMS As New SFTPrototype(pSFTSharedSupport, "HH:MM:SS")
    Public Shared HMSM As New SFTPrototype(pSFTSharedSupport, "HH:MM:SS.SSS")
    Public Shared FormattedDegree As New SFTPrototype(pSFTSharedSupport, "+DD.DDD degree")
    Public Shared FormattedDMS As New SFTPrototype(pSFTSharedSupport, "+DDd MMm SSs")
    Public Shared FormattedHMS As New SFTPrototype(pSFTSharedSupport, "HHh MMm SSs")
    Public Shared FormattedHMSM As New SFTPrototype(pSFTSharedSupport, "HHh MMm SS.SSSs")
    Public Shared DatafileDMS As New SFTPrototype(pSFTSharedSupport, "DD MM SS")
    Public Shared DatafileHMS As New SFTPrototype(pSFTSharedSupport, "HH MM SS")
    Public Shared LX200LongDeg As New SFTPrototype(pSFTSharedSupport, "DDD^MM:SS#")
    Public Shared LX200ShortDeg As New SFTPrototype(pSFTSharedSupport, "DDD^MM.M#")
    Public Shared LX200SignedLongDeg As New SFTPrototype(pSFTSharedSupport, "+DD^MM:SS#")
    Public Shared LX200SignedShortDeg As New SFTPrototype(pSFTSharedSupport, "+DD^MM.M#")
    Public Shared LX200LongHr As New SFTPrototype(pSFTSharedSupport, "HH:MM:SS#")
    Public Shared LX200ShortHr As New SFTPrototype(pSFTSharedSupport, "HH:MM#")
    Public Shared XmlDMS As New SFTPrototype(pSFTSharedSupport, "")
    Public Shared XmlHMSM As New SFTPrototype(pSFTSharedSupport, "")
    Public Shared AirMass As New SFTPrototype(pSFTSharedSupport, "")
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CoordExpType
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordExpType = New CoordExpType
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As CoordExpType
        Return New CoordExpType
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
