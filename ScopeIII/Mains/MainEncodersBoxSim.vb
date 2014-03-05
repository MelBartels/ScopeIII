Public Class MainEncodersBoxSim
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

    'Public Shared Function GetInstance() As MainEncodersBoxSim
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainEncodersBoxSim = New MainEncodersBoxSim
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainEncodersBoxSim
        Return New MainEncodersBoxSim
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim EncodersBoxSimPresenter As EncodersBoxSimPresenter = Forms.EncodersBoxSimPresenter.GetInstance
        EncodersBoxSimPresenter.IMVPView = New FrmEncodersBoxSim
        EncodersBoxSimPresenter.ShowDialog()
    End Sub
#End Region

End Class
