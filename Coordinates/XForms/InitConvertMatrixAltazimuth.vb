Public Class InitConvertMatrixAltazimuth
    Inherits InitBase

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

    'Public Shared Function GetInstance() As InitConvertMatrixAltazimuth
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As InitConvertMatrixAltazimuth = New InitConvertMatrixAltazimuth
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As InitConvertMatrixAltazimuth
        Return New InitConvertMatrixAltazimuth
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Function Init() As Boolean
        Dim convert As ConvertMatrix = CType(ICoordXform, ConvertMatrix)
        Dim tempPosition As Position = PositionArraySingleton.GetInstance.GetPosition

        ' see notes for CoordXformBase
        ConvertMatrixPresetPositionAltaz.GetInstance.Preset(convert.One, convert.Two, convert.Site.Latitude.Rad)

        convert.One.ObjName = ""
        convert.Two.ObjName = ""

        convert.One.Init = True
        convert.Two.Init = True
        convert.Three.Init = False
        tempPosition.CopyFrom(convert.Position)
        convert.Position.CopyFrom(convert.One)
        convert.InitMatrix(1)
        convert.Position.CopyFrom(tempPosition)

        tempPosition.Available = True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class