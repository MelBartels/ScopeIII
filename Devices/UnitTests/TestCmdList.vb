Imports NUnit.Framework

<TestFixture()> Public Class TestCmdListAndComparer

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestList()
        Dim list As Generic.List(Of ISFT) = TestCmds.ISFT.GetList
        Assert.AreEqual(4, list.Count)
        Assert.AreSame(list(0), TestCmds.ISFT.FirstItem)
        Assert.AreSame(list(0), TestCmds.One)

        Assert.IsTrue(True)
    End Sub

    <Test(), ExpectedException(GetType(InvalidOperationException))> Public Sub TestTypeException()
        Dim list As Generic.List(Of ISFT) = SFTTest.ISFT.GetList

        Dim comparer As CmdListComparer = CmdListComparer.GetInstance
        list.Sort(comparer)

        ' should never reach here
        Assert.IsTrue(False)
    End Sub

    <Test()> Public Sub TestComparer()
        Dim list As Generic.List(Of ISFT) = TestCmds.ISFT.GetList

        Dim comparer As CmdListComparer = CmdListComparer.GetInstance
        list.Sort(comparer)

        Assert.AreSame(list(0), TestCmds.One)
        Assert.AreSame(list(1), TestCmds.Three)
        Assert.AreSame(list(2), TestCmds.Four)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class