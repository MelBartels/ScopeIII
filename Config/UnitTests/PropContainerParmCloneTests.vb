Imports NUnit.Framework
Imports system.ComponentModel

<TestFixture()> Public Class PropContainerParmCloneTests

    Private pShowFormTimeMilliseconds As Int32 = 30000
    Private pFrmPropGrid As FrmPropertyGrid
    Private pFrmPropGridCloned As FrmPropertyGrid
    Private pPropContainer As PropContainer
    Private pClonedContainer As PropContainer
    Private pTestName As String = "number1"
    Private pTestName2 As String = "number2"
    Private pTestCategory As String = "Numbers"
    Private pTestDescription As String = "an Int32"
    Private pTestDefaultValue As Int32 = 44
    Private pTestNumber1 As Int32
    Private pTestNumber1cloned As Int32

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestCloning()
        pPropContainer = PropContainer.GetInstance

        AddHandler pPropContainer.GetValue, AddressOf thisGetValue
        AddHandler pPropContainer.SetValue, AddressOf thisSetValue

        Dim propParm As PropParm = Config.PropParm.GetInstance
        propParm.Init(pTestName, GetType(Int32), pTestCategory, pTestDescription, pTestDefaultValue, Nothing, Nothing)
        pPropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init(pTestName2, GetType(Int32), pTestCategory, pTestDescription, pTestDefaultValue, Nothing, Nothing)
        propParm.Attributes = New Attribute() {ReadOnlyAttribute.Yes}
        pPropContainer.Properties.Add(propParm)

        Assert.IsTrue(True)

        Dim frmThread As New Threading.Thread(AddressOf showOriginal)
        frmThread.Start()
        Dim frmTimer As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimer.Elapsed, AddressOf killPropGridShowOriginal
        frmTimer.AutoReset = False
        frmTimer.Start()

        pClonedContainer = CType(pPropContainer.Clone, PropContainer)
        ' remove the cloned event handlers which still point to the original's event handling methods
        'RemoveHandler pClonedContainer.GetValue, AddressOf thisGetValue
        'RemoveHandler pClonedContainer.SetValue, AddressOf thisSetValue
        AddHandler pClonedContainer.GetValue, AddressOf thisGetValueCloned
        AddHandler pClonedContainer.SetValue, AddressOf thisSetValueCloned

        Dim frmThreadCloned As New Threading.Thread(AddressOf showCloned)
        frmThreadCloned.Start()
        Dim frmTimerCloned As New Timers.Timer(pShowFormTimeMilliseconds)
        AddHandler frmTimerCloned.Elapsed, AddressOf killPropGridShowCloned
        frmTimerCloned.AutoReset = False
        frmTimerCloned.Start()

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Sub showOriginal()
        pFrmPropGrid = New FrmPropertyGrid
        pFrmPropGrid.PropertyGrid.SelectedObject = pPropContainer
        pFrmPropGrid.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropGridShowOriginal(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropGrid.InvokeRequired Then
            pFrmPropGrid.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropGrid.Close))
        Else
            pFrmPropGrid.Close()
        End If
    End Sub

    Private Sub showCloned()
        pFrmPropGridCloned = New FrmPropertyGrid
        pFrmPropGridCloned.PropertyGrid.SelectedObject = pClonedContainer
        pFrmPropGridCloned.ShowDialog()
        Assert.IsTrue(True)
    End Sub

    Private Sub killPropGridShowCloned(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        pFrmPropGridCloned.Close()
    End Sub

    Private Sub thisGetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case pTestName
                e.Value = pTestNumber1
            Case pTestName2
                e.Value = pTestDefaultValue
        End Select
    End Sub

    Private Sub thisSetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case pTestName
                pTestNumber1 = CType(e.Value, Int32)
            Case pTestName2
                'readonly
        End Select
    End Sub

    Private Sub thisGetValueCloned(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case pTestName
                e.Value = pTestNumber1cloned
            Case pTestName2
                e.Value = pTestDefaultValue
        End Select
    End Sub

    Private Sub thisSetValueCloned(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case pTestName
                pTestNumber1cloned = CType(e.Value, Int32)
            Case pTestName2
                'readonly
        End Select
    End Sub
End Class
