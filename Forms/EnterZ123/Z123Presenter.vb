#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class Z123Presenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pFrmEnterZ123 As frmEnterZ123
    Private pUserCtrlZ123Presenter As userCtrlZ123Presenter

    Private pUseCorrections As Boolean
    Private pFabErrors As FabErrors
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Z123Presenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Z123Presenter = New Z123Presenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Z123Presenter
        Return New Z123Presenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property UseCorrections() As Boolean
        Get
            Return pUseCorrections
        End Get
        Set(ByVal Value As Boolean)
            pUseCorrections = Value
        End Set
    End Property

    Public Property FabErrors() As FabErrors
        Get
            Return pFabErrors
        End Get
        Set(ByVal Value As FabErrors)
            pFabErrors = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEnterZ123 = CType(IMVPView, FrmEnterZ123)
        AddHandler pFrmEnterZ123.OK, AddressOf ok

        pUserCtrlZ123Presenter = UserCtrlZ123Presenter.GetInstance
        pUserCtrlZ123Presenter.IMVPUserCtrl = pFrmEnterZ123.UserCtrlZ123

        FabErrors = Coordinates.FabErrors.GetInstance
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub ok()
        UseCorrections = pUserCtrlZ123Presenter.UseCorrections

        If pUserCtrlZ123Presenter.UseCorrections Then
            FabErrors = pUserCtrlZ123Presenter.FabErrors
        End If

        pFrmEnterZ123.DialogResult.equals(DialogResult.OK)
        pFrmEnterZ123.Close()
    End Sub
#End Region

End Class
