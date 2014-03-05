#Region "Imports"
#End Region

Public Class FormsDependencyInjector
    Inherits DependencyInjectorBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"

#End Region

#Region "Constructors (Singleton Pattern)"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As FormsDependencyInjector
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As FormsDependencyInjector = New FormsDependencyInjector
    End Class
#End Region

#Region "Constructors"
    'Protected Sub New()
    'End Sub

    'Public Shared Function GetInstance() As FormsDependencyInjector
    '    Return New FormsDependencyInjector
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

#Region "Presenters"
    Public Function IPropGridPresenterFactory() As IPropGridPresenter
        Return pResolver.Resolve(Of IPropGridPresenter)()
    End Function

    Public Function IUserCtrlTerminalPresenterFactory() As IUserCtrlTerminalPresenter
        Return pResolver.Resolve(Of IUserCtrlTerminalPresenter)()
    End Function

    Public Function IEncodersBoxCtrlPresenterFactory() As IEncodersBoxCtrlPresenter
        Return pResolver.Resolve(Of IEncodersBoxCtrlPresenter)()
    End Function

    Public Function IEncodersBoxSimPresenterFactory() As IEncodersBoxSimPresenter
        Return pResolver.Resolve(Of IEncodersBoxSimPresenter)()
    End Function

    Public Function IUserCtrlEncoderPresenterFactory() As IUserCtrlEncoderPresenter
        Return pResolver.Resolve(Of IUserCtrlEncoderPresenter)()
    End Function

    Public Function ITwoAxisEncoderTranslatorPresenterFactory() As ITwoAxisEncoderTranslatorPresenter
        Return pResolver.Resolve(Of ITwoAxisEncoderTranslatorPresenter)()
    End Function
#End Region

#Region "Forms and Controls"
    Public Function IUserCtrlTerminalFactory(ByRef userCtrlTerminal As UserCtrlTerminal) As IUserCtrlTerminal
        If UseUnitTestContainer Then
            Return pResolver.Resolve(Of IUserCtrlTerminal)()
        Else
            Return userCtrlTerminal
        End If
    End Function

    Public Function IFrmShowSettingsFactory() As IFrmShowSettings
        Return pResolver.Resolve(Of IFrmShowSettings)()
    End Function
#End Region

#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub buildProductionIoC()
        InversionOfControl.Initialize(Nothing)
        pResolver = DependencyResolver.GetInstance

        pResolver.Register(Of IPropGridPresenter)(GetType(PropGridPresenter))
        pResolver.Register(Of IUserCtrlTerminalPresenter)(GetType(UserCtrlTerminalPresenter))
        pResolver.Register(Of IEncodersBoxCtrlPresenter)(GetType(EncodersBoxCtrlPresenter))
        pResolver.Register(Of IEncodersBoxSimPresenter)(GetType(EncodersBoxSimPresenter))
        pResolver.Register(Of IUserCtrlEncoderPresenter)(GetType(UserCtrlEncoderPresenter))
        pResolver.Register(Of ITwoAxisEncoderTranslatorPresenter)(GetType(TwoAxisEncoderTranslatorPresenter))
        pResolver.Register(Of IUserCtrlTerminal)(GetType(UserCtrlTerminal))
        pResolver.Register(Of IFrmShowSettings)(GetType(FrmShowSettings))

        InversionOfControl.Initialize(pResolver)
    End Sub

    Protected Overrides Sub buildTestIoC()
        InversionOfControl.Initialize(Nothing)
        pResolver = DependencyResolver.GetInstance

        pResolver.Register(Of IPropGridPresenter)(GetType(EndoTestPropGridPresenter))
        pResolver.Register(Of IUserCtrlTerminalPresenter)(GetType(EndoTestUserCtrlTerminalPresenter))
        pResolver.Register(Of IEncodersBoxCtrlPresenter)(GetType(EndoTestEncodersBoxCtrlPresenter))
        pResolver.Register(Of IEncodersBoxSimPresenter)(GetType(EndoTestEncodersBoxSimPresenter))
        pResolver.Register(Of IUserCtrlEncoderPresenter)(GetType(EndoTestUserCtrlEncoderPresenter))
        pResolver.Register(Of ITwoAxisEncoderTranslatorPresenter)(GetType(EndoTestTwoAxisEncoderTranslatorPresenter))
        pResolver.Register(Of IUserCtrlTerminal)(GetType(EndoTestUserCtrlTerminal))
        pResolver.Register(Of IFrmShowSettings)(GetType(EndoTestFrmShowSettings))

        InversionOfControl.Initialize(pResolver)
    End Sub
#End Region

End Class
