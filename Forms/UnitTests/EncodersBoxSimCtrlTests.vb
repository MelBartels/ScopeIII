Imports NUnit.Framework

<TestFixture()> Public Class EncodersBoxSimCtrlTests

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        FormsDependencyInjector.GetInstance.UseUnitTestContainer = True
    End Sub

    <Test()> Sub TestSimComm()
        Dim testPort As Int32 = 2020

        ' build simulator
        Dim endoTestEncodersBoxSimPresenter As EndoTestEncodersBoxSimPresenter = CType(FormsDependencyInjector.GetInstance.IEncodersBoxSimPresenterFactory, Forms.EndoTestEncodersBoxSimPresenter)
        ' set form before user control
        endoTestEncodersBoxSimPresenter.IMVPView = New FrmEncodersBoxSim
        ' build terminal presenter
        Dim simTermPresenter As EndoTestUserCtrlTerminalPresenter = endoTestEncodersBoxSimPresenter.EndoTestUserCtrlTerminalPresenter
        simTermPresenter.EndoTestUserCtrlTerminal.FireEvents = False
        ' update port
        simTermPresenter.EndoTestPortType(IOType.TCPserver.Name)
        simTermPresenter.UpdatePortViaSettingsPresenter(testPort)
        ' logging
        Dim simFilename As String = System.IO.Path.GetFullPath(simTermPresenter.IIO.IOLoggingFacade.LoggingObserver.Filename)
        Debug.WriteLine("simulator logging filename " & simFilename)
        CType(New TestDeleteFile, TestDeleteFile).Delete(simFilename)
        simTermPresenter.EndoTestLogging(True)
        ' open port
        simTermPresenter.EndoTestToggleOpenClose()
        Assert.IsTrue(simTermPresenter.IIO.isOpened)
        Threading.Thread.Sleep(500)

        ' build test terminal
        Dim endoTestUserCtrlTerminalPresenter As EndoTestUserCtrlTerminalPresenter = UserCtrlTerminalPresenterTestBuilder.GetInstance.BuildTestPresenter(IOType.TCPclient.Name, testPort)
        Dim termFilename As String = System.IO.Path.GetFullPath(endoTestUserCtrlTerminalPresenter.IIO.IOLoggingFacade.LoggingObserver.Filename)
        Debug.WriteLine("terminal logging filename " & termFilename)
        CType(New TestDeleteFile, TestDeleteFile).Delete(termFilename)
        endoTestUserCtrlTerminalPresenter.EndoTestLogging(True)
        endoTestUserCtrlTerminalPresenter.EndoTestToggleOpenClose()
        Assert.IsTrue(endoTestUserCtrlTerminalPresenter.IIO.isOpened)
        Threading.Thread.Sleep(500)

        ' set starting encoder values
        Dim testEncoderPriValue As Int32 = 100
        Dim testEncoderSecValue As Int32 = 200
        endoTestEncodersBoxSimPresenter.SetEncoderValues(testEncoderPriValue, testEncoderSecValue)

        ' for testing by injecting data directly into the IO
        ' simTermPresenter.IIO.QueueReceiveBytes(CType(New UTF8Encoding, UTF8Encoding).GetBytes("Q"))
        ' Threading.Thread.Sleep(500)

        ' send query
        endoTestUserCtrlTerminalPresenter.IIO.Send("Q")
        Threading.Thread.Sleep(500)

        ' terminal shutdown
        endoTestUserCtrlTerminalPresenter.EndoTestToggleOpenClose()
        Assert.IsFalse(endoTestUserCtrlTerminalPresenter.IIO.isOpened)
        endoTestUserCtrlTerminalPresenter.EndoTestLogging(False)

        ' sim shutdown
        simTermPresenter.EndoTestToggleOpenClose()
        Assert.IsFalse(simTermPresenter.IIO.isOpened)
        simTermPresenter.EndoTestLogging(False)

        ' log files exists?
        Assert.IsTrue(System.IO.File.Exists(termFilename))
        Assert.IsTrue(System.IO.File.Exists(simFilename))
        ' check contents of term log file: does the test encoder value exist?
        Dim fileContents1 As String = My.Computer.FileSystem.ReadAllText(termFilename)
        Assert.IsTrue(fileContents1.IndexOf(CStr(testEncoderPriValue)) > -1)

        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestControllerSimulatorTalk()
        Dim testPort As Int32 = 2021

        ' build simulator
        Dim endoTestEncodersBoxSimPresenter As EndoTestEncodersBoxSimPresenter = CType(FormsDependencyInjector.GetInstance.IEncodersBoxSimPresenterFactory, Forms.EndoTestEncodersBoxSimPresenter)
        ' set form before user control
        endoTestEncodersBoxSimPresenter.IMVPView = New FrmEncodersBoxSim
        ' build terminal presenter
        Dim simTermPresenter As EndoTestUserCtrlTerminalPresenter = endoTestEncodersBoxSimPresenter.EndoTestUserCtrlTerminalPresenter
        simTermPresenter.EndoTestUserCtrlTerminal.FireEvents = False
        ' update port
        simTermPresenter.EndoTestPortType(IOType.TCPserver.Name)
        simTermPresenter.UpdatePortViaSettingsPresenter(testPort)
        ' logging
        Dim simFilename As String = System.IO.Path.GetFullPath(simTermPresenter.IIO.IOLoggingFacade.LoggingObserver.Filename)
        Debug.WriteLine("simulator logging filename " & simFilename)
        CType(New TestDeleteFile, TestDeleteFile).Delete(simFilename)
        simTermPresenter.EndoTestLogging(True)
        ' open port
        simTermPresenter.EndoTestToggleOpenClose()
        Assert.IsTrue(simTermPresenter.IIO.isOpened)
        Threading.Thread.Sleep(500)

        ' build controller
        Dim endoTestEncodersBoxCtrlPresenter As EndoTestEncodersBoxCtrlPresenter = CType(FormsDependencyInjector.GetInstance.IEncodersBoxCtrlPresenterFactory, Forms.EndoTestEncodersBoxCtrlPresenter)
        ' set form before user control
        endoTestEncodersBoxCtrlPresenter.IMVPView = New FrmEncodersBoxCtrl
        ' build terminal presenter
        Dim ctrlTermPresenter As EndoTestUserCtrlTerminalPresenter = endoTestEncodersBoxCtrlPresenter.EndoTestUserCtrlTerminalPresenter
        ctrlTermPresenter.EndoTestUserCtrlTerminal.FireEvents = False
        ' ctrl logging
        endoTestEncodersBoxCtrlPresenter.StartLogging()
        ' update port
        ctrlTermPresenter.EndoTestPortType(IOType.TCPclient.Name)
        ctrlTermPresenter.UpdatePortViaSettingsPresenter(testPort)
        ' logging
        Dim ctrlFilename As String = System.IO.Path.GetFullPath(ctrlTermPresenter.IIO.IOLoggingFacade.LoggingObserver.Filename)
        Debug.WriteLine("controller logging filename " & ctrlFilename)
        CType(New TestDeleteFile, TestDeleteFile).Delete(ctrlFilename)
        ctrlTermPresenter.EndoTestLogging(True)
        ' open port
        ctrlTermPresenter.EndoTestToggleOpenClose()
        Assert.IsTrue(ctrlTermPresenter.IIO.isOpened)
        Threading.Thread.Sleep(500)

        ' set starting encoder values
        Dim testEncoderPriValue As Int32 = 3000
        Dim testEncoderSecValue As Int32 = 6000
        endoTestEncodersBoxSimPresenter.SetEncoderValues(testEncoderPriValue, testEncoderSecValue)

        ' send query
        endoTestEncodersBoxCtrlPresenter.EndoTestSendQueryCmd()
        Threading.Thread.Sleep(1000)

        ' verify that AxisEncoderAdapter observed the changed encoder values
        Assert.AreEqual(testEncoderPriValue, eMath.RInt(endoTestEncodersBoxCtrlPresenter.AxisEncoderTranslatorPriValue))
        Assert.AreEqual(testEncoderSecValue, eMath.RInt(endoTestEncodersBoxCtrlPresenter.AxisEncoderTranslatorSecValue))

        ' controller shutdown
        ctrlTermPresenter.EndoTestToggleOpenClose()
        Assert.IsFalse(ctrlTermPresenter.IIO.isOpened)
        ctrlTermPresenter.EndoTestLogging(False)
        ' ctrl logging
        endoTestEncodersBoxCtrlPresenter.StopLogging()

        ' sim shutdown
        simTermPresenter.EndoTestToggleOpenClose()
        Assert.IsFalse(simTermPresenter.IIO.isOpened)
        simTermPresenter.EndoTestLogging(False)

        ' log files exists?
        Assert.IsTrue(System.IO.File.Exists(ctrlFilename))
        Assert.IsTrue(System.IO.File.Exists(simFilename))
        Assert.IsTrue(System.IO.File.Exists(endoTestEncodersBoxCtrlPresenter.LogFilename))
        ' check contents of controller term log file: do the test encoder values exist?
        Dim fileContents1 As String = My.Computer.FileSystem.ReadAllText(ctrlFilename)
        Assert.IsTrue(fileContents1.IndexOf(CStr(testEncoderPriValue)) > -1)
        Assert.IsTrue(fileContents1.IndexOf(CStr(testEncoderSecValue)) > -1)
        ' check contents of controller log file: do the test encoder values exist?
        Dim fileContents2 As String = My.Computer.FileSystem.ReadAllText(endoTestEncodersBoxCtrlPresenter.LogFilename)
        Assert.IsTrue(fileContents2.IndexOf(CStr(testEncoderPriValue)) > -1)
        Assert.IsTrue(fileContents2.IndexOf(CStr(testEncoderSecValue)) > -1)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
        FormsDependencyInjector.GetInstance.UseUnitTestContainer = False
    End Sub

End Class

