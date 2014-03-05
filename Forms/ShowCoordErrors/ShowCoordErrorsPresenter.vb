#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class ShowCoordErrorsPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event OK()
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmShowCoordErrors As FrmShowCoordErrors
    Private pUserCtrlCoordErrorsPresenter As UserCtrlCoordErrorsPresenter
    Private pObjectName As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ShowCoordErrorsPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ShowCoordErrorsPresenter = New ShowCoordErrorsPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ShowCoordErrorsPresenter
        Return New ShowCoordErrorsPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ObjectName() As String
        Get
            Return pObjectName
        End Get
        Set(ByVal value As String)
            pObjectName = value
        End Set
    End Property

    Public ReadOnly Property UncorrectedUserCtrl2AxisCoordPresenter() As UserCtrl2AxisCoordPresenter
        Get
            Return pUserCtrlCoordErrorsPresenter.UncorrectedUserCtrl2AxisCoordPresenter
        End Get
    End Property

    Public ReadOnly Property CorrectedUserCtrl2AxisCoordPresenter() As UserCtrl2AxisCoordPresenter
        Get
            Return pUserCtrlCoordErrorsPresenter.CorrectedUserCtrl2AxisCoordPresenter
        End Get
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmShowCoordErrors = CType(IMVPView, FrmShowCoordErrors)
        AddHandler pFrmShowCoordErrors.OK, AddressOf okHandler
        pUserCtrlCoordErrorsPresenter = UserCtrlCoordErrorsPresenter.GetInstance
        pUserCtrlCoordErrorsPresenter.IMVPUserCtrl = pFrmShowCoordErrors.UserCtrlCoordErrors
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub okHandler()
        RaiseEvent OK()
    End Sub
#End Region

End Class
