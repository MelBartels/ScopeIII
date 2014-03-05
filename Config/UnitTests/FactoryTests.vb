Imports NUnit.Framework
Imports System.Reflection

<TestFixture()> Public Class FactoryTests
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestInvokeMethodAcrossNamespaces()
        Dim testResultObj As Object

        ' can't find the type from a string unless namespace included
        Dim failed As Boolean = False
        Try
            testResultObj = System.Type.GetType("FactoryTestClassB").GetMethod("GetInstance", Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static).Invoke(Nothing, Nothing)
        Catch ex As Exception
            failed = True
        End Try
        Assert.IsTrue(failed)

        ' this doesn't work either
        failed = False
        Try
            testResultObj = System.Type.GetType("BartelsLibrary.FactoryTestClassB").GetMethod("GetInstance", Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static).Invoke(Nothing, Nothing)
        Catch ex As Exception
            failed = True
        End Try
        Assert.IsTrue(failed)

        ' how to include namespace
        testResultObj = System.Type.GetType("BartelsLibrary.FactoryTestClassB, BartelsLibrary").GetMethod("GetInstance", Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static).Invoke(Nothing, Nothing)
        Assert.IsInstanceOfType(GetType(FactoryTestClassB), testResultObj)

        Assert.IsTrue(True)
    End Sub

    ' copy of unit test from BartelsLibrary.FactoryTests
    <Test()> Public Sub TestFactoryBuild()
        Dim testResultObj As Object = CType(New FactoryTestBuild, FactoryTestBuild).Build("FactoryTestClassB")
        Assert.IsNull(testResultObj)

        testResultObj = CType(New FactoryTestBuild, FactoryTestBuild).BuildWithNamespace("FactoryTestClassB")
        Assert.IsInstanceOfType(GetType(FactoryTestClassB), testResultObj)

        Assert.IsTrue(True)
    End Sub

    ' copy of unit test above but using factory in this namespace
    <Test()> Public Sub TestConfigFactoryBuild()
        Dim testResultObj As Object = CType(New ConfigFactoryTestBuild, ConfigFactoryTestBuild).Build("FactoryTestClassB")
        Assert.IsNull(testResultObj)

        ' unlike unit test immediately above, this fails, demonstrating that the factory (as coded) must be in the
        ' same namespace as the object to instantiate
        testResultObj = CType(New ConfigFactoryTestBuild, ConfigFactoryTestBuild).BuildWithNamespace("FactoryTestClassB")
        Assert.IsNull(testResultObj)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
Public Class ConfigFactoryTestBuild
    Public Function Build(ByVal TypeName As String) As Object
        Dim mType As Type = Type.GetType(TypeName)
        If mType Is Nothing Then
            Return Nothing
        Else
            Return mType.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        End If
    End Function
    ' namespace must be included for Type.GetType()
    Public Function BuildWithNamespace(ByVal typeName As String) As Object
        Dim mType As Type = Type.GetType(CommonShared.IncludeNamespaceWithTypename(typeName, Me))
        If mType Is Nothing Then
            Return Nothing
        Else
            Return mType.GetMethod("GetInstance", BindingFlags.IgnoreCase Or BindingFlags.Static Or BindingFlags.Public).Invoke(Nothing, Nothing)
        End If
    End Function

End Class
