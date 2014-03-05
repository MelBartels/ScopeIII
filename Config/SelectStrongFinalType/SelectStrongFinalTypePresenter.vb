#Region "imports"
Imports System.IO
Imports System.Threading
#End Region

Public Class SelectStrongFinalTypePresenter
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
    Private WithEvents pFrm As frmSelectStrongFinalType
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SelectStrongFinalTypePresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SelectStrongFinalTypePresenter = New SelectStrongFinalTypePresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As SelectStrongFinalTypePresenter
        Return New SelectStrongFinalTypePresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property MultiSelect() As Boolean
        Get
            Return pFrm.MultiSelect
        End Get
        Set(ByVal Value As Boolean)
            pFrm.MultiSelect = Value
        End Set
    End Property

    Public Function AddStrongFinalTypeToListView(ByRef ISFTFacade As ISFTFacade) As Boolean
        Return pFrm.AddStrongFinalTypeToListView(ISFTFacade)
    End Function

    Public Function DialogResult() As Windows.Forms.DialogResult
        Return pFrm.DialogResult
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrm = CType(pIMVPView, frmSelectStrongFinalType)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
