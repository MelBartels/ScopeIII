Imports NUnit.Framework

<TestFixture()> Public Class TestIOBytes

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub ViaTCP()
        Dim port As Int32 = 5000

        Dim xmtIIO As IIO = IOBuilder.GetInstance.Build(CType(IOType.TCPserver, ISFT))
        CType(xmtIIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port = port

        Dim recIIO As IIO = IOBuilder.GetInstance.Build(CType(IOType.TCPclient, ISFT))
        CType(recIIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port = port

        xmtRecInt32sAsBytes(xmtIIO, recIIO)

        Assert.IsTrue(True)
    End Sub

    '<Test(), Ignore("COM1 must be connected to COM4")> Public Sub ViaSerial()
    <Test()> Public Sub ViaSerial()

        Dim portA As String = "COM1"
        Dim portB As String = "COM4"

        Dim portNames() As String = System.IO.Ports.SerialPort.GetPortNames
        Dim portNameFound As String = Array.Find(portNames.ToArray, AddressOf New FuncDelegate(Of String, String) _
                                                 (AddressOf portNameMatch, portB).CallDelegate)
        If String.IsNullOrEmpty(portNameFound) Then
            Assert.Ignore(portB & " not available")
            Exit Sub
        End If

        Dim xmtIIO As IIO = IOBuilder.GetInstance.Build(CType(IOType.SerialPort, ISFT))
        CType(xmtIIO.IOSettingsFacadeTemplate.ISettings, SerialSettings).Portname = portA

        Dim recIIO As IIO = IOBuilder.GetInstance.Build(CType(IOType.SerialPort, ISFT))
        CType(recIIO.IOSettingsFacadeTemplate.ISettings, SerialSettings).Portname = portB

        xmtRecInt32sAsBytes(xmtIIO, recIIO)

        Assert.IsTrue(True)
    End Sub

    Private Function portNameMatch(ByVal portName As String, ByVal match As String) As Boolean
        Return portName.Equals(match)
    End Function

    Private Sub xmtRecInt32sAsBytes(ByVal xmtIIO As IIO, ByVal recIIO As IIO)
        xmtIIO.Open()
        Threading.Thread.Sleep(500)
        recIIO.Open()
        Threading.Thread.Sleep(500)

        Dim recObserver As New recObserver
        recIIO.ReceiveObservers.Attach(CType(recObserver, IObserver))

        Dim xmtBytes(7) As Byte
        Dim xmtTest1 As Int32 = 1234    ' 0-0-4-210
        Dim xmtTest2 As Int32 = 9876    ' 0-0-38-148
        Array.Copy(BitConverter.GetBytes(xmtTest1), 0, xmtBytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(xmtTest2), 0, xmtBytes, 4, 4)
        xmtIIO.Send(xmtBytes)
        Threading.Thread.Sleep(500)

        ' processed observed received string
        Dim recBytes() As Byte = BartelsLibrary.Encoder.StringtoBytes(recObserver.SB.ToString)
        Assert.AreEqual(8, recBytes.Length)
        Dim recTest1 As Int32 = BitConverter.ToInt32(recBytes, 0)
        Dim recTest2 As Int32 = BitConverter.ToInt32(recBytes, 4)
        Assert.AreEqual(xmtTest1, recTest1)
        Assert.AreEqual(xmtTest2, recTest2)

        recIIO.Close()
        xmtIIO.Close()
        Threading.Thread.Sleep(500)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub

    Private Class recObserver : Implements IObserver
        Public SB As New Text.StringBuilder
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements BartelsLibrary.IObserver.ProcessMsg
            ' serial port <-> usb adapter can kick out data in several small chunks
            SB.Append(CStr([object]))
        End Function
    End Class
End Class
