Imports NUnit.Framework

<TestFixture()> Public Class CoordXformFactoryTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFactory()
        Dim ICoordXformArray As New ArrayList
        Dim eCoordXformType As System.Collections.IEnumerator = CoordXformType.ISFT.Enumerator
        While eCoordXformType.MoveNext()
            Dim cxt As ISFT = CType(eCoordXformType.Current, ISFT)
            DebugTrace.WriteLine("attempting factory creation of " & cxt.Name)
            Dim ICoordXform As ICoordXform = CoordXformFactory.GetInstance.Build(cxt)
            If ICoordXform IsNot Nothing Then
                ICoordXformArray.Add(ICoordXform)
            End If
        End While
        Assert.AreEqual(ICoordXformArray.Count, CoordXformType.ISFT.Size)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
