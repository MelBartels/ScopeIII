Public Class ConvertMatrixPresetPositionAltaz
    Inherits ConvertMatrixPresetPositionBase

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

    'Public Shared Function GetInstance() As ConvertMatrixPresetPositionAltaz
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ConvertMatrixPresetPositionAltaz = New ConvertMatrixPresetPositionAltaz
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As ConvertMatrixPresetPositionAltaz
        Return New ConvertMatrixPresetPositionAltaz
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Sub Preset(ByRef one As Position, ByRef two As Position, ByVal latitudeRad As Double)
        Dim absLatDeg As Double = Math.Abs(latitudeRad * Units.RadToDeg)
        Dim decDeg As Double = 90
        If latitudeRad < 0 Then
            decDeg = -90
        End If
        one.SetCoordDeg(0, decDeg, 0, absLatDeg, 0)
        two.SetCoordDeg(0, 0, 180, 90 - absLatDeg, 0)
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class