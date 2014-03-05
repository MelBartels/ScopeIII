#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class CoordinateObserverPresenter
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
    Private pUserCtrlCoordPresenter As UserCtrlCoordPresenter
    Private pName As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CoordinateObserverPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordinateObserverPresenter = New CoordinateObserverPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As CoordinateObserverPresenter
        Return New CoordinateObserverPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property UserCtrlCoordPresenter() As UserCtrlCoordPresenter
        Get
            Return pUserCtrlCoordPresenter
        End Get
        Set(ByVal Value As UserCtrlCoordPresenter)
            pUserCtrlCoordPresenter = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlCoordPresenter = Forms.UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenter.IMVPUserCtrl = CType(IMVPView, FrmCoordinateObserver).UserCtrlCoord
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
