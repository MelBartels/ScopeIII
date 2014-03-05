#Region "imports"
Imports BartelsLibrary.DelegateSigs
#End Region

Public Class QueryDatafilesPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event ObjectSelected(ByVal selectedLWPosition As LWPosition)
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmQueryDatafiles As frmQueryDatafiles
    Private WithEvents pUserCtrlObjectLibraryPresenter As UserCtrlObjectLibraryPresenter
    Private pProgressMediator As ProgressMediator
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As QueryDatafilesPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As QueryDatafilesPresenter = New QueryDatafilesPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As QueryDatafilesPresenter
        Return New QueryDatafilesPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmQueryDatafiles = CType(IMVPView, FrmQueryDatafiles)
        AddHandler pFrmQueryDatafiles.Save, AddressOf save

        pUserCtrlObjectLibraryPresenter = UserCtrlObjectLibraryPresenter.GetInstance
        pUserCtrlObjectLibraryPresenter.IMVPUserCtrl = pFrmQueryDatafiles.UserCtrlObjectLibrary
        AddHandler pUserCtrlObjectLibraryPresenter.ObjectSelected, AddressOf objectSelectedHandler

        pProgressMediator = ProgressMediator.GetInstance
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub objectSelectedHandler(ByVal selectedLWPosition As LWPosition)
        RaiseEvent ObjectSelected(selectedLWPosition)
    End Sub

    Private Sub save()
        pProgressMediator.ProcessStartup(New DelegateObjDoWorkEventArgs(AddressOf saveCallback))
    End Sub

    Private Sub saveCallback(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim library As Library = pUserCtrlObjectLibraryPresenter.Library

        ' count of .ShowMsgs
        pProgressMediator.ProgressBarMaxValue = 2 + library.Objects.Count
        pProgressMediator.ShowMsg("Saving datafile objects to file: " & pFrmQueryDatafiles.Filename & "...Please wait.")

        library.ObservableImp.Attach(CType(pProgressMediator, IObserver))
        library.Save(pFrmQueryDatafiles.Filename)
        library.ObservableImp.Detach(CType(pProgressMediator, IObserver))

        pProgressMediator.ShowMsg("Finished.")
        pProgressMediator.ProcessShutdown()
    End Sub
#End Region

End Class
