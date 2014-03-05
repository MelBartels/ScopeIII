#Region "Imports"
#End Region

Public Class ProgressBarPresenter
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
    Private pFrmProgressBar As FrmProgressBar
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ProgressBarPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ProgressBarPresenter = New ProgressBarPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ProgressBarPresenter
        Return New ProgressBarPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property FormTitle() As String
        Get
            Return pFrmProgressBar.FormTitle
        End Get
        Set(ByVal Value As String)
            pFrmProgressBar.FormTitle = Value
        End Set
    End Property

    Public Property IObservable() As IObservable
        Get
            Return pFrmProgressBar.IObservable
        End Get
        Set(ByVal Value As IObservable)
            pFrmProgressBar.IObservable = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmProgressBar = CType(IMVPView, FrmProgressBar)
    End Sub

    Protected Overrides Sub loadViewFromModel()
        If DataModel IsNot Nothing Then
            If pFrmProgressBar.InvokeRequired Then
                pFrmProgressBar.Invoke(New Windows.Forms.MethodInvoker(AddressOf loadViewFromModel))
            Else
                pFrmProgressBar.ProgressPercent(CDbl(CType(DataModel, Object())(0)))
                pFrmProgressBar.ProgressText(CStr(CType(DataModel, Object())(1)))
            End If
        End If
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

#End Region

End Class
