Imports NUnit.Framework
Imports System.Collections.Generic

<TestFixture()> Public Class TestPredicate

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub Predicates()
        Dim nums() As Int32 = {1, 2, 3, 4, 5}
        Dim strs() As String = {"a", "b", "c", "d", "e"}

        ' .Find
        Dim num_5 As Int32 = Array.Find(nums, AddressOf arrayTestInt)
        Assert.AreEqual(nums(nums.Length - 1), num_5)

        Dim str_5 As String = Array.Find(strs, AddressOf arrayTestStr)
        Assert.AreEqual(strs(strs.Length - 1), str_5)

        ' .ForEach
        Debug.WriteLine(Environment.NewLine & "displayInts")
        Array.ForEach(nums, AddressOf displayInts)
        Debug.WriteLine(Environment.NewLine & "displayStrs")
        Array.ForEach(strs, AddressOf displayStrs)

        ' .FindAll using predicate and reusing .ForEach
        Dim predicate As New Predicate(Of Int32)(AddressOf lessThan3)
        Dim allNums() As Int32 = Array.FindAll(nums, predicate)
        Debug.WriteLine(Environment.NewLine & "displayInts")
        Array.ForEach(allNums, AddressOf displayInts)

        ' Actions and Predicates
        Dim match As New Predicate(Of Int32)(AddressOf match345)
        Dim action As New Action(Of Int32)(AddressOf displayInts)
        allNums = Array.FindAll(nums, match)
        Debug.WriteLine(Environment.NewLine & "displayInts")
        Array.ForEach(allNums, action)

        ' converters
        Dim allStrs() As String = Array.ConvertAll(Of Int32, String)(allNums, AddressOf convertInt32ToString)
        Debug.WriteLine(Environment.NewLine & "displayStrs")
        Array.ForEach(allStrs, AddressOf displayStrs)

        'output from immediate window
        'displayInts
        '12345
        'displayStrs
        'abcde
        'displayInts
        '12
        'displayInts
        '345
        'displayStrs
        '345

        Assert.IsTrue(True)
    End Sub

    Private Function arrayTestInt(ByVal [int32] As Int32) As Boolean
        Return [int32].Equals(5)
    End Function

    Private Function arrayTestStr(ByVal [string] As String) As Boolean
        Return [string].Equals("e")
    End Function

    Private Function lessThan3(ByVal [int32] As Int32) As Boolean
        Return [int32] < 3
    End Function

    Private Function match345(ByVal [int32] As Int32) As Boolean
        Return [int32].Equals(3) OrElse [int32].Equals(4) OrElse [int32].Equals(5)
    End Function

    Private Function convertInt32ToString(ByVal [int32] As Int32) As String
        Return [int32].ToString
    End Function

    Private Sub displayInts(ByVal [integer] As Integer)
        Debug.Write([Integer].ToString)
    End Sub

    Private Sub displayStrs(ByVal [string] As String)
        Debug.Write([string])
    End Sub

    ' predicates with extra arguments, the arguments or parameters being the 'context'

    ' FindAll predicate example

    <Test()> Public Sub AdvancedPredicateFindAll()
        Dim testArray As ArrayList = buildTestArray()

        ' simple predicate test to verify the groundwork
        Dim cells() As Object = Array.FindAll(testArray.ToArray, AddressOf findCellByText)
        Assert.AreEqual(2, cells.Length)
        Assert.AreEqual(testText, CType(cells(0), cell).Text)
        Assert.AreEqual(testText, CType(cells(1), cell).Text)

        ' advanced predicate with context:
        ' send in the findAll method that uses the context (findCellByTextByNum) 
        Dim advPredicate As FuncDelegate(Of Object, Object) = _
                New FuncDelegate(Of Object, Object)(AddressOf findCellByTextByNum, testNum)
        ' call the advanced predicate's delegate which wraps the context evaluation within the standard FindAll predicate
        cells = Array.FindAll(testArray.ToArray, AddressOf advPredicate.CallDelegate)

        Assert.AreEqual(1, cells.Length)
        Assert.AreEqual(testText, CType(cells(0), cell).Text)
        Assert.AreEqual(testNum, CType(cells(0), cell).Num)
    End Sub

    Private testText As String = "hello"
    Private testNum As Int32 = 2

    Private Function buildTestArray() As ArrayList
        Dim testArray As New ArrayList

        testArray.Add(New cell(testText, 1))
        testArray.Add(New cell(testText, testNum))
        testArray.Add(New cell("goodbye", 3))

        Return testArray
    End Function

    Private Class cell
        Public Sub New(ByVal text As String, ByVal num As Int32)
            Me.Text = text
            Me.Num = num
        End Sub
        Public Text As String
        Public Num As Int32
    End Class

    Private Function findCellByText(ByVal cell As Object) As Boolean
        Return CType(cell, cell).Text.Equals(testText)
    End Function

    Private Function findCellByTextByNum(ByVal cell As Object, ByVal num As Object) As Boolean
        Return findCellByText(cell) AndAlso CType(cell, cell).Num.Equals(CType(num, Int32))
    End Function

    <Test()> Public Sub FuncPredicateTest()
        Dim cellList As New List(Of cell)
        cellList.Add(New cell(String.Empty, Int32.MaxValue))
        cellList.Add(New cell(testText, testNum))
        cellList.Add(New cell(String.Empty, Int32.MaxValue))
        cellList.Add(New cell(testText, testNum))
        cellList.Add(New cell(String.Empty, Int32.MaxValue))

        Dim cellsFound As List(Of cell) = cellList.FindAll(AddressOf New FuncDelegate2(Of cell, String, Int32) _
            (AddressOf matchCellByTextAndNum, testText, testNum).CallDelegate)

        Assert.AreEqual(2, cellsFound.Count)
    End Sub

    Private Function matchCellByTextAndNum(ByVal cell As cell, ByVal text As String, ByVal num As Int32) As Boolean
        Return cell.Text.Equals(text) AndAlso cell.Num.Equals(num)
    End Function

    <Test()> Public Sub SubPredicateTest()
        Dim intList As New List(Of Int32)
        intList.Add(2)
        intList.Add(3)
        Dim resultList As New List(Of Int32)

        intList.ForEach(AddressOf New SubDelegate2(Of Int32, Int32, List(Of Int32))(AddressOf multiplyNums, 2, resultList).CallDelegate)

        Assert.AreEqual(2, resultList.Count)
        Assert.AreEqual(4, resultList(0))
        Assert.AreEqual(6, resultList(1))
    End Sub

    Private Sub multiplyNums(ByVal num As Int32, ByVal multiplier As Int32, ByVal resultList As List(Of Int32))
        resultList.Add(num * multiplier)
    End Sub

    ' ForEach action example

    <Test()> Public Sub AdvancedPredicateForEach()
        Dim testArray As ArrayList = buildTestArray()
        Assert.AreEqual(3, testArray.Count)

        Dim contextArray As New ArrayList

        ' advanced predicate with context:
        ' send in the forEach method that uses the context (addValueToArray)
        Dim advPredicate As SubDelegate(Of Object, Object) = _
                New SubDelegate(Of Object, Object)(AddressOf addValueToArray, contextArray)
        ' call the advanced predicate's delegate which wraps the context evaluation within the standard ForEach predicate
        Array.ForEach(testArray.ToArray, AddressOf advPredicate.CallDelegate)

        Assert.AreEqual(3, contextArray.Count)

        ' or compressed coding
        contextArray = New ArrayList
        Array.ForEach(testArray.ToArray, AddressOf New SubDelegate(Of Object, Object)(AddressOf addValueToArray, contextArray).CallDelegate)
        Assert.AreEqual(3, contextArray.Count)
    End Sub

    Private Sub addValueToArray(ByVal value As Object, ByVal array As Object)
        CType(array, ArrayList).Add(value)
    End Sub

    <Test()> Public Sub GenericListActions()
        ' create the list
        Dim genericListTestClasses As New List(Of genericListTestClass)
        genericListTestClasses.Add(New genericListTestClass(1))
        genericListTestClasses.Add(New genericListTestClass(2))
        genericListTestClasses.Add(New genericListTestClass(3))
        ' inline foreach action
        pGenericListTestClassSum = 0
        genericListTestClasses.ForEach(New Action(Of genericListTestClass)(AddressOf sumGenericListTestClass))
        Assert.AreEqual(6, pGenericListTestClassSum)
    End Sub

    Private Sub sumGenericListTestClass(ByVal genericListTestClass As genericListTestClass)
        pGenericListTestClassSum += genericListTestClass.Number
    End Sub

    Private pGenericListTestClassSum As Integer
    Private Class genericListTestClass
        Public Number As Integer
        Public Sub New(ByVal number As Integer)
            Me.Number = number
        End Sub
    End Class

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
