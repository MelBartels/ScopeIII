Imports NUnit.Framework

<TestFixture()> Public Class TestConsoleHelper

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub Test()
        Dim ch As ConsoleHelperFake = New ConsoleHelperFake

        Dim i As Int32
        ch.GetInt("unit test get int ", i)
        Assert.AreEqual(eMath.RInt(ch.Input(0)), i)

        Dim d As Double
        ch.GetDouble("unit test get double ", d)
        Assert.AreEqual(CDbl(ch.Input(1)), d)

        Dim s As String = String.Empty
        ch.GetString("unit test get string ", s)
        Assert.AreEqual(ch.Input(2), s)

        ch.PressReturnToContinue()
        ' test is that we continue execution
        Assert.IsTrue(True)
    End Sub

    Class ConsoleHelperFake : Inherits ConsoleHelper
        Public Input As String() = {"1", "2.3", "msg", " "}
        Private ciIx As Int32 = -1
        Protected Overrides Sub displayPrompt(ByVal prompt As String)
            Debug.Write(prompt + " ")
        End Sub
        Protected Overrides Function readConsole() As String
            ciIx += 1
            Return Input(ciIx)
        End Function
    End Class

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
