Imports NUnit.Framework

<TestFixture()> Public Class TestSettingsFacadeTemplate

    Public Sub New()
        IO.PreLoadConfig.GetInstance.IncludeTypes()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestBuild()
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance.Build(IPsettings.GetInstance)
        settingsFacadeTemplate.ISettings.SetToDefaults()
        Assert.AreEqual(BartelsLibrary.Constants.DefaultIPPort, CType(settingsFacadeTemplate.ISettings, IPsettings).Port)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestSetToDefaultSettings()
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance.Build(IPsettings.GetInstance)
        settingsFacadeTemplate.ISettings.SetToDefaults()
        ' change setting
        Dim testPort As Int32 = 2001
        CType(settingsFacadeTemplate.ISettings, IPsettings).Port = testPort
        ' verify change 
        Assert.AreEqual(testPort, CType(settingsFacadeTemplate.ISettings, IPsettings).Port)
        ' revert to default
        settingsFacadeTemplate.SetToDefaultSettings()
        ' verify default
        Assert.AreEqual(BartelsLibrary.Constants.DefaultIPPort, CType(settingsFacadeTemplate.ISettings, IPsettings).Port)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestCloneISettings()
        Dim IPSettings As IPsettings = IPSettings.GetInstance
        IPSettings.SetToDefaults()
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance.Build(CType(IPSettings, ISettings))
        Assert.AreEqual(BartelsLibrary.Constants.DefaultIPPort, CType(settingsFacadeTemplate.ISettings, IPsettings).Port)
        ' clone
        settingsFacadeTemplate.CloneISettings()
        ' verify that clone is new instance
        Assert.AreNotSame(IPSettings, settingsFacadeTemplate.ISettings)
        Assert.AreNotSame(IPSettings, CType(settingsFacadeTemplate.ISettings, IPsettings))
        ' change a setting property
        Dim testPort As Int32 = 2002
        CType(settingsFacadeTemplate.ISettings, IPsettings).Port = testPort
        ' verify that original not changed
        Assert.AreNotEqual(IPSettings.Port, CType(settingsFacadeTemplate.ISettings, IPsettings).Port)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestClone()
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance.Build(IPsettings.GetInstance)
        settingsFacadeTemplate.ISettings.SetToDefaults()
        ' change setting
        Dim testPort As Int32 = 2003
        CType(settingsFacadeTemplate.ISettings, IPsettings).Port = testPort
        ' verify change 
        Assert.AreEqual(testPort, CType(settingsFacadeTemplate.ISettings, IPsettings).Port)
        ' clone
        Dim settingsFacadeTemplateClone As SettingsFacadeTemplate = CType(settingsFacadeTemplate.Clone, Config.SettingsFacadeTemplate)
        ' verify that clone is new instance
        Assert.AreNotSame(settingsFacadeTemplate, settingsFacadeTemplateClone)
        ' verify that setting change was copied over into the clone
        Assert.AreEqual(CType(settingsFacadeTemplate.ISettings, IPsettings).Port, CType(settingsFacadeTemplateClone.ISettings, IPsettings).Port)
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestSaveSettingsToConfig()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance.Build(IPsettings.GetInstance, "TestSettingsFacadeTemplate")
        settingsFacadeTemplate.SaveSettingsToConfig()
        ' did we get this far? can also inspect C:\Program Files\TestDriven.NET 2.0\ScopeIII.settings
        Assert.IsTrue(True)
    End Sub

    <Test()> Public Sub TestLoadSettingsFromConfig()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()
        Dim settingsFacadeTemplate As SettingsFacadeTemplate = settingsFacadeTemplate.GetInstance.Build(IPsettings.GetInstance, "TestSettingsFacadeTemplate")
        ' get whatever settings are there (default if none)
        settingsFacadeTemplate.LoadSettingsFromConfig()
        ' record setting
        Dim port As Int32 = CType(settingsFacadeTemplate.ISettings, IPsettings).Port
        ' change setting
        Dim testPort As Int32
        If port > 2000 Then
            testPort = port - 1
        Else
            testPort = port + 1
        End If
        CType(settingsFacadeTemplate.ISettings, IPsettings).Port = testPort
        ' save changed setting
        settingsFacadeTemplate.SaveSettingsToConfig()
        ' reload
        CType(settingsFacadeTemplate.ISettings, IPsettings).Port = port
        settingsFacadeTemplate.LoadSettingsFromConfig()
        ' did we save and then reload the changed setting?
        Assert.AreEqual(testPort, CType(settingsFacadeTemplate.ISettings, IPsettings).Port)
        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class

