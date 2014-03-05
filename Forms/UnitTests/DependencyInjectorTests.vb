Imports NUnit.Framework

<TestFixture()> Public Class DependencyInjectorTests

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestPropGridPresenter()
        Assert.IsInstanceOfType(GetType(PropGridPresenter), FormsDependencyInjector.GetInstance.IPropGridPresenterFactory)

        FormsDependencyInjector.GetInstance.UseUnitTestContainer = True
        Assert.IsInstanceOfType(GetType(EndoTestPropGridPresenter), FormsDependencyInjector.GetInstance.IPropGridPresenterFactory)

        FormsDependencyInjector.GetInstance.UseUnitTestContainer = False
        Assert.IsInstanceOfType(GetType(PropGridPresenter), FormsDependencyInjector.GetInstance.IPropGridPresenterFactory)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestBuildTestIoC()
        Dim dif As New DependencyInjectorFake
        dif.BuildTestIoC()
        Dim [object] As Object = dif.GetResolver.Resolve(Of IPropGridPresenter)()
        Assert.IsInstanceOfType(GetType(EndoTestPropGridPresenter), [object])

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Class DependencyInjectorFake : Inherits FormsDependencyInjector
        Public Overloads Sub BuildTestIoC()
            MyBase.buildTestIoC()
        End Sub
        Public Function GetResolver() As DependencyResolver
            Return pResolver
        End Function
    End Class

End Class

