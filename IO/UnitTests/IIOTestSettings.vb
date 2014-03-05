Imports NUnit.Framework

<TestFixture()> Public Class IIOTestSettings

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestSettings()
        ScopeIII.IO.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
        Dim IIO As IIO = TCPserverFacade.GetInstance
        IIO.SettingsName = "TestSettings"
        IIO.LoadSettings()
        ' change the port # for each test
        Dim testPort As Int32 = CType(IIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port + 1
        If testPort >= Int16.MaxValue Then
            testPort = 1
        End If
        CType(IIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port = testPort
        IIO.SaveSettings()
        ' see if we get this far
        Assert.IsTrue(True)
        IIO.LoadSettings()
        ' test saved port #
        Assert.AreEqual(testPort, CType(IIO.IOSettingsFacadeTemplate.ISettings, IPsettings).Port)
    End Sub

    <Test()> Public Sub TestCloning()
        ' start w/ default facade
        Dim defaultFacade As SettingsFacadeTemplate = SettingsFacadeTemplate.GetInstance.Build(IPsettings.GetInstance, GetType(IPsettings).Name)
        defaultFacade.SetToDefaultSettings()
        ' now clone it
        Dim facadeClone As SettingsFacadeTemplate = CType(defaultFacade.Clone, SettingsFacadeTemplate)
        ' vary the clone
        Dim testPort As Int32 = 5555
        CType(facadeClone.ISettings, IPsettings).Port = testPort
        ' test that the change propogated through the clone
        Assert.AreEqual(testPort, CType(facadeClone.ISettings, IPsettings).Port)
        ' test that the change starting default facade did not change
        Assert.IsFalse(CType(defaultFacade.ISettings, IPsettings).Port.Equals(CType(facadeClone.ISettings, IPsettings).Port))
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
