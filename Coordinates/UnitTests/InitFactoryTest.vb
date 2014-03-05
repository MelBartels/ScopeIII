Imports NUnit.Framework

<TestFixture()> Public Class InitFactoryTest
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFactory()
        Dim InitArray As New ArrayList
        Dim eInitType As System.Collections.IEnumerator = InitType.ISFT.Enumerator
        While eInitType.MoveNext
            Dim initType As ISFT = CType(eInitType.Current, ISFT)
            DebugTrace.WriteLine("attempting factory creation of " & initType.Name)
            Dim IInit As IInit = InitFactory.GetInstance.Build(initType, Nothing)
            If IInit IsNot Nothing Then
                InitArray.Add(IInit)
            End If
        End While
        Assert.AreEqual(InitArray.Count, InitType.ISFT.Size)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
