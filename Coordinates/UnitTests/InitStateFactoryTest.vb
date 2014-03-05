Imports NUnit.Framework

<TestFixture()> Public Class InitStateFactoryTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFactory()
        Dim IST As New ArrayList

        Dim eCoordXformType As IEnumerator = CoordXformType.ISFT.Enumerator
        While eCoordXformType.MoveNext
            Dim eInitStateType As IEnumerator = InitStateType.ISFT.Enumerator
            While eInitStateType.MoveNext
                Dim InitStateTemplate As InitStateTemplate = InitStateFactory.GetInstance.Build(CType(eCoordXformType.Current, ISFT), CType(eInitStateType.Current, ISFT))
                IST.Add(InitStateTemplate)
            End While
        End While

        Assert.AreEqual(IST.Count, CoordXformType.ISFT.Size * InitStateType.ISFT.Size)

        Dim countNothing As Int32 = 0
        Dim count(InitType.ISFT.Size - 1) As Int32
        Dim eInitStateTemplate As IEnumerator = IST.GetEnumerator
        While eInitStateTemplate.MoveNext
            Dim InitStateTemplate As InitStateTemplate = CType(eInitStateTemplate.Current, InitStateTemplate)
            If InitStateTemplate.IInit Is Nothing Then
                countNothing += 1
            Else
                count(InitStateTemplate.IInit.InitType.Key) += 1
            End If
        End While

        ' should be 2 not initialized, one for each CoordXformType {ConvertTrig, ConvertMatrix}
        Assert.AreEqual(CoordXformType.ISFT.Size, countNothing)
        ' should be 3 DoNothings, one for each InitStateType (not counting 'None') for ConvertTrig
        Assert.AreEqual(InitStateType.ISFT.Size - 1, count(InitType.InitDoNothing.Key))
        ' should be one of each of the remainder
        Assert.AreEqual(1, count(InitType.InitConvertMatrixAltazimuth.Key))
        Assert.AreEqual(1, count(InitType.InitConvertMatrixCelestial.Key))
        Assert.AreEqual(1, count(InitType.InitConvertMatrixEquatorial.Key))
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
