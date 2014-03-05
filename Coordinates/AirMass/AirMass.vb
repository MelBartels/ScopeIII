#Region "Imports"
#End Region

Public Class AirMass

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

    'Public Shared Function GetInstance() As AirMass
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AirMass = New AirMass
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AirMass
        Return New AirMass
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
        Dim zenithDistance As Double
        Dim secZenithDistance As Double

        zenithDistance = Units.QtrRev - altRad
        If zenithDistance > Units.QtrRev Then
            zenithDistance = Units.QtrRev
        Else
            If zenithDistance < 0 Then
                zenithDistance = 0
            End If
        End If

        secZenithDistance = 1 / Math.Cos(zenithDistance)
        pAirMass = secZenithDistance - 0.0018161 * (secZenithDistance - 1) - 0.002875 * Math.Pow(secZenithDistance - 1, 2) - 0.0008083 * Math.Pow(secZenithDistance - 1, 3)
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
