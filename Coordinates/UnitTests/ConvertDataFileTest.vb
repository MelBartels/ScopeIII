Imports NUnit.Framework

<TestFixture()> Public Class ConvertDataFileTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestConvert()
        Dim filename As String = "TestDataFileConverterInput.txt"
        Dim observer As New observer
        ConvertDataFile.GetInstance.Convert(observer, filename)
        Dim expected As String = "00 08 45.7 23 50 16 Peg NGC 8"
        Assert.AreEqual(expected, observer.msg.Replace(vbNewLine, String.Empty))

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class observer : Implements IObserver
        Public msg As String
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements BartelsLibrary.IObserver.ProcessMsg
            If [object].GetType IsNot GetType(String) Then
                Return False
            End If
            If CStr([object]).StartsWith("0") Then
                msg = CStr([object])
            End If
        End Function
    End Class
End Class
