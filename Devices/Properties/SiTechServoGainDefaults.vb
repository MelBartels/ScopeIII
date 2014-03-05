#Region "Imports"
#End Region

Public Class SiTechServoGainDefaults
    Implements IServoGainDefaults

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

    'Public Shared Function GetInstance() As SiTechDevPropServoGainDefaults
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SiTechServoGainDefaults = New SiTechServoGainDefaults
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SiTechServoGainDefaults
        Return New SiTechServoGainDefaults
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub SetToDefaults(ByVal servoGain As ServoGain) Implements IServoGainDefaults.SetToDefaults
        With servoGain
            .PositionGainKp = 20000
            .DerivativeGainKd = 32000
            .IntegralGainKi = 5000
            .IntegrationLimitIL = 20000
        End With
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
