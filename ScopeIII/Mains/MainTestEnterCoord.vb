Public Class MainTestEnterCoord
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

    'Public Shared Function GetInstance() As MainTestEnterCoord
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainTestEnterCoord = New MainTestEnterCoord
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainTestEnterCoord
        Return New MainTestEnterCoord
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim enterCoordPresenter As EnterCoordPresenter = Forms.EnterCoordPresenter.GetInstance
        enterCoordPresenter.IMVPView = New FrmEnterCoord
        enterCoordPresenter.ICoordPresenter.SetCoordinateName("Test")
        enterCoordPresenter.ShowDialog()

        If CType(enterCoordPresenter.IMVPView, Windows.Forms.Form).DialogResult.equals(DialogResult.OK) Then
            MessageBoxCoord.GetInstance.Show(enterCoordPresenter.Coordinate)
        Else
            AppMsgBox.Show(BartelsLibrary.Constants.NothingEntered)
        End If
    End Sub
#End Region

End Class
