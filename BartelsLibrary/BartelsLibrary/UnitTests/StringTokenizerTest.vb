Imports NUnit.Framework

<TestFixture()> Public Class StringTokenizerTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestStringTokenizer()
        Dim testStr As String = " 1.2   3.4 5.6   7.8  "
        Dim st As StringTokenizer = StringTokenizer.GetInstance
        st.Tokenize(testStr)
        Assert.AreEqual(4, st.Count)
        Assert.AreEqual("1.2", st.NextToken)
        Assert.AreEqual(3.4, st.GetNextDouble)
        Assert.AreEqual("5.6", st.NextToken)

        testStr = " 1.2 /  3.4 /5.6/   7.8/  "
        st.Tokenize(testStr, " /".ToCharArray)
        Assert.AreEqual(4, st.Count)
        Assert.AreEqual("1.2", st.NextToken)
        Assert.AreEqual(3.4, st.GetNextDouble)
        Assert.AreEqual("5.6", st.NextToken)

        testStr = " 1.2 + 3.4 -5.6 + 7 -8"
        st.Tokenize(testStr)
        Assert.AreEqual(1.2, st.GetNextDouble)
        Assert.AreEqual(3.4, st.GetNextDouble)
        Assert.AreEqual(-5.6, st.GetNextDouble)
        Assert.AreEqual(7, st.GetNextInt32)
        Assert.AreEqual(-8, st.GetNextInt32)

        testStr = "123.45 67.8 xxx  9"
        st.Tokenize(testStr)
        Assert.AreEqual(3, st.GetCountDoubles)
    End Sub

    <Test()> Public Sub StripTrailingCrNotNecessary()
        Dim testString As String = "1234" & vbCr
        Dim testInt32 As Int32
        Assert.IsTrue(Int32.TryParse(testString, testInt32))
        testString = "12.34" & vbCr
        Dim testDouble As Double
        Assert.IsTrue(Double.TryParse(testString, testDouble))
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
