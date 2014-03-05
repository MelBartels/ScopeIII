Imports NUnit.Framework

<TestFixture()> Public Class SettingsPresenterTests

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        FormsDependencyInjector.GetInstance.UseUnitTestContainer = True
    End Sub

    <Test()> Sub TestFakedView()
        Dim settingsPresenter As SettingsPresenter = SettingsPresenterBuilder.GetInstance.Build(CType(BartelsLibrary.SettingsType.IP, ISFT))
        Assert.IsInstanceOfType(GetType(IPsettings), settingsPresenter.SettingsFacadeTemplate.ISettings)
        Assert.IsInstanceOfType(GetType(EndoTestFrmShowSettings), settingsPresenter.IMVPView)

        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestUpdateSettings()
        Dim settingsPresenter As SettingsPresenter = SettingsPresenterBuilder.GetInstance.Build(CType(BartelsLibrary.SettingsType.IP, ISFT))
        Dim endoTestPropGridPresenter As EndoTestPropGridPresenter = CType(settingsPresenter.IPropGridPresenter, EndoTestPropGridPresenter)
        Assert.IsNotNull(endoTestPropGridPresenter.ISettingsFacadeClone)
        Dim DevicesSettings As IPsettings = CType(endoTestPropGridPresenter.SettingsFacadeTemplate.ISettings, IPsettings)
        Dim clonedDevicesSettings As IPsettings = CType(endoTestPropGridPresenter.ISettingsFacadeClone.ISettings, IPsettings)
        Dim newPortValue As Int32 = 9999
        clonedDevicesSettings.Port = newPortValue
        Assert.AreEqual(newPortValue, clonedDevicesSettings.Port)
        Assert.AreNotEqual(newPortValue, DevicesSettings.Port)
        endoTestPropGridPresenter.FireOKButton()
        Assert.AreEqual(newPortValue, clonedDevicesSettings.Port)
        Assert.AreEqual(newPortValue, DevicesSettings.Port)

        Assert.IsTrue(True)
    End Sub

    <Test()> Sub TestDeviceSettings()
        ScopeIII.Devices.PreLoadConfig.GetInstance.IncludeTypes()
        ScopeIII.Config.PreLoadConfig.GetInstance.IncludeTypes()

        Dim settingsPresenter As SettingsPresenter = SettingsPresenterBuilder.GetInstance.Build(CType(BartelsLibrary.SettingsType.Devices, ISFT))
        Dim endoTestPropGridPresenter As EndoTestPropGridPresenter = CType(settingsPresenter.IPropGridPresenter, EndoTestPropGridPresenter)
        endoTestPropGridPresenter.DefaultSettings()
        endoTestPropGridPresenter.SaveSettings()
        endoTestPropGridPresenter.LoadSettings()
        endoTestPropGridPresenter.FireOKButton()

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
        FormsDependencyInjector.GetInstance.UseUnitTestContainer = False
    End Sub

End Class

