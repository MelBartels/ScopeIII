''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.AltOffset
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' 
''' Coordinate Transform Notes:
''' 
''' Hour angle increases to the east in the direction of rising stars, as does Right Ascension;
''' Hour Angle offset = Local Sidereal time - scope's meridian;
''' scope's meridian = ra - ha;
''' haOff = LST - ra - ha, ha = LST - ra - haOff, ha + haOff = LST - ra; 
''' (+) HA = scope aimed to East, (-) HA = scope aimed to West;
''' (+) offset = scope tilted to West, (-) offset = scope tilted to East;
''' ex: scope tilted 1 hr east of meridian (haOff = -1 hr), HA = 2 hrs:
'''     ha + haOff = LST - ra, 2 + -1 = LST - ra, 1 = LST - ra: actual or net HA = 1;
''' 
''' Defined alignments for ConvertMatrix (altaz and equat):
''' 1st point: scope's celestial pole (faces north in northern hemisphere, faces south 
'''     in southern hemisphere)
''' 2nd point: intersection of celestial equator and meridian (faces south in northern
'''     hemisphere, faces north in southern hemisphere)
''' 
''' Coordinate Rules (consequences of the defined alignments, to be applied to all coordinate conversions):
'''     alt increases from horizon to zenith;
'''     az always increases clockwise (position your body along the pole with head 
'''         pointed upward, sweep arms from left to right clockwise);
'''     az of 0 always points towards Earth's closest pole, az of 180 points to Earth's equator;
'''     tracking motion causes az to reverse direction in southern hemisphere as compared 
'''         to northern hemisphere;
''' 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	4/25/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public MustInherit Class CoordXformBase
    Implements ICoordXform

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pCoordXformType As ISFT

    Protected pEMath As eMath
    Protected pSite As Coordinates.Site
    Protected pMeridianFlip As MeridianFlip
    Protected pXposition As Position
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CoordXformBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordXformBase = New CoordXformBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pSite = Coordinates.Site.GetInstance
        pMeridianFlip = Coordinates.MeridianFlip.GetInstance
        pXposition = PositionArraySingleton.GetInstance.GetPosition("Current")
    End Sub

    'Public Shared Function GetInstance() As CoordXformBase
    '    Return New CoordXformBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordXformType() As ISFT Implements ICoordXform.CoordXformType
        Get
            Return pCoordXformType
        End Get
        Set(ByVal Value As ISFT)
            pCoordXformType = Value
        End Set
    End Property

    Public Property MeridianFlip() As MeridianFlip Implements ICoordXform.MeridianFlip
        Get
            Return pMeridianFlip
        End Get
        Set(ByVal Value As MeridianFlip)
            pMeridianFlip = Value
        End Set
    End Property

    Public Property Position() As Coordinates.Position Implements ICoordXform.Position
        Get
            Return pXposition
        End Get
        Set(ByVal Value As Coordinates.Position)
            pXposition = Value
        End Set
    End Property

    Public Property Site() As Coordinates.Site Implements ICoordXform.Site
        Get
            Return pSite
        End Get
        Set(ByVal Value As Coordinates.Site)
            pSite = Value
        End Set
    End Property

    Public MustOverride Function GetEquat() As Boolean Implements ICoordXform.GetEquat

    Public MustOverride Function GetAltaz() As Boolean Implements ICoordXform.GetAltaz

    Public Sub TranslateAltazAcrossPole() Implements ICoordXform.TranslateAltazAcrossPole
        pXposition.Alt.Rad = Units.HalfRev - pXposition.Alt.Rad
        pXposition.Az.Rad = eMath.ValidRad(pXposition.Az.Rad + Units.HalfRev)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Sub TranslateAltazAcrossPoleBasedOnMeridianFlip()
        If MeridianFlip.Possible AndAlso MeridianFlip.State Is MeridianFlipState.PointingEast Then
            TranslateAltazAcrossPole()
        End If
    End Sub
#End Region


End Class