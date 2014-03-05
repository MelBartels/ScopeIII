Imports NUnit.Framework

<TestFixture()> Public Class TestDependencyResolver

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub CanRegisterInstance()
        Dim resolver As DependencyResolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(New Foo)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub CanRegisterType()
        DependencyResolver.GetInstance.Register(Of Foo)(GetType(Foo))
        DependencyResolver.GetInstance.Register(Of IFoo)(GetType(Foo))
        DependencyResolver.GetInstance.Register(Of IFoo)(GetType(Foo12))
        DependencyResolver.GetInstance.Register(Of Foo12)(GetType(Foo3))
        DependencyResolver.GetInstance.Register(Of Foo12)(GetType(Foo4))
        DependencyResolver.GetInstance.Register(Of IFoo)(GetType(Foo3))
        DependencyResolver.GetInstance.Register(Of IFoo)(GetType(Foo4))
    End Sub

    ' attempt to register object that doesn't match expected type
    <Test(), ExpectedException(GetType(InvalidOperationException))> Public Sub InvalidOperationExceptionInstance()
        ' single line alternative to above
        DependencyResolver.GetInstance.Register(Of IFoo)(New Object)
    End Sub

    ' attempt to register type that doesn't match expected type
    <Test(), ExpectedException(GetType(InvalidOperationException))> Public Sub InvalidOperationExceptionType()
        ' single line alternative to above
        DependencyResolver.GetInstance.Register(Of IFoo)(GetType(Object))
    End Sub

    <Test()> Public Sub CanResolveInstance()
        Dim resolver As DependencyResolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(New Foo)
        Assert.IsNotNull(resolver.Resolve(Of IFoo)())
    End Sub

    <Test()> Public Sub CanResolveType()
        ' 2 types that implement 2 Ts
        Dim resolver As DependencyResolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(GetType(Foo))
        resolver.Register(Of IFoo2)(GetType(Foo2))
        Assert.IsNotNull(resolver.Resolve(Of IFoo)())
        Assert.IsNotNull(resolver.Resolve(Of IFoo2)())

        ' type that inherits from super that implements T
        resolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(GetType(Foo12))
        resolver.Register(Of IFoo2)(GetType(Foo12))
        Assert.IsNotNull(resolver.Resolve(Of IFoo)())
        Assert.IsNotNull(resolver.Resolve(Of IFoo2)())

        ' type that inherits from super that inherits from super that implements T
        resolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(GetType(Foo4))
        Assert.IsNotNull(resolver.Resolve(Of IFoo)())

        ' resolve to interface, then use specific type that implements T; also tests GetInstance instantiation
        resolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(GetType(Foo5))
        Dim IFoo As IFoo = resolver.Resolve(Of IFoo)()
        Assert.IsTrue(CType(IFoo, Foo5).B)

        ' type that implements T; also tests explicit public constructor
        resolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(GetType(Foo6))
        Assert.IsNotNull(resolver.Resolve(Of IFoo)())

        ' type that implements T; also tests GetInstance instantiation with explicit private constructor
        resolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(GetType(Foo7))
        Assert.IsNotNull(resolver.Resolve(Of IFoo)())
    End Sub

    ' attempt to resolve object not registered
    <Test(), ExpectedException(GetType(System.Collections.Generic.KeyNotFoundException))> Public Sub KeyNotFoundExceptionInstance()
        Dim resolver As DependencyResolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(New Foo)
        resolver.Resolve(Of IFoo2)()
    End Sub

    ' attempt to resolve type not registered
    <Test(), ExpectedException(GetType(System.Collections.Generic.KeyNotFoundException))> Public Sub KeyNotFoundExceptionType()
        Dim resolver As DependencyResolver = DependencyResolver.GetInstance
        resolver.Register(Of IFoo)(GetType(Foo))
        resolver.Resolve(Of IFoo2)()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Interface IFoo
    End Interface
    Private Class Foo : Implements IFoo
    End Class
    Private Interface IFoo2
    End Interface
    Private Class Foo2 : Implements IFoo2
    End Class
    Private Class Foo12 : Implements IFoo, IFoo2
    End Class
    Private Class Foo3 : Inherits Foo12
    End Class
    Private Class Foo4 : Inherits Foo3
    End Class
    Private Class Foo5 : Implements IFoo
        Public B As Boolean = True
        Public Shared Function GetInstance() As Foo5
            Return New Foo5
        End Function
    End Class
    Private Class Foo6 : Implements IFoo
        Public Sub New()
        End Sub
    End Class
    Private Class Foo7 : Implements IFoo
        Private Sub New()
        End Sub
        Public Shared Function GetInstance() As Foo7
            Return New Foo7
        End Function
    End Class

End Class
