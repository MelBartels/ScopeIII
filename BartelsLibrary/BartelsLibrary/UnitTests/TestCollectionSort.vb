Imports NUnit.Framework

<TestFixture()> Public Class TestCollectionSort

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestSortViaArray()
        Dim c As New Collection
        c.Add(3)
        c.Add(2)
        c.Add(1)

        SortCollection.Sort(c, New collectionComparer)

        Dim lasti As Int32 = Int32.MinValue
        For Each i As Int32 In c
            Assert.IsTrue(i >= lasti)
            lasti = i
        Next

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class collectionComparer : Implements IComparer
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Return CType(x, Int32).CompareTo(CType(y, Int32))
        End Function
    End Class
End Class
