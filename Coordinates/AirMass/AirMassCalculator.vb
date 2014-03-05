#Region "Imports"
#End Region

Public Class AirMassCalculator

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Dim pAirMass As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As AirMassCalculator
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AirMassCalculator = New AirMassCalculator
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AirMassCalculator
        Return New AirMassCalculator
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property AirMass() As Double
        Get
            Return pAirMass
        End Get
        Set(ByVal Value As Double)
            pAirMass = Value
        End Set
    End Property

    Public Sub Calc(ByVal altRad As Double)
        Dim zenithDistanceRad As Double = Units.QtrRev - altRad
        If zenithDistanceRad > Units.QtrRev Then
            zenithDistanceRad = Units.QtrRev
        ElseIf zenithDistanceRad < 0 Then
            zenithDistanceRad = 0
        End If

        pAirMass = 1 / (Math.Cos(zenithDistanceRad) + 0.50572 * Math.Pow(96.07995 - zenithDistanceRad * Units.RadToDeg, -1.6364))
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
