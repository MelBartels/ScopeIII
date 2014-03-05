''' -----------------------------------------------------------------------------
''' Project	 : CoordXforms
''' Class	 : CoordXforms.PostInitCalcs
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Calculate various post initialization variables.
''' Independent of coordinate transform method.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	4/25/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class PostInitCalcs

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ICoordXform As ICoordXform

    Public UpdatePostInitVars As Boolean
    Public UpdatePostInitVarsForFieldRotationOnly As Boolean

    Public PositionOffset As PositionOffset
    Public LatitudeBasedOnScopeAtEquatPole As Coordinate
    Public PolarAlignErrorBasedOnScopeAtEquatPole As AZdouble
    Public ZenithErrorBasedOnScopeAtSiteZenith As AZdouble
    Public LatitudeBasedOnScopeAtScopePole As Coordinate
    Public LongitudeBasedOnScopeAtScopePole As Double
    Public PolarAlignErrorBasedOnScopeAtScopePole As AZdouble
#End Region

#Region "Private and Protected Members"
    Dim pTime As Time
    Dim pTempPosition As Position
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PostInitCalcs
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PostInitCalcs = New PostInitCalcs
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        PositionOffset = Coordinates.PositionOffset.GetInstance
        LatitudeBasedOnScopeAtEquatPole = Coordinate.GetInstance
        PolarAlignErrorBasedOnScopeAtEquatPole = AZdouble.GetInstance
        ZenithErrorBasedOnScopeAtSiteZenith = AZdouble.GetInstance
        LatitudeBasedOnScopeAtScopePole = Coordinate.GetInstance
        PolarAlignErrorBasedOnScopeAtScopePole = AZdouble.GetInstance
        pTime = Time.GetInstance
    End Sub

    Public Shared Function GetInstance() As PostInitCalcs
        Return New PostInitCalcs
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub CheckForPostInitVars()
        If UpdatePostInitVars = True Then
            If UpdatePostInitVarsForFieldRotationOnly = True Then
                calcPostInitVarsForFieldRotationOnly()
            Else
                calcPostInitVars()
            End If
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' can be called after initialization: 
    ''' calculates apparent scope latitude (from two perspectives), longitude, offset hour angle, offset azimuth, 
    ''' zenith and polar offsets
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	2/23/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub calcPostInitVars()
        saveCurrentPositionToTemp()
        useCurrentSidT()
        scopeAtEquatPole()
        scopeAtSiteZenith()
        scopeAtScopePole()
        ScopeAtScopePoleForPolarAlignError()
        RestoreCurrentPositionFromTemp()
        UpdatePostInitVars = False
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' shortened version of calcPostInitVars() that runs must faster
    ''' use when only variables for field rotation calculation are needed 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	2/25/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub calcPostInitVarsForFieldRotationOnly()
        saveCurrentPositionToTemp()
        useCurrentSidT()
        scopeAtEquatPole()
        scopeAtScopePole()
        RestoreCurrentPositionFromTemp()
        UpdatePostInitVars = False
    End Sub

    Private Sub saveCurrentPositionToTemp()
        pTempPosition = PositionArraySingleton.GetInstance.GetPosition
        pTempPosition.CopyFrom(ICoordXform.Position)
    End Sub

    Private Sub RestoreCurrentPositionFromTemp()
        ICoordXform.Position.CopyFrom(pTempPosition)
        pTempPosition.Available = True
    End Sub

    ' use current time
    Private Sub useCurrentSidT()
        ICoordXform.Position.SidT.Rad = pTime.CalcSidTNow
    End Sub

    ' aim at equatorial pole to get polar alignment error and latitude
    Private Sub scopeAtEquatPole()
        ICoordXform.Position.Dec.Rad = Units.QtrRev
        ICoordXform.Position.RA.Rad = ICoordXform.Position.SidT.Rad
        ICoordXform.GetAltaz()
        PositionOffset.Position.Az.Rad = ICoordXform.Position.Az.Rad
        LatitudeBasedOnScopeAtEquatPole.Rad = ICoordXform.Position.Alt.Rad
        PolarAlignErrorBasedOnScopeAtEquatPole.A = Units.QtrRev - ICoordXform.Position.Alt.Rad
        PolarAlignErrorBasedOnScopeAtEquatPole.Z = ICoordXform.Position.Az.Rad
    End Sub

    ' aim at site zenith (meridian is sidereal time, declination of zenith = site latitude) to get scope offset from site zenith
    Private Sub scopeAtSiteZenith()
        ICoordXform.Position.RA.Rad = ICoordXform.Position.SidT.Rad
        ICoordXform.Position.Dec.Rad = ICoordXform.Site.Latitude.Rad
        ICoordXform.GetAltaz()
        ZenithErrorBasedOnScopeAtSiteZenith.A = Units.QtrRev - ICoordXform.Position.Alt.Rad
        ZenithErrorBasedOnScopeAtSiteZenith.Z = ICoordXform.Position.Az.Rad
    End Sub

    ' aim at scope pole to get latitude from this perspective, longitude, and hour angle offset
    Private Sub scopeAtScopePole()
        ICoordXform.Position.Alt.Rad = Units.QtrRev
        ICoordXform.Position.Az.Rad = 0
        ICoordXform.GetEquat()
        LatitudeBasedOnScopeAtScopePole.Rad = ICoordXform.Position.Dec.Rad
        ' longitudeDeg*units.DegToRad + current.sidT.rad = Greenwich Sidereal time
        ' difference between GST and current.ra.rad (= zenith) will be scope longitude
        LongitudeBasedOnScopeAtScopePole = ICoordXform.Site.Longitude.Rad + ICoordXform.Position.SidT.Rad - ICoordXform.Position.RA.Rad
        LongitudeBasedOnScopeAtScopePole = eMath.ValidRad(LongitudeBasedOnScopeAtScopePole)
        ' find hour angle offset = LST(current.sidT.rad) - scope's meridian,
        ' ha = LST - offset.ha.rad - ra, or, offset.ha.rad = LST - ra, by setting for zenith (ha = 0)
        ' + offset = scope tilted to West, - offset = scope tilted to East
        ' offset.ha.rad varies from - offset to + offset(should be a small amount)
        PositionOffset.Position.HA.Rad = ICoordXform.Position.SidT.Rad - ICoordXform.Position.RA.Rad
        PositionOffset.Position.HA.Rad = eMath.ValidRadPi(PositionOffset.Position.HA.Rad)
    End Sub

    ' aim at scope pole to get polar offset from another perspective
    Private Sub ScopeAtScopePoleForPolarAlignError()
        ICoordXform.Position.Alt.Rad = Units.QtrRev
        ICoordXform.Position.Az.Rad = 0
        ICoordXform.GetEquat()
        PolarAlignErrorBasedOnScopeAtScopePole.A = Units.QtrRev - ICoordXform.Position.Dec.Rad
        PolarAlignErrorBasedOnScopeAtScopePole.Z = eMath.ValidRadPi(ICoordXform.Position.SidT.Rad - ICoordXform.Position.RA.Rad)
    End Sub
#End Region

End Class