#Region "Imports"
#End Region

Public MustInherit Class MVPPresenterBase
    Implements IMVPPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pIMVPView As IMVPView
    Protected pDataModel As Object
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MVPPresenterBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MVPPresenterBase = New MVPPresenterBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As MVPPresenterBase
    '    Return New MVPPresenterBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overridable Property IMVPView() As IMVPView Implements IMVPPresenter.IMVPView
        Get
            Return pIMVPView
        End Get
        Set(ByVal Value As IMVPView)
            pIMVPView = Value
            init()
        End Set
    End Property

    Public Overridable Property DataModel() As Object Implements IMVPPresenter.DataModel
        Get
            Return pDataModel
        End Get
        Set(ByVal Value As Object)
            pDataModel = Value
            loadViewFromModel()
        End Set
    End Property

    Public Overridable Sub ShowDialog() Implements IMVPPresenter.ShowDialog
        IMVPView.ShowDialog()
    End Sub

    Public Overridable Sub Show() Implements IMVPPresenter.Show
        IMVPView.Show()
    End Sub

    Public Overridable Sub Close() Implements IMVPPresenter.Close
        pIMVPView.Close()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overridable Sub init()

    End Sub

    Protected MustOverride Sub loadViewFromModel()
    Protected MustOverride Sub saveToModel()
    Protected MustOverride Sub viewUpdated()
#End Region

End Class
