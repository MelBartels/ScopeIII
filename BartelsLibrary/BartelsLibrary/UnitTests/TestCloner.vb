Imports NUnit.Framework

<TestFixture()> Public Class TestCloner

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestCloner()
        Dim cloner As Cloner = BartelsLibrary.Cloner.GetInstance
        Dim o1 As AZdouble = AZdouble.GetInstance
        o1.A = 1
        Dim o2 As AZdouble = CType(cloner.ViaSerialization(o1), AZdouble)
        ' verify that value was cloned
        Assert.AreEqual(o1.A, o2.A)
        ' change value and verify that original object was not changed
        o2.A = 2
        Assert.IsFalse(o1.A.Equals(o2.A))
        ' create reference to original, change reference and verify that original was changed
        Dim o3 As AZdouble = o1
        o3.A = 3
        Assert.AreEqual(o1.A, o3.A)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
