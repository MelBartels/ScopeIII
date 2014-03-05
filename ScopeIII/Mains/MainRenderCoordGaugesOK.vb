Public Class MainRenderCoordGaugesOK
    Inherits MainPrototype

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainRenderCoordGaugesOK
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainRenderCoordGaugesOK = New MainRenderCoordGaugesOK
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainRenderCoordGaugesOK
        Return New MainRenderCoordGaugesOK
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim GaugeCoordsOKPresenter As RenderCoordGaugesOKPresenter = Forms.RenderCoordGaugesOKPresenter.GetInstance
        GaugeCoordsOKPresenter.IMVPView = New FrmRenderCoordGaugesOK
        GaugeCoordsOKPresenter.ShowDialog()
    End Sub
#End Region

End Class
