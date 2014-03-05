Imports NUnit.Framework

<TestFixture()> Public Class TestDatafiles

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestLoad()
        Dim library As Library = Coordinates.Library.GetInstance
        Assert.IsTrue(library.Load(library.GetDatafilesFromDirectory("..\..\data files abbrev")))
        Assert.AreEqual(134, library.Objects.Count)
        Assert.AreEqual(2, library.Sources.Count)

        Assert.IsFalse(library.Load(Nothing))
        Assert.IsFalse(library.Load(New String() {}))
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class
