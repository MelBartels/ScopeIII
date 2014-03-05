Imports NUnit.Framework

<TestFixture()> Public Class PropGridPresenterTests

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
        FormsDependencyInjector.GetInstance.UseUnitTestContainer = True
    End Sub

    <Test()> Sub TestISettingsUpdateCancel()
        Dim endoTestPropGridPresenter As EndoTestPropGridPresenter = CType(FormsDependencyInjector.GetInstance.IPropGridPresenterFactory, EndoTestPropGridPresenter)
        endoTestPropGridPresenter.IMVPUserCtrl = New UserCtrlPropGrid

        Dim IPsettingsFacade As SettingsFacadeTemplate = SettingsFacadeTemplate.GetInstance.Build(IO.IPsettings.GetInstance, GetType(IPsettings).Name)
        IPsettingsFacade.ISettings.SetToDefaults()
        endoTestPropGridPresenter.SettingsFacadeTemplate = IPsettingsFacade
        endoTestPropGridPresenter.CloneSettingsFacadeTemplate()

        Dim IPSettings As IPsettings = CType(endoTestPropGridPresenter.SettingsFacadeTemplate.ISettings, IPsettings)
        Dim clonedIPSettings As IPsettings = CType(endoTestPropGridPresenter.ISettingsFacadeClone.ISettings, IPsettings)

        ' verify that port and its clone were built properly
        Assert.IsTrue(IPSettings.Port() > 0)
        Assert.AreEqual(IPSettings.Port, clonedIPSettings.Port)
        Dim defaultPortValue As Int32 = IPSettings.Port()

        ' make change and accept it
        Dim newPortValue As Int32 = 9999
        clonedIPSettings.Port = newPortValue
        Assert.AreEqual(newPortValue, clonedIPSettings.Port)
        Assert.AreNotEqual(newPortValue, IPSettings.Port)
        endoTestPropGridPresenter.FireOKButton()
        Assert.AreEqual(newPortValue, clonedIPSettings.Port)
        Assert.AreEqual(newPortValue, IPSettings.Port)

        ' make another change but cancel it
        clonedIPSettings.Port = defaultPortValue
        Assert.AreEqual(defaultPortValue, clonedIPSettings.Port)
        Assert.AreNotEqual(defaultPortValue, IPSettings.Port)
        endoTestPropGridPresenter.FireCancelButton()
        Assert.AreNotEqual(defaultPortValue, clonedIPSettings.Port)
        Assert.AreNotEqual(defaultPortValue, IPSettings.Port)

        ' make another change and accept it this time
        clonedIPSettings.Port = defaultPortValue
        Assert.AreEqual(defaultPortValue, clonedIPSettings.Port)
        Assert.AreNotEqual(defaultPortValue, IPSettings.Port)
        endoTestPropGridPresenter.FireOKButton()
        Assert.AreEqual(defaultPortValue, clonedIPSettings.Port)
        Assert.AreEqual(defaultPortValue, IPSettings.Port)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
        FormsDependencyInjector.GetInstance.UseUnitTestContainer = False
    End Sub

End Class

