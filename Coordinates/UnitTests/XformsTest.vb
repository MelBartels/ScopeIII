Imports NUnit.Framework
Imports Xunit

<TestFixture()> Public Class XformsTest

    Private Const AllowedErrorRad As Double = Units.ArcsecToRad / 1000

    Private pCelestialCoordinateCalcs As CelestialCoordinateCalcs

    Private pConvertTrig As ConvertTrig
    Private pConvertMatrix As ConvertMatrix

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        pCelestialCoordinateCalcs = CelestialCoordinateCalcs.GetInstance

        pConvertTrig = ConvertTrig.GetInstance
        pConvertMatrix = ConvertMatrix.GetInstance
    End Sub

    <Test()> <Fact()> Public Sub TestConvertMatrix()
        pCelestialCoordinateCalcs = CelestialCoordinateCalcs.GetInstance

        pConvertTrig = ConvertTrig.GetInstance
        pConvertMatrix = ConvertMatrix.GetInstance

        Dim varianceDeg As Double = 0.01

        ' from Toshimi Taki's test data, Sky and Telescope magazine, February 1989
        ' which uses z1=-.04, z2=.4, z3=-1.63
        pConvertMatrix.FabErrors.SetFabErrorsDeg(-0.04, 0.4, -1.63)
        'sidt=9.8268315082
        pConvertMatrix.Position.SetCoordDeg(79.172, 45.998, 360 - 39.9, 39.9, 39.2 * Units.SidRate / 4)
        pConvertMatrix.InitMatrix(1)
        NUnit.Framework.Assert.IsTrue(pConvertMatrix.One.Init)
        NUnit.Framework.Assert.AreEqual(79.172, pConvertMatrix.One.RA.Rad * Units.RadToDeg)
        'sidt=10.102584433175
        pConvertMatrix.Position.SetCoordDeg(37.96, 89.264, 360 - 94.6, 36.2, 40.3 * Units.SidRate / 4)
        pConvertMatrix.InitMatrix(2)
        NUnit.Framework.Assert.IsTrue(pConvertMatrix.Two.Init)
        NUnit.Framework.Assert.AreEqual(37.96, pConvertMatrix.Two.RA.Rad * Units.RadToDeg)
        DebugTrace.WriteLine(pConvertMatrix.One.ShowCoordDeg)
        DebugTrace.WriteLine(pConvertMatrix.Two.ShowCoordDeg)

        'sidt=11.78217043075
        pConvertMatrix.Position.SetCoordDeg(326.05, 9.88, 360 - 0, 0, 47 * Units.SidRate / 4)
        pConvertMatrix.GetAltaz()
        DebugTrace.WriteLine("following should be accurate to 0.01 deg   az: 157.46   alt: 42.16")
        DebugTrace.WriteLine(pConvertMatrix.Position.ShowCoordDeg())
        NUnit.Framework.Assert.AreEqual(360 - 202.54, pConvertMatrix.Position.Az.Rad * Units.RadToDeg, varianceDeg)
        NUnit.Framework.Assert.AreEqual(42.16, pConvertMatrix.Position.Alt.Rad * Units.RadToDeg, varianceDeg)

        pConvertMatrix.Position.SetCoordDeg(71.53, 17.07, 360 - 0, 0, 62 * Units.SidRate / 4)
        pConvertMatrix.GetAltaz()
        DebugTrace.WriteLine("following should be accurate to 0.01 deg   az: 0.02   alt: 40.31")
        DebugTrace.WriteLine(pConvertMatrix.Position.ShowCoordDeg())
        NUnit.Framework.Assert.AreEqual(360 - 359.98, pConvertMatrix.Position.Az.Rad * Units.RadToDeg, varianceDeg)
        NUnit.Framework.Assert.AreEqual(40.31, pConvertMatrix.Position.Alt.Rad * Units.RadToDeg, varianceDeg)

        pConvertMatrix.Position.SetCoordDeg(0, 0, 360 - 24.1, 35.5, 71.9 * Units.SidRate / 4)
        pConvertMatrix.GetEquat()
        DebugTrace.WriteLine("following should be accurate to 0.01 deg   ra: 87.99   dec: 32.51")
        DebugTrace.WriteLine(pConvertMatrix.Position.ShowCoordDeg())
        NUnit.Framework.Assert.AreEqual(87.99, pConvertMatrix.Position.RA.Rad * Units.RadToDeg, varianceDeg)
        NUnit.Framework.Assert.AreEqual(32.51, pConvertMatrix.Position.Dec.Rad * Units.RadToDeg, varianceDeg)
    End Sub

    <Test()> Public Sub TestConvertMatrixCast()
        Dim x As ICoordXform = CoordXformFactory.GetInstance.Build(CoordXformType.ConvertMatrix)
        CType(x, ConvertMatrix).Position.PosName = "test"
        DebugTrace.WriteLine(CType(x, ConvertMatrix).Position.ShowCoordDeg())
        NUnit.Framework.Assert.IsTrue(CObj(x).GetType Is GetType(ConvertMatrix))
    End Sub

    <Test()> Public Sub TestConvertTrig()
        ' from Peter Duffet-Smith's Practical Astronomy With Your Calculator
        pConvertTrig.Site.Latitude.Rad = 52 * Units.DegToRad
        pConvertTrig.Position.SidT.Rad = Units.HalfRev
        ' HA=LST-RA, so RA=LST-HA (+HA==west of meridian)
        pConvertTrig.Position.RA.Rad = pConvertTrig.Position.SidT.Rad - 5.862269 * Units.HrToRad
        pConvertTrig.Position.Dec.Rad = 23.219444 * Units.DegToRad
        pConvertTrig.GetAltaz()
        NUnit.Framework.Assert.AreEqual(283.271558 * Units.DegToRad, pConvertTrig.Position.Az.Rad, AllowedErrorRad * 100)
        NUnit.Framework.Assert.AreEqual(19.333925 * Units.DegToRad, pConvertTrig.Position.Alt.Rad, AllowedErrorRad * 100)

        ' northern hemisphere

        pConvertTrig.Site.Latitude.Rad = 45 * Units.DegToRad
        pConvertTrig.Position.SidT.Rad = Units.HalfRev

        'altazimuth alignment, latitude = 45 deg, 6 hr east of meridian
        'altaz should be on horizon facing directly east
        pConvertTrig.Position.RA.Rad = pConvertTrig.Position.SidT.Rad + 6 * Units.HrToRad
        pConvertTrig.Position.Dec.Rad = 0
        pConvertTrig.GetAltaz()
        NUnit.Framework.Assert.AreEqual(90 * Units.DegToRad, pConvertTrig.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(0, pConvertTrig.Position.Alt.Rad, AllowedErrorRad)

        'altazimuth alignment, latitude = 45 deg, on meridian
        'altaz should be facing directly south
        pConvertTrig.Position.RA.Rad = pConvertTrig.Position.SidT.Rad
        pConvertTrig.GetAltaz()
        NUnit.Framework.Assert.AreEqual(180 * Units.DegToRad, pConvertTrig.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(Units.QtrRev - Math.Abs(pConvertTrig.Site.Latitude.Rad), pConvertTrig.Position.Alt.Rad, AllowedErrorRad)

        'altazimuth alignment, latitude = 45 deg, 6 hr west of meridian
        'altaz should be on horizon facing directly west
        pConvertTrig.Position.RA.Rad = pConvertTrig.Position.SidT.Rad - 6 * Units.HrToRad
        pConvertTrig.Position.Dec.Rad = 0 * Units.DegToRad
        pConvertTrig.GetAltaz()
        NUnit.Framework.Assert.AreEqual(270 * Units.DegToRad, pConvertTrig.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(0, pConvertTrig.Position.Alt.Rad, AllowedErrorRad)

        ' southern hemisphere

        pConvertTrig.Site.Latitude.Rad = -45 * Units.DegToRad
        pConvertTrig.Position.SidT.Rad = Units.HalfRev

        'altazimuth alignment, latitude = -45 deg, 6 hr east of meridian
        'altaz should be on horizon facing directly east
        pConvertTrig.Position.RA.Rad = pConvertTrig.Position.SidT.Rad + 6 * Units.HrToRad
        pConvertTrig.Position.Dec.Rad = 0 * Units.DegToRad
        pConvertTrig.GetAltaz()
        NUnit.Framework.Assert.AreEqual(270 * Units.DegToRad, pConvertTrig.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(0, pConvertTrig.Position.Alt.Rad, AllowedErrorRad)

        'altazimuth alignment, latitude = -45 deg, on meridian
        'altaz should be facing directly north 
        pConvertTrig.Position.RA.Rad = pConvertTrig.Position.SidT.Rad
        pConvertTrig.Position.Dec.Rad = 0 * Units.DegToRad
        pConvertTrig.GetAltaz()
        NUnit.Framework.Assert.AreEqual(180 * Units.DegToRad, pConvertTrig.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(Units.QtrRev - Math.Abs(pConvertTrig.Site.Latitude.Rad), pConvertTrig.Position.Alt.Rad, AllowedErrorRad)

        'altazimuth alignment, latitude = -45 deg, 6 hr west of meridian
        'altaz should be on horizon facing directly west
        pConvertTrig.Position.RA.Rad = pConvertTrig.Position.SidT.Rad - 6 * Units.HrToRad
        pConvertTrig.Position.Dec.Rad = 0 * Units.DegToRad
        pConvertTrig.GetAltaz()
        NUnit.Framework.Assert.AreEqual(90 * Units.DegToRad, pConvertTrig.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(0, pConvertTrig.Position.Alt.Rad, AllowedErrorRad)
    End Sub

    <Test()> Public Sub TestConvertEquatTrig()
        Dim InitState As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertTrig, ISFT), CType(InitStateType.Equatorial, ISFT))
        TestConvertEquat(InitState)
    End Sub

    <Test()> Public Sub TestConvertEquatMatrix()
        Dim InitState As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Equatorial, ISFT))
        TestConvertEquat(InitState)
    End Sub

    Public Sub TestConvertEquat(ByRef initState As InitStateTemplate)
        ' northern hemisphere

        initState.ICoordXform.Site.Latitude.Rad = 90 * Units.DegToRad
        initState.ICoordXform.Position.SidT.Rad = Units.HalfRev
        initState.IInit.Init()
        ' 1 hr east of meridian
        initState.ICoordXform.Position.RA.Rad = initState.ICoordXform.Position.SidT.Rad + 30 * Units.DegToRad
        initState.ICoordXform.Position.Dec.Rad = 10 * Units.DegToRad
        initState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(150 * Units.DegToRad, eMath.ValidRad(initState.ICoordXform.Position.Az.Rad), AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(10 * Units.DegToRad, initState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        ' on meridian facing south
        initState.ICoordXform.Position.RA.Rad = initState.ICoordXform.Position.SidT.Rad
        initState.ICoordXform.Position.Dec.Rad = 10 * Units.DegToRad
        initState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(180 * Units.DegToRad, eMath.ValidRad(initState.ICoordXform.Position.Az.Rad), AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(10 * Units.DegToRad, initState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        ' convert from altaz to equat and back to altaz
        ' 1 hr east of meridian towards zenith
        initState.ICoordXform.Position.Az.Rad = 150 * Units.DegToRad
        initState.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        initState.ICoordXform.GetEquat()
        NUnit.Framework.Assert.AreEqual(eMath.ValidRad(initState.ICoordXform.Position.SidT.Rad + 30 * Units.DegToRad), initState.ICoordXform.Position.RA.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(70 * Units.DegToRad, initState.ICoordXform.Position.Dec.Rad, AllowedErrorRad)
        initState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(150 * Units.DegToRad, eMath.ValidRad(initState.ICoordXform.Position.Az.Rad), AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(70 * Units.DegToRad, initState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)

        ' southern hemisphere (az reverses direction)

        initState.ICoordXform.Site.Latitude.Rad = -90 * Units.DegToRad
        initState.ICoordXform.Position.SidT.Rad = Units.HalfRev
        initState.IInit.Init()
        ' 1 hr east of meridian facing north
        initState.ICoordXform.Position.RA.Rad = initState.ICoordXform.Position.SidT.Rad + 30 * Units.DegToRad
        initState.ICoordXform.Position.Dec.Rad = -10 * Units.DegToRad
        initState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(210 * Units.DegToRad, eMath.ValidRad(initState.ICoordXform.Position.Az.Rad), AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(10 * Units.DegToRad, initState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        ' on meridian facing north
        initState.ICoordXform.Position.RA.Rad = initState.ICoordXform.Position.SidT.Rad
        initState.ICoordXform.Position.Dec.Rad = -10 * Units.DegToRad
        initState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(180 * Units.DegToRad, eMath.ValidRad(initState.ICoordXform.Position.Az.Rad), AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(10 * Units.DegToRad, initState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        ' convert from altaz to equat and back to altaz
        ' 1 hr east of meridian facing north
        initState.ICoordXform.Position.Az.Rad = 210 * Units.DegToRad
        initState.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        initState.ICoordXform.GetEquat()
        NUnit.Framework.Assert.AreEqual(eMath.ValidRad(initState.ICoordXform.Position.SidT.Rad + 30 * Units.DegToRad), initState.ICoordXform.Position.RA.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(-70 * Units.DegToRad, initState.ICoordXform.Position.Dec.Rad, AllowedErrorRad)
        initState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(210 * Units.DegToRad, eMath.ValidRad(initState.ICoordXform.Position.Az.Rad), AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(70 * Units.DegToRad, initState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
    End Sub

    <Test()> Public Sub TestConvertMatrixCelestial()
        Dim InitState As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Celestial, ISFT))
        Dim pConvertMatrix As ConvertMatrix = CType(InitState.ICoordXform, ConvertMatrix)

        pConvertMatrix.FabErrors.SetFabErrorsDeg(-0.04, 0.4, -1.63)

        Dim position As Position = Coordinates.Position.GetInstance
        position.Available = False
        position.SetCoordDeg(79.172, 45.998, 360 - 39.9, 39.9, 39.2 * Units.SidRate / 4)
        pConvertMatrix.One.CopyFrom(position)
        position.SetCoordDeg(37.96, 89.264, 360 - 94.6, 36.2, 40.3 * Units.SidRate / 4)
        pConvertMatrix.Two.CopyFrom(position)
        InitState.IInit.Init()

        NUnit.Framework.Assert.IsTrue(pConvertMatrix.One.Init)
        NUnit.Framework.Assert.AreEqual(79.172, pConvertMatrix.One.RA.Rad * Units.RadToDeg)
        NUnit.Framework.Assert.IsTrue(pConvertMatrix.Two.Init)
        NUnit.Framework.Assert.AreEqual(37.96, pConvertMatrix.Two.RA.Rad * Units.RadToDeg)

        pConvertMatrix.Position.SetCoordDeg(326.05, 9.88, 360 - 0, 0, 47 * Units.SidRate / 4)
        pConvertMatrix.GetAltaz()
        DebugTrace.WriteLine("following should be accurate to 0.01 deg   az: 157.46   alt: 42.16")
        DebugTrace.WriteLine(pConvertMatrix.Position.ShowCoordDeg())

        Dim varianceDeg As Double = 0.01
        NUnit.Framework.Assert.AreEqual(360 - 202.54, pConvertMatrix.Position.Az.Rad * Units.RadToDeg, varianceDeg)
        NUnit.Framework.Assert.AreEqual(42.16, pConvertMatrix.Position.Alt.Rad * Units.RadToDeg, varianceDeg)
    End Sub

    <Test()> Public Sub TestConvertAltazTrig()
        Dim InitState As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertTrig, ISFT), CType(InitStateType.Altazimuth, ISFT))
        TestConvertAltaz(InitState)
    End Sub

    <Test()> Public Sub TestConvertAltazMatrix()
        Dim InitState As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Altazimuth, ISFT))
        TestConvertAltaz(InitState)
    End Sub

    Public Sub TestConvertAltaz(ByRef initState As InitStateTemplate)
        ' northern hemisphere

        initState.IInit.ICoordXform.Site.Latitude.Rad = 45 * Units.DegToRad
        initState.IInit.ICoordXform.Position.SidT.Rad = Units.HalfRev
        initState.IInit.Init()

        'altazimuth alignment, latitude = 45 deg, 6 hr east of meridian
        'altaz should be on horizon facing directly east
        initState.IInit.ICoordXform.Position.RA.Rad = initState.IInit.ICoordXform.Position.SidT.Rad + 6 * Units.HrToRad
        initState.IInit.ICoordXform.Position.Dec.Rad = 0
        initState.IInit.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(90 * Units.DegToRad, initState.IInit.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(0, initState.IInit.ICoordXform.Position.Alt.Rad, AllowedErrorRad)

        'altazimuth alignment, latitude = 45 deg, on meridian
        'altaz should be facing Earth's equator
        initState.IInit.ICoordXform.Position.RA.Rad = initState.IInit.ICoordXform.Position.SidT.Rad
        initState.IInit.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(180 * Units.DegToRad, initState.IInit.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(Units.QtrRev - Math.Abs(initState.IInit.ICoordXform.Site.Latitude.Rad), initState.IInit.ICoordXform.Position.Alt.Rad, AllowedErrorRad)

        'altazimuth alignment, latitude = 45 deg, 6 hr west of meridian
        'altaz should be on horizon facing directly west
        initState.IInit.ICoordXform.Position.RA.Rad = initState.IInit.ICoordXform.Position.SidT.Rad - 6 * Units.HrToRad
        initState.IInit.ICoordXform.Position.Dec.Rad = 0 * Units.DegToRad
        initState.IInit.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(270 * Units.DegToRad, initState.IInit.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(0, initState.IInit.ICoordXform.Position.Alt.Rad, AllowedErrorRad)

        ' southern hemisphere

        initState.IInit.ICoordXform.Site.Latitude.Rad = -45 * Units.DegToRad
        initState.IInit.ICoordXform.Position.SidT.Rad = Units.HalfRev
        initState.IInit.Init()

        'altazimuth alignment, latitude = -45 deg, 6 hr east of meridian
        'altaz should be on horizon facing directly east
        initState.IInit.ICoordXform.Position.RA.Rad = initState.IInit.ICoordXform.Position.SidT.Rad + 6 * Units.HrToRad
        initState.IInit.ICoordXform.Position.Dec.Rad = 0 * Units.DegToRad
        initState.IInit.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(270 * Units.DegToRad, initState.IInit.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(0, initState.IInit.ICoordXform.Position.Alt.Rad, AllowedErrorRad)

        'altazimuth alignment, latitude = -45 deg, on meridian
        'altaz should be facing Earth's equator
        initState.IInit.ICoordXform.Position.RA.Rad = initState.IInit.ICoordXform.Position.SidT.Rad
        initState.IInit.ICoordXform.Position.Dec.Rad = 0 * Units.DegToRad
        initState.IInit.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(180 * Units.DegToRad, eMath.ValidRad(initState.IInit.ICoordXform.Position.Az.Rad), AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(Units.QtrRev - Math.Abs(initState.IInit.ICoordXform.Site.Latitude.Rad), initState.IInit.ICoordXform.Position.Alt.Rad, AllowedErrorRad)

        'altazimuth alignment, latitude = -45 deg, 6 hr west of meridian
        'altaz should be on horizon facing directly west
        initState.IInit.ICoordXform.Position.RA.Rad = initState.IInit.ICoordXform.Position.SidT.Rad - 6 * Units.HrToRad
        initState.IInit.ICoordXform.Position.Dec.Rad = 0 * Units.DegToRad
        initState.IInit.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(90 * Units.DegToRad, initState.IInit.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(0, initState.IInit.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
    End Sub

    <Test()> Public Sub TestConvertMatrixAltazimuthMeridianFlip()
        ' RA~10deg, Dec~12deg
        Dim desiredDecRad As Double = 0.21498339225721916
        Dim desiredRaRad As Double = 0.17594587901697
        Dim InitState As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Altazimuth, ISFT))
        InitState.ICoordXform.MeridianFlip.Possible = True
        InitState.ICoordXform.Site.Latitude.Rad = 30 * Units.DegToRad
        InitState.IInit.Init()
        InitState.ICoordXform.Position.Az.Rad = 150 * Units.DegToRad
        InitState.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        InitState.ICoordXform.GetEquat()
        NUnit.Framework.Assert.AreEqual(desiredRaRad, InitState.ICoordXform.Position.RA.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(desiredDecRad, InitState.ICoordXform.Position.Dec.Rad, AllowedErrorRad)
        ' now use flipped altaz coordinates
        InitState.ICoordXform.Position.Az.Rad = 330 * Units.DegToRad
        InitState.ICoordXform.Position.Alt.Rad = 110 * Units.DegToRad
        InitState.ICoordXform.MeridianFlip.State = MeridianFlipState.PointingEast
        InitState.ICoordXform.GetEquat()
        NUnit.Framework.Assert.AreEqual(InitState.ICoordXform.Position.RA.Rad - desiredRaRad, InitState.ICoordXform.Position.SidT.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(desiredDecRad, InitState.ICoordXform.Position.Dec.Rad, AllowedErrorRad)
        ' try to get back to flipped altaz coordinates
        InitState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(330 * Units.DegToRad, InitState.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(110 * Units.DegToRad, InitState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        ' try unflipped altaz coordinates
        InitState.ICoordXform.MeridianFlip.State = MeridianFlipState.PointingWest
        InitState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(150 * Units.DegToRad, InitState.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(70 * Units.DegToRad, InitState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
    End Sub

    <Test()> Public Sub TestConvertTrigAltazimuthMeridianFlip()
        ' RA~10deg, Dec~12deg
        Dim desiredDecRad As Double = 0.21498339225721916
        Dim desiredRaRad As Double = 0.17594587901697
        Dim InitState As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertTrig, ISFT), CType(InitStateType.Altazimuth, ISFT))
        InitState.ICoordXform.MeridianFlip.Possible = True
        InitState.ICoordXform.Site.Latitude.Rad = 30 * Units.DegToRad
        InitState.IInit.Init()
        InitState.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        InitState.ICoordXform.Position.Az.Rad = 150 * Units.DegToRad
        InitState.ICoordXform.GetEquat()
        NUnit.Framework.Assert.AreEqual(desiredDecRad, InitState.ICoordXform.Position.Dec.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(desiredRaRad, InitState.ICoordXform.Position.RA.Rad, AllowedErrorRad)
        ' now use flipped altaz coordinates
        InitState.ICoordXform.Position.Alt.Rad = 110 * Units.DegToRad
        InitState.ICoordXform.Position.Az.Rad = 330 * Units.DegToRad
        InitState.ICoordXform.MeridianFlip.State = MeridianFlipState.PointingEast
        InitState.ICoordXform.GetEquat()
        NUnit.Framework.Assert.AreEqual(desiredDecRad, InitState.ICoordXform.Position.Dec.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(desiredRaRad, InitState.ICoordXform.Position.RA.Rad, AllowedErrorRad)
        ' try to get back to flipped altaz coordinates
        InitState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(110 * Units.DegToRad, InitState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(330 * Units.DegToRad, InitState.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        ' try unflipped altaz coordinates
        InitState.ICoordXform.MeridianFlip.State = MeridianFlipState.PointingWest
        InitState.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(70 * Units.DegToRad, InitState.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(150 * Units.DegToRad, InitState.ICoordXform.Position.Az.Rad, AllowedErrorRad)
    End Sub

    <Test()> Public Sub TestHysteresis()
        Dim convert As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Altazimuth, ISFT))
        Dim convertm As ConvertMatrix = CType(convert.ICoordXform, ConvertMatrix)
        convertm.FabErrors.SetFabErrorsDeg(1, -1, 1)
        convertm.Site.Latitude.Rad = 45 * Units.DegToRad
        convertm.Position.SetCoordDeg(79.172, 45.998, 360 - 39.9, 39.9, 39.2 * Units.SidRate / 4)
        convertm.One.Init = True
        convertm.InitMatrix(1)
        convertm.Position.SetCoordDeg(37.96, 89.264, 360 - 94.6, 36.2, 40.3 * Units.SidRate / 4)
        convertm.Two.Init = True
        convertm.InitMatrix(2)
        Dim startAltRad As Double = 80 * Units.DegToRad
        Dim startAzRad As Double = 160 * Units.DegToRad
        Dim variance As Double = 1
        Dim deltaAltArcsec As Double
        Dim deltaAzArcsec As Double

        'hysteresis for TakiSimple delta alt 118.908310653051 arcsec, delta az 43936.3531455043 arcsec
        'hysteresis for TakiSmallAngle delta alt -1528.15763411614 arcsec, delta az 1048.070475014 arcsec
        'hysteresis for BellIterative delta alt 9.15999748261493E-11 arcsec, delta az 0.0704750087293922 arcsec
        'hysteresis for TakiIterative delta alt -9.15999748261493E-11 arcsec, delta az 2.74799924478448E-10 arcsec
        'hysteresis for BellTaki delta alt 9.15999748261493E-11 arcsec, delta az 2.74799924478448E-10 arcsec

        ' if coarser value for faster speed adopted, is
        'hysteresis for BellIterative delta alt 9.15999748261493E-11 arcsec, delta az 0.632005790222984 arcsec

        convertm.ConvertSubrSelect = ConvertSubrType.TakiSimple
        convert.ICoordXform.Position.Alt.Rad = startAltRad
        convert.ICoordXform.Position.Az.Rad = startAzRad
        convert.ICoordXform.GetEquat()
        convert.ICoordXform.GetAltaz()
        deltaAltArcsec = (convertm.Position.Alt.Rad - startAltRad) * Units.RadToArcsec
        deltaAzArcsec = (convertm.Position.Az.Rad - startAzRad) * Units.RadToArcsec
        DebugTrace.WriteLine("hysteresis for " _
                        & convertm.ConvertSubrSelect.Name _
                        & " delta alt " _
                        & deltaAltArcsec _
                        & " arcsec, delta az " _
                        & deltaAzArcsec _
                        & " arcsec")

        convertm.ConvertSubrSelect = ConvertSubrType.TakiSmallAngle
        convert.ICoordXform.Position.Alt.Rad = startAltRad
        convert.ICoordXform.Position.Az.Rad = startAzRad
        convert.ICoordXform.GetEquat()
        convert.ICoordXform.GetAltaz()
        deltaAltArcsec = (convertm.Position.Alt.Rad - startAltRad) * Units.RadToArcsec
        deltaAzArcsec = (convertm.Position.Az.Rad - startAzRad) * Units.RadToArcsec
        DebugTrace.WriteLine("hysteresis for " _
                        & convertm.ConvertSubrSelect.Name _
                        & " delta alt " _
                        & deltaAltArcsec _
                        & " arcsec, delta az " _
                        & deltaAzArcsec _
                        & " arcsec")

        convertm.ConvertSubrSelect = ConvertSubrType.BellIterative
        convert.ICoordXform.Position.Alt.Rad = startAltRad
        convert.ICoordXform.Position.Az.Rad = startAzRad
        convert.ICoordXform.GetEquat()
        convert.ICoordXform.GetAltaz()
        deltaAltArcsec = (convertm.Position.Alt.Rad - startAltRad) * Units.RadToArcsec
        deltaAzArcsec = (convertm.Position.Az.Rad - startAzRad) * Units.RadToArcsec
        DebugTrace.WriteLine("hysteresis for " _
                        & convertm.ConvertSubrSelect.Name _
                        & " delta alt " _
                        & deltaAltArcsec _
                        & " arcsec, delta az " _
                        & deltaAzArcsec _
                        & " arcsec")
        NUnit.Framework.Assert.IsTrue(Math.Abs(deltaAltArcsec) < variance)
        NUnit.Framework.Assert.IsTrue(Math.Abs(deltaAzArcsec) < variance)

        convertm.ConvertSubrSelect = ConvertSubrType.TakiIterative
        convert.ICoordXform.Position.Alt.Rad = startAltRad
        convert.ICoordXform.Position.Az.Rad = startAzRad
        convert.ICoordXform.GetEquat()
        convert.ICoordXform.GetAltaz()
        deltaAltArcsec = (convertm.Position.Alt.Rad - startAltRad) * Units.RadToArcsec
        deltaAzArcsec = (convertm.Position.Az.Rad - startAzRad) * Units.RadToArcsec
        DebugTrace.WriteLine("hysteresis for " _
                        & convertm.ConvertSubrSelect.Name _
                        & " delta alt " _
                        & deltaAltArcsec _
                        & " arcsec, delta az " _
                        & deltaAzArcsec _
                        & " arcsec")
        NUnit.Framework.Assert.IsTrue(Math.Abs(deltaAltArcsec) < variance)
        NUnit.Framework.Assert.IsTrue(Math.Abs(deltaAzArcsec) < variance)

        convertm.ConvertSubrSelect = ConvertSubrType.BellTaki
        convert.ICoordXform.Position.Alt.Rad = startAltRad
        convert.ICoordXform.Position.Az.Rad = startAzRad
        convert.ICoordXform.GetEquat()
        convert.ICoordXform.GetAltaz()
        deltaAltArcsec = (convertm.Position.Alt.Rad - startAltRad) * Units.RadToArcsec
        deltaAzArcsec = (convertm.Position.Az.Rad - startAzRad) * Units.RadToArcsec
        DebugTrace.WriteLine("hysteresis for " _
                        & convertm.ConvertSubrSelect.Name _
                        & " delta alt " _
                        & deltaAltArcsec _
                        & " arcsec, delta az " _
                        & deltaAzArcsec _
                        & " arcsec")
        NUnit.Framework.Assert.IsTrue(Math.Abs(deltaAltArcsec) < variance)
        NUnit.Framework.Assert.IsTrue(Math.Abs(deltaAzArcsec) < variance)
    End Sub

    <Test()> Public Sub CompareMatrixToTrig()
        ' use convertTrig to get altaz and equat coordinates to initialize convertMatrix
        Dim lat As Double = 50 * Units.DegToRad
        Dim convertT As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertTrig, ISFT), CType(InitStateType.Altazimuth, ISFT))
        Dim convertM As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Celestial, ISFT))

        convertT.ICoordXform.Site.Latitude.Rad = lat
        ' Init() not necessary for convertTrig, but call for consistency
        convertT.IInit.Init()

        ' 1st position
        convertT.ICoordXform.Position.Az.Rad = 180 * Units.DegToRad
        convertT.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        CType(convertM.ICoordXform, ConvertMatrix).One.CopyFrom(convertT.ICoordXform.Position)
        CType(convertM.ICoordXform, ConvertMatrix).One.Init = True
        CType(convertM.ICoordXform, ConvertMatrix).One.Available = False

        ' 2nd position
        convertT.ICoordXform.Position.Az.Rad = 100 * Units.DegToRad
        convertT.ICoordXform.Position.Alt.Rad = 40 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        CType(convertM.ICoordXform, ConvertMatrix).Two.CopyFrom(convertT.ICoordXform.Position)
        CType(convertM.ICoordXform, ConvertMatrix).Two.Init = True
        CType(convertM.ICoordXform, ConvertMatrix).Two.Available = False

        convertM.ICoordXform.Site.Latitude.Rad = lat
        convertM.IInit.Init()

        ' now compare using init#2 position just used above
        convertM.ICoordXform.Position.CopyFrom(convertT.ICoordXform.Position)
        convertM.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(100 * Units.DegToRad, convertM.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(40 * Units.DegToRad, convertM.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        ' compare with totally new coordinates 
        convertT.ICoordXform.Position.Az.Rad = 180 * Units.DegToRad
        convertT.ICoordXform.Position.Alt.Rad = 20 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        convertM.ICoordXform.Position.CopyFrom(convertT.ICoordXform.Position)
        convertM.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(180 * Units.DegToRad, convertM.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(20 * Units.DegToRad, convertM.ICoordXform.Position.Alt.Rad, AllowedErrorRad)
        ' compare with totally new coordinates
        convertT.ICoordXform.Position.Az.Rad = 200 * Units.DegToRad
        convertT.ICoordXform.Position.Alt.Rad = 20 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        convertM.ICoordXform.Position.CopyFrom(convertT.ICoordXform.Position)
        convertM.ICoordXform.GetAltaz()
        NUnit.Framework.Assert.AreEqual(200 * Units.DegToRad, convertM.ICoordXform.Position.Az.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(20 * Units.DegToRad, convertM.ICoordXform.Position.Alt.Rad, AllowedErrorRad)

        ' run through great circles
        Dim testAllowedError As Double = Units.ArcsecToRad

        lat = 42 * Units.DegToRad
        convertT.ICoordXform.Site.Latitude.Rad = lat
        convertT.IInit.Init()

        convertM.ICoordXform.Site.Latitude.Rad = lat

        convertT.ICoordXform.Position.Az.Rad = 180 * Units.DegToRad
        convertT.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        CType(convertM.ICoordXform, ConvertMatrix).One.CopyFrom(convertT.ICoordXform.Position)
        CType(convertM.ICoordXform, ConvertMatrix).One.Init = True
        CType(convertM.ICoordXform, ConvertMatrix).One.Available = False

        ' 2nd position
        convertT.ICoordXform.Position.Az.Rad = 100 * Units.DegToRad
        convertT.ICoordXform.Position.Alt.Rad = 40 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        CType(convertM.ICoordXform, ConvertMatrix).Two.CopyFrom(convertT.ICoordXform.Position)
        CType(convertM.ICoordXform, ConvertMatrix).Two.Init = True
        CType(convertM.ICoordXform, ConvertMatrix).Two.Available = False

        convertM.IInit.Init()

        Dim testSidTRad As Double = 3.3 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = testSidTRad
        convertM.ICoordXform.Position.SidT.Rad = testSidTRad

        Dim stepSizeRad As Double = 30 * Units.DegToRad
        For RaRad As Double = 0 To Units.OneRev Step stepSizeRad
            For DecRad As Double = -Units.HalfRev To Units.HalfRev Step stepSizeRad
                Debug.WriteLine("RADeg " & RaRad * Units.RadToDeg & " DecDeg " & DecRad * Units.RadToDeg)
                convertT.ICoordXform.Position.RA.Rad = RaRad
                convertM.ICoordXform.Position.RA.Rad = RaRad
                convertT.ICoordXform.Position.Dec.Rad = DecRad
                convertM.ICoordXform.Position.Dec.Rad = DecRad
                convertT.ICoordXform.GetAltaz()
                convertM.ICoordXform.GetAltaz()
                Debug.WriteLine("'T.AzDeg " & convertT.ICoordXform.Position.Az.Rad * Units.RadToDeg & " = 'M.AzDeg " & convertM.ICoordXform.Position.Az.Rad * Units.RadToDeg & " ?")
                NUnit.Framework.Assert.Less(eMath.ValidRadPi(convertT.ICoordXform.Position.Az.Rad - convertM.ICoordXform.Position.Az.Rad), testAllowedError)
                Debug.WriteLine("'T.AltDeg " & convertT.ICoordXform.Position.Alt.Rad * Units.RadToDeg & " = 'M.AltDeg " & convertM.ICoordXform.Position.Alt.Rad * Units.RadToDeg & " ?")
                NUnit.Framework.Assert.Less(eMath.ValidRadPi(convertT.ICoordXform.Position.Alt.Rad - convertM.ICoordXform.Position.Alt.Rad), testAllowedError)
            Next
        Next
        For AzRad As Double = 0 To Units.OneRev Step stepSizeRad
            For AltRad As Double = -Units.HalfRev To Units.HalfRev Step stepSizeRad
                Debug.WriteLine("AzDeg " & AzRad * Units.RadToDeg & " AltDeg " & AltRad * Units.RadToDeg)
                convertT.ICoordXform.Position.Az.Rad = AzRad
                convertM.ICoordXform.Position.Az.Rad = AzRad
                convertT.ICoordXform.Position.Alt.Rad = AltRad
                convertM.ICoordXform.Position.Alt.Rad = AltRad
                convertT.ICoordXform.GetEquat()
                convertM.ICoordXform.GetEquat()
                Debug.WriteLine("'T.RaDeg " & convertT.ICoordXform.Position.RA.Rad * Units.RadToDeg & " = 'M.RaDeg " & convertM.ICoordXform.Position.RA.Rad * Units.RadToDeg & " ?")
                NUnit.Framework.Assert.Less(eMath.ValidRadPi(convertT.ICoordXform.Position.RA.Rad - convertM.ICoordXform.Position.RA.Rad), testAllowedError)
                Debug.WriteLine("'T.DecDeg " & convertT.ICoordXform.Position.Dec.Rad * Units.RadToDeg & " = 'M.DecDeg " & convertM.ICoordXform.Position.Dec.Rad * Units.RadToDeg & " ?")
                NUnit.Framework.Assert.Less(eMath.ValidRadPi(convertT.ICoordXform.Position.Dec.Rad - convertM.ICoordXform.Position.Dec.Rad), testAllowedError)
            Next
        Next
    End Sub

    <Test()> Public Sub CompareMatrixToTrigVaryingAllParms()
        Dim convertT As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertTrig, ISFT), CType(InitStateType.Altazimuth, ISFT))
        Dim convertM As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Altazimuth, ISFT))

        Dim stepSizeRad As Double = 45 * Units.DegToRad
        For latitudeRad As Double = -Units.QtrRev To Units.QtrRev - stepSizeRad Step stepSizeRad

            convertT.ICoordXform.Site.Latitude.Rad = latitudeRad
            convertT.IInit.Init()
            convertM.ICoordXform.Site.Latitude.Rad = latitudeRad
            convertM.IInit.Init()

            For SidTRad As Double = 0 To Units.OneRev - stepSizeRad Step stepSizeRad

                convertT.ICoordXform.Position.SidT.Rad = SidTRad
                convertM.ICoordXform.Position.SidT.Rad = SidTRad

                For priRad As Double = 0 To Units.OneRev - stepSizeRad Step stepSizeRad
                    For secRad As Double = -Units.HalfRev To Units.HalfRev Step stepSizeRad

                        convertT.ICoordXform.Position.RA.Rad = priRad
                        convertM.ICoordXform.Position.RA.Rad = priRad
                        convertT.ICoordXform.Position.Dec.Rad = secRad
                        convertM.ICoordXform.Position.Dec.Rad = secRad
                        convertT.ICoordXform.GetAltaz()
                        convertM.ICoordXform.GetAltaz()

                        compareTrigToMatrixPositions(convertT.ICoordXform.Position, convertM.ICoordXform.Position, latitudeRad, SidTRad, priRad, secRad)

                        convertT.ICoordXform.Position.Az.Rad = priRad
                        convertM.ICoordXform.Position.Az.Rad = priRad
                        convertT.ICoordXform.Position.Alt.Rad = secRad
                        convertM.ICoordXform.Position.Alt.Rad = secRad
                        convertT.ICoordXform.GetEquat()
                        convertM.ICoordXform.GetEquat()

                        compareTrigToMatrixPositions(convertT.ICoordXform.Position, convertM.ICoordXform.Position, latitudeRad, SidTRad, priRad, secRad)
                    Next
                Next
            Next
        Next

        NUnit.Framework.Assert.IsTrue(True)
    End Sub

    Public Sub compareTrigToMatrixPositions(ByVal trigPosition As Position, ByVal matrixPosition As Position, ByVal latitudeRad As Double, ByVal SidTRad As Double, ByVal priRad As Double, ByVal secRad As Double)
        Dim variance As Double = Units.ArcsecToRad / 100

        matrixPosition.RA.Rad = eMath.ValidRad(matrixPosition.RA.Rad)
        matrixPosition.Az.Rad = eMath.ValidRad(matrixPosition.Az.Rad)
        matrixPosition.Alt.Rad = eMath.ValidRadPi(matrixPosition.Alt.Rad)
        trigPosition.RA.Rad = eMath.ValidRad(trigPosition.RA.Rad)
        trigPosition.Az.Rad = eMath.ValidRad(trigPosition.Az.Rad)
        trigPosition.Alt.Rad = eMath.ValidRadPi(trigPosition.Alt.Rad)

        NUnit.Framework.Assert.AreEqual(matrixPosition.SidT.Rad, trigPosition.SidT.Rad, variance)

        If matrixPosition.Az.Rad.Equals(trigPosition.Az.Rad) _
        AndAlso matrixPosition.Alt.Rad.Equals(trigPosition.Alt.Rad) Then
            NUnit.Framework.Assert.IsTrue(True)
        ElseIf Math.Abs(Math.Abs(matrixPosition.Az.Rad) - Math.Abs(trigPosition.Az.Rad)) < variance _
        AndAlso Math.Abs(Math.Abs(matrixPosition.Alt.Rad) - Math.Abs(trigPosition.Alt.Rad)) < variance Then
            NUnit.Framework.Assert.IsTrue(True)
        Else
            Debug.WriteLine("positions do not agree: latitude " & latitudeRad * Units.RadToDeg & " SidT " & SidTRad * Units.RadToDeg & " az " & priRad * Units.RadToDeg & " alt " & secRad * Units.RadToDeg & ":")
            Debug.WriteLine("    matrixPosition " & matrixPosition.ShowCoordDeg)
            Debug.WriteLine("    trigPosition " & trigPosition.ShowCoordDeg)
            Debug.Write("testing altaz angular separation...")

            Dim angSepRad As Double = pCelestialCoordinateCalcs.CalcAltazAngularSep(matrixPosition, trigPosition)
            Debug.Write(angSepRad * Units.RadToArcsec)
            Debug.Write(BartelsLibrary.Constants.Quote)
            If angSepRad < variance Then
                Debug.WriteLine(String.Empty)
                NUnit.Framework.Assert.IsTrue(True)
            Else
                Debug.WriteLine("angular separation test failed")
                NUnit.Framework.Assert.IsTrue(False)
            End If
        End If

        If matrixPosition.RA.Rad.Equals(trigPosition.RA.Rad) _
        AndAlso matrixPosition.Dec.Rad.Equals(trigPosition.Dec.Rad) Then
            NUnit.Framework.Assert.IsTrue(True)
        ElseIf Math.Abs(Math.Abs(matrixPosition.RA.Rad) - Math.Abs(trigPosition.RA.Rad)) < variance _
        AndAlso Math.Abs(Math.Abs(matrixPosition.Dec.Rad) - Math.Abs(trigPosition.Dec.Rad)) < variance Then
            NUnit.Framework.Assert.IsTrue(True)
        Else
            Debug.WriteLine("positions do not agree: latitude " & latitudeRad * Units.RadToDeg & " SidT " & SidTRad * Units.RadToDeg & " az " & priRad * Units.RadToDeg & " alt " & secRad * Units.RadToDeg & ":")
            Debug.WriteLine("    matrixPosition " & matrixPosition.ShowCoordDeg)
            Debug.WriteLine("    trigPosition " & trigPosition.ShowCoordDeg)
            Debug.Write("testing equat angular separation...")

            Dim angSepRad As Double = pCelestialCoordinateCalcs.CalcEquatAngularSepViaRa(matrixPosition, trigPosition)
            Debug.Write(angSepRad * Units.RadToArcsec)
            Debug.Write(BartelsLibrary.Constants.Quote)
            If angSepRad < variance Then
                Debug.WriteLine(String.Empty)
                NUnit.Framework.Assert.IsTrue(True)
            Else
                Debug.WriteLine("angular separation test failed")
                NUnit.Framework.Assert.IsTrue(False)
            End If
        End If
    End Sub

    <Test()> Public Sub TestPostInitCalcsLatitude()
        ' use convertTrig to get altaz and equat coordinates to initialize convertMatrix with,
        ' then compare convertTrig's given latitude with convertMatrix's calculated latitudes
        Dim lat As Double = 50 * Units.DegToRad
        Dim convertT As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertTrig, ISFT), CType(InitStateType.Altazimuth, ISFT))
        Dim convertM As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Celestial, ISFT))

        convertT.ICoordXform.Site.Latitude.Rad = lat
        ' Init() not necessary for convertTrig, but call for consistency
        convertT.IInit.Init()

        ' 1st position
        convertT.ICoordXform.Position.Az.Rad = 180 * Units.DegToRad
        convertT.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        CType(convertM.ICoordXform, ConvertMatrix).One.CopyFrom(convertT.ICoordXform.Position)
        CType(convertM.ICoordXform, ConvertMatrix).One.Init = True
        CType(convertM.ICoordXform, ConvertMatrix).One.Available = False

        ' 2nd position
        convertT.ICoordXform.Position.Az.Rad = 100 * Units.DegToRad
        convertT.ICoordXform.Position.Alt.Rad = 40 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        CType(convertM.ICoordXform, ConvertMatrix).Two.CopyFrom(convertT.ICoordXform.Position)
        CType(convertM.ICoordXform, ConvertMatrix).Two.Init = True
        CType(convertM.ICoordXform, ConvertMatrix).Two.Available = False

        convertM.ICoordXform.Site.Latitude.Rad = lat
        convertM.IInit.Init()

        Dim postInitCalcs As PostInitCalcs = Coordinates.PostInitCalcs.GetInstance
        postInitCalcs.ICoordXform = convertM.ICoordXform
        postInitCalcs.UpdatePostInitVars = True
        postInitCalcs.CheckForPostInitVars()

        NUnit.Framework.Assert.AreEqual(lat, postInitCalcs.LatitudeBasedOnScopeAtEquatPole.Rad, AllowedErrorRad)
        NUnit.Framework.Assert.AreEqual(lat, postInitCalcs.LatitudeBasedOnScopeAtScopePole.Rad, AllowedErrorRad)
    End Sub

    <Test()> Public Sub TestIterativeLatitude()
        ' use convertTrig to get altaz and equat coordinates to initialize convertMatrix with,
        ' then compare convertTrig's given latitude with convertMatrix's calculated latitudes
        Dim lat As Double = 50 * Units.DegToRad
        Dim convertT As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertTrig, ISFT), CType(InitStateType.Altazimuth, ISFT))

        convertT.ICoordXform.Site.Latitude.Rad = lat
        ' Init() not necessary for convertTrig, but call for consistency
        convertT.IInit.Init()

        ' 1st position
        convertT.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        convertT.ICoordXform.Position.Az.Rad = 250 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = 0
        convertT.ICoordXform.GetEquat()
        Dim tempPosition1 As Position = Position.GetInstance
        tempPosition1.CopyFrom(convertT.ICoordXform.Position)

        ' 2nd position
        convertT.ICoordXform.Position.Alt.Rad = 40 * Units.DegToRad
        convertT.ICoordXform.Position.Az.Rad = 40 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = Units.HrToRad
        convertT.ICoordXform.GetEquat()
        Dim tempPosition2 As Position = Position.GetInstance
        tempPosition2.CopyFrom(convertT.ICoordXform.Position)

        Dim iterLat As IterativeLatitude = IterativeLatitude.GetInstance
        iterLat.Calc(tempPosition1, tempPosition2)
        NUnit.Framework.Assert.AreEqual(iterLat.Latitude.Rad, lat, Units.RadToArcsec)

        ' southern hemisphere

        convertT.ICoordXform.Site.Latitude.Rad = -lat
        ' 1st position
        convertT.ICoordXform.Position.Alt.Rad = 70 * Units.DegToRad
        convertT.ICoordXform.Position.Az.Rad = 250 * Units.DegToRad
        convertT.ICoordXform.GetEquat()
        tempPosition1 = Position.GetInstance
        tempPosition1.CopyFrom(convertT.ICoordXform.Position)
        ' 2nd position
        convertT.ICoordXform.Position.Alt.Rad = 40 * Units.DegToRad
        convertT.ICoordXform.Position.Az.Rad = 40 * Units.DegToRad
        convertT.ICoordXform.Position.SidT.Rad = Units.RadToHr
        convertT.ICoordXform.GetEquat()
        tempPosition2 = Position.GetInstance
        tempPosition2.CopyFrom(convertT.ICoordXform.Position)
        ' get lat
        iterLat.Calc(tempPosition1, tempPosition2)
        NUnit.Framework.Assert.AreEqual(iterLat.Latitude.Rad, -lat, Units.RadToArcsec)
    End Sub

    <TearDown()> Public Sub Dispose()
        pCelestialCoordinateCalcs = Nothing

        pConvertTrig = Nothing
        pConvertMatrix = Nothing
    End Sub
End Class
