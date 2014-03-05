Imports NUnit.Framework
Imports System.Reflection

<TestFixture()> Public Class FactoryTests
    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestCreateInstance()
        Dim factoryTestClassA As FactoryTestClassA = Nothing
        Dim objectName As New Text.StringBuilder
        objectName.Append(GetType(FactoryTestClassA).Namespace.ToString)
        objectName.Append(".")
        objectName.Append(GetType(FactoryTestClassA).Name())
        Dim testResultObj As Object = Activator.CreateInstance(Type.GetType(objectName.ToString))
        Assert.IsInstanceOfType(GetType(FactoryTestClassA), testResultObj)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestInvokeMethod()
        Dim type As Type = GetType(FactoryTestClassB)
        Dim method As Reflection.MethodInfo = type.GetMethod("GetInstance")
        Dim testResultObj As Object = method.Invoke(Nothing, Nothing)
        Assert.IsInstanceOfType(GetType(FactoryTestClassB), testResultObj)

        method = type.GetMethod("GetInstance", Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
        testResultObj = method.Invoke(Nothing, Nothing)
        Assert.IsInstanceOfType(GetType(FactoryTestClassB), testResultObj)

        testResultObj = type.GetMethod("GetInstance", Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static).Invoke(Nothing, Nothing)
        Assert.IsInstanceOfType(GetType(FactoryTestClassB), testResultObj)

        Assert.IsTrue(True)
    End Sub

    ' see identical unit test in another project
    <Test()> Public Sub TestInvokeMethodAcrossNamespaces()
        Assert.IsTrue(True)
    End Sub

    ' see identical unit test in another project
    <Test()> Public Sub TestFactoryBuild()
        Dim testResultObj As Object = CType(New FactoryTestBuild, FactoryTestBuild).Build("FactoryTestClassB")
        Assert.IsNull(testResultObj)

        testResultObj = CType(New FactoryTestBuild, FactoryTestBuild).BuildWithNamespace("FactoryTestClassB")
        Assert.IsInstanceOfType(GetType(FactoryTestClassB), testResultObj)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class

Class FactoryTestClassA
End Class
Public Class FactoryTestClassB
    Private Sub New()
    End Sub
    Public Shared Function GetInstance() As FactoryTestClassB
        Return New FactoryTestClassB
    End Function
End Class
Public Class FactoryTestBuild
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
