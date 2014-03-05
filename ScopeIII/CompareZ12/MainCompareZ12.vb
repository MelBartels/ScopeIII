Public Class MainCompareZ12
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

    'Public Shared Function GetInstance() As MainCompareZ12
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainCompareZ12 = New MainCompareZ12
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainCompareZ12
        Return New MainCompareZ12
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim compareZ12Presenter As CompareZ12Presenter = ScopeIII.CompareZ12Presenter.GetInstance
        compareZ12Presenter.IMVPView = New FrmCompareZ12
        compareZ12Presenter.Title = Application.ProductName _
                & "  Z1 versus Z2 Error ('X' axis azimuth error in arcminutes, 'Y' altitude axis in degrees elevation)."

        Dim multiXYDataRenderer As MultiXYDataRenderer = Forms.MultiXYDataRenderer.GetInstance
        Dim multiXYData As MultiXYData = Forms.MultiXYData.GetInstance
        multiXYDataRenderer.ObjectToRender = multiXYData
        compareZ12Presenter.DataModel = multiXYDataRenderer

        compareZ12Presenter.ShowDialog()
    End Sub
#End Region

End Class
