#Region "Imports"
#End Region

Public Class DGVColumnNames
    Inherits SFTFacadeBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Protected Shared pSFTSharedSupport As New SFTSharedSupport(GetType(DGVColumnNames))

    Public Shared Name As New SFTPrototype(pSFTSharedSupport, "Name", "Object Name")
    Public Shared Source As New SFTPrototype(pSFTSharedSupport, "Source", "Source File")
    Public Shared ErrorType As New SFTPrototype(pSFTSharedSupport, "ErrorType", "Type of Error")
    Public Shared RA As New SFTPrototype(pSFTSharedSupport, "RA")
    Public Shared Dec As New SFTPrototype(pSFTSharedSupport, "Dec")
    Public Shared Az As New SFTPrototype(pSFTSharedSupport, "Az")
    Public Shared Alt As New SFTPrototype(pSFTSharedSupport, "Alt")
    Public Shared RADisplay As New SFTPrototype(pSFTSharedSupport, "RADisplay", Coordinates.CoordName.RA.Description)
    Public Shared DecDisplay As New SFTPrototype(pSFTSharedSupport, "DecDisplay", Coordinates.CoordName.Dec.Description)
    Public Shared AzDisplay As New SFTPrototype(pSFTSharedSupport, "AzDisplay", Coordinates.CoordName.Az.Description)
    Public Shared AltDisplay As New SFTPrototype(pSFTSharedSupport, "AltDisplay", Coordinates.CoordName.Alt.Description)
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DGVColumnNames
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DGVColumnNames = New DGVColumnNames
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As DGVColumnNames
        Return New DGVColumnNames
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
