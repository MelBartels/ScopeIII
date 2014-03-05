Imports NUnit.Framework

<TestFixture()> Public Class HierarchicalTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub HierarchicalTest()
        Dim textBoxHierarchicalAdapter As TextBoxHierarchicalAdapter = BartelsLibrary.TextBoxHierarchicalAdapter.GetInstance
        Dim IHierarchical As IHierarchical = textBoxHierarchicalAdapter

        Dim textbox As New Windows.Forms.TextBox
        textBoxHierarchicalAdapter.RegisterComponent(CObj(textbox))

        Dim parent As Object = textBoxHierarchicalAdapter.AddChild(Nothing, "A")
        parent = textBoxHierarchicalAdapter.AddChild(parent, "A.1")
        parent = textBoxHierarchicalAdapter.AddChild(parent, "A.1.1")
        parent = textBoxHierarchicalAdapter.AddChild(Nothing, "B")
        parent = textBoxHierarchicalAdapter.AddChild(parent, "B.1")

        ' # of spaces indicate whether parent/child were assigned properly
        Dim spacesCount As Int32
        For ix As Int32 = 0 To textbox.Text.Length - 1
            If textbox.Text.Substring(ix, 1).Equals(" ") Then
                spacesCount += 1
            End If
        Next

        Assert.AreEqual(4 * Constants.HierarchicalIncrement.Length, spacesCount)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

End Class
