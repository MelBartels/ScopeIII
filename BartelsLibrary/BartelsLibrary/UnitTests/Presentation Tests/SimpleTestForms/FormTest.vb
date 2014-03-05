Imports NUnit.Framework

<TestFixture()> Public Class FormTest

    Private pFrmTreeView As frmTreeView
    Private pFrmTextBox As FrmTextBox
    Private pFrmPropertyGrid As FrmPropertyGrid

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestFrmTreeView()
        Dim frmThread As New Threading.Thread(AddressOf showTreeViewForm)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(10000)
        AddHandler frmTimer.Elapsed, AddressOf killTreeViewForm
        frmTimer.Start()

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestFrmTextBox()
        Dim frmThread As New Threading.Thread(AddressOf showTextBoxForm)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(10000)
        AddHandler frmTimer.Elapsed, AddressOf killTextBoxForm
        frmTimer.AutoReset = False
        frmTimer.Start()

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestFrmPropertyGrid()
        Dim frmThread As New Threading.Thread(AddressOf showPropertyGridForm)
        frmThread.Start()

        Dim frmTimer As New Timers.Timer(10000)
        AddHandler frmTimer.Elapsed, AddressOf killPropertyGridForm
        frmTimer.AutoReset = False
        frmTimer.Start()

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Sub showTreeViewForm()
        pFrmTreeView = New FrmTreeView
        pFrmTreeView.TreeView.Nodes.Add(New Windows.Forms.TreeNode("A"))
        pFrmTreeView.TreeView.Nodes(0).Nodes.Add(New Windows.Forms.TreeNode("1"))
        pFrmTreeView.TreeView.Nodes.Add(New Windows.Forms.TreeNode("B"))
        pFrmTreeView.TreeView.ExpandAll()
        pFrmTreeView.ShowDialog()
    End Sub

    Private Sub showTextBoxForm()
        pFrmTextBox = New FrmTextBox
        pFrmTextBox.txBxDisplay.Text = "A" & vbCrLf & "B"
        pFrmTextBox.ShowDialog()
    End Sub

    Private Sub showPropertyGridForm()
        pFrmPropertyGrid = New FrmPropertyGrid
        pFrmPropertyGrid.PropertyGrid.SelectedObject = New PGObject
        pFrmPropertyGrid.ShowDialog()

        Assert.IsTrue(True)
    End Sub

    Private Sub killTreeViewForm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmTreeView.InvokeRequired Then
            pFrmTreeView.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmTreeView.Close))
        Else
            pFrmTreeView.Close()
        End If
    End Sub

    Private Sub killTextBoxForm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmTextBox.InvokeRequired Then
            pFrmTextBox.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmTextBox.Close))
        Else
            pFrmTextBox.Close()
        End If
    End Sub

    Private Sub killPropertyGridForm(ByVal source As Object, ByVal e As Timers.ElapsedEventArgs)
        If pFrmPropertyGrid.InvokeRequired Then
            pFrmPropertyGrid.Invoke(New Windows.Forms.MethodInvoker(AddressOf pFrmPropertyGrid.Close))
        Else
            pFrmPropertyGrid.Close()
        End If
    End Sub

    Private Class PGObject
        Public Property TrueFalse() As Boolean
            Get
                Return pTrueFalse
            End Get
            Set(ByVal Value As Boolean)
                pTrueFalse = Value
            End Set
        End Property
        Private pTrueFalse As Boolean
    End Class
End Class
