Imports NUnit.Framework

<TestFixture()> Public Class TestLogArcGaugeRendererBase

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()

    End Sub

    <Test()> Public Sub TestLogToScalePercent()
        Dim precision As Double = 0.0000000000000005
        Dim fake As New LogArcGaugeRendererBaseFake
        Dim maxValue As Double = 1000
        Dim maxScaleLog10 As Double = Math.Log10(maxValue)
        Dim testValue As Double = 2
        Dim scalePercent As Double = fake.ScalePercentUsingLog(testValue, maxValue, maxScaleLog10)
        Dim logScaleOffset As Double = 1
        ' use scalePercent in reversing equation based on eMath.GetValueLogScaling()
        Assert.AreEqual(testValue, Math.Exp(scalePercent * 2.3025850929940459 * maxScaleLog10) - logScaleOffset, precision)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()

    End Sub

    Class LogArcGaugeRendererBaseFake : Inherits LogArcGaugeRendererBase
        Public Overloads Function ScalePercentUsingLog(ByVal scaleValue As Double, ByVal maxValue As Double, ByVal maxScaleLog10 As Double) As Double
            Return MyBase.scalePercentUsingLog(scaleValue, maxValue, maxScaleLog10)
        End Function
        Public Overrides Function MeasurementFromObjectToRender() As Double
        End Function
        Public Overrides Function MeasurementToPoint(ByVal point As System.Drawing.Point) As Double
        End Function
        Protected Overrides Sub renderPointer()
        End Sub
    End Class
End Class
