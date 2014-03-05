#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
#End Region

Public Class UserCtrlOneTwoPresenter
    Inherits MVPUserCtrlPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlOneTwo As UserCtrlOneTwo

    Private pUserCtrlPositionPresenterOne As UserCtrlPositionPresenter
    Private pUserCtrlPositionPresenterTwo As UserCtrlPositionPresenter
    Private pUserCtrlZ123Presenter As UserCtrlZ123Presenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlOneTwoPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlOneTwoPresenter = New UserCtrlOneTwoPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlOneTwoPresenter
        Return New UserCtrlOneTwoPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub UpdateDataModelFromView()
        oneTwoPresenterDataModel.UseCorrections = pUserCtrlZ123Presenter.UseCorrections
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlOneTwo = CType(IMVPUserCtrl, UserCtrlOneTwo)

        pUserCtrlPositionPresenterOne = UserCtrlPositionPresenter.GetInstance
        pUserCtrlPositionPresenterOne.IMVPUserCtrl = pUserCtrlOneTwo.UserCtrlPositionOne

        pUserCtrlPositionPresenterTwo = UserCtrlPositionPresenter.GetInstance
        pUserCtrlPositionPresenterTwo.IMVPUserCtrl = pUserCtrlOneTwo.UserCtrlPositionTwo

        pUserCtrlZ123Presenter = UserCtrlZ123Presenter.GetInstance
        pUserCtrlZ123Presenter.IMVPUserCtrl = pUserCtrlOneTwo.UserCtrlZ123
    End Sub

    Protected Overrides Sub loadViewFromModel()
        With oneTwoPresenterDataModel()
            pUserCtrlPositionPresenterOne.DataModel = .One
            pUserCtrlPositionPresenterTwo.DataModel = .Two
            pUserCtrlZ123Presenter.DataModel = New Object() {.UseCorrections, .FabErrors}
        End With
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function oneTwoPresenterDataModel() As OneTwoPresenterDataModel
        Return CType(DataModel, OneTwoPresenterDataModel)
    End Function
#End Region

End Class
