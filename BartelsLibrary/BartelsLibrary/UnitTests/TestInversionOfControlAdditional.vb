Imports System.Collections.Generic
Imports NUnit.Framework
Imports System.Reflection

<TestFixture()> Public Class TestInversionOfControlAdditional

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        Dim resolver As DependencyResolver = DependencyResolver.GetInstance
        resolver.Register(Of IServiceA)(New ServiceA)
        InversionOfControl.Initialize(resolver)
    End Sub

    <Test()> Public Sub TestServiceAProcess()
        Dim b As New B
        Dim serviceA As New ServiceA
        Assert.IsTrue(serviceA.Process(b))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestServiceAProcessUsingResolver()
        Dim b As New B
        Dim serviceA As IServiceA = InversionOfControl.Resolve(Of IServiceA)()
        Assert.IsTrue(serviceA.Process(b))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestMockUsingResolver()
        ' reset inline using faked service
        InversionOfControl.Initialize(Nothing)
        Dim resolver As DependencyResolver = DependencyResolver.GetInstance
        resolver.Register(Of IServiceA)(New FakeServiceA)
        InversionOfControl.Initialize(resolver)
        ' test using faked service
        Dim ServiceA As IServiceA = InversionOfControl.Resolve(Of IServiceA)()
        Assert.IsTrue(ServiceA.Process(Nothing))
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestGetAttributes()
        ' too long running on large assemblies
        'Dim a As Assembly = Assembly.Load("BartelsLibrary")
        'Assert.IsNotNull(a)
        'For Each t As Type In a.GetTypes

        Dim types As List(Of Type) = New List(Of Type)
        types.Add(GetType(ServiceA))
        types.Add(GetType(IServiceA))
        types.Add(GetType(TestInversionOfControlAdditional))

        For Each t As Type In types
            Debug.WriteLine("class " & t.FullName)
            For Each classAttr As Attribute In Attribute.GetCustomAttributes(t)
                Debug.WriteLine("     class attr " & classAttr.ToString)
                For Each mInfo As MethodInfo In t.GetMethods()
                    Debug.WriteLine("   method " & mInfo.Name)
                    For Each methodAttr As Attribute In Attribute.GetCustomAttributes(t)
                        Debug.WriteLine("     method attr " & methodAttr.ToString)
                    Next
                Next
            Next
        Next

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
        InversionOfControl.Initialize(Nothing)
    End Sub

    Private Interface IServiceA
        Function Process(ByVal b As IServiceB) As Boolean
    End Interface
    Private Class ServiceA : Implements IServiceA
        Public Function Process(ByVal b As IServiceB) As Boolean Implements IServiceA.Process
            Return b IsNot Nothing
        End Function
    End Class
    Private Class FakeServiceA : Implements IServiceA
        Public Function Process(ByVal b As IServiceB) As Boolean Implements IServiceA.Process
            Return True
        End Function
    End Class
    Private Interface IServiceB
    End Interface
    Private Class B : Implements IServiceB
    End Class

End Class
