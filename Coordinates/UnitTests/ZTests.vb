Imports NUnit.Framework

<TestFixture()> Public Class ZTests

    Private Const AllowedErrorRad As Double = Units.ArcsecToRad / 1000

    Private pConvertTrig As ConvertTrig
    Private pConvertMatrix As ConvertMatrix

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        pConvertTrig = ConvertTrig.GetInstance
        pConvertMatrix = ConvertMatrix.GetInstance
    End Sub

    <Test()> Public Sub TestAltOffset()
        Dim startAltOffsetDeg As Double = 1
        Dim ICoordXform As ICoordXform = ConvertMatrix.GetInstance
        CType(ICoordXform, ConvertMatrix).One.SetCoordDeg(72.1, 20.4333, 210.287, 63.785 + startAltOffsetDeg, 0)
        CType(ICoordXform, ConvertMatrix).Two.SetCoordDeg(359.138, 2.5833, 269.58, 4.164 + startAltOffsetDeg, 0)
        ' not necessary to Init() to calc AltOffset: just need 2 positions w/equat + altaz coord
        Dim AltOffset As AltOffset = Coordinates.AltOffset.GetInstance
        Dim variance As Double = Units.ArcminToDeg
        Dim calcAltOffsetDeg As Double = -AltOffset.CalcAltOffsetDirectly(CType(ICoordXform, ConvertMatrix).One, CType(ICoordXform, ConvertMatrix).Two).Rad * Units.RadToDeg
        Assert.IsTrue(startAltOffsetDeg >= calcAltOffsetDeg - variance AndAlso startAltOffsetDeg <= calcAltOffsetDeg + variance)
        calcAltOffsetDeg = -AltOffset.CalcAltOffsetIteratively(CType(ICoordXform, ConvertMatrix).One, CType(ICoordXform, ConvertMatrix).Two).Rad * Units.RadToDeg
        Assert.IsTrue(startAltOffsetDeg >= calcAltOffsetDeg - variance AndAlso startAltOffsetDeg <= calcAltOffsetDeg + variance)
    End Sub

    <Test()> Public Sub TestBestZ123()
        Dim convert As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Celestial, ISFT))
        Dim convertm As ConvertMatrix = CType(convert.ICoordXform, ConvertMatrix)
        convertm.FabErrors.SetFabErrorsDeg(0, 0, 0)

        'convertm.Position.SetCoordDeg(79.172, 45.998, 360 - 39.9, 39.9, 39.2 * Units.SidRate / 4)
        'convertm.One.Init = True
        'convertm.InitMatrix(1)
        'convertm.Position.SetCoordDeg(37.96, 89.264, 360 - 94.6, 36.2, 40.3 * Units.SidRate / 4)
        'convertm.Two.Init = True
        'convertm.InitMatrix(2)
        ' same as above commented code
        convertm.One.SetCoordDeg(79.172, 45.998, 360 - 39.9, 39.9, 39.2 * Units.SidRate / 4)
        convertm.One.Init = True
        convertm.Two.SetCoordDeg(37.96, 89.264, 360 - 94.6, 36.2, 40.3 * Units.SidRate / 4)
        convertm.Two.Init = True
        convert.IInit.Init()

        Dim positionArray As PositionArray = Coordinates.PositionArray.GetInstance
        positionArray.IPositionArrayIO = PositionArrayIODegreeDelimited.GetInstance
        Dim filename As String = "scope.analysis.taki.txt"
        positionArray.Import(filename)

        Dim bestZ123 As BestZ123 = Coordinates.BestZ123.GetInstance
        bestZ123.BestZ123FromPositionArray(convertm, positionArray)

        ' from Taki: z1=-.04, z2=.4, z3=-1.63
        Assert.AreEqual(-1.63981150794351, bestZ123.BestZ3 * Units.RadToDeg)
        Assert.AreEqual(-0.030000000000001453, bestZ123.BestZ1 * Units.RadToDeg)
        Assert.AreEqual(0.40499999999999858, bestZ123.BestZ2 * Units.RadToDeg)
    End Sub

    <Test()> Public Sub TestZ12CompareTest()
        Dim z12CompareTest As Z12CompareTest = Coordinates.Z12CompareTest.GetInstance
        Dim z1Rad As Double = 0.5 * Units.DegToRad
        Dim z2Rad As Double = -0.707 * Units.DegToRad
        Dim latitudeRad As Double = 45 * Units.DegToRad
        Dim azimuthRad As Double = 180 * Units.DegToRad

        z12CompareTest.GenerateValues(z1Rad, 0, latitudeRad, azimuthRad)
        Dim filename As String = "TestZ12CompareA.txt"
        Assert.IsTrue(z12CompareTest.WriteCVSToFile(filename, ", "))
        Assert.IsTrue(IO.File.Exists(filename))

        z12CompareTest.GenerateValues(0, z2Rad, latitudeRad, azimuthRad)
        filename = "TestZ12CompareB.txt"
        Assert.IsTrue(z12CompareTest.WriteCVSToFile(filename, ", "))
        Assert.IsTrue(IO.File.Exists(filename))
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
