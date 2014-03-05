Imports NUnit.Framework

<TestFixture()> Public Class IOPresenterIOTest

    Public Sub New()
        MyBase.New()
    End Sub

    <SetUp()> Public Sub Init()
    End Sub

    <Test()> Public Sub TestBuildShutdown()
        Dim IIOPresenter As IIOPresenter = IOPresenterIO.GetInstance

        IIOPresenter.BuildIO(CType(IOType.SerialPort, ISFT))
        Assert.IsNotNull(IIOPresenter.IIO)

        IIOPresenter.IIO.Open()
        Assert.IsTrue(IIOPresenter.IIO.isOpened)

        IIOPresenter.ShutdownIO()
        Assert.IsFalse(IIOPresenter.IIO.isOpened)

        Assert.IsTrue(True)
    End Sub

    <TearDown()> Public Sub Dispose()
    End Sub
End Class
