#Region "Imports"
#End Region

Public MustInherit Class MVPUserCtrlPresenterBase
    Implements IMVPUserCtrlPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pIMVPUserCtrl As IMVPUserCtrl
    Protected pDataModel As Object
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MVPUserCtrlPresenterBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MVPUserCtrlPresenterBase = New MVPUserCtrlPresenterBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As MVPUserCtrlPresenterBase
    '    Return New MVPUserCtrlPresenterBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IMVPUserCtrl() As IMVPUserCtrl Implements IMVPUserCtrlPresenter.IMVPUserCtrl
        Get
            Return pIMVPUserCtrl
        End Get
        Set(ByVal Value As IMVPUserCtrl)
            pIMVPUserCtrl = Value
            init()
        End Set
    End Property

    Public Overridable Property DataModel() As Object Implements IMVPUserCtrlPresenter.DataModel
        Get
            Return pDataModel
        End Get
        Set(ByVal Value As Object)
            pDataModel = Value
            loadViewFromModel()
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overridable Sub init()
    End Sub

    Protected MustOverride Sub loadViewFromModel()
    Protected MustOverride Sub saveToModel()
    Protected MustOverride Sub viewUpdated()
#End Region

End Class
