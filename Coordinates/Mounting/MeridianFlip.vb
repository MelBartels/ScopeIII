' meridianFlip:
'
' if scope is on east side of pier facing west, then flip is defined as 'off' such that no coordinate
' ranslation need be done
' if scope is on west side of pier facing east, then flip is active
' 
' if mount is flipped across meridian, then flipped ra differs from original setting circle
' ra by 12 hrs altitude reading is mirrored across the pole, that is, an alt of 80 is actually 100
' (mirrored across 90) as read from the original setting circle orientation
' ie,
' northern hemisphere (az increases as scope tracks, az=0 when on meridian):
' not flipped:
' 	1 hr west of meridian (ra < sidT), coord are ra:(sidT-1hr), dec:45, alt:45, az:15
' 	1 hr east of meridian (ra > sidT), coord are ra:(sidT+1hr), dec:45, alt:45, az:345 (should be flipped)
' same coord flipped (scope assumed to have moved the flip, but aimed back at original equat coord):
' 	1 hr west of meridian (ra < sidT), coord are ra:(sidT-1hr), dec:45, alt:135, az:195 (should be un-flipped)
' 	1 hr east of meridian (ra > sidT), coord are ra:(sidT+1hr), dec:45, alt:135, az:165
' southern hemisphere (az decreases as scope tracks, az=0 when on meridian):
' not flipped:
' 	1 hr west of meridian (ra < sidT), coord are ra:(sidT-1hr), dec:-45, alt:45, az:345
' 	1 hr east of meridian (ra > sidT), coord are ra:(sidT+1hr), dec:-45, alt:45, az:15 (should be flipped)
' same coord flipped (scope assumed to have moved the flip, but aimed back at original equat coord):
' 	1 hr west of meridian (ra < sidT), coord are ra:(sidT-1hr), dec:-45, alt:135, az:165 (should be un-flipped)
' 	1 hr east of meridian (ra > sidT), coord are ra:(sidT+1hr), dec:-45, alt:135, az:195
'
' Z3 or altitude offset error:
' if scope aimed at 70 but setting circles say 60, then Z3 = 10
' meridian flipped position: scope aimed at 110 with setting circles indicate 100
'
' also see meridianNeedsFlipping() notes
Public Class MeridianFlip

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pPossible As Boolean
    Private pRequired As Boolean
    Private pMeridianFlipState As ISFT
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MeridianFlip
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MeridianFlip = New MeridianFlip
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pMeridianFlipState = MeridianFlipState.PointingWest
        pPossible = False
        pRequired = False
    End Sub

    Public Shared Function GetInstance() As MeridianFlip
        Return New MeridianFlip
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Possible() As Boolean
        Get
            Return pPossible
        End Get
        Set(ByVal Value As Boolean)
            pPossible = Value
        End Set
    End Property

    Public Property Required() As Boolean
        Get
            Return pRequired
        End Get
        Set(ByVal Value As Boolean)
            pRequired = Value
        End Set
    End Property

    Public Property State() As ISFT
        Get
            Return pMeridianFlipState
        End Get
        Set(ByVal Value As ISFT)
            pMeridianFlipState = Value
        End Set
    End Property

    Public Function SetMeridianFlipFromCurrentAltaz(ByRef position As Position, ByRef fabErrors As FabErrors) As Boolean
        If pPossible = True Then
            pMeridianFlipState = MeridianFlipState.PointingWest
            If pRequired = True Then
                ' use true, not indicated altitude;
                ' northern hemisphere:
                ' if setting circles = 85 and Z3 = 10 then scope aimed at 95 and is flipped;
                ' southern hemisphere:
                ' if setting circles = -85 and Z3 = 10 then scope aimed at -75 and is NOT flipped;
                ' if setting circles = -85 and Z3 = -10 then scope aimed at -95 and is flipped;
                Dim trueAltRad As Double = position.Alt.Rad + fabErrors.Z3.Rad
                If trueAltRad > Units.QtrRev OrElse trueAltRad < -Units.QtrRev Then
                    pMeridianFlipState = MeridianFlipState.PointingEast
                    Return True
                End If
            End If
        End If
        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class