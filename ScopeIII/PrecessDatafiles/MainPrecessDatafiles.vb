Public Class MainPrecessDatafiles
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

    'Public Shared Function GetInstance() As MainPrecessDatafiles
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainPrecessDatafiles = New MainPrecessDatafiles
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainPrecessDatafiles
        Return New MainPrecessDatafiles
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim precessDataFilesPresenter As PrecessDatafilesPresenter = ScopeIII.PrecessDatafilesPresenter.GetInstance
        precessDataFilesPresenter.IMVPView = New FrmPrecessDatafiles
        precessDataFilesPresenter.ShowDialog()
    End Sub
#End Region

End Class
