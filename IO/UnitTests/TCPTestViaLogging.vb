Imports NUnit.Framework

<TestFixture()> Public Class TCPTestViaLogging

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestTCP()
        Dim port As Int32 = 3000

        Dim IIOserver As IIO = IOBuilder.GetInstance.Build(CType(IOType.TCPserver, ISFT))
        CType(IIOserver.IOSettingsFacadeTemplate.ISettings, IPsettings).Port = port
        IIOserver.IOLoggingFacade.SetIOLoggingFilenameToPortname()

        ' kill any preexisting file
        CType(New TestDeleteFile, TestDeleteFile).Delete(IIOserver.IOLoggingFacade.LoggingObserver.Filename)

        IIOserver.IOobservers.DisplayAsHex = True
        IIOserver.IOLoggingFacade.Open()
        IIOserver.Open()
        Threading.Thread.Sleep(500)

        Dim IIOclient As IIO = IOBuilder.GetInstance.Build(CType(IOType.TCPclient, ISFT))
        CType(IIOclient.IOSettingsFacadeTemplate.ISettings, IPsettings).Port = port
        IIOclient.IOLoggingFacade.SetIOLoggingFilenameToPortname()
        IIOclient.IOobservers.DisplayAsHex = True
        IIOclient.IOLoggingFacade.Open()
        IIOclient.Open()

        Threading.Thread.Sleep(500)

        Dim bs() As Byte = {70, 71, 72}
        IIOserver.Send(bs)
        Threading.Thread.Sleep(500)

        Dim br() As Byte = {80, 81, 82}
        IIOclient.Send(br)
        Threading.Thread.Sleep(500)

        IIOclient.Close()
        IIOclient.IOLoggingFacade.Close()
        IIOserver.Close()
        IIOserver.IOLoggingFacade.Close()

        ' server file:
        'Status: Opening port IO_TCPserver_127.0.0.1_2000
        'Status: Connected.
        'Xmt: x46(F) x47(G) x48(H) 
        'Rec: x50(P) x51(Q) x52(R) 
        'Status: Close Port

        ' (should NOT see more than one Status: Connected.: if so, port opened 2x w/ data going to now lost 1st ip socket
        ' client file: 
        'Status: Opening port IO_TCPclient_127.0.0.1_2000
        'Status: Connected.
        'Rec: x46(F) x47(G) x48(H) 
        'Xmt: x50(P) x51(Q) x52(R) 
        'Status: Close Port

        Threading.Thread.Sleep(1000)

        Debug.WriteLine("opening for reading " & System.IO.Path.GetFullPath(IIOserver.IOLoggingFacade.LoggingObserver.Filename))
        Dim reader As New System.IO.StreamReader(IIOserver.IOLoggingFacade.LoggingObserver.Filename)
        Dim line As String = String.Empty
        Do
            line = reader.ReadLine
        Loop While line.IndexOf(IOState.Xmt.Description).Equals(-1)
        Assert.IsTrue(line.Equals(IOState.Xmt.Description & "x46(F) x47(G) x48(H) "))
        Do
            line = reader.ReadLine
        Loop While line.IndexOf(IOState.Rec.Description).Equals(-1)
        Assert.IsTrue(line.Equals(IOState.Rec.Description & "x50(P) x51(Q) x52(R) "))
        reader.Close()
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
