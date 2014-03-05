Imports NUnit.Framework

<TestFixture()> Public Class SFTUnitTest
    Public Sub New()
        MyBase.New()
    End Sub

    Dim testISFT As ISFT
    Dim testISFTcopy As ISFT
    Dim testISFT2 As ISFT
    Dim testISFTextended As ISFT

    Dim testKey As Int32 = 0
    Dim extendedTestKey As Int32 = 5
    Dim testString As String = "aaa"
    Dim nextTestString As String = "bbb"
    Dim testString2 As String = "ddd"
    Dim testStringExtendedLastItem As String = "hhh"
    Dim testDescription As String = "description of aaa"
    Dim testSize As Int32 = 3
    Dim testExtendedSize As Int32 = 6

    <SetUp()> Public Sub Init()
        testISFT = SFTTest.GetInstance.FirstItem
        testISFTcopy = SFTTest.GetInstance.FirstItem
        ' make sure that 1st instance is not affected by a 2nd SFT
        testISFT2 = SFTTest2.GetInstance.FirstItem
    End Sub

    <Test()> Public Sub SingleItemTest()
        Dim s As String = SFTTestSingleItem.aaa.Name
        Assert.IsTrue(s.Equals(testString))
    End Sub

    <Test()> Public Sub CreationTest()
        Assert.AreSame(testISFT.FirstItem, testISFTcopy.FirstItem)
    End Sub

    <Test()> Public Sub FacadeNameTest()
        Assert.IsTrue(GetType(SFTTest).FullName.Equals(testISFT.FacadeName))
    End Sub

    <Test()> Public Sub KeyAndFirstItemTest()
        Assert.AreEqual(testISFT.FirstItem.Key, testKey)
    End Sub

    <Test()> Public Sub DescriptionTest()
        Assert.IsTrue(testDescription.Equals(testISFT.FirstItem.Description))
    End Sub

    <Test()> Public Sub SizeAndNextItemTest()
        Assert.AreEqual(testSize, testISFT.Size)
        Assert.AreEqual(testSize, testISFT2.Size)
        If testISFT.Size > 1 Then
            Assert.IsTrue(nextTestString.Equals(testISFT.NextItem.Name))
        End If
    End Sub

    <Test()> Public Sub CompareTest()
        Assert.IsTrue(testISFT.FirstItem Is testISFTcopy.FirstItem)
        Assert.IsFalse(testISFT.FirstItem Is testISFT2.FirstItem)
    End Sub

    <Test()> Public Sub MatchKeyTest()
        Assert.IsTrue(testString.Equals(testISFT.MatchKey(testKey).Name))
        Assert.IsNull(testISFT.MatchKey(testSize))
    End Sub

    <Test()> Public Sub MatchStringAndToStringTest()
        Assert.IsTrue(testString.Equals(testISFT.MatchString(testString).Name))
        Assert.IsTrue(testString2.Equals(testISFT2.MatchString(testString2).Name))
        Assert.IsNull(testISFT.MatchString(testString2))
    End Sub

    <Test()> Public Sub EnumeratorTest()
        Dim count As Int32 = 0
        Dim eSFT As IEnumerator = testISFT.Enumerator
        While eSFT.MoveNext
            count += 1
            Debug.WriteLine("EnumeratorTest: current value is " & CType(eSFT.Current, ISFT).Name)
        End While
        Assert.AreEqual(testISFT.Size, count)
    End Sub

    <Test()> Public Sub ToStringArrayTest()
        Dim s() As String = testISFT.ToStringArray
        Assert.IsTrue(s(0).Equals(testString))
        Assert.IsTrue(s(1).Equals(testDescription))
    End Sub

    <Test()> Public Sub DisplayAllTest()
        Debug.WriteLine(testISFT.DisplayAll)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub EnumTest()
        ' SFT to enum
        Assert.IsTrue(SFTTest.GetInstance.EnumName(testString).ToString.Equals(testISFT.MatchString(testString).Name))
        Assert.IsTrue(SFTTest.GetInstance.EnumName(nextTestString).ToString.Equals(testISFT.MatchString(nextTestString).Name))
        ' enum to SFT
        Assert.IsTrue(testISFT.MatchKey(SFTTest.Names.aaa).Name.Equals(testString))
        Assert.IsTrue(testISFT.MatchKey(SFTTest.Names.bbb).Name.Equals(nextTestString))
    End Sub

    <Test()> Public Sub DataSourceTest()
        Assert.AreEqual(3, testISFT.DataSource.Count)
    End Sub

    <Test()> Public Sub ListTest()
        Dim list As Generic.List(Of ISFT) = testISFT.GetList
        Assert.AreEqual(3, list.Count)
        Assert.AreSame(testISFT.FirstItem, list(0))
    End Sub

    <Test()> Public Sub SelectedItemTest()
        Dim ISFTFacade As ISFTFacade = SFTTest.GetInstance
        Assert.IsNotNull(ISFTFacade)
        ISFTFacade.SelectedItem = testISFT.FirstItem.NextItem
        Assert.IsTrue(ISFTFacade.SelectedItem Is SFTTest.bbb)
        ' get 2nd item a different way
        Assert.IsTrue(ISFTFacade.SelectedItem Is testISFT.FirstItem.NextItem)

        Dim ISFTFacade2 As ISFTFacade = SFTTest.GetInstance
        ISFTFacade2.SelectedItem = SFTTest.aaa
        Assert.IsTrue(ISFTFacade2.SelectedItem Is SFTTest.aaa)
        ' retest 1st template to demonstrate different template instances
        Assert.IsTrue(ISFTFacade.SelectedItem Is testISFT.FirstItem.NextItem)
    End Sub

    <Test()> Public Sub SelectedItemsTest()
        Dim ISFTFacade As ISFTFacade = SFTTest.GetInstance
        Assert.IsNotNull(ISFTFacade)
        ISFTFacade.SelectedItems.Add(testISFT.FirstItem.NextItem)
        Dim found As Boolean = False
        For Each ISFT As ISFT In ISFTFacade.SelectedItems
            If ISFT Is SFTTest.bbb Then
                found = True
            End If
        Next
        Assert.IsTrue(found)
    End Sub

    <Test()> Public Sub SetSelectedItemSFTTest()
        Dim ISFTFacade2 As ISFTFacade = SFTTest.GetInstance.SetSelectedItem(CType(SFTTest.bbb, ISFT))
        Assert.IsNotNull(ISFTFacade2)
        Assert.IsTrue(ISFTFacade2.SelectedItem.Name.Equals(SFTTest.bbb.Name))
    End Sub

    <Test()> Public Sub SetSelectedItemNameTest()
        Dim ISFTFacade As ISFTFacade = SFTTest.GetInstance
        ' ... by name
        ISFTFacade.SetSelectedItem(SFTTest.bbb.Name)
        Assert.AreSame(ISFTFacade.SelectedItem, SFTTest.bbb)
    End Sub

    <Test()> Public Sub SetSelectedItemKeyTest()
        Dim ISFTFacade As ISFTFacade = SFTTest.GetInstance
        ISFTFacade.SetSelectedItem(testKey + 1)
        Assert.AreSame(ISFTFacade.SelectedItem, SFTTest.bbb)
    End Sub

    <Test()> Public Sub TestTag()
        Assert.AreEqual(5, SFTTestTag.lll.Tag)
        Assert.AreEqual("good", SFTTestTag.mmm.Tag)
        Assert.AreEqual(SFTTest.bbb, SFTTestTag.nnn.Tag)
    End Sub

    <Test()> Public Sub FactoryTest()
        Dim ISFT As ISFT = SFTFacadeFactory.GetInstance.Build("SFTTest").FirstItem
        Assert.IsNotNull(ISFT)
        Assert.AreEqual(testSize, ISFT.Size)
    End Sub

    <Test()> Public Sub BaseObjectTest()
        Dim sharedString As String = "x"

        Dim child1 As New Child1
        child1.s = CObj(sharedString)
        Dim child2 As New Child2
        child2.s = CObj(sharedString)

        child1.o = child1.s
        Assert.IsNull(child2.o)
    End Sub

    <Test()> Public Sub Z_MustTestLast_ExtendedTest()
        ' demonstrate that the shared member is complete when called statically;
        Assert.IsNotNull(SFTTestExtended.hhh.Name)
        Assert.IsTrue(SFTTestExtended.hhh.Name.Equals(testStringExtendedLastItem))

        testISFTextended = SFTTestExtended.GetInstance.FirstItem
        ' test facade name
        Assert.IsTrue(GetType(SFTTestExtended).FullName.Equals(testISFTextended.FacadeName))
        ' test size
        Assert.AreEqual(testExtendedSize, testISFTextended.Size)
        ' test match methods
        Assert.IsTrue(testISFTextended.MatchKey(testISFTextended.Size - 1).Name.Equals(testStringExtendedLastItem))
        Assert.IsTrue(testISFTextended.FirstItem Is testISFT.FirstItem)

        ' test facade methods: SetSelected...SFT
        Dim ISFTFacade2 As ISFTFacade = SFTTestExtended.GetInstance.SetSelectedItem(CType(SFTTestExtended.hhh, ISFT))
        Assert.IsNotNull(ISFTFacade2)
        Assert.IsTrue(ISFTFacade2.SelectedItem.Name.Equals(testStringExtendedLastItem))

        ' test facade methods: SetSelected...Name
        Dim ISFTFacade As ISFTFacade = SFTTestExtended.GetInstance
        ISFTFacade.SetSelectedItem(nextTestString)
        Assert.AreSame(ISFTFacade.SelectedItem, SFTTestExtended.bbb)
        ISFTFacade.SetSelectedItem(testStringExtendedLastItem)
        Assert.IsTrue(ISFTFacade.SelectedItem Is SFTTestExtended.hhh)

        ' test facade methods: SetSelected...Key
        ISFTFacade = SFTTestExtended.GetInstance
        ISFTFacade.SetSelectedItem(testKey + 1)
        Assert.AreSame(ISFTFacade.SelectedItem, SFTTest.bbb)
        ISFTFacade.SetSelectedItem(testExtendedSize - 1)
        Assert.IsTrue(ISFTFacade.SelectedItem Is SFTTestExtended.hhh)
    End Sub

    <Test()> Public Sub SharedISFT()
        Assert.AreSame(SFTTest.ISFT.MatchKey(testKey), SFTTest.ISFT.MatchKey(testKey))
    End Sub

    <Test()> Public Sub Z_MustTestLast_SharedISFT()
        Assert.AreSame(SFTTestExtended.ISFT.MatchKey(extendedTestKey), SFTTestExtended.ISFT.MatchKey(extendedTestKey))
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class Base
        Public o As Object
    End Class
    Private Class Child1
        Inherits Base
        Public Shared s As Object
    End Class
    Private Class Child2
        Inherits Base
        Public Shared s As Object
    End Class
End Class
