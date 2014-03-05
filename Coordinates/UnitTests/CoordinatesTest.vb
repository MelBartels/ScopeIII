Imports System.IO
Imports NUnit.Framework

<TestFixture()> Public Class CoordinatesTest
    Dim pTestRad As Double

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestPositionArray()
        Dim positionArray As PositionArray = Coordinates.PositionArray.GetInstance
        Dim position1 As Position = positionArray.GetPosition
        Dim position2 As Position = positionArray.GetPosition("3")
        position1.Available = True
        Dim position3 As Position = positionArray.GetPosition
        Assert.AreSame(position1, position3)
        Assert.IsTrue(position2.PosName.Equals("3"))
    End Sub

    <Test()> Public Sub TestPositionArraySingleton()
        Dim positionArray As PositionArraySingleton = PositionArraySingleton.GetInstance
        Dim position1 As Position = positionArray.GetPosition
        Dim position2 As Position = positionArray.GetPosition("2")
        position1.Available = True
        Dim position3 As Position = positionArray.GetPosition
        Assert.AreSame(position1, position3)
        Assert.IsTrue(position2.PosName.Equals("2"))
    End Sub

    <Test()> Public Sub TestPositionArrayIOSerialize()
        Dim positionArray As PositionArray = Coordinates.PositionArray.GetInstance
        Dim position1 As Position = positionArray.GetPosition
        Dim position2 As Position = positionArray.GetPosition("3")
        position1.Available = True
        Dim position3 As Position = positionArray.GetPosition
        positionArray.IPositionArrayIO = PositionArrayIoSerialize.GetInstance
        Dim filename As String = "testPositionArrayIO.soap.xml"
        positionArray.Export(filename)
        Assert.IsTrue(File.Exists(filename))
        positionArray.Import(filename)
        ' 1 of the 3 positions above is marked available, so 3rd position reuses 2nd position for total of 2
        Assert.AreEqual(2, positionArray.PositionArray.Count)
    End Sub

    <Test()> Public Sub TestPositionArrayIODegreeDelimited()
        Dim positionArray As PositionArray = Coordinates.PositionArray.GetInstance
        positionArray.IPositionArrayIO = PositionArrayIODegreeDelimited.GetInstance
        Dim filename As String = "scope.analysis.taki.txt"
        positionArray.Import(filename)
        Assert.AreEqual(6, positionArray.PositionArray.Count)
    End Sub

    <Test()> Public Sub TestPositionArrayIODatafile()
        Dim positionArray As PositionArray = Coordinates.PositionArray.GetInstance
        positionArray.IPositionArrayIO = PositionArrayIODatafile.GetInstance
        Dim filename As String = "bstars.dat"
        positionArray.Import(filename)
        ' 4th record:  05 14 45   -08 11 48   b.Ori-Rigel
        Dim position As position = CType(positionArray.PositionArray.Item(3), position)
        Dim testRA As Double = 5 * Units.HrToRad + 14 * Units.MinToRad + 45 * Units.SecToRad
        Dim testDec As Double = 8 * Units.DegToRad + 11 * Units.ArcminToRad + 48 * Units.ArcsecToRad
        testDec = -testDec
        Assert.AreEqual(testRA, position.RA.Rad)
        Assert.AreEqual(testDec, position.Dec.Rad)
        Assert.IsTrue("b.Ori-Rigel".Equals(position.ObjName))
    End Sub

    <Test()> Public Sub TestPositionArrayIODatafileNegDec()
        Dim positionArray As PositionArray = Coordinates.PositionArray.GetInstance
        positionArray.IPositionArrayIO = PositionArrayIODatafile.GetInstance
        Dim filename As String = "negDecTest.dat"
        positionArray.Import(filename)
        ' dec should = -00 31 36
        Dim testDec As Double = -31 * Units.ArcminToRad + -36 * Units.ArcsecToRad
        For Each position As position In positionArray.PositionArray
            Assert.AreEqual(testDec, position.Dec.Rad)
        Next
    End Sub

    <Test()> Public Sub TestPositionArrayIODegreeDelimitedObjName()
        Dim positionArray As PositionArray = Coordinates.PositionArray.GetInstance
        Dim position As position = positionArray.GetPosition
        position.RA.Rad = 1.1
        position.Dec.Rad = 2.2
        Dim testAlt As Double = -3.3
        position.Alt.Rad = testAlt
        Dim testAz As Double = 2.8
        position.Az.Rad = testAz
        position.SidT.Rad = 1.8
        Dim testObjName As String = "this is my object name"
        position.ObjName = testObjName
        positionArray.IPositionArrayIO = PositionArrayIODegreeDelimited.GetInstance
        Dim filename As String = "TestPositionArrayIODegreeDelimitedObjName.txt"
        positionArray.Export(filename)
        Assert.IsTrue(File.Exists(filename))

        positionArray = Coordinates.PositionArray.GetInstance
        positionArray.IPositionArrayIO = PositionArrayIODegreeDelimited.GetInstance
        positionArray.Import(filename)
        position = CType(positionArray.PositionArray.Item(0), position)
        Assert.IsTrue(testObjName.Equals(position.ObjName))
        Dim variance As Double = Units.MilliDegToRad
        Assert.AreEqual(testAlt, position.Alt.Rad, variance)
        Assert.AreEqual(testAz, position.Az.Rad, variance)
    End Sub

    <Test()> Public Sub TestPositionCopy()
        Dim positionArray As PositionArraySingleton = PositionArraySingleton.GetInstance
        Dim position1 As Position = positionArray.GetPosition
        Dim position2 As Position = positionArray.GetPosition
        position1.RA.Rad = 1
        position2.CopyFrom(position1)
        position1.RA.Rad = 0
        Assert.IsTrue(position1.RA.Rad.Equals(0.0))
        Assert.IsTrue(position2.RA.Rad.Equals(1.0))
    End Sub

    <Test()> Public Sub TestSetPositionCoordExp()
        Dim position As Position = Coordinates.Position.GetInstance
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        position.RA.Rad = pTestRad
        Dim radian As ISFT = CType(CoordExpType.Radian, ISFT)
        Dim degree As ISFT = CType(CoordExpType.Degree, ISFT)
        Assert.IsTrue(position.RA.ToString(radian).Equals("0.175997062516384"))
        Assert.IsTrue(position.RA.ToString(degree).Equals(" 10.0839"))
        position.RA.Rad = -position.RA.Rad
        Assert.IsTrue(position.RA.ToString(radian).Equals("-0.175997062516384"))
        Assert.IsTrue(position.RA.ToString(degree).Equals("-10.0839"))
    End Sub

    <Test()> Public Sub TestCommonCalcRad()
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Assert.AreEqual(pTestRad, CommonCalcs.GetInstance.CalcRadDMS(10, 5, 2))
        Assert.AreEqual(-pTestRad, CommonCalcs.GetInstance.CalcRadDMS(-10, 5, 2))
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17 * Units.SecToRad
        Assert.AreEqual(pTestRad, CommonCalcs.GetInstance.CalcRadHMS(5, 6, 17))
        Assert.AreEqual(-pTestRad, CommonCalcs.GetInstance.CalcRadHMS(-5, 6, 17))
    End Sub

    <Test()> Public Sub TestCommonCalcParse()
        Dim st As StringTokenizer = StringTokenizer.GetInstance

        Dim testStr As String = " 1 12 +30 1 12 30 "
        st.Tokenize(testStr)
        Assert.AreEqual(0.021089395128264816, CommonCalcs.GetInstance.ParseCoordinateValueDec(st))
        Assert.AreEqual(0.3163409269239722, CommonCalcs.GetInstance.ParseCoordinateValueRA(st))

        testStr = " +1.2   - 1.2"
        st.Tokenize(testStr)
        Assert.AreEqual(0.020943951023931952, CommonCalcs.GetInstance.ParseCoordinateValueDeg(st))
        Assert.AreEqual(-1.2, CommonCalcs.GetInstance.ParseCoordinateValueRad(st))
    End Sub

    <Test()> Public Sub TestCoordinateParser()
        Dim testStr As String

        ' should decode to degrees
        testStr = " 1 12 +30"
        Assert.AreEqual(0.021089395128264816, CoordinateParser.GetInstance.Parse(testStr))
        testStr = " -1 12 +30"
        Assert.AreEqual(-0.021089395128264816, CoordinateParser.GetInstance.Parse(testStr))

        ' test to deduce coordinate type
        testStr = " 1h 12 +30"
        Assert.AreEqual(0.3163409269239722, CoordinateParser.GetInstance.Parse(testStr))
        testStr = " 1 12 30 hours"
        Assert.AreEqual(0.3163409269239722, CoordinateParser.GetInstance.Parse(testStr))

        ' test for 2 numbers that should decode to dec of 1.2 : 12 : 0, where deg of 1.2 is double
        testStr = " + 1.2 12"
        Assert.AreEqual(0.024434609527920613, CoordinateParser.GetInstance.Parse(testStr))
        testStr = "1.2:12"
        Assert.AreEqual(0.024434609527920613, CoordinateParser.GetInstance.Parse(testStr))
        ' and the negatives
        testStr = "-1.2:12"
        Assert.AreEqual(-0.024434609527920613, CoordinateParser.GetInstance.Parse(testStr))
        testStr = "- 1.2:12"
        Assert.AreEqual(-0.024434609527920613, CoordinateParser.GetInstance.Parse(testStr))

        ' test to deduce deg 
        testStr = " + 1.2d"
        Assert.AreEqual(0.020943951023931952, CoordinateParser.GetInstance.Parse(testStr))
        testStr = " - 1.2d"
        Assert.AreEqual(-0.020943951023931952, CoordinateParser.GetInstance.Parse(testStr))

        ' test to deduce rad
        testStr = " - 1.2ra"
        Assert.AreEqual(-1.2, CoordinateParser.GetInstance.Parse(testStr))

        ' test to deduce default (deg)
        testStr = " + 1.2"
        Assert.AreEqual(0.020943951023931952, CoordinateParser.GetInstance.Parse(testStr))
        testStr = " - 1.2"
        Assert.AreEqual(-0.020943951023931952, CoordinateParser.GetInstance.Parse(testStr))

        'GoogleSky
        testStr = "00h42m44.30s"
        Dim expected As Double = (0 * 3600 + 42 * 60 + 44.3) * Units.SecToRad
        Assert.AreEqual(expected, CoordinateParser.GetInstance.Parse(testStr))
        testStr = "+41°16'10.0"""
        expected = (41 * 3600 + 16 * 60 + 10.0) * Units.ArcsecToRad
        Assert.AreEqual(expected, CoordinateParser.GetInstance.Parse(testStr))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestCoordExpRadian()
        Dim coordExp As ICoordExp = Radian.GetInstance
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("0.175997062516384"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-0.175997062516384"))
    End Sub

    <Test()> Public Sub TestCoordExpDegree()
        Dim coordExp As ICoordExp = Degree.GetInstance
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(" 10.0839"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-10.0839"))
    End Sub

    <Test()> Public Sub TestCoordExpWholeNumDegree()
        Dim coordExp As ICoordExp = wholeNumDegree.GetInstance
        pTestRad = 100 * Units.DegToRad + 5 * Units.ArcminToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("100d"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("260d"))
    End Sub

    <Test()> Public Sub TestCoordExpWholeNumNegDegree()
        Dim coordExp As ICoordExp = WholeNumNegDegree.GetInstance
        pTestRad = 100 * Units.DegToRad + 5 * Units.ArcminToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("100d"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-100d"))
    End Sub

    <Test()> Public Sub TestCoordExpWholeNumHour()
        Dim coordExp As ICoordExp = WholeNumHour.GetInstance
        pTestRad = 150 * Units.DegToRad + 5 * Units.ArcminToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("10h"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("14h"))
    End Sub

    <Test()> Public Sub TestCoordExpWholeNegNumHour()
        Dim coordExp As ICoordExp = WholeNumNegHour.GetInstance
        pTestRad = 150 * Units.DegToRad + 5 * Units.ArcminToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("10h"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-10h"))
    End Sub

    <Test()> Public Sub TestCoordExpDMS()
        Dim coordExp As ICoordExp = DMS.GetInstance
        pTestRad = 11 * Units.DegToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+11:00:00"))
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+10:05:02"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+09:59:59"))
        pTestRad = -9 * Units.DegToRad + -59 * Units.ArcminToRad + -59 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-09:59:59"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59.9 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+10:00:00"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59.4 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+09:59:59"))
        pTestRad = 0 * Units.DegToRad + -5 * Units.ArcminToRad + -2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-00:05:02"))
    End Sub

    <Test()> Public Sub TestCoordExpHMS()
        Dim coordExp As ICoordExp = HMS.GetInstance
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("05:06:17"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-05:06:17"))
        pTestRad = 0 * Units.HrToRad + -5 * Units.MinToRad + -2 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-00:05:02"))
    End Sub

    <Test()> Public Sub TestCoordExpFormattedDegree()
        Dim coordExp As ICoordExp = FormattedDegree.GetInstance
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(" 10.0839 d"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-10.0839 d"))
    End Sub

    <Test()> Public Sub TestCoordExpFormattedDMS()
        Dim coordExp As ICoordExp = FormattedDMS.GetInstance
        pTestRad = 11 * Units.DegToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+11d 00m 00s"))
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+10d 05m 02s"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+09d 59m 59s"))
        pTestRad = -9 * Units.DegToRad + -59 * Units.ArcminToRad + -59 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-09d 59m 59s"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59.9 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+10d 00m 00s"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59.4 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+09d 59m 59s"))
        pTestRad = 0 * Units.DegToRad + -5 * Units.ArcminToRad + -2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-00d 05m 02s"))
    End Sub

    <Test()> Public Sub TestCoordExpFormattedHMS()
        Dim coordExp As ICoordExp = FormattedHMS.GetInstance
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("05h 06m 17s"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-05h 06m 17s"))
        pTestRad = 0 * Units.HrToRad + -5 * Units.MinToRad + -2 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-00h 05m 02s"))
    End Sub

    <Test()> Public Sub TestCoordExpFormattedHMSM()
        Dim coordExp As ICoordExp = FormattedHMSM.GetInstance
        ' test milliSec formatting and rounding
        pTestRad = 5 * Units.HrToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("05h 00m 00.000s"))
        pTestRad = -5 * Units.HrToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-05h 00m 00.000s"))
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17.5 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("05h 06m 17.500s"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-05h 06m 17.500s"))
        pTestRad = 0 * Units.HrToRad + -5 * Units.MinToRad + -2 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-00h 05m 02.000s"))
    End Sub

    <Test()> Public Sub TestCoordExpHMSM()
        Dim coordExp As ICoordExp = HMSM.GetInstance
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17 * Units.SecToRad + 45 * Units.MilliSecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("05:06:17.045"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-05:06:17.045"))
        pTestRad = 0 * Units.HrToRad + -5 * Units.MinToRad + -2 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-00:05:02.000"))
    End Sub

    <Test()> Public Sub TestCoordExpDatafileDMS()
        Dim coordExp As ICoordExp = DatafileDMS.GetInstance
        pTestRad = 11 * Units.DegToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+11 00 00"))
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+10 05 02"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+09 59 59"))
        pTestRad = -9 * Units.DegToRad + -59 * Units.ArcminToRad + -59 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-09 59 59"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59.9 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+10 00 00"))
        pTestRad = 9 * Units.DegToRad + 59 * Units.ArcminToRad + 59.4 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("+09 59 59"))
        pTestRad = 0 * Units.DegToRad + -5 * Units.ArcminToRad + -2 * Units.ArcsecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-00 05 02"))
    End Sub

    <Test()> Public Sub TestCoordExpDatafileHMS()
        Dim coordExp As ICoordExp = DatafileHMS.GetInstance
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("05 06 17"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("-05 06 17"))
        pTestRad = 0 * Units.HrToRad + -5 * Units.MinToRad + -2 * Units.SecToRad
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("-00 05 02"))
    End Sub

    <Test()> Public Sub TestCoordExpLX200SignedLongDeg()
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Dim coordExp As ICoordExp = LX200SignedLongDeg.GetInstance()
        Dim testResult As String = "+10" & CoordExpBase.LX200DegSym & "05:02#"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
        pTestRad = -10 * Units.DegToRad + -5 * Units.ArcminToRad + -2 * Units.ArcsecToRad
        testResult = "-10" & CoordExpBase.LX200DegSym & "05:02#"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
    End Sub

    <Test()> Public Sub TestCoordExpLX200SignedShortDeg()
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 30 * Units.ArcsecToRad
        Dim coordExp As ICoordExp = LX200SignedShortDeg.GetInstance()
        Dim testResult As String = "+10" & CoordExpBase.LX200DegSym & "05.5#"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
        testResult = "-10" & CoordExpBase.LX200DegSym & "05.5#"
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals(testResult))
    End Sub

    <Test()> Public Sub TestCoordExpLX200LongDeg()
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Dim coordExp As ICoordExp = LX200LongDeg.GetInstance()
        Dim testResult As String = "010" & CoordExpBase.LX200DegSym & "05:02#"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals(testResult))
    End Sub

    <Test()> Public Sub TestCoordExpLX200ShortDeg()
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 30 * Units.ArcsecToRad
        Dim coordExp As ICoordExp = LX200ShortDeg.GetInstance()
        Dim testResult As String = "010" & CoordExpBase.LX200DegSym & "05.5#"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals(testResult))
    End Sub

    <Test()> Public Sub TestCoordExpLX200LongHr()
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17 * Units.SecToRad
        Dim coordExp As ICoordExp = LX200LongHr.GetInstance
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("05:06:17#"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("05:06:17#"))
    End Sub

    <Test()> Public Sub TestCoordExpLX200ShortHr()
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17 * Units.SecToRad
        Dim coordExp As ICoordExp = LX200ShortHr.GetInstance
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals("05:06#"))
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals("05:06#"))
    End Sub

    <Test()> Public Sub TestCoordExpXmlHMSM()
        pTestRad = 5 * Units.HrToRad + 6 * Units.MinToRad + 17 * Units.SecToRad
        Dim coordExp As ICoordExp = XmlHMSM.GetInstance
        Dim testResult As String = "<RightAscension><Sign>+</Sign><Hours>5</Hours><Minutes>6</Minutes><Seconds>16.9999999999989</Seconds></RightAscension>"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
        testResult = "<RightAscension><Sign>-</Sign><Hours>5</Hours><Minutes>6</Minutes><Seconds>16.9999999999989</Seconds></RightAscension>"
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals(testResult))
    End Sub

    <Test()> Public Sub TestCoordExpXmlDMS()
        pTestRad = 10 * Units.DegToRad + 5 * Units.ArcminToRad + 2 * Units.ArcsecToRad
        Dim coordExp As ICoordExp = XmlDMS.GetInstance()
        Dim testResult As String = "<Declination><Sign>+</Sign><Degrees>10</Degrees><Minutes>5</Minutes><Seconds>2</Seconds></Declination>"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
        testResult = "<Declination><Sign>-</Sign><Degrees>10</Degrees><Minutes>5</Minutes><Seconds>2</Seconds></Declination>"
        Assert.IsTrue(coordExp.ToString(-pTestRad).Equals(testResult))
    End Sub

    <Test()> Public Sub TestCoordExpDMSSymbols()
        pTestRad = 8 * Units.DegToRad + 12 * Units.ArcminToRad + 16.2 * Units.ArcsecToRad
        Dim coordExp As ICoordExp = DMSSymbols.GetInstance()
        Dim testResult As String = "+08°12'16.2"""
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
        pTestRad = -8 * Units.DegToRad + -12 * Units.ArcminToRad + -16.2 * Units.ArcsecToRad
        testResult = "-08°12'16.2"""
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
    End Sub

    <Test()> Public Sub TestAirMass()
        Dim coordExp As ICoordExp = AirMass.GetInstance()
        pTestRad = 5
        Dim testResult As String = "5"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
        pTestRad = 1.2345
        testResult = "1.23"
        Assert.IsTrue(coordExp.ToString(pTestRad).Equals(testResult))
    End Sub

    <Test()> Public Sub TestCoordParser()
        Dim coordParser As CoordinateParser = CoordinateParser.GetInstance
        Dim testStr As String
        testStr = "2h5m6s"
        Assert.AreEqual(0.54585172356122647, coordParser.Parse(testStr))
        testStr = "2 5 6 h"
        Assert.AreEqual(0.54585172356122647, coordParser.Parse(testStr))
        testStr = "2d5m6s"
        Assert.AreEqual(0.036390114904081769, coordParser.Parse(testStr))
        testStr = "2 5 6"
        Assert.AreEqual(0.036390114904081769, coordParser.Parse(testStr))
        testStr = "2:5:6"
        Assert.AreEqual(0.036390114904081769, coordParser.Parse(testStr))
        testStr = "02:05:06"
        Assert.AreEqual(0.036390114904081769, coordParser.Parse(testStr))
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
