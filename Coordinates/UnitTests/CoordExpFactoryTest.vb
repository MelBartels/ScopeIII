Imports NUnit.Framework

<TestFixture()> Public Class CoordExpFactoryTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFactory()
        Dim CoordExpType As CoordExpType = Coordinates.CoordExpType.GetInstance
        Dim ICoordExpArray As New ArrayList
        Dim eCoordExpType As IEnumerator = Coordinates.CoordExpType.ISFT.Enumerator
        While eCoordExpType.MoveNext()
            Dim cet As ISFT = CType(eCoordExpType.Current, ISFT)
            DebugTrace.WriteLine("attempting factory creation of " & cet.Name)
            Dim ICoordExp As ICoordExp = CoordExpFactory.GetInstance.Build(cet)
            If ICoordExp IsNot Nothing Then
                ICoordExpArray.Add(ICoordExp)
            End If
        End While
        Assert.AreEqual(ICoordExpArray.Count, Coordinates.CoordExpType.ISFT.Size)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
