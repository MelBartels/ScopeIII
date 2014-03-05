Imports NUnit.Framework

<TestFixture()> Public Class GaugeValueTests

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestValidation()
        Dim IGaugeValue As IGaugeValue = GaugeLinearValue.GetInstance
        IGaugeValue.MinValue = 10
        IGaugeValue.MaxValue = 20

        Assert.AreEqual(10, IGaugeValue.Validate(5))
        Assert.AreEqual(20, IGaugeValue.Validate(25))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestLinearScale()
        Dim IGaugeValue As IGaugeValue = GaugeLinearValue.GetInstance
        IGaugeValue.MinValue = 10
        IGaugeValue.MaxValue = 20

        Assert.AreEqual(16, IGaugeValue.ScaleValue(0.6))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestLogPercent()
        Dim IGaugeValue As IGaugeValue = GaugeLogValue.GetInstance
        IGaugeValue.MinValue = 100
        IGaugeValue.MaxValue = 200

        ' 'cause scaling
        Assert.AreEqual(0.5187132489703119, IGaugeValue.ScalePercent(110))
        ' w/ scaling
        Dim adjustedValue As Double = eMath.ScaleValue(110, -1, IGaugeValue.MinValue, IGaugeValue.MaxValue)
        Dim adjustedScalePercent As Double = IGaugeValue.ScalePercent(adjustedValue)
        Assert.AreEqual(0.5, adjustedScalePercent)

        Assert.IsTrue(True)
    End Sub

    ' also see TestLogArcGaugeRendererBase.TestLogToScalePercent()

    <Test()> Public Sub TestLogScale()
        Dim IGaugeValue As IGaugeValue = GaugeLogValue.GetInstance
        IGaugeValue.MinValue = 100
        IGaugeValue.MaxValue = 200

        ' 'cause scaling
        Assert.AreEqual(110, IGaugeValue.ScaleValue(0.5187132489703119))
        ' w/ scaling
        Dim adjustedValue As Double = eMath.ScaleValue(110, -1, IGaugeValue.MinValue, IGaugeValue.MaxValue)
        Assert.AreEqual(adjustedValue, IGaugeValue.ScaleValue(0.5))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestLinearPercent()
        Dim IGaugeValue As IGaugeValue = GaugeLinearValue.GetInstance
        IGaugeValue.MinValue = 10
        IGaugeValue.MaxValue = 20

        Assert.AreEqual(0.6, IGaugeValue.ScalePercent(16))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class

