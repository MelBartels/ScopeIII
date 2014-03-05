#Region "Imports"
#End Region

Public Class EndoTestPropGridPresenter
    Inherits PropGridPresenter

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
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As EndoTestPropGridPresenter
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As EndoTestPropGridPresenter = New EndoTestPropGridPresenter
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Shadows Function GetInstance() As EndoTestPropGridPresenter
        Return New EndoTestPropGridPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public ReadOnly Property ISettingsFacadeClone() As SettingsFacadeTemplate
        Get
            Return pSettingsFacadeClone
        End Get
    End Property

    Public Sub FireOKButton()
        ok()
    End Sub

    Public Sub FireCancelButton()
        cancel()
    End Sub

    Public Overloads Sub DefaultSettings()
        MyBase.defaultSettings()
    End Sub

    Public Overloads Sub LoadSettings()
        MyBase.loadSettings()
    End Sub

    Public Overloads Sub SaveSettings()
        MyBase.saveSettings()
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
