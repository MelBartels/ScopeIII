Imports NUnit.Framework

<TestFixture()> Public Class TestInversionOfControl

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestResolve()
        Dim resolver As DependencyResolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(New Foo())
        InversionOfControl.Initialize(resolver)
        Dim foo As IFoo = InversionOfControl.Resolve(Of IFoo)()
        Assert.IsInstanceOfType(GetType(Foo), foo)
        Assert.IsTrue(True)
    End Sub

    <Test(), ExpectedException(GetType(NullReferenceException))> Public Sub TestInitializeNothing()
        Dim drf As New DependencyResolverFake
        ' put 1 entry in dictionary
        drf.Register(Of IFoo)(New Foo())
        InversionOfControl.Initialize(drf)
        Assert.AreEqual(1, drf.Dictionary.Count)
        ' clear resolver
        InversionOfControl.Initialize(Nothing)
        InversionOfControl.Resolve(Of IFoo)()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Interface IFoo
    End Interface
    Private Class Foo : Implements IFoo
    End Class
    Private Class DependencyResolverFake : Inherits DependencyResolver
        Public Function Dictionary() As System.Collections.Generic.Dictionary(Of Type, Object)
            Return pTypes
        End Function
    End Class

End Class
