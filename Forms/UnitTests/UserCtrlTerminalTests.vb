Imports NUnit.Framework

<TestFixture()> Public Class UserCtrlTerminalTests

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        FormsDependencyInjector.GetInstance.UseUnitTestContainer = True
    End Sub

    <Test()> Public Sub TestUpdatePort()
        Dim port As Int32 = 4000

        Dim endoTestUserCtrlTerminalPresenter As EndoTestUserCtrlTerminalPresenter = UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenter
        endoTestUserCtrlTerminalPresenter.EndoTestPortType(IOType.TCPserver.Name)
        ' show that the current port number is something else
        Assert.IsFalse(CType(endoTestUserCtrlTerminalPresenter.IIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port.Equals(port))

        ' via settings presenter...
        endoTestUserCtrlTerminalPresenter.UpdatePortViaSettingsPresenter(port)
        ' show that the port number is updated
        Assert.IsTrue(CType(endoTestUserCtrlTerminalPresenter.IIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port.Equals(port))

        port = 4001

        ' via property grid presenter...
        endoTestUserCtrlTerminalPresenter.UpdatePortViaPropGridPresenter(port)
        ' show that the port number is updated
        Assert.IsTrue(CType(endoTestUserCtrlTerminalPresenter.IIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port.Equals(port))

        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestTerminalSerialOpenClose()
        Dim endoTestUserCtrlTerminalPresenter As EndoTestUserCtrlTerminalPresenter = UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenter
        endoTestUserCtrlTerminalPresenter.EndoTestPortType(IOType.SerialPort.Name)

        ' test closed/open/close
        Assert.IsFalse(endoTestUserCtrlTerminalPresenter.IIO.isOpened)
        endoTestUserCtrlTerminalPresenter.EndoTestToggleOpenClose()
        Assert.IsTrue(endoTestUserCtrlTerminalPresenter.IIO.isOpened)
        endoTestUserCtrlTerminalPresenter.EndoTestToggleOpenClose()
        Assert.IsFalse(endoTestUserCtrlTerminalPresenter.IIO.isOpened)

        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestTerminalTCPOpenClose()
        Dim port As Int32 = 4002

        Dim endoTestUserCtrlTerminalPresenter As EndoTestUserCtrlTerminalPresenter = UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenter(IOType.TCPserver.Name, port)
        ' test open/close
        endoTestUserCtrlTerminalPresenter.EndoTestToggleOpenClose()
        Assert.IsTrue(endoTestUserCtrlTerminalPresenter.IIO.isOpened)
        endoTestUserCtrlTerminalPresenter.EndoTestToggleOpenClose()
        Assert.IsFalse(endoTestUserCtrlTerminalPresenter.IIO.isOpened)

        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestLoggingFilenameUpdatedWhenSettingsChanged()
        Dim port As Int32 = 4003

        Dim endoTestUserCtrlTerminalPresenter As EndoTestUserCtrlTerminalPresenter = UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenter
        endoTestUserCtrlTerminalPresenter.EndoTestPortType(IOType.TCPserver.Name)
        ' show that the current port number is something else
        Assert.IsFalse(CType(endoTestUserCtrlTerminalPresenter.IIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port.Equals(port))
        Dim oldFilename As String = System.IO.Path.GetFullPath(endoTestUserCtrlTerminalPresenter.IIO.IOLoggingFacade.LoggingObserver.Filename)
        Debug.WriteLine("old filename " & oldFilename)
        endoTestUserCtrlTerminalPresenter.UpdatePortViaSettingsPresenter(port)
        ' show that the port number is updated
        Assert.IsTrue(CType(endoTestUserCtrlTerminalPresenter.IIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port.Equals(port))
        Dim newFilename As String = System.IO.Path.GetFullPath(endoTestUserCtrlTerminalPresenter.IIO.IOLoggingFacade.LoggingObserver.Filename)
        ' show that the filename is updated
        Debug.WriteLine("new filename " & newFilename)
        Assert.IsFalse(newFilename.Equals(oldFilename))
        Assert.IsTrue(newFilename.Contains(CStr(port)))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestLoggingActive()
        Dim port As Int32 = 4004

        Dim endoTestUserCtrlTerminalPresenter As EndoTestUserCtrlTerminalPresenter = UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenter(IOType.TCPserver.Name, port)
        Dim logFilename As String = System.IO.Path.GetFullPath(endoTestUserCtrlTerminalPresenter.IIO.IOLoggingFacade.LoggingObserver.Filename)
        ' show that the filename is updated
        Debug.WriteLine("new filename " & logFilename)
        Assert.IsTrue(logFilename.Contains(CStr(port)))

        ' kill any preexisting file
        CType(New TestDeleteFile, TestDeleteFile).Delete(logFilename)

        ' activate logging
        endoTestUserCtrlTerminalPresenter.EndoTestLogging(True)
        Assert.IsTrue(endoTestUserCtrlTerminalPresenter.IIO.IOLoggingFacade.LoggingObserver.IsOpen)
        ' turn off logging
        endoTestUserCtrlTerminalPresenter.EndoTestLogging(False)
        Assert.IsFalse(endoTestUserCtrlTerminalPresenter.IIO.IOLoggingFacade.LoggingObserver.IsOpen)
        ' verify file exists
        Assert.IsTrue(System.IO.File.Exists(logFilename))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestTalkingTerminals()
        Dim port As Int32 = 4005
        TalkingTerminals(port, _
                         UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenter(IOType.TCPserver.Name, port), _
                         UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenter(IOType.TCPclient.Name, port))

        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestTalkingTerminalsWithIOPresenterDevice()
        Dim port As Int32 = 4006
        TalkingTerminals(port, _
                         UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenterWithIOPresenterDevice(IOType.TCPserver.Name, port), _
                         UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenterWithIOPresenterDevice(IOType.TCPclient.Name, port))

        Assert.IsTrue(True)
    End Sub

    Public Sub TalkingTerminals(ByVal port As Int32, ByRef uctp1 As EndoTestUserCtrlTerminalPresenter, ByRef uctp2 As EndoTestUserCtrlTerminalPresenter)
        ' build first terminal
        Dim log1 As String = System.IO.Path.GetFullPath(uctp1.IIO.IOLoggingFacade.LoggingObserver.Filename)
        CType(New TestDeleteFile, TestDeleteFile).Delete(log1)
        uctp1.EndoTestLogging(True)
        uctp1.EndoTestToggleOpenClose()
        Threading.Thread.Sleep(500)

        ' build second terminal
        Dim log2 As String = System.IO.Path.GetFullPath(uctp2.IIO.IOLoggingFacade.LoggingObserver.Filename)
        CType(New TestDeleteFile, TestDeleteFile).Delete(log2)
        uctp2.EndoTestLogging(True)
        uctp2.EndoTestToggleOpenClose()
        Threading.Thread.Sleep(500)

        ' send msg from term 1 to term 2
        Dim msg1 As String = "hello from terminal 1"
        uctp1.EndoTestSendText(msg1)
        Threading.Thread.Sleep(500)

        ' send msg from term 2 to term 1
        Dim msg2 As String = "hello from terminal 2"
        uctp2.EndoTestSendText(msg2)
        Threading.Thread.Sleep(500)

        ' close ports, logging
        uctp2.EndoTestToggleOpenClose()
        uctp2.EndoTestLogging(False)
        uctp1.EndoTestToggleOpenClose()
        uctp1.EndoTestLogging(False)

        ' check contents of log files: each should have a xmt and rec
        Dim fileContents1 As String = My.Computer.FileSystem.ReadAllText(log1)
        Assert.IsTrue(fileContents1.IndexOf(IOState.Xmt.Description) > -1)
        Assert.IsTrue(fileContents1.IndexOf(IOState.Rec.Description) > -1)
        Dim fileContents2 As String = My.Computer.FileSystem.ReadAllText(log2)
        Assert.IsTrue(fileContents2.IndexOf(IOState.Xmt.Description) > -2)
        Assert.IsTrue(fileContents2.IndexOf(IOState.Rec.Description) > -2)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
        FormsDependencyInjector.GetInstance.UseUnitTestContainer = False
    End Sub

End Class

