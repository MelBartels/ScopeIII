Imports NUnit.Framework

<TestFixture()> Public Class TestEMath
    ' repeated operations with Pi accumulate error from the last digit of Math.PI
    Private Const PiVariance As Double = 0.000000000000001

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestResolveNumToPrecision()
        Dim num As Double = 3.6
        Dim precision As Double = 1 / 16

        Assert.AreEqual(3.625, eMath.ResolveNumToPrecision(num, precision))
    End Sub

    <Test()> Public Sub TestWholeNum()
        ' goes to next lowest integer
        Assert.AreEqual(3.0, Math.Floor(3.1))
        Assert.AreEqual(3.0, Math.Floor(3.5))
        Assert.AreEqual(3.0, Math.Floor(3.9))
        Assert.AreEqual(-4.0, Math.Floor(-3.1))
        Assert.AreEqual(-4.0, Math.Floor(-3.5))
        Assert.AreEqual(-4.0, Math.Floor(-3.9))
        Assert.AreEqual(4.0, Math.Floor(4.1))
        Assert.AreEqual(4.0, Math.Floor(4.5))
        Assert.AreEqual(4.0, Math.Floor(4.9))
        Assert.AreEqual(-5.0, Math.Floor(-4.1))
        Assert.AreEqual(-5.0, Math.Floor(-4.5))
        Assert.AreEqual(-5.0, Math.Floor(-4.9))

        ' baker's rounding (to nearest even number)
        Assert.AreEqual(3, CInt(3.1))
        Assert.AreEqual(4, CInt(3.5))
        Assert.AreEqual(4, CInt(3.9))
        Assert.AreEqual(-3, CInt(-3.1))
        Assert.AreEqual(-4, CInt(-3.5))
        Assert.AreEqual(-4, CInt(-3.9))
        Assert.AreEqual(4, CInt(4.1))
        Assert.AreEqual(4, CInt(4.5))
        Assert.AreEqual(5, CInt(4.9))
        Assert.AreEqual(-4, CInt(-4.1))
        Assert.AreEqual(-4, CInt(-4.5))
        Assert.AreEqual(-5, CInt(-4.9))

        ' drops fractional portion
        Assert.AreEqual(3, Fix(3.1))
        Assert.AreEqual(3, Fix(3.5))
        Assert.AreEqual(3, Fix(3.9))
        Assert.AreEqual(-3, Fix(-3.1))
        Assert.AreEqual(-3, Fix(-3.5))
        Assert.AreEqual(-3, Fix(-3.9))
        Assert.AreEqual(4, Fix(4.1))
        Assert.AreEqual(4, Fix(4.5))
        Assert.AreEqual(4, Fix(4.9))
        Assert.AreEqual(-4, Fix(-4.1))
        Assert.AreEqual(-4, Fix(-4.5))
        Assert.AreEqual(-4, Fix(-4.9))

        ' like Fix but takes the negative numbers down 1
        Assert.AreEqual(3, Int(3.1))
        Assert.AreEqual(3, Int(3.5))
        Assert.AreEqual(3, Int(3.9))
        Assert.AreEqual(-4, Int(-3.1))
        Assert.AreEqual(-4, Int(-3.5))
        Assert.AreEqual(-4, Int(-3.9))
        Assert.AreEqual(4, Int(4.1))
        Assert.AreEqual(4, Int(4.5))
        Assert.AreEqual(4, Int(4.9))
        Assert.AreEqual(-5, Int(-4.1))
        Assert.AreEqual(-5, Int(-4.5))
        Assert.AreEqual(-5, Int(-4.9))

        ' baker's rounding (to nearest even number)
        Assert.AreEqual(3, Convert.ToInt32(3.1))
        Assert.AreEqual(4, Convert.ToInt32(3.5))
        Assert.AreEqual(4, Convert.ToInt32(3.9))
        Assert.AreEqual(-3, Convert.ToInt32(-3.1))
        Assert.AreEqual(-4, Convert.ToInt32(-3.5))
        Assert.AreEqual(-4, Convert.ToInt32(-3.9))
        Assert.AreEqual(4, Convert.ToInt32(4.1))
        Assert.AreEqual(4, Convert.ToInt32(4.5))
        Assert.AreEqual(5, Convert.ToInt32(4.9))
        Assert.AreEqual(-4, Convert.ToInt32(-4.1))
        Assert.AreEqual(-4, Convert.ToInt32(-4.5))
        Assert.AreEqual(-5, Convert.ToInt32(-4.9))
    End Sub

    <Test()> Public Sub TestRInt()
        Assert.AreEqual(3, eMath.RInt(3.1))
        Assert.AreEqual(4, eMath.RInt(3.5))
        Assert.AreEqual(4, eMath.RInt(3.9))
        Assert.AreEqual(-3, eMath.RInt(-3.1))
        Assert.AreEqual(-4, eMath.RInt(-3.5))
        Assert.AreEqual(-4, eMath.RInt(-3.9))
        Assert.AreEqual(4, eMath.RInt(4.1))
        Assert.AreEqual(5, eMath.RInt(4.5))
        Assert.AreEqual(5, eMath.RInt(4.9))
        Assert.AreEqual(-4, eMath.RInt(-4.1))
        Assert.AreEqual(-5, eMath.RInt(-4.5))
        Assert.AreEqual(-5, eMath.RInt(-4.9))
    End Sub

    <Test()> Public Sub TestFractional()
        Assert.AreEqual(-0.4, Math.IEEERemainder(3.6, 1), PiVariance)
        Assert.AreEqual(0.6, eMath.Fractional(3.6), PiVariance)
        Dim r As Int32
        Math.DivRem(5, 2, r)
        Assert.AreEqual(r, 1)
    End Sub

    <Test()> Public Sub TestfModulus()
        Assert.AreEqual(0.5, (3.5 Mod 3))
    End Sub

    <Test()> Public Sub TestIntPow()
        Dim r As Int32 = CType(Math.Pow(3, 4), Int32)
        Assert.AreEqual(81, r)
    End Sub

    <Test()> Public Sub TestSqr()
        Assert.AreEqual(20.25, eMath.Sqr(4.5))
    End Sub

    <Test()> Public Sub TestValidRad()
        ' send in 3.7*360deg = 1332deg and see if routine normalizes value to 252deg
        Assert.AreEqual(0.7 * Units.OneRev, eMath.ValidRad(3.7 * Units.OneRev))
        Assert.AreEqual(0.3 * Units.OneRev, eMath.ValidRad(-3.7 * Units.OneRev))
    End Sub

    <Test()> Public Sub TestValidRadPi()
        ' send in 3.7*360deg = 1332deg and see if routine normalizes value to -108deg
        Assert.AreEqual(-0.3 * Units.OneRev, eMath.ValidRadPi(3.7 * Units.OneRev))
        Assert.AreEqual(0.3 * Units.OneRev, eMath.ValidRadPi(-3.7 * Units.OneRev))
    End Sub

    ' slight errors because math.PI = 3.1415926535897931
    <Test()> Public Sub TestValidRadHalfPi()
        ' 1st quadrant 0-90
        Assert.AreEqual(eMath.ValidRadHalfPi(10 * Units.DegToRad), 10 * Units.DegToRad)
        Assert.AreEqual(eMath.ValidRadHalfPi(80 * Units.DegToRad), 80 * Units.DegToRad)
        ' 2nd quadrant 90-0
        Assert.AreEqual(eMath.ValidRadHalfPi(100 * Units.DegToRad), 80 * Units.DegToRad)
        Assert.AreEqual(eMath.ValidRadHalfPi(170 * Units.DegToRad), 10 * Units.DegToRad, PiVariance)
        ' 3rd quadrant 0--90
        Assert.AreEqual(eMath.ValidRadHalfPi(190 * Units.DegToRad), -10 * Units.DegToRad, PiVariance)
        Assert.AreEqual(eMath.ValidRadHalfPi(260 * Units.DegToRad), -80 * Units.DegToRad, PiVariance)
        ' 4th quadrant -90-0
        Assert.AreEqual(eMath.ValidRadHalfPi(280 * Units.DegToRad), -80 * Units.DegToRad, PiVariance)
        Assert.AreEqual(eMath.ValidRadHalfPi(350 * Units.DegToRad), -10 * Units.DegToRad, PiVariance)
        ' carry quadrant 0-90 again
        Assert.AreEqual(eMath.ValidRadHalfPi(370 * Units.DegToRad), 10 * Units.DegToRad, PiVariance)
        Assert.AreEqual(eMath.ValidRadHalfPi(440 * Units.DegToRad), 80 * Units.DegToRad, PiVariance)
    End Sub

    <Test()> Public Sub TestReverseRad()
        Assert.AreEqual(Units.ThreeFourthsRev, eMath.ReverseRad(Units.QtrRev))
        Assert.AreEqual(Units.QtrRev, eMath.ReverseRadPi(-Units.QtrRev))
        Assert.AreEqual(-Units.QtrRev, eMath.ReverseRadPi(Units.QtrRev))

        Assert.AreEqual(Units.ThreeFourthsRev, eMath.ReverseRad(True, Units.QtrRev))
        Assert.AreEqual(Units.QtrRev, eMath.ReverseRadPi(True, -Units.QtrRev))
        Assert.AreEqual(-Units.QtrRev, eMath.ReverseRadPi(True, Units.QtrRev))
        Assert.AreEqual(Units.QtrRev, eMath.ReverseRad(False, Units.QtrRev))
        Assert.AreEqual(-Units.QtrRev, eMath.ReverseRadPi(False, -Units.QtrRev))
        Assert.AreEqual(Units.QtrRev, eMath.ReverseRadPi(False, Units.QtrRev))
    End Sub

    <Test()> Public Sub TestQuadrant()
        Assert.AreEqual(1, eMath.Quadrant(45 * Units.DegToRad))
        Assert.AreEqual(2, eMath.Quadrant(135 * Units.DegToRad))
        Assert.AreEqual(3, eMath.Quadrant(225 * Units.DegToRad))
        Assert.AreEqual(4, eMath.Quadrant(315 * Units.DegToRad))
    End Sub

    <Test()> Public Sub TestBoundsSinCos()
        Assert.AreEqual(-1.0, eMath.BoundsSinCos(-1.1))
        Assert.AreEqual(0.7, eMath.BoundsSinCos(0.7))
    End Sub

    <Test()> Public Sub TestCot()
        Dim angle As Double = 50
        Assert.AreEqual(eMath.Cot(angle), 1 / Math.Tan(angle))
    End Sub

    <Test()> Public Sub TestRandomize()
        Dim lowerBound As Int32 = 5
        Dim upperBound As Int32 = 10
        Dim testArray As New ArrayList
        For ix As Int32 = lowerBound To upperBound
            testArray.Add(ix)
        Next

        For tries As Int32 = 0 To 100
            Dim d As Double = eMath.Randomize(lowerBound, upperBound)
            Dim id As Int32 = CType(d, Int32)
            For tIx As Int32 = 0 To testArray.Count - 1
                If id.Equals(testArray(tIx)) Then
                    testArray.RemoveAt(tIx)
                    Exit For
                End If
            Next
            If testArray.Count.Equals(0) Then
                Exit For
            End If
        Next

        Assert.IsTrue(testArray.Count.Equals(0))
    End Sub

    <Test()> Public Sub TestAngleRadFromPoints()
        Dim fromPoint As New Drawing.Point(0, 0)
        Dim toPoint As New Drawing.Point(1, -1)
        Assert.AreEqual(45 * Units.DegToRad, eMath.AngleRadFromPoints(fromPoint, toPoint))
        toPoint.Y = 1
        Assert.AreEqual(135 * Units.DegToRad, eMath.AngleRadFromPoints(fromPoint, toPoint))
        toPoint.X = -1
        Assert.AreEqual(225 * Units.DegToRad, eMath.AngleRadFromPoints(fromPoint, toPoint))
        toPoint.Y = -1
        Assert.AreEqual(315 * Units.DegToRad, eMath.AngleRadFromPoints(fromPoint, toPoint))
    End Sub

    <Test()> Public Sub MinMax()
        Debug.WriteLine("int32 ranges from " & Int32.MinValue & " to " & Int32.MaxValue)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub Promotion()
        Assert.AreEqual(0.4, 1 / 2.5)
        Assert.AreEqual(2.5, 1 * 2.5)
        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
