Imports NUnit.Framework

<TestFixture()> Public Class MountingTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestConfigAdapter()
        Dim mount As IMount = ConfigAdapter.GetInstance.GetMounting(MountType.MountTypeNone)
        Assert.IsTrue(MountType.MountTypeNone.Description.Equals(mount.MountType.Description))

        Dim mountArray As New ArrayList
        Dim eMountType As IEnumerator = MountType.ISFT.Enumerator
        While eMountType.MoveNext
            mountArray.Add(ConfigAdapter.GetInstance.GetMounting(CType(eMountType.Current, ISFT)))
        End While
        Assert.AreEqual(MountType.ISFT.Size, mountArray.Count)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
