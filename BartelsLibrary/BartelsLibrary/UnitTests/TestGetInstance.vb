Imports NUnit.Framework

<TestFixture()> Public Class TestGetInstance

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestInstanceConstructor()
        Dim t1 As GetInstanceTest = GetInstanceTest.GetInstance
        Assert.IsNotNull(t1)
        Assert.AreNotSame(t1, GetInstanceTest.GetInstance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestSingleton()
        Dim t1 As GetSingletonTest = GetSingletonTest.GetInstance
        Assert.IsNotNull(t1)
        Assert.AreSame(t1, GetSingletonTest.GetInstance)

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub CallToBaseConstructor()
        Dim t1 As GetInstanceSubClassTest = GetInstanceSubClassTest.GetInstance
        Assert.IsTrue(t1.ConstructorCalled)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Class GetInstanceTest
        Private Sub New()
        End Sub
        Public Shared Function GetInstance() As GetInstanceTest
            Return New GetInstanceTest
        End Function
    End Class

    Class GetSingletonTest
        Private Sub New()
        End Sub
        Public Shared Function GetInstance() As GetSingletonTest
            Return NestedInstance.INSTANCE
        End Function
        Private Class NestedInstance
            ' explicit constructor informs compiler not to mark type as beforefieldinit
            Shared Sub New()
            End Sub
            ' friend = internal, shared = static, readonly = final
            Friend Shared ReadOnly INSTANCE As GetSingletonTest = New GetSingletonTest
        End Class
    End Class

    Class GetInstanceBaseClassTest
        Public ConstructorCalled As Boolean = False
        Protected Sub New()
            ConstructorCalled = True
        End Sub
        Public Shared Function GetInstance() As GetInstanceBaseClassTest
            Return New GetInstanceBaseClassTest
        End Function
    End Class
    Class GetInstanceSubClassTest : Inherits GetInstanceBaseClassTest
        Private Sub New()
            ' explicit call not necessary
            'MyBase.New()
        End Sub
        Public Overloads Shared Function GetInstance() As GetInstanceSubClassTest
            Return New GetInstanceSubClassTest
        End Function
    End Class
End Class
