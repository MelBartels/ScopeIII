Public MustInherit Class ConvertMatrixPresetPositionBase
    Implements IConvertMatrixPresetPosition

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

    'Public Shared Function GetInstance() As ConvertMatrixPresetPositionBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ConvertMatrixPresetPositionBase = New ConvertMatrixPresetPositionBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As ConvertMatrixPresetPositionBase
    '    Return New ConvertMatrixPresetPositionBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public MustOverride Sub Preset(ByRef one As Position, ByRef two As Position, ByVal latitudeRad As Double) Implements IConvertMatrixPresetPosition.Preset
#End Region

#Region "Private and Protected Methods"
#End Region

End Class