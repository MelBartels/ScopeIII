#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
#End Region

Public Class SelectStrongFinalTypeMediator

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pFrmSelectStrongFinalType As frmSelectStrongFinalType
    Private pSelectStrongFinalTypePresenter As SelectStrongFinalTypePresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SelectStrongFinalTypeMediator
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SelectStrongFinalTypeMediator = New SelectStrongFinalTypeMediator
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As SelectStrongFinalTypeMediator
        Return New SelectStrongFinalTypeMediator
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function GetSelection(ByRef ISFTFacade As ISFTFacade, ByVal multiSelect As Boolean) As Boolean
        Try
            pSelectStrongFinalTypePresenter = SelectStrongFinalTypePresenter.GetInstance
            pSelectStrongFinalTypePresenter.IMVPView = New frmSelectStrongFinalType
            pSelectStrongFinalTypePresenter.AddStrongFinalTypeToListView(ISFTFacade)
            pSelectStrongFinalTypePresenter.MultiSelect = multiSelect
            pSelectStrongFinalTypePresenter.ShowDialog()
            If pSelectStrongFinalTypePresenter.DialogResult.Equals(DialogResult.OK) Then
                Return True
            End If
        Catch tae As ThreadAbortException
            Debug.WriteLine("form blown away by thread abort")
        End Try
        Return False
    End Function

    Public Sub CloseForm()
        pSelectStrongFinalTypePresenter.Close()
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
