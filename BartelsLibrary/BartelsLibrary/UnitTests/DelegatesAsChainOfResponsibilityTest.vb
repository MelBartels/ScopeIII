Imports NUnit.Framework

<TestFixture()> Public Class DelegatesAsChainOfResponsibilityTest

    Delegate Function testDel(ByRef [object] As Object) As Boolean
    Private calls As Int32

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestCoR()
        ' test calling delegate
        calls = 0
        Dim d1 As New testDel(AddressOf f1)
        d1.Invoke(Nothing)
        Assert.AreEqual(1, calls)

        ' test calling delegate that chains to another delegate
        calls = 0
        Dim d2 As New testDel(AddressOf f2)
        d2.Invoke(CObj(d1))
        Assert.AreEqual(2, calls)

        ' as above but inline declaration for 1st delegate
        calls = 0
        Dim d3 As New testDel(AddressOf f2)
        d3.Invoke(New testDel(AddressOf f2))
        Assert.AreEqual(2, calls)

        ' all delegates declared inline
        calls = 0
        CType(New testDel(AddressOf f2), testDel).Invoke(New testDel(AddressOf f2))
        Assert.AreEqual(2, calls)

        ' chaining 3 delegates
        calls = 0
        CType(New testDel(AddressOf f2), testDel).Invoke( _
        CType(New testDel(AddressOf f2), testDel).Invoke( _
        New testDel(AddressOf f2)))
        Assert.AreEqual(3, calls)

        ' shows how true/false controls the chain
        calls = 0
        CType(New testDel(AddressOf f3), testDel).Invoke( _
        CType(New testDel(AddressOf f3), testDel).Invoke( _
        New testDel(AddressOf f3)))
        Assert.AreEqual(1, calls)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Function f1(ByRef [object] As Object) As Boolean
        calls += 1
        Return True
    End Function

    Private Function f2(ByRef [object] As Object) As Boolean
        If [object] IsNot Nothing AndAlso [object].GetType.Name.Equals("testDel") Then
            CType([object], testDel).Invoke(Nothing)
        End If
        calls += 1
        Return True
    End Function

    Private Function f3(ByRef [object] As Object) As Boolean
        If [object] IsNot Nothing AndAlso [object].GetType.Name.Equals("testDel") Then
            If CType([object], testDel).Invoke(Nothing) Then
                calls += 1
                Return calls <= 1
            End If
            Return False
        End If
        Return True
    End Function

    <Test()> Public Sub TestMissingReturn()
        Assert.IsFalse(r1)

        Assert.IsTrue(True)
    End Sub
    Private Function r1() As Boolean
    End Function
End Class
