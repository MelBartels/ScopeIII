Imports NUnit.Framework

<TestFixture()> Public Class SerialPortTestViaLogging

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestSerialPort()
        Dim IIOSerialPort As IIO = IOBuilder.GetInstance.Build(CType(IOType.SerialPort, ISFT))
        IIOSerialPort.IOobservers.DisplayAsHex = True
        IIOSerialPort.IOLoggingFacade.Open()
        IIOSerialPort.Open()

        Threading.Thread.Sleep(1000)
        Dim bs() As Byte = {70, 71, 72}
        IIOSerialPort.Send(bs)
        Threading.Thread.Sleep(500)
        IIOSerialPort.Close()
        IIOSerialPort.IOLoggingFacade.Close()

        ' log file contents:
        '
        'Status: Opening port IO_SerialPort_COM1_9600
        'Xmt: x46(F) x47(G) x48(H) 
        'Status: Close Port

        Debug.WriteLine("opening for reading " & System.IO.Path.GetFullPath(IIOSerialPort.IOLoggingFacade.LoggingObserver.Filename))
        Dim reader As New System.IO.StreamReader(IIOSerialPort.IOLoggingFacade.LoggingObserver.Filename)
        Dim line As String = String.Empty
        Do
            line = reader.ReadLine
        Loop While line.IndexOf(IOState.Xmt.Description).Equals(-1)
        Assert.IsTrue(line.Equals(IOState.Xmt.Description & "x46(F) x47(G) x48(H) "))
        reader.Close()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
